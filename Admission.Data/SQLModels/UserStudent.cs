using Admission.Data.Models;
using System;

namespace Admission.Data.SQLModels
{
    public class UserStudent
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }
        public int? RoleId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public DateTime? Dob { get; set; }
        public OldSchoolSQL OldSchool { get; set; }
    }
}
