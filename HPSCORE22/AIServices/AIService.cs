namespace CSWMS.AIServices
{
    public class AIService: IAIService
    {
        public readonly IAITaskSystem _aITaskSystem;
        public AIService(IAITaskSystem aITaskSystem)
        {
            _aITaskSystem = aITaskSystem;
        }
        public async Task<float[]> GenerateQuestionEmbeddingAsync(string question)
        {
            if (string.IsNullOrWhiteSpace(question))
            {
                throw new ArgumentException("Question is required.");
            }
            var embedding = await _aITaskSystem.GenerateEmbeddingAsync(question);
          //  var length= embedding.Length;
            return embedding;
        }
        public async Task<bool> SaveToQdrantAsync(string question, float[] embedding)
        {
            if (string.IsNullOrWhiteSpace(question))
            {
                throw new ArgumentException("Question is required.");
            }

            if (embedding == null || embedding.Length == 0)
            {
                throw new ArgumentException("Embedding is required.");
            }

            bool result = await _aITaskSystem.SaveToQdrantAsync(question, embedding);

            return result;
        }
    }
}
