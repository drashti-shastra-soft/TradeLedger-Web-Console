using System.Configuration;
using System.Threading.Tasks;
using TradeLedgerWebConsole.Infrastructure.IService;

namespace TradeLedgerWebConsole.Service
{
    public class ContractService : IContractService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public ContractService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }
        }

        #region Public Methods
        public async Task<Domain.ContractLister> GetAllAsync(Domain.ContractLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.ContractLister>(
                    url: $"{_baseUrl}/Contract/GetAll",
                    obj: mLister,
                    token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task<Domain.Contract> CreateAsync(Domain.Contract mContract)
        {
            return await _httpClientService.PostAsync<Domain.Contract>(
                    url: $"{_baseUrl}/Contract",
                    obj: mContract,
                    token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.Contract> GetAsync(int id)
        {
            var result = await _httpClientService.GetAsync<Domain.Contract>(
                    url: $"{_baseUrl}/Contract/{id}",
                    token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClientService.DeleteAsync(
                    url: $"{_baseUrl}/Contract/{id}/LoginUser/{_loginUserId}",
                    token: _token
            ).ConfigureAwait(false);
        }
        #endregion
    }
}
