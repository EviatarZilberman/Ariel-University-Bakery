using System;
using System.Collections.Generic;
using System.Text;
using Bakery.Products;
using Bakery.Other;

namespace Bakery.Products
{
    class Cake : Product // Cake inheritate Product.
    {
        private int slices;
        private bool forKids;
    

        // Constructor with inheritance.
    public Cake(string name, double price, int calories, bool hasMilk, Time_date expieryDate, int amountInBakery,
        int slices, bool forKids) : base(name, price, calories, hasMilk, expieryDate, amountInBakery)
        {
            this.slices = slices;
            this.forKids = forKids;
    }

        // Getters & setters.
        public int Slices
        {
            get { return slices; }
        }

        public bool ForKids
        {
            get { return forKids; }
            set { forKids = value; }
        }
    }
}
