using CSWMS.Interface;

namespace CSWMS.Repositories
{
    using CSWMS.Models;
    using CSWMS.Utility;
    using Microsoft.Data.SqlClient;
    using QCMS.Services;
    using System.Data;
    public class BrandRepository : IBrandRepository
    {
        private readonly DatabaseService _db;
        public BrandRepository(DatabaseService db)
        {
            _db = db;
        }
        public List<BrandModel> GetALLBrand()
        {
            DataTable dt = new DataTable();
            using (SqlConnection Connection = _db.GetConnection())
            {
                Connection.Open();
                using (SqlCommand Command = new SqlCommand("SP_GetAllGroups", Connection))
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
            return ExtractData.Convert<BrandModel>(dt).ToList();
        }
    }
}
