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
    public interface IBannerService
    {
        Talkshow GetTalkshow(int talkshowId);
        Hashtable GetBannersNotShow(SearchTalkshow search);
        Task<bool> ShowBanner(int talkshowId);
        Hashtable GetBannersShow(SearchTalkshow search);
        Task<bool> RemoveBanner(int talkshowId);
    }
}
