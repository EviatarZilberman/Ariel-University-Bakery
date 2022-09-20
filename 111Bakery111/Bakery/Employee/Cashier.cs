using System;
using System.Collections.Generic;
using System.Text;
using Bakery.Employee;
using System.Threading;
using Bakery.BakeryLogic;
using Bakery.Clients;
using Bakery.Products;

namespace Bakery.Employee
{
    class Cashier : BakeryEmployee
    {
        private double moneyEarned;
        
        


        public Cashier(string firstName, string lastName) 
            : base(firstName, lastName)
        {
            this.moneyEarned=0;               
        }

        public double MoneyEarned
        {
            get { return moneyEarned; }
            set { moneyEarned = value; }
        }

            public void buyItems(TheBakery bakery, Client client)// Function that check if the products in shopping
                                                             // list of the client exsists in the bakery and set them
                                                             // to a sum of money and update the amount of the product
                                                             // in the bakery.
            {
            Console.ForegroundColor = ConsoleColor.Gray;
            double totalSum = 0; // A variable to sum the buying and present it in the end of the purchasing.

            for (int i = 0; i < client.List.Length; i++) // Run on the shopping list of the client.
            {
                for (int j = 0; j < bakery.ProductsInBakery.Length; j++) // Run on the products list of the bakery to check
                                                                         // if the product from the client list exsists in
                                                                         // the bakery by name and amount.
                {
                    if (!(client.List[i].GetType() == typeof(Drink)))
                    {

                        if ((client.List[i].NameOfProduct.Equals(bakery.ProductsInBakery[j].Name))
                            && (client.List[i].DemandOfProducts <= bakery.ProductsInBakery[j].AmountInBakery)) // If there are enough products and
                                                                                                               // the name fits we set the amount
                                                                                                               // in the bakery and update the
                                                                                                               // client list.
                        {
                            bakery.ProductsInBakery[j].AmountInBakery = bakery.ProductsInBakery[j].AmountInBakery - client.List[i].DemandOfProducts;

                            client.List[i].BoughtProducts = client.List[i].DemandOfProducts;

                            this.MoneyEarned += client.List[i].DemandOfProducts * bakery.ProductsInBakery[j].Price; // Money earned
                                                                                                                    // for specific
                                                                                                                    // cashier.

                            bakery.MoneyEarned += client.List[i].DemandOfProducts * bakery.ProductsInBakery[j].Price; // All money
                                                                                                                      // earned by
                                                                                                                      // cashiers.

                            totalSum += client.List[i].DemandOfProducts * bakery.ProductsInBakery[j].Price;
                        }
                        else if ((client.List[i].NameOfProduct.Equals(bakery.ProductsInBakery[j].Name)) &&
                          (client.List[i].DemandOfProducts > bakery.ProductsInBakery[j].AmountInBakery) &&
                          (bakery.ProductsInBakery[j].AmountInBakery > 0))// If there arne't enough products but there are a few we update
                                                                          // the client list and the bakery products.

                        {
                            client.List[i].BoughtProducts = bakery.ProductsInBakery[j].AmountInBakery;

                            this.MoneyEarned += bakery.ProductsInBakery[j].AmountInBakery * bakery.ProductsInBakery[j].Price;

                            bakery.MoneyEarned += bakery.ProductsInBakery[j].AmountInBakery * bakery.ProductsInBakery[j].Price;

                            totalSum += bakery.ProductsInBakery[j].AmountInBakery * bakery.ProductsInBakery[j].Price;

                            bakery.ProductsInBakery[j].AmountInBakery = 0;
                        }
                    }

                    Console.WriteLine("Your total purchasing sum is " + totalSum);
                }
            }
            this.aRE.Reset(); // Resets the AutoResetEvent to true again so the next thread could use it.
            Console.ResetColor();
            }

        public void buyProductsThread(Client client, TheBakery bakery) // The method checks the client's shopping list and the cashier sells them. If there are not enough products the cashier call the baker to make more.
        {
            
            double totalSum = 0;
            bool needMoreProducts = false;
            Console.ForegroundColor = ConsoleColor.Gray;
            for (int i = 0; i < client.List.Length; i++)
            {
                for (int j = 0; j < bakery.ProductsInBakery.Length; j++)
                {
                    if (client.List[i].NameOfProduct.Equals(bakery.ProductsInBakery[j].Name) && !(bakery.ProductsInBakery[j] is Drink) & client.List[i].DemandOfProducts <= bakery.ProductsInBakery[j].AmountInBakery)
                    {
                        client.List[i].BoughtProducts += client.List[i].DemandOfProducts;
                        totalSum += bakery.ProductsInBakery[j].Price * client.List[i].BoughtProducts;
                        bakery.MoneyEarned += totalSum;

                        //client.List[i].BoughtProducts += bakery.ProductsInBakery[j].AmountInBakery;
                                                                               
                    } else if (client.List[i].NameOfProduct.Equals(bakery.ProductsInBakery[j].Name) && !(bakery.ProductsInBakery[j] is Drink) && client.List[i].DemandOfProducts > bakery.ProductsInBakery[j].AmountInBakery)
                    {
                        client.List[i].BoughtProducts += bakery.ProductsInBakery[j].AmountInBakery;
                        totalSum += bakery.ProductsInBakery[j].Price * bakery.ProductsInBakery[j].AmountInBakery+1;
                        bakery.MoneyEarned += totalSum;                        
                        needMoreProducts = true;
                    }
                }
            }
            for (int l = 0; l < client.List.Length; l++)
            {
                if (!(client.List[l].GetType() == typeof(Drink)) && client.List[l].BoughtProducts > 0)
                {
                    Console.WriteLine(client.FirstName + " " + client.LastName + " bought " + client.List[l].BoughtProducts + " " + client.List[l].NameOfProduct);

                }
            }
            Thread.Sleep(1500);

            if (needMoreProducts is true)
            {
                ((Baker)bakery.Employees[1]).bakingLikeHell(bakery); // Call the baker to make more products that are not drinks.
            }
            this.aRE.Reset(); // Resets the AutoResetEvent to true again so the next thread could use it.
            Console.WriteLine(client.FirstName + " " + client.LastName + " cost of purchase " + totalSum);
            Console.ResetColor();
        }
    }   
}
