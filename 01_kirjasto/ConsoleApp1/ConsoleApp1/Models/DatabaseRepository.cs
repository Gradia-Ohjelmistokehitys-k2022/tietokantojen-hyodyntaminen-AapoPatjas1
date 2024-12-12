using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConsoleApp1.Models
{
    internal class DatabaseRepository
    {
        private string _connectionString;

        public void DatabaseConnection(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string IsDbConnectionEstablished()
        {
            SqlConnection myConnection = new(
                                        "Server=(localdb)\\MSSQLLocalDB;" +
                                       "Trusted_Connection=true;" +
                                       "Database=database_name");

            try
            {
                myConnection.Open();
                return "Connection established!";
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public List<Book> GetAllBooksLastFiveYears()

        {
            List<Book> books = new();


            using var dbConnection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;" +
                                                       "Trusted_Connection=true;" +
                                                       "Database=database_name");

            dbConnection.Open();

            string query = "SELECT * FROM Book WHERE PublicationYear >= @StartYear";
            using var command = new SqlCommand(query, dbConnection);
            command.Parameters.AddWithValue("@StartYear", DateTime.Now.Year - 5);

            using var reader = command.ExecuteReader();

            while (reader.Read()) // Käydään läpi kaikki kyselyn palauttamat rivit
            {
                // Luodaan uusi kirjaolio ja täytetään tiedot
                Book book = new()
                {
                    BookId = Convert.ToInt32(reader["BookId"]), // Korjaa, jos Book-taulussa ei ole "ID"-sarake
                    Title = reader["Title"].ToString(),
                    ISBN = reader["ISBN"].ToString(),
                    PublicationYear = Convert.ToInt32(reader["PublicationYear"]),
                    AvailableCopies = Convert.ToInt32(reader["AvailableCopies"])
                };

                books.Add(book); // Lisätään kirja listaan
            }

            return books; // Palautetaan lista kirjoista
        }

        public void GetAverageMemberAge()
        {
            using var dbConnection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;" +
                                                       "Trusted_Connection=true;" +
                                                       "Database=database_name");

            dbConnection.Open();

            // Vaihe 1: Lisää oletusarvoinen syntymäaika puuttuville riveille
            string updateQuery = "UPDATE Member SET DateOfBirth = @DefaultDateOfBirth WHERE DateOfBirth IS NULL";
            using (var updateCommand = new SqlCommand(updateQuery, dbConnection))
            {
                updateCommand.Parameters.AddWithValue("@DefaultDateOfBirth", new DateTime(1990, 1, 1)); // Oletus syntymäaika
                updateCommand.ExecuteNonQuery();
            }

            // Vaihe 2: Laske asiakkaiden keski-ikä
            string query = @"
                SELECT AVG(DATEDIFF(YEAR, DateOfBirth, GETDATE())) AS AverageAge
                FROM Member
                WHERE DateOfBirth IS NOT NULL";
            using var command = new SqlCommand(query, dbConnection);

            object result = command.ExecuteScalar();

            // Tulosta keski-ikä
            if (result != DBNull.Value && result != null)
            {
                double averageAge = Convert.ToDouble(result);
                Console.WriteLine($"Kirjaston asiakkaiden keski-ikä on: {averageAge:F1} vuotta");
            }
            else
            {
                Console.WriteLine("Ei voitu laskea keski-ikää, koska syntymäaikatiedot puuttuvat.");
            }
        }
        public void GetMostAvailableBook()
        {
            using var dbConnection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;" +
                                                       "Trusted_Connection=true;" +
                                                       "Database=database_name");

            dbConnection.Open();

            // SQL-kysely: Hae kirja, jolla on eniten kappaleita
            string query = @"
        SELECT TOP 1 Title, AvailableCopies 
        FROM Book 
        ORDER BY AvailableCopies DESC";

            using var command = new SqlCommand(query, dbConnection);
            using var reader = command.ExecuteReader();

            if (reader.Read()) // Jos tuloksia löytyy
            {
                string title = reader["Title"].ToString();
                int availableCopies = Convert.ToInt32(reader["AvailableCopies"]);

                Console.WriteLine($"Kirja, jota on eniten tarjolla: {title} ({availableCopies} kappaletta)");
            }
            else
            {
                Console.WriteLine("Kirjoja ei löydy tietokannasta.");
            }
        }
        public void GetMembersWithLoans()
        {
            using var dbConnection = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;" +
                                                       "Trusted_Connection=true;" +
                                                       "Database=database_name");

            dbConnection.Open();

            // SQL-kysely: Hae jäsenet, jotka ovat lainanneet ainakin yhden kirjan, sekä kirjojen ISBN:t
            string query = @"
        SELECT 
            Member.FirstName,
            Member.LastName,
            Book.ISBN
        FROM 
            Loan
        INNER JOIN 
            Member ON Loan.MemberId = Member.MemberId
        INNER JOIN 
            Book ON Loan.BookId = Book.BookId
        ORDER BY 
            Member.LastName, Member.FirstName";

            using var command = new SqlCommand(query, dbConnection);
            using var reader = command.ExecuteReader();

            Console.WriteLine("Jäsenet, jotka ovat lainanneet kirjoja:");

            // Käy läpi kyselyn palauttamat rivit
            while (reader.Read())
            {
                string firstName = reader["FirstName"].ToString();
                string lastName = reader["LastName"].ToString();
                string isbn = reader["ISBN"].ToString();

                Console.WriteLine($"{firstName} {lastName} lainasi kirjan ISBN: {isbn}");
            }
        }

    }
}
