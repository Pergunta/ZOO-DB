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
          
          
        
            cmd.ExecuteNonQuery();
            cn.Close();

        }

        private void InsertZookeerperCmd(Zookeeper Z)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("insert into zoodb.zookeeper (emp_ID,speciality,birthdate ) VALUES (@emp_ID, @speciality, @birthdate);", cn);
            cmd.Parameters.AddWithValue("@emp_ID",currentEmployee );
            cmd.Parameters.AddWithValue("@speciality", Z.Speciality);
            cmd.Parameters.AddWithValue("@zone", Z.Zone);


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
                String[] date = txtbirthdate.Text.Split('-');
                employee1.Birthdate = new DateTime(Int32.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
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
                string[] date = txtbirthdate.Text.Split('-');
                DateTime tt = new DateTime(Int32.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
                employee1 = new Employee(txtfname.Text, txtlname.Text,tt );

            } catch (Exception ex)
            {
                MessageBox.Show("Select Employee from left list");
            }
           
            Zookeeper zookeeper1 = new Zookeeper(employee1.Fname, employee1.Lname, employee1.Birthdate);
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
            if (listBox1.SelectedIndex > 0)
            {
                currentEmployee = listBox1.SelectedIndex;
                ShowEmployee();
            }

        }


        public void ShowEmployee()
        {
            if (listBox1.Items.Count == 0 | currentEmployee < 0)
                return;
            Employee employee1 = new Employee();
            employee1 = (Employee)listBox1.Items[currentEmployee];
            txtfname.Text = employee1.Fname;
            txtlname.Text = employee1.Lname;
            txtbirthdate.Text = employee1.Birthdate.ToString("yyyy-mm-ddd");


        }

      
        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM zoodb.employee", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            listBox1.Items.Clear();

            while (reader.Read())
            {
                Employee E = new Employee();
                E.Fname = reader["Fname"].ToString();
                E.Lname = reader["Lname"].ToString();
                string[] date = reader["Birthdate"].ToString().Split('-');
                DateTime tt = new DateTime(Int32.Parse(date[0]), int.Parse(date[1]), int.Parse(date[2]));
                E.Birthdate = tt;
                listBox1.Items.Add(E);
            }

            cn.Close();


            currentEmployee = 0;
            ShowEmployee();
        }
    }
}
