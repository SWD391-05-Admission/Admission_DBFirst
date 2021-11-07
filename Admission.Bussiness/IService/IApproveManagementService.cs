using Admission.Bussiness.Request;
using Admission.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.IService
{
    public interface IApproveManagementService
    {
        Talkshow GetTalkshow(int talkshowId);
        Hashtable GetTalkshowsNotApprove(SearchTalkshow search);
        Task<bool> ApproveTalkshow(int talkshowId);
        Hashtable GetTalkshowsApprove(SearchTalkshow search);
    }
}
