using Microsoft.EntityFrameworkCore;
using QCMS.Models;
using QCMS.Repositories;

namespace QCMS.Services
{
    public class QCCompanyService
    {
        private readonly QCCompanyRepository _repo;

        public QCCompanyService(QCCompanyRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        // Get All
        public async Task<IEnumerable<QCCompany>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        // Get By ID
        public async Task<QCCompany?> GetByIdAsync(string id)
        {
            return await _repo.GetByIdAsync(id);
        }

        // Save (Insert or Update)
        public async Task<(bool success, string message)> SaveAsync(QCCompany model, string user)
        {
            if (string.IsNullOrWhiteSpace(model.COMPANY_ID))
                return (false, "Company ID is required");

            if (string.IsNullOrWhiteSpace(model.COMPANY_NAME))
                return (false, "Company Name is required");

            var exists = await _repo.ExistsAsync(model.COMPANY_ID);

            if (exists)
            {
                model.UPDATED_BY = user;
                await _repo.UpdateAsync(model);
                return (true, "Updated successfully");
            }
            else
            {
                model.CREATED_BY = user;
                model.IS_ACTIVE = 1;
                await _repo.InsertAsync(model);
                return (true, "Created successfully");
            }
        }

        // Delete
        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _repo.DeleteAsync(id);
            return result > 0;
        }

        //public async Task<string> GenerateCompanyIdAsync()
        //{
        //    return await _repo.GenerateCompanyIdAsync();
        //}
    }
}