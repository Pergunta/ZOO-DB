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
            ListBoxLoadEmployee();
        }

        private void ListBoxLoadZK()
        {
            
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from zoodb.getEmployeeZK()", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(reader[0].ToString() + " : " + reader[1].ToString() + " " + reader[2].ToString() + " : Zookeeper");
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
                listBox1.Items.Add(reader[0].ToString() + " : " + reader[1].ToString() + " " + reader[2].ToString() + " : Cashier");
            }

            cn.Close();

        }

        private void ListBoxLoadEmployee()
        {

            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from zoodb.getEmployeeUnref()", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(reader[0].ToString() + " : " + reader[1].ToString() + " " + reader[2].ToString() + " : Employee");
            }

            cn.Close();

        }

        private void LoadZookeeper(string emp_ID)
        {
            txtShopID.Clear();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from zoodb.getZKData(@emp_ID)", cn);
            cmd.Parameters.AddWithValue("@emp_ID", emp_ID);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txtID.Text = reader[0].ToString();
                txtfname.Text = reader[1].ToString();
                txtlname.Text = reader[2].ToString();
                txtbirthdate.Text = reader[3].ToString();
                txtZone.Text = reader[4].ToString();
                txtSpeciality.Text = reader[5].ToString();
            }

            cn.Close();

        }


        private void LoadEmployee(string emp_ID)
        {
            txtShopID.Clear();
            txtZone.Clear();
            txtSpeciality.Clear();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from zoodb.getUnrefData(@emp_ID)", cn);
            cmd.Parameters.AddWithValue("@emp_ID", emp_ID);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txtID.Text = reader[0].ToString();
                txtfname.Text = reader[1].ToString();
                txtlname.Text = reader[2].ToString();
                txtbirthdate.Text = reader[3].ToString();
            }

            cn.Close();

        }

        private void LoadCashier(string emp_ID)
        {
            txtZone.Clear();
            txtSpeciality.Clear();

            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from zoodb.getCashierData(@emp_ID)", cn);
            cmd.Parameters.AddWithValue("@emp_ID", emp_ID);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txtID.Text = reader[0].ToString();
                txtfname.Text = reader[1].ToString();
                txtlname.Text = reader[2].ToString();
                txtbirthdate.Text = reader[3].ToString();
                txtShopID.Text = reader[4].ToString();
            }

            cn.Close();

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = listBox1.GetItemText(listBox1.SelectedItem);
            string[] split = Array.ConvertAll(item.Split(':'), p => p.Trim());

            if (split[2] == "Zookeeper")
            {
                LoadZookeeper(split[0]);
            }

            if (split[2] == "Cashier")
            {
                LoadCashier(split[0]);
            }

            if (split[2] == "Employee")
            {
                LoadEmployee(split[0]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Employee employee1 = new Employee();
            try
            {

                employee1 = new Employee(Int32.Parse(txtID.Text.ToString()), txtfname.Text, txtlname.Text, txtbirthdate.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Select Employee from left list");
            }

            Zookeeper zookeeper1 = new Zookeeper(employee1.ID, employee1.Fname, employee1.Lname, employee1.Birthdate);
            try
            {
                zookeeper1.Speciality = txtSpeciality.Text;
                zookeeper1.Zone = Int32.Parse(txtZone.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            InsertZookeerperCmd(zookeeper1);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            Employee employee1 = new Employee();
            try
            {

                employee1 = new Employee(Int32.Parse(txtID.Text.ToString()), txtfname.Text, txtlname.Text, txtbirthdate.Text);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Select Employee from left list");
            }

            Cashier cashier1 = new Cashier(employee1.ID, employee1.Fname, employee1.Lname, employee1.Birthdate);
            try
            {
                cashier1.ShopID = Int32.Parse(txtShopID.Text.ToString());


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            InsertCashierCmd(cashier1);

        }

        private void InsertCashierCmd(Cashier Z)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("insert into zoodb.cashier (emp_ID, shop_ID) VALUES (@emp_ID, @shop_ID)", cn);
            cmd.Parameters.AddWithValue("@emp_ID", Int32.Parse(txtID.Text.ToString()));
            cmd.Parameters.AddWithValue("@shop_ID", txtShopID.Text.ToString());


            cmd.ExecuteNonQuery();


            cn.Close();
            string message = "cashier added";
            string caption = "added";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;
            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);

        }

        private void InsertEmployeeCmd(Employee E)
        {
            if (!verifySGBDConnection())
                return;


            SqlCommand cmd = new SqlCommand("insert into zoodb.employee (fname, lname, birthdate) VALUES (@fname, @lname, @birthdate);", cn);
            cmd.Parameters.AddWithValue("@fname", E.Fname);
            cmd.Parameters.AddWithValue("@lname", E.Lname);
            cmd.Parameters.AddWithValue("@birthdate", E.Birthdate);
            string message = "Emplyee added";
            string caption = "added";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;
            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);


            cmd.ExecuteNonQuery();
            cn.Close();

        }

        private void InsertZookeerperCmd(Zookeeper Z)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("insert into zoodb.zookeeper (emp_ID, specialty, zone) VALUES (@emp_ID, @specialty, @zone)", cn);
            cmd.Parameters.AddWithValue("@emp_ID", Int32.Parse(txtID.Text.ToString()));
            cmd.Parameters.AddWithValue("@specialty", txtSpeciality.Text.ToString());
            cmd.Parameters.AddWithValue("@zone", Int32.Parse(txtZone.Text.ToString()));


            cmd.ExecuteNonQuery();


            cn.Close();
            string message = "Zookeeper added";
            string caption = "added";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;
            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);

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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
