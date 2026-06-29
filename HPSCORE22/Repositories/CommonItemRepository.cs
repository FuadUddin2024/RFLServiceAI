using CSWMS.Interface;
using CSWMS.Models;
using CSWMS.Utility;
using QCMS.Services;
using System.Data;
using System.Data.SqlClient;

namespace CSWMS.Repositories
{
    using CSWMS.Models;
    using CSWMS.Utility;
    using Microsoft.Data.SqlClient;
    using QCMS.Services;
    using System.Data;
    public class CommonItemRepository: ICommonItemRepository
    {
        private readonly DatabaseService _db;

        public CommonItemRepository(DatabaseService db)
        {
            _db = db;
        }
        public List<WarrantyCardInfo> GetALLWarrantyCardInfo()
        {
            DataTable dt = new DataTable();

            using (SqlConnection Connection = _db.GetConnection())
            {
                Connection.Open();

                using (SqlCommand Command = new SqlCommand("sp_GetWarrantyCardInfo", Connection))
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

            return ExtractData.Convert<WarrantyCardInfo>(dt).ToList();
        }
        public List<ComplainType> GetALLComplainType()
        {
            DataTable dt = new DataTable();

            using (SqlConnection Connection = _db.GetConnection())
            {
                Connection.Open();

                using (SqlCommand Command = new SqlCommand("CSP_GetAllComplainType", Connection))
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

            return ExtractData.Convert<ComplainType>(dt).ToList();
        }
    }
}
