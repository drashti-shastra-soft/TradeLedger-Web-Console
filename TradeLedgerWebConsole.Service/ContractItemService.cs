using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradeLedgerWebConsole.Infrastructure.IService;

namespace TradeLedgerWebConsole.Service
{
    public class ContractItemService : IContractItemService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string _baseUrl;
        private readonly string _token;
        private readonly int _loginUserId;

        public ContractItemService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            _baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");

            if (Infrastructure.HttpContextHelper.LoginUser != null)
            {
                _token = Infrastructure.HttpContextHelper.LoginUser.Token;
                _loginUserId = Infrastructure.HttpContextHelper.LoginUser.Id;
            }
        }

        public async Task<List<Domain.ContractItem>> GetByAsync(int id)
        {
            return await _httpClientService.GetAsync<List<Domain.ContractItem>>(
            url: $"{_baseUrl}/ContractItem/{id}",
                token: _token
            ).ConfigureAwait(false);
        }
    }
}
