using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ZOO_db
{
    public partial class EmployeeForm : Form
    {
        private SqlConnection cn = new SqlConnection("Data Source = tcp:mednat.ieeta.pt\\SQLSERVER,8101 ;" +
                                "Initial Catalog = p8g8 ;" +
                                "uid = p8g8 ;" +
                                "Password = Tudomerda69. ;");

        public EmployeeForm()
        {
            InitializeComponent();
            ListBoxLoad();

        }

        private bool verifySGBDConnection()
        {

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cn.State == ConnectionState.Open;
        }

        private void ListBoxLoad() {
            listBox1.Items.Clear();
            ListBoxLoadZK();
            ListBoxLoadCashier();
        }

        private void ListBoxLoadZK()
        {
            
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from zoodb.getEmployeeZK()", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(reader[0].ToString() + ":" + reader[1].ToString() + " " + reader[2].ToString());
            }

            cn.Close();

        }

        private void ListBoxLoadCashier()
        {
            
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from zoodb.getEmployeeCashier()", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(reader[0].ToString() + ":        " + reader[1].ToString() + " " + reader[2].ToString());
            }

            cn.Close();

        }
    }
}
