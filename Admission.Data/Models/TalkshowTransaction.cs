using System;
using System.Collections.Generic;

#nullable disable

namespace Admission.Data.Models
{
    public partial class TalkshowTransaction
    {
        public int Id { get; set; }
        public int? TalkshowId { get; set; }
        public int? TransactionId { get; set; }

        public virtual Talkshow Talkshow { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}
