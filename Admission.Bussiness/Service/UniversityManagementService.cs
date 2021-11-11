using Admission.Bussiness.Request;
using Admission.Data.Models;
using Admission.Data.Repository;
using Admission.Data.SQLModels;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace Admission.Bussiness.Service
{
    public interface IUniversityManagementService
    {
        University GetUniversity(string code);
        UniversitySQL GetUniversity(int uniId);
        Hashtable GetUniversities(SearchUniversity searchUniversity);
        Task<bool> CreateUniversity(CreateUniversity createUniversity);
        Task<bool> UpdateUniversity(UpdateUniversity updateUniversity);
    }

    public class UniversityManagementService : IUniversityManagementService
    {
        private readonly IUniversityRepository _iUniversityRepository;

        public UniversityManagementService(IUniversityRepository iUniversityRepository)
        {
            _iUniversityRepository = iUniversityRepository;
        }

        public University GetUniversity(string code)
        {
            return _iUniversityRepository.GetUniversity(code);
        }

        public UniversitySQL GetUniversity(int uniId)
        {
            return _iUniversityRepository.GetUniversity(uniId, null);
        }

        public Hashtable GetUniversities(SearchUniversity searchUniversity)
        {
            return _iUniversityRepository.GetUniversities(searchUniversity.Page, searchUniversity.Limit, null);
        }

        public Task<bool> CreateUniversity(CreateUniversity createUniversity)
        {
            var university = new University()
            {
                Code = createUniversity.Code.Trim().ToUpper(),
                Name = createUniversity.Name,
                Email = createUniversity.Email,
                Facebook = createUniversity.Facebook,
                Website = createUniversity.Website,
                Description = createUniversity.Description,
                LastYearBenchmark = createUniversity.LastYearBenchmark,
                MinFee = createUniversity.MinFee,
                MaxFee = createUniversity.MaxFee,
                IsActive = true
            };

            return _iUniversityRepository.InsertUniversity(university);
        }

        public async Task<bool> UpdateUniversity(UpdateUniversity updateUniversity)
        {
            University newUniversity = _iUniversityRepository.GetUniversity(updateUniversity.Id);

            newUniversity.Code = updateUniversity.Code.Trim().ToUpper();
            newUniversity.Name = updateUniversity.Name;
            newUniversity.Email = updateUniversity.Email;
            newUniversity.Facebook = updateUniversity.Facebook;
            newUniversity.Website = updateUniversity.Website;
            newUniversity.Description = updateUniversity.Description;
            newUniversity.LastYearBenchmark = updateUniversity.LastYearBenchmark;
            newUniversity.MinFee = updateUniversity.MinFee;
            newUniversity.MaxFee = updateUniversity.MaxFee;
            newUniversity.IsActive = updateUniversity.IsActive;

            return await _iUniversityRepository.UpdateUniversity(newUniversity);
        }
    }
}
