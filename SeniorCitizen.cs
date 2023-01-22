//============================================================
// Student Number : S10218941, S10223353
// Student Name : Mandy Tang, Felicia Chua
// Module Group : T11
//============================================================

using System;

namespace Programming_2_Assignment
{
    class SeniorCitizen : Ticket
    {
        //private properties
        private int yearOfBirth;

        //public properties
        public int YearOfBirth { get; set; }

        //contructors + methods
        public SeniorCitizen() { }

        public SeniorCitizen(Screening s, string sn, int y) : base(s, sn)
        {
            YearOfBirth = y;
        }

        public override double CalculatePrice()
        {
            double price = 0;
            if ((Screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Monday) || (Screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Tuesday) || (Screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Wednesday) || (Screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Thursday))
            {
                if ((Screening.ScreeningDateTime - Screening.Movie.OpeningDate).Days > 7)
                {
                    if (Screening.ScreeningType == "2D")
                    {
                        price = 5;

                    }

                    else if (Screening.ScreeningType == "3D")
                    {
                        price = 6;

                    }
                }

                else
                {
                    if (Screening.ScreeningType == "2D")
                    {
                        price = 8.50;
                    }

                    else if (Screening.ScreeningType == "3D")
                    {
                        price = 11;
                    }
                }
            }

            else
            {
                if (Screening.ScreeningType == "2D")
                {
                    price = 12.50;
                }

                else if (Screening.ScreeningType == "3D")
                {
                    price = 14;
                }
            }

            return price;
        }

        public override string ToString()
        {
            return base.ToString() + "\n" + "Year Of Birth: " + YearOfBirth;
        }
    }
}
