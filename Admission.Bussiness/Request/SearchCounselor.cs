﻿namespace Admission.Bussiness.Request
{
    public class SearchCounselor : BaseSearch
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
    }
}
