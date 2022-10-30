using System;
using System.Collections.Generic;
using System.Text;
using Bakery.Products;
using Bakery.Other;

namespace Bakery.Products
{
    class Drink : Product // Cake inheritate Product.
    {
        private int amountMl;
        private bool isHot;

    // Constructor with inheritance.
    public Drink(string name, double price, int calories, bool hasMilk, Time_date expieryDate, int amountInBakery,
        int amountMl, bool isHot) : base(name, price, calories, hasMilk, expieryDate, amountInBakery)
    {
            this.amountMl = amountMl;
            this.isHot = isHot;
    }
    // Getters & setters.
    public int AmountMl
        {
            get { return amountMl; }
            set { amountMl = value; }
        }
        public bool IsHot
        {
            get { return isHot; }
            set { isHot = value; }
        }
    }
}
