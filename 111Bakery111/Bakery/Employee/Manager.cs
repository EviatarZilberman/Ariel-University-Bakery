using System;
using System.Collections.Generic;
using System.Text;
using Bakery.Employee;
using Bakery.BakeryLogic;

namespace Bakery.Employee
{
    class BakeryManager : BakeryEmployee
    {
        private int reasonsToBeAngry;
        
        public BakeryManager(string firstName, string lastName, int reasonsToBeAngry)
            : base(firstName, lastName)
        {
            this.reasonsToBeAngry = reasonsToBeAngry;
        }

        public int ReasonsToBeAngry
        {
            get { return reasonsToBeAngry; }
            set { reasonsToBeAngry = value; }
        }

     

        public void callTheBaker(Baker baker, TheBakery bakery)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("Hey! Lazy baker! start doing your job!");

            baker.bakingLikeHell(bakery); //  Call a method from Baker Class.
        }

        public void gettingAngry()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("I want to fire you all!!!");
        }

        public void fileComplaint(TheBakery bakery)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            int idToComplain = 0, i=0;
            string complain = " ";
            bool isComplained = false;
            Console.Write("Enter the ID of the employee you want to file a complaint about: ");
           
                try // Exception to prevent code crush if the user enters a wrong number.
                {
                    idToComplain = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                Console.WriteLine(e.Message);
                string errorCatch = e.StackTrace;
                //Console.WriteLine("Choose from the options!");
            }

                for (; i < bakery.Employees.Length; i++)
                {
                    if (bakery.Employees[i].Id == idToComplain)
                    {
                    complain= Console.ReadLine();
                    bakery.Employees[i].Complaints[bakery.Employees[i].NumberOfComplaints++] = complain;
                    isComplained = true;
                    }
                }
          if  (isComplained is true) {
                Console.Write($"Your complaint was filed: {complain}");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("The comaplain wasn't filed!");
                Console.WriteLine();
            }
        }
    }
}

