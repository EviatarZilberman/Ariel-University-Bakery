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
using System.Security.Cryptography.X509Certificates;
using System.Collections.Generic;
using System.Linq;

namespace Bakery
{
    class Program
    {
        public static void Main(string[] args)
        {

            //=====================================The Bakery========================================================

            BakeryEmployee[] employees = new BakeryEmployee[4];
            employees[0] = new BakeryManager("Eviatar", "Zilberman"); // The bakery manager. id=1
            employees[1] = new Baker("Efraim", "Profus"); // the baker of the bakery. id=2
            employees[2] = new Cashier("Orel", "Magori");
            employees[3] = new BarTender("Tom", "Cohen");

            Product[] productsInBakery = new Product[10]; // Polymorphism of products array.
            productsInBakery[0] = new Cake("Chocolate Cake", 10.99, 100, true, new Time_date(), 10, 10, true);
            productsInBakery[1] = new Cake("Orange Cake", 8.3, 80, false, new Time_date(), 10, 8, false);
            productsInBakery[2] = new Bread("Shiffon Bread", 10.5, 90, false, new Time_date(), 10, true, false, false);
            productsInBakery[3] = new Bread("Shabath Hala", 12.4, 120, false, new Time_date(), 10, false, true, false);
            productsInBakery[4] = new Cookie("Maple Cookie", 8.49, 30, false, new Time_date(), 10, 5);
            productsInBakery[5] = new Cookie("Vegan Cookie", 7.2, 28, false, new Time_date(), 10, 3);
            productsInBakery[6] = new Cookie("Chocolate Chips Cookie", 10.8, 35, true, new Time_date(), 10, 10);
            productsInBakery[7] = new Drink("Coffee", 5, 40, true, new Time_date(), 10, 200, true);
            productsInBakery[8] = new Drink("Orange Juice", 3, 80, false, new Time_date(), 20, 350, false);
            productsInBakery[9] = new Drink("CocaCola", 5, 150, false, new Time_date(), 30, 330, false);

            TheBakery ArielUniversityBakery = new TheBakery(productsInBakery, employees);

            //============================================The Code!=================================================================

            Console.ForegroundColor = ConsoleColor.Cyan;

            int threadOrManual = 4, switchUserChoose = 7, clientListLength = 0, outerLoopInt;

            bool outerLoop = true, mainLoop = true, innerLoop = true;

            DateTime dateTime = DateTime.Now;
            string thisAppUse = dateTime.Day + "-" + dateTime.Month + "-" + dateTime.Year +
                " " + dateTime.Hour + "-" + dateTime.Minute + "-" + dateTime.Second + "-" + dateTime.Millisecond,
                logFolderPath = @"C:\Logs\" + thisAppUse, caseLog = "";

            while (outerLoop == true)
            {
                while (mainLoop == true && outerLoop == true)
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
                    if (threadOrManual == 999)
                    {
                        outerLoop = false;
                    }

                    innerLoop = true; // Reinitilize these bools for the next while iterations.
                    mainLoop = true;

                    switch (threadOrManual)
                    {
                        case 1: // Manual case.

                            caseLog = logFolderPath + " Manual";

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

                            Console.ResetColor();

                            Console.WriteLine();

                            client.sayHi();
                            CreateCurrentUseLogFolder(client, caseLog, dateTime);

                            Console.WriteLine();

                            Console.WriteLine("Welcome to Ariel University Bakery!\nChoose one of the options: \n" +
                    "1) Buy in the bakery.\n" +
                    "2) File a complaint.\n" +
                    "3) Remove old products.\n" +
                    "4) Ask the kitchen to make more.\n" +
                    "5) Ask the bar tender to re-fill the drinks.\n" +
                    "6) Get the count of money the bakery earned.\n" +
                    "7) Close the bakery or gets to a former menu.\n");

                            while (innerLoop == true)
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
                                        WriteShoppingListBuyingToLog(client, caseLog, ArielUniversityBakery);
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
                                        FinalLogWriting(client, dateTime, caseLog);
                                        ArielUniversityBakery.closeTheBakery(); // Close the bakery. Should close the manual switch case.
                                        innerLoop = false;
                                        break;
                                }

                            } // Don't move this!!!

                            Console.ResetColor();
                            break;  // The break belongs to threadOrManual.

                        /*====== Thread case=====*/

                        case 2: // Second loop for threads.                            

                            int loopStopTime = 0;
                            double maxTimeRun = 0;
                            caseLog = logFolderPath + " Thread";

                            Console.Write("How long would you like to run the program in minutes? "); // Asks the user how long to run the program. The calculating is down the road.
                            while (maxTimeRun == 0) {
                                try
                                {
                                    maxTimeRun = Convert.ToDouble(Console.ReadLine()); // User Choose how to run the program (manualy or by threads).
                                    Console.WriteLine();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    string errorCatch = e.StackTrace;
                                }
                            }

                            Thread BackGroundThread1 = new Thread(play);

                            BackGroundThread1.IsBackground = true;
                            BackGroundThread1.Start();

                            for (int i = 0; i < ArielUniversityBakery.Employees.Length; i++)
                            {
                                Console.WriteLine("Hi!\nMy name is " + ArielUniversityBakery.Employees[i].FirstName + " " + ArielUniversityBakery.Employees[i].LastName + ". My ID is " + ArielUniversityBakery.Employees[i].Id + ".");
                                Console.WriteLine();
                            }


                            Thread[] allClientsThread = new Thread[5];

                            for (int i = 0; i < allClientsThread.Length; i++)
                            {
                                allClientsThread[i] = new Thread(() => ClientStart(ArielUniversityBakery, thisAppUse, logFolderPath, dateTime, caseLog));
                                allClientsThread[i].Start();
                            }

                            maxTimeRun = maxTimeRun * 60000;

                            while (loopStopTime <= maxTimeRun)
                            { // While the loop is still running Clients keep coming to the bakery.
                                for (int i = 0; i < allClientsThread.Length; i++) // A loop for initializing the threads of the clients and start them.
                                {
                                    if (allClientsThread[i].IsAlive == false)
                                    {
                                        allClientsThread[i].Join();
                                        allClientsThread[i] = new Thread(() => ClientStart(ArielUniversityBakery, thisAppUse, logFolderPath, dateTime, caseLog));
                                        allClientsThread[i].Start();
                                    }
                                }

                                if (loopStopTime % 60000 >= 0 && loopStopTime >= 100000)
                                {
                                    ((Baker)ArielUniversityBakery.Employees[1]).bakingLikeHell(ArielUniversityBakery);
                                    ((BarTender)ArielUniversityBakery.Employees[3]).CreateNewDrinks(ArielUniversityBakery);
                                }

                                Thread.Sleep(1000);
                                loopStopTime += 1000;
                            }

                            //Thread.Sleep(maxTimeRun+1000); // Keeps time until the end of the loop while the threads are working.
                            int coundDeadThreads = allClientsThread.Length;

                            while (coundDeadThreads > 0) // Checks if all the threads are dead and if so- stop the program running. 
                            {
                                if (coundDeadThreads > 0)
                                {
                                    coundDeadThreads = allClientsThread.Length;
                                }

                                for (int i = 0; i < allClientsThread.Length; i++)
                                {
                                    if (allClientsThread[i].IsAlive == false)
                                    {
                                        coundDeadThreads--;
                                    }
                                }
                                Thread.Sleep(500);
                            }

                            if (coundDeadThreads == 0) // If all the threads are dead the program can get to its end peacfully.
                            {
                                ArielUniversityBakery.closeTheBakery(); // Send this day details to word document.
                                BackGroundThread1.Join();
                            }

                            Console.ResetColor();
                            break; // Breaks the second switch case.
                    } // Breaks the first switch case (threadOrManual).

                    // Out of inner loop.

                }
                Console.WriteLine("Are you sure? 1 - No | Any other key - Yes");
                outerLoopInt = Convert.ToInt32(Console.ReadLine());
                if (outerLoopInt == 1)
                {
                    outerLoop = true;
                }
                else
                {
                    outerLoop = false;
                }
            }
            Console.ReadLine();
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
            Console.ForegroundColor = ConsoleColor.DarkGreen;
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

        public static void ClientStart(TheBakery ArielUniversityBakery, string thisAppUse, string logFolderPath, DateTime dateTime, string caseLog)
        {

            bool buying = false, complaintFile = false; // Methods the client must do.

            Random randomEmployee = new Random();

            Client threadClient = new Client(ArielUniversityBakery); // Initialize the client.
            CreateCurrentUseLogFolder(threadClient, caseLog, dateTime);

            threadClient.sayHiThread();
            Console.ResetColor();

            while (buying == false || complaintFile == false)
            { // When all bool conditions are true the method is done.*/

                if (buying == false)
                {
                    threadClient.buyByThread(ArielUniversityBakery);
                    buying = true;
                    Console.ResetColor();
                }
                else if (complaintFile == false)
                {
                    if (threadClient.IsAngryThread == true)
                    {
                        threadClient.FileComplaint(ArielUniversityBakery);
                        complaintFile = true;
                        Console.ResetColor();
                    }
                    else
                    {
                        complaintFile = true;
                        Console.ResetColor();
                    }
                }
                Thread.Sleep(500);
            }

            for (int i = 0; i < threadClient.List.Length; i++)
            {
                if (threadClient.List[i].BoughtProducts > 0)
                {
                    Console.WriteLine($"{threadClient.FirstName} {threadClient.LastName} bought {threadClient.List[i].BoughtProducts} {threadClient.List[i].NameOfProduct}");
                }
            }
            ArielUniversityBakery.isHappy(threadClient);

            Console.WriteLine(threadClient.FirstName + " " + threadClient.LastName + ": Thank you!\n");
            WriteShoppingListBuyingToLog(threadClient, caseLog, ArielUniversityBakery);
            FinalLogWriting(threadClient, dateTime, caseLog);
            Console.ResetColor();
        }

        public static void CreateCurrentUseLogFolder(Client client, string caseLog, DateTime dateTime) // Method to create a new Folder for logs.
        {
                if (!(Directory.Exists(caseLog)))
                {
                    Directory.CreateDirectory(caseLog);
                }

            StreamWriter logsTextWriter = new StreamWriter(caseLog + "\\" + client.FirstName + " " + client.LastName + " " + client.Id + ".txt", true);

            logsTextWriter.WriteLine(client.FirstName + " " + client.LastName + " " + client.Id + "\n" +
                dateTime.Day + "/" + dateTime.Month + "/" + dateTime.Year + " " +
                dateTime.Hour + ":" + dateTime.Minute + ":" + dateTime.Second + ":" + dateTime.Millisecond + " ENTERED\n\n" +
                "Shopping List:\n");

            logsTextWriter.Close();
        }

        public static void WriteShoppingListBuyingToLog(Client client, string caseLog, TheBakery bakery)
        {
            StreamWriter logsTextWriter = new StreamWriter(caseLog + "\\" + client.FirstName + " " + client.LastName + " " + client.Id + ".txt", true);

            for (int i = 0; i < client.List.Length; i++)
            {
                for (int j=0; j<bakery.ProductsInBakery.Length; j++)
                {
                    if (client.List[i].NameOfProduct.Equals(bakery.ProductsInBakery[j].Name)) {
                        logsTextWriter.WriteLine("Name of product: " + client.List[i].NameOfProduct +
                            " | Demand: " + client.List[i].DemandOfProducts +
                            " | Bought: " + client.List[i].BoughtProducts + "" +
                            " | Price for product: " + bakery.ProductsInBakery[j].Price * client.List[i].BoughtProducts);
                    }
                }
            }
            logsTextWriter.Close();
        }

        public static void FinalLogWriting(Client client, DateTime dateTime, string caseLog)
        {

                StreamWriter logsTextWriter = new StreamWriter(caseLog + "\\" + client.FirstName + " " + client.LastName + " " + client.Id  + ".txt", true);

                logsTextWriter.WriteLine("\n" + client.FirstName + " " + client.LastName + " total purchase: " + client.PurchaseSummary + "\n" +
                    dateTime.Day + "/" + dateTime.Month + "/" + dateTime.Year + " " +
                    dateTime.Hour + ":" + dateTime.Minute + ":" + dateTime.Second + ":" + dateTime.Millisecond + " THE CUSTOMER LEFT");

                logsTextWriter.Close();
        }
    }
}