using TradeLedgerWebConsole.Infrastructure;
using TradeLedgerWebConsole.Filter;
using System.Web.Mvc;

namespace TradeLedgerWebConsole.Controllers
{
    [Authentication]
    public class LogoutController : Controller
    {
        public ActionResult Index()
        {
            HttpContextHelper.LoginUser = null;
            return RedirectToAction("Index", "Authentication");
        }
    }
}