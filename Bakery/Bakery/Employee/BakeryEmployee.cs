using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading;

namespace Bakery.Employee
{
    class BakeryEmployee
    {
        protected string firstName;
        protected string lastName;
        protected static int idGen = 1;
        protected int id;
        protected string[] complaints;
        protected int numberOfComplaints;
        protected AutoResetEvent aRE;

        public BakeryEmployee(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.id = idGen++;
            this.complaints = new string[5];
            this.numberOfComplaints = 0;
            this.aRE = new AutoResetEvent(true);                        
        }

        public string FirstName
        {
            get { return firstName; }
            set { lastName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string[] Complaints
        {
            get { return complaints; }
            set { complaints = value; }
        }

        public int NumberOfComplaints
        {
            get { return numberOfComplaints; }
            set { numberOfComplaints = value; }
        }

        public AutoResetEvent ARE
        {
            get { return ARE; }
            set { ARE = value; }
        }
    }
}
