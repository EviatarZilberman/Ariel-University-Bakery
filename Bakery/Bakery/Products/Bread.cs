using System;
using System.Collections.Generic;
using System.Text;
using Bakery.Products;
using Bakery.Other;

namespace Bakery.Products
{
    class Bread :Product
    {
        private bool sliced;
        private bool forShabath;
        private bool blackBread;

        public Bread(string name, double price, int calories, bool hasMilk, Time_date expieryDate,
            int amountInBakery, bool sliced, bool forShabath, bool blacBread)
            : base(name, price, calories, hasMilk, expieryDate, amountInBakery)
        {
            this.sliced = sliced;

            if (this.sliced == true)
            {
                this.forShabath = false;
            }
            else this.forShabath = true;

            this.blackBread = true;
        }

        public bool Sliced
        {
            get { return sliced; }
            set { sliced = value; }
        }

        public bool ForShabath
        {
            get { return forShabath; }
            set { forShabath = value; }
        }

        public bool BlackBread
        {
            get { return blackBread; }
        }

    }
}
