using Dapper;
using Oracle.ManagedDataAccess.Client;
using QCMS.Models;
using QCMS.Services;

namespace QCMS.Repositories
{
    public class QCLocationRepository
    {
        private readonly DatabaseService _databaseService;

        public QCLocationRepository(DatabaseService databaseService)
        {
            _databaseService = databaseService ?? throw new ArgumentNullException(nameof(databaseService));
        }
        public async Task<IEnumerable<QC_Location>> GetAllAsync()
        {
            using var conn = _databaseService.GetConnection();

            string sql = @"
                SELECT 
                    l.LOCATION_ID,
                    l.COMPANY_ID,
                    c.COMPANY_NAME, 
                    l.LOCATION_NAME,
                    l.LOCATION_SHORT_NAME,
                    l.IS_ACTIVE
                FROM QC_LOCATION l
                LEFT JOIN QC_COMPANY c 
                    ON l.COMPANY_ID = c.COMPANY_ID
                WHERE l.IS_ACTIVE = 1
                ORDER BY l.LOCATION_ID";

            return await conn.QueryAsync<QC_Location>(sql);
        }

        public async Task<QC_Location?> GetByIdAsync(string id)
        {
            using var conn = _databaseService.GetConnection();

                    string sql = @"
                SELECT 
                    l.*,
                    c.COMPANY_NAME
                FROM QC_LOCATION l
                LEFT JOIN QC_COMPANY c 
                    ON l.COMPANY_ID = c.COMPANY_ID
                WHERE l.LOCATION_ID = :id";

            return await conn.QueryFirstOrDefaultAsync<QC_Location>(sql, new { id });
        }

        public async Task<bool> ExistsAsync(string id)
        {
            using var conn = _databaseService.GetConnection();

            string sql = @"SELECT COUNT(1) FROM QC_LOCATION WHERE LOCATION_ID = :id";

            return await conn.ExecuteScalarAsync<int>(sql, new { id }) > 0;
        }

        public async Task InsertAsync(QC_Location model)
        {
            using var conn = _databaseService.GetConnection();

            string sql = @"INSERT INTO QC_LOCATION
            (LOCATION_ID, COMPANY_ID, LOCATION_NAME, LOCATION_SHORT_NAME,
             CREATED_DATE, CREATED_BY, IS_ACTIVE)
            VALUES
            (:LOCATION_ID, :COMPANY_ID, :LOCATION_NAME, :LOCATION_SHORT_NAME,
             SYSDATE, :CREATED_BY, :IS_ACTIVE)";

            try
            {
                await conn.ExecuteAsync(sql, model);
            }
            catch (OracleException ex) when (ex.Number == 1)
            {
                throw new Exception("Duplicate Location Name not allowed");
            }
        }

        public async Task UpdateAsync(QC_Location model)
        {
            using var conn = _databaseService.GetConnection();

            string sql = @"
                UPDATE QC_LOCATION SET
                    COMPANY_ID = :COMPANY_ID,
                    LOCATION_NAME = :LOCATION_NAME,
                    LOCATION_SHORT_NAME = :LOCATION_SHORT_NAME,
                    UPDATED_DATE = SYSDATE,
                    UPDATED_BY = :UPDATED_BY
                WHERE LOCATION_ID = :LOCATION_ID";

            await conn.ExecuteAsync(sql, model);
        }

        public async Task<bool> NameExistsAsync(string name, string? id = null)
        {
            using var conn = _databaseService.GetConnection();

            string sql = @"
        SELECT COUNT(1)
        FROM QC_LOCATION
        WHERE LOCATION_NAME = :name
        AND (:id IS NULL OR LOCATION_ID != :id)";

            return await conn.ExecuteScalarAsync<int>(sql, new { name, id }) > 0;
        }

        public async Task<int> DeleteAsync(string locationId)
        {
            using var conn = _databaseService.GetConnection();

            string sql = @"UPDATE QCMS.QC_LOCATION
                           SET IS_ACTIVE = 0
                           WHERE LOCATION_ID = :LocationId";

            return await conn.ExecuteAsync(sql, new
            {
                LocationId = locationId
            });
        }
    }
}