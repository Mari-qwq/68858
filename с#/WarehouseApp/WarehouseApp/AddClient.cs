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
    public partial class AddClient : Form
    {
        public NpgsqlConnection con;

        int id;
        public AddClient(NpgsqlConnection con, int id)
        {
            this.con = con;
            this.id = id;

            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        public AddClient(
           NpgsqlConnection con,
           int id,
           string name,
           string phone,
           string address)
        {
            InitializeComponent();

            this.con = con;
            this.id = id;

            textBox1.Text = name;
            textBoxNumber.Text = phone;
            textBoxAdress.Text = address;

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // ДОБАВЛЕНИЕ
            if (id == -1)
            {
                try
                {
                    NpgsqlCommand command =
                        new NpgsqlCommand(
                            "INSERT INTO Clients(name, phone, address) " +
                            "VALUES(:name, :phone, :address)",
                            con);

                    command.Parameters.AddWithValue(
                        "name",
                        textBox1.Text);

                    command.Parameters.AddWithValue(
                        "phone",
                        textBoxNumber.Text);

                    command.Parameters.AddWithValue(
                        "address",
                        textBoxAdress.Text);

                    command.ExecuteNonQuery();

                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            // ИЗМЕНЕНИЕ
            else
            {
                try
                {
                    NpgsqlCommand command =
                        new NpgsqlCommand(
                            "UPDATE Clients " +
                            "SET name = :name, " +
                            "phone = :phone, " +
                            "address = :address " +
                            "WHERE client_id = :id",
                            con);

                    command.Parameters.AddWithValue("id", id);

                    command.Parameters.AddWithValue(
                        "name",
                        textBox1.Text);

                    command.Parameters.AddWithValue(
                        "phone",
                        textBoxNumber.Text);

                    command.Parameters.AddWithValue(
                        "address",
                        textBoxAdress.Text);

                    command.ExecuteNonQuery();

                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button_No_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBoxNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxAdress_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddClient_Load(object sender, EventArgs e)
        {

        }
    }
}
