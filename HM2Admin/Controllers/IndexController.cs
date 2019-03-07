using BLL;
using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HM2Admin.Controllers
{
    public class IndexController : Controller
    {
        public IAdminBLL adminBLL = new AdminBLL();

        // GET: Index
        public ActionResult Index(Admin admin)
        {
            return View();
        }

        public ActionResult Home() {
            return View();
        }
    }
}