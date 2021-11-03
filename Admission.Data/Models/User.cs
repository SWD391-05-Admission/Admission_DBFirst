using System;
using System.Collections.Generic;

#nullable disable

namespace Admission.Data.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }
        public int? RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual Counselor Counselor { get; set; }
        public virtual Student Student { get; set; }
    }
}
