using Admission.Data.IRepository;
using Admission.Data.Models;
using Admission.Data.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Data.Repository
{
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
