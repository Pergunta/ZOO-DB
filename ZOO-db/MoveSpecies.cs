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
    public partial class MoveSpecies : Form
    {
        private string zone_ID = "0";
        private string species_name = "";
        private SqlConnection cn = new SqlConnection(DBConnectionString.ConnectionString);

        public MoveSpecies(String ID)
        {
            zone_ID = ID;
            InitializeComponent();
            LoadComboBox1();
            LoadComboBox2();
        }

        private bool verifySGBDConnection()
        {

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cn.State == ConnectionState.Open;
        }

        public void LoadComboBox1() {
            comboBox1.Items.Clear();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from zoodb.getZoneEncInhabited(@zone_ID)", cn);
            cmd.Parameters.AddWithValue("@zone_ID", zone_ID);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader[0].ToString());
                species_name = reader.GetString(1);
            }

            cn.Close();
        }

        public void LoadComboBox2()
        {
            comboBox2.Items.Clear();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from zoodb.getZoneEncEmpty(@zone_ID)", cn);
            cmd.Parameters.AddWithValue("@zone_ID", zone_ID);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox2.Items.Add(reader[0].ToString());
            }

            cn.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from zoodb.getEncAnimals(@zone_ID, @enc_number)", cn);
            cmd.Parameters.AddWithValue("@zone_ID", zone_ID);
            cmd.Parameters.AddWithValue("@enc_number", comboBox1.GetItemText(comboBox1.SelectedItem));
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txtspecies.Text = reader.GetString(2);
            }

            cn.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!verifySGBDConnection())
                return;
            try
            {
                SqlCommand cmd = new SqlCommand("zoodb.moveSpecies", cn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@zone_ID", zone_ID);
                cmd.Parameters.AddWithValue("@enc1", comboBox1.GetItemText(comboBox1.SelectedItem));
                cmd.Parameters.AddWithValue("@enc2", comboBox2.GetItemText(comboBox2.SelectedItem));
                cmd.ExecuteNonQuery();
            }catch(Exception ex) {
                MessageBox.Show("Enclosure not big enough");
            }
            cn.Close();
            this.Close();
        }
    }
}
