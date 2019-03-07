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
    public class AdminBLL : BaseBLL<Admin> , IAdminBLL
    {
        IAdminDAL adminDAL = new AdminDAL();

        public AdminBLL() {
            this.SetDal(adminDAL);
        }
    }
}
