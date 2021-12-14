using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.DAO.ADO;

namespace ZhiFang.Digitlab.ReagentSys
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ReaADOService : ZhiFang.Digitlab.ReagentSys.ServerContract.IReaADOService
    {  
        IBLL.ReagentSys.ADO.IBLabInStock IBLabInStock { get; set; }

        public BaseResultDataValue RADOS_UDTO_CheckDataBaseLink(string orgNo, string orgName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                brdv = IBLabInStock.CheckDataBaseLink(orgNo, orgName);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：" + ex.Message;
            }
            return brdv;
        }

        public BaseResultDataValue RADOS_UDTO_CheckDataBaseLinkByConnectStr(string orgNo, string orgName, string dbConnectStr)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue.success = ZhiFang.Digitlab.DAO.ADO.SqlServerHelper.TestDataBaseConnection(dbConnectStr);
                if (!baseResultDataValue.success)
                {
                    baseResultDataValue.ErrorInfo = "错误信息：机构【" + orgName + "】数据库链接失败！";
                    ZhiFang.Common.Log.Log.Error(baseResultDataValue.ErrorInfo);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue RADOS_UDTO_GetDataBaseLink(string orgNo, string orgName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                brdv = DataBaseLink.GetDataBaseLinkByOrgNo(orgNo, orgName);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：" + ex.Message;
            }
            return brdv;
        }

        public BaseResultDataValue RADOS_UDTO_GetLabInStockCount(string orgNo, string orgName, string goodsID, string goodsNo, string prodGoodsNo, string goodsLotNo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                brdv = IBLabInStock.GetLabInStockCount(orgNo, orgName, goodsID, goodsNo, prodGoodsNo, goodsLotNo);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：" + ex.Message;
            }
            return brdv;
        }
        public BaseResultDataValue RADOS_UDTO_GetTestConsumeCountResult(string orgNo, string orgName, string beginDate, string endDate, string goodsID, string goodsNo, string prodGoodsNo, string goodsLotNo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(beginDate) || string.IsNullOrEmpty(endDate))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "错误信息：请指定日期范围！";
                    return brdv;
                }
                brdv = IBLabInStock.GetTestConsumeCountResult(orgNo, orgName, beginDate, endDate, goodsID, goodsNo, prodGoodsNo, goodsLotNo);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：" + ex.Message;
            }
            return brdv;
        }

    }
}
