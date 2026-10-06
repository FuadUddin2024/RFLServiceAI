using CSWMS.AIServices.Model;
using CSWMS.Models;
using Microsoft.Data.SqlClient;
using QCMS.Services;
using Qdrant.Client;
using System.Data;
using System.Net.Http;
using System.Text;
namespace CSWMS.AIServices.Extracode
{
    //public interface IAITaskSQL
    //{
    //    // Confirmed Code start
    //    public string ConvertDatabaseObjectToText(DatabaseObjectModel schema);
    //    Task<List<DatabaseObjectModel>> GetDatabaseObjectsAsync();
    //    Task<float[]> GenerateEmbeddingAsync(DatabaseObjectModel text);
    //    Task<bool> SaveToQdrantAsync(string objectName, string objectType, string text, float[] embedding);
    //    // Confirmed Code end

    //    Task<string> ExecuteQueryAsync(string sql);
    //    Task<DataTable> GetDatabaseSchemaAsync();
       
        
        
    //    Task<string> SearchQdrantAsync(float[] embedding);
    //    Task<float[]> GenerateEmbeddingDatabaseQuestionAsync(string text);
    //    Task<string> GenerateSqlAsync(string question, string schema);
    //    Task<string> ClassifyQuestionAsync(string question, string schema);
    //    public Dictionary<string, string> ConvertSchemaToTableTexts(DataTable schema);
        
    //}
    //public class AITask
    //{

    //    // Confirmed Code start
        
       

    //    public async Task<bool> SaveToQdrantAsync(string objectName, string objectType, string text, float[] embedding)
    //    {
    //        try
    //        {
    //            var pointId = Guid.NewGuid();

    //            var requestBody = new
    //            {
    //                points = new[]
    //                {
    //            new
    //            {
    //                id = pointId,
    //                vector = embedding,
    //                payload = new
    //                {
    //                    object_name = objectName,
    //                    object_type = objectType,
    //                    text
    //                }
    //            }
    //        }
    //            };

    //            var response = await _httpClient.PutAsJsonAsync(
    //                "http://localhost:6333/collections/database_schema/points",
    //                requestBody);

    //            if (!response.IsSuccessStatusCode)
    //            {
    //                return false;
    //            }

    //            return true;
    //        }
    //        catch
    //        {
    //            return false;
    //        }
    //    }
    //    // Confirmed Code end
        
        
    //    public async Task<string> ExecuteQueryAsync(string sql)
    //    {
    //        using (SqlConnection connection = _db.GetConnection())
    //        {
    //            await connection.OpenAsync();

    //            using (SqlCommand cmd = new SqlCommand(sql, connection))
    //            {
    //                var result = await cmd.ExecuteScalarAsync();

    //                return result?.ToString() ?? "";
    //            }
    //        }
    //    }
    //    public async Task<DataTable> GetDatabaseSchemaAsync()
    //    {
    //        using (SqlConnection connection = _db.GetConnection())
    //        {
    //            await connection.OpenAsync();

    //            string sql = @"
    //        SELECT
    //            TABLE_NAME,
    //            COLUMN_NAME,
    //            DATA_TYPE
    //        FROM INFORMATION_SCHEMA.COLUMNS
    //        ORDER BY TABLE_NAME, ORDINAL_POSITION";

    //            using (SqlCommand cmd = new SqlCommand(sql, connection))
    //            {
    //                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
    //                {
    //                    DataTable table = new DataTable();

    //                    table.Load(reader);

    //                    return table;
    //                }
    //            }
    //        }
    //    }
    //    public Dictionary<string, string> ConvertSchemaToTableTexts(DataTable schema)
    //    {
    //        var tableSchemas = new Dictionary<string, string>();

    //        foreach (DataRow row in schema.Rows)
    //        {
    //            string tableName = row["TABLE_NAME"]?.ToString() ?? "";
    //            string columnName = row["COLUMN_NAME"]?.ToString() ?? "";
    //            string dataType = row["DATA_TYPE"]?.ToString() ?? "";

    //            if (string.IsNullOrWhiteSpace(tableName))
    //            {
    //                continue;
    //            }

    //            if (!tableSchemas.ContainsKey(tableName))
    //            {
    //                tableSchemas[tableName] =
    //                    $"Table: {tableName}\nColumns:\n";
    //            }

    //            tableSchemas[tableName] +=
    //                $"- {columnName} ({dataType})\n";
    //        }

    //        return tableSchemas;
    //    }

        

    //    //public string ConvertSchemaToText(DataTable schema)
    //    //{
    //    //    StringBuilder sb = new StringBuilder();

    //    //    string currentTable = "";

    //    //    foreach (DataRow row in schema.Rows)
    //    //    {
    //    //        string tableName = row["TABLE_NAME"].ToString();
    //    //        string columnName = row["COLUMN_NAME"].ToString();
    //    //        string dataType = row["DATA_TYPE"].ToString();

    //    //        if (currentTable != tableName)
    //    //        {
    //    //            currentTable = tableName;

    //    //            sb.AppendLine();
    //    //            sb.AppendLine($"Table: {tableName}");
    //    //            sb.AppendLine("Columns:");
    //    //        }

    //    //        sb.AppendLine($"- {columnName} ({dataType})");
    //    //    }

    //    //    return sb.ToString();
    //    //}
    //    public async Task<float[]> GenerateEmbeddingDatabaseQuestionAsync(string text)
    //    {
    //        var requestBody = new
    //        {
    //            input = text,
    //            model = "bge-m3"
    //        };
    //        var response = await _httpClient.PostAsJsonAsync("http://localhost:11434/api/embed", requestBody);
    //        response.EnsureSuccessStatusCode();
    //        var responseData = await response.Content.ReadFromJsonAsync<AIEmbeddingModel>();
    //        return responseData?.Embeddings?.FirstOrDefault()
    //               ?? Array.Empty<float>();
    //    }
    //    public async Task<string> SearchQdrantAsync(float[] embedding)
    //    {
    //        if (embedding == null || embedding.Length == 0)
    //        {
    //            throw new ArgumentException("Embedding is required.");
    //        }
    //        var requestBody = new
    //        {
    //            vector = embedding,
    //            limit = 2,
    //            with_payload = true
    //        };

    //        var response = await _httpClient.PostAsJsonAsync(
    //            "http://localhost:6333/collections/database_schema/points/search",
    //            requestBody);

    //        response.EnsureSuccessStatusCode();

    //        var result = await response.Content.ReadAsStringAsync();

    //        return result;
    //    }
    //    public async Task<string> GenerateSqlAsync(string question, string schema)
    //    {
    //        if (string.IsNullOrWhiteSpace(question))
    //        {
    //            throw new ArgumentException("Question is required.");
    //        }

    //        if (string.IsNullOrWhiteSpace(schema))
    //        {
    //            throw new ArgumentException("Schema is required.");
    //        }

    //        var prompt = $"""
    //                You are a SQL Server expert.

    //                Generate a SQL query based only on the database schema provided below.

    //                User Question:
    //                {question}

    //                Database Schema:
    //                {schema}

    //                Rules:
    //                - Generate SQL Server syntax.
    //                - Use only tables and columns that exist in the provided schema.
    //                - Do not explain the SQL.
    //                - Return only the SQL query.
    //                """;

    //        var requestBody = new
    //        {
    //            model = "qwen2.5:3b",
    //            prompt,
    //            stream = false
    //        };

    //        var response = await _httpClient.PostAsJsonAsync(
    //            "http://localhost:11434/api/generate",
    //            requestBody);

    //        response.EnsureSuccessStatusCode();

    //        var result =
    //            await response.Content.ReadFromJsonAsync<OllamaGenerateResponse>();

    //        return result?.Response?.Trim() ?? string.Empty;
    //    }
    //    public async Task<string> ClassifyQuestionAsync(string question,string schema)
    //    {
    //        if (string.IsNullOrWhiteSpace(question))
    //        {
    //            throw new ArgumentException("Question is required.");
    //        }

    //        if (string.IsNullOrWhiteSpace(schema))
    //        {
    //            throw new ArgumentException("Schema is required.");
    //        }
    //        var prompt = $@"
    //            You analyze database questions.

    //            Question:
    //            {question}

    //            Database Schema:
    //            {schema}

    //            Return ONLY JSON.

    //            {{
    //                ""understanding"": ""Explain what the user wants"",
    //                ""relevant_tables"": [""table names""],
    //                ""relevant_columns"": [""column names""],
    //                ""conditions"": [""conditions from the question""],
    //                ""requested_result"": ""describe the requested result""
    //            }}

    //            Important:
    //            - Fill the JSON with information from the question.
    //            - Use only tables and columns from the schema.
    //            - Do not invent tables or columns.
    //            - Do not return empty values when the question and schema provide the information.
    //            - Do not explain anything outside JSON.
    //            ";

    //        var requestBody = new
    //        {
    //            model = "qwen2.5:3b",
    //            prompt,
    //            stream = false
    //        };

    //        var response = await _httpClient.PostAsJsonAsync(
    //            "http://localhost:11434/api/generate",
    //            requestBody);

    //        response.EnsureSuccessStatusCode();

    //        var result =
    //            await response.Content.ReadFromJsonAsync<OllamaGenerateResponse>();

    //        return result?.Response?.Trim() ?? string.Empty;
    //    }
       
    //}

    //public class AIRepository : IAIRepository
    //{
    //    private readonly DatabaseService _db;
    //    private readonly HttpClient _httpClient;
    //    private readonly QdrantClient _qdrantClient;
    //    public AIRepository(DatabaseService db, HttpClient httpClient, QdrantClient qdrantClient)
    //    {
    //        _db = db;
    //        _httpClient = httpClient;
    //        _qdrantClient = qdrantClient;
    //    }

    //    /// <summary>
    //    /// Making Database Teaching My System and Ambedding and Generating Vector and Saving to Qdrant
    //    /// </summary>
    //    // Fetching Database Schema and Teaching My System 
    //    public async Task<List<DatabaseObjectModel>> GetDataBaseSchema()
    //    {
    //        var objects = new List<DatabaseObjectModel>();

    //        using (SqlConnection connection = _db.GetConnection())
    //        {
    //            await connection.OpenAsync();

    //            string sql = @"
    //        SELECT
    //            o.object_id,
    //            o.name AS ObjectName,

    //            CASE
    //                WHEN o.type = 'U' THEN 'TABLE'
    //                WHEN o.type = 'V' THEN 'VIEW'
    //                WHEN o.type = 'P' THEN 'PROCEDURE'
    //                WHEN o.type = 'FN' THEN 'FUNCTION'
    //                WHEN o.type = 'IF' THEN 'FUNCTION'
    //                WHEN o.type = 'TF' THEN 'FUNCTION'
    //            END AS ObjectType,

    //            c.name AS ColumnName,
    //            t.name AS DataType,

    //            m.definition AS SqlDefinition

    //        FROM sys.objects o

    //        LEFT JOIN sys.columns c
    //            ON o.object_id = c.object_id

    //        LEFT JOIN sys.types t
    //            ON c.user_type_id = t.user_type_id

    //        LEFT JOIN sys.sql_modules m
    //            ON o.object_id = m.object_id

    //        WHERE o.type IN ('U', 'V', 'P', 'FN', 'IF', 'TF')
    //          AND o.is_ms_shipped = 0

    //        ORDER BY
    //            o.name,
    //            c.column_id;
    //    ";

    //            using (SqlCommand cmd = new SqlCommand(sql, connection))
    //            {
    //                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
    //                {
    //                    var objectDictionary =
    //                        new Dictionary<int, DatabaseObjectModel>();

    //                    while (await reader.ReadAsync())
    //                    {
    //                        int objectId =
    //                            Convert.ToInt32(reader["object_id"]);

    //                        string objectName =
    //                            reader["ObjectName"]?.ToString() ?? "";

    //                        string objectType =
    //                            reader["ObjectType"]?.ToString() ?? "";

    //                        string columnName =
    //                            reader["ColumnName"]?.ToString() ?? "";

    //                        string dataType =
    //                            reader["DataType"]?.ToString() ?? "";

    //                        string sqlDefinition =
    //                            reader["SqlDefinition"]?.ToString() ?? "";

    //                        // Create object only once
    //                        if (!objectDictionary.ContainsKey(objectId))
    //                        {
    //                            objectDictionary[objectId] =
    //                                new DatabaseObjectModel
    //                                {
    //                                    ObjectName = objectName,
    //                                    ObjectType = objectType,
    //                                    Definition = ""
    //                                };
    //                        }

    //                        var currentObject =
    //                            objectDictionary[objectId];

    //                        // TABLE / VIEW
    //                        if (objectType == "TABLE" ||
    //                            objectType == "VIEW")
    //                        {
    //                            if (currentObject.Definition == "")
    //                            {
    //                                currentObject.Definition =
    //                                    $"Object: {objectName}\n" +
    //                                    $"Type: {objectType}\n" +
    //                                    "Columns:\n";
    //                            }

    //                            if (!string.IsNullOrWhiteSpace(columnName))
    //                            {
    //                                currentObject.Definition +=
    //                                    $"- {columnName} ({dataType})\n";
    //                            }
    //                        }

    //                        // PROCEDURE / FUNCTION
    //                        else if (objectType == "PROCEDURE" ||
    //                                 objectType == "FUNCTION")
    //                        {
    //                            currentObject.Definition =
    //                                sqlDefinition;
    //                        }
    //                    }

    //                    objects = objectDictionary.Values.ToList();
    //                }
    //            }
    //        }

    //        return objects;
    //    }
    //    public string ConvertDatabaseObjectToString(DatabaseObjectModel databaseObject)
    //    {
    //        if (databaseObject == null)
    //        {
    //            return string.Empty;
    //        }

    //        return
    //            $"Object Name: {databaseObject.ObjectName}\n" +
    //            $"Object Type: {databaseObject.ObjectType}\n\n" +
    //            $"Definition:\n" +
    //            $"{databaseObject.Definition}";
    //    }
    //    public async Task<float[]> ConvertingDataBaseTextToEmbadding(string text)
    //    {
    //        var requestBody = new
    //        {
    //            input = text,
    //            model = "bge-m3"
    //        };
    //        var response = await _httpClient.PostAsJsonAsync("http://localhost:11434/api/embed", requestBody);
    //        response.EnsureSuccessStatusCode();
    //        var responseData = await response.Content.ReadFromJsonAsync<AIEmbeddingModel>();
    //        return responseData?.Embeddings?.FirstOrDefault()
    //               ?? Array.Empty<float>();
    //    }
    //    // Making Database Teaching My System and Ambedding and Generating Vector and Saving to Qdrant END
    //}
}
