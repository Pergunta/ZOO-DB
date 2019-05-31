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
    public partial class Form1 : Form
    {

       
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
    

        }

        private SqlConnection getSGBDConnection()
        {
            return new SqlConnection("data source= DESKTOP-SKUGM7Q\\SQLEXPRESS01;integrated security=true;initial catalog=Northwind");
        }

       


        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void loadEmployeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new InsertEmployee();
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }
    }
}
