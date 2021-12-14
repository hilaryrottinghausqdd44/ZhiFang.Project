using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.IBLL.Common.BaseDictionary;
namespace ZhiFang.BLL.Common.BaseDictionary
{
    public partial class barCodeSeq : IBbarCodeSeq
    {
        IDAL.IDbarCodeSeq dal;
        public barCodeSeq()
        {
            dal = DALFactory.DalFactory<IDAL.IDbarCodeSeq>.GetDal("barCodeSeq", ZhiFang.Common.Dictionary.DBSource.LisDB());
        }

        public string GetMaxBarCodeOrderNum(string LabCode, string Operdate)
        {
            return dal.ExecStoredProcedure(LabCode, Operdate);
        }

        public string GetBarCode(string LabCode, string Operdate)
        {
            string LastNum = dal.ExecStoredProcedure(LabCode, Operdate);
            string tempLabCode = string.Empty;
            if (LabCode.Length == 1)
                tempLabCode = "00" + LabCode;
            else if (LabCode.Length == 2)
                tempLabCode = "0" + LabCode;
            else
                tempLabCode = LabCode;
            try
            {
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("weblisbarcode") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("weblisbarcode") != "")
                    tempLabCode = ZhiFang.Common.Public.ConfigHelper.GetConfigString("weblisbarcode");
            }
            catch (Exception)
            {
            }
            return tempLabCode + DateTime.Now.ToString("yyMMdd") + LastNum;

        }
        public int Add(Model.barCodeSeq model)
        {
            throw new NotImplementedException();
        }

        public int Update(Model.barCodeSeq model)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet GetList(Model.barCodeSeq model)
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount(Model.barCodeSeq model)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet GetAllList()
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet GetListByPage(Model.barCodeSeq t, int nowPageNum, int nowPageSize)
        {
            throw new NotImplementedException();
        }

        int IBLL.Common.IBBase<Model.barCodeSeq>.Add(Model.barCodeSeq model)
        {
            throw new NotImplementedException();
        }

        int IBLL.Common.IBBase<Model.barCodeSeq>.Update(Model.barCodeSeq model)
        {
            throw new NotImplementedException();
        }

        System.Data.DataSet IBLL.Common.IBBase<Model.barCodeSeq>.GetList(Model.barCodeSeq model)
        {
            throw new NotImplementedException();
        }

        int IBLL.Common.IBBase<Model.barCodeSeq>.GetTotalCount()
        {
            throw new NotImplementedException();
        }

        int IBLL.Common.IBBase<Model.barCodeSeq>.GetTotalCount(Model.barCodeSeq model)
        {
            throw new NotImplementedException();
        }

        System.Data.DataSet IBLL.Common.IBBase<Model.barCodeSeq>.GetAllList()
        {
            throw new NotImplementedException();
        }

        System.Data.DataSet IBLL.Common.IBDataPage<Model.barCodeSeq>.GetListByPage(Model.barCodeSeq t, int nowPageNum, int nowPageSize)
        {
            throw new NotImplementedException();
        }
    }
}
