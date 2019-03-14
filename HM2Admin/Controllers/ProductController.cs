using BLL;
using IBLL;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace HM2Admin.Controllers
{
    [Filters.LoginFilter]
    public class ProductController : Controller
    {
        public IProductBLL productBLL = new ProductBLL();
        public IProductTypeBLL productTypeBLL = new ProductTypeBLL();
        public IProductSizeBLL productSizeBLL = new ProductSizeBLL();
        public IProductColorBLL productColorBLL = new ProductColorBLL();
        public IProductImgBLL productImgBLL = new ProductImgBLL();

        public const int PAGE_SIZE = 3;
        //查看商品列表
        public ActionResult List(int? Page, int? TypeId, string OrderField, int? Asc, string Key)
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

            if (Asc == null)
            {
                asc = true;
            }

            Expression<Func<Product, bool>> whereExpression = p => true;
            if (TypeId != -1)
            {
                whereExpression = p => p.ProductType.Id == TypeId || p.ProductType.ProductType2.Id == TypeId;
            }

            if (Key != null)
            {
                whereExpression = p => p.Detail.Contains(Key);
            }
            var totalProduct = productBLL.GetBySituation(whereExpression).Count();
            ViewBag.totalPage = Math.Ceiling(totalProduct * 1.0 / PAGE_SIZE);
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


        //新增商品
        [HttpGet]
        public ActionResult Create() {
            ViewBag.ProductType = productTypeBLL.GetAll();
            ViewBag.ProductSize = productSizeBLL.GetAll();
            ViewBag.ProductColor = productColorBLL.GetAll();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Product p, IEnumerable<HttpPostedFileBase> Images) {
            if (p.OnSale == null)
            {
                p.OnSale = 0;
            }
            else {
                p.OnSale = 1;
            }
            string path = Server.MapPath("~/Images/ProductImg/");
            int productId = productBLL.Add(p).Id;
            List<string> bigName = Common.ImagesHelper.ImagesSave(Images, path);
            List<string> smallName = Common.ImagesHelper.smallImage(Images, path);

            for (int i = 0; i < bigName.Count; i++)
            {
                productImgBLL.Add(new ProductImg
                {
                    ProductId = productId,
                    BigImage = bigName[i],
                    SmallImage = smallName[i]
                });
            }

            return RedirectToAction("List");
        }

        //编辑商品
        [HttpGet]
        public ActionResult Edit(int id) {
            var list = productBLL.GetById(id);
            ViewBag.ProductType = productTypeBLL.GetAll();
            ViewBag.ProductSize = productSizeBLL.GetAll();
            ViewBag.ProductColor = productColorBLL.GetAll();
            return View(list);
        }

        [HttpGet]
        public ActionResult EditById()
        {
            return View();
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Product p, int? OnSale) {
            if (OnSale == null || OnSale == 0)
            {
                p.OnSale = 0;
            }
            else {
                p.OnSale = 1;
            }
            productBLL.Modify(p, "Name", "TypeId", "OldPrice", "NewPrice", "ColorId", "SizeId", "Detail", "Sold", "Stock", "OnSale");

            return RedirectToAction("List");
        }

        //删除商品
        public ActionResult Delete(int id) {
            Product  p = productBLL.GetById(id);
            p.OnSale = 2;
            productBLL.Modify(p, "OnSale");
            return RedirectToAction("List");
        }


        public ActionResult DeleteById() {
            return View();
        }
    }
}