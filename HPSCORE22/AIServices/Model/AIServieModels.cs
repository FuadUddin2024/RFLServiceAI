using System.Text.Json.Serialization;

namespace CSWMS.AIServices.Model
{
    public class AIServieModels
    {
        // Represtents the complete Embedding response
        public class AIEmbeddingModel
        {
            [JsonPropertyName("embeddings")]
            public List<float[]> Embeddings { get; set; } = new();
        }
        // Represents the complete Qdrant response
        public class QdrantSearchResult
        {
            public string? Id { get; set; }

            public double Score { get; set; }

            public Dictionary<string, object>? Payload { get; set; }
        }
    }
}
