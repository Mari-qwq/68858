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
    public partial class FuturaForm : Form
    {

        public NpgsqlConnection con;

        DataTable dtFutura = new DataTable();
        DataTable dtInfo = new DataTable();
        DataSet ds = new DataSet();
        public FuturaForm(NpgsqlConnection con)
        {
            InitializeComponent();

            this.con = con;

            this.StartPosition = FormStartPosition.CenterScreen;

            LoadFutura();
        }

        // ЗАКАЗЫ (FUTURA)
        public void LoadFutura()
        {
            string sql = @"
        SELECT 
            f.futura_id,
            c.name AS client,
            f.order_date,
            f.totalsum
        FROM Futura f
        JOIN Clients c ON c.client_id = f.client_id
        ORDER BY f.futura_id";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridViewFutura.DataSource = dt;
        }

        // ТОВАРЫ В ЗАКАЗЕ
        public void LoadInfo(int futuraId)
        {
            string sql = @"
        SELECT
            fi.futura_info_id,
            p.name AS product,
            fi.quantity,
            pl.price,
            fi.quantity * pl.price AS sum
        FROM futura_info fi
        JOIN Products p ON p.product_id = fi.product_id
        JOIN Price_List pl ON pl.price_list_id = fi.price_list_id
        WHERE fi.futura_id = @id";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);
            da.SelectCommand.Parameters.AddWithValue("id", futuraId);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridViewInfo.DataSource = dt;
        }

        private void FuturaForm_Load(object sender, EventArgs e)
        {

        }

        private void AddOrderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddFutura f = new AddFutura(con, -1);
            f.ShowDialog();

            LoadFutura();
        }

        private void DeleteOrderToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridViewFutura.CurrentRow.Cells["futura_id"].Value;

            NpgsqlCommand cmd = new NpgsqlCommand(
                "DELETE FROM Futura WHERE futura_id = :id",
                con);

            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();

            LoadFutura();
        }

        private void UpdateOrderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadFutura();

        }

        private void AddProductТоварToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewFutura.CurrentRow == null) return;

            int futuraId =
                (int)dataGridViewFutura.CurrentRow.Cells["futura_id"].Value;

            AddFuturaInfo f = new AddFuturaInfo(con, futuraId);
            f.ShowDialog();

            LoadFutura();
            LoadInfo(futuraId);
        }

        private void DeleteProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridViewInfo.CurrentRow.Cells["futura_info_id"].Value;

            NpgsqlCommand cmd = new NpgsqlCommand(
                "DELETE FROM futura_info WHERE futura_info_id = :id",
                con);

            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();

            if (dataGridViewFutura.CurrentRow != null)
            {
                int futuraId =
                    (int)dataGridViewFutura.CurrentRow.Cells["futura_id"].Value;

                LoadInfo(futuraId);
                LoadFutura();
            }
        }

        private void UpdateProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewFutura.CurrentRow != null)
            {
                int id = Convert.ToInt32(
                    dataGridViewFutura.CurrentRow.Cells["futura_id"].Value);

                LoadInfo(id);
            }
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridViewFutura_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewInfo_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewFutura_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewFutura.CurrentRow != null)
            {
                if (e.RowIndex < 0) return;

                int id = Convert.ToInt32(
                    dataGridViewFutura.Rows[e.RowIndex].Cells["futura_id"].Value);

                LoadInfo(id);
            }
        }
    }
}
