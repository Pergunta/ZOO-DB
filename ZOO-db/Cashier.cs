using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZOO_db
{
    public class Cashier : Employee
    {
        private Int32 emp_ID;
        private Int32 _shopID;



        public int Emp_ID
        {
            get { return ID; }
            set { ID = value; }
        }

        public int ShopID
        {
            get { return _shopID; }
            set
            {
                if (value == null )
                {
                    throw new Exception("shop_ID cant be  empty");
                    return;
                }
                _shopID = value;
            }

        }

        public override String ToString()
        {
            return "Cashier "+this.Fname + " " + this.Lname + " "+ this.Birthdate+" "+ _shopID + " " + this.ID;
        }


        public Cashier(int ID , String Fname, String Lname, String Birthdate) : base(ID ,Fname, Lname, Birthdate)
        {
        }


        public Cashier(int empID ,String Fname, String Lname, String birthdate, int shopID) : base(Fname, Lname, birthdate)
        {
            this.Emp_ID = empID;
            this.ShopID = shopID;
            

        }
    }
}
