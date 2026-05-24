using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace WarehouseApp
{
    public partial class AddPriceForm : Form
    {
        public NpgsqlConnection con;
        int id;

        public AddPriceForm(NpgsqlConnection con, int id)
        {
            InitializeComponent();

            this.con = con;
            this.id = id;

            LoadProducts();
        }


        public AddPriceForm(NpgsqlConnection con, int id, int productId, double price)
        {
            InitializeComponent();

            this.con = con;
            this.id = id;

            LoadProducts();

            textBoxPrice.Text = price.ToString();

            comboBoxProduct.SelectedValue = productId; 
        }

        public void LoadProducts()
        {
            string sql = "SELECT * FROM Products";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            comboBoxProduct.DataSource = dt;
            comboBoxProduct.ValueMember = "product_id";
            comboBoxProduct.DisplayMember = "name";
        }

        private void button_Yes_Click(object sender, EventArgs e)
        {
            try
            {
                double price = Convert.ToDouble(textBoxPrice.Text);
                int productId = Convert.ToInt32(comboBoxProduct.SelectedValue);
                DateTime date = dateTimePicker1.Value.Date;
                if (id == -1)
                {
                    // ➕ ДОБАВЛЕНИЕ
                    NpgsqlCommand cmd = new NpgsqlCommand(
                        "INSERT INTO Price_List(product_id, price, date_from) " +
                        "VALUES(:product_id, :price, :date_from)",
                        con);

                    cmd.Parameters.AddWithValue("product_id", productId);
                    cmd.Parameters.AddWithValue("price", price);
                    cmd.Parameters.AddWithValue("date_from", date);

                    cmd.ExecuteNonQuery();
                }
                else
                {
                   
                    NpgsqlCommand cmd = new NpgsqlCommand(
                        "UPDATE Price_List SET product_id=:product_id, price=:price, date_from=:date_from " +
                "WHERE price_list_id=:id", con);

                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("product_id", productId);
                    cmd.Parameters.AddWithValue("price", price);
                    cmd.Parameters.AddWithValue("date_from", date);

                    cmd.ExecuteNonQuery();
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_No_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
