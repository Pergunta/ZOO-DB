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

        private int currentEmployee;
        private SqlConnection cn = new SqlConnection("Data Source = tcp:mednat.ieeta.pt\\SQLSERVER,8101 ;" +
                                        "Initial Catalog = p8g8 ;" +
                                        "uid = p8g8 ;" +
                                        "Password = Tudomerda69. ;");
        public InsertEmployee()
        {
            InitializeComponent();
            LoadToolStripMenuItem_Click(null, null);
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
             cmd.Parameters.AddWithValue("@fname", E.Fname);
             cmd.Parameters.AddWithValue("@lname", E.Lname);
             cmd.Parameters.AddWithValue("@birthdate", E.Birthdate);
            string message = "Emplyee added";
            string caption = "added";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;
            LoadToolStripMenuItem_Click(null, null);
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
            cmd.Parameters.AddWithValue("@emp_ID",Int32.Parse(txtID.Text.ToString()));
            cmd.Parameters.AddWithValue("@specialty",txtSpeciality.Text.ToString());
            cmd.Parameters.AddWithValue("@zone", Int32.Parse(txtZone.Text.ToString()));


            cmd.ExecuteNonQuery();


            cn.Close();
            string message = "Zookeeper added";
            string caption = "added";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;
            LoadToolStripMenuItem_Click(null, null);
            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);

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

        private void Button2_Click(object sender, EventArgs e)
        {
            Employee employee1 = new Employee();
            try
            {
             
                employee1 = new Employee(Int32.Parse(txtID.Text.ToString()),txtfname.Text, txtlname.Text,txtbirthdate.Text );

            } catch (Exception ex)
            {
                MessageBox.Show("Select Employee from left list");
            }
           
            Zookeeper zookeeper1 = new Zookeeper(employee1.ID ,employee1.Fname, employee1.Lname, employee1.Birthdate);
            try {
                zookeeper1.Speciality = txtSpeciality.Text;
                zookeeper1.Zone = Int32.Parse(txtZone.Text);

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            InsertZookeerperCmd(zookeeper1);

        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                currentEmployee = listBox1.SelectedIndex;
                ShowEmployee();
            }

        }


        public void ShowEmployee()
        {
            if (listBox1.Items.Count == 0 | currentEmployee < 0)
                return;
            string item = listBox1.GetItemText(listBox1.SelectedItem);
            string[] split = item.Split(' ');

            if (split[0].Equals("Cashier"))
            {
                txtfname.Text = split[1];
                txtlname.Text = split[2];
                txtbirthdate.Text = split[3];
                txtID.Text = split[5];
                txtShopID.Text = split[4];
                txtZone.Text = null;
                txtSpeciality.Text = null;

            }
            else if (split[0].Equals("Zookeeper"))
            {
                txtfname.Text = split[1];
                txtlname.Text = split[2];
                txtbirthdate.Text = split[3];
                txtID.Text = split[6];
                txtSpeciality.Text = split[4];
                txtZone.Text = split[5];
                txtShopID.Text = null;
            }
            else if (split[0].Equals("Employee"))
            {
                txtfname.Text = split[1];
                txtlname.Text = split[2];
                txtbirthdate.Text = split[3];
                txtID.Text = split[4];
                txtZone.Text = null;
                txtSpeciality.Text = null;
                txtShopID.Text = null;

            }

                
         
            


        }


        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<int> idlist = new List<int>();

            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM zoodb.employee", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            listBox1.Items.Clear();
            List<Employee> employeelist = new List<Employee>();

            while (reader.Read())
            {
                Employee E = new Employee();
                E.ID = Int32.Parse((reader["ID"].ToString()));
                E.Fname = reader["Fname"].ToString();
                E.Lname = reader["Lname"].ToString();
                E.Birthdate = reader["Birthdate"].ToString();

                employeelist.Add(E);
            }


            cn.Close();
            employeelist.ForEach(el => LoadEmployees(el.ID, idlist));

            foreach (var el in idlist)
            {
                for (int i = 0; i < employeelist.Count; i++)
                {
                    if ((employeelist[i].ID) == el)
                        employeelist.Remove(employeelist[i]);

                }
                    
            }

            employeelist.ForEach(el => listBox1.Items.Add(el));
         
        }

        private void LoadEmployees(Int32 emp , List<int> list)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd1 = new SqlCommand("SELECT * FROM zoodb.getEmployeeJob(@ID)", cn);
            cmd1.Parameters.AddWithValue("@ID", emp);
            SqlDataReader reader1 = cmd1.ExecuteReader();

            while (reader1.Read())
            {
                list.Add(Int32.Parse(reader1["emp_ID"].ToString()));
                Employee E = new Employee();
                E.ID = emp;
                E.Fname = reader1["Fname"].ToString();
                E.Lname = reader1["Lname"].ToString();
                E.Birthdate = reader1["Birthdate"].ToString();
                Zookeeper Z = new Zookeeper(E.ID ,E.Fname,E.Lname,E.Birthdate);
                Z.Speciality = reader1["specialty"].ToString();
                Z.Zone = Int32.Parse(reader1["zone"].ToString());
                listBox1.Items.Add(Z);
            }
            cn.Close();


            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("SELECT * FROM zoodb.getEmployeeJob2(@ID)", cn);
            cmd.Parameters.AddWithValue("@ID", emp);
            SqlDataReader reader = cmd.ExecuteReader();
            List<Employee> employeelist = new List<Employee>();

            while (reader.Read())
            {
                list.Add(Int32.Parse(reader["emp_ID"].ToString()));
                Employee E = new Employee();
                E.ID = emp;
                E.Fname = reader["Fname"].ToString();
                E.Lname = reader["Lname"].ToString();
                E.Birthdate = reader["Birthdate"].ToString();
                Cashier C= new Cashier(E.ID,E.Fname, E.Lname, E.Birthdate);
                C.ShopID = Int32.Parse(reader["shop_ID"].ToString());
             
                listBox1.Items.Add(C);
              
            }
            cn.Close();

            currentEmployee = 0;
            ShowEmployee();

        }

        private void Button3_Click(object sender, EventArgs e)
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

            Cashier cashier1 = new Cashier(employee1.ID ,employee1.Fname, employee1.Lname, employee1.Birthdate);
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
            LoadToolStripMenuItem_Click(null, null);
            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);

        }

    }
}
