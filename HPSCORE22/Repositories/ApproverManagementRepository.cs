using CSWMS.Interface;

namespace CSWMS.Repositories
{
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
        public 
    }
}
