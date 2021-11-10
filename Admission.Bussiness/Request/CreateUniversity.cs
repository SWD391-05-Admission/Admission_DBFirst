using System.Collections.Generic;

namespace Admission.Bussiness.Request
{
    public class CreateUniversity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Facebook { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public decimal LastYearBenchmark { get; set; }
        public decimal MinFee { get; set; }
        public decimal MaxFee { get; set; }
    }
}
