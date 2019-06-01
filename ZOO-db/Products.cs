using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZOO_db
{
    class Products
    {

        private Int32 _product_ID;
        private String _name;
        private Int32 _price;
        private Int32 _shopID;


        public Int32 ProductID
        {
            get { return _product_ID; }
            set { _product_ID = value; }
        }


        public String Name
        {
            get { return _name; }
            set
            {
                if (value == null | String.IsNullOrEmpty(value))
                {
                    throw new Exception("First Name can’t be empty");
                    return;
                }
                _name = value;
            }
        }

        public Int32 Price
        {
            get { return _price; }
            set
            {
                if (value == null )
                {
                    throw new Exception("Price can’t be empty");
                    return;
                }
                _price = value;
            }
        }

        public Int32 ShopID
        {

            get { return _shopID; }
            set
            {
                if (value == null)
                {
                    throw new Exception("shop ID must be valid");
                    return;
                }
                _shopID = value;
            }
        }


        public override String ToString()
        {
            return _name + "   " + _price;
        }

        public Products() : base()
        {
        }

        public Products(String name ,Int32 price,Int32 shopID) : base()
        {
            this.Name = name;
            this.Price = price;
            this.ShopID = shopID;
        }


    }
}

