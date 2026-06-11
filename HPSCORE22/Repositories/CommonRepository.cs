using Dapper;
using QCMS.Services;

namespace QCMS.Repositories
{
    public class CommonRepository
    {
        private readonly DatabaseService _databaseService;

        public CommonRepository(DatabaseService databaseService)
        {
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
        }
        public async Task<string> GenerateIdAsync(string tableName)
        {
            using var conn = _databaseService.GetConnection();
            await conn.OpenAsync();

            using var tran = conn.BeginTransaction();

            try
            {
                string sql = @"
            SELECT TABLE_SEQ, PREFIX 
            FROM TBL_SEQ 
            WHERE TABLE_NAME = :tableName AND ACT = 1
            FOR UPDATE";

                var result = await conn.QueryFirstOrDefaultAsync<dynamic>(
                    sql, new { tableName }, tran);

                if (result == null)
                    throw new Exception($"Sequence not found for {tableName}");

                long currentSeq = result.TABLE_SEQ;
                string prefix = result.PREFIX;

                long newSeq = currentSeq + 1;

                string updateSql = @"
            UPDATE TBL_SEQ 
            SET TABLE_SEQ = :seq 
            WHERE TABLE_NAME = :tableName";

                await conn.ExecuteAsync(updateSql,
                    new { seq = newSeq, tableName }, tran);

                tran.Commit();

                // 🔥 Final ID format
                return $"{prefix}-{newSeq.ToString("D4")}";
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }
    }

}
