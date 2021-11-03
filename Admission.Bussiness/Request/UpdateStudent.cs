using System;

namespace Admission.Bussiness.Request
{
    public class UpdateStudent
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? Dob { get; set; }
        public string OldSchool { get; set; }
        public string Avatar { get; set; }
    }
}
