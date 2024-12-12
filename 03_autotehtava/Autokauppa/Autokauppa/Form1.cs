using System.Data.SqlClient;

namespace Autokauppa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void HaeAutot()
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Autokauppa;Trusted_Connection=True;";

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Merkki, Malli FROM Auto";

                    using (var command = new SqlCommand(query, connection))
                    {
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            // K‰ytet‰‰n string-interpolointia
                            string merkki = reader["Merkki"].ToString();
                            string malli = reader["Malli"].ToString();

                            // Tulostetaan tietoja konsoliin
                            Console.WriteLine($"Merkki: {merkki}, Malli: {malli}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Virhe tietojen haussa: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
