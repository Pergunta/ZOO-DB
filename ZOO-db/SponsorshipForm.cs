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
    public partial class SponsorshipForm : Form
    {
        private SqlConnection cn = new SqlConnection("Data Source = tcp:mednat.ieeta.pt\\SQLSERVER,8101 ;" +
                                        "Initial Catalog = p8g8 ;" +
                                        "uid = p8g8 ;" +
                                        "Password = Tudomerda69. ;");
        public SponsorshipForm()
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

        private void AddSponsorship(String NIF, String animal_ID)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("zoodb.addSponsorship", cn){CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@NIF", NIF);
            cmd.Parameters.AddWithValue("@animal_ID", animal_ID);

            cmd.ExecuteNonQuery();
            cn.Close();

        }

        private void RemoveSponsorship(String NIF, String animal_ID)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("zoodb.removeSponsorship", cn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@NIF", NIF);
            cmd.Parameters.AddWithValue("@animal_ID", animal_ID);

            cmd.ExecuteNonQuery();
            cn.Close();

        }

        private void ListBoxLoad()
        {

            listBox1.Items.Clear();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from zoodb.getSponsorshipList()", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(reader[0].ToString() + ":                " + reader[1].ToString()
                                   +";             "+ reader[2].ToString() + ":          " + reader[3].ToString() + " " + reader[4].ToString());
            }

            cn.Close();

        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var confirmResult = MessageBox.Show("ADD SPONSORSHIP",
                                     "Confirm",
                                     MessageBoxButtons.OK);
            if (confirmResult == DialogResult.OK)
            {
                AddSponsorship(addNIF.Text, addID.Text);
                ListBoxLoad();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("REMOVE SPONSORSHIP",
                         "Confirm",
                         MessageBoxButtons.OK);
            if (confirmResult == DialogResult.OK)
            {
                RemoveSponsorship(removeNIF.Text, removeID.Text);
                ListBoxLoad();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
