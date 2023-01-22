//============================================================
// Student Number : S10218941, S10223353
// Student Name : Mandy Tang, Felicia Chua
// Module Group : T11
//============================================================

using System;

namespace Programming_2_Assignment
{
    class Adult : Ticket
    {
        //private properties
        private bool popcornOffer;

        //public properties
        public bool PopcornOffer { get; set; }

        //contructors + methods
        public Adult() { }

        public Adult(Screening s, string sn, bool p) : base(s, sn)
        {
            PopcornOffer = p;
        }

        public override double CalculatePrice()
        {
            double price = 0;
            if ((Screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Monday) || (Screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Tuesday) || (Screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Wednesday) || (Screening.ScreeningDateTime.DayOfWeek == DayOfWeek.Thursday))
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

            if (PopcornOffer == true)
            {
                price = price + 3;
            }

            return price;
        }

        public override string ToString()
        {
            return base.ToString() + "\n" + "Popcorn Offer: " + PopcornOffer;
        }
    }
}
