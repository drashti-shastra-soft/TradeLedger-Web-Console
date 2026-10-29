using TradeLedgerWebConsole.Infrastructure.IService;
using System.Threading.Tasks;
using System.Configuration;

namespace TradeLedgerWebConsole.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Declaration
        private readonly IHttpClientService _httpClientService;
        private readonly string _apiBaseUrl;

        public AuthenticationService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _apiBaseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");
        }
        #endregion

        #region Public Methods
        public async Task<Domain.User> AuthenticateAsync(Domain.Credential mCredential)
        {
            return await _httpClientService.PostAsync<Domain.Credential, Domain.User>(
                url: $"{_apiBaseUrl}/Authentication",
                obj: mCredential
            );
        }
        #endregion
    }
}
