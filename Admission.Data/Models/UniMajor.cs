using System;
using System.Collections.Generic;

#nullable disable

namespace Admission.Data.Models
{
    public partial class UniMajor
    {
        public int Id { get; set; }
        public int? UniversityId { get; set; }
        public int? MajorId { get; set; }

        public virtual Major Major { get; set; }
        public virtual University University { get; set; }
    }
}
