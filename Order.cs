//============================================================
// Student Number : S10218941, S10223353
// Student Name : Mandy Tang, Felicia Chua
// Module Group : T11
//============================================================

using System;
using System.Collections.Generic;

namespace Programming_2_Assignment
{
    class Order
    {
        //private properties
        private int orderNo;
        private DateTime orderDateTime;
        private double amount;
        private string status;
        private List<Ticket> ticketList;

        //public properties
        public int OrderNo { get; set; }
        public DateTime OrderDateTime { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }
        public List<Ticket> TicketList { get; set; } = new List<Ticket>();

        //method + constructors
        public Order() { }

        public Order(int n, DateTime dt)
        {
            OrderNo = n;
            OrderDateTime = dt;
        }

        public void AddTicket(Ticket t)
        {
            TicketList.Add(t);
        }

        public override string ToString()
        {
            string ticketstring = "";
            for (int i = 0; i < TicketList.Count; i++)
            {
                ticketstring = ticketstring + TicketList[i];
            }

            return "Order Number: " + OrderNo + "\n" + "Order DateTime: " + OrderDateTime + "\n" + "Amount: " + Amount + "\n" + "Status: " + Status + "\n" + "Tickets: " + ticketstring;
        }
    }
}
