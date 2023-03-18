
using System.Windows.Controls;

namespace Lab2
{
    /// <summary>
    /// Interaction logic for PersonOutput.xaml
    /// </summary>
    public partial class PersonOutput : Page
    {

        public PersonOutput(object data)
        {
            InitializeComponent();

            Person person = (Person)data;

            this.nameTxt.Text = person.Name;
            this.surnameTxt.Text = person.Surname;
            this.emailTxt.Text = person.Email;
            this.birthdayTxt.Text = person.Birthday.ToString();
            this.isAdultTxt.Text = person.IsAdult ? "yes" : "no";
            this.zodiacTxt.Text = person.Zodiac;
            this.chineseZodiacTxt.Text = person.ChineseZodiac;

            if (person.IsBirthday)
            {
                this.happyBirhdayTxt.Text = "Happy birthday";
            }
        }
    }
}
