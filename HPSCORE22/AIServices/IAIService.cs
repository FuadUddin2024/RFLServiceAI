namespace CSWMS.AIServices
{
    public interface IAIService
    {
        Task<float[]> GenerateQuestionEmbeddingAsync(string question);
        Task<bool> SaveToQdrantAsync(string question, float[] embedding);
    }
}
