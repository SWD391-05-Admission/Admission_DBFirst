using System;
using System.Collections.Generic;

#nullable disable

namespace Admission.Data.Models
{
    public partial class UniImage
    {
        public int Id { get; set; }
        public string Src { get; set; }
        public string Alt { get; set; }
        public bool? IsLogo { get; set; }
        public int? UniversityId { get; set; }

        public virtual University University { get; set; }
    }
}
