using Admission.Bussiness.Request;
using Admission.Data.SQLModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Bussiness.IService
{
    public interface ITalkshowService
    {
        TalkshowSQL GetTalkshow(int talkshowId);
        Hashtable GetTalkshowsAvailable(int studentId, SearchTalkshow search);
        Hashtable GetTalkshowsPending(int studentId, SearchTalkshow search);
        Hashtable GetTalkshowsHistory(int studentId, SearchTalkshow search);
    }
}
