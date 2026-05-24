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
    public partial class ClientForm : Form
    {
        public NpgsqlConnection con;


        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        public ClientForm(NpgsqlConnection con)
        {
            this.con = con;
            InitializeComponent();
            UpdateTable();
        }


        public void UpdateTable()
        {
            string sql = "SELECT * FROM Clients ORDER BY client_id";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);

            ds.Reset();

            da.Fill(ds);

            dt = ds.Tables[0];

            dataGridView1.DataSource = dt;

            dataGridView1.Columns[0].HeaderText = "Номер";
            dataGridView1.Columns[1].HeaderText = "Имя";
            dataGridView1.Columns[2].HeaderText = "Телефон";
            dataGridView1.Columns[3].HeaderText = "Адрес";

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {

        }

        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddClient f = new AddClient(con, -1);

            f.ShowDialog();

            UpdateTable();
        }

        private void UpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id =
               (int)dataGridView1.CurrentRow.Cells["client_id"].Value;

            string name =
                (string)dataGridView1.CurrentRow.Cells["name"].Value;

            string phone =
                (string)dataGridView1.CurrentRow.Cells["phone"].Value;

            string address =
                (string)dataGridView1.CurrentRow.Cells["address"].Value;

            AddClient f = new AddClient(con, id, name, phone, address);

            f.ShowDialog();

            UpdateTable();
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int id = (int)dataGridView1.CurrentRow.Cells["client_id"].Value;

            NpgsqlCommand command =
                new NpgsqlCommand(
                    "DELETE FROM Clients WHERE client_id = :id",
                    con);

            command.Parameters.AddWithValue("id", id);

            command.ExecuteNonQuery();

            UpdateTable();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
