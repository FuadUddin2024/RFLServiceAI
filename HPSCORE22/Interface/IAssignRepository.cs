using CSWMS.Models;

namespace CSWMS.Interface
{
    public interface IAssignRepository
    {
        public List<AssignModel> GetAllComplainList(string ticketCode);
    }
}
