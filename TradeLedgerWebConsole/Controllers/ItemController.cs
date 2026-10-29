using TradeLedgerWebConsole.Infrastructure.IService;
using TradeLedgerWebConsole.Infrastructure;
using TradeLedgerWebConsole.Helper;
using TradeLedgerWebConsole.Filter;
using System.Web.Mvc;
using System;

namespace TradeLedgerWebConsole.Controllers
{
    [Authentication]
    public class ItemController : Controller
    {
        private readonly IItemService _service;
        private readonly IDropdownService _dropdownService;

        public ItemController(IItemService service, IDropdownService dropdownService)
        {
            _service = service;
            _dropdownService = dropdownService;
        }

        #region Item
        public ActionResult Index()
        {
            ViewBag.ShowCompany = false;
            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                ViewBag.ShowCompany = true;

            return View(new Domain.ItemLister());
        }

        public ActionResult List(Domain.ItemLister mLister)
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
                ViewBag.unit = _dropdownService.GetUnitOptionsAsync().GetAwaiter().GetResult();

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
            ViewBag.unit = _dropdownService.GetUnitOptionsAsync().GetAwaiter().GetResult();

            ViewBag.ShowCompany = false;
            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                ViewBag.ShowCompany = true;

            return PartialView("_Create", new Domain.Item());
        }

        [HttpPost]
        public ActionResult Create(Domain.Item mItem)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);

                if (HttpContextHelper.LoginUser.RoleId != EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                    Infrastructure.Helper.DefaultSetter.ApplyDefaults(mItem);

                Infrastructure.Helper.AuditHelper.ApplyAuditInfo(mItem);

                mItem = Infrastructure.Helper.AuditHelper.ApplyAuditInfo(mItem);

                mItem = _service.CreateAsync(mItem).GetAwaiter().GetResult();

                bool success = mItem != null && mItem.Id > 0;
                ViewBag.NotificationType = success
                    ? EnumsHelper.GetEnumString(Enums.Result.Success).StrToLower()
                    : EnumsHelper.GetEnumString(Enums.Result.Error).StrToLower();

                ViewBag.NotificationMsg = success
                    ? string.Format(Constant.SuccessMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Item).StrToLower())
                    : string.Format(Constant.ErrorMessage, EnumsHelper.GetEnumString(Enums.Operation.Save).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Item).StrToLower());

                return base.PartialView("_Create", (object)mItem);

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
                ViewBag.unit = _dropdownService.GetUnitOptionsAsync().GetAwaiter().GetResult();
            }
            return PartialView("_Create", mItem);
        }

        [HttpGet]
        public ActionResult Get(int id)
        {
            var mItem = new Domain.Item();
            try
            {
                System.Threading.Thread.Sleep(1000);
                mItem = _service.GetAsync(id).GetAwaiter().GetResult();
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
                ViewBag.unit = _dropdownService.GetUnitOptionsAsync().GetAwaiter().GetResult();
            }
            return PartialView("_Create", mItem);
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
                notificationMsg = string.Format(Constant.SuccessMessage, EnumsHelper.GetEnumString(Enums.Operation.Delete).StrToLower(), EnumsHelper.GetEnumString(Enums.MenuItem.Item).StrToLower());
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
    }
}