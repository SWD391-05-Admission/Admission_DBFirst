using Admission.Data.Models;
using Admission.Data.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Data.Repository
{
    public interface IUniAddressRepository
    {
        UniAddress GetUniAddress(int uniAddressId);
        Task<bool> InsertUniAddress(UniAddress uniAddress);
        Task<bool> UpdateUniAddress(UniAddress newUniAddress);
        Task<bool> DeleteUniAddress(UniAddress uniAddress);
    }
    public class UniAddressRepository : IUniAddressRepository
    {
        private readonly AdmissionsDBContext _admissionsDBContext;

        public UniAddressRepository(AdmissionsDBContext admissionsDBContext)
        {
            _admissionsDBContext = admissionsDBContext;
        }

        public UniAddress GetUniAddress(int uniAddressId)
        {
            return _admissionsDBContext.UniAddresses.Where(uniAddress => uniAddress.Id == uniAddressId).FirstOrDefault();
        }

        public async Task<bool> InsertUniAddress(UniAddress uniAddress)
        {
            if (uniAddress == null) return false;
            await _admissionsDBContext.UniAddresses.AddAsync(uniAddress);
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUniAddress(UniAddress newUniAddress)
        {
            if (newUniAddress == null) return false;
            var uniAdress = _admissionsDBContext.UniAddresses.Where(uniAddress => uniAddress.Id == newUniAddress.Id).FirstOrDefault();
            if (uniAdress == null) return false;
            uniAdress = newUniAddress;
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUniAddress(UniAddress uniAddress)
        {
            _admissionsDBContext.UniAddresses.Remove(uniAddress);
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }
    }
}
