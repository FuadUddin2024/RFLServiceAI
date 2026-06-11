using QCMS.Models;
using QCMS.Repositories;

namespace QCMS.Services
{
    public class QCLocationService
    {
        private readonly QCLocationRepository _repo;

        public QCLocationService(QCLocationRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<QC_Location>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<QC_Location?> GetByIdAsync(string id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<(bool success, string message)> SaveAsync(QC_Location model, string user)
        {
            if (string.IsNullOrWhiteSpace(model.LOCATION_ID))
                return (false, "Location ID required");

            if (string.IsNullOrWhiteSpace(model.COMPANY_ID))
                return (false, "Company required");

            if (string.IsNullOrWhiteSpace(model.LOCATION_NAME))
                return (false, "Location name required");

            //  NEW CHECK (IMPORTANT)
            var nameExists = await _repo.NameExistsAsync(model.LOCATION_NAME, model.LOCATION_ID);

            if (nameExists)
                return (false, "Location name already exists");

            var exists = await _repo.ExistsAsync(model.LOCATION_ID);

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

        public async Task<bool> DeleteAsync(string id)
        {
            return await _repo.DeleteAsync(id) > 0;
        }
    }
}