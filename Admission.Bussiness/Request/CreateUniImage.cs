using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.Request
{
    public class CreateUniImage
    {
        public string Src { get; set; }
        public string Alt { get; set; }
        public bool IsLogo { get; set; }
        public int UniversityId { get; set; }
    }
}
