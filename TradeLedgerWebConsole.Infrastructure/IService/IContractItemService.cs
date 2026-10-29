using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeLedgerWebConsole.Infrastructure.IService
{
    public interface IContractItemService
    {
        Task<List<Domain.ContractItem>> GetByAsync(int id);
    }
}
