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
    public interface ITalkshowRepository
    {
        Talkshow GetTalkshow(int? counselorId, int talkshowId);
        TalkshowSQL GetTalkshowSQL(int? counselorId, int talkshowId
            , bool? isCancel, bool? isApprove);
        Hashtable GetTalkshows(int? counselorId, int page, int limit, IEnumerable<int> talkshowsId, bool? isBooking
            , bool? isFinish, bool? isCancel, bool? isApprove, bool? isBanner);
        IEnumerable<Talkshow> GetTalkshows();
        Task<bool> InsertTalkshow(Talkshow talkshow);
        Task<bool> UpdateTalkshow(Talkshow newTalkshow);
    }

    public class TalkshowRepository : ITalkshowRepository
    {
        private readonly AdmissionsDBContext _admissionsDBContext;

        public TalkshowRepository(AdmissionsDBContext admissionsDBContext)
        {
            _admissionsDBContext = admissionsDBContext;
        }

        public Talkshow GetTalkshow(int? counselorId, int talkshowId)
        {
            var talkshow = _admissionsDBContext.Talkshows.Where(talkshow => talkshow.Id == talkshowId);
            if (counselorId != null) talkshow = talkshow.Where(talkshow => talkshow.CounselorId == counselorId);

            return talkshow.FirstOrDefault();
        }

        public TalkshowSQL GetTalkshowSQL(int? counselorId, int talkshowId
            , bool? isCancel, bool? isApprove)
        {
            var talkshows = _admissionsDBContext.Talkshows
                .Where(talkshow => talkshow.Id == talkshowId)
                .Select(talkshow => new TalkshowSQL
                {
                    Id = talkshow.Id,
                    Description = talkshow.Description,
                    Image = talkshow.Image,
                    UrlMeet = talkshow.UrlMeet,
                    Price = talkshow.Price,
                    CreatedDate = talkshow.CreatedDate.AddHours(7),
                    StartDate = talkshow.StartDate.AddHours(7),
                    IsFinish = talkshow.IsFinish,
                    IsCancel = talkshow.IsCancel,
                    IsBanner = talkshow.IsBanner,
                    Counselor = (from counselor in _admissionsDBContext.Counselors
                                 join user in _admissionsDBContext.Users
                                 on counselor.Id equals user.Id
                                 where user.RoleId == 2 && user.Id == talkshow.CounselorId
                                 select new UserCounselor
                                 {
                                     Id = user.Id,
                                     Email = user.Email,
                                     IsActive = user.IsActive,
                                     RoleId = user.RoleId,
                                     FullName = counselor.FullName,
                                     Phone = counselor.Phone,
                                     Avatar = counselor.Avatar,
                                     Description = counselor.Description
                                 }).FirstOrDefault(),
                    Students = _admissionsDBContext.Slots
                    .Where(slot => slot.TalkshowId == talkshow.Id)
                    .Select(slot => new TalkshowStudentSQL
                    {
                        Id = slot.StudentId,
                        Email = _admissionsDBContext.Users.Where(user => user.Id == slot.StudentId).FirstOrDefault().Email,
                        FullName = _admissionsDBContext.Students.Where(student => student.Id == slot.StudentId).FirstOrDefault().FullName,
                        Phone = _admissionsDBContext.Students.Where(student => student.Id == slot.StudentId).FirstOrDefault().Phone,
                        Avatar = _admissionsDBContext.Students.Where(student => student.Id == slot.StudentId).FirstOrDefault().Avatar,
                        Address = _admissionsDBContext.Students.Where(student => student.Id == slot.StudentId).FirstOrDefault().Address,
                        Dob = _admissionsDBContext.Students.Where(student => student.Id == slot.StudentId).FirstOrDefault().Dob,
                        OldSchool = _admissionsDBContext.OldSchools
                                    .Where(oldSchool => oldSchool.Id == _admissionsDBContext.Students.Where(student => student.Id == slot.StudentId).FirstOrDefault().OldSchoolId)
                                    .FirstOrDefault()
                    }).ToList(),
                    Major = _admissionsDBContext.Majors
                    .Where(major => major.Id == talkshow.MajorId)
                    .Select(major => new MajorSQL
                    {
                        Id = major.Id,
                        Name = major.Name,
                        Description = major.Description
                    }).FirstOrDefault(),
                    University = _admissionsDBContext.Universities
                    .Where(uni => uni.Id == talkshow.UniversityId)
                    .Select(uni => new TallshowUniversitySQL
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
                    }).FirstOrDefault()
                });

            if (counselorId != null) talkshows = talkshows.Where(talkshow => talkshow.Counselor.Id == counselorId);
            if (isCancel != null) talkshows = talkshows.Where(talkshow => talkshow.IsCancel == isCancel);
            if (isApprove != null) talkshows = talkshows.Where(talkshow => talkshow.IsApprove == isApprove);

            return talkshows.FirstOrDefault();
        }

        public IEnumerable<Talkshow> GetTalkshows()
        {
            var talkshows = _admissionsDBContext.Talkshows.ToList();
            if (talkshows != null && talkshows.Any())
            {
                return talkshows;
            }
            return null;

        }

        public Hashtable GetTalkshows(int? counselorId, int page, int limit
            , IEnumerable<int> talkshowsId, bool? isBooking
            , bool? isFinish, bool? isCancel, bool? isApprove, bool? isBanner)
        {
            var talkshows = _admissionsDBContext.Talkshows
                .Select(talkshow => new TalkshowSQL
                {
                    Id = talkshow.Id,
                    Description = talkshow.Description,
                    Image = talkshow.Image,
                    UrlMeet = talkshow.UrlMeet,
                    Price = talkshow.Price,
                    CreatedDate = talkshow.CreatedDate.AddHours(7),
                    StartDate = talkshow.StartDate.AddHours(7),
                    IsFinish = talkshow.IsFinish,
                    IsCancel = talkshow.IsCancel,
                    IsApprove = talkshow.IsApprove,
                    IsBanner = talkshow.IsBanner,
                    Counselor = (from counselor in _admissionsDBContext.Counselors
                                 join user in _admissionsDBContext.Users
                                 on counselor.Id equals user.Id
                                 where user.RoleId == 2 && user.Id == talkshow.CounselorId
                                 select new UserCounselor
                                 {
                                     Id = user.Id,
                                     Email = user.Email,
                                     IsActive = user.IsActive,
                                     RoleId = user.RoleId,
                                     FullName = counselor.FullName,
                                     Phone = counselor.Phone,
                                     Avatar = counselor.Avatar,
                                     Description = counselor.Description
                                 }).FirstOrDefault(),
                    Students = _admissionsDBContext.Slots
                    .Where(slot => slot.TalkshowId == talkshow.Id)
                    .Select(slot => new TalkshowStudentSQL
                    {
                        Id = slot.StudentId,
                        Email = _admissionsDBContext.Users.Where(user => user.Id == slot.StudentId).FirstOrDefault().Email,
                        FullName = _admissionsDBContext.Students.Where(student => student.Id == slot.StudentId).FirstOrDefault().FullName,
                        Phone = _admissionsDBContext.Students.Where(student => student.Id == slot.StudentId).FirstOrDefault().Phone,
                        Avatar = _admissionsDBContext.Students.Where(student => student.Id == slot.StudentId).FirstOrDefault().Avatar,
                        Address = _admissionsDBContext.Students.Where(student => student.Id == slot.StudentId).FirstOrDefault().Address,
                        Dob = _admissionsDBContext.Students.Where(student => student.Id == slot.StudentId).FirstOrDefault().Dob,
                        OldSchool = _admissionsDBContext.OldSchools
                                    .Where(oldSchool => oldSchool.Id == _admissionsDBContext.Students.Where(student => student.Id == slot.StudentId).FirstOrDefault().OldSchoolId)
                                    .FirstOrDefault()
                    }).ToList(),
                    Major = _admissionsDBContext.Majors
                    .Where(major => major.Id == talkshow.MajorId)
                    .Select(major => new MajorSQL
                    {
                        Id = major.Id,
                        Name = major.Name,
                        Description = major.Description
                    }).FirstOrDefault(),
                    University = _admissionsDBContext.Universities
                    .Where(uni => uni.Id == talkshow.UniversityId)
                    .Select(uni => new TallshowUniversitySQL
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
                    }).FirstOrDefault()
                });


            if (counselorId != null) talkshows = talkshows.Where(talkshow => talkshow.Counselor.Id == counselorId);
            if (isBooking != null)
            {
                if ((bool)isBooking) talkshows = talkshows.Where(talkshow => talkshowsId.Contains(talkshow.Id));
                else talkshows = talkshows.Where(talkshow => !talkshowsId.Contains(talkshow.Id));
            }
            if (isFinish != null) talkshows = talkshows.Where(talkshow => talkshow.IsFinish == isFinish);
            if (isCancel != null) talkshows = talkshows.Where(talkshow => talkshow.IsCancel == isCancel);
            if (isApprove != null) talkshows = talkshows.Where(talkshow => talkshow.IsApprove == isApprove);
            if (isBanner != null) talkshows = talkshows.Where(talkshow => talkshow.IsBanner == isBanner);

            int count = count = talkshows.Count(); ;

            if (talkshows != null && talkshows.Any())
            {
                Hashtable result = new();
                if (page > 0 && limit > 0)
                {
                    talkshows = talkshows.Skip(((page - 1) * limit)).Take(limit);
                    result.Add("numPage", (int)Math.Ceiling(((float)count / limit)));
                }
                result.Add("talkshows", talkshows.OrderByDescending(talkshow => talkshow.Id));
                return result;
            }

            return null;
        }

        public async Task<bool> InsertTalkshow(Talkshow talkshow)
        {
            if (talkshow == null) return false;
            await _admissionsDBContext.Talkshows.AddAsync(talkshow);
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateTalkshow(Talkshow newTalkshow)
        {
            if (newTalkshow == null) return false;
            Talkshow talkshow = _admissionsDBContext.Talkshows.Where(talkshow => talkshow.Id == newTalkshow.Id).FirstOrDefault();
            if (talkshow == null) return false;
            talkshow = newTalkshow;
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }
    }
}
