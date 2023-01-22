//============================================================
// Student Number : S10218941, S10223353
// Student Name : Mandy Tang, Felicia Chua
// Module Group : T11
//============================================================

using System;

namespace Programming_2_Assignment
{
    class Student : Ticket
    {
        //private properties
        private string levelOfStudy;

        //public properties
        public string LevelOfStudy { get; set; }

        //contructors + methods
        public Student() { }

        public Student(Screening s, string sn, string l) : base(s, sn)
        {
            LevelOfStudy = l;
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
                        price = 7;

                    }

                    else if (Screening.ScreeningType == "3D")
                    {
                        price = 8;

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
            return base.ToString() + "\n" + "Level Of Study: " + LevelOfStudy;
        }
    }
}
