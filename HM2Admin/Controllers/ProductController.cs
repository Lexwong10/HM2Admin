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
    public class ProductController : Controller
    {
        public IProductBLL productBLL = new ProductBLL();
        public IProductTypeBLL productTypeBLL = new ProductTypeBLL();

        public const int PAGE_SIZE = 3;
        //查看商品列表
        public ActionResult List(int? Page , int? TypeId , string OrderField , int? Asc)
        {
            if (Page == null)
            {
                Page = 1;
            }

            if (TypeId == null)
            {
                TypeId = -1;
            }

            if (OrderField == null)
            {
                OrderField = "Id";
            }
            bool asc = true;
            if (Asc == 1)
            {
                asc = false;
            }
            else {
                asc = true;
            }

            Expression<Func<Product, bool>> whereExpression = p => true;
            if (TypeId != -1)
            {
                whereExpression = p => p.ProductType.Id == TypeId || p.ProductType.ProductType2.Id == TypeId;
            }

            var totalProduct = productBLL.GetBySituation(whereExpression).Count();
            ViewBag.totalPage = Math.Ceiling(totalProduct * 1.0 / PAGE_SIZE );
            ViewBag.currentPage = Page;


            ViewBag.productType = productTypeBLL.GetAll().ToList();
            ViewBag.TypeId = TypeId;

            var list = productBLL.GetByPage(PAGE_SIZE, Page, asc, OrderField, whereExpression);

            return View(list.ToList());
        }

        //查看商品详情
        public ActionResult Detail(int id) {
            return View(productBLL.GetById(id));
        }
    }
}