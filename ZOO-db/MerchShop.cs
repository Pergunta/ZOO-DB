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
    public partial class MerchShop : Form
    {
        private int currentShop;
        private int currentProduct;
        public MerchShop()
        {
            InitializeComponent();
            LoadToolStripMenuItem_Click(null, null);
        }
        private SqlConnection cn = new SqlConnection("Data Source = tcp:mednat.ieeta.pt\\SQLSERVER,8101 ;" +
                                      "Initial Catalog = p8g8 ;" +
                                      "uid = p8g8 ;" +
                                      "Password = Tudomerda69. ;");

        private bool verifySGBDConnection()
        {

            if (cn.State != ConnectionState.Open)
                cn.Open();

            return cn.State == ConnectionState.Open;
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                currentShop= listBox1.SelectedIndex;
                ShowMerchShop();
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.SelectedIndex >= 0)
            {
                currentProduct = listBox2.SelectedIndex;
                Products product1 = new Products();
                product1 = (Products)listBox2.Items[currentProduct];
                ShowProduct();
            }
        }

        public void ShowMerchShop()
        {
            if (listBox1.Items.Count == 0 | currentShop < 0)
                return;
            Shop shop1 = new Shop();
            shop1 = (Shop)listBox1.Items[currentShop];
            txtShopID.Text = shop1.ShopID.ToString();
            txtZone.Text = shop1.ZoneID.ToString();
            txtManager.Text = shop1.ManagerID.ToString();
            loadProductsFromstore(shop1.ShopID);
           


        }

        public void ShowProduct()
        {
            if (listBox2.Items.Count == 0 | currentProduct < 0 )
                return;
            Products product1 = new Products();
            product1 = (Products)listBox2.Items[currentProduct];
            
            txtPName.Text = product1.Name;
            txtPrice.Text = product1.Price.ToString();
            txtPID.Text = product1.ProductID.ToString();
            txtquantity.Text = product1.Quantity.ToString();
   

        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM zoodb.merchandise_shop", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            listBox1.Items.Clear();

            while (reader.Read())
            {
              
                Shop S = new Shop();
                
                S.ShopID = Int32.Parse(reader["shop_ID"].ToString());
                S.ZoneID = Int32.Parse(reader["zone_ID"].ToString());
                S.ManagerID = Int32.Parse(reader["manager_ID"].ToString());
                listBox1.Items.Add(S);
            }
            cn.Close();

            currentShop = 0;
            ShowMerchShop();           
  
        }



        private void loadProductsFromstore(int store_ID) {

            if (!verifySGBDConnection())
                return;

            SqlCommand cmd1 = new SqlCommand("SELECT * FROM zoodb.getShopIDProduct(@shop_ID)", cn);
            cmd1.Parameters.AddWithValue("@shop_ID", store_ID);
            SqlDataReader reader1 = cmd1.ExecuteReader();
            listBox2.Items.Clear();
            while (reader1.Read())
            {
               
                Products P = new Products();
            
                P.ProductID = Int32.Parse(reader1["product_ID"].ToString());
                P.Price = Int32.Parse(reader1["price"].ToString());
                P.Name = reader1["name"].ToString();
                P.Quantity = Int32.Parse(reader1["quantity"].ToString());
                listBox2.Items.Add(P);
            }
            cn.Close();
            currentProduct = 0;
            ShowProduct();

        }

        private void createProduct(Products P){
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("zoodb.ADDproduct") { CommandType = CommandType.StoredProcedure};

         
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@name", P.Name);
            cmd.Parameters.AddWithValue("@shop_ID", P.ShopID);
            cmd.Parameters.AddWithValue("@price", P.Price);
            cmd.Parameters.AddWithValue("@quantity", P.Quantity);
            cmd.Connection = cn;

            cmd.ExecuteNonQuery();
            
            cn.Close();
            
        }

        private void deleteProduct(int productID)
        {
            int rows = 0;
            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("zoodb.DeleteProduct") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@product_ID", productID);
            cmd.Connection = cn;
            try
            {
                rows = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to delete product , it was purchased\n ERROR MESSAGE: \n" + ex.Message);
                

            }
            finally
            {
                if(rows == 1)
                    MessageBox.Show("delete OK");
                else
                    MessageBox.Show("DELETE NOT OK");
                cn.Close();
            }

            
        }

        private void updateProduct(Products p)
        {

            if (!verifySGBDConnection())
                return;
            SqlCommand cmd = new SqlCommand("zoodb.UpdateProduct") { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@name", p.Name);
            cmd.Parameters.AddWithValue("@price", p.Price);
            cmd.Parameters.AddWithValue("@quantity", p.Quantity);
            cmd.Parameters.AddWithValue("@shop_ID", p.ShopID);
            cmd.Parameters.AddWithValue("@product_ID", p.ProductID);
            cmd.Connection = cn;
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        private void BackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Products p = new Products();
            p.Quantity = Int32.Parse(txtquantity.Text.ToString());
            p.Price = Int32.Parse(txtPrice.Text.ToString());
            p.Name = txtPName.Text.ToString();
            p.ShopID = Int32.Parse(txtShopID.Text.ToString());
            createProduct(p);
            string message = "Completed adding";
            string caption = "Item added";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;
            loadProductsFromstore(p.ShopID);
            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);
           

            
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            deleteProduct(Int32.Parse(txtPID.Text.ToString()));
            string message = "Item deleted";
            string caption = "added";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;
            loadProductsFromstore(Int32.Parse(txtShopID.Text.ToString()));
            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            Products p = new Products();
            p.ProductID = Int32.Parse(txtPID.Text.ToString());
            p.Quantity = Int32.Parse(txtquantity.Text.ToString());
            p.Price = Int32.Parse(txtPrice.Text.ToString());
            p.Name = txtPName.Text.ToString();
            p.ShopID = Int32.Parse(txtShopID.Text.ToString());
            updateProduct(p);


            string message = "Item updated";
            string caption = "update";
            MessageBoxButtons buttons = MessageBoxButtons.OK;
            DialogResult result;
            loadProductsFromstore(Int32.Parse(txtShopID.Text.ToString()));
            // Displays the MessageBox.
            result = MessageBox.Show(message, caption, buttons);
        }
        private void loadEmployeesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new InsertEmployee();
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }

        private void veterinarianToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new VetListForm();
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }

        private void MerchShopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new MerchShop();
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();

        }

        private void zoneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new ZoneForm();
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }

        private void sponsorshipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new SponsorshipForm();
            frm.Location = this.Location;
            frm.StartPosition = FormStartPosition.Manual;
            frm.FormClosing += delegate { this.Show(); };
            frm.Show();
            this.Hide();
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
