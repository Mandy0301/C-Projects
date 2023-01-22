//============================================================
// Student Number : S10218941, S10223353
// Student Name : Mandy Tang, Felicia Chua
// Module Group : T11
//============================================================

using System;

namespace Programming_2_Assignment
{
    class Screening : IComparable<Screening>
    {
        //private properties
        private int screeningNo;
        private DateTime screeningDateTime;
        private string screeningType;
        private int seatsRemaining;
        private Cinema cinema;
        private Movie movie;

        //public properties
        public int ScreeningNo { get; set; }
        public DateTime ScreeningDateTime { get; set; }
        public string ScreeningType { get; set; }
        public int SeatsRemaining { get; set; }
        public Cinema Cinema { get; set; }
        public Movie Movie { get; set; }

        //method + constructor
        public Screening() { }

        public Screening(int n, DateTime dt, int s, string t, Cinema c, Movie m)
        {
            ScreeningNo = n;
            ScreeningDateTime = dt;
            SeatsRemaining = s;
            ScreeningType = t;
            Cinema = c;
            Movie = m;
        }

        public override string ToString()
        {
            return "Screening Number: " + ScreeningNo + "\n" + "Screening DateTime: " + ScreeningDateTime + "\n" + "Seats Remaining:" + SeatsRemaining + "\n" + "Screening Type: " + ScreeningType + "\n" + "Seats Remaining: " + SeatsRemaining + "\n" + "Cinema: " + Cinema + "\n" + "Movie: " + Movie;
        }

        public int CompareTo(Screening e) // sort by Id
        {
            if (ScreeningDateTime > e.ScreeningDateTime)
                return 1;

            else if (ScreeningDateTime == e.ScreeningDateTime)
                return 0;

            else
                return -1;
        }

    }
}
