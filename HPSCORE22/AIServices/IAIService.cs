namespace CSWMS.AIServices
{
    public interface IAIService
    {
        Task<float[]> GetAnswerAsync(string question);
    }
}
