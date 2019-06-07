using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace ZOO_db
{
    
    static class DBConnectionString
    {

        private static String _connection = "Data Source = tcp:mednat.ieeta.pt\\SQLSERVER,8101 ;" +
                                "Initial Catalog = p8g8 ;" +
                                "uid = ;" +
                                "Password = ;";

        public static String ConnectionString
        {
            get { return _connection; }

        }
    }
}

