using CSWMS.Interface;
using CSWMS.Models;
using CSWMS.Utility;
using QCMS.Services;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CSWMS.Repositories
{
    public class ComplainListRepository : IComplainListToken
    {
        private readonly DatabaseService _db;

        public ComplainListRepository(DatabaseService db)
        {
            _db = db;
        }

        public List<ComplainModel> GetALlComplainListForAssing()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = _db.GetConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("sp_GetAllComplainList", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 300;

                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return ExtractData.Convert<ComplainModel>(dt).ToList();
        }

        public List<ComplainModel> GetAllComplainList(string ticketCode)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = _db.GetConnection())
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SP_GetComplainByTicketCode", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 300;

                    command.Parameters.AddWithValue("@TicketCode", ticketCode);

                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return ExtractData.Convert<ComplainModel>(dt).ToList();
        }

        public bool UpdateComplain(ComplainModel complainModel)
        {
            bool isSuccess = true;

            try
            {
                using (SqlConnection connection = _db.GetConnection())
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("SP_UpdateSendAssign", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@TicketID", complainModel.TicketID);
                        cmd.Parameters.AddWithValue("@SendAssign", complainModel.SendAssign);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                isSuccess = false;
                throw;
            }

            return isSuccess;
        }
    }
}