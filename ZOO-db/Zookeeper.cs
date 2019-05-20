using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZOO_db
{
    public class Zookeeper : Employee 
    {
        private String emp_ID;
        private String specialty;



        public int em_ID
        {
            get { return em; }
            set { _ID = value; }
        }





        public Zookeeper(String Fname, String Lname , String birthdate , String speciality) : base(Fname ,Lname , birthdate )
        {
            this.emp_ID = ID;
            this.specialty = specialty;

        }
    }
}
