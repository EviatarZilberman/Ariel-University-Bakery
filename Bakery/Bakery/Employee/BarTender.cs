using Bakery.BakeryLogic;
using Bakery.Clients;
using Bakery.Other;
using Bakery.Products;
using System;
using System.Threading;


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

        public void makeDrinks(Client client, TheBakery bakery)
        {
            bool needMoreDrinks = false;
            for (int i = 0; i < client.List.Length; i++)
            {
                for (int j = 0; j < bakery.ProductsInBakery.Length; j++)
                {
                    if (client.List[i].NameOfProduct.Equals(bakery.ProductsInBakery[j].Name)
                    && bakery.ProductsInBakery[j].GetType() == typeof(Drink)
                    && client.List[i].DemandOfProducts <= bakery.ProductsInBakery[j].AmountInBakery)
                    {
                        client.List[i].BoughtProducts = client.List[i].DemandOfProducts;
                        bakery.MoneyEarned += (double)bakery.ProductsInBakery[j].Price * (double)client.List[i].BoughtProducts;
                        bakery.ProductsInBakery[j].AmountInBakery -= client.List[i].BoughtProducts;
                        client.PurchaseSummary += (double)bakery.ProductsInBakery[j].Price * (double)client.List[i].BoughtProducts;
                    }
                    else if (client.List[i].NameOfProduct.Equals(bakery.ProductsInBakery[j].Name)
                    && bakery.ProductsInBakery[j].GetType() == typeof(Drink)
                    && client.List[i].DemandOfProducts > bakery.ProductsInBakery[j].AmountInBakery)
                    {
                        client.List[i].BoughtProducts = bakery.ProductsInBakery[j].AmountInBakery;
                        bakery.MoneyEarned += (double)bakery.ProductsInBakery[j].Price * (double)client.List[i].BoughtProducts;
                        bakery.ProductsInBakery[j].AmountInBakery = 0;
                        client.PurchaseSummary += (double)bakery.ProductsInBakery[j].Price * (double)client.List[i].BoughtProducts;
                        needMoreDrinks = true;
                    }
                }
            }
            for (int l = 0; l < client.List.Length; l++)
            {
                if (!(client.List[l].GetType() == typeof(Drink)) && client.List[l].BoughtProducts > 0)
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.WriteLine(client.FirstName + " " + client.LastName + " bought " + client.List[l].BoughtProducts + " " + client.List[l].NameOfProduct);
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
            Thread.Sleep(1500);

            if (needMoreDrinks == true)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                ((BarTender)bakery.Employees[3]).CreateNewDrinks(bakery); // Call the baker to make more products that are not drinks.
                Console.ResetColor();
            }

        }

        public void CreateNewDrinks(TheBakery bakery)
        {
            Random reFillDrinks = new Random();

            Random year = new Random(); // New random year variable.

            Random month = new Random(); // New random month variable.

            Random day = new Random(); // New random day variable.

            Time_date newExpieryDate = new Time_date(0, 0, 0);

            //DateTime thisDate = new DateTime();

            for (int i = 0; i < bakery.ProductsInBakery.Length; i++)
            {
                if (bakery.ProductsInBakery[i].GetType() == typeof(Drink) && bakery.ProductsInBakery[i].AmountInBakery < 5)
                {
                    Thread.Sleep(1500);
                    bakery.ProductsInBakery[i].AmountInBakery += reFillDrinks.Next(1, 15);

                    bakery.ProductsInBakery[i].ExpieryDate= new Time_date(); // Creates the new expiery date by random.
                }
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("I brought more drinks!\n");
            Console.ResetColor();
            this.aRE.Reset(); // Resets the AutoResetEvent to true again so the next thread could use it.
        }
    }
}