namespace CSWMS.Models
{
    public class SubProblemModel
    {
        public int SubProbId { get; set; }
        public int problemId { get; set; }
        public string? SubProblemName { get; set; }
        public string? EntryBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? EntryPC { get; set; }
        public string? ModifiedPC { get; set; }
        public string? Type { get; set; }
        public string? Status { get; set; }
        public string? ProblemType { get; set; }
    }
}
