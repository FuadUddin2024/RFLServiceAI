using System.Text.Json.Serialization;

namespace CSWMS.AIServices
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
}
