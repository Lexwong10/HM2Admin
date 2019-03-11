using DAL;
using IBLL;
using IDAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ProductImgBLL : BaseBLL<ProductImg>, IProductImgBLL
    {
        IProductImgDAL productImgDAL = new ProductImgDAL();

        public ProductImgBLL()
        {
            this.SetDal(productImgDAL);
        }
    }
}
