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
        public List<NatureofProblem> GetProductyWiseActualProblem(int productID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection Connection = _db.GetConnection())
            {
                Connection.Open();

                using (SqlCommand Command = new SqlCommand("sp_GetNatureOfProblem", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 300;

                    // Send parameter
                    Command.Parameters.AddWithValue("@ProductId", productID);

                    using (SqlDataAdapter da = new SqlDataAdapter(Command))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return ExtractData.Convert<NatureofProblem>(dt).ToList();
        }
        public List<SubProblemModel> GetProductyWiseSubProblem(int ProblemID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection Connection = _db.GetConnection())
            {
                Connection.Open();

                using (SqlCommand Command = new SqlCommand("sp_GetSubProblemNatureOfProblem", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 300;

                    // Send parameter
                    Command.Parameters.AddWithValue("@ProblemID", ProblemID);

                    using (SqlDataAdapter da = new SqlDataAdapter(Command))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return ExtractData.Convert<SubProblemModel>(dt).ToList();
        }
        //public int InsertComplainFeedback(FeedabackModel FeedbackModel)
        //{

        //}
        public List<FeedabackModel> GetSingleFeedbackModel(string TickedID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection Connection = _db.GetConnection())
            {
                Connection.Open();

                using (SqlCommand Command = new SqlCommand("CSP_GetFeedbackByTicketId", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 300;

                    // Send parameter
                    Command.Parameters.AddWithValue("@TicketID", TickedID);

                    using (SqlDataAdapter da = new SqlDataAdapter(Command))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return ExtractData.Convert<FeedabackModel>(dt).ToList();
        }
        public List<SalesReturnApplication> GetSingleSrinfoForApply(string TickedID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection Connection = _db.GetConnection())
            {
                Connection.Open();

                using (SqlCommand Command = new SqlCommand("CSP_GetSRListForApply", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 300;

                    // Send parameter
                    Command.Parameters.AddWithValue("@TicketID", TickedID);

                    using (SqlDataAdapter da = new SqlDataAdapter(Command))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return ExtractData.Convert<SalesReturnApplication>(dt).ToList();
        }
    }
}
