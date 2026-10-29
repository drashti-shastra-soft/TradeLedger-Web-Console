using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeLedgerWebConsole.Infrastructure.IService
{
    public interface IContractService
    {
        Task<Domain.ContractLister> GetAllAsync(Domain.ContractLister mLister);
        Task<Domain.Contract> CreateAsync(Domain.Contract mContract);
        Task<Domain.Contract> GetAsync(int id);
        Task DeleteAsync(int id);
    }
}
