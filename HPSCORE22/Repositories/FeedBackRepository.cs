using CSWMS.Interface;
using CSWMS.Models;
using CSWMS.Utility;
using QCMS.Services;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CSWMS.Repositories
{
    public class FeedBackRepository: IFeedbackRepository
    {
        private readonly DatabaseService _db;

        public FeedBackRepository(DatabaseService db)
        {
            _db = db;
        }
        public List<FeedbackListForAssign> GetZoneWiseFeedBack(int ZoneID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection Connection = _db.GetConnection())
            {
                Connection.Open();

                using (SqlCommand Command = new SqlCommand("sp_GetDataZoneWiseAssignListNew", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 300;

                    // Send parameter
                    Command.Parameters.AddWithValue("@ZoneId", ZoneID);

                    using (SqlDataAdapter da = new SqlDataAdapter(Command))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return ExtractData.Convert<FeedbackListForAssign>(dt).ToList();
        }

    }
}
