using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json.Linq;
using ZhiFang.BloodTransfusion.Common;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.BloodTransfusion.hisinterface
{
    /// <summary>
    /// 用血申请与HIS交互辅助类
    /// </summary>
    public static class BReqToHisHelp
    {
        //正在与HIS交互的业务单的Id集合
        public static IList<string> ToHisCurIdList = new List<string>();

        /// <summary>
        /// 用血申请单上传HIS
        /// </summary>
        /// <returns></returns>
        public static BaseResult UploadReqDataToHis(BloodBReqForm entity, BloodsConfig bloodsConfigVO)
        {
            if (!BReqToHisHelp.ToHisCurIdList.Contains(entity.Id.ToString()))
                BReqToHisHelp.ToHisCurIdList.Add(entity.Id.ToString());//添加正在上传的业务单Id
            BaseResult baseresultdata = new BaseResult();
            ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",上传HIS调用开始!");
            string url = bloodsConfigVO.CSServer.CS_TOHISDATA_URL + "?sBreqFormID=" + entity.Id;

            string resultStr = WebRequestHelp.Get(url, WebRequestHelp.TIME_OUT_MILLISECOND);
            ZhiFang.Common.Log.Log.Debug(string.Format("返回结果为:{0}", resultStr));
            ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",上传HIS调用结束!");
            if (!string.IsNullOrEmpty(resultStr))
            {
                JObject jresult = JObject.Parse(resultStr);
                if (jresult["success"] != null)
                {
                    baseresultdata.InterfaceSuccess = bool.Parse(jresult["success"].ToString());
                    baseresultdata.InterfaceMsg = jresult["ErrorInfo"].ToString();
                }
                else
                {
                    baseresultdata.InterfaceSuccess = false;
                    baseresultdata.InterfaceMsg = "用血申请单号为:" + entity.Id + ",上传HIS返回结果异常!";
                    //return baseresultdata;
                }
            }
            else
            {
                baseresultdata.InterfaceSuccess = false;
                baseresultdata.InterfaceMsg = "用血申请单号为:" + entity.Id + ",上传HIS返回结果为空!";
                //return baseresultdata;
            }
            //移除正在上传的业务单Id
            if (BReqToHisHelp.ToHisCurIdList.Contains(entity.Id.ToString()))
                BReqToHisHelp.ToHisCurIdList.Remove(entity.Id.ToString());
            return baseresultdata;
        }
        /// <summary>
        /// 用血申请作废(BS调用CS服务,CS服务调用HIS作废接口作废)", Desc = "用血申请作废(BS调用CS服务,CS服务调用HIS作废接口作废)
        /// </summary>
        /// <returns></returns>
        public static BaseResult ObsoleteReqDataToHis(BloodBReqForm entity, BloodsConfig bloodsConfigVO,SysCurUserInfo curDoctor)
        {
            if (!BReqToHisHelp.ToHisCurIdList.Contains(entity.Id.ToString()))
                BReqToHisHelp.ToHisCurIdList.Add(entity.Id.ToString());//添加正在上传的业务单Id
            BaseResult baseresultdata = new BaseResult();
            ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",BS调用CS作废服务开始!");
           string hisOperID = curDoctor.HisDoctorId;
            string url = bloodsConfigVO.CSServer.CS_TOHISOBSOLETE_URL + "?sBreqFormID=" + entity.Id;
            url = url + "&hisOperID=" + hisOperID;

            string resultStr = WebRequestHelp.Get(url, WebRequestHelp.TIME_OUT_MILLISECOND);
            ZhiFang.Common.Log.Log.Debug(string.Format("返回结果为:{0}", resultStr));
            ZhiFang.Common.Log.Log.Debug("用血申请单号为:" + entity.Id + ",BS调用CS作废服务结束!");
            if (!string.IsNullOrEmpty(resultStr))
            {
                JObject jresult = JObject.Parse(resultStr);
                if (jresult["success"] != null)
                {
                    baseresultdata.InterfaceSuccess = bool.Parse(jresult["success"].ToString());
                    baseresultdata.InterfaceMsg = jresult["ErrorInfo"].ToString();
                }
                else
                {
                    baseresultdata.InterfaceSuccess = false;
                    baseresultdata.InterfaceMsg = "用血申请单号为:" + entity.Id + ",BS调用CS作废服务返回结果异常!";
                    //return baseresultdata;
                }
            }
            else
            {
                baseresultdata.InterfaceSuccess = false;
                baseresultdata.InterfaceMsg = "用血申请单号为:" + entity.Id + ",BS调用CS作废服务返回结果为空!";
                //return baseresultdata;
            }
            //移除正在作废的业务单Id
            if (BReqToHisHelp.ToHisCurIdList.Contains(entity.Id.ToString()))
                BReqToHisHelp.ToHisCurIdList.Remove(entity.Id.ToString());
            return baseresultdata;
        }
    }
}