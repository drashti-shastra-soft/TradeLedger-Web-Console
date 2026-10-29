using System.Threading.Tasks;

namespace TradeLedgerWebConsole.Infrastructure.IService
{
    public interface IAuthenticationService
    {
        Task<Domain.User> AuthenticateAsync(Domain.Credential mCredential);
    }
}
