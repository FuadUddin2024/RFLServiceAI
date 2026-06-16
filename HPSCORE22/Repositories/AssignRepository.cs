using CSWMS.Interface;
using CSWMS.Models;
using CSWMS.Utility;
using QCMS.Services;
using System.Data;
using System.Data.SqlClient;
namespace CSWMS.Repositories
{
    using Microsoft.Data.SqlClient;
    using System.Data;

    public class AssignRepository: IAssignRepository
    {
        private readonly DatabaseService _db;

        public AssignRepository(DatabaseService db)
        {
            _db = db;
        }

        public List<AssignModel> GetAllComplainList(string ticketCode)
        {
            DataTable dt = new DataTable();

            using (SqlConnection Connection = _db.GetConnection())
            {
                Connection.Open();

                using (SqlCommand Command = new SqlCommand("SP_GetAssingByTicketCode", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 300;

                    // Send parameter
                    Command.Parameters.AddWithValue("@TicketCode", ticketCode);

                    using (SqlDataAdapter da = new SqlDataAdapter(Command))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return ExtractData.Convert<AssignModel>(dt).ToList();
        }
        public int InsertAssign(AssignModel AssingModel)
        {
            int assignId = 0;

            using (SqlConnection connection = _db.GetConnection())
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("SP_InsertAssign", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@TicketID", AssingModel.TicketID);
                    cmd.Parameters.AddWithValue("@AssignDate", AssingModel.AssignDate);
                    cmd.Parameters.AddWithValue("@FinishDate",
                        AssingModel.FinishDate ?? (object)DBNull.Value);

                    cmd.Parameters.AddWithValue("@CustomerName", AssingModel.CustomerName);
                    cmd.Parameters.AddWithValue("@CustomerContactNo", AssingModel.CustomerContactNo);
                    cmd.Parameters.AddWithValue("@CustomerAddress", AssingModel.CustomerAddress);
                    cmd.Parameters.AddWithValue("@ProductName", AssingModel.ProductName);

                    cmd.Parameters.AddWithValue("@StatusId", AssingModel.StatusId);
                    cmd.Parameters.AddWithValue("@AssignZoneId", AssingModel.AssignZoneId);
                    cmd.Parameters.AddWithValue("@SupervisorId", AssingModel.SupervisorId);
                    cmd.Parameters.AddWithValue("@CompanyId", AssingModel.CompanyId);

                    cmd.Parameters.AddWithValue("@IsAssign", AssingModel.IsAssign);

                    cmd.Parameters.AddWithValue("@EntryBy", AssingModel.EntryBy);
                    cmd.Parameters.AddWithValue("@EntryDate", AssingModel.EntryDate);

                    cmd.Parameters.AddWithValue("@Remarks",
                        string.IsNullOrEmpty(AssingModel.Remarks)
                        ? (object)DBNull.Value
                        : AssingModel.Remarks);

                    assignId = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }

            return assignId;
        }

        // Compain Technician Assign Person
        public List<AssignModel> GetAllZoneAssingComplainList()
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = _db.GetConnection())
            {
                connection.Open();

                string query = @"
            SELECT
                a.AssignId,
                a.TicketID,
                a.AssignDate,
                a.AssignZoneId,
                a.StatusId,
                a.SupervisorId,
                a.CompanyId,
                a.CustomerName,
                a.CustomerAddress,
                a.CustomerContactNo,
                a.FinishDate,
                a.ProductName,
                a.Remarks,
                a.IsAssign,
                a.SendFeedback,
                st.StatusName,
                z.ZoneName,
                su.SupervisorName,
                c.CompanyName,
                com.EntryDate,
                com.ProblemName
            FROM dbo.Assign a
            INNER JOIN dbo.Complain com
                ON com.TicketCode = a.TicketID
            INNER JOIN dbo.Status st
                ON st.StatusId = a.StatusId
            INNER JOIN dbo.Zone z
                ON z.ZoneId = a.AssignZoneId
            INNER JOIN dbo.Company c
                ON c.CompanyId = a.CompanyId
            INNER JOIN dbo.Supervisor su
                ON su.SupervisorId = a.SupervisorId
where a.IsAssign=0 ORDER BY com.EntryDate DESC";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.CommandType = CommandType.Text;
                    command.CommandTimeout = 300;

                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return ExtractData.Convert<AssignModel>(dt).ToList();
        }
        public List<AssignModel> GetAllZoneSingleAssing(string ticketCode)
        {
            DataTable dt = new DataTable();

            using (SqlConnection Connection = _db.GetConnection())
            {
                Connection.Open();

                using (SqlCommand Command = new SqlCommand("sp_GetAssignDetailsByTicketID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 300;

                    // Send parameter
                    Command.Parameters.AddWithValue("@TicketID", ticketCode);

                    using (SqlDataAdapter da = new SqlDataAdapter(Command))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return ExtractData.Convert<AssignModel>(dt).ToList();
        }
        public bool AssignPerson(AssignModel AssignModel)
        {
            bool isUpdated = true;
            try
            {
                using (SqlConnection connection = _db.GetConnection())
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_UpdateAssignlISTPerson", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@CustomerAddress", AssignModel.CustomerAddress ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@ProductName", AssignModel.ProblemName ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TechnicianId", AssignModel.TechnicianId);
                        cmd.Parameters.AddWithValue("@FinishDate", AssignModel.FinishDate ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@Remarks", AssignModel.Remarks ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@SendFeedback", AssignModel.SendFeedback ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@IsAssign", AssignModel.IsAssign ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@TicketID", AssignModel.TicketID);
                        int rows = cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                isUpdated = false;
                // Log the exception (you can use a logging framework here)
                Console.WriteLine($"Error in AssignPerson: {ex.Message}");
            }
            return isUpdated;
        }
    }
}
