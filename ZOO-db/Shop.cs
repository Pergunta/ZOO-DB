using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZOO_db
{
    class Shop
    {

        private Int32 _shopID;
        private Int32 _zoneID;
        private Int32 _managerID;
       


        public Int32 ProductID
        {
            get { return _shopID; }
            set { _shopID = value; }
        }


        public Int32 ZoneID
        {
            get { return _zoneID; }
            set
            {
                if (value == null)
                {
                    throw new Exception("Zone can not be empty try choose one ");
                    return;
                }
                _zoneID = value;
            }
        }

        public Int32 ManagerID
        {
            get { return _managerID; }
            set
            {
                if (value == null)
                {
                    throw new Exception("Manager ID can not be empty");
                    return;
                }
                _managerID = value;
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
            return "shop:"+_shopID + "   " + _managerID;
        }

        public Shop() : base()
        {
        }

        public Shop(Int32 shopID, Int32 managerID, Int32 zoneID) : base()
        {
            this.ShopID = shopID;
            this.ManagerID = managerID;
            this.ShopID = shopID;
        }


    }
}
