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
    public partial class ChangeManager : Form
    {
        private string zone_ID = "0";
        private SqlConnection cn = new SqlConnection(DBConnectionString.ConnectionString);

        public ChangeManager(String ID)
        {
            zone_ID = ID;
            InitializeComponent();
            LoadManager();
            LoadZooKeepers();
        }

        private bool verifySGBDConnection()
        {

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cn.State == ConnectionState.Open;
        }

        private void LoadManager() {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from zoodb.getZoneManager(@zone_ID)", cn);
            cmd.Parameters.AddWithValue("@zone_ID", zone_ID);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                txtname.Text = reader.GetString(0) + " " + reader.GetString(1);
                txtid.Text = reader[2].ToString();
            }

            cn.Close();
        }

        private void LoadZooKeepers() {

            listBox1.Items.Clear();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from zoodb.getZoneZK(@zone_ID)", cn);
            cmd.Parameters.AddWithValue("@zone_ID", zone_ID);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(reader[2].ToString() + ":" + reader[0].ToString() + " " + reader[1].ToString());
            }

            cn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selected = listBox1.GetItemText(listBox1.SelectedItem);
            string[] emp = selected.Split(':');

            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("zoodb.changeZoneManager", cn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@zone_ID", zone_ID);
            cmd.Parameters.AddWithValue("@emp_ID", emp[0]);
            cmd.ExecuteNonQuery();

            cn.Close();
            LoadManager();
        }
    }
}
