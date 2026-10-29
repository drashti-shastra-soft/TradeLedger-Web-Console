using TradeLedgerWebConsole.Infrastructure.IService;
using System.Threading.Tasks;
using System.Configuration;

namespace TradeLedgerWebConsole.Service
{
    public class ChangePasswordService : IChangePasswordService
    {
        #region Declaration
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;

        public ChangePasswordService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
        }
        #endregion

        #region Public Methods
        public async Task Initiate(Domain.ChangePassword mChangePassword)
        {
            await _httpClientService.PutAsync<Domain.ChangePassword>(
                url: $"{_baseUrl}/ChangePassword",
                obj: mChangePassword,
                token: _token
            ).ConfigureAwait(false);
        }
        #endregion
    }
}
