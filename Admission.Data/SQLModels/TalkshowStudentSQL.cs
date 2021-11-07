﻿using Admission.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Data.SQLModels
{
    public class TalkshowStudentSQL
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public string Address { get; set; }
        public DateTime? Dob { get; set; }
        public OldSchool OldSchool { get; set; }
    }
}
