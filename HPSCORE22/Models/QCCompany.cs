namespace QCMS.Models
{
    public class QCCompany
    {
        public string COMPANY_ID { get; set; }
        public string COMPANY_NAME { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public string CREATED_BY { get; set; }
        public DateTime? UPDATED_DATE { get; set; }
        public string UPDATED_BY { get; set; }
        public int IS_ACTIVE { get; set; }
    }
}
