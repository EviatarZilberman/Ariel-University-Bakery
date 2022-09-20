using System;
using Bakery.Clients;
using Bakery.Products;
using Bakery.Employee;
using Bakery.Enums;
using System.Threading;
using System.Media;
using System.Windows.Forms;
using System.IO;
using Bakery.BakeryLogic;
using Bakery.Other;
using System.Timers;


namespace Bakery
{
    class Program
    {
        public static void Main(string[] args)
        {

            //=====================================The Bakery========================================================

            Time_date[] time_dateArray = new Time_date[10];
            time_dateArray[0] = new Time_date(28, 2, 2023); // Expiery dates for all the products in the bakery.
            time_dateArray[1] = new Time_date(13, 8, 2024);
            time_dateArray[2] = new Time_date(27, 2, 2023);
            time_dateArray[3] = new Time_date(11, 10, 2025);
            time_dateArray[4] = new Time_date(23, 6, 2027);
            time_dateArray[5] = new Time_date(19, 7, 2024);
            time_dateArray[6] = new Time_date(17, 10, 2023);
            time_dateArray[7] = new Time_date(6, 3, 2023);
            time_dateArray[8] = new Time_date(20, 3, 2028);
            time_dateArray[9] = new Time_date(22, 11, 2024);

            BakeryEmployee[] employees = new BakeryEmployee[4];
            employees[0] = new BakeryManager("Eviatar", "Zilberman", 0); // The bakery manager. id=1
            employees[1] = new Baker("Efraim", "Profus"); // the baker of the bakery. id=2
            employees[2] = new Cashier("Orel", "Magori");
            employees[3] = new BarTender("Tom", "Cohen");


            Product[] productsInBakery = new Product[10]; // Polymorphism of products array.
            productsInBakery[0] = new Cake("Chocolate Cake", 10.99, 100, true, time_dateArray[0], 0, 10, true);
            productsInBakery[1] = new Cake("Orange Cake", 8.3, 80, false, time_dateArray[1], 0, 8, false);
            productsInBakery[2] = new Bread("Shiffon Bread", 10.5, 90, false, time_dateArray[2], 0, true, false, false);
            productsInBakery[3] = new Bread("Shabath Hala", 12.4, 120, false, time_dateArray[3], 10, false, true, false);
            productsInBakery[4] = new Cookie("Maple Cookie", 8.49, 30, false, time_dateArray[4], 10, 5);
            productsInBakery[5] = new Cookie("Vegan Cookie", 7.2, 28, false, time_dateArray[5], 10, 3);
            productsInBakery[6] = new Cookie("Chocolate Chips Cookie", 10.8, 35, true, time_dateArray[6], 10, 10);
            productsInBakery[7] = new Drink("Coffee", 5, 40, true, time_dateArray[7], 10, 200, true);
            productsInBakery[8] = new Drink("Orange Juice", 3, 80, false, time_dateArray[8], 20, 350, false);
            productsInBakery[9] = new Drink("CocaCola", 5, 150, false, time_dateArray[9], 50, 330, false);

            /*Product[] productsInBakery = new Product[3]; // Polymorphism of products array.
            
            productsInBakery[0] = new Drink("Coffee", 5, 40, true, time_dateArray[7], 10, 200, true);
            productsInBakery[1] = new Drink("Orange Juice", 3, 80, false, time_dateArray[8], 20, 350, false);
            productsInBakery[2] = new Drink("CocaCola", 5, 150, false, time_dateArray[9], 50, 330, false);*/

            TheBakery ArielUniversityBakery = new TheBakery(productsInBakery, employees);

            //============================================The Code!=================================================================

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.ResetColor();

            int threadOrManual = 4, switchUserChoose = 7, clientListLength = 0;

            bool outerLoop = true, mainLoop = true, innerLoop = true;
            int outerLoopInt;

            while (outerLoop is true)
            {                
                while (mainLoop is true & outerLoop is true)
                {
                    mainMenu();
                    Console.WriteLine();
                    try
                    {
                        threadOrManual = Convert.ToInt32(Console.ReadLine()); // User Choose how to run the program (manualy or by threads).
                        Console.WriteLine();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        string errorCatch = e.StackTrace;
                    }
                    if (threadOrManual is 999)
                    {
                        outerLoop = false;
                    }

                    innerLoop = true; // Reinitilize these bools for the next while iterations.
                    mainLoop = true;

                    switch (threadOrManual)
                    {
                        case 1: // Manual case.

                            Console.Write("Before you enter to our bakery, let's get to know you!\nWhat's your first name: ");
                            string firstName = Console.ReadLine();
                            Console.WriteLine();

                            Console.Write("What's your last name: ");
                            string lastName = Console.ReadLine();
                            Console.WriteLine();

                            Console.Write("Enter how many products you want: ");

                            while (clientListLength <= 0) { // An exception to get the correct value (int) to the list length.

                                try // clientListLength must be an int.
                                {
                                    clientListLength = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    string errorCatch = e.StackTrace;
                                }
                            }

                            Client client = new Client(firstName, lastName, clientListLength); // Creating the user Client object.

                            Console.WriteLine();

                            client.sayHi();

                            Console.WriteLine();

                            Console.WriteLine("Welcome to Ariel University Bakery!\nChoose one of the options: \n" +
                    "1) Buy in the bakery.\n" +
                    "2) File a complaint.\n" +
                    "3) Remove old products.\n" +
                    "4) Ask the kitchen to make more.\n" +
                    "5) Ask the bar tender to re-fill the drinks.\n" +
                    "6) Get the count of money the bakery earned.\n" +
                    "7) Close the bakery or gets to a former menu.\n");
                            while (innerLoop is true)
                            {
                                try // Gets only ints.
                                {
                                    switchUserChoose = Convert.ToInt32(Console.ReadLine());
                                    Console.WriteLine();
                                }

                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    string errorCatch = e.StackTrace;
                                }

                                switch (switchUserChoose) // Every option in the switch case finished with presenting the menu and return to the current process.
                                {

                                    case 1:
                                        client.buyByUser(ArielUniversityBakery);// Buying by manual useing. Call the method from Client Class. 
                                        secondMenu();
                                        break;

                                    case 2:
                                        ((BakeryManager)ArielUniversityBakery.Employees[0]).fileComplaint(ArielUniversityBakery); // Call the method from the Manager Class.                                                                          
                                        secondMenu();
                                        break;

                                    case 3:
                                        ((Baker)ArielUniversityBakery.Employees[1]).checkExpieryDate(ArielUniversityBakery); // Call the method from Baker Class.
                                        secondMenu();
                                        break;

                                    case 4:
                                        ((Baker)ArielUniversityBakery.Employees[1]).bakingLikeHell(ArielUniversityBakery);
                                        for (int i = 0; i < ArielUniversityBakery.ProductsInBakery.Length; i++)
                                        {
                                            Console.WriteLine(ArielUniversityBakery.ProductsInBakery[i].Name + "---" + ArielUniversityBakery.ProductsInBakery[i].AmountInBakery);
                                        }
                                        secondMenu();
                                        break;

                                    case 5:
                                        ((BarTender)ArielUniversityBakery.Employees[3]).CreateNewDrinks(ArielUniversityBakery); // Makes more drinks.
                                        secondMenu();
                                        break;

                                    case 6:
                                        Console.WriteLine(ArielUniversityBakery.MoneyEarned); // Prints how much money was earned since the bakery was opened.
                                        secondMenu();
                                        break;

                                    case 7:
                                        ArielUniversityBakery.closeTheBakery(); // Close the bakery. Should close the manual switch case.

                                        innerLoop = false;
                                        //outerLoop = false;

                                        break;
                                }

                            } // Don't move this!!!

                            Console.ResetColor();
                            break;  // The break belongs to threadOrManual.

                        /*====== Thread case=====*/

                        case 2: // Second loop for threads.                            
                            Thread BackGroundThread1 = new Thread(play);
                            
                            BackGroundThread1.IsBackground = true;
                            //BackGroundThread1.Start();

                            for (int i = 0; i < ArielUniversityBakery.Employees.Length; i++)
                            {
                                Console.WriteLine("Hi!\nMy name is " + ArielUniversityBakery.Employees[i].FirstName + " " + ArielUniversityBakery.Employees[i].LastName + ". My ID is " + ArielUniversityBakery.Employees[i].Id + ".");
                                Console.WriteLine();
                            }

                            int loopStopTime = 0, maxTimeRun = 300000;

                            Thread[] allClientsThread = new Thread[5];

                            for (int i = 0; i < allClientsThread.Length; i++)
                            {
                                allClientsThread[i] = new Thread(() => ClientStart(ArielUniversityBakery));
                                allClientsThread[i].Start();
                            }

                            while (loopStopTime < maxTimeRun)
                            { // While the loop is still running Clients keep coming to the bakery.
                                for (int i = 0; i < allClientsThread.Length; i++) // A loop for initializing the threads of the clients and start them.
                                {
                                    if (allClientsThread[i].IsAlive == false)
                                    {
                                        allClientsThread[i].Join();                                        
                                        allClientsThread[i] = new Thread(() => ClientStart(ArielUniversityBakery));
                                        allClientsThread[i].Start();                                        
                                    }
                                }

                                if (loopStopTime % 60000 == 0 && loopStopTime >= 100000)
                                {
                                    for (int i=0; i< ArielUniversityBakery.Employees.Length; i++)
                                    {
                                        if (ArielUniversityBakery.Employees[i] is Baker)
                                        {
                                            ((Baker)ArielUniversityBakery.Employees[i]).checkExpieryDate(ArielUniversityBakery); // Checks the unventory of the bakery.
                                        }
                                    }
                                }

                                Thread.Sleep(1000);
                                loopStopTime += 1000;
                            }
                            //Thread.Sleep(maxTimeRun+1000); // Keeps time until the end of the loop while the threads are working.
                            int coundDeadThreads = allClientsThread.Length;

                            while (coundDeadThreads>0) // Checks if all the threads are dead. 
                            {
                                if (coundDeadThreads>0)
                                {
                                    coundDeadThreads = allClientsThread.Length;
                                }

                                for (int i= 0; i< allClientsThread.Length; i++)
                                {
                                    if (allClientsThread[i].IsAlive == false)
                                    {
                                        coundDeadThreads--;
                                    }
                                }
                            }

                            if (coundDeadThreads == 0) // If all the threads are dead the program can get to its end peacfully.
                            {
                            ArielUniversityBakery.closeTheBakery(); // Send this day details to word document.

                            }
                            
                            Console.ResetColor();
                            break; // Breaks the second switch case.
                    } // Breaks the first switch case (threadOrManual).

                    // Out of inner loop.

                }
                Console.WriteLine("Are you sure? 1 - No | Any other key - Yes");
                outerLoopInt = Convert.ToInt32(Console.ReadLine());
                if (outerLoopInt is 1)
                {
                    outerLoop = true;                    
                }
                else
                {
                    outerLoop = false;
                }
            }
        }        
        
        /*===== Methods =====*/

        public static void mainMenu() // The mainMenu method presents the first options for how to start the program- manual or automatic.
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Hi!\n\nHow would you like to run the program?\n\n1-manual\n\n2-thread\n\n999- exit the program");
        }
        public static void secondMenu() // The menu method that shows the options for the user.
                                        // The methos was void but was changed for the returning.
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\nChoose an option: \n" +
                "1) Buy in the bakery.\n" +
                "2) File a complaint.\n" +
                "3) Remove old products.\n" +
                "4) Ask the kitchen to make more.\n" +
                "5) Get the count of money the bakery earned.\n" +
                "6) Close the bakery.\n" +
                "7) Return to the former menu.\n" +
                "999) Close the program.\n" +
                "" +
                "What would you like to do now?");
        }

        public static void play()
        {
            Thread.Sleep(1000);
            SoundPlayer sound = new SoundPlayer(@"C:\Users\User\Desktop\Audio\Linkin Park - In The End Remix.wav.wav");
            sound.Load();
            sound.Play();
            Thread.Sleep(1000);

        }

        public static void ClientStart(TheBakery ArielUniversityBakery)
        {
            bool buying = false, complaintFile = false; // Methods the client must do.

            Random randomEmployee = new Random();

            Client c1 = new Client(ArielUniversityBakery); // Initialize the client.
            c1.sayHiThread();


            while (buying is false | complaintFile is false)
            { // When all bool conditions are true the method is done.*/

                if (buying is false)
                {
                    c1.buyByThread(ArielUniversityBakery);
           /* for (int i=0; i<c1.List.Length; i++)
            {
                Console.WriteLine(c1.List[i].NameOfProduct+"==="+ c1.List[i].DemandOfProducts+"==="+c1.List[i].BoughtProducts);
            }*/
                    buying = true;
                }
                else if (complaintFile is false)
                {
                    if (c1.IsAngryThread is true)
                    {
                        c1.FileComplaint(ArielUniversityBakery);
                        complaintFile = true;
                    }
                    else
                    {
                        complaintFile = true;
                    }
                }
                Thread.Sleep(500);
            }
            ArielUniversityBakery.isHappy(c1);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(c1.FirstName + " " + c1.LastName + ": Thank you!");
            Console.ResetColor();
        }
    }
}