using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.Request
{
    public class UpdateCounselor
    {
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }
    }
}
