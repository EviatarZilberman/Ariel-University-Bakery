using System;
using System.Collections.Generic;
using System.Text;
using Bakery.Other;

namespace Bakery.Products
{
    class Product // Class atributes.
    {
        protected string name;
        protected double price;
        protected int calories;
        protected bool hasMilk;
        protected Time_date expieryDate;
        protected int amountInBakery;
        

        // Constructor of abstruct Class.
        public Product(string name, double price, int calories, bool hasMilk, Time_date expieryDate, int amountInBakery) 
        {
            this.name = name;
            this.price = price;
            this.calories = calories;
            this.hasMilk = hasMilk;
            this.expieryDate = expieryDate;
            this.amountInBakery = amountInBakery;
        }        

        // Getters & setters.
        public string Name
        {
            get { return name;}
            set { name = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public int Calories
        {
            get { return calories; }
            set { calories = value; }
        }

        public bool HasMilk
        {
            get { return hasMilk; }
            set { hasMilk = value; }
        }

        public Time_date ExpieryDate
        {
            get { return expieryDate; }
            set { expieryDate = value; }
        }

        public int AmountInBakery
        {
            get { return amountInBakery; }
            set { amountInBakery = value; }
        }
    }
}