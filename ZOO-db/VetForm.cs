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
        private SqlConnection cn = new SqlConnection("Data Source = tcp:mednat.ieeta.pt\\SQLSERVER,8101 ;" +
                                "Initial Catalog = p8g8 ;" +
                                "uid = p8g8 ;" +
                                "Password = Tudomerda69. ;");
        private String vet_ID = "";

        public VetForm(String vet_string)
        {
            InitializeComponent();
            getID(vet_string);
            getData();
            

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

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
