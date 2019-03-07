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
    public class AdminController : Controller
    {
        IAdminBLL adminBLL = new AdminBLL();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin Admin) {
            var result =  adminBLL.GetBySituation(a => a.Username == Admin.Username && a.Password == Admin.Password);


            if (result.Count() == 0)
            {
                return Json(new Common.JsonResult {
                    Result = Common.ResultType.Error,
                    ErrorTips = "用户或密码错误"
                });
            }
            return RedirectToAction("Index","Index");
        }
    }
}