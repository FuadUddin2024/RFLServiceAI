using CSWMS.AIServices.Model;

namespace CSWMS.AIServices.Repository
{
    public interface IAIServiceRepository
    {
        Task<float[]> GenerateEmbeddingQuestionAsync(string text);
    }
    public class AIServiceRepository : IAIServiceRepository
    {
        private readonly HttpClient _httpClient;
        public AIServiceRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        // Making Embedding Model : bge-m3 for User Question and also returning the Embedding Vector as float array
        public async Task<float[]> GenerateEmbeddingQuestionAsync(string text)
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

    }
}
