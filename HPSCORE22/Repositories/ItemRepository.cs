using CSWMS.Interface;


namespace CSWMS.Repositories
{
    using CSWMS.Models;
    using CSWMS.Utility;
    using Microsoft.Data.SqlClient;
    using QCMS.Services;
    using System.Data;
    public class ItemRepository: IItemRepositoryInterface
    {
        private readonly DatabaseService _db;
        public ItemRepository(DatabaseService db)
        {
            _db = db;
        }
        public List<ItemModel> GetAllClassNameBrandWise(int BrandID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection Connection = _db.GetConnection())
            {
                Connection.Open();

                using (SqlCommand Command = new SqlCommand("GetItemsByGroupId", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 300;

                    // Send parameter
                    Command.Parameters.AddWithValue("@GroupId", BrandID);

                    using (SqlDataAdapter da = new SqlDataAdapter(Command))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return ExtractData.Convert<ItemModel>(dt).ToList();
        }
    }
}
