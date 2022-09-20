using System;
using System.Collections.Generic;
using System.Text;
using Bakery.Employee;
using Bakery.Products;
using Bakery.Clients;
using System.Threading;
using System.Media;
using Bakery.BakeryLogic;
using Bakery.Other;


namespace Bakery.Employee
{
    class BarTender : BakeryEmployee
    {
        private int drinksMade;
       
        public BarTender(string firstName, string lastName) : base(firstName, lastName)
        {
            this.drinksMade = 0;            
        }

        public int DrinksMade
        {
            get { return drinksMade; }
            set { drinksMade = value; }
        }
        public void makeDrinks(Client client, TheBakery bakery) // The method checks the client's shopping list and the barTender makes the drinks.
        {
            int makingTime = 0;            
            bool needMoreDrinks = false;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            for (int i = 0; i<client.List.Length; i++)
            {
               for(int j=0; j<bakery.ProductsInBakery.Length; j++)
               {
                    if (client.List[i].NameOfProduct.Equals(bakery.ProductsInBakery[j].Name) && bakery.ProductsInBakery[j] is Drink)
                    {
                        while (makingTime < (750 * client.List[i].DemandOfProducts))
                        {

                            if (client.List[i].DemandOfProducts <= bakery.ProductsInBakery[j].AmountInBakery)
                            {
                                client.List[i].BoughtProducts = client.List[i].DemandOfProducts;
                                client.PurchaseSummary += ((double)bakery.ProductsInBakery[j].Price * client.List[i].BoughtProducts);
                                bakery.MoneyEarned += client.PurchaseSummary;
                            }
                            else if (client.List[i].DemandOfProducts > bakery.ProductsInBakery[j].AmountInBakery)
                            {
                                client.List[i].BoughtProducts = bakery.ProductsInBakery[j].AmountInBakery;
                                client.PurchaseSummary += ((double)bakery.ProductsInBakery[j].Price * bakery.ProductsInBakery[j].AmountInBakery);
                                bakery.MoneyEarned += client.PurchaseSummary;
                                needMoreDrinks = true;
                            }                            
                            Thread.Sleep(1000);
                            makingTime += 1000;
                        }
                    } 
               }
            }
            // For every Drink that is ready there is a bell ring to call the client.
            SoundPlayer sound = new SoundPlayer(@"C:\Users\User\Desktop\Audio\bellRinging04.wav");
            sound.Load();
            sound.Play();
            Thread.Sleep(1500); // The sleep is inavoidable. Ohterwise, the bell won't ring.        

            for(int l=0; l<client.List.Length; l++)
            {
                if (client.List[l].GetType() == typeof(Drink) && client.List[l].BoughtProducts > 0)
                {
                    Console.WriteLine("11111111111111111111111111");
                    Console.WriteLine($"{client.FirstName} {client.LastName} bought  {client.List[l].BoughtProducts} {client.List[l].NameOfProduct}");
                    Console.WriteLine("22222222222222222222222222");

                }
            }

            if (needMoreDrinks is true)
            {
                this.CreateNewDrinks(bakery);
            }
            this.aRE.Reset(); // Resets the AutoResetEvent to true again so the next thread could use it.
            Console.WriteLine(client.FirstName + " " + client.LastName + " cost of purchase " + client.PurchaseSummary);
        }

        public void CreateNewDrinks(TheBakery bakery)
        {
            Random reFillDrinks = new Random();

            Random year = new Random(); // New random year variable.

            Random month = new Random(); // New random month variable.

            Random day = new Random(); // New random day variable.

            Time_date newExpieryDate = new Time_date(0,0,0);

            DateTime thisDate = new DateTime();

            for (int i=0; i<bakery.ProductsInBakery.Length; i++)
            {
                if (bakery.ProductsInBakery[i] is Drink && bakery.ProductsInBakery[i].AmountInBakery<5 && bakery.ProductsInBakery[i].AmountInBakery > 0)
                {
                    Thread.Sleep(1500);
                    bakery.ProductsInBakery[i].AmountInBakery += reFillDrinks.Next(1, 15);
                }
                else if (bakery.ProductsInBakery[i] is Drink && bakery.ProductsInBakery[i].AmountInBakery is 0)
                {
                    Thread.Sleep(1500);
                    bakery.ProductsInBakery[i].AmountInBakery += reFillDrinks.Next(1, 15);
                    bakery.ProductsInBakery[i].ExpieryDate = null;


                    while (bakery.ProductsInBakery[i].ExpieryDate is null | bakery.ProductsInBakery[i].ExpieryDate.Isokay is false)
                    {

                        bakery.ProductsInBakery[i].ExpieryDate.Year = year.Next(thisDate.Year, thisDate.Year + 3);
                        // make randomally new expiery date to the new products.

                        bakery.ProductsInBakery[i].ExpieryDate.Month = month.Next(1, 12);

                        bakery.ProductsInBakery[i].ExpieryDate.Day = day.Next(1, 31);
                    }
                }

            }
            for (int i=0; i<bakery.ProductsInBakery.Length; i++)
            {
                if (bakery.ProductsInBakery[i] is Drink)
                {
                    Console.WriteLine(bakery.ProductsInBakery[i].Name+" ----- "+ bakery.ProductsInBakery[i].AmountInBakery); // Prints the amount the bar tender added.
                }
            }
            this.aRE.Reset(); // Resets the AutoResetEvent to true again so the next thread could use it.
        }
    }
}