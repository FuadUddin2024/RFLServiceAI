namespace QCMS.Models
{
        public class User
        {
            public string? USER_TEXT { get; set; }
            public string? USER_NAME { get; set; }
            public string? PASSWORD { get; set; }

            // DB stores 'Y'/'N', so keep as string
            public string? ACTIVE { get; set; }
            public string? USER_REAU { get; set; }
            public string? USER_ISAU { get; set; }
            public string? USER_MRAU { get; set; }
            public string? USER_POAU { get; set; }
            public string? USER_PRAU { get; set; }
            public string? HR_ACTIVE { get; set; }
            public string? HR_ACTIVE1 { get; set; }
            public string? ACCEPTED { get; set; } // if stored as Y/N
            public string? USER_STAT { get; set; } // keep string if not strictly bool

            // Other fields
            public string? IUSER { get; set; }
            public string? EUSER { get; set; }
            public DateTime? IDAT { get; set; }
            public DateTime? EDAT { get; set; }
            public int VER { get; set; }
            public DateTime? UDT { get; set; }
            public string? SITE_ID { get; set; }
            public string? EMAIL_ADD { get; set; }
            public string? MOBILE_NO { get; set; }
            public string? USER_TYPE { get; set; }
            public string? USER_SUSR { get; set; }
            public string? USER_GUSR { get; set; }
            public string? USER_QUSR { get; set; }
            public string? SOFT_VERSION { get; set; }
            public string? HR_ID { get; set; }
            public string? ASSIGN_BU { get; set; }

            // Convenience properties to check Y/N as bool
            public bool IsActive => ACTIVE == "Y";
            public bool IsUSER_REAU => USER_REAU == "Y";
            public bool IsUSER_ISAU => USER_ISAU == "Y";
            public bool IsUSER_MRAU => USER_MRAU == "Y";
            public bool IsUSER_POAU => USER_POAU == "Y";
            public bool IsUSER_PRAU => USER_PRAU == "Y";
            public bool IsHR_ACTIVE => HR_ACTIVE == "Y";
            public bool IsHR_ACTIVE1 => HR_ACTIVE1 == "Y";
            public bool IsAccepted => ACCEPTED == "Y";
    }

    /// <summary>
    /// Request model for login
    /// </summary>
    public class LoginRequest
    {
        public string UserText { get; set; } = string.Empty;
        public string UserPass { get; set; } = string.Empty;
        public string StaffID { get; set; } = string.Empty;
        public string HRISPassword { get; set; } = string.Empty;
    }
    public class UserSessionModel
    {
        public string? USERID { get; set; }
        public string? USERNAME { get; set; }
        public string? USERTYPEID { get; set; }
        public int CompanyID { get; set; }
        public int ZoneID { get; set; }


        public string? USER_TEXT { get; set; }
        public string? USER_NAME { get; set; }
        public string? USER_TYPE { get; set; }

        public string? DUSR_WHID { get; set; }
        public string? DUSR_ALLP { get; set; }
        public string? DUSR_BUSN { get; set; }
        public string? DUSR_DIST { get; set; }
        public string? DUSR_SPTY { get; set; }
        public string? DUSR_COMN { get; set; }

        public string? DIST_NAME { get; set; }
        public string? INFO_MVRS { get; set; }
        public int INFO_OID { get; set; }
        public string? INFO_TEXT { get; set; }
        public string? INFO_DESC { get; set; }
        public string? INFO_WELCOME_MSG { get; set; }

        public string? SOFT_ON { get; set; }
        public string? HRIS_CHECK { get; set; }

        // Helper properties
        public bool IsSoftOn => SOFT_ON == "Y";
        public bool IsHrisEnabled => HRIS_CHECK == "Y";
    }

    public class HrisData
    {
        public string stafF_ID { get; set; } = string.Empty;
        public string stafF_NAME { get; set; } = string.Empty;
        public string designation { get; set; } = string.Empty;
    }

    public class HrisResponse
    {
        public string succesS_CODE { get; set; } = string.Empty;
        public string succesS_MESSAGE { get; set; } = string.Empty;
        public HrisData? data { get; set; }
    }

    // Newly Added Code 5/7/26
    public class UserProfile
    {
        public int UserCode { get; set; }
        public string? BU_Code { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public int StaffID { get; set; }
        public int UserTypeId { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? ContactNo { get; set; }
        public string? Address { get; set; }
        public int CompanyId { get; set; }
        public int DepartmentId { get; set; }
        public int DesignationId { get; set; }
        public int ZoneId { get; set; }
        public int DepotId { get; set; }
        public bool DepoAct { get; set; }
        public bool IsNew { get; set; }
        public bool IsActive { get; set; }
        public string? ServiceUser { get; set; }
        public string? EntryBy { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string? EntryPC { get; set; }
        public string? ModifiedPC { get; set; }
        public bool MailPermission { get; set; }
        public bool OfferPer { get; set; }
        public bool VigoPer { get; set; }
        public bool MenuPer { get; set; }

    }

}
