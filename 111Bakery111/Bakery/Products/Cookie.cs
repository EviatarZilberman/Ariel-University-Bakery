using System;
using System.Collections.Generic;
using System.Text;
using Bakery.Products;
using Bakery.Other;

namespace Bakery.Products
{
    class Cookie : Product // Cookies inheritate Product.
    {
        private int cookiesInBox;

    // Constructor with inheritance.
    public Cookie(string name, double price, int calories, bool hasMilk, Time_date expieryDate, int amountInBakery,
        int cookiesInBox) : base(name, price, calories, hasMilk, expieryDate, amountInBakery)
        {
        this.cookiesInBox = cookiesInBox;
    }
public int CookiesInBox
        {
            get { return cookiesInBox; }
        }
    }
}

