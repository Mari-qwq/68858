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
    public partial class AddFutura : Form
    {
        public NpgsqlConnection con;

        public AddFutura(NpgsqlConnection con, int id)
        {
            InitializeComponent();

            this.con = con;

            this.StartPosition = FormStartPosition.CenterScreen;

            LoadClients();
        }

        public void LoadClients()
        {
            string sql = "SELECT * FROM Clients ORDER BY client_id";

            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sql, con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            comboBoxClient.DataSource = dt;
            comboBoxClient.ValueMember = "client_id";
            comboBoxClient.DisplayMember = "name";
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime date = dateTimePicker1.Value.Date;

                NpgsqlCommand cmd = new NpgsqlCommand(
                    "INSERT INTO Futura(client_id, order_date, totalsum) " +
                    "VALUES(:client_id, :order_date, 0)",
                    con);

                cmd.Parameters.AddWithValue(
                    "client_id",
                    comboBoxClient.SelectedValue);

                cmd.Parameters.AddWithValue(
                    "order_date",
                    date);

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

        private void comboBoxClient_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
