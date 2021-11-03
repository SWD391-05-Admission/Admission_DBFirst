using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.Response
{
    public class StudentRes
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime? Dob { get; set; }
        public string OldSchool { get; set; }
        public string Avatar { get; set; }
    }
}
