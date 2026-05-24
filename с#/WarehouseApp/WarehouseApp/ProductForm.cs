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
    public partial class ProductForm : Form
    {
        public NpgsqlConnection con;

        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        public ProductForm(NpgsqlConnection con)
        {
            this.con = con;

            InitializeComponent();

            LoadTable();
        }

        public void LoadTable()
        {
            string sql = "SELECT * FROM Products ORDER BY product_id";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);

            ds.Reset();
            da.Fill(ds);

            dt = ds.Tables[0];

            dataGridView1.DataSource = dt;

            dataGridView1.Columns[0].HeaderText = "Номер";
            dataGridView1.Columns[1].HeaderText = "Название";
            dataGridView1.Columns[2].HeaderText = "Ед. изм.";

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void ProductForm_Load(object sender, EventArgs e)
        {
            
        }

        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddProduct f = new AddProduct(con, -1);
            f.ShowDialog();

            LoadTable();
        }

        private void UpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells["product_id"].Value;
            string name = (string)dataGridView1.CurrentRow.Cells["name"].Value;
            string unit = (string)dataGridView1.CurrentRow.Cells["unit"].Value;

            AddProduct f = new AddProduct(con, id, name, unit);

            f.ShowDialog();

            LoadTable();
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells["product_id"].Value;

            NpgsqlCommand cmd = new NpgsqlCommand(
                "DELETE FROM Products WHERE product_id = :id",
                con);

            cmd.Parameters.AddWithValue("id", id);

            cmd.ExecuteNonQuery();

            LoadTable();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
