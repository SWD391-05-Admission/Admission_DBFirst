using Admission.Data.Models;
using Admission.Data.Models.Context;
using Admission.Data.SQLModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Admission.Data.Repository
{
    public interface IUniversityRepository
    {
        University GetUniversity(string code);
        UniversitySQL GetUniversity(int uniId, bool? isActive);
        Hashtable GetUniversities(int page, int limit, bool? isActive);
        Task<bool> InsertUniversity(University university);
        Task<bool> UpdateUniversity(University newUniversity);
    }

    public class UniversityRepository : IUniversityRepository
    {
        private readonly AdmissionsDBContext _admissionsDBContext;

        public UniversityRepository(AdmissionsDBContext admissionsDBContext)
        {
            _admissionsDBContext = admissionsDBContext;
        }

        University IUniversityRepository.GetUniversity(string code)
        {
            return _admissionsDBContext.Universities.Where(uni => uni.Code.Equals(code.ToUpper())).FirstOrDefault();
        }

        public UniversitySQL GetUniversity(int uniId, bool? isActive)
        {
            var university = _admissionsDBContext.Universities
                .Where(uni => uni.Id == uniId)
                .Select(uni => new UniversitySQL
                {
                    Id = uni.Id,
                    Code = uni.Code,
                    Name = uni.Name,
                    Email = uni.Email,
                    Facebook = uni.Facebook,
                    Website = uni.Website,
                    Description = uni.Description,
                    LastYearBenchmark = uni.LastYearBenchmark,
                    MinFee = uni.MinFee,
                    MaxFee = uni.MaxFee,
                    IsActive = uni.IsActive,
                    Addresses = (IEnumerable<UniAddressSQL>)_admissionsDBContext.UniAddresses
                    .Where(uniAdd => uniAdd.UniversityId == uni.Id)
                    .Select(uniAdd => new UniAddressSQL
                    {
                        Id = uniAdd.Id,
                        Address = uniAdd.Address,
                        District = _admissionsDBContext.Districts
                        .Where(district => district.Id == uniAdd.DistrictId)
                        .Select(district => new DistrictSQL
                        {
                            Id = district.Id,
                            Name = district.Name
                        }).FirstOrDefault()
                    }),
                    Admissions = (IEnumerable<UniAdmissionSQL>)_admissionsDBContext.UniAdmissions
                    .Where(uniAdmission => uniAdmission.UniversityId == uni.Id)
                    .Select(uniAdmission => new UniAdmissionSQL
                    {
                        Id = uniAdmission.Id,
                        Admission = _admissionsDBContext.AdmissionForms
                        .Where(admission => admission.Id == uniAdmission.AdmissionId)
                        .Select(admission => new AdmissionFormSQL
                        {
                            Id = admission.Id,
                            Method = admission.Method,
                            Description = admission.Description
                        }).FirstOrDefault()
                    }),
                    Images = (IEnumerable<UniImageSQL>)_admissionsDBContext.UniImages
                    .Where(uniImage => uniImage.UniversityId == uni.Id)
                    .Select(uniImage => new UniImageSQL
                    {
                        Id = uniImage.Id,
                        Src = uniImage.Src,
                        Alt = uniImage.Alt,
                        IsLogo = uniImage.IsLogo
                    }),
                    Majors = (IEnumerable<UniMajorSQL>)_admissionsDBContext.UniMajors
                    .Where(uniMajor => uniMajor.UniversityId == uni.Id)
                    .Select(uniMajor => new UniMajorSQL
                    {
                        Id = uniMajor.Id,
                        Major = _admissionsDBContext.Majors
                        .Where(major => major.Id == uniMajor.MajorId)
                        .Select(major => new MajorSQL
                        {
                            Id = major.Id,
                            Name = major.Name,
                            Description = major.Description
                        }).FirstOrDefault()
                    }),

                });

            if (isActive != null) university = university.Where(university => university.IsActive == isActive);

            return university.FirstOrDefault();
        }

        public Hashtable GetUniversities(int page, int limit, bool? isActive)
        {
            var universities = _admissionsDBContext.Universities
                .Select(uni => new UniversitySQL
                {
                    Id = uni.Id,
                    Code = uni.Code,
                    Name = uni.Name,
                    Email = uni.Email,
                    Facebook = uni.Facebook,
                    Website = uni.Website,
                    Description = uni.Description,
                    LastYearBenchmark = uni.LastYearBenchmark,
                    MinFee = uni.MinFee,
                    MaxFee = uni.MaxFee,
                    IsActive = uni.IsActive,
                    Addresses = (IEnumerable<UniAddressSQL>)_admissionsDBContext.UniAddresses
                    .Where(uniAdd => uniAdd.UniversityId == uni.Id)
                    .Select(uniAdd => new UniAddressSQL
                    {
                        Id = uniAdd.Id,
                        Address = uniAdd.Address,
                        District = _admissionsDBContext.Districts
                        .Where(district => district.Id == uniAdd.DistrictId)
                        .Select(district => new DistrictSQL
                        {
                            Id = district.Id,
                            Name = district.Name
                        }).FirstOrDefault()
                    }),
                    Admissions = (IEnumerable<UniAdmissionSQL>)_admissionsDBContext.UniAdmissions
                    .Where(uniAdmission => uniAdmission.UniversityId == uni.Id)
                    .Select(uniAdmission => new UniAdmissionSQL
                    {
                        Id = uniAdmission.Id,
                        Admission = _admissionsDBContext.AdmissionForms
                        .Where(admission => admission.Id == uniAdmission.AdmissionId)
                        .Select(admission => new AdmissionFormSQL
                        {
                            Id = admission.Id,
                            Method = admission.Method,
                            Description = admission.Description
                        }).FirstOrDefault()
                    }),
                    Images = (IEnumerable<UniImageSQL>)_admissionsDBContext.UniImages
                    .Where(uniImage => uniImage.UniversityId == uni.Id)
                    .Select(uniImage => new UniImageSQL
                    {
                        Id = uniImage.Id,
                        Src = uniImage.Src,
                        Alt = uniImage.Alt,
                        IsLogo = uniImage.IsLogo
                    }),
                    Majors = (IEnumerable<UniMajorSQL>)_admissionsDBContext.UniMajors
                    .Where(uniMajor => uniMajor.UniversityId == uni.Id)
                    .Select(uniMajor => new UniMajorSQL
                    {
                        Id = uniMajor.Id,
                        Major = _admissionsDBContext.Majors
                        .Where(major => major.Id == uniMajor.MajorId)
                        .Select(major => new MajorSQL
                        {
                            Id = major.Id,
                            Name = major.Name,
                            Description = major.Description
                        }).FirstOrDefault()
                    }),
                });

            //string where = "";

            //if (!string.IsNullOrEmpty(email))
            //{
            //    where += "Email.Contains(\"" + email + "\")";
            //}
            //if (!string.IsNullOrEmpty(fullname))
            //{
            //    if (!string.IsNullOrEmpty(where)) where += " OR ";
            //    where += "FullName.Contains(\"" + fullname + "\")";
            //}
            //if (!string.IsNullOrEmpty(phone))
            //{
            //    if (!string.IsNullOrEmpty(where)) where += " OR ";
            //    where += "Phone.Contains(\"" + phone + "\")";
            //}

            if (isActive != null) universities = universities.Where(university => university.IsActive == isActive);

            int count = universities.Count();

            if (universities != null && universities.Any())
            {
                Hashtable result = new();
                if (page > 0 && limit > 0)
                {
                    universities = universities.Skip((page - 1) * limit).Take(limit);
                    result.Add("numPage", (int)Math.Ceiling(((float)count / limit)));
                }
                result.Add("universities", universities);
                return result;
            }

            return null;
        }

        public async Task<bool> InsertUniversity(University university)
        {
            if (university == null) return false;
            await _admissionsDBContext.Universities.AddAsync(university);
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUniversity(University newUniversity)
        {
            if (newUniversity == null) return false;
            University university = _admissionsDBContext.Universities.Where(university => university.Id == newUniversity.Id).FirstOrDefault();
            if (university == null) return false;
            university = newUniversity;
            //if (isLoop) return true;
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }
    }
}
