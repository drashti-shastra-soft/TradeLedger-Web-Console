using TradeLedgerWebConsole.Infrastructure.IService;
using System.Threading.Tasks;
using System.Configuration;

namespace TradeLedgerWebConsole.Service
{
    public class BrokerService : IBrokerService
    {
        #region Declaration
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public BrokerService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;

            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }
        }
        #endregion

        #region Public Methods
        public async Task<Domain.BrokerLister> GetAllAsync(Domain.BrokerLister mLister)
        {
            var result = await _httpClientService.PostAsync<Domain.BrokerLister>(
                    url: $"{_baseUrl}/Broker/GetAll",
                    obj: mLister,
                    token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task<Domain.Broker> CreateAsync(Domain.Broker mBroker)
        {
            return await _httpClientService.PostAsync<Domain.Broker>(
                    url: $"{_baseUrl}/Broker",
                    obj: mBroker,
                    token: _token
            ).ConfigureAwait(false);
        }

        public async Task<Domain.Broker> GetAsync(int id)
        {
            var result = await _httpClientService.GetAsync<Domain.Broker>(
                    url: $"{_baseUrl}/Broker/{id}",
                    token: _token
            ).ConfigureAwait(false);
            return result;
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClientService.DeleteAsync(
                    url: $"{_baseUrl}/Broker/{id}/LoginUser/{_loginUserId}",
                    token: _token
            ).ConfigureAwait(false);
        }

        public async Task UpdateAddressAsync(int id, int addressId)
        {
            await _httpClientService.PatchAsync(
                    url: $"{_baseUrl}/Broker/{id}/AddressId/{addressId}/LoginUser/{_loginUserId}",
                    token: _token
            ).ConfigureAwait(false);
        }
        #endregion
    }

}
