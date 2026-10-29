using System.Threading.Tasks;

namespace TradeLedgerWebConsole.Infrastructure.IService
{
    public interface IDropdownService
    {
        dynamic GetStatusOptions();

        dynamic GetPageSizeOptions(int selectedSize = 20);

        Task<dynamic> GetRolesOptionsAsync(int roleId = 0);

        Task<dynamic> GetCompanyOptionsAsync();

        Task<dynamic> GetActiveUsersOptionsAsync(int companyId);

        Task<dynamic> GetClientTypeOptionsAsync();

        Task<dynamic> GetUnitOptionsAsync();

        Task<dynamic> GetBrokerOptionsAsync();

        Task<dynamic> GetSellerClientOptionsAsync();

        Task<dynamic> GetBuyerClientOptionsAsync();

        Task<dynamic> GetItemsOptionsAsync();
    }
}
