using CSWMS.Interface;
using CSWMS.Models;
using CSWMS.Utility;
using QCMS.Services;
using System.Data;
using System.Data.SqlClient;

namespace CSWMS.Repositories
{   using Microsoft.Data.SqlClient;
    using System.Data;

    public class ZoneRepository: IZoneRepository
    {

        private readonly DatabaseService _db;

        public ZoneRepository(DatabaseService db)
        {
            _db = db;
        }

        public List<ZoneModel> GetALlZoneistForAssing()
        {
            DataTable dt = new DataTable();

            using (SqlConnection Connection = _db.GetConnection())
            {
                Connection.Open();

                using (SqlCommand Command = new SqlCommand("SP_GetAllZones", Connection))
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

            return ExtractData.Convert<ZoneModel>(dt).ToList();
        }
    }
}
