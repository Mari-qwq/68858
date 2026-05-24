using Npgsql;


namespace WarehouseApp
{
    public partial class Form1 : Form
    {

        public NpgsqlConnection con;
        public Form1()
        {
            InitializeComponent();
            MyLoad();
        }

        public void MyLoad()
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            con = new NpgsqlConnection("Server=localhost;Port=5432;UserID=postgres;Password=postpass;Database=WarehouseApp");
            try
            {
                con.Open();
                MessageBox.Show("Подключение успешно");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Clients_Click(object sender, EventArgs e)
        {
            ClientForm cf = new ClientForm(con);
            cf.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Products_Click(object sender, EventArgs e)
        {
            ProductForm pf = new ProductForm(con);
            pf.ShowDialog();
        }

        private void Futura_Click(object sender, EventArgs e)
        {
            FuturaForm ff = new FuturaForm(con);
            ff.ShowDialog();
        }

        private void buttonPrice_Click(object sender, EventArgs e)
        {
            PriceListForm plf = new PriceListForm(con);
            plf.ShowDialog();
        }
    }
}