//============================================================
// Student Number : S10218941, S10223353
// Student Name : Mandy Tang, Felicia Chua
// Module Group : T11
//============================================================

using System;
using System.Collections.Generic;

namespace Programming_2_Assignment
{
    class Cinema
    {
        //private properties
        private string name;
        private int hallNo;
        private int capacity;
        private List<int> seatlist;

        //public properties
        public string Name { get; set; }
        public int HallNo { get; set; }
        public int Capacity { get; set; }
        public List<string> Seatlist { get; set; } = new List<string>();


        //method + constructor
        public Cinema() { }

        public Cinema(string n, int hn, int c)
        {
            Name = n;
            HallNo = hn;
            Capacity = c;
        }

        public void InitSeatList()
        {
            for (int i = 1; i <= Capacity; i++)
            {
                string seat = " ";

                if (i < 10)
                {
                    seat = " 0";
                }

                seat = seat + i + " ";
                
                Seatlist.Add(seat);
            }
        }

        public override string ToString()
        {
            return "Name: " + Name + "\n" + "Hall Number: " + HallNo + "\n" + "Capacity: " + Capacity;
        }


    }
}
