using System.Text.Json.Serialization;

namespace CSWMS.AIServices.Model
{



    public class AIServiceModel
    {
        public string? Question { get; set; }
        public string? Answer { get; set; }
    }

    public class AIEmbeddingModel
    {
        [JsonPropertyName("embeddings")]
        public List<float[]> Embeddings { get; set; } = new();
    }

    // Represents the complete Qdrant response
    public class QdrantSearchResponse
    {
        [JsonPropertyName("result")]
        public List<QdrantSearchResult> Result { get; set; } = new();
    }

    // Represents one item inside result[]
    public class QdrantSearchResult
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("score")]
        public float Score { get; set; }

        [JsonPropertyName("payload")]
        public QdrantPayload Payload { get; set; }
    }

    public class QdrantPayload
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
    public class OllamaGenerateResponse
    {
        [JsonPropertyName("response")]
        public string? Response { get; set; }
    }
    public class DatabaseObjectModel
    {
        public string ObjectName { get; set; }
        public string ObjectType { get; set; }
        public string Definition { get; set; }
    }
}