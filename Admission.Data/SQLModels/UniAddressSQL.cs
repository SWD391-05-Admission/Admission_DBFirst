using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Data.SQLModels
{
    public class UniAddressSQL
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public DistrictSQL District { get; set; }
    }
}
