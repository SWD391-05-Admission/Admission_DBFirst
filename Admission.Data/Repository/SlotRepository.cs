using Admission.Data.Models;
using Admission.Data.Models.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admission.Data.Repository
{
    public interface ISlotRepository
    {
        Slot GetSlot(int studentId, int talkshowId);
        IEnumerable<Slot> GetSlots(int talkshowId);
        IEnumerable<int> GetTalkshowId(int studentId);
        Task<bool> InsertSlot(Slot slot);
        Task<bool> DeleteSlot(int studentId, int talkshowId, bool isLoop);
    }

    public class SlotRepository : ISlotRepository
    {
        private readonly AdmissionsDBContext _admissionsDBContext;

        public SlotRepository(AdmissionsDBContext admissionsDBContext)
        {
            _admissionsDBContext = admissionsDBContext;
        }

        public Slot GetSlot(int studentId, int talkshowId)
        {
            return _admissionsDBContext.Slots.Where(slot => slot.StudentId == studentId && slot.TalkshowId == talkshowId).FirstOrDefault();
        }

        public IEnumerable<Slot> GetSlots(int talkshowId)
        {
            var slots = _admissionsDBContext.Slots
                .Where(slot => slot.TalkshowId == talkshowId);
            if (slots != null && slots.Any()) return slots;
            return null;
        }

        public IEnumerable<int> GetTalkshowId(int studentId)
        {
            var talkshowsId = _admissionsDBContext.Slots
                .Where(slot => slot.StudentId == studentId)
                .Select(slot => slot.TalkshowId);
            if (talkshowsId != null && talkshowsId.Any()) return talkshowsId;
            return null;
        }

        public async Task<bool> InsertSlot(Slot slot)
        {
            if (slot == null) return false;
            await _admissionsDBContext.Slots.AddAsync(slot);
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteSlot(int studentId, int talkshowId, bool isLoop)
        {
            var slot = _admissionsDBContext.Slots.Where(slot => slot.StudentId == studentId && slot.TalkshowId == talkshowId).FirstOrDefault();
            _admissionsDBContext.Slots.Remove(slot);
            if (isLoop) return true;
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }
    }
}
