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
    public partial class VetListForm : Form
    {
        private SqlConnection cn = new SqlConnection("Data Source = tcp:mednat.ieeta.pt\\SQLSERVER,8101 ;" +
                                        "Initial Catalog = p8g8 ;" +
                                        "uid = p8g8 ;" +
                                        "Password = Tudomerda69. ;");
        public VetListForm()
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

        private void InsertVet(String fname, String lname, String specialty)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("zoodb.Insert_Vet", cn){CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@fname",fname);
            cmd.Parameters.AddWithValue("@lname", lname);
            cmd.Parameters.AddWithValue("@specialty", specialty);

            cmd.ExecuteNonQuery();
            cn.Close();

        }

        private void DeleteVet(String license_ID)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("zoodb.Delete_Vet", cn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@license_ID", license_ID);

            cmd.ExecuteNonQuery();
            cn.Close();

        }

        private void ListBoxLoad()
        {

            listBox1.Items.Clear();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from zoodb.getVetList()", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(reader[0].ToString() + ":" + reader[1].ToString() + " " + reader[2].ToString());
            }

            cn.Close();

        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var confirmResult = MessageBox.Show("Insert Veterinarian",
                                     "Confirm Insertion",
                                     MessageBoxButtons.OK);
            if (confirmResult == DialogResult.OK)
            {
                InsertVet(txtfname.Text, txtlname.Text, txtspecialty.Text);
                ListBoxLoad();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Delete Veterinarian",
                         "Confirm Deletion",
                         MessageBoxButtons.OK);
            if (confirmResult == DialogResult.OK)
            {
                DeleteVet(txtlicense2.Text);
                ListBoxLoad();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            string text = listBox1.GetItemText(listBox1.SelectedItem);
            var frm = new VetForm(text);
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
