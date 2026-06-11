using CSWMS.Models;

namespace CSWMS.Interface
{
    public interface IStatusRepository
    {
        public List<StatusModel> GetALLStatus();
    }
}
