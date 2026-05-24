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
    public partial class AddFuturaInfo : Form
    {

        public NpgsqlConnection con;
        int futuraId;
        public AddFuturaInfo(NpgsqlConnection con, int futuraId)
        {
            InitializeComponent();

            this.con = con;
            this.futuraId = futuraId;

            this.StartPosition = FormStartPosition.CenterScreen;

            LoadProducts();
        }

        public void LoadProducts()
        {
            string sql = @"
        SELECT 
            p.product_id,
            p.name
        FROM Products p";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            comboBoxProduct.DataSource = dt;
            comboBoxProduct.ValueMember = "product_id";
            comboBoxProduct.DisplayMember = "name";
        }

        private void AddFuturaInfo_Load(object sender, EventArgs e)
        {
            
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxProduct.SelectedValue == null)
                {
                    MessageBox.Show("Выберите товар");
                    return;
                }

                double quantity = Convert.ToDouble(textBoxQuantity.Text);
                int productId = Convert.ToInt32(comboBoxProduct.SelectedValue);

                NpgsqlCommand cmd = new NpgsqlCommand(
                    "INSERT INTO futura_info(futura_id, product_id, quantity, price_list_id) " +
                    "VALUES(:futura_id, :product_id, :quantity, :price_list_id)",
                    con);

                cmd.Parameters.AddWithValue("futura_id", futuraId);
                cmd.Parameters.AddWithValue("product_id", productId);
                cmd.Parameters.AddWithValue("quantity", quantity);

                // 🔥 ВАЖНО: price_list_id нужно получить из БД, а не из ComboBox
                NpgsqlCommand cmd2 = new NpgsqlCommand(
                    "SELECT price_list_id FROM Price_List WHERE product_id = :pid ORDER BY date_from DESC LIMIT 1",
                    con);

                cmd2.Parameters.AddWithValue("pid", productId);

                object priceListId = cmd2.ExecuteScalar();

                if (priceListId == null)
                {
                    MessageBox.Show("Нет цены для этого товара");
                    return;
                }

                cmd.Parameters.AddWithValue("price_list_id", priceListId);

                cmd.ExecuteNonQuery();

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
