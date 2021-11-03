using System;
using System.Collections.Generic;

#nullable disable

namespace Admission.Data.Models
{
    public partial class Slot
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int TalkshowId { get; set; }
        public int StudentId { get; set; }

        public virtual Student Student { get; set; }
        public virtual Talkshow Talkshow { get; set; }
    }
}
