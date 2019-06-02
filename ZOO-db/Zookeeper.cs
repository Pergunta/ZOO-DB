using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZOO_db
{
    public class Zookeeper : Employee 
    {
        private Int32 emp_ID;
        private String _speciality;
        private Int32 _zone;


        public int em_ID
        {
            get { return ID; }
            set { ID = value; }
        }

        public String Speciality
        {
            get { return _speciality;}
            set
            {
                if (value == null | String.IsNullOrEmpty(value))
                {
                    throw new Exception("Speciality can not be empty");
                    return;
                }
                _speciality = value;
            }

        }

        public int Zone {
            get { return _zone; }
            set
            {
                if (value == null )
                {
                    throw new Exception("Zone can not be empty");
                    return;
                }
                _zone = value;

            }
        }

        public Zookeeper(String Fname, String Lname, String Birthdate) : base(Fname, Lname, Birthdate)
        {
        }


        public Zookeeper(String Fname, String Lname , String birthdate , String speciality,int zone) : base(Fname ,Lname , birthdate )
        {
            this.emp_ID = ID;
            this._speciality = speciality;

        }
    }
}
