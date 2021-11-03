using System;
using System.Collections.Generic;

#nullable disable

namespace Admission.Data.Models
{
    public partial class UniAddress
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int? DistrictId { get; set; }
        public int? UniversityId { get; set; }

        public virtual District District { get; set; }
        public virtual University University { get; set; }
    }
}
