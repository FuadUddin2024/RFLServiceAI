using CSWMS.Interface;
using CSWMS.Models;
using CSWMS.Utility;
using QCMS.Services;
using System.Data;

namespace CSWMS.Repositories
{
    using CSWMS.Models;
    using CSWMS.Utility;
    using Microsoft.Data.SqlClient;
    using QCMS.Services;
    using System.Data;
    public class ProductRepository: IProductRepository
    {
        private readonly DatabaseService _db;

        public ProductRepository(DatabaseService db)
        {
            _db = db;
        }
        public List<ProductModel> GetAllProductClassWise(int BrandID)
        {
            DataTable dt = new DataTable();

            using (SqlConnection Connection = _db.GetConnection())
            {
                Connection.Open();

                using (SqlCommand Command = new SqlCommand("GetProductByItemID", Connection))
                {
                    Command.CommandType = CommandType.StoredProcedure;
                    Command.CommandTimeout = 300;

                    // Send parameter
                    Command.Parameters.AddWithValue("@itemID", BrandID);

                    using (SqlDataAdapter da = new SqlDataAdapter(Command))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return ExtractData.Convert<ProductModel>(dt).ToList();
        }
    }
}
