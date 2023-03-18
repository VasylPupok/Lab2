using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;

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
            string name = this.nameInput.Text;
            string surname = this.surnameInput.Text;
            string email = this.emailInput.Text;
            DateTime? birthday = this.dateInput.SelectedDate;

            bool[] results = Task.WhenAll(new List<Task<bool>> {
                validateName(name),
                validateSurname(surname),
                validateEmail(email),
                validateDate(birthday),
            }).Result;

            return results.All(x => x);
        }

        private Task<bool> validateName(string name)
        {
            System.Threading.Thread.Sleep(5000);
            return
                Task.Run(() => !string.IsNullOrEmpty(name) &&
                Regex.IsMatch(name, "[\\w]+", RegexOptions.IgnoreCase));
        }

        private Task<bool> validateSurname(string surname)
        {
            return
                Task.Run(() => !string.IsNullOrEmpty(surname) &&
                Regex.IsMatch(surname, "[\\w]+", RegexOptions.IgnoreCase));
        }

        private Task<bool> validateEmail(string email)
        {
            return
                Task.Run(() => !string.IsNullOrEmpty(email) &&
                Regex.IsMatch(email, "[\\w_.]+@[\\w_.]+", RegexOptions.IgnoreCase));
        }

        private Task<bool> validateDate(DateTime? date)
        {
            return
                Task.Run(() => date.HasValue &&
                date.Value <= DateTime.Now &&
                (DateTime.Now.Year - date.Value.Year) <= Person.MAX_AGE);
        }

    }
}
