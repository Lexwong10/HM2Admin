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
    public class ProductColorBLL : BaseBLL<ProductColor>, IProductColorBLL
    {
        IProductColorDAL productColorDAL = new ProductColorDAL();

        public ProductColorBLL()
        {
            this.SetDal(productColorDAL);
        }
    }
}
