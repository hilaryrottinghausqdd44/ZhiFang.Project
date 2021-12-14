using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.BLL.Common
{
    public class DataDownload : ZhiFang.IBLL.Common.IBDataDownload
    {
        #region IDataDownload 成员
        IDAL.IDLogInfo LogInfo_dal = DALFactory.DalFactory<IDAL.IDLogInfo>.GetDal("D_LogInfo", ZhiFang.Common.Dictionary.DBSource.LisDB());

        public int GetDictionaryXML(string LabCode, int time, out string strXML, out string strXMLSchema, out string strMsg)
        {
            throw new NotImplementedException();
        }
        
        public int GetDictionaryNameListXML(int time, string LabCode, out string strXML, out string strMsg)
        {
            try
            {
                Model.LogInfo model_logInfo = new Model.LogInfo();
                model_logInfo.IntTimeStampe = time;
                DataSet dsDicList = LogInfo_dal.GetListByTimeStampe(model_logInfo);
                if (dsDicList != null && dsDicList.Tables.Count > 0 && dsDicList.Tables[0].Rows.Count > 0)
                {
                    dsDicList.Tables[0].TableName = "DicTables";
                    strXML = dsDicList.GetXml();
                    strMsg = "通过服务获取XML成功";
                }
                else
                {
                    strXML = "";
                    strMsg = "当前字典版本最新，不需要更新";
                }                
                return 1;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("ZhiFang.BLL.Common.DataDownload.GetDictionaryNameListXML---->参数time=" + time + "；LabCode=" + LabCode, ex);
                strXML = "";
                strMsg = "失败，获取待更新字典列表时出错：" + ex.ToString();
                return 0;
            }
        }
                
        #endregion
    }
}
