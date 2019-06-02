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
    public partial class AnimalForm : Form
    {
        private SqlConnection cn = new SqlConnection("Data Source = tcp:mednat.ieeta.pt\\SQLSERVER,8101 ;" +
                                "Initial Catalog = p8g8 ;" +
                                "uid = p8g8 ;" +
                                "Password = Tudomerda69. ;");
        private String animal_ID = "";

        public AnimalForm(String _animal_ID)
        {
            animal_ID = _animal_ID;
            InitializeComponent();
            getData();
            listViewLoad();

        }

        private bool verifySGBDConnection()
        {

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cn.State == ConnectionState.Open;
        }

        private void getData()
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from zoodb.getAnimal(@animal_ID)", cn);
            cmd.Parameters.AddWithValue("@animal_ID", animal_ID);
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            this.txtname.Text = reader.GetString(0);
            this.txtid.Text = reader[1].ToString();
            this.txtspecies.Text = reader.GetString(2);

            cn.Close();
        }

        private void listViewLoad()
        {

            listBox1.Items.Clear();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from zoodb.getAnimalHC(@animal_ID)", cn);
            cmd.Parameters.AddWithValue("@animal_ID", animal_ID);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(reader[0].ToString() + ";    " + reader[1].ToString() + ";   " + reader[2].ToString() + " " + reader[3].ToString() + ";   (" + reader[4].ToString()+")");
            }

            cn.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
