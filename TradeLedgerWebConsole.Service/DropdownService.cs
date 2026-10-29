using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TradeLedgerWebConsole.Domain;
using TradeLedgerWebConsole.Infrastructure;
using TradeLedgerWebConsole.Infrastructure.IService;

namespace TradeLedgerWebConsole.Service
{
    public class DropdownService : IDropdownService
    {
        private readonly IHttpClientService _httpClientService;
        private readonly string baseUrl;

        public DropdownService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
            baseUrl = ConfigurationManager.AppSettings.Get("APIBaseUrl");
        }

        #region Status
        public dynamic GetStatusOptions()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Active", Value = "Active" },
                new SelectListItem { Text = "Disabled", Value = "Disabled" }
            };
        }
        #endregion

        #region Document
        public dynamic GetDocumentOptions()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Text = "Aadhar_Card", Value = "Aadhar_Card" },
                new SelectListItem { Text = "Pan_Card", Value = "Pan_Card" },
                new SelectListItem { Text = "Driving_license", Value = "Driving_license" }
            };
        }
        #endregion

        #region Page Size
        public dynamic GetPageSizeOptions(int selectedSize = 20)
        {
            var list = new List<SelectListItem>
            {
                new SelectListItem { Text = "5", Value = "5" },
                new SelectListItem { Text = "10", Value = "10" },
                new SelectListItem { Text = "20", Value = "20" },
                new SelectListItem { Text = "50", Value = "50" },
                new SelectListItem { Text = "100", Value = "100" },
                new SelectListItem { Text = "All", Value = "-1" }
            };

            list.ForEach(item => item.Selected = item.Value == selectedSize.ToString());
            return list;
        }
        #endregion

        #region Roles
        public async Task<dynamic> GetRolesOptionsAsync(int roleId = 0)
        {
            var mSelectListItems = new List<SelectListItem>();
            var systemOwnerRoleId = EnumsHelper.GetEnumIndex(Enums.Role.System_Owner);
            var isSystemOwner = roleId == systemOwnerRoleId;

            var mRoles = await _httpClientService.GetAsync<List<Domain.Role>>(
                url: $"{baseUrl}/Role",
                token: HttpContextHelper.LoginUser?.Token).ConfigureAwait(false);

            var filteredRoles = mRoles.Where(role => isSystemOwner || role.Id != systemOwnerRoleId).ToList();

            foreach (var filteredRole in filteredRoles)
            {
                mSelectListItems.Add(new SelectListItem() { Text = filteredRole.Name, Value = Convert.ToString(filteredRole.Id) });
            }
            return mSelectListItems;
        }
        #endregion

        #region Company
        public async Task<dynamic> GetCompanyOptionsAsync()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mCompanies = await _httpClientService.GetAsync<List<Domain.Company>>(
                url: $"{baseUrl}/Company/GetActiveCompanies",
                token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mCompany in mCompanies)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mCompany.Name, Value = Convert.ToString(mCompany.Id) });
            }
            return mSelectListItems;
        }
        #endregion

        #region Users
        public async Task<dynamic> GetActiveUsersOptionsAsync(int companyId)
        {
            var mSelectListItems = new List<SelectListItem>();

            var mUsers = await _httpClientService.GetAsync<List<Domain.User>>(
                url: $"{baseUrl}/User/GetActiveUsers/CompanyId/{companyId}",
                token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var user in mUsers)
            {
                mSelectListItems.Add(new SelectListItem()
                {
                    Text = $"{user.FirstName} {user.LastName}",
                    Value = Convert.ToString(user.Id)
                });
            }

            return mSelectListItems;
        }
        #endregion

        #region Client Type
        public async Task<dynamic> GetClientTypeOptionsAsync()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mClientTypes = await _httpClientService.GetAsync<List<Domain.ClientType>>(
                    url: $"{baseUrl}/ClientType/GetActiveClientTypes",
                    token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mClientType in mClientTypes)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mClientType.Name, Value = Convert.ToString(mClientType.Id) });
            }
            return mSelectListItems;
        }
        #endregion

        #region Unit
        public async Task<dynamic> GetUnitOptionsAsync()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mUnits = await _httpClientService.GetAsync<List<Domain.Unit>>(
                    url: $"{baseUrl}/Unit/GetActiveUnit",
                    token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mUnit in mUnits)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mUnit.Name, Value = Convert.ToString(mUnit.Id) });
            }
            return mSelectListItems;
        }
        #endregion

        #region Broker
        public async Task<dynamic> GetBrokerOptionsAsync()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mBrokers = await _httpClientService.GetAsync<List<Domain.Broker>>(
                    url: $"{baseUrl}/Broker/GetActiveBroker",
                    token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mBroker in mBrokers)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mBroker.Name, Value = Convert.ToString(mBroker.Id) });
            }
            return mSelectListItems;
        }
        #endregion

        #region Seller Client
        public async Task<dynamic> GetSellerClientOptionsAsync()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mClients = await _httpClientService.GetAsync<List<Domain.Client>>(
                    url: $"{baseUrl}/Client/GetActiveSellerClient",
                    token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mClient in mClients)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mClient.Name, Value = Convert.ToString(mClient.Id) });
            }
            return mSelectListItems;
        }
        #endregion

        #region Buyer Client
        public async Task<dynamic> GetBuyerClientOptionsAsync()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mClients = await _httpClientService.GetAsync<List<Domain.Client>>(
                    url: $"{baseUrl}/Client/GetActiveBuyerClient",
                    token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mClient in mClients)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mClient.Name, Value = Convert.ToString(mClient.Id) });
            }
            return mSelectListItems;
        }
        #endregion

        #region Items

        public async Task<dynamic> GetItemsOptionsAsync()
        {
            var mSelectListItems = new List<SelectListItem>();

            var mItems = await _httpClientService.GetAsync<List<Domain.Item>>(
                    url: $"{baseUrl}/Item/GetActiveItems",
                    token: HttpContextHelper.LoginUser?.Token
            ).ConfigureAwait(false);

            foreach (var mItem in mItems)
            {
                mSelectListItems.Add(new SelectListItem() { Text = mItem.Name, Value = Convert.ToString(mItem.Id) });
            }
            return mSelectListItems;
        }
        #endregion
    }
}
