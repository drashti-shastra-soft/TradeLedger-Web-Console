using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Web.Mvc;
using TradeLedgerWebConsole.Helper;
using TradeLedgerWebConsole.Infrastructure;
using TradeLedgerWebConsole.Infrastructure.IService;

namespace TradeLedgerWebConsole.Controllers
{
    public class ContractController : Controller
    {
        #region Declaration
        private readonly IContractService _service;
        private readonly IDropdownService _dropdownService;
        private readonly IContractItemService _contractItemService;

        public ContractController(IContractService service, IDropdownService dropdownService, IContractItemService contractItemService)
        {
            _service = service;
            _dropdownService = dropdownService;
            _contractItemService = contractItemService;
        }
        #endregion

        #region Contract
        public ActionResult Index()
        {
            ViewBag.ShowCompany = false;
            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                ViewBag.ShowCompany = true;

            return View(new Domain.ContractLister());
        }

        public ActionResult List(Domain.ContractLister mLister)
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
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = Constant.SomethingWentWrong;
            }
            finally
            {
                mLister.Pagination.PaginationSummary = PaginationHelper.GetPaginationSummary(mLister.Pagination);
                ViewBag.Status = _dropdownService.GetStatusOptions();
                ViewBag.Pagination = _dropdownService.GetPageSizeOptions(mLister.Pagination.PageSize);
                ViewBag.Roles = _dropdownService.GetRolesOptionsAsync().GetAwaiter().GetResult();

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
            ViewBag.Brokers = _dropdownService.GetBrokerOptionsAsync().GetAwaiter().GetResult();
            ViewBag.Sellers = _dropdownService.GetSellerClientOptionsAsync().GetAwaiter().GetResult();
            ViewBag.Buyers = _dropdownService.GetBuyerClientOptionsAsync().GetAwaiter().GetResult();



            ViewBag.ShowCompany = false;
            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                ViewBag.ShowCompany = true;

            return PartialView("_Create", new Domain.Contract());
        }

        [HttpPost]
        public ActionResult Create(Domain.Contract mContract)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);

                if (mContract == null)
                {
                    return Json(new { NotificationType = "error", NotificationMsg = "Contract data not found!" }, JsonRequestBehavior.AllowGet);
                }

                mContract.ContractItems = mContract.ContractItems ?? new List<Domain.ContractItem>();

                if (HttpContextHelper.LoginUser.RoleId != EnumsHelper.GetEnumIndex(Enums.Role.System_Owner)) { 
                    Infrastructure.Helper.DefaultSetter.ApplyDefaults(mContract); }

                mContract = Infrastructure.Helper.AuditHelper.ApplyAuditInfo(mContract);

                var savedContract = _service.CreateAsync(mContract).GetAwaiter().GetResult(); 

                if (savedContract != null && savedContract.Id > 0)
                {
                    return Json(new { NotificationType = "success", NotificationMsg = "Contract has been saved successfully!" }, JsonRequestBehavior.AllowGet);
                }

                return Json(new { NotificationType = "error",  NotificationMsg = "Failed to save contract!"  }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { NotificationType = "error", NotificationMsg = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var mContract = new Domain.Contract();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mContract = _service.GetAsync(id).GetAwaiter().GetResult();
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

                ViewBag.Roles = _dropdownService.GetRolesOptionsAsync(HttpContextHelper.LoginUser?.RoleId ?? 0).GetAwaiter().GetResult();
                ViewBag.Brokers = _dropdownService.GetBrokerOptionsAsync().GetAwaiter().GetResult();
                ViewBag.Sellers = _dropdownService.GetSellerClientOptionsAsync().GetAwaiter().GetResult();
                ViewBag.Buyers = _dropdownService.GetBuyerClientOptionsAsync().GetAwaiter().GetResult();
            }
            return PartialView("_Create", mContract);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var notificationType = EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();
            var notificationMsg = string.Format(Constant.ErrorMessage, EnumsHelper.GetEnumString(Enums.Operation.Delete).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Item).StrToLower());

            try
            {
                System.Threading.Thread.Sleep(1000);
                _service.DeleteAsync(id).GetAwaiter().GetResult();
                notificationType = EnumsHelper.GetEnumString(Enums.Result.Success).StrToLower();
                notificationMsg = string.Format(Constant.SuccessMessage, EnumsHelper.GetEnumString(Enums.Operation.Delete).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Contract).StrToLower());
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

        #endregion

        [HttpGet]
        public ActionResult GetContractItemRowPartial(int index)
        {
            try
            {
                ViewBag.ItemIndex = index;

                ViewBag.Items = _dropdownService.GetItemsOptionsAsync().GetAwaiter().GetResult();

                return PartialView("_CreateContractItem", new Domain.ContractItem());
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Contract Item

        [HttpGet, Route("GetContractItem")]
        public ActionResult GetContractItem(int id)
        {
            var mContractItem = new List<Domain.ContractItem>();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mContractItem = _contractItemService.GetByAsync(id).GetAwaiter().GetResult();
            }
            catch (Exception)
            {
                ViewBag.NotificationType = "error";
                ViewBag.NotificationMsg = "Internal server error!";
            }
            return PartialView("_ContractItem", mContractItem);
        }

        #endregion

    }
}