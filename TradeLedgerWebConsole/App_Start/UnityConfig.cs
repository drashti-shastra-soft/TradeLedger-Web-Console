using System.Web.Mvc;
using TradeLedgerWebConsole.Infrastructure.IService;
using TradeLedgerWebConsole.Service;
using Unity;
using Unity.Mvc5;

namespace TradeLedgerWebConsole
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<IAddressService, AddressService>();
            container.RegisterType<IBrokerService, BrokerService>();
            container.RegisterType<IAuthenticationService, AuthenticationService>();
            container.RegisterType<IChangePasswordService, ChangePasswordService>();
            container.RegisterType<IClientService, ClientService>();
            container.RegisterType<ICompanyService, CompanyService>();
            container.RegisterType<IContractItemService, ContractItemService>();
            container.RegisterType<IContractService, ContractService>();
            container.RegisterType<IDropdownService, DropdownService>();
            container.RegisterType<IForgotPasswordService, ForgotPasswordService>();
            container.RegisterType<IHttpClientService, HttpClientService>();
            container.RegisterType<IItemService, ItemService>();
            container.RegisterType<IUserService, UserService>();


            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}