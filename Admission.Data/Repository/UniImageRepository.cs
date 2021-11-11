using Admission.Data.Models;
using Admission.Data.Models.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Admission.Data.Repository
{
    public interface IUniImageRepository
    {
        bool GetUniImageLogo();
        UniImage GetUniImage(int uniImageId);
        Task<bool> InsertUniImage(UniImage uniImage);
        Task<bool> UpdateUniImage(UniImage newUniImage);
        Task<bool> DeleteUniImage(UniImage uniImage);
    }

    public class UniImageRepository : IUniImageRepository
    {
        private readonly AdmissionsDBContext _admissionsDBContext;

        public UniImageRepository(AdmissionsDBContext admissionsDBContext)
        {
            _admissionsDBContext = admissionsDBContext;
        }

        public bool GetUniImageLogo()
        {
            return _admissionsDBContext.UniImages.Where(uniImage => uniImage.IsLogo == true).Any();
        }

        public UniImage GetUniImage(int uniImageId)
        {
            return _admissionsDBContext.UniImages.Where(uniImage => uniImage.Id == uniImageId).FirstOrDefault();
        }

        public async Task<bool> InsertUniImage(UniImage uniImage)
        {
            if (uniImage == null) return false;
            await _admissionsDBContext.UniImages.AddAsync(uniImage);
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUniImage(UniImage newUniImage)
        {
            if (newUniImage == null) return false;
            var uniImage = _admissionsDBContext.UniImages.Where(uniImage => uniImage.Id == newUniImage.Id).FirstOrDefault();
            if (uniImage == null) return false;
            uniImage = newUniImage;
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUniImage(UniImage uniImage)
        {
            _admissionsDBContext.UniImages.Remove(uniImage);
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }
    }
}
