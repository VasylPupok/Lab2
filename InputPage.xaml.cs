using System;
using System.Text.RegularExpressions;
using System.Windows.Controls;


namespace Lab2
{
    /// <summary>
    /// Interaction logic for InputPage.xaml
    /// </summary>
    public partial class InputPage : Page
    {
        public InputPage()
        {
            InitializeComponent();
        }

        public void submitButtonClicked(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            if (allInputsValid())
            {
                Person p = new Person(
                    this.nameInput.Text,
                    this.surnameInput.Text,
                    this.emailInput.Text,
                    this.dateInput.SelectedDate.Value
                    );
                PersonOutput personOutputPage = new PersonOutput(p);
                this.NavigationService.Navigate(personOutputPage);
            }
            this.IsEnabled = true;
        }

        private bool allInputsValid()
        {

            // TODO make this stuff multithreaded

            return
                nameIsValid(this.nameInput.Text) &&
                surnameIsValid(this.surnameInput.Text) &&
                emailIsValid(this.emailInput.Text) &&
                dateIsValid(this.dateInput.SelectedDate);
        }

        private bool nameIsValid(string name)
        {
            return
                !string.IsNullOrEmpty(name) &&
                Regex.IsMatch(name, "[\\w]+", RegexOptions.IgnoreCase);
        }

        private bool surnameIsValid(string surname)
        {
            return
                !string.IsNullOrEmpty(surname) &&
                Regex.IsMatch(surname, "[\\w]+", RegexOptions.IgnoreCase);
        }

        private bool emailIsValid(string email)
        {
            return
                !string.IsNullOrEmpty(email) &&
                Regex.IsMatch(email, "[\\w_.]+@[\\w_.]+", RegexOptions.IgnoreCase);
        }

        private bool dateIsValid(DateTime? date)
        {
            return
                date.HasValue &&
                date.Value <= DateTime.Now &&
                (DateTime.Now.Year - date.Value.Year) <= Person.MAX_AGE;
        }

    }
}
