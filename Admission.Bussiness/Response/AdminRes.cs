using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.Response
{
    public class AdminRes
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }
        public int? RoleId { get; set; }
    }
}
