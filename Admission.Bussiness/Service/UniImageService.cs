using Admission.Bussiness.Request;
using Admission.Data.Models;
using Admission.Data.Repository;
using System;
using System.Threading.Tasks;

namespace Admission.Bussiness.Service
{
    public interface IUniImageService
    {
        bool GetUniImageLogo();
        UniImage GetUniImage(int uniAddressId);
        Task<bool> CreateUniImage(CreateUniImage createUniImage);
        Task<bool> UpdateUniImage(UpdateUniImage updateUniImage);
        Task<bool> DeleteUniImage(UniImage uniImage);
    }
    public class UniImageService : IUniImageService
    {
        private readonly IUniImageRepository _iUniImageRepository;

        public UniImageService(IUniImageRepository iUniImageRepository)
        {
            _iUniImageRepository = iUniImageRepository;
        }

        public bool GetUniImageLogo()
        {
            return _iUniImageRepository.GetUniImageLogo();
        }

        public UniImage GetUniImage(int uniAddressId)
        {
            return _iUniImageRepository.GetUniImage(uniAddressId);
        }

        public async Task<bool> CreateUniImage(CreateUniImage createUniImage)
        {
            var uniAddress = new UniImage()
            {
                Src = createUniImage.Src,
                Alt = createUniImage.Alt,
                UniversityId = createUniImage.UniversityId,
                IsLogo = false
            };

            return await _iUniImageRepository.InsertUniImage(uniAddress);
        }

        public async Task<bool> UpdateUniImage(UpdateUniImage updateUniImage)
        {
            var uniAddress = _iUniImageRepository.GetUniImage(updateUniImage.Id);
            uniAddress.Src = updateUniImage.Src;
            uniAddress.Alt = updateUniImage.Alt;
            uniAddress.IsLogo = updateUniImage.IsLogo;

            return await _iUniImageRepository.UpdateUniImage(uniAddress);
        }

        public async Task<bool> DeleteUniImage(UniImage uniImage)
        {
            return await _iUniImageRepository.DeleteUniImage(uniImage);
        }
    }
}
