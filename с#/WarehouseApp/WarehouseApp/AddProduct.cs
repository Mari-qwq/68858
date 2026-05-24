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
    public partial class AddProduct : Form
    {
        public NpgsqlConnection con;

        int id;
        // ДОБАВЛЕНИЕ
        public AddProduct(NpgsqlConnection con, int id)
        {
            this.con = con;
            this.id = id;

            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterScreen;
        }

        // ИЗМЕНЕНИЕ
        public AddProduct(NpgsqlConnection con, int id, string name, string unit)
        {
            this.con = con;
            this.id = id;

            InitializeComponent();

            textBoxName.Text = name;
            textBoxUnit.Text = unit;

            this.StartPosition = FormStartPosition.CenterScreen;
        }


        private void button_Yes_Click(object sender, EventArgs e)
        {
            if (id == -1)
            {
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(
                        "INSERT INTO Products(name, unit) VALUES(:name, :unit)",
                        con);

                    cmd.Parameters.AddWithValue("name", textBoxName.Text);
                    cmd.Parameters.AddWithValue("unit", textBoxUnit.Text);

                    cmd.ExecuteNonQuery();

                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(
                        "UPDATE Products SET name = :name, unit = :unit WHERE product_id = :id",
                        con);

                    cmd.Parameters.AddWithValue("id", id);
                    cmd.Parameters.AddWithValue("name", textBoxName.Text);
                    cmd.Parameters.AddWithValue("unit", textBoxUnit.Text);

                    cmd.ExecuteNonQuery();

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


        private void textBoxName_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddProduct_Load(object sender, EventArgs e)
        {

        }
    }
}
