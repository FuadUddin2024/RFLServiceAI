namespace CSWMS.Models
{
    public class NatureofProblem
    {
        public int problemId { get; set; }
        public int ProblemCode { get; set; }
        public string? ProblemName { get; set; }
        public int ProductId { get; set; }
        public bool Active { get; set; }
        public string? EntryBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? EntryPC { get; set; }
        public string? ModifiedPC { get; set; }
    }
}
