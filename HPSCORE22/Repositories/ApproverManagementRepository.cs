using CSWMS.Interface;

namespace CSWMS.Repositories
{
    using CSWMS.Models;
    using CSWMS.Utility;
    using Microsoft.Data.SqlClient;
    using QCMS.Services;
    using System.Data;
    public class ApproverManagementRepository: IApproverManagementRepository
    {
        private readonly DatabaseService _db;

        public ApproverManagementRepository(DatabaseService db)
        {
            _db = db;
        }
        public void InsertSRPermission(UserWiseSRPermission Permission)
        {

            using (SqlConnection connection = _db.GetConnection())
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("CSP_InsertUserWiseSRPermission", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", Permission.UserId);
                    cmd.Parameters.AddWithValue("@ItemId", Permission.ItemId);
                    cmd.Parameters.AddWithValue("@Active", Permission.Active);
                    cmd.Parameters.AddWithValue("@InitialApv", Permission.InitialApv);
                    cmd.Parameters.AddWithValue("@FirstApv", Permission.FirstApv);
                    cmd.Parameters.AddWithValue("@SecApv", Permission.SecApv);
                    cmd.Parameters.AddWithValue("@FinalApv", Permission.FinalApv);
                    cmd.Parameters.AddWithValue("@EntryBy", Permission.EntryBy);
                    cmd.Parameters.AddWithValue("@EntryDate", Permission.EntryDate);
                    cmd.ExecuteNonQuery(); 
                }
            }
        }
        public List<UserWiseSRPermission> GetUserWiseSRPermission(string UserID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection Connection = _db.GetConnection())
            {
                Connection.Open();
                using (SqlCommand Command = new SqlCommand("CSP_GetUserWiseSrPer", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    // Increase timeout (in seconds)
                    Command.CommandTimeout = 300;
                    // Send parameter
                    Command.Parameters.AddWithValue("@UserId", UserID);
                    using (SqlDataAdapter da = new SqlDataAdapter(Command))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return ExtractData.Convert<UserWiseSRPermission>(dt).ToList();
        }
        public bool DeletePreviousPermissions(string UserID)
        {
            try
            {
                using (SqlConnection connection = _db.GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CSP_DeleteUserWiseSRPermission", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 300;
                        command.Parameters.AddWithValue("@UserId", UserID);
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Panel Permission
        public void InsertPanelPermission(panelPermissionSetupModel Permission)
        {

            using (SqlConnection connection = _db.GetConnection())
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("sp_InsertUserWiseZonePermission", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", Permission.UserId);
                    cmd.Parameters.AddWithValue("@ZoneId", Permission.ZoneId);
                    cmd.Parameters.AddWithValue("@Active", Permission.Active);
                    cmd.Parameters.AddWithValue("@FirstApv", Permission.FirstApv);
                    cmd.Parameters.AddWithValue("@FinalApv", Permission.FinalApv);
                    cmd.Parameters.AddWithValue("@EntryBy", Permission.EntryBy);
                    cmd.Parameters.AddWithValue("@EntryDate", Permission.EntryDate);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<panelPermissionSetupModel> GetUserWisePanelPermission(string UserID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection Connection = _db.GetConnection())
            {
                Connection.Open();
                using (SqlCommand Command = new SqlCommand("CSp_GetUserWisePanelApv", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    // Increase timeout (in seconds)
                    Command.CommandTimeout = 300;
                    // Send parameter
                    Command.Parameters.AddWithValue("@UserId", UserID);
                    using (SqlDataAdapter da = new SqlDataAdapter(Command))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return ExtractData.Convert<panelPermissionSetupModel>(dt).ToList();
        }
        public bool DeletePreviousPermissionsPanel(string UserID)
        {
            try
            {
                using (SqlConnection connection = _db.GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CSP_DeleteUserWisePanelPermission", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 300;
                        command.Parameters.AddWithValue("@UserId", UserID);
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Damage Return Permission 
        public void InsertDamagePermission(panelPermissionSetupModel Permission)
        {
            using (SqlConnection connection = _db.GetConnection())
            {
                connection.Open();

                using (SqlCommand cmd = new SqlCommand("sp_InsertUserWiseZonePermission", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", Permission.UserId);
                    cmd.Parameters.AddWithValue("@ZoneId", Permission.ZoneId);
                    cmd.Parameters.AddWithValue("@Active", Permission.Active);
                    cmd.Parameters.AddWithValue("@FirstApv", Permission.FirstApv);
                    cmd.Parameters.AddWithValue("@FinalApv", Permission.FinalApv);
                    cmd.Parameters.AddWithValue("@EntryBy", Permission.EntryBy);
                    cmd.Parameters.AddWithValue("@EntryDate", Permission.EntryDate);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<panelPermissionSetupModel> GetUserWiseDamage(string UserID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection Connection = _db.GetConnection())
            {
                Connection.Open();
                using (SqlCommand Command = new SqlCommand("CSp_GetUserWisePanelApv", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    // Increase timeout (in seconds)
                    Command.CommandTimeout = 300;
                    // Send parameter
                    Command.Parameters.AddWithValue("@UserId", UserID);
                    using (SqlDataAdapter da = new SqlDataAdapter(Command))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return ExtractData.Convert<panelPermissionSetupModel>(dt).ToList();
        }
        public bool DeletePreviousPermissionsDamage(string UserID)
        {
            try
            {
                using (SqlConnection connection = _db.GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand("CSP_DeleteUserWisePanelPermission", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = 300;
                        command.Parameters.AddWithValue("@UserId", UserID);
                        command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
