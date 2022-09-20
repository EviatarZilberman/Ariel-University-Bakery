using System;
using System.Collections.Generic;
using System.Text;
using Bakery.Products;
using Bakery.Clients;

namespace Bakery.Clients
{
    class ShoppingList // object to represent the list of a client. 
    {
        private string nameOfProducts;
        private int demandOfProducts; //how many products the client wants.
        private int boughtProducts = 0; // how many products the clients find.
        
        public ShoppingList(string nameOfProducts, int demandOfProducts)
        {
            this.nameOfProducts = nameOfProducts;
            this.demandOfProducts = demandOfProducts;
            this.boughtProducts = 0;
        }

        public ShoppingList()
        {
            this.nameOfProducts = "";
            this.demandOfProducts = 0;
            this.boughtProducts = 0;
        }

        public string NameOfProduct
        {
            get { return nameOfProducts; }
            set { nameOfProducts = value; }
        }

        public int DemandOfProducts
        {
            get { return demandOfProducts; }
            set { demandOfProducts = value; }
        }

        public int BoughtProducts
        {
            get { return boughtProducts; }
            set { boughtProducts = value; }
        }
    }
}
