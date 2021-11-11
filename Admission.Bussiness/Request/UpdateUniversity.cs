using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.Request
{
    public class UpdateUniversity
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public decimal LastYearBenchmark { get; set; }
        public decimal MinFee { get; set; }
        public decimal MaxFee { get; set; }
        public bool IsActive { get; set; }
    }
}
