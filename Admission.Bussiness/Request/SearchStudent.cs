using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.Request
{
    public class SearchStudent : BaseSearch
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
    }
}
