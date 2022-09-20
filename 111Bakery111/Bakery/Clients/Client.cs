using Bakery.Products; // import the Products file so I can use the Class Product.
using System;
using System.Collections.Generic;
using System.Text;
using Bakery.BakeryLogic;
using Bakery.Clients;
using Bakery.Employee;
using System.Linq;
using System.Threading;



namespace Bakery.Clients
{
    class Client // Class atributes.
    {
        private string firstName;
        private string lastName;
        private ShoppingList[] list;
        private bool isAngryThread;
        private static double PurchaseSummarry;



        // Constructor for manual mode.
        public Client(string firstName, string lastName, int shoppingListLength)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.list = new ShoppingList[shoppingListLength];
            for (int i=0; i<this.list.Length; i++) // Initilizes the ShoppingList array.
            {
               this.list[i] = new ShoppingList();
            }
            PurchaseSummarry = 0;
        }
        // Constructor for thread mode.
        public Client(TheBakery bakery)
        {
            /*2 arrays of names- first and last.*/

            string[] firstNameArchive = { "Alexander", "Bill", "Muhamad", "Jacob", "Tom", "Hila", "Steeve", "Johnatan", "Nicholas", "Harry", "Ronald", "Jessica", "Witney", "Miranda", "Haryet", "Meirav", "Angelina", "Pricilla", "Christin", "Katy", "Artemis", "Tonny", "Bruce", "Karamon", "Reistlin", "James", "Bat'El" };

            string[] lastNameArchive = { "Cohen", "Blau", "Jackson", "Profus", "Resler", "Kinn", "Majer", "Levi", "Russle", "Charter", "Bond", "Wein", "Stark", "Rogers", "Shech", "Fowl", "Mccartney", "Mcdonald", "Yung", "Wexler", "Peri", "Clinton", "Chahla", "Levi", "Silverman", "Abdel-Rahman" };

            Random firstNameindex = new Random(); // 4 Random objects for random clients generation.

            Random lastNameindex = new Random();

            Random shoppingListLengthThread = new Random();

            Random randomBoolIsAngry = new Random();


            this.firstName = firstNameArchive[firstNameindex.Next(0, firstNameArchive.Length)]; // Random first name from array.
            this.lastName = lastNameArchive[lastNameindex.Next(0, lastNameArchive.Length)]; // Random first name from array.
            this.list = new ShoppingList[shoppingListLengthThread.Next(1, bakery.ProductsInBakery.Length)]; // Random shpping list array length.
            for (int i = 0; i < this.list.Length; i++) // Initilizes the ShoppingList array.
            {
                this.list[i] = new ShoppingList();
            }
            this.fillShoppingList(bakery);
            this.isAngryThread = randomBoolIsAngry.NextDouble() > 0.5; // If the result bigger than 0.5 = true, otherwise = false. A way to randomize bool.
            PurchaseSummarry = 0; // Static argument can't get 'this'.
        }
        // Getters & Setters.
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public ShoppingList [] List
        {
            get { return list; }
            set { list = value; }
        }

        public bool IsAngryThread
        {
            get { return isAngryThread; }
            set { isAngryThread = value; }
        }

        public double PurchaseSummary
        {
            get { return PurchaseSummarry; }
            set { PurchaseSummarry = value; }
        }

        public void sayHi() // The client UI when it gets into the Bakery.
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Hi! My name is " + this.firstName + " " + this.lastName + " and I want to buy in your Bakery!" +
                " I hope you have all I want!");
            Console.WriteLine();
            Console.ResetColor();
        }

        public void sayHiThread() // The client UI when it gets into the Bakery.
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("Hi! My name is " + this.firstName + " " + this.lastName + " and I want to buy in your Bakery!" +
                " I hope you have all I want! My list includes: ");
            for (int i=0; i<this.List.Length; i++)
            {
                Console.WriteLine(this.List[i].NameOfProduct);
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        public void buyByUser(TheBakery bakery) // When manual and the user
                                                // chooses what he wants to buy,
                                                // the menu will be presented.
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;

            int clientProductsList = 0;
            double totalSum = 0;

            for (int i = 0; i < bakery.ProductsInBakery.Length; i++) // Prints the menu and the prices.
            {
                int menuNumber = i + 1;
                Console.WriteLine("#" + menuNumber + " " + bakery.ProductsInBakery[i].Name + "-----" + bakery.ProductsInBakery[i].Price);
            }
            try // Try to let the user Choose from the numbers of the products.
            {
                    for (int i=0; i<this.list.Length; i++)
                    {
                Console.Write("Choose from the products list: ");
                int productUserChoose = Convert.ToInt32(Console.ReadLine())-1;
                Console.Write("Choose from the amount you want: ");
                int amountUserChoose = Convert.ToInt32(Console.ReadLine());
                    this.List[i].NameOfProduct = bakery.ProductsInBakery[productUserChoose].Name;
                    this.List[i].DemandOfProducts = amountUserChoose; // Set the client's list of shopping names of products.
                                              
                    if (bakery.ProductsInBakery[productUserChoose].AmountInBakery<= amountUserChoose) // If there are more products in the bakery than the client wants.
                    {
                        this.List[i].BoughtProducts = amountUserChoose;
                    }
                    else
                    {
                        this.List[i].BoughtProducts = bakery.ProductsInBakery[productUserChoose].AmountInBakery;// If there aren't products in the bakery than the client wants.
                    }
                    totalSum += this.List[i].BoughtProducts * bakery.ProductsInBakery[clientProductsList++].Price;
                    PurchaseSummarry += totalSum;
                    bakery.MoneyEarned += totalSum;
                }
            }
            catch
            {
                Console.WriteLine("Choose from the list");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n=====   =====   =====\n");
            Console.WriteLine("Your Total sum is "+ totalSum+" You bought:");
            for (int i = 0; i < this.list.Length; i++)
            {
                Console.WriteLine(this.List[i].NameOfProduct);               
            }
            Console.WriteLine("\n=====   =====   =====\n");

            Console.ResetColor();
        }

        public void fillShoppingList(TheBakery bakery) // A method that fill the shopping array
                                                                   // of every client with different products.
        {
            /* 3 Random objects to fill the shopping list with demands.*/
            Random randomProductName = new Random();
            Random randomProductAmount = new Random();
            Random randomTimeToFindProduct = new Random();

            for (int i = 0; i < this.list.Length; i++)
            {
                Thread.Sleep(randomTimeToFindProduct.Next(100, 2000)); // Random sleep time to find a product on the shelf.
                this.list[i].NameOfProduct = bakery.ProductsInBakery[randomProductName.Next(0, bakery.ProductsInBakery.Length)].Name; // Sets the Shopping list of the client.
                this.list[i].DemandOfProducts = randomProductAmount.Next(1, 10); // Sets the demanded amount of product the client wants.        
            }
            for (int i = 0; i < this.list.Length; i++) // 3 loops to make sure that all the
                                                             // products in the array are defferent.
            {
                for (int j = 0; j < this.list.Length & j != i; j++)
                {
                    if (this.list[i].NameOfProduct.Equals(this.list[j].NameOfProduct))
                    {
                        while (this.list[i].NameOfProduct.Equals(this.list[j].NameOfProduct)) // Randomize a specific index of product name until all the products are different.
                        {
                            this.list[i].NameOfProduct = bakery.ProductsInBakery[randomProductName.Next(0, bakery.ProductsInBakery.Length)].Name;
                        }
                    }
                }
            }
           
        }

        public void FileComplaint(TheBakery bakery) // Method that randomally files complaints on random employees.
        {
                Console.ForegroundColor = ConsoleColor.DarkRed;

                Random idToComplain = new Random(); // To recognize the employee to complain.

                Random specificComplaint = new Random(); // Get specific complaint.

                string complainedEmployee = "";

                int sComplaint = specificComplaint.Next(0, 4), sEmploee = idToComplain.Next(0, bakery.Employees.Length);

            if (bakery.Employees[sComplaint] is BakeryManager) // Find the type of the employee for the complain and sets it into a string.
            {
                complainedEmployee = "manager";
            } else if(bakery.Employees[sEmploee] is Baker)
            {
                complainedEmployee = "baker";

            } else if (bakery.Employees[sComplaint] is Cashier)
            {
                complainedEmployee = "cashier";

            }
            else
            {
                complainedEmployee = "bar tender";

            }

            if (this.isAngryThread is true && bakery.Employees[sEmploee].Complaints.Length > bakery.Employees[sEmploee].NumberOfComplaints)
            {
                if (sComplaint is 0 & sComplaint < bakery.Employees[sEmploee].Complaints.Length)// Find specific complaint from archive.
                {
                    bakery.Employees[sEmploee].Complaints[bakery.Employees[sEmploee].NumberOfComplaints++] = "There are not enough Borekas's";
                }
                else if (sComplaint is 1)
                {
                    bakery.Employees[sEmploee].Complaints[bakery.Employees[sEmploee].NumberOfComplaints++] = "The service is slow";
                }
                else if (sComplaint is 2)
                {
                    bakery.Employees[sEmploee].Complaints[bakery.Employees[sEmploee].NumberOfComplaints++] = "The bakery isn't clean";
                }
                else if (sComplaint is 3)
                {
                    bakery.Employees[sEmploee].Complaints[bakery.Employees[sEmploee].NumberOfComplaints++] = "There is no parking near by";
                }
                else if (sComplaint is 4)
                {
                    bakery.Employees[sEmploee].Complaints[bakery.Employees[sEmploee].NumberOfComplaints++] = "This is so expensive!";
                } 
            }
                else
                {
                    Console.WriteLine("We will fire this employee after that shift!");
                }
            // Prints the complain and the charger.
            if (bakery.Employees[sEmploee].NumberOfComplaints <= bakery.Employees[sEmploee].Complaints.Length)
            {
                Console.WriteLine(this.firstName + " " + this.lastName + " filed a complain about the " + complainedEmployee + ". The complaint is: " + bakery.Employees[sEmploee].Complaints[bakery.Employees[sEmploee].NumberOfComplaints - 1] + "\n");
            } else { 
            Console.WriteLine("We wiil fire this employee!");
            }
            Console.ResetColor();
        }
        public void buyByThread(TheBakery bakery) // Run the client in thread. The method does all the shopping in thread mode.
        { 

            if (this.list[0].GetType() == typeof(Drink)                
                //&& bakery.Employees[3].ARE.WaitOne(0, true)
                )
            {
                ((BarTender)bakery.Employees[3]).makeDrinks(this, bakery); // Call the bar tender to make more products that are not drinks.
            }
            else if (!(this.list[0].GetType() == typeof(Drink))
                //&& bakery.Employees[1].ARE.WaitOne(0, true)
                )
            {
                ((Cashier)bakery.Employees[2]).buyProductsThread(this, bakery); // Call the baker to make more products that are not drinks.
            }
            Console.ResetColor(); // Reset to basic color just to be safe.
        }
    }
}