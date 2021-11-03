using Admission.Data.IRepository;
using Admission.Data.Models;
using Admission.Data.Models.Context;
using Admission.Data.SQLModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Admission.Data.Repository
{
    public class UniversityRepository : IUniversityRepository
    {
        private readonly AdmissionsDBContext _admissionsDBContext;

        public UniversityRepository(AdmissionsDBContext admissionsDBContext)
        {
            _admissionsDBContext = admissionsDBContext;
        }

        public UniversitySQL GetUniversity(int uniId, bool isShowAll)
        {
            //var university1 = _admissionsDBContext.Universities
            //    .Include(uni => uni.UniMajors)
            //    .ThenInclude(uniMajor => uniMajor.Major)
            //    .Include(uni => uni.UniAddresses)
            //    .ThenInclude(uniAddress => uniAddress.District)
            //    .Include(uni => uni.UniAdmissions)
            //    .ThenInclude(uniAdmissions => uniAdmissions.Admission)
            //    .Include(uni => uni.UniImages)
            //    .Where(uni => uni.Id == uniId)
            //    .FirstOrDefault();

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

            if (!isShowAll) university = university.Where(university => university.IsActive == true);

            return university.FirstOrDefault();
        }

        public Hashtable GetUniversities(int page, int limit, bool isShowAll)
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

            if (!isShowAll) universities = universities.Where(university => university.IsActive == true);

            int count = universities.Count();

            universities = universities.Skip((page - 1) * limit).Take(limit);

            if (universities != null && universities.Any())
            {
                Hashtable result = new();

                result.Add("universities", universities);
                result.Add("numPage", (int)Math.Ceiling((float)count / limit));
                return result;
            }

            return null;
        }



        public Task<bool> InsertUniversity(University university)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUniversity(University university)
        {
            throw new NotImplementedException();
        }
    }
}
