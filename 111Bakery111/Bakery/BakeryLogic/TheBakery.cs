using System;
using System.Collections.Generic;
using System.Text;
using Bakery.Clients;
using Bakery.Products;
using Bakery.Employee;
using System.Threading;
using Spire.Doc;
using Spire.Doc.Documents;

namespace Bakery.BakeryLogic
{
    class TheBakery
    {
        private Product[] productsInBakery;
        private BakeryEmployee[] employees;
        static private double moneyEarned;
        
        public TheBakery(Product[] productsInBakery, BakeryEmployee[] employees)
        {
            this.productsInBakery = productsInBakery;
            this.employees= employees;
            this.MoneyEarned = 0; // Must have capital letter because it's static.
        }

        public Product[] ProductsInBakery
        {
            get { return productsInBakery; }
            set { productsInBakery = value; }
        }      

        public BakeryEmployee[] Employees
        {
            get { return employees; }
            set { employees = value; }
        }

        public double MoneyEarned
        {
            get { return moneyEarned; }
            set { moneyEarned = value; }
        }

        public void introduceTheStaff() // In every opening of the bakery the staff will introduce itself.
        {
            for (int i = 0; i < this.employees.Length; i++)
            {
                Console.WriteLine("The " + employees[i].GetType() + " name is " + this.employees[i].FirstName + " " +
                    this.employees[i].LastName); // Every employee introduce himself by first and last name.
                Console.WriteLine();

            }
        }
        public void buyItems(Client client, Cashier cashier)// Function that check if the products in shopping
                                                            // list of the client exsists in the bakery and set them
                                                            // to a sum of money and update the amount of the product
                                                            // in the bakery.
        {
            double totalSum = 0; // A variable to sum the buying and present it in the end of the purchasing.

            for (int i = 0; i < client.List.Length; i++) // Run on the shopping list of the client.
            {
                for (int j = 0; j < this.productsInBakery.Length; j++) // Run on the products list of the bakery to check
                                                                       // if the product from the client list exsists in
                                                                       // the bakery by name and amount.
                {
                    if ((client.List[i].NameOfProduct.Equals(productsInBakery[j].Name))
                        && (client.List[i].DemandOfProducts <= productsInBakery[j].AmountInBakery)) // If there are enough products and
                                                                                                    // the name fits we set the amount
                                                                                                    // in the bakery and update the
                                                                                                    // client list.
                    {
                        productsInBakery[j].AmountInBakery = productsInBakery[j].AmountInBakery - client.List[i].DemandOfProducts;

                        client.List[i].BoughtProducts = client.List[i].DemandOfProducts;

                        cashier.MoneyEarned += client.List[i].DemandOfProducts * productsInBakery[j].Price; // Money earned
                                                                                                            // for specific
                                                                                                            // cashier.

                        this.MoneyEarned += client.List[i].DemandOfProducts * productsInBakery[j].Price; // All money
                                                                                                               // earned by
                                                                                                               // cashiers.

                        totalSum += client.List[i].DemandOfProducts * productsInBakery[j].Price;
                    }
                    else if ((client.List[i].NameOfProduct.Equals(productsInBakery[j].Name)) &&
                      (client.List[i].DemandOfProducts > productsInBakery[j].AmountInBakery) &&
                      (productsInBakery[j].AmountInBakery > 0))// If there arne't enough products but there are a few we update
                                                               // the client list and the bakery products.
                    {
                        client.List[i].BoughtProducts = productsInBakery[j].AmountInBakery;

                        cashier.MoneyEarned += productsInBakery[j].AmountInBakery * productsInBakery[j].Price;

                        this.MoneyEarned += productsInBakery[j].AmountInBakery * productsInBakery[j].Price;

                        totalSum += productsInBakery[j].AmountInBakery * productsInBakery[j].Price;

                        productsInBakery[j].AmountInBakery = 0;
                    }
                }
                Console.WriteLine("Your total purchasing sum is " + totalSum);
            }
        }

        public void isHappy(Client client) // Check if the client has gotten all he wanted.
        {
            bool allFound = true;
            for(int i=0; i< client.List.Length; i++)
            {
                if (client.List[i].BoughtProducts != client.List[i].DemandOfProducts)
                {
                    allFound = false;
                }
            }
            if (allFound is false)
            {
                Console.WriteLine($"{client.FirstName} {client.LastName} hasn't found: "); // If the client didn't find anything.
                for (int i=0; i<client.List.Length; i++)
                {
                    if (client.List[i].BoughtProducts != client.List[i].DemandOfProducts)
                    {
                        Console.WriteLine(client.List[i].NameOfProduct);
                    }
                }
            } else
            {
                Console.WriteLine($"{client.FirstName} {client.LastName} found everything and won't sleep on the couch!");
            }
            Console.WriteLine();
        }



        public void productsAmountCheck(TheBakery bakery) // Check if the storage is fine.
        {
            for (int i = 0; i < this.productsInBakery.Length; i++) // The manager checks the amount of every
                                                                   // product in the bakery.
            {
                if (this.productsInBakery[i].AmountInBakery < 5) // If there is any of the product the
                                                                // manager calls the baker to bake more.
                {
                    ((Baker)this.Employees[1]).bakingLikeHell(bakery);

                }
                else if (this.productsInBakery[i].AmountInBakery == 0) // If there is nothing of this product the
                                                                       // manager calls the baker and get a little
                                                                       // more crazy.

                {
                    ((BakeryManager)bakery.Employees[1]).callTheBaker(((Baker)bakery.Employees[1]), this); // Call a method from Manager Class.

                    ((BakeryManager)bakery.Employees[0]).ReasonsToBeAngry++; // Checks how angry the manager.
                }
            }            
        }

        public void closeTheBakery()
        {
            Console.WriteLine("Thank you for buying in Ariel University bakery! here's the money we have earned today: " + this.MoneyEarned);
            Document doc = new Document();
            Section section = doc.AddSection();
            Paragraph para = section.AddParagraph();
            string moneyEarned = this.MoneyEarned.ToString();
            para.AppendText(moneyEarned);
            doc.SaveToFile("EndDay.docx", FileFormat.Docx);
        }
    }
}