using QCMS.Models;

namespace CSWMS.CommonMethod
{
    public class SessionHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public SessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public UserSessionModel GetUser()
        {
            var session = _httpContextAccessor.HttpContext.Session;

            return new UserSessionModel
            {
                USERID = session.GetString("USERID"),
                USERNAME = session.GetString("USERNAME"),
                CompanyID = session.GetInt32("CompanyID") ?? 0,
                ZoneID = session.GetInt32("ZoneID") ?? 0,
                USERTYPEID = session.GetString("USERTYPEID"),
                USER_TEXT = session.GetString("USER_TEXT"),
                USER_NAME = session.GetString("USER_NAME"),
                USER_TYPE = session.GetString("USER_TYPE")
            };
        }
    }
    public class CommonMethods
    {
    }
}
