using Microsoft.Data.SqlClient;
using System.Data;

namespace QCMS.Services
{
    public class DatabaseService
    {
        private readonly IConfiguration _configuration;

        public DatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Main Database Connection
        public SqlConnection GetConnection()
        {
            string connStr = _configuration.GetConnectionString("hps")
                ?? throw new InvalidOperationException("HPS connection string missing");

            return new SqlConnection(connStr);
        }

        // HR Database Connection
        public SqlConnection GetHrsConnection()
        {
            string connStrHr = _configuration.GetConnectionString("hris")
                ?? throw new InvalidOperationException("HRIS connection string missing");

            return new SqlConnection(connStrHr);
        }

        public async Task<DataTable> ExecuteQueryAsync(string query)
        {
            DataTable dt = new DataTable();

            // Use the correct connection string from configuration
            using (SqlConnection con = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    await con.OpenAsync();

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    da.Fill(dt);
                }
            }

            return dt;
        }
    }
}