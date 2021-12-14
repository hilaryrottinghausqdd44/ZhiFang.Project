using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using ZhiFang.IBLL.Report;
using ZhiFang.BLLFactory;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.Model;

namespace ZhiFang.WebLisService
{
    /// <summary>
    /// ReTestCheckService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "ZhiFang.WebLis")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class ReTestCheckService : System.Web.Services.WebService
    {
        private readonly IBReportFormFull ibrff = BLLFactory<IBReportFormFull>.GetBLL("ReportFormFull");
        private readonly IBSearchReCheckLog ibsrc = BLLFactory<IBSearchReCheckLog>.GetBLL("BaseDictionary.SearchReCheckLog");
        /// <summary>
        /// 重复检验提醒
        /// </summary>
        /// <param name="BUSINESS_ID">业务流水号</param>
        /// <param name="ORGANIZATION_CODE">组织机构代码</param>
        /// <param name="ORGANIZATION_NAME">组织机构名称</param>
        /// <param name="REGION_IDEN">地区标识</param>
        /// <param name="SYS_CODE">源系统代码</param>
        /// <param name="SYS_NAME">源系统名称</param>
        /// <param name="ORG_CODE">目标机构代码</param>
        /// <param name="ORG_NAME">目标机构名称</param>
        /// <param name="TASK_TYPE">消息对象</param>
        /// <param name="PERSON_NAME">居民姓名</param>
        /// <param name="CERT_TYPE">居民证件类型</param>
        /// <param name="CERT_NAME">居民证件名称</param>
        /// <param name="CERT_NUMBER">居民证件号码</param>
        /// <param name="PERSON_TEL">居民联系信息</param>
        /// <param name="TASK_DESC">消息提醒分类代码</param>
        /// <param name="DOCTOR_ID">干系医生id</param>
        /// <param name="DOCTOR_NAME">干系医生姓名</param>
        /// <param name="ReturnDescription">返回值描述</param>
        /// <returns>int。1：重复，0：不重复,-1:异常</returns>
        [WebMethod(Description = "重复检验提醒")]
        public int ReTestCheck(string BUSINESS_ID, string ORGANIZATION_CODE, string ORGANIZATION_NAME, string REGION_IDEN, string SYS_CODE, string SYS_NAME, string ORG_CODE, string ORG_NAME, string TASK_TYPE, string PERSON_NAME, string CERT_TYPE, string CERT_NAME, string CERT_NUMBER, string PERSON_TEL, string TASK_DESC, string DOCTOR_ID, string DOCTOR_NAME, out string ReturnDescription)
        {
            try
            {                
                string paralog = "";
                #region 参数判断
                if (BUSINESS_ID == null || BUSINESS_ID.Trim() == "")
                {
                    ReturnDescription = "BUSINESS_ID为空！";
                    ZhiFang.Common.Log.Log.Info("BUSINESS_ID为空！");
                    return -1;
                }
                if (ORGANIZATION_CODE == null || ORGANIZATION_CODE.Trim() == "")
                {
                    ReturnDescription = "ORGANIZATION_CODE为空！";
                    ZhiFang.Common.Log.Log.Info("ORGANIZATION_CODE为空！");
                    return -1;
                }
                if (ORGANIZATION_NAME == null || ORGANIZATION_NAME.Trim() == "")
                {
                    ReturnDescription = "ORGANIZATION_NAME为空！";
                    ZhiFang.Common.Log.Log.Info("ORGANIZATION_NAME为空！");
                    return -1;
                }
                if (REGION_IDEN == null || REGION_IDEN.Trim() == "")
                {
                    ReturnDescription = "REGION_IDEN为空！";
                    ZhiFang.Common.Log.Log.Info("REGION_IDEN为空！");
                    return -1;
                }
                if (SYS_CODE == null || SYS_CODE.Trim() == "")
                {
                    ReturnDescription = "SYS_CODE为空！";
                    ZhiFang.Common.Log.Log.Info("SYS_CODE为空！");
                    return -1;
                }
                if (SYS_NAME == null || SYS_NAME.Trim() == "")
                {
                    ReturnDescription = "SYS_NAME为空！";
                    ZhiFang.Common.Log.Log.Info("SYS_NAME为空！");
                    return -1;
                }
                if (ORG_CODE == null || ORG_CODE.Trim() == "")
                {
                    ReturnDescription = "ORG_CODE为空！";
                    ZhiFang.Common.Log.Log.Info("ORG_CODE为空！");
                    return -1;
                }
                if (ORG_NAME == null || ORG_NAME.Trim() == "")
                {
                    ReturnDescription = "ORG_NAME为空！";
                    ZhiFang.Common.Log.Log.Info("ORG_NAME为空！");
                    return -1;
                }
                if (TASK_TYPE == null || TASK_TYPE.Trim() == "")
                {
                    ReturnDescription = "TASK_TYPE为空！";
                    ZhiFang.Common.Log.Log.Info("TASK_TYPE为空！");
                    return -1;
                }
                if (PERSON_NAME == null || PERSON_NAME.Trim() == "")
                {
                    ReturnDescription = "PERSON_NAME为空！";
                    ZhiFang.Common.Log.Log.Info("PERSON_NAME为空！");
                    return -1;
                }
                if (CERT_TYPE == null || CERT_TYPE.Trim() == "")
                {
                    ReturnDescription = "CERT_TYPE为空！";
                    ZhiFang.Common.Log.Log.Info("CERT_TYPE为空！");
                    return -1;
                }
                if (CERT_NAME == null || CERT_NAME.Trim() == "")
                {
                    ReturnDescription = "CERT_NAME为空！";
                    ZhiFang.Common.Log.Log.Info("CERT_NAME为空！");
                    return -1;
                }
                if (CERT_NUMBER == null || CERT_NUMBER.Trim() == "")
                {
                    ReturnDescription = "CERT_NUMBER为空！";
                    ZhiFang.Common.Log.Log.Info("CERT_NUMBER为空！");
                    return -1;
                }
                //if (PERSON_TEL == null || PERSON_TEL.Trim() == "")
                //{
                //    ReturnDescription = "PERSON_TEL为空！";
                //    ZhiFang.Common.Log.Log.Info("PERSON_TEL为空！");
                //    return -1;
                //}
                if (TASK_DESC == null || TASK_DESC.Trim() == "")
                {
                    ReturnDescription = "TASK_DESC为空！";
                    ZhiFang.Common.Log.Log.Info("TASK_DESC为空！");
                    return -1;
                }
                //if (DOCTOR_ID == null || DOCTOR_ID.Trim() == "")
                //{
                //    ReturnDescription = "DOCTOR_ID为空！";
                //    ZhiFang.Common.Log.Log.Info("DOCTOR_ID为空！");
                //    return -1;
                //}
                //if (DOCTOR_NAME == null || DOCTOR_NAME.Trim() == "")
                //{
                //    ReturnDescription = "DOCTOR_NAME为空！";
                //    ZhiFang.Common.Log.Log.Info("DOCTOR_NAME为空！");
                //    return -1;
                //}
                #endregion
                paralog = "BUSINESS_ID:" + BUSINESS_ID;
                paralog += "ORGANIZATION_CODE:" + ORGANIZATION_CODE;
                paralog += "ORGANIZATION_NAME:" + ORGANIZATION_NAME;
                paralog += "REGION_IDEN:" + REGION_IDEN;
                paralog += "SYS_CODE:" + SYS_CODE;
                paralog += "SYS_NAME:" + SYS_NAME;
                paralog += "ORG_CODE:" + ORG_CODE;
                paralog += "ORG_NAME:" + ORG_NAME;
                paralog += "TASK_TYPE:" + TASK_TYPE;
                paralog += "PERSON_NAME:" + PERSON_NAME;
                paralog += "CERT_TYPE:" + CERT_TYPE;
                paralog += "CERT_NAME:" + CERT_NAME;
                paralog += "CERT_NUMBER:" + CERT_NUMBER;
                paralog += "PERSON_TEL:" + PERSON_TEL;
                paralog += "TASK_DESC:" + TASK_DESC;
                paralog += "DOCTOR_ID:" + DOCTOR_ID;
                paralog += "DOCTOR_NAME:" + DOCTOR_NAME;
                ZhiFang.Common.Log.Log.Info("ReTestCheck调用。参数(" + paralog + ")");
                string DOMAIN_CODE = "340200200000";
                string DOMAIN_NAME = "区域LIS系统";
                string UNIQUEID = DOMAIN_CODE + "TX001" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string BASIC_ACTIVE_ID = DOMAIN_CODE + "TX001" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string DATAGENERATE_DATE = DateTime.Now.ToString("yyyyMMddHHmmss");
                string TASK_TIME = DateTime.Now.ToString("yyyyMMddHHmmss");
                string BUS_RESULT_CODE = "";
                string BUS_RESULT_DESC = "";
                ReturnDescription = "";
                
                #region 对象赋值
                SearchReCheckLog entity=new SearchReCheckLog();
                entity.Id = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
                entity.BUSINESS_ID=BUSINESS_ID;
                entity.ORGANIZATION_CODE=ORGANIZATION_CODE;
                entity.ORGANIZATION_NAME=ORGANIZATION_NAME;
                entity.REGION_IDEN=REGION_IDEN;
                entity.SYS_CODE=SYS_CODE;
                entity.SYS_NAME=SYS_NAME;
                entity.ORG_CODE=ORG_CODE;
                entity.ORG_NAME=ORG_NAME;
                entity.TASK_TYPE=TASK_TYPE;
                entity.PERSON_NAME=PERSON_NAME;
                entity.CERT_TYPE=CERT_TYPE;
                entity.CERT_NAME=CERT_NAME;
                entity.CERT_NUMBER=CERT_NUMBER;
                entity.PERSON_TEL=PERSON_TEL;
                entity.TASK_DESC=TASK_DESC;
                entity.DOCTOR_ID=DOCTOR_ID;
                entity.DOCTOR_NAME=DOCTOR_NAME;
                entity.DOMAIN_CODE=DOMAIN_CODE;
                entity.DOMAIN_NAME=DOMAIN_NAME;
                entity.UNIQUEID=UNIQUEID;
                entity.BASIC_ACTIVE_ID=BASIC_ACTIVE_ID;
                entity.DATAGENERATE_DATE=DATAGENERATE_DATE;
                entity.TASK_TIME=TASK_TIME;
                #endregion
                int days = -7;
                string ReTestCheckReceivedate = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReTestCheckReceivedate");
                
                if (ReTestCheckReceivedate != "")
                    days = Convert.ToInt32(ReTestCheckReceivedate);

                string wherestr = " zdy5='" + CERT_NUMBER + "' and Receivedate>='" + DateTime.Now.AddDays(days).ToString("yyyy-MM-dd") + "' ";
                if (ibrff.Count(wherestr) > 0)
                {
                    BUS_RESULT_CODE = "1";
                    BUS_RESULT_DESC = "是";
                    entity.BUS_RESULT_CODE = BUS_RESULT_CODE;
                    entity.BUS_RESULT_DESC = BUS_RESULT_DESC;
                    ibsrc.Add(entity);
                    string weblisreporturl=ZhiFang.Common.Public.ConfigHelper.GetConfigString("WebLisReportServiceUrl");
                    if (weblisreporturl == null || weblisreporturl.Trim() == "")
                    {
                         ReturnDescription = "未配置报告系统路径";
                        ZhiFang.Common.Log.Log.Info("未配置报告系统路径(WebLisReportServiceUrl)!");
                    }
                    else
                    {
                        ReturnDescription = weblisreporturl + "?ZDY5=" + CERT_NUMBER + "&StartDate=" + DateTime.Now.AddDays(days).ToString("yyyy-MM-dd") + ",0";
                    }
                    
                    return 1;
                }
                else
                {
                    BUS_RESULT_CODE = "2";
                    BUS_RESULT_DESC = "否";
                    entity.BUS_RESULT_CODE = BUS_RESULT_CODE;
                    entity.BUS_RESULT_DESC = BUS_RESULT_DESC;
                    ibsrc.Add(entity);
                    return 0;
                }
                return -1;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Info("ReTestCheck调用。异常：" + e.ToString());
                ReturnDescription = "重复检验提醒服务调用异常。";
                return -1;
            }
        }
    }
}
