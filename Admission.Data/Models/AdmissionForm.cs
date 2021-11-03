using System;
using System.Collections.Generic;

#nullable disable

namespace Admission.Data.Models
{
    public partial class AdmissionForm
    {
        public AdmissionForm()
        {
            UniAdmissions = new HashSet<UniAdmission>();
        }

        public int Id { get; set; }
        public string Method { get; set; }
        public string Description { get; set; }

        public virtual ICollection<UniAdmission> UniAdmissions { get; set; }
    }
}
