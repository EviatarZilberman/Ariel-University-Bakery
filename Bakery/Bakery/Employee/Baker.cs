using System;
using System.Collections.Generic;
using System.Text;
using Bakery.Products;
using Bakery.BakeryLogic;
using System.Threading;
using Bakery.Clients;
using Bakery.Employee;
using Bakery.Enums;
using System.Media;
using System.Windows.Forms;
using System.IO;
using Bakery.Other;
using System.Linq;

namespace Bakery.Employee
{
    class Baker : BakeryEmployee
    {
        private int countOfBaking;

        public Baker(string firstName, string lastName) : base(firstName, lastName)
        {
            this.countOfBaking = 0;
        }

        public int CountOfBaking
        {
            get { return countOfBaking; }
            set { countOfBaking = value; }
        }

        public void bakingLikeHell(TheBakery bakery) // Method that refill the storage
                                                     // of products in the bakery by random number of products.
        {
            Random newAmountOfProduct = new Random(); // Random number of products the baker            

            for (int i=0; i<bakery.ProductsInBakery.Length; i++)
            {
                if (!(bakery.ProductsInBakery[i] is Drink) && bakery.ProductsInBakery[i].AmountInBakery <= 5) // If product needs a refill (count 0),
                                                                  // the baker bakes for a random time,
                                                                  // limited to 5 seconds and bakes random
                                                                  // number of products limited by 10
                                                                  // only if the product is not a drink.
                {
                    Random randomNumber = new Random();

                    Thread.Sleep(randomNumber.Next(500, 1000)); // Time of baking.
                    bakery.ProductsInBakery[i].AmountInBakery+= newAmountOfProduct.Next(1, 10); // A random number of new products.

                    while (bakery.ProductsInBakery[i].ExpieryDate.Day is -1
                        || bakery.ProductsInBakery[i].ExpieryDate.Month is -1 
                        || bakery.ProductsInBakery[i].ExpieryDate.Year is -1)
                    { // Sets a new expiery date to the new products the baker bakes.
                        DateTime thisDate = new DateTime(); // New current time object for random year for the new products.

                        Random year = new Random(); // New random year variable.

                        Random month = new Random(); // New random month variable.

                        Random day = new Random(); // New random day variable.

                        bakery.ProductsInBakery[i].ExpieryDate.Year = year.Next(thisDate.Year, thisDate.Year+3); 
                        // make randomally new expiery date to the new products.

                        bakery.ProductsInBakery[i].ExpieryDate.Month = month.Next(1, 12);

                        bakery.ProductsInBakery[i].ExpieryDate.Day = day.Next(1, 31);
                    }
                }
            }            
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("I baked more!\n");          
            
            Console.ResetColor();
        }

        public void checkExpieryDate(TheBakery bakery) // The method remove all the expiery
                                                       // date products from the storage
                                                       // and call the baker to bake more.
        {
                Console.ForegroundColor = ConsoleColor.Yellow;
            DateTime currentDate = new DateTime(); // New DateTime object for comparing the
                                                   // current date to the products expiery date.
            for (int i = 0; i < bakery.ProductsInBakery.Length; i++)
            {
                if (bakery.ProductsInBakery[i].ExpieryDate.Year < currentDate.Year) // Checks every parameter
                                                                                    // (day, month, year) to see if the
                                                                                    // product's date has expiered.
                                                                                    // If the product's expiery date has
                                                                                    // passed the product's removed from
                                                                                    // the storage.
                {
                    bakery.ProductsInBakery[i].AmountInBakery = 0;
                    Console.WriteLine($"I removed all the {bakery.ProductsInBakery[i].Name}");

                } else if (bakery.ProductsInBakery[i].ExpieryDate.Month < currentDate.Month)
                {
                    bakery.ProductsInBakery[i].AmountInBakery = 0;
                    Console.WriteLine($"I removed all the {bakery.ProductsInBakery[i].Name}");

                } else if (bakery.ProductsInBakery[i].ExpieryDate.Day < currentDate.Day)
                {
                    bakery.ProductsInBakery[i].AmountInBakery = 0;
                    Console.WriteLine($"I removed all the {bakery.ProductsInBakery[i].Name}");

                }
            }
            Console.ResetColor();
            bakingLikeHell(bakery); // Eventually, the baker bakes more products to
                                    // refill the null products in the storage.
        }   
    }
}