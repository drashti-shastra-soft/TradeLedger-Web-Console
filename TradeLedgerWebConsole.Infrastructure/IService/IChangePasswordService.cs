using System.Threading.Tasks;

namespace TradeLedgerWebConsole.Infrastructure.IService
{
    public interface IChangePasswordService
    {
        Task Initiate(Domain.ChangePassword mChangePassword);
    }
}
