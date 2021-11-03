using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.Request
{
    public abstract class BaseSearch
    {
        public int Page { get; set; }
        public int Limit { get; set; }
    }
}
