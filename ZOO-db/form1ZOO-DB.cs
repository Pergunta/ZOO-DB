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

        private SqlConnection cn;
        private int currentEmployee;
        private bool add;
        private object txtFname;
        private object txtLname;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            cn = getSGBDConnection();
            LoadEmployeesToolStripMenuItem_Click(null, null);


        }

        private SqlConnection getSGBDConnection()
        {
            return new SqlConnection("data source= DESKTOP-SKUGM7Q\\SQLEXPRESS01;integrated security=true;initial catalog=Northwind");
        }

        private bool verifySGBDConnection()
        {
            if (cn == null)
                cn = getSGBDConnection();

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cn.State == ConnectionState.Open;
        }

        private void MenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > 0)
            {
                currentEmployee = listBox1.SelectedIndex;
                ShowEmployee();
            }
        }

        private void LoadEmployeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;


            SqlCommand cmd = new SqlCommand("SELECT * FROM zoodb.employee", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            listBox1.Items.Clear();
            while (reader.Read())
            {
                Employee M = new Employee();
                M.ID = reader["ID"].ToString();
                M.Fname = reader["fname"].ToString();
                M.Lname = reader["lname"].ToString();
                M.Birthdate = reader["birthday"].ToString();
                listBox1.Items.Add(M);
            }

            cn.Close();


            currentEmployee = 0;
            ShowEmployee();


        }


        public void ShowEmployee()
        {
            if (listBox1.Items.Count == 0 | currentEmployee < 0)
                return;
            Employee employee = new Employee();
            employee = (Employee)listBox1.Items[currentEmployee];
            txtID.Text = employee.ID;
            txtfname.Text = employee.Fname;
            txt1lname.Text = employee.Lname;
            txtbirthdate.Text = employee.Birthdate;

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }
    }
}
