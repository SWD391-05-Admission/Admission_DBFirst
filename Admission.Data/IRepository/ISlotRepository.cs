using Admission.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Data.IRepository
{
    public interface ISlotRepository
    {
        Slot GetSlot(int studentId, int talkshowId);
        IEnumerable<Slot> GetSlots(int talkshowId);
        IEnumerable<int> GetTalkshowId(int studentId);
        Task<bool> InsertSlot(Slot slot);
        Task<bool> DeleteSlot(int studentId, int talkshowId, bool isLoop);
    }
}
