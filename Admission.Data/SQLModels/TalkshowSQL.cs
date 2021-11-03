using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Data.SQLModels
{
    public class TalkshowSQL
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string UrlMeet { get; set; }
        public int Price { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsFinish { get; set; }
        public bool IsCancel { get; set; }
        public bool IsBanner { get; set; }

        public UserCounselor Counselor { get; set; }
        public MajorSQL Major { get; set; }
        public TallshowUniversitySQL University { get; set; }
    }
}
