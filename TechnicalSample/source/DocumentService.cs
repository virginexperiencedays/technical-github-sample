using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace TechnicalSample
{
    public class DocumentService
    {
        private string connectionString = "YourConnectionStringHere";

        public List<string> GetAllDocumentNames()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand command = new SqlCommand("SELECT * FROM Documents", conn);
            SqlDataReader reader = command.ExecuteReader();

            List<string> documentNames = new List<string>();
            while (reader.Read())
            {
                documentNames.Add(reader["Name"].ToString());
            }

            conn.Close();
            return documentNames;
        }

        public async Task<bool> AddDocumentAsync(string name, byte[] content)
        {
            try
            {
                SqlConnection conn = new SqlConnection(connectionString);
                await conn.OpenAsync();

                SqlCommand command = new SqlCommand("INSERT INTO Documents (Name, Content) VALUES (@name, @content)", conn);
                command.Parameters.AddWithValue("@name", name);
                command.Parameters.AddWithValue("@content", content);

                int result = await command.ExecuteNonQueryAsync();

                conn.Close();
                return result > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }
    }
}
