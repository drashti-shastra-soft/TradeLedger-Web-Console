using System.Threading.Tasks;

namespace TradeLedgerWebConsole.Infrastructure.IService
{
    public interface IAddressService
    {
        Task<Domain.Address> CreateAsync(Domain.Address mAddress);
        Task<Domain.Address> GetAsync(int id);
        Task DeleteAsync(int id);
    }
}
