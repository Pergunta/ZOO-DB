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
    public partial class ZoneForm : Form
    {

        private SqlConnection cn = new SqlConnection("Data Source = tcp:mednat.ieeta.pt\\SQLSERVER,8101 ;" +
                        "Initial Catalog = p8g8 ;" +
                        "uid = p8g8 ;" +
                        "Password = Tudomerda69. ;");

        public ZoneForm()
        {
            InitializeComponent();
            ComboBoxLoad();
        }

        private bool verifySGBDConnection()
        {

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cn.State == ConnectionState.Open;
        }

        private void ComboBoxLoad()
        {

            comboBox1.Items.Clear();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from zoodb.getZoneList()", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                comboBox1.Items.Add(reader[0].ToString() + ":" + reader[1].ToString());
            }

            cn.Close();

        }

        private void ManagerLoad(String zone_ID)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from zoodb.getZoneManager(@zone_ID)", cn);
            cmd.Parameters.AddWithValue("@zone_ID", zone_ID);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            this.txtname.Text = reader.GetString(0) + " " + reader.GetString(1);
            this.txtid.Text = reader[2].ToString();
            cn.Close();
        }

        private void listViewLoad(String zone_ID)
        {

            listView1.Items.Clear();
            listView2.Items.Clear();
            listView3.Items.Clear();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from zoodb.getZoneEnc(@zone_ID)", cn);
            cmd.Parameters.AddWithValue("@zone_ID", zone_ID);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listView1.Items.Add(reader[0].ToString());
                listView2.Items.Add(reader[1].ToString());
                listView3.Items.Add(reader[2].ToString());
            }

            cn.Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = comboBox1.GetItemText(comboBox1.SelectedItem);
            string[] split = item.Split(':');
            ManagerLoad(split[0]);
            listViewLoad(split[0]);

        }
    }
}
