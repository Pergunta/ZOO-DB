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
        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (MerchShops.SelectedIndex > 0)
            {
                currentShop= MerchShops.SelectedIndex;
                ShowMerchShop();
            }
        }

        public void ShowMerchShop()
        {
            if (MerchShops.Items.Count == 0 | currentShop < 0)
                return;
            Shop shop1 = new Shop();
            shop1 = (Shop)MerchShops.Items[currentShop];
            txtShopID.Text = shop1.ShopID.ToString();
            txtZone.Text = shop1.ZoneID.ToString();
            txtManager.Text = shop1.ManagerID.ToString();


        }

        public void ShowProduct()
        {
            if (ProductsList.Items.Count == 0 | currentProduct < 0 )
                return;
            Products product1 = new Products();
            product1 = (Products)ProductsList.Items[currentProduct];
            txtPName.Text = product1.Name;
            txtPrice.Text = product1.Price.ToString();
            txtPID.Text = product1.ProductID.ToString();


        }

        private void LoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!verifySGBDConnection())
                return;

            SqlCommand cmd = new SqlCommand("SELECT * FROM zoodb.merchandise_shop", cn);
            SqlDataReader reader = cmd.ExecuteReader();
            MerchShops.Items.Clear();

            while (reader.Read())
            {
              
                Shop S = new Shop();
                S.ShopID = Int32.Parse(reader["shop_ID"].ToString());
                S.ZoneID = Int32.Parse(reader["zone_ID"].ToString());
                S.ManagerID = Int32.Parse(reader["manager_ID"].ToString());
                MerchShops.Items.Add(S);
            }
            currentShop = 0;
            ShowMerchShop();
            cn.Close();


            if (!verifySGBDConnection())
                return;

            SqlCommand cmd1 = new SqlCommand("SELECT * FROM zoodb.product", cn);
            SqlDataReader reader1 = cmd1.ExecuteReader();
            ProductsList.Items.Clear();
            while (reader1.Read())
            {
                Shop shop1= new Shop();
                shop1 = (Shop)MerchShops.Items[currentShop];
                Products P = new Products();
                P.ShopID = Int32.Parse(reader1["shop_ID"].ToString());
                P.ProductID = Int32.Parse(reader1["product_ID"].ToString());
                P.Price = Int32.Parse(reader1["price"].ToString());
                P.Name = reader1["name"].ToString();
                if(P.ShopID == shop1.ShopID)
                    ProductsList.Items.Add(P);
            }

            currentProduct = 0;
            ShowProduct();
            cn.Close();
        }
    }
}
