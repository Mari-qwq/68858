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
//using Excel = Microsoft.Office.Interop.Excel;

namespace WarehouseApp
{
    public partial class PriceListForm : Form
    {
        public NpgsqlConnection con;

        DataTable dt = new DataTable();

        public PriceListForm(NpgsqlConnection con)
        {
            InitializeComponent();

            this.con = con;

            this.StartPosition = FormStartPosition.CenterScreen;

            LoadPriceList();
        }

        public void LoadPriceList()
        {
            string sql = @"
                 SELECT 
                    pl.price_list_id,
                    pl.product_id,
                    p.name AS product,
                    pl.price,
                    pl.date_from
                FROM Price_List pl
                JOIN Products p ON p.product_id = pl.product_id
                ORDER BY pl.price_list_id";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);

            dt.Clear();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddPriceForm f = new AddPriceForm(con, -1);
            f.ShowDialog();

            LoadPriceList();
        }

        private void PriceListForm_Load(object sender, EventArgs e)
        {

        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["price_list_id"].Value);

            NpgsqlCommand cmd = new NpgsqlCommand(
                "DELETE FROM Price_List WHERE price_list_id = :id",
                con);

            cmd.Parameters.AddWithValue("id", id);
            cmd.ExecuteNonQuery();

            LoadPriceList();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ExportToExcel(dt);
        }

        private void UpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null) return;

            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells["price_list_id"].Value);
            int productId = Convert.ToInt32(dataGridView1.CurrentRow.Cells["product_id"].Value);
            double price = Convert.ToDouble(dataGridView1.CurrentRow.Cells["price"].Value);

            AddPriceForm f = new AddPriceForm(con, id, productId, price);
            f.ShowDialog();

            LoadPriceList();
        }

        /*

        private void ExportToExcel(DataTable table)
        {
            Excel.Application excel = new Excel.Application();
            Excel.Workbook wb = excel.Workbooks.Add();
            Excel.Worksheet ws = wb.Worksheets[1];

            // заголовки
            for (int i = 0; i < table.Columns.Count; i++)
                ws.Cells[1, i + 1] = table.Columns[i].ColumnName;

            // данные
            for (int i = 0; i < table.Rows.Count; i++)
                for (int j = 0; j < table.Columns.Count; j++)
                    ws.Cells[i + 2, j + 1] = table.Rows[i][j];

            excel.Visible = true;
        }*/
    }
}
