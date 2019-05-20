using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace ZOO_db
{

    [Serializable()]
    public class Employee
    {
        private Int32 _ID;
        private String _fname;
        private String _lname;
        private String _birthdate;


        public Int32 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }


        public String Fname
        {
            get { return _fname; }
            set
            {
                if (value == null | String.IsNullOrEmpty(value))
                {
                    throw new Exception("First Name can’t be empty");
                    return;
                }
                _fname = value;
            }
        }

        public String Lname
        {
            get { return _lname; }
            set
            {
                if (value == null | String.IsNullOrEmpty(value))
                {
                    throw new Exception("Last Name can’t be empty");
                    return;
                }
                _lname = value;
            }
        }

        public String Birthdate
        {

            get { return _birthdate; }
            set
            {
                if (value == null || Convert.ToInt32(value.Substring(0, 4)) < 0 || Convert.ToInt32(value.Substring(0, 4)) > Convert.ToInt32(DateTime.Now.Year.ToString()) || value.Substring(4) != "-" || Convert.ToInt32(value.Substring(5, 7)) > 12 || Convert.ToInt32(value.Substring(5, 7)) < 0 || value.Substring(7) != "-" || Convert.ToInt32(value.Substring(8, 10)) > 31 || Convert.ToInt32(value.Substring(8, 10)) < 0)
                {
                    throw new Exception("Format for date YYYY-MM-DD ");
                    return;
                }
                _birthdate = "'" + value + "'";
            }
        }


        public override String ToString()
        {
            return _fname + "   " + _lname;
        }

        public Employee() : base()
        {
        }

        public Employee(String Fname, String Lname, String birthdate) : base()
        {
            this.Fname = Fname;
            this.Lname = Lname;
            this.Birthdate = birthdate;
        }


    }
}

