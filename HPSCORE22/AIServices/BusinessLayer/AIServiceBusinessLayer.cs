using CSWMS.AIServices.Model;
using CSWMS.AIServices.Repository;

namespace CSWMS.AIServices.BusinessLayer
{
    public interface IAIServiceBusinessLayer
    {
        Task<float[]> GenerateQuestionEmbeddingAsync(string question);
    }
    public class AIServiceBusinessLayer : IAIServiceBusinessLayer
    {
        public readonly IAIServiceRepository _IAIServiceRepository;
        public AIServiceBusinessLayer(IAIServiceRepository IAIServiceRepository)
        {
            _IAIServiceRepository = IAIServiceRepository;
        }
        // Setup: Questions Embedding Generation
        public async Task<float[]> GenerateQuestionEmbeddingAsync(string question)
        {
            if (string.IsNullOrWhiteSpace(question))
            {
                throw new ArgumentException("Question is required.");
            }
            var embedding = await _IAIServiceRepository.GenerateEmbeddingQuestionAsync(question);
            //  var length= embedding.Length;
            return embedding;
        }
        public async Task<List<QdrantSearchResult>> SearchQdrantAsync(float[] questionVector)
        {
            if (questionVector == null || questionVector.Length == 0)
            {
                throw new ArgumentException("Question vector is required.");
            }
            var result = await _IAIServiceRepository.SearchQdrantAsync(questionVector);
            return result;
        }
    }
}
