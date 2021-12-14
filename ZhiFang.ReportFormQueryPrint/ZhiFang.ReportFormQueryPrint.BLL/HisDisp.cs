using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.IDAL;
namespace ZhiFang.ReportFormQueryPrint.BLL
{
    public class BHisDisp
    {
        #region IBHisDisp 成员
        private readonly IHisDisp dal = DalFactory<IHisDisp>.GetDal("HisDisp");
     
        public System.Data.DataSet GetList(Model.HisDisp hisDisp)
        {
            return dal.GetList(hisDisp);
        }

        #endregion 

        #region IBLLBase<HisDisp> 成员

        public int GetMaxId()
        {
            throw new NotImplementedException();
        }

        //public int Add(Model.HisDisp t)
        //{
        //    throw new NotImplementedException();
        //}

        //public int Update(Model.HisDisp t)
        //{
        //    throw new NotImplementedException();
        //}

        public List<Model.HisDisp> GetModelList(Model.HisDisp t)
        {
            throw new NotImplementedException();
        }

        public List<Model.HisDisp> DataTableToList(System.Data.DataTable dt)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet GetAllList()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
