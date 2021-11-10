using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.Request
{
    public class UpdateTalkshow
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string UrlMeet { get; set; }
        public int Price { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsCancel { get; set; }
        public int? MajorId { get; set; }
        public int? UniversityId { get; set; }
    }
}
