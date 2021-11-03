using System;
using System.Collections.Generic;

#nullable disable

namespace Admission.Data.Models
{
    public partial class District
    {
        public District()
        {
            UniAddresses = new HashSet<UniAddress>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UniAddress> UniAddresses { get; set; }
    }
}
