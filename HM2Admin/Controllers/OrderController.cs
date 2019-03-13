using BLL;
using IBLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace HM2Admin.Controllers
{
    public class OrderController : Controller
    {
        public IOrderBLL orderBLL = new OrderBLL();
        public const int PAGE_SIZE = 5;
        // GET: Order
        public ActionResult List(int? Page)
        {
            if (Page == null)
            {
                Page = 1;
            }
            Expression<Func<Order, bool>> whereExpression = o => true;

            var totalOrder = orderBLL.GetBySituation(whereExpression).Count();
            ViewBag.totalPages = Math.Ceiling(totalOrder * 1.0 / PAGE_SIZE);
            ViewBag.currentPage = Page;

            var list =  orderBLL.GetByPage(PAGE_SIZE, Page, true, "Id", whereExpression);
            return View(list.ToList());
        }
    }
}