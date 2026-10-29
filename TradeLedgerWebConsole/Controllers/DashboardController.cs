using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TradeLedgerWebConsole.Filter;
using TradeLedgerWebConsole.Infrastructure;
using TradeLedgerWebConsole.Infrastructure.IService;

namespace TradeLedgerWebConsole.Controllers
{
    [Authentication]
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ShowCompany = false;
            if (HttpContextHelper.LoginUser.RoleId == EnumsHelper.GetEnumIndex(Enums.Role.System_Owner))
                ViewBag.ShowCompany = true;
            return View();
        }
    }
}