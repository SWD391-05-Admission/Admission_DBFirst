using System;
using System.Collections.Generic;

#nullable disable

namespace Admission.Data.Models
{
    public partial class Major
    {
        public Major()
        {
            Talkshows = new HashSet<Talkshow>();
            UniMajors = new HashSet<UniMajor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Talkshow> Talkshows { get; set; }
        public virtual ICollection<UniMajor> UniMajors { get; set; }
    }
}
