using CSWMS.AIServices.Model;
using Qdrant.Client;
namespace CSWMS.AIServices
{
    public interface IAITaskSystem
    {
        Task<float[]> GenerateEmbeddingAsync(string text);
        Task<bool> SaveToQdrantAsync(string question, float[] embedding);
    }
    public class AITaskSystem : IAITaskSystem
    {
        private readonly HttpClient _httpClient;
        private readonly QdrantClient _qdrantClient;
        public AITaskSystem(HttpClient httpClient, QdrantClient qdrantClient)
        {
            _httpClient = httpClient;
            _qdrantClient = qdrantClient;
        }
        public async Task<float[]> GenerateEmbeddingAsync(string text)
        {
            var requestBody = new
            {
                input = text,
                model = "bge-m3"
            };
            var response = await _httpClient.PostAsJsonAsync("http://localhost:11434/api/embed", requestBody);
            response.EnsureSuccessStatusCode();
            var responseData = await response.Content.ReadFromJsonAsync<AIEmbeddingModel>();
            return responseData?.Embeddings?.FirstOrDefault()
                   ?? Array.Empty<float>();
        }
        public async Task<bool> SaveToQdrantAsync(string question, float[] embedding)
        {
            try
            {
                var pointId = Guid.NewGuid();

                var requestBody = new
                {
                    points = new[]
                    {
                new
                {
                    id = pointId,
                    vector = embedding,
                    payload = new
                    {
                        text = question
                    }
                }
            }
                };
                var response = await _httpClient.PutAsJsonAsync("http://localhost:6333/collections/rag_documents/points",requestBody);
                if (!response.IsSuccessStatusCode)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    }
