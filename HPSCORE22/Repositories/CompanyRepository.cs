using CSWMS.Interface;
using QCMS.Services;

namespace CSWMS.Repositories
{
    using CSWMS.Models;
    using CSWMS.Utility;
    using Microsoft.Data.SqlClient;
    using QCMS.Services;
    using System.Data;
    public class CompanyRepository: ICompanyRepository
    {
        private readonly DatabaseService _db;

        public CompanyRepository(DatabaseService db)
        {
            _db = db;
        }
        public List<CompanyModel> GetALLCompany()
        {
            DataTable dt = new DataTable();

            using (SqlConnection Connection = _db.GetConnection())
            {
                Connection.Open();

                using (SqlCommand Command = new SqlCommand("SP_GetAllcompany", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;

                    // Increase timeout (in seconds)
                    Command.CommandTimeout = 300;

                    using (SqlDataAdapter da = new SqlDataAdapter(Command))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return ExtractData.Convert<CompanyModel>(dt).ToList();
        }
    }
}
