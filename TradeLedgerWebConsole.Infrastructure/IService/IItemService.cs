using System.Threading.Tasks;

namespace TradeLedgerWebConsole.Infrastructure.IService
{
    public interface IItemService
    {
        Task<Domain.ItemLister> GetAllAsync(Domain.ItemLister mLister);
        Task<Domain.Item> CreateAsync(Domain.Item mItem);
        Task<Domain.Item> GetAsync(int id);
        Task DeleteAsync(int id);
    }
}
