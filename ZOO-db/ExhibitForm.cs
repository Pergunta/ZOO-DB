﻿using System;
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
    public partial class ExhibitForm : Form
    {
        private SqlConnection cn = new SqlConnection("Data Source = tcp:mednat.ieeta.pt\\SQLSERVER,8101 ;" +
                                        "Initial Catalog = p8g8 ;" +
                                        "uid = p8g8 ;" +
                                        "Password = Tudomerda69. ;");
        public ExhibitForm()
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

        private void AddExhibit(String name, String zone_ID)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("zoodb.addExhibit", cn){CommandType = CommandType.StoredProcedure};
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@zone_ID", zone_ID);

            cmd.ExecuteNonQuery();
            cn.Close();

        }

        private void RemoveExhibit(String exhibit_ID)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("zoodb.removeExhibit", cn) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.AddWithValue("@exhibit_ID", exhibit_ID);

            cmd.ExecuteNonQuery();
            cn.Close();

        }

        private void ListBoxLoad()
        {

            listBox1.Items.Clear();
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("select * from zoodb.getExhibitList()", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                listBox1.Items.Add(reader[0].ToString() + ": " + reader[1].ToString() +" "+ reader[2].ToString() + " " + reader[3].ToString());
            }

            cn.Close();

        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var confirmResult = MessageBox.Show("ADD EXHIBIT",
                                     "Confirm",
                                     MessageBoxButtons.OK);
            if (confirmResult == DialogResult.OK)
            {
                AddExhibit(addname.Text, addID.Text);
                ListBoxLoad();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string item = listBox1.GetItemText(listBox1.SelectedItem);
            string[] split = item.Split(':');
            var confirmResult = MessageBox.Show("REMOVE EXHIBIT",
                         "Confirm",
                         MessageBoxButtons.OK);
            if (confirmResult == DialogResult.OK)
            {
                RemoveExhibit(split[0]);
                ListBoxLoad();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
