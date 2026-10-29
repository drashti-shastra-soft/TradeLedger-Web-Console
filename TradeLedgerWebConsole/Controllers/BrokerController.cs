using TradeLedgerWebConsole.Infrastructure.IService;
using TradeLedgerWebConsole.Infrastructure;
using TradeLedgerWebConsole.Filter;
using TradeLedgerWebConsole.Helper;
using System.Web.Mvc;
using System;

namespace TradeLedgerWebConsole.Controllers
{
    [Authentication]
    public class BrokerController : Controller
    {
        private readonly IBrokerService _service;
        private readonly IAddressService _addressService;
        private readonly IDropdownService _dropdownService;

        public BrokerController(IBrokerService service, IDropdownService dropdownService, IAddressService addressservice)
        {
            _service = service;
            _dropdownService = dropdownService;
            _addressService = addressservice;
        }

        public ActionResult Index()
        {
            ViewBag.ShowCompany = false;
            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                ViewBag.ShowCompany = true;

            return View(new Domain.BrokerLister());
        }

        public ActionResult List(Domain.BrokerLister mLister)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                ViewBag.ShowCompany = false;
                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    ViewBag.ShowCompany = true;
                else
                    mLister.SearchCriteria.CompanyId = HttpContextHelper.LoginUser.CompanyId;

                mLister = _service.GetAllAsync(mLister).GetAwaiter().GetResult();

            }
            catch (Exception ex)
            {
                ViewBag.NotificationType = EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();
                ViewBag.NotificationMsg = ex.Message;
            }
            finally
            {
                mLister.Pagination.PaginationSummary = PaginationHelper.GetPaginationSummary(mLister.Pagination);
                ViewBag.Status = _dropdownService.GetStatusOptions();
                ViewBag.Pagination = _dropdownService.GetPageSizeOptions(mLister.Pagination.PageSize);

                ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(HttpContextHelper.LoginUser?.RoleId ?? 0).GetAwaiter().GetResult();

                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    ViewBag.Companies = _dropdownService.GetCompanyOptionsAsync().GetAwaiter().GetResult();
            }

            return PartialView("_List", mLister);
        }

        [HttpGet]
        public ActionResult Create()
        {
            System.Threading.Thread.Sleep(1000);
            ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(HttpContextHelper.LoginUser?.RoleId ?? 0).GetAwaiter().GetResult();
            ViewBag.ShowCompany = false;

            ViewBag.Companies = _dropdownService.GetCompanyOptionsAsync().GetAwaiter().GetResult();
            ViewBag.ShowCompany = false;
            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                ViewBag.ShowCompany = true;

            return PartialView("_Create", new Domain.Broker());
        }

        [HttpPost]
        public ActionResult Create(Domain.Broker mBroker)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);

                if (HttpContextHelper.LoginUser.RoleId != EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    Infrastructure.Helper.DefaultSetter.ApplyDefaults(mBroker);

                Infrastructure.Helper.AuditHelper.ApplyAuditInfo(mBroker);

                mBroker = _service.CreateAsync(mBroker).GetAwaiter().GetResult();

                bool success = mBroker != null && mBroker.Id > 0;

                ViewBag.NotificationType = success
                        ? EnumsHelper.GetEnumString(Enums.Result.Success).StrToLower()
                        : EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();

                ViewBag.NotificationMsg = success
                        ? string.Format(Constant.SuccessMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Broker).StrToLower())
                        : string.Format(Constant.ErrorMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Broker).StrToLower());

                return base.PartialView("_Create", (object)mBroker);
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = Constant.InternalServerError;
            }
            finally
            {
                ViewBag.ShowCompany = false;
                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    ViewBag.ShowCompany = true;

                ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(HttpContextHelper.LoginUser?.RoleId ?? 0).GetAwaiter().GetResult();
                ViewBag.Companies = _dropdownService.GetCompanyOptionsAsync().GetAwaiter().GetResult();
            }
            return PartialView("_Create", mBroker);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var mBroker = new Domain.Broker();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mBroker = _service.GetAsync(id).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            finally
            {
                ViewBag.ShowCompany = false;
                ViewBag.Companies = string.Empty;
                if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                {
                    ViewBag.ShowCompany = true;
                    ViewBag.Companies = _dropdownService.GetCompanyOptionsAsync().GetAwaiter().GetResult();
                }
                //ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(HttpContextHelper.LoginUser?.RoleId ?? 0).GetAwaiter().GetResult();
            }
            return PartialView("_Create", mBroker);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var notificationType = EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();
            var notificationMsg = string.Format(Constant.ErrorMessage, EnumsHelper.GetEnumString(Enums.Operation.Delete).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Broker).StrToLower());

            try
            {
                System.Threading.Thread.Sleep(1000);
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                notificationType = EnumsHelper.GetEnumString(Enums.Result.Success).StrToLower();
                notificationMsg = string.Format(Constant.SuccessMessage, EnumsHelper.GetEnumString(Enums.Operation.Delete).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Broker).StrToLower());
            }
            catch (Exception ex)
            {
                ViewBag.NotificationType = EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();
                ViewBag.NotificationMsg = ex.Message;
            }

            var resultData = new
            {
                NotificationType = notificationType,
                NotificationMsg = notificationMsg
            };

            return Json(resultData, JsonRequestBehavior.AllowGet);
        }

        #region Broker Address

        [HttpGet]
        public ActionResult GetAddress(int brokerId)
        {
            var mAddress = new Domain.Address();
            try
            {
                mAddress = _addressService.GetAsync(brokerId).GetAwaiter().GetResult();
                mAddress = mAddress ?? new Domain.Address();
                mAddress.BrokerId = brokerId;
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            return PartialView("_Address", mAddress);
        }

        [HttpPost]
        public ActionResult SaveAddress(Domain.Address mAddress)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                mAddress = Infrastructure.Helper.AuditHelper.ApplyAuditInfo(mAddress);
                mAddress = _addressService.CreateAsync(mAddress).GetAwaiter().GetResult();

                bool success = mAddress != null && mAddress.Id > 0;
                ViewBag.NotificationType = success
                        ? EnumsHelper.GetEnumString(Enums.Result.Success).StrToLower()
                        : EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();

                ViewBag.NotificationMsg = success
                        ? string.Format(Constant.SuccessMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Address).StrToLower())
                        : string.Format(Constant.ErrorMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Address).StrToLower());
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            return PartialView("_Address", mAddress);
        }
        #endregion

    }

}