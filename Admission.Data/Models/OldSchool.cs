using System;
using System.Collections.Generic;

#nullable disable

namespace Admission.Data.Models
{
    public partial class OldSchool
    {
        public OldSchool()
        {
            Students = new HashSet<Student>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
