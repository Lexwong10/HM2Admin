using BLL;
using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace HM2Admin.Controllers
{
    [Filters.LoginFilter]
    public class IndexController : Controller
    {
        public IMenuBLL menuBLL = new MenuBLL();

        // GET: Index
        public ActionResult Index()
        {
            IQueryable<MenuViewModel> menus = menuBLL.GetBySituation(m => m.ParentId == null).Select(m => new MenuViewModel
            {
                Id = m.Id,
                ParentId = m.ParentId,
                Name = m.Name,
                Controller = m.Controller,
                Action = m.Action,
                ChildMenus = m.Menu1
            });

            return View(menus.ToList());
        }

        public ActionResult Home() {
            return View();
        }
    }
}