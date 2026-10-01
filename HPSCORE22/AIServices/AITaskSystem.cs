namespace CSWMS.AIServices
{
    public interface IAITaskSystem
    {
        Task<float[]> GenerateEmbeddingAsync(string text);
    }
    public class AITaskSystem: IAITaskSystem
    {
        private readonly HttpClient _httpClient;
        public AITaskSystem(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<float[]> GenerateEmbeddingAsync(string text)
        {
            var requestBody = new
            {
                input = text,
                model = "bge-m3"
            };
            var response = await _httpClient.PostAsJsonAsync( "http://localhost:11434/api/embed",requestBody);
            response.EnsureSuccessStatusCode();
            var responseData =await response.Content.ReadFromJsonAsync<AIEmbeddingModel>();
            return responseData?.Embeddings?.FirstOrDefault()
                   ?? Array.Empty<float>();
        }
    }
}
