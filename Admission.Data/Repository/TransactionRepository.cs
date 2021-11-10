using Admission.Data.Models;
using Admission.Data.Models.Context;
using System.Threading.Tasks;

namespace Admission.Data.Repository
{
    public interface ITransactionRepository
    {
        Task<bool> InsertTransaction(Transaction transaction, bool isLoop);
    }

    public class TransactionRepository : ITransactionRepository
    {
        private readonly AdmissionsDBContext _admissionsDBContext;

        public TransactionRepository(AdmissionsDBContext admissionsDBContext)
        {
            _admissionsDBContext = admissionsDBContext;
        }

        public async Task<bool> InsertTransaction(Transaction transaction, bool isLoop)
        {
            if (transaction == null) return false;
            await _admissionsDBContext.Transactions.AddAsync(transaction);
            if (isLoop) return true;
            return await _admissionsDBContext.SaveChangesAsync() > 0;
        }
    }
}
