using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Data.SQLModels
{
    public class UniMajorSQL
    {
        public int Id { get; set; }
        public MajorSQL Major { get; set; }
    }
}
