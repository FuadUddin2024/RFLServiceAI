using CSWMS.Interface;


namespace CSWMS.Repositories
{
    using CSWMS.Models;
    using CSWMS.Utility;
    using Microsoft.Data.SqlClient;
    using QCMS.Services;
    using System.Data;
    public class UserManagementRepository: IUserManagementRepository
    {
        private readonly DatabaseService _db;
        public UserManagementRepository(DatabaseService Db) 
        {
            _db=Db;
        }
        public List<UserManagementModel> GetAllUserList()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = _db.GetConnection())
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("sp_GetAllUsers", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 300;
                    using (SqlDataAdapter da = new SqlDataAdapter(command))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return ExtractData.Convert<UserManagementModel>(dt).ToList();
        }
    }
}
