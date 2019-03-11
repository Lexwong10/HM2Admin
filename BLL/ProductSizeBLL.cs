using BLL;
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
    public class ProductSizeBLL : BaseBLL<ProductSize>, IProductSizeBLL
    {
        IProductSizeDAL productSizeDAL = new ProductSizeDAL();

        public ProductSizeBLL()
        {
            this.SetDal(productSizeDAL);
        }
    }
}
