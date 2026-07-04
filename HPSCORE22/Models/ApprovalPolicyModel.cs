namespace CSWMS.Models
{
    public class ApprovalPolicyModel
    {
        public int ApprovalPolicyId { get; set; }
        public string? ApprovalPolicyName { get; set; }
        public string? ApprovalPolicyDescription { get; set; }
        public bool IsActive { get; set; }
    }
}
