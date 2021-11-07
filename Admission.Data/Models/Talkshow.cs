using System;
using System.Collections.Generic;

#nullable disable

namespace Admission.Data.Models
{
    public partial class Talkshow
    {
        public Talkshow()
        {
            Rates = new HashSet<Rate>();
            Slots = new HashSet<Slot>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string UrlMeet { get; set; }
        public int Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsFinish { get; set; }
        public bool IsCancel { get; set; }
        public bool IsApprove { get; set; }
        public bool IsBanner { get; set; }
        public int CounselorId { get; set; }
        public int? MajorId { get; set; }
        public int? UniversityId { get; set; }

        public virtual Counselor Counselor { get; set; }
        public virtual Major Major { get; set; }
        public virtual University University { get; set; }
        public virtual ICollection<Rate> Rates { get; set; }
        public virtual ICollection<Slot> Slots { get; set; }
    }
}
