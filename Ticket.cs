//============================================================
// Student Number : S10218941, S10223353
// Student Name : Mandy Tang, Felicia Chua
// Module Group : T11
//============================================================

using System;

namespace Programming_2_Assignment
{
    abstract class Ticket : IComparable<Ticket>
    {
        //private properties
        private Screening screening;
        private string seatno;

        //public properties
        public Screening Screening { get; set; }
        public string Seatno { get; set; }

        //constructors + methods
        public Ticket() { }

        public Ticket(Screening s, string sn)
        {
            Screening = s;
            Seatno = sn;
        }

        public abstract double CalculatePrice();

        public override string ToString()
        {
            return "Screening: " + Screening;
        }

        public int CompareTo(Ticket t) // sort by Id
        {
            return string.Compare(Seatno, t.Seatno);
        }

    }
}

