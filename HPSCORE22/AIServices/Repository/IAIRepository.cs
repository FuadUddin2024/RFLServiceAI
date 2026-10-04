using CSWMS.AIServices.Model;

namespace CSWMS.AIServices.Repository
{
    public interface IAIRepository
    {
        Task<List<DatabaseObjectModel>> GetDataBaseSchema();
        public string ConvertDatabaseObjectToString(DatabaseObjectModel databaseObject);
        Task<float[]> ConvertingDataBaseTextToEmbadding(string text);
    }
}
