using CSWMS.AIServices.Model;
using CSWMS.AIServices.Repository;
using System.Data;

namespace CSWMS.AIServices.Extracode
{

    //public interface IAIServiceSQL
    //{
    //    // Confirm Code Start
    //    Task<List<DatabaseObjectModel>> GetDatabaseObjectsAsync();
    //    public string ConvertDatatabletostring(DatabaseObjectModel schema);
    //    Task<float[]> GenerateSchemaEmbeddingAsync(DatabaseObjectModel schema);
    //    Task<bool> SaveToQdrantAsync(string objectName, string objectType, string text, float[] embedding);
    //    // Confirm Code End

    //    Task<string> ExecuteQueryAsync(string sql);
    //    Task<DataTable> GetDatabaseSchemaAsync();
    //    Task<bool> SaveToQdrantAsync(string question, float[] embedding);
        
       
    //    Task<float[]> GenerateQuestionEmbeddingAsyncSqlQuestion(string question);
    //    Task<string> SearchQdrantAsync(float[] embedding);
    //    Task<string> GenerateSqlAsync(string question, string schema);
    //    Task<string> ClassifyQuestionAsync(string question, string schema);
    //    public Dictionary<string, string> ConvertSchemaToTableTexts(DataTable schema);
       
    //}
    //public class AIBusinessLayer : IAIBusinessLayer
    //{
    //    public readonly IAIRepository _aITaskSystemSQL;
    //    public AIBusinessLayer(IAIRepository aITaskSystemSQL)
    //    {
    //        _aITaskSystemSQL = aITaskSystemSQL;
    //    }
    //    // Setup: Database Schema Retrieval
    //    public async Task<List<DatabaseObjectModel>> GetDatabaseObjectsAsync()
    //    {
    //        return await _aITaskSystemSQL.GetDatabaseObjectsAsync();
    //    }
    //    public string ConvertDataBaseObjectToSting(DatabaseObjectModel schema)
    //    {
    //        if (schema == null)
    //        {
    //            throw new ArgumentException("Schema is required.");
    //        }
    //        var embedding = _aITaskSystemSQL.ConvertDatabaseObjectToText(schema);
    //        //  var length= embedding.Length;
    //        return embedding;
    //    }
    //    public async Task<float[]> ConvertingDatabaseTextToEmbedding(DatabaseObjectModel schema)
    //    {
    //        if (schema == null)
    //        {
    //            throw new ArgumentException("Schema is required.");
    //        }
    //        var embedding = await _aITaskSystemSQL.ConvertingDataBaseTextToEmbadding(schema.ToString());
    //        return embedding;
    //    }




    //    // Confirm Code Start



    //    public async Task<bool> SaveToQdrantAsync(string objectName,string objectType,string text,float[] embedding)
    //    {
    //        if (objectName == null)
    //        {
    //            throw new ArgumentException("objectName is required.");
    //        }
    //        if (objectType == null)
    //        {
    //            throw new ArgumentException("objectType is required.");
    //        }
    //        if (text == null)
    //        {
    //            throw new ArgumentException("text is required.");
    //        }
    //        if (embedding == null || embedding.Length == 0)
    //        {
    //            throw new ArgumentException("Embedding is required.");
    //        }

    //        bool result = await _aITaskSystemSQL.SaveToQdrantAsync(objectName, objectType, text, embedding);

    //        return result;
    //    }
    //    // Confirm Code End
    //    public async Task<string> ExecuteQueryAsync(string sql)
    //    {
    //        if (string.IsNullOrWhiteSpace(sql))
    //        {
    //            throw new ArgumentException("SQL query is required.");
    //        }
    //        var result = await _aITaskSystemSQL.ExecuteQueryAsync(sql);
    //        return result;
    //    }
    //    public async Task<DataTable> GetDatabaseSchemaAsync()
    //    {
    //        var schema = await _aITaskSystemSQL.GetDatabaseSchemaAsync();
    //        return schema;
    //    }
   
       
      
    //    public async Task<float[]> GenerateQuestionEmbeddingAsyncSqlQuestion(string question)
    //    {
    //        if (string.IsNullOrWhiteSpace(question))
    //        {
    //            throw new ArgumentException("Question is required.");
    //        }
    //        var embedding = await _aITaskSystemSQL.GenerateEmbeddingDatabaseQuestionAsync(question);
    //        //  var length= embedding.Length;
    //        return embedding;
    //    }
    //    public async Task<string> SearchQdrantAsync(float[] embedding)
    //    {
    //        if (embedding == null || embedding.Length == 0)
    //        {
    //            throw new ArgumentException("Embedding is required.");
    //        }
    //        var result = await _aITaskSystemSQL.SearchQdrantAsync(embedding);
    //        return result;
    //    }
    //    public async Task<string> GenerateSqlAsync(string question, string schema)
    //    {
    //        if (string.IsNullOrWhiteSpace(question) || string.IsNullOrWhiteSpace(schema))
    //        {
    //            throw new ArgumentException("Question and schema are required.");
    //        }
    //        else
    //        {
    //            var result = await _aITaskSystemSQL.GenerateSqlAsync(question, schema);
    //            return result;
    //        }

    //    }
    //    public async Task<string> ClassifyQuestionAsync(string question, string schema)
    //    {
    //        if (string.IsNullOrWhiteSpace(question) || string.IsNullOrWhiteSpace(schema))
    //        {
    //            throw new ArgumentException("Question and schema are required.");
    //        }
    //        else
    //        {
    //            var result = await _aITaskSystemSQL.ClassifyQuestionAsync(question, schema);
    //            return result;
    //        }

    //    }
    //    public Dictionary<string, string> ConvertSchemaToTableTexts(DataTable schema)
    //    {
    //        if (schema == null)
    //        {
    //            throw new ArgumentException("Schema is required.");
    //        }
    //        var embedding = _aITaskSystemSQL.ConvertSchemaToTableTexts(schema);
    //        //  var length= embedding.Length;
    //        return embedding;
    //    }
    
    //}
}
