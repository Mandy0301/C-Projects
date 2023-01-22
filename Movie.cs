//============================================================
// Student Number : S10218941, S10223353
// Student Name : Mandy Tang, Felicia Chua
// Module Group : T11
//============================================================

using System;
using System.Collections.Generic;


namespace Programming_2_Assignment
{
    class Movie : IComparable<Movie>
    {
        //private properties
        private string title;
        private int duration;
        private string classification;
        private DateTime openingDate;
        private List<string> genreList;
        private List<Screening> screeningList;

        //public properties
        public string Title { get; set; }
        public int Duration { get; set; }
        public string Classification { get; set; }
        public DateTime OpeningDate { get; set; }
        public List<string> GenreList { get; set; } = new List<string>();
        public List<Screening> ScreeningList { get; set; } = new List<Screening>();

        //method + constructor
        public Movie() { }

        public Movie(string t, int d, string c, DateTime o, List<string> g)
        {
            Title = t;
            Duration = d;
            Classification = c;
            OpeningDate = o;
            GenreList = g;
        }

        public void AddGenre(string g)
        {
            GenreList.Add(g);
        }

        public void AddScreening(Screening s)
        {
            ScreeningList.Add(s);
        }

        public override string ToString()
        {
            string genrestring = "";
            for (int i = 0; i < GenreList.Count; i++)
            {
                genrestring = genrestring + GenreList[i] + ", ";
            }

            return "Title: " + Title + "\n" + "Duration" + Duration + "min" + "\n" + "Classification: " + Classification + "\n" + "Opening Date: " + OpeningDate + "\n" + "Genres: " + genrestring;
        }

        public int CompareTo(Movie e) // sort by Id
        {
            if (OpeningDate > e.OpeningDate)
                return 1;

            else if (OpeningDate > e.OpeningDate)
                return 0;

            else
                return -1;
        }
    }
}
