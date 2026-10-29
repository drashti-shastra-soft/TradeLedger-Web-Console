using System.Threading.Tasks;

namespace TradeLedgerWebConsole.Infrastructure.IService
{
    public interface IForgotPasswordService
    {
        Task Initiate(Domain.ForgotPassword mForgotPassword);
    }
}
