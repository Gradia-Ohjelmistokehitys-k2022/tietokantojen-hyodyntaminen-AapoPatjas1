using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadOpiskelijat(); // Lataa opiskelijatiedot DataGridViewiin
            LoadOpiskelijaryhmat(); // Lataa opiskelijaryhmät ComboBoxiin
        }

        private void LoadOpiskelijat()
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Opiskelijat;Trusted_Connection=True;";

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT id, Etunimi, Sukunimi FROM Opiskelija";

                    var adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Bindataan DataGridView komponenttiin
                    dataGridViewOpiskelijat.DataSource = dataTable;
                    dataGridViewOpiskelijat.Columns["id"].Visible = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Virhe tietoja haettaessa: " + ex.Message);
                }
            }
        }

        private void LoadOpiskelijaryhmat()
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Opiskelijat;Trusted_Connection=True;";

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT DISTINCT RyhmänNimi FROM OpiskelijaRyhma";

                    using (var command = new SqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Lisätään ComboBoxiin jokainen ryhmä
                            comboBoxOpiskelijaryhmat.Items.Add(reader["RyhmänNimi"].ToString());
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Virhe ryhmiä haettaessa: " + ex.Message);
                }
            }
        }

        private int seVitunRyhmaId;
        private void buttonLisaaOpiskelija_Click(object sender, EventArgs e)
        {
            string etunimi = textBoxEtunimi.Text.Trim();
            string sukunimi = textBoxSukunimi.Text.Trim();
            string ComboBoxOpiskelijaRyhmat = comboBoxOpiskelijaryhmat.Text.Trim();

            if (string.IsNullOrEmpty(etunimi) || string.IsNullOrEmpty(sukunimi))
            {
                MessageBox.Show("Täytä sekä etunimi että sukunimi ennen tallentamista.");
                return;
            }

            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Opiskelijat;Trusted_Connection=True;";
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (var coonn1 = new SqlCommand("select id from OpiskelijaRyhma\r\nWHERE RyhmänNimi='" + ComboBoxOpiskelijaRyhmat + "';", connection))
                    {
                        using (var reader = coonn1.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Lisätään ComboBoxiin jokainen ryhmä
                                seVitunRyhmaId = Convert.ToInt32(reader["id"]);
                            }
                        }
                    }
                    string query = "INSERT INTO Opiskelija (Etunimi, Sukunimi, RyhmaId) VALUES (@Etunimi, @Sukunimi, @RyhmaId)";

                    using (var command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Etunimi", etunimi);
                        command.Parameters.AddWithValue("@Sukunimi", sukunimi);
                        command.Parameters.AddWithValue("@RyhmaId", seVitunRyhmaId);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Opiskelija lisätty onnistuneesti!");
                            LoadOpiskelijat(); // Päivitetään DataGridView
                            textBoxEtunimi.Clear();
                            textBoxSukunimi.Clear();
                        }
                        else
                        {
                            MessageBox.Show("Opiskelijan lisääminen epäonnistui.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Virhe lisättäess   ä opiskelijaa: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buttonLisaaOpiskelija_Click(sender, e);
        }

        private void buttonPoistaOpiskelija_Click(object sender, EventArgs e)
        {
            // Varmistetaan, että käyttäjä on valinnut rivin
            if (dataGridViewOpiskelijat.SelectedRows.Count > 0)
            {
                // Haetaan valitun opiskelijan ID
                var selectedRow = dataGridViewOpiskelijat.SelectedRows[0];
                if (selectedRow.Cells["id"].Value != null)
                {
                    int opiskelijaId = Convert.ToInt32(selectedRow.Cells["id"].Value);

                    // Poistetaan opiskelija tietokannasta
                    string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Opiskelijat;Trusted_Connection=True;";
                    using (var connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            connection.Open();
                            string query = "DELETE FROM Opiskelija WHERE id = @Id";
                            using (var command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@Id", opiskelijaId);
                                command.ExecuteNonQuery();
                            }

                            // Poistetaan rivi DataGridViewistä
                            dataGridViewOpiskelijat.Rows.Remove(selectedRow);

                            MessageBox.Show("Opiskelija poistettu onnistuneesti.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Virhe opiskelijaa poistettaessa: " + ex.Message);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Valitun opiskelijan ID ei ole kelvollinen.");
                }
            }
            else
            {
                MessageBox.Show("Valitse ensin opiskelija, jonka haluat poistaa.");
            }
        }
    }
    }

