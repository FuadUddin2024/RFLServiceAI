using CSWMS.Models;

namespace CSWMS.Interface
{
    public interface IComplainListToken
    {
        public List<ComplainModel> GetALlComplainListForAssing();

        List<ComplainModel> GetAllComplainList(string ticketCode);

        bool UpdateComplain(ComplainModel complainModel);
    }
}

