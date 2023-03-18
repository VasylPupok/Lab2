
using System;
using System.Text.RegularExpressions;

namespace Lab2
{
    internal class Person
    {
        // user info
        public string Name { get; private set; }

        public string Surname { get; private set; }
        public string Email { get; private set; }
        public DateTime Birthday { get; private set; }

        // some other stuff, related to birthday
        public bool IsAdult { get; private set; }
        public string Zodiac { get; private set; }
        public string ChineseZodiac { get; private set; }
        public bool IsBirthday { get; private set; }

        // max age
        public static readonly short MAX_AGE = 135;

        // some stuff to set all fields (maybe I should rewrite it)

        private static string[] CHINESE_SIGNS = new string[]
        {
            "Monkey",
            "Rooster",
            "Dog",
            "Pig",
            "Rat",
            "Ox",
            "Tiger",
            "Rabbit",
            "Dragon",
            "Snake",
            "Horse",
            "Goat"
        };

        public string calculateZodiac(DateTime birthday)
        {
            switch (birthday.Month)
            {
                case 1:
                    if (birthday.Day < 20)
                    {
                        return "Capricorn";
                    }
                    else
                    {
                        return "Aquarius";
                    }
                case 2:
                    if (birthday.Day < 19)
                    {
                        return "Aquarius";
                    }
                    else
                    {
                        return "Pisces";
                    }
                case 3:
                    if (birthday.Day < 21)
                    {
                        return "Pisces";
                    }
                    else
                    {
                        return "Aries";
                    }
                case 4:
                    if (birthday.Day < 20)
                    {
                        return "Aries";
                    }
                    else
                    {
                        return "Taurus";
                    }
                case 5:
                    if (birthday.Day < 21)
                    {
                        return "Taurus";
                    }
                    else
                    {
                        return "Gemini";
                    }
                case 6:
                    if (birthday.Day < 22)
                    {
                        return "Gemini";
                    }
                    else
                    {
                        return "Cancer";
                    }
                case 7:
                    if (birthday.Day < 23)
                    {
                        return "Cancer";
                    }
                    else
                    {
                        return "Leo";
                    }
                case 8:
                    if (birthday.Day < 24)
                    {
                        return "Leo";
                    }
                    else
                    {
                        return "Virgo";
                    }
                case 9:
                    if (birthday.Day < 24)
                    {
                        return "Virgo";
                    }
                    else
                    {
                        return "Libra";
                    }
                case 10:
                    if (birthday.Day < 23)
                    {
                        return "Libra";
                    }
                    else
                    {
                        return "Scorpio";
                    }
                case 11:
                    if (birthday.Day < 23)
                    {
                        return "Scorpio";
                    }
                    else
                    {
                        return "Sagittarius";
                    }
                case 12:
                    if (birthday.Day < 22)
                    {
                        return "Sagittarius";
                    }
                    else
                    {
                        return "Capricorn";
                    }
                default:
                    return "Error";
            }
        }

        

        private void setFullName(string name, string surname)
        {
            if (Regex.IsMatch(name, "[\\w]+", RegexOptions.IgnoreCase))
            {
                this.Name = name;
            }
            else
            {
                throw new Exception("Illegal name value: " + name);
            }

            if (Regex.IsMatch(surname, "[\\w]+", RegexOptions.IgnoreCase))
            {
                this.Surname = surname;
            }
            else
            {
                throw new Exception("Illegal surname value: " + surname);
            }
        }

        private void setEmail(string email)
        {
            if (Regex.IsMatch(
                email,
                "[a-zA-Z0-9_.]+@[a-zA-Z0-9_.]+",
                RegexOptions.IgnoreCase)
                )
            {
                this.Email = email;
            }
            else
            {
                throw new Exception("invalid email input:" + email);
            }
        }

        private void setBirthday(DateTime birthday)
        {
            if (DateTime.Now < birthday)
            {
                throw new Exception("Illegal birthday value: birthday cannot be in future. Value: " + birthday);
            }
            Birthday = birthday;


            IsAdult = (DateTime.Now.Year - birthday.Year > 18) ||
                (DateTime.Now.Year - birthday.Year == 18 && DateTime.Now.DayOfYear >= birthday.DayOfYear);

            Zodiac = calculateZodiac(birthday);
            ChineseZodiac = CHINESE_SIGNS[birthday.Year % 12];

            // TODO ask if it is ok
            IsBirthday = DateTime.Now.DayOfYear == birthday.DayOfYear;
        }

        // constructors

        public Person(string name, string surname, string email, DateTime birthday)
        {
            setFullName(name, surname);
            setBirthday(birthday);
            setEmail(email);
        }

        public Person(string name, string surname, string email)
        {
            setFullName(name, surname);
            setEmail(email);
        }

        public Person(string name, string surname, DateTime birthday)
        {
            setFullName(name, surname);
            setBirthday(birthday);
        }
    }
}
