using Admission.Bussiness.Request;
using Admission.Data.Models;
using Admission.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.Service
{
    public interface IUniAddressService
    {
        UniAddress GetUniAddress(int uniAddressId);
        Task<bool> CreateUniAddress(CreateUniAddress createUniAddress);
        Task<bool> UpdateUniAddress(UpdateUniAddress updateUniAddress);
        Task<bool> DeleteUniAddress(UniAddress uniAddress);
    }
    public class UniAddressService : IUniAddressService
    {
        private readonly IUniAddressRepository _iUniAddressRepository;

        public UniAddressService(IUniAddressRepository iUniAddressRepository)
        {
            _iUniAddressRepository = iUniAddressRepository;
        }

        public UniAddress GetUniAddress(int uniAddressId)
        {
            return _iUniAddressRepository.GetUniAddress(uniAddressId);
        }

        public async Task<bool> CreateUniAddress(CreateUniAddress createUniAddress)
        {
            var uniAddress = new UniAddress()
            {
                Address = createUniAddress.Address,
                UniversityId = createUniAddress.UniversityId,
                DistrictId = createUniAddress.DistrictId
            };

            return await _iUniAddressRepository.InsertUniAddress(uniAddress);
        }

        public async Task<bool> UpdateUniAddress(UpdateUniAddress updateUniAddress)
        {
            var uniAddress = _iUniAddressRepository.GetUniAddress(updateUniAddress.Id);
            uniAddress.Address = updateUniAddress.Address;
            uniAddress.DistrictId = updateUniAddress.DistrictId;

            return await _iUniAddressRepository.UpdateUniAddress(uniAddress);
        }

        public async Task<bool> DeleteUniAddress(UniAddress uniAddress)
        {
            return await _iUniAddressRepository.DeleteUniAddress(uniAddress);
        }
    }
}
