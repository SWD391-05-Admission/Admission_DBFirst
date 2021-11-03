using System;
using System.Collections.Generic;

#nullable disable

namespace Admission.Data.Models
{
    public partial class UniAdmission
    {
        public int Id { get; set; }
        public int UniversityId { get; set; }
        public int AdmissionId { get; set; }

        public virtual AdmissionForm Admission { get; set; }
        public virtual University University { get; set; }
    }
}
