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
        private string zone_ID ="0";
        private string enc_number="0";
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

        private void EncListLoad(String zone_ID)
        {
            listBox1.Items.Clear();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from zoodb.getZoneEnc(@zone_ID)", cn);
            cmd.Parameters.AddWithValue("@zone_ID", zone_ID);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if (reader[1].ToString() == "")
                {
                    listBox1.Items.Add(reader[0].ToString() + ";    Empty;   " + reader[2].ToString()); 
                }
                else
                {
                    listBox1.Items.Add(reader[0].ToString() + ";    " + reader[1].ToString() + ";   " + reader[2].ToString());
                }
            }

            cn.Close();

        }

        private void AnimalListLoad(String zone_ID, String enc_number)
        {
            listBox2.Items.Clear();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from zoodb.getEncAnimals(@zone_ID, @enc_number)", cn);
            cmd.Parameters.AddWithValue("@zone_ID", zone_ID);
            cmd.Parameters.AddWithValue("@enc_number", enc_number);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listBox2.Items.Add(reader[0].ToString() + ";    " + reader[1].ToString());
            }

            cn.Close();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = comboBox1.GetItemText(comboBox1.SelectedItem);
            string[] split = item.Split(':');
            zone_ID = split[0];
            ManagerLoad(zone_ID);
            EncListLoad(zone_ID);

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string item = listBox1.GetItemText(listBox1.SelectedItem);
            string[] split = item.Split(';');
            enc_number = split[0];
            AnimalListLoad(zone_ID, enc_number);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var frm = new MoveSpecies(zone_ID);
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { EncListLoad(zone_ID); };
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var frm = new ChangeManager(zone_ID);
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { EncListLoad(zone_ID); };
            frm.Show();
        }
    }
}
