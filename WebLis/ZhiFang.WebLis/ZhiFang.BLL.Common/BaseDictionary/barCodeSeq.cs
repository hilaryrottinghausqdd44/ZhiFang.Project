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
            string dateformat = "yyMMdd";
            string BarCodeLabCodePlaceStr = "0";

            int LabCodeLength = 3;
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("BarCodeDateFormat") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("BarCodeDateFormat") != "")
                dateformat = ZhiFang.Common.Public.ConfigHelper.GetConfigString("BarCodeDateFormat");

            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("BarCodeLabCodeLength") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("BarCodeLabCodeLength") != "")
                LabCodeLength = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("BarCodeLabCodeLength");

            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("BarCodeLabCodePlaceStr") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("BarCodeLabCodePlaceStr") != "")
                BarCodeLabCodePlaceStr = ZhiFang.Common.Public.ConfigHelper.GetConfigString("BarCodeLabCodePlaceStr");

            int tmp = LabCodeLength - LabCode.Length;
            tempLabCode = LabCode;
            if (tmp > 0)
            {
                for (int i = 0; i < tmp; i++)
                {
                    tempLabCode = BarCodeLabCodePlaceStr + tempLabCode;
                }
            }
            try
            {
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("weblisbarcode") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("weblisbarcode") != "")
                    tempLabCode = ZhiFang.Common.Public.ConfigHelper.GetConfigString("weblisbarcode");
            }
            catch (Exception)
            {
            }
            //ZhiFang.Common.Log.Log.Debug("GetBarCode.dateformat="+ dateformat);
            //ZhiFang.Common.Log.Log.Debug("GetBarCode.BarCode=" + tempLabCode + DateTime.Now.ToString(dateformat) + LastNum);
            return tempLabCode + DateTime.Now.ToString(dateformat).Substring(DateTime.Now.ToString(dateformat).Length-dateformat.Length) + LastNum;

        }

        public string GetBarCode(string LabCode, string Operdate,string DateFormat="yyMMdd",int LabCodeLength=3)
        {
            string tempLabCode = string.Empty;
            try
            {
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("weblisbarcode") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("weblisbarcode") != "")
                    tempLabCode = ZhiFang.Common.Public.ConfigHelper.GetConfigString("weblisbarcode");
                else
                {
                    int tmp = LabCodeLength - LabCode.Length;
                    tempLabCode = LabCode;
                    if (tmp > 0)
                    {
                        for (int i = 0; i < tmp; i++)
                        {
                            tempLabCode = "0" + tempLabCode;
                        }
                    }
                }
                string LastNum = dal.ExecStoredProcedure(LabCode, Operdate);
                return tempLabCode + DateTime.Now.ToString(DateFormat) + LastNum;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.BaseDictionary.barCodeSeq.GetBarCode异常：" + ex.ToString());
                throw (ex);
            }
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
