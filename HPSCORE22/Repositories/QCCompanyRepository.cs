using Dapper;
using QCMS.Models;
using QCMS.Services;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace QCMS.Repositories
{
    public class QCCompanyRepository
    {
        private readonly DatabaseService _databaseService;

        public QCCompanyRepository(DatabaseService databaseService)
        {
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
        }

        // 🔹 Get All Company
        public async Task<IEnumerable<QCCompany>> GetAllAsync()
        {
            using var conn = _databaseService.GetConnection();

            string sql = @"SELECT COMPANY_ID, COMPANY_NAME, CREATED_DATE, CREATED_BY,
                                  UPDATED_DATE, UPDATED_BY, IS_ACTIVE
                           FROM QCMS.QC_COMPANY
                           WHERE IS_ACTIVE = 1
                           ORDER BY COMPANY_NAME";

            return await conn.QueryAsync<QCCompany>(sql);
        }

        // 🔹 Get By ID
        public async Task<QCCompany?> GetByIdAsync(string companyId)
        {
            using var conn = _databaseService.GetConnection();

            string sql = @"SELECT COMPANY_ID, COMPANY_NAME, CREATED_DATE, CREATED_BY,
                                  UPDATED_DATE, UPDATED_BY, IS_ACTIVE
                           FROM QCMS.QC_COMPANY
                           WHERE COMPANY_ID = :CompanyId";

            return await conn.QueryFirstOrDefaultAsync<QCCompany>(sql, new
            {
                CompanyId = companyId
            });
        }

        // 🔹 Insert
        public async Task<int> InsertAsync(QCCompany model)
        {
            using var conn = _databaseService.GetConnection();

            string sql = @"INSERT INTO QCMS.QC_COMPANY
                          (COMPANY_ID, COMPANY_NAME, CREATED_DATE, CREATED_BY, IS_ACTIVE)
                          VALUES
                          (:CompanyId, :CompanyName, SYSDATE, :CreatedBy, 1)";

            return await conn.ExecuteAsync(sql, new
            {
                CompanyId = model.COMPANY_ID,
                CompanyName = model.COMPANY_NAME,
                CreatedBy = model.CREATED_BY
            });
        }

        // 🔹 Update
        //public async Task<int> UpdateAsync(QCCompany model)
        //{
        //    using var conn = _databaseService.GetConnection();

        //    string sql = @"UPDATE QCMS.QC_COMPANY
        //                   SET COMPANY_NAME = :CompanyName,
        //                       IS_ACTIVE = :IS_ACTIVE,
        //                       UPDATED_DATE = SYSDATE,
        //                       UPDATED_BY = :UpdatedBy
        //                   WHERE COMPANY_ID = :CompanyId";
        //    await conn.ExecuteAsync(sql, model);

        //    return await conn.ExecuteAsync(sql, new
        //    {
        //        CompanyName = model.COMPANY_NAME,
        //        UpdatedBy = model.UPDATED_BY,
        //        CompanyId = model.COMPANY_ID,
        //        IsActive = model.IS_ACTIVE
        //    }); 
        //}

        public async Task<int> UpdateAsync(QCCompany model)
        {
            using var conn = _databaseService.GetConnection();

            string sql = @"
        UPDATE QCMS.QC_COMPANY
        SET COMPANY_NAME = :COMPANY_NAME,
            UPDATED_DATE = SYSDATE,
            UPDATED_BY = :UPDATED_BY
        WHERE COMPANY_ID = :COMPANY_ID";

            return await conn.ExecuteAsync(sql, model); // 🔥 clean
        }

        // 🔹 Delete (Soft Delete)
        public async Task<int> DeleteAsync(string companyId)
        {
            using var conn = _databaseService.GetConnection();

            string sql = @"UPDATE QCMS.QC_COMPANY
                           SET IS_ACTIVE = 0
                           WHERE COMPANY_ID = :CompanyId";

            return await conn.ExecuteAsync(sql, new
            {
                CompanyId = companyId
            });
        }

        // 🔹 Check Exists (Important for validation)
        public async Task<bool> ExistsAsync(string companyId)
        {
            using var conn = _databaseService.GetConnection();

            string sql = @"SELECT COUNT(1)
                           FROM QCMS.QC_COMPANY
                           WHERE COMPANY_ID = :CompanyId";

            var count = await conn.ExecuteScalarAsync<int>(sql, new
            {
                CompanyId = companyId
            });

            return count > 0;
        }
        //public async Task<string> GenerateCompanyIdAsync()
        //{
        //    using var conn = _databaseService.GetConnection();
        //    await conn.OpenAsync();

        //    using var tran = conn.BeginTransaction();

        //    try
        //    {
        //        // 🔹 1. Get current sequence
        //        string getSql = @"SELECT TABLE_SEQ 
        //                  FROM TBL_SEQ 
        //                  WHERE TABLE_NAME = 'QC_COMPANY' 
        //                  FOR UPDATE";

        //        int currentSeq = await conn.ExecuteScalarAsync<int>(getSql, transaction: tran);

        //        // 🔹 2. Increment
        //        int newSeq = currentSeq + 1;

        //        // 🔹 3. Update sequence
        //        string updateSql = @"UPDATE TBL_SEQ 
        //                     SET TABLE_SEQ = :seq 
        //                     WHERE TABLE_NAME = 'QC_COMPANY'";

        //        await conn.ExecuteAsync(updateSql, new { seq = newSeq }, tran);

        //        tran.Commit();

        //        // 🔹 4. Format ID → COM-0001
        //        return "COM-" + newSeq.ToString("D4");
        //    }
        //    catch
        //    {
        //        tran.Rollback();
        //        throw;
        //    }
        //}
    }
}