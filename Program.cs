//============================================================
// Student Number : S10218941, S10223353
// Student Name : Mandy Tang, Felicia Chua
// Module Group : T11
//============================================================

//advanced features are
//letting buyers choose their own seats
//and
//putting seatno on the ticket

using System;
using System.Collections.Generic;
using System.IO;

namespace Programming_2_Assignment
{
    class Program
    {
        //show main menu
        static void MainMenu()
        {
            Console.WriteLine("--------------- Main Menu ---------------");
            Console.WriteLine("[1] Show all information for all movies");
            Console.WriteLine("[2] Show all movie screenings");
            Console.WriteLine("[3] Show a specific movie's screenings");
            Console.WriteLine("[4] Add a movie screening session");
            Console.WriteLine("[5] Delete a movie screening session");
            Console.WriteLine("[6] Order movie ticket(s)");
            Console.WriteLine("[7] Cancel order of ticket(s)");
            Console.WriteLine("[0] Exit");
            Console.WriteLine("-----------------------------------------");
        }

        static void Main(string[] args)
        {
            //make cinema, movie and screening lists
            List<Movie> movielist = new List<Movie>();
            List<Cinema> cinemalist = new List<Cinema>();
            List<Screening> screeninglist = new List<Screening>();
            List<Screening> emptyscreeninglist = new List<Screening>();
            List<Order> orderlist = new List<Order>();

            //1. Load movie and Cinema Data
            InitCinemaList(cinemalist);
            InitMovieList(movielist);

            //initialise seat list in all cinemas
            InitCinemaSeats(cinemalist);

            //sort movie list
            movielist.Sort();

            //2. Load Screening Data
            InitScreeningList(cinemalist, movielist, screeninglist);

            //initialise remainingseats
            CountRemainingSeats(orderlist, screeninglist);

            while (true)
            {
            restartall:
                Console.WriteLine();

                MainMenu();
                Console.WriteLine();
                int choice = GetInt("Enter your choice: ");
                Console.WriteLine();

                //3. List All Movies
                if (choice == 1)
                {
                    ShowMovies(movielist);
                }

                //4. List all screenings
                else if (choice == 2)
                {
                    ShowAllScreenings(screeninglist);
                }

                //list a chosen movie's screenings
                else if (choice == 3)
                {
                    ShowMovies(movielist);

                reentermovieno:
                    Console.WriteLine();

                    //4b. prompt user to select a movie

                    int input = GetInt("Input the movie S/N to show it's screenings: ");
                    Console.WriteLine();

                    if (input > movielist.Count || input <= 0)
                    {
                        Console.WriteLine("Error! Input out of range");
                        goto reentermovieno;
                    }

                    //4d. retrieve and display screening sessions for that movie
                    else
                    {
                        SortMovieScreeningList(movielist);
                        ShowMovieScreenings(movielist, input);
                    }
                }

                else if (choice == 4)
                {
                    while (true)
                    {
                        //5. Create new screening session
                        string screentype;
                        DateTime screendt;
                        Cinema screencinema;

                    reentername:
                        Console.WriteLine();

                        //5a. . list all movies
                        ShowMovies(movielist);
                        Console.WriteLine();

                        //5b. prompt user to select a movie

                        int input2 = GetInt("Input the movie S/N to add a new screening session: ");
                        Console.WriteLine();

                        //find it input out of range
                        if (input2 > movielist.Count || input2 <= 0)
                        {
                            Console.WriteLine("Input out of range");
                            Console.WriteLine("Screening session creation unsuccessful");
                            break;
                        }

                        //find movie
                        Movie mov1 = movielist[input2 - 1];

                    reenterchoice:
                        Console.WriteLine();

                        //enter if entered movie choice if correct.
                        //Yes then continue to make a screening,
                        //no then retry,
                        //input weird then jump back and ask again at the label above 

                        Console.WriteLine("You have selected {0}", mov1.Title);
                        Console.WriteLine();
                        Console.Write("Is that correct? (Y/N): ");
                        string confirm1 = Console.ReadLine().ToUpper();

                        if (confirm1 == "Y")
                        {
                            //choosing movie screening type
                            screentype = ChooseScreenMovieType();

                            //if returned screen type is null, throw them out of the loop
                            if (screentype == null)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Screening session creation unsuccessful");
                                Console.WriteLine();
                                break;
                            }

                        //choose_screening datetime
                        //if the screen datetime invalid throw them out of loop

                        //timing disclaimers
                        reenterdatetime:
                            Console.WriteLine();

                            Console.WriteLine("Please enter the screening date and time in the allowed formats");
                            Console.WriteLine();
                            Console.WriteLine("Date");
                            Console.WriteLine("[1] DD/MM/YY");
                            Console.WriteLine("[2] DD/MM/YYYY");
                            Console.WriteLine("[3] YYYY/MM/DD");
                            Console.WriteLine();

                            Console.WriteLine("Time");
                            Console.WriteLine();
                            Console.WriteLine("[1] HH:MM:SS (24H clock)");
                            Console.WriteLine("[2] HH:MM:SS (am/pm)");
                            Console.WriteLine();


                            screendt = GetDT("Enter the date and time of the screening: ");


                            //give error message if screening datetime is before opening
                            if (screendt <= mov1.OpeningDate)
                            {
                                Console.WriteLine("Movie is not available for screening yet");
                                Console.WriteLine("Screening creation unsuccessful");
                                break;
                            }
                            Console.WriteLine();

                            //confirming of screening date time
                            Console.WriteLine("The screening date and time is {0}: ", screendt);
                            Console.WriteLine();

                        reenterdatetimechoice:
                            Console.WriteLine();

                            Console.Write("Is that correct? (Y/N): ");
                            string dtchoice = Console.ReadLine().ToUpper();

                            //if n, reenter the datetime
                            if (dtchoice == "N")
                            {
                                goto reenterdatetime;
                            }

                            //if not y or n, give error then re-enter again
                            else if (dtchoice != "Y")
                            {
                                Console.WriteLine("Invalid input! Please try again!");
                                goto reenterdatetimechoice;
                            }

                            //if the returned cinema is null, throw them out of the loop
                            screencinema = ChooseScreenCinema(cinemalist, screeninglist, screendt, mov1);

                            if (screencinema == null)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Screening session creation unsuccessful");
                                Console.WriteLine();
                                break;
                            }

                        }

                        else if (confirm1 == "N")
                        {
                            goto reentername;
                        }

                        else
                        {
                            Console.WriteLine("Error! Invalid input!");
                            goto reenterchoice;
                        }

                        //when new screening made already, show them the details
                        Console.WriteLine("Your new screening is:");
                        Console.WriteLine();
                        Console.WriteLine("Movie: {0}", mov1.Title);
                        Console.WriteLine();
                        Console.WriteLine("{0,-20} {1,-15} {2,-5} {3,-15} {4,-12} {5}", "DateTime", "Remaining Seats", "type", "Cinema", "Hall number", "Movie");
                        Console.WriteLine("{0,-20} {1,-15} {2,-5} {3,-15} {4,-12} {5}", screendt, screencinema.Capacity, screentype, screencinema.Name, screencinema.HallNo, mov1.Title);
                        Console.WriteLine();


                    reenterscreenchoice:
                        Console.WriteLine();

                        //ask if the screening is correct
                        //if yes, then make a screening and break
                        //if no, then discard all change and kick the user
                        //if weird input go back to label directly above and ask again
                        Console.Write("Is that correct? (Y/N): ");
                        string screenchoice = Console.ReadLine().ToUpper();


                        if (screenchoice == "Y")
                        {
                            Screening newscreen = new Screening(0, Convert.ToDateTime(screendt), screencinema.Capacity, screentype, screencinema, mov1);
                            Console.WriteLine("New screening session creation successful!");
                            screeninglist.Add(newscreen);
                            mov1.ScreeningList.Add(newscreen);
                            break;
                        }

                        else if (screenchoice == "N")
                        {
                            Console.WriteLine("Discarding all changes...");
                            Console.WriteLine("Exiting screening session creation...");
                            Console.WriteLine("Please restart screening session creation");
                            break;
                        }

                        else
                        {
                            Console.WriteLine("Invalid input! Please try again!");
                            goto reenterscreenchoice;
                        }
                    }
                }
                //6. Delete a movie screening session
                else if (choice == 5)
                {
                    while (true)
                    {
                    retrydel:
                        Console.WriteLine();

                        //6a. List all movie screening sessions that have not sold any tickets
                        Console.WriteLine("Showing screenings with no tickets sold...");
                        Console.WriteLine();

                        CountRemainingSeats(orderlist, screeninglist);
                        ShowEmptyScreening(screeninglist, emptyscreeninglist);
                        Console.WriteLine();

                        //6b. Prompt user to select a session
                        Screening delscreen = DeleteScreening(emptyscreeninglist);
                        Console.WriteLine();

                        //6c. Remove the movie screening from all screening lists
                        if (delscreen == null)
                        {

                        retrydelconfirm:
                            Console.WriteLine();

                            Console.WriteLine("Removal of screening unsuccessful");
                            Console.Write("Do you wish to retry? (Y/N): ");
                            string retrydelchoice = Console.ReadLine().ToUpper();

                            if (retrydelchoice == "Y")
                            {
                                goto retrydel;
                            }

                            else if (retrydelchoice == "N")
                            {
                                break;
                            }

                            else
                            {
                                Console.WriteLine("Invalid input, please try again");
                                goto retrydelconfirm;
                            }
                        }

                        //6d. Display the status of the removal (i.e. successful or unsuccessful)
                        delscreen.Movie.ScreeningList.Remove(delscreen);
                        screeninglist.Remove(delscreen);

                        ShowEmptyScreening(screeninglist, emptyscreeninglist);
                        Console.WriteLine("Removal of screening successful");
                        break;

                    }

                }

                //7. Order movie ticket(s)
                else if (choice == 6)
                {
                    ShowMovies(movielist);

                    //7a. Prompt user to select a movie
                    int movchoice = GetInt("Choose a movie by inputting the S/N (or 0 to go to main menu): ");

                    if (movchoice == 0)
                    {
                        goto restartall;
                    }

                    Console.WriteLine();

                    //7b. List all movie screenings of the selected movie
                    SortMovieScreeningList(movielist);
                    ShowMovieScreenings(movielist, movchoice);

                    if (movchoice > movielist.Count || movchoice <= 0)
                    {
                        Console.WriteLine("Index out of range, movie not found");
                        break;
                    }

                    Movie m = movielist[movchoice -1];


                    //7c. Prompt user to select movie screening
                    int screeningchoice = GetInt("Choose a screening by inputting the S/N (or 0 to go to main menu): ");

                    if (screeningchoice == 0)
                    {
                        goto restartall;
                    }

                    //7d. Retrieve the selected movie screening
                    Screening sorder = SelectMovieScreening(screeningchoice, m);

                    if (sorder == null)
                    {
                        Console.WriteLine("Error! Screening not found!");
                        break;
                    }

                    //7e. Prompt user to enter the total number of tickets to order
                    int nooftickets = OrderTicketNo(sorder);

                    CountRemainingSeats(orderlist, screeninglist);

                    if (nooftickets == 0)
                    {
                        goto restartall;
                    }

                    else if (nooftickets > sorder.SeatsRemaining)
                    {
                        Console.WriteLine("Not enough remaining seats!");
                        goto restartall;
                    }

                reenterdt:
                    Console.WriteLine();

                    //timing disclaimers
                    Console.WriteLine("Enter your current date and time");
                    Console.WriteLine();
                    Console.WriteLine("Please enter the date and time in the allowed formats");
                    Console.WriteLine();
                    Console.WriteLine("Date");
                    Console.WriteLine("[1] DD/MM/YY");
                    Console.WriteLine("[2] DD/MM/YYYY");
                    Console.WriteLine("[3] YYYY/MM/DD");
                    Console.WriteLine();

                    Console.WriteLine("Time");
                    Console.WriteLine();
                    Console.WriteLine("[1] HH:MM:SS (24H clock)");
                    Console.WriteLine("[2] HH:MM:SS (am/pm)");
                    Console.WriteLine();


                    DateTime orderdt = GetDT("Enter the current date and time: ");

                    //check if the movie has already passed the screening period
                    if (orderdt >= sorder.ScreeningDateTime)
                    {
                        Console.WriteLine("Movie has already been screened");
                        break;
                    }

                    //check if the movie has not been released yet
                    else if (orderdt <= sorder.Movie.OpeningDate)
                    {
                        Console.WriteLine("Movie is not released yet");
                        break;
                    }

                reenterconfirmdt:
                    Console.WriteLine();

                    Console.WriteLine("Your order date and time is {0}", orderdt);
                    Console.WriteLine();
                    Console.Write("Is that correct? (Y/N): ");
                    string dtconfirm = Console.ReadLine().ToUpper();

                    if (dtconfirm == "N")
                    {
                        goto reenterdt;
                    }
                    else if (dtconfirm != "Y")
                    {
                        Console.WriteLine("Invalid input, please try again");
                        goto reenterconfirmdt;
                    }

                    int ordernum = orderlist.Count + 1;

                    //7f. Prompt user if all ticket holders meet the movie classification requirements
                    //(except movies classified as G)
                    if (sorder.Movie.Classification != "G")
                    {
                        string ageprompt = "";

                        if (sorder.Movie.Classification == "PG13")
                        {
                            ageprompt = "Are all ticket holders above 13 years old? (Y/N): ";
                        }

                        else if (sorder.Movie.Classification == "NC16")
                        {
                            ageprompt = "Are all ticket holders above 16 years old? (Y/N): ";
                        }

                        else if (sorder.Movie.Classification == "M18")
                        {
                            ageprompt = "Are all ticket holders above 18 years old? (Y/N): ";
                        }

                        else if (sorder.Movie.Classification == "R21")
                        {
                            ageprompt = "Are all ticket holders above 21 years old? (Y/N): ";
                        }

                    //check age
                    reenteragecheck:
                        Console.WriteLine();

                        Console.Write(ageprompt);
                        string agecheck = Console.ReadLine().ToUpper();

                        if (agecheck == "N")
                        {
                            Console.WriteLine("Aborting order....");
                            break;
                        }

                        else if (agecheck != "Y")
                        {
                            Console.WriteLine("Invalid input, please retry!");
                            goto reenteragecheck;
                        }
                    }

                    //7g. Create an Order object with the status “Unpaid”
                    Order myorder = new Order(ordernum, orderdt);
                    myorder.Status = "Unpaid";

                    //7h. Add the ticket object to the ticket list of the order
                    orderlist.Add(myorder);

                    //number of tickets user wants to buy must be less than total seats of movie theater
                    //legend for movie seating selection
                    for (int ticcount = 1; ticcount <= nooftickets; ticcount++)
                    {
                        ShowCinema(myorder, sorder, orderlist);
                        Console.WriteLine();

                        Console.WriteLine("Legend:");
                        Console.WriteLine("[XX]: Seat is unavailable");
                        Console.WriteLine("[  ]: Current order's chosen seat");
                        Console.WriteLine();


                    chooseseatnumber:
                        Console.WriteLine();

                        //choose seat no and make ticket
                        int seatno = GetInt("Enter your chosen seat number: ");
                        Console.WriteLine();

                        if (seatno > sorder.Cinema.Capacity || seatno <= 0)
                        {
                            Console.WriteLine("Seat is non-existent");
                            break;
                        }

                        string seatstring = "";

                        if (seatno < 10)
                        {
                            seatstring = seatstring + " 0" + seatno + " ";
                        }

                        else
                        {
                            seatstring = " " + seatno + " ";
                        }

                        //check if movie theater has seat user wants 
                        //if not will display that seat is unavaliable and user can select another seat
                        if (sorder.Cinema.Seatlist.Contains(seatstring))
                        {
                            foreach (Order o in orderlist)
                            {
                                foreach (Ticket tix in o.TicketList)
                                {
                                    if (seatstring == tix.Seatno && tix.Screening == sorder && o.Status != "Cancelled")
                                    {
                                        Console.WriteLine("Seat Unavailable!");
                                        goto chooseseatnumber;
                                    }
                                }

                            }

                            Ticket t = MakeTicket(seatstring, sorder, myorder);

                            if (t == null)
                            {
                                goto restartall;
                            }

                            CountRemainingSeats(orderlist, screeninglist);
                        }

                        else
                        {
                            Console.WriteLine("Seat Unavailable! please enter a different seat");
                            goto chooseseatnumber;
                        }
                    }
                    //7i. List amount payable
                    double price = CalculateOrderAmount(myorder);

                    Console.WriteLine("Amount payable: {0:c}", price);
                    Console.WriteLine();

                    //7j. Prompt user to press any key to make payment
                    Console.WriteLine("Press any key to make payment");
                    Console.ReadKey();

                    //7k. change order status to “Paid”
                    myorder.Amount = price;
                    myorder.Status = "Paid";

                    Console.WriteLine();

                    ShowCinema(myorder, sorder, orderlist);
                    Console.WriteLine();

                    Console.WriteLine("[00]: Ordered Seats");
                    Console.WriteLine();

                    myorder.TicketList.Sort();

                    //display order reciept
                    //7l. Fill in the necessary details to the new order (e.g amount)
                    Console.WriteLine("---------------- Order Reciept ------------------");
                    Console.WriteLine("Order Number: {0}", myorder.OrderNo);
                    Console.WriteLine("Order DateTime: {0}", myorder.OrderDateTime);
                    Console.WriteLine();
                    Console.WriteLine("Movie: {0}", sorder.Movie.Title);
                    Console.WriteLine("Screening DateTime: {0}", sorder.ScreeningDateTime);
                    Console.WriteLine();
                    Console.WriteLine("Cinema: {0}", sorder.Cinema.Name);
                    Console.WriteLine("Hall Number:  {0}", sorder.Cinema.HallNo);
                    Console.WriteLine();

                    foreach (Ticket receipttick in myorder.TicketList)
                    {
                        Console.WriteLine("Seat Number: {0}", receipttick.Seatno);
                    }

                    Console.WriteLine();
                    Console.WriteLine("Amount Paid: {0:c}", myorder.Amount);
                    Console.WriteLine();
                    Console.WriteLine("Order Status: {0}", myorder.Status);
                    Console.WriteLine("-------------------------------------------------");
                }

                //8. Cancel order of ticket
                //check the date the user is refunding ticket 
                else if (choice == 7)
                {
                    Console.WriteLine("Enter your current date and time");
                    Console.WriteLine();
                    Console.WriteLine("Please enter the date and time in the allowed formats");
                    Console.WriteLine();
                    Console.WriteLine("Date");
                    Console.WriteLine("[1] DD/MM/YY");
                    Console.WriteLine("[2] DD/MM/YYYY");
                    Console.WriteLine("[3] YYYY/MM/DD");
                    Console.WriteLine();

                    Console.WriteLine("Time");
                    Console.WriteLine();
                    Console.WriteLine("[1] HH:MM:SS (24H clock)");
                    Console.WriteLine("[2] HH:MM:SS (am/pm)");
                    Console.WriteLine();

                    DateTime cdt = GetDT("Enter the current date and time: ");

                    //8a. Prompt user for order number
                    int ordernum = GetInt("Enter your order number: ");

                    //8b. Retrieve the selected order                   
                    Order o = Searchorder(orderlist, ordernum);

                    //check if order exists
                    //if not kick user
                    if (o == null)
                    {
                        Console.WriteLine("Order not found!");
                        break;
                    }

                    //check if order status is cancelled or not
                    //if yes write that order has been cancelled
                    else if (o.Status == "Cancelled")
                    {
                        Console.WriteLine("Order has already been cancelled");
                        break;
                    }
                    //8c. Check if the screening in the selected order is screened
                    else if (o.TicketList[0].Screening.ScreeningDateTime < cdt)
                    {
                        Console.WriteLine("Movie has already been screened/is currently screening");
                        Console.WriteLine("Order could not be cancelled");
                        Console.WriteLine();
                        Console.WriteLine("Order cancellation unsuccessful");
                    }


                    else
                    {
                        //8d. Update seat remaining for the movie screening based on the selected order
                        foreach (Ticket t in o.TicketList)
                        {
                            t.Screening.SeatsRemaining = t.Screening.SeatsRemaining + 1;
                        }

                        //8e. Change order status to “Cancelled”
                        o.Status = "Cancelled";

                        Console.WriteLine();
                        Console.WriteLine("Order has successfully been cancelled");
                        Console.WriteLine("The amount of {0:c} has been refunded to your account", o.Amount);
                        Console.WriteLine();
                        Console.WriteLine("----------------- Cancellation Receipt -----------------------");
                        Console.WriteLine("Refunded amount: {0:c}", o.Amount);
                        Console.WriteLine();
                        Console.WriteLine("Order Status: {0}", o.Status);
                        Console.WriteLine("--------------------------------------------------------------");

                    }

                }

                //exit
                else if (choice == 0)
                {
                reenterexitchoice:
                    Console.WriteLine();

                    Console.Write("Are you sure you want to exit? (Y/N): ");
                    string exitchoice = Console.ReadLine().ToUpper();

                    if (exitchoice == "Y")
                    {
                        break;
                    }

                    else if (exitchoice == "N")
                    {
                        goto restartall;
                    }

                    else
                    {
                        Console.WriteLine("Invalid input! Please try again!");
                        goto reenterexitchoice;
                    }
                }

                else
                {
                    Console.WriteLine("Invalid input! Please try again!");
                    goto restartall;
                }
            }
        }



        //initializing methods

        //1a. method to load data from csv to movie list
        static void InitMovieList(List<Movie> mlist)
        {
            //read file
            string[] csvLines = File.ReadAllLines("Movie.csv");

            //split file details
            for (int i = 1; i < csvLines.Length; i++)
            {
                //split each line further to their details
                string[] moviedetails = csvLines[i].Split(',');

                //split genre part into seperate strings and whack in array
                string[] moviegenre = moviedetails[2].Split('/');

                //create temp genre list
                List<string> genre = new List<string>();

                //add genres frm array to list
                for (int g = 0; g < moviegenre.Length; g++)
                {
                    genre.Add(moviegenre[g]);
                }

                //create movie object
                Movie m = new Movie(moviedetails[0], Convert.ToInt32(moviedetails[1]), moviedetails[3], Convert.ToDateTime(moviedetails[4]), genre);

                mlist.Add(m);
            }
        }

        //1b. method to load data from csv file to cinema list
        static void InitCinemaList(List<Cinema> clist)
        {
            //read file
            string[] csvLines = File.ReadAllLines("Cinema.csv");

            //split file details
            for (int i = 1; i < csvLines.Length; i++)
            {
                //split each line further to their details
                string[] cinemadetails = csvLines[i].Split(',');

                //make cinema object
                Cinema c = new Cinema(cinemadetails[0], Convert.ToInt32(cinemadetails[1]), Convert.ToInt32(cinemadetails[2]));

                //add to cinema list
                clist.Add(c);
            }
        }

        //2a method to load data from csv file to screening list and also add in movie list
        static void InitScreeningList(List<Cinema> clist, List<Movie> mlist, List<Screening> slist)
        {
            string[] csvLines = File.ReadAllLines("Screening.csv");

            //split file details
            for (int i = 1; i < csvLines.Length; i++)
            {
                //split each line further to their details
                string[] screendetails = csvLines[i].Split(',');

                //search for cinema
                Cinema c = Searchcinema(clist, screendetails[2], Convert.ToInt32(screendetails[3]));
                Movie m = Searchmovie(mlist, screendetails[4]);

                //make screening object, use i as placeholder for screening number
                Screening s = new Screening(1000 + 1, Convert.ToDateTime(screendetails[0]), c.Capacity, screendetails[1], c, m);

                //add screening to slist
                slist.Add(s);

                //add to movie list
                m.AddScreening(s);

            }
        }

        //add cinema seats into the seatlist
        static void InitCinemaSeats(List<Cinema> clist)
        {
            foreach (Cinema c in clist)
            {
                c.InitSeatList();
            }
        }








        //processing methods

        //show movies in movie list
        static void ShowMovies(List<Movie> mlist)
        {
            //header
            Console.WriteLine("{0,-5} {1,-30} {2,-10} {3,-30} {4,-15} {5,-15}", "S/N", "Title", "Duration", "Genre", "Classification", "Opening Date");

            //print out movies
            for (int i = 0; i < mlist.Count; i++)
            {
                Movie m = mlist[i];
                string genre = "";

                //format genre list
                int length = m.GenreList.Count - 1;
                for (int b = 0; b < length; b++)
                {
                    genre = genre + m.GenreList[b] + ", ";
                }

                genre = genre + m.GenreList[length];

                Console.WriteLine("{0,-5} {1,-30} {2,-10} {3,-30} {4,-15} {5,-15}", i + 1, m.Title, m.Duration, genre, m.Classification, m.OpeningDate);

            }
        }

        //Show all screenings
        static void ShowAllScreenings(List<Screening> slist)
        {
            Console.WriteLine("{0,-5} {1,-25} {2,-30} {3,-15} {4,-15} {5,-15} {6,-15}", "S/O", "Screening DateTime", "Movie Title", "Screening Type", "Cinema Name", "Hall Number", "Remaining Seats");

            for (int i = 0; i < slist.Count; i++)
            {
                Screening s = slist[i];
                Console.WriteLine("{0,-5} {1,-25} {2,-30} {3,-15} {4,-15} {5,-15} {6,-15}", (1001 + i), s.ScreeningDateTime, s.Movie.Title, s.ScreeningType, s.Cinema.Name, s.Cinema.HallNo, s.SeatsRemaining);
            }
        }

        //4c. retrieve movie object
        //4d. retrieve and display screening sessions specific movie 
        static void ShowMovieScreenings(List<Movie> mlist, int choice)
        {
            if (choice >= mlist.Count || choice <= 0)
            {
                Console.WriteLine("Input out of range, movie not found");
            }

            else
            {
                //find the movie in movie list
                Movie m = mlist[choice - 1];





                //display movie tile + headers
                Console.WriteLine("Screenings for {0}:", m.Title);
                Console.WriteLine();
                Console.WriteLine("{0,-5} {1,-25} {2,-15} {3,-15} {4,-15} {5,-15}", "S/O", "Screening DateTime", "Screening Type", "Cinema Name", "Hall Number", "Remaining Seats");

                //display movie screening details
                foreach (Screening s in m.ScreeningList)
                {
                    Console.WriteLine("{0,-5} {1,-25} {2,-15} {3,-15} {4,-15} {5,-15}", s.ScreeningNo, s.ScreeningDateTime, s.ScreeningType, s.Cinema.Name, s.Cinema.HallNo, s.SeatsRemaining);
                }
            }
        }




        //choose movie screening type
        static string ChooseScreenMovieType()
        {
            //preset values
            string type;

        //redo entries marker, there are more similar ones
        reentertype:
            Console.WriteLine();

            //choose screening type by selecting from menu
            Console.WriteLine("Enter a number to select screening type ");
            Console.WriteLine();

            //screening types
            Console.WriteLine("[1] 2D");
            Console.WriteLine("[2] 3D");
            Console.WriteLine();

            int input1 = GetInt("Enter your choice: ");

            //input checking for 2d
            if (input1 == 1)
            {
            reentertypechoice2d:
                Console.WriteLine();

                Console.WriteLine("Your screening type is 2D");
                Console.WriteLine();
                Console.Write("Is that correct? (Y/N): ");
                string typechoice = Console.ReadLine().ToUpper();

                //if choice is y, set type as 2d
                if (typechoice == "Y")
                {
                    type = "2D";
                    return type;
                }

                //if n, go out and allow re-entries
                else if (typechoice == "N")
                {
                    goto reentertype;
                }

                //if choice is not y or n, show error msg and go back to allow re-entry
                else
                {
                    Console.WriteLine("Invalid input! Please try again!");
                    goto reentertypechoice2d;
                }

            }

            //input checking for 3d, same as 2d
            else if (input1 == 2)
            {
            reentertypechoice3d:
                Console.WriteLine();

                Console.WriteLine("Your screening type is 3D");
                Console.WriteLine();
                Console.Write("Is that correct? (Y/N): ");
                string typechoice = Console.ReadLine().ToUpper();

                if (typechoice == "Y")
                {
                    type = "3D";
                    return type;
                }

                else if (typechoice == "N")
                {
                    goto reentertype;
                }

                else
                {
                    Console.WriteLine("Invalid input! Please try again!");
                    goto reentertypechoice3d;
                }
            }

            else
            {
                Console.WriteLine("Input unavailable");
                return null;
            }
        }

        static Cinema ChooseScreenCinema(List<Cinema> clist, List<Screening> slist, DateTime sdatetime, Movie m)
        {
        reentercinema:
            Console.WriteLine();

            //show cinema list
            Console.WriteLine("{0,-5} {1,-15} {2,-12} {3}", "S/N", "Name", "Hall Number", "Capacity");

            for (int i = 0; i < clist.Count; i++)
            {
                Cinema ci = clist[i];
                Console.WriteLine("{0,-5} {1,-15} {2,-12} {3}", (i + 1), ci.Name, ci.HallNo, ci.Capacity);
            }

            //choose cinema by menu input
            int cinedex = GetInt("Enter the S/N of the screening cinema: ");


            if (cinedex > clist.Count || cinedex <= 0)
            {
                Console.WriteLine("Input out of range");
                return null;
            }

            Cinema screencinema = clist[cinedex - 1];

            Console.WriteLine("Your cinema is {0}, hall number: {1}, Capacity: {2}", screencinema.Name, screencinema.HallNo, screencinema.Capacity);
            Console.WriteLine();

        reentercinemachoice:
            Console.WriteLine();

            Console.Write("Is that correct? (Y/N): ");
            string cinechoice = Console.ReadLine().ToUpper();

            foreach (Screening s in slist)
            {
                if (((s.Cinema == screencinema) && ((s.ScreeningDateTime.AddMinutes(s.Movie.Duration + 30) >= sdatetime) || (sdatetime.AddMinutes(m.Duration + 30) >= s.ScreeningDateTime))))
                {
                    Console.WriteLine("Cinema Unavailable");
                    return null;
                }
            }

            if (cinechoice == "Y")
            {
                return screencinema;
            }

            else if (cinechoice == "N")
            {
                goto reentercinema;
            }

            else
            {
                Console.WriteLine("Invalid input! Please try again!");
                goto reentercinemachoice;
            }
        }

        static void ShowEmptyScreening(List<Screening> slist, List<Screening> eslist)
        {
            eslist.Clear();

            foreach (Screening s in slist)
            {
                if (s.SeatsRemaining == s.Cinema.Capacity)
                {
                    eslist.Add(s);
                }

            }

            eslist.Sort();

            Console.WriteLine("{0,-5} {1,-25} {2,-30} {3,-15} {4,-15} {5,-15} {6,-15}", "S/N", "Screening DateTime", "Movie Title", "Screening Type", "Cinema Name", "Hall Number", "Remaining Seats");

            for (int i = 0; i < eslist.Count; i++)
            {
                Screening es = eslist[i];
                Console.WriteLine("{0,-5} {1,-25} {2,-30} {3,-15} {4,-15} {5,-15} {6,-15}", (1001 + i), es.ScreeningDateTime, es.Movie.Title, es.ScreeningType, es.Cinema.Name, es.Cinema.HallNo, es.SeatsRemaining);
            }
        }

        static Screening DeleteScreening(List<Screening> eslist)
        {

            int delchoice = GetInt("Enter the S/N of the screening you want to remove: ");

            if ((delchoice > (eslist.Count + 1000)) || (delchoice < 1001))
            {
                Console.WriteLine("Error! Input out of range!");
                return null;
            }

            Screening delscreen = eslist[delchoice - 1001];
            return delscreen;

        }

        static Screening SelectMovieScreening(int input, Movie m)
        {
            if (input > m.ScreeningList.Count || input <= 0)
            {
                Console.WriteLine("Input out of range! Screening unavailable");
                return null;
            }

            else
            {
                return m.ScreeningList[input - 1];
            }
        }

        static int OrderTicketNo(Screening s)
        {

            Console.WriteLine("Movie: {0}", s.Movie.Title);
            Console.WriteLine("Screening Date and Time: {0}", s.ScreeningDateTime);
            Console.WriteLine("Number of seats remaining: {0}", s.SeatsRemaining);
            Console.WriteLine();

        reenterorderticketno:
            Console.WriteLine();

            int nooftickets = GetInt("Enter the number of tickets to order (or enter 0 to exit):");

            if (nooftickets < 0)
            {
                Console.WriteLine("Invalid input");
                goto reenterorderticketno;
            }
            return nooftickets;
        }

        static void ShowCinema(Order myor, Screening s, List<Order> olist)
        {

            Console.WriteLine(s.Cinema.Name);
            Console.WriteLine();
            Console.WriteLine("------------------------- Screen -------------------------");
            Console.WriteLine();


            for (int seatrow = 0; seatrow != Math.Floor(s.Cinema.Capacity / 10.0); seatrow++)
            {
                string seat = " ";


                for (int seatcol = 1; seatcol <= 10; seatcol++)
                {
                    if (seatrow == 0 && seatcol <= 9)
                    {
                        seat = seat + "0" + (seatcol + seatrow * 10) + "    ";
                        foreach (Order o in olist)
                        {
                            foreach (Ticket t in o.TicketList)
                            {

                                if (seat.Contains(t.Seatno) && o.Status == "Paid" && t.Screening == s && o.OrderNo != myor.OrderNo)
                                {
                                    seat = seat.Replace(t.Seatno, "[XX]");
                                }

                                if (seat.Contains(t.Seatno) && o.Status == "Unpaid" && t.Screening == s && o.OrderNo == myor.OrderNo)
                                {
                                    seat = seat.Replace(t.Seatno, "[  ]");
                                }

                                if (seat.Contains(t.Seatno) && o.Status == "Paid" && t.Screening == s && o.OrderNo == myor.OrderNo)
                                {
                                    seat = seat.Replace(t.Seatno, "[OO]");
                                }
                            }
                        }
                    }

                    else
                    {
                        seat = seat + (seatcol + seatrow * 10) + "    ";
                        foreach (Order o in olist)
                        {
                            foreach (Ticket t in o.TicketList)
                            {
                                if (seat.Contains(t.Seatno) && o.Status == "Paid" && t.Screening == s && o.OrderNo != myor.OrderNo)
                                {
                                    seat = seat.Replace(t.Seatno, "[XX]");
                                }

                                if (seat.Contains(t.Seatno) && o.Status == "Unpaid" && t.Screening == s && o.OrderNo == myor.OrderNo)
                                {
                                    seat = seat.Replace(t.Seatno, "[  ]");
                                }

                                if (seat.Contains(t.Seatno) && o.Status == "Paid" && t.Screening == s && o.OrderNo == myor.OrderNo)
                                {
                                    seat = seat.Replace(t.Seatno, "[OO]");
                                }
                            }
                        }
                    }

                }

                Console.WriteLine(seat);
            }

            string backseatstring = " ";
            for (int backseat = 1; backseat <= s.Cinema.Capacity % 10; backseat++)
            {
                int seat = Convert.ToInt32(Math.Floor(s.Cinema.Capacity / 10.0) * 10);
                backseatstring = backseatstring + seat + backseat + "    ";
            }

            foreach (Order o in olist)
            {
                foreach (Ticket t in o.TicketList)
                {
                    if (backseatstring.Contains(t.Seatno) && o.Status == "Paid" && t.Screening == s && o.OrderNo != myor.OrderNo)
                    {
                        backseatstring = backseatstring.Replace(t.Seatno, "[XX]");
                    }

                    if (backseatstring.Contains(t.Seatno) && o.Status == "Unpaid" && t.Screening == s && o.OrderNo == myor.OrderNo)
                    {
                        backseatstring = backseatstring.Replace(t.Seatno, "[  ]");
                    }

                    if (backseatstring.Contains(t.Seatno) && o.Status == "Paid" && t.Screening == s && o.OrderNo == myor.OrderNo)
                    {
                        backseatstring = backseatstring.Replace(t.Seatno, "[OO]");
                    }
                }
            }

            Console.WriteLine(backseatstring);
        }

        static Ticket MakeTicket(string seatno, Screening s, Order o)
        {
        reenterticketchoice:
            Console.WriteLine();

            Console.WriteLine("--------------------Tickets--------------------");
            Console.WriteLine("[1] Student (Primary, Secondary, Tertiary)");
            Console.WriteLine("[2] Senior Citizen (55 years old or above)");
            Console.WriteLine("[3] Adult (Eligible for a popcorn offer)");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine();

            int tickchoice = GetInt("Enter the ticket type by typing its S/N (or 0 to go to main menu): ");

            if (tickchoice == 1)
            {
            reenterlos:
                Console.WriteLine();
                Console.Write("Enter the level of study: ");
                string los = Console.ReadLine();

                los = los.ToUpper();

                if (los == "PRIMARY" || los == "SECONDARY" || los == "TERTIARY")
                {
                    Student st = new Student(s, seatno, los);

                    o.AddTicket(st);
                    return st;
                }

                else
                {
                    Console.WriteLine("Invalid input, please try again");
                    goto reenterlos;
                }
            }

            else if (tickchoice == 2)
            {
                Console.WriteLine();

                Console.WriteLine("Please enter the date in the allowed formats");
                Console.WriteLine();
                Console.WriteLine("[1] DD/MM/YY");
                Console.WriteLine("[2] DD/MM/YYYY");
                Console.WriteLine("[3] YYYY/MM/DD");
                Console.WriteLine();
                DateTime dob = GetDT("Enter the date of birth: ");

                int age = o.OrderDateTime.Year - dob.Year - 1;

                if (o.OrderDateTime.Month >= dob.Month && o.OrderDateTime.Day >= dob.Day)
                {
                    age++;
                }

                if (age < 55)
                {
                    Console.WriteLine("The person is not eligible for a Senior Citizen ticket");
                    goto reenterticketchoice;
                }

                SeniorCitizen sc = new SeniorCitizen(s, seatno, age);

                o.AddTicket(sc);
                return sc;
            }

            else if (tickchoice == 3)
            {

            reenterpo:
                Console.WriteLine();
                Console.Write("Do you want the popcorn offer for an additional price of $3 (Y/N): ");
                string pobool = Console.ReadLine().ToUpper();

                bool poffer;

                if (pobool == "Y")
                {
                    poffer = true;
                }

                else if (pobool == "N")
                {
                    poffer = false;
                }

                else
                {
                    Console.WriteLine("Invalid option, please try again");
                    goto reenterpo;
                }

                Adult ad = new Adult(s, seatno, poffer);

                o.AddTicket(ad);
                return ad;

            }

            else if (tickchoice == 0)
            {
                return null;
            }

            else
            {
                Console.WriteLine("Input unavailable, please retry");
                goto reenterticketchoice;
            }
        }

        static double CalculateOrderAmount(Order o)
        {
            Console.WriteLine("Your Cart");

            Console.WriteLine("For movie: {0}", o.TicketList[0].Screening.Movie.Title);

            o.Amount = 0;

            foreach (Ticket t in o.TicketList)
            {
                o.Amount = o.Amount + t.CalculatePrice();
            }

            double tickprice = o.Amount;
            o.Amount = 0;

            return tickprice;

        }

        // search for movie
        static Movie Searchmovie(List<Movie> mlist, string title)
        {
            foreach (Movie m in mlist)
            {
                if (title == m.Title)
                {
                    return m;
                }

            }

            return null;
        }

        // search for cinema
        static Cinema Searchcinema(List<Cinema> clist, string name, int hall)
        {
            foreach (Cinema c in clist)
            {
                if (name == c.Name && hall == c.HallNo)
                {
                    return c;
                }

            }

            return null;
        }

        static Order Searchorder(List<Order> olist, int orderno)
        {
            foreach (Order o in olist)
            {
                if (orderno == o.OrderNo)
                {
                    return o;
                }

            }

            return null;
        }

        //sort screenings in movielist
        static void SortMovieScreeningList(List<Movie> mlist)
        {
            //sort movie screening list
            foreach (Movie mov in mlist)
            {
                mov.ScreeningList.Sort();

                //relabel screening number
                foreach (Screening s in mov.ScreeningList)
                {
                    s.ScreeningNo = mov.ScreeningList.IndexOf(s) + 1;
                }
            }
        }

        static void CountRemainingSeats(List<Order> olist, List<Screening> slist)
        {
            foreach (Screening s in slist)
            {
                s.SeatsRemaining = s.Cinema.Capacity;
            }

            foreach (Order or in olist)
            {
                if (or.Status != "Cancelled")
                {
                    foreach (Ticket t in or.TicketList)
                    {
                        t.Screening.SeatsRemaining = t.Screening.SeatsRemaining - 1;
                    }
                }
            }
        }

        //validation
        static int GetInt(string prompt)
        {
            int n;
            while (true)
            {
                try
                {
                    Console.Write(prompt);
                    n = Convert.ToInt32(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input, Please try again.");
                }
            }
            return n;
        }
        static DateTime GetDT(string prompt)
        {
            DateTime n;
            while (true)
            {
                try
                {
                    Console.Write(prompt);
                    n = Convert.ToDateTime(Console.ReadLine());
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid input, Please try again.");
                }
            }
            return n;
        }

    }
}