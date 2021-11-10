using System;
using System.Collections.Generic;

#nullable disable

namespace Admission.Data.Models
{
    public partial class University
    {
        public University()
        {
            Talkshows = new HashSet<Talkshow>();
            UniAddresses = new HashSet<UniAddress>();
            UniAdmissions = new HashSet<UniAdmission>();
            UniImages = new HashSet<UniImage>();
            UniMajors = new HashSet<UniMajor>();
        }

        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public decimal LastYearBenchmark { get; set; }
        public decimal MinFee { get; set; }
        public decimal MaxFee { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Talkshow> Talkshows { get; set; }
        public virtual ICollection<UniAddress> UniAddresses { get; set; }
        public virtual ICollection<UniAdmission> UniAdmissions { get; set; }
        public virtual ICollection<UniImage> UniImages { get; set; }
        public virtual ICollection<UniMajor> UniMajors { get; set; }
    }
}
