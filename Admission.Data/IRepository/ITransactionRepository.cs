using Admission.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admission.Data.IRepository
{
    public interface ITransactionRepository
    {
        Task<bool> InsertTransaction(Transaction transaction, bool isLoop);
    }
}
