﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.Request
{
    public class CreateUniAddress
    {
        public string Address { get; set; }
        public int DistrictId { get; set; }
        public int UniversityId { get; set; }
    }
}
