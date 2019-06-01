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
    public partial class InsertEmployee : Form
    {
        private SqlConnection cn = new SqlConnection("Data Source = tcp:mednat.ieeta.pt\\SQLSERVER,8101 ;" +
                                        "Initial Catalog = p8g8 ;" +
                                        "uid = p8g8 ;" +
                                        "Password = Tudomerda69. ;");
        public InsertEmployee()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cn.Open();

        }

        private bool verifySGBDConnection()
        {

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cn.State == ConnectionState.Open;
        }

        private void InsertEmployeeCmd(Employee E)
        {
            if (!verifySGBDConnection())
                return;
           
            SqlCommand cmd = new SqlCommand("insert into zoodb.employee (fname, lname, birthdate) VALUES (@fname, @lname, @birthdate);", cn);
            cmd.Parameters.AddWithValue("@fname",E.Fname);
            cmd.Parameters.AddWithValue("@lname", E.Lname);
            cmd.Parameters.AddWithValue("@birthdate", E.Birthdate);

        
            cmd.ExecuteNonQuery();
            cn.Close();

        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new Form1();
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Employee employee1 = new Employee();
            try
            {
                employee1.Fname = txtfname.Text;
                employee1.Lname = txtlname.Text;
                employee1.Birthdate = txtbirthdate.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
           
            }
            InsertEmployeeCmd(employee1);
        }

       
    }
}
