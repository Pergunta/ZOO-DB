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
    public partial class VetForm : Form
    {
        private SqlConnection cn = new SqlConnection(DBConnectionString.ConnectionString);
        private String vet_ID = "";

        public VetForm(String vet_string)
        {
            InitializeComponent();
            getID(vet_string);
            getData();
            listViewLoad();

        }

        private bool verifySGBDConnection()
        {

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cn.State == ConnectionState.Open;
        }

        private void getID(String line)
        {
            string[] split = line.Split(':');
            vet_ID = split[0];
            this.txtlicense.Text = vet_ID;
        }

        private void getData()
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from zoodb.getVet(@license_ID)", cn);
            cmd.Parameters.AddWithValue("@license_ID", vet_ID);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            this.txtname.Text = reader.GetString(0) + " " + reader.GetString(1);
            this.txtspecialty.Text = reader.GetString(2);

            cn.Close();
        }

        private void listViewLoad()
        {

            listView1.Items.Clear();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from zoodb.getVetHC(@license_ID)", cn);
            cmd.Parameters.AddWithValue("@license_ID", vet_ID);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listView1.Items.Add(reader[0].ToString());
                listView1.Items.Add(reader[1].ToString());
                listView1.Items.Add(reader[2].ToString());
                listView1.Items.Add(reader[3].ToString());
            }

            cn.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
