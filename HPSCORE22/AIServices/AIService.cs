namespace CSWMS.AIServices
{
    public class AIService: IAIService
    {
        public readonly IAITaskSystem _aITaskSystem;
        public AIService(IAITaskSystem aITaskSystem)
        {
            _aITaskSystem = aITaskSystem;
        }
        public async Task<float[]> GetAnswerAsync(string question)
        {
            if (string.IsNullOrWhiteSpace(question))
            {
                throw new ArgumentException("Question is required.");
            }
            var embedding = await _aITaskSystem.GenerateEmbeddingAsync(question);
            return embedding;
        }
    }
}
