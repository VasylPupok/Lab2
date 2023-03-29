using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Navigation;
using System.Xml.Linq;
using System.Windows.Interop;

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
            lock (this)
            {

                this.IsEnabled = false;

                string name = this.nameInput.Text;
                string surname = this.surnameInput.Text;
                string email = this.emailInput.Text;
                DateTime? birthday = this.dateInput.SelectedDate;

                Thread worker = new Thread(
                async () =>
                    {
                        if (await Task.Run(() =>
                            {
                                return allInputsValid(name, surname, email, birthday);
                            })
                        )
                        {
                            Person p = new Person(name, surname, email, birthday.Value);
                            this.Dispatcher.Invoke(() => this.NavigationService.Navigate(new PersonOutput(p)));
                        }
                        this.Dispatcher.Invoke(() => this.IsEnabled = true);
                    }

                    );

                worker.IsBackground = true;
                worker.SetApartmentState(ApartmentState.STA);
                worker.Start();
            }
        }

        private bool allInputsValid(
                string name,
                string surname,
                string email,
                DateTime? birthday
            )
        {

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
            Thread.Sleep(1000);
            return
                Task.Run(() =>
                !string.IsNullOrEmpty(name) && Regex.IsMatch(name, "[\\w]+", RegexOptions.IgnoreCase));
        }

        private Task<bool> validateSurname(string surname)
        {
            return
                Task.Run(() =>
                !string.IsNullOrEmpty(surname) && Regex.IsMatch(surname, "[\\w]+", RegexOptions.IgnoreCase));
        }

        private Task<bool> validateEmail(string email)
        {
            return
                Task.Run(
                    () => !string.IsNullOrEmpty(email) &&
                    Regex.IsMatch(email, "[\\w_.]+@[\\w_.]+", RegexOptions.IgnoreCase)
                        );
        }

        private Task<bool> validateDate(DateTime? date)
        {
            return Task.Run(() =>
                    date.HasValue &&
                    date.Value <= DateTime.Now &&
                    (DateTime.Now.Year - date.Value.Year) <= Person.MAX_AGE
                    );
        }
    }
}
