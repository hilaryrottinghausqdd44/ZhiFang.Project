using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.LabInformationIntegratePlatform.ServerContract;

namespace ZhiFang.LabInformationIntegratePlatform.ServerWCF
{

    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class LIIPStaticService : ILIIPStaticService
    {
        IDAO.LIIP.IDSCMsgStaticDao IDSCMsgStaticDao { get; set; }
        public BaseResultDataValue Static_SCMsg_Confirm_Handle_TimelyPerc(string LabCode, string StartDate, string EndDdate, string SickTypeId, string MsgTypeCodes, int DeptType, string DId)
        {

            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(LabCode))
                {
                    LabCode = "";
                }
                if (DateTime.TryParse(StartDate, out var startdatetime))
                {
                    StartDate = startdatetime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (DateTime.TryParse(EndDdate, out var enddatetime))
                {
                    EndDdate = enddatetime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (string.IsNullOrEmpty(MsgTypeCodes))
                {
                    return new BaseResultDataValue() { ErrorInfo = "消息类型参数错误！", success = true };
                }
                DataTable dt = IDSCMsgStaticDao.Static_SCMsg_Confirm_Handle_TimelyPerc(LabCode, StartDate, EndDdate, SickTypeId, MsgTypeCodes, DeptType, DId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    brdv.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
                }

                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ".Static_SCMsg_TimelyPerc.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                return brdv;
            }

        }

        public BaseResultDataValue Static_SCMsg_Confirm_Handle_TimeRange(string LabCode, string StartDate, string EndDdate, string SickTypeId, string MsgTypeCodes, int DeptType, string DId)
        {

            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(LabCode))
                {
                    LabCode = "";
                }
                if (DateTime.TryParse(StartDate, out var startdatetime))
                {
                    StartDate = startdatetime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (DateTime.TryParse(EndDdate, out var enddatetime))
                {
                    EndDdate = enddatetime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                if (string.IsNullOrEmpty(MsgTypeCodes))
                {
                    return new BaseResultDataValue() { ErrorInfo = "消息类型参数错误！", success = true };
                }
                DataTable dt = IDSCMsgStaticDao.Static_SCMsg_Confirm_Handle_TimeRange(LabCode, StartDate, EndDdate, SickTypeId, MsgTypeCodes, DeptType, DId);
                if (dt != null && dt.Rows.Count > 0)
                {
                    brdv.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
                }

                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ".Static_SCMsg_Confirm_Handle_TimeRange.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                return brdv;
            }

        }

        /// <summary>
        /// 全部处理完成时间同比统计
        /// </summary>
        /// <param name="LabCode"></param>
        /// <param name="RangType">1年\2季度\3月</param>
        /// <param name="Year">年</param>
        /// <param name="Quarter">季度</param>
        /// <param name="Month">月</param>
        /// <param name="SickTypeId">就诊类型</param>
        /// <param name="MsgTypeCodes">消息编码</param>
        /// <param name="DeptType">部门病区类型</param>
        /// <param name="DId">部门\病区ID</param>
        /// <param name="DataType">数据类型,1总数量,2及时率</param>
        /// <returns></returns>
        public BaseResultDataValue Static_SCMsg_AllHandleFinish_YOYTimeRange(string LabCode, int RangType, int Year, int Quarter, int Month, string SickTypeId, string MsgTypeCodes, int DeptType, string DId, int DataType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(LabCode))
                {
                    LabCode = "";
                }
                if (RangType <= 0)
                {
                    return new BaseResultDataValue() { ErrorInfo = "同比周期类型参数错误！", success = true };
                }
                if (Year <= 0)
                {
                    return new BaseResultDataValue() { ErrorInfo = "同比年份参数错误！", success = true };
                }
                if (DataType <= 0)
                {
                    return new BaseResultDataValue() { ErrorInfo = "同比数据类型参数错误！", success = true };
                }
                switch (RangType)
                {
                    case 2:
                        if (Quarter <= 0 || Quarter >= 5)
                            return new BaseResultDataValue() { ErrorInfo = "同比季度参数错误！", success = true };
                        break;
                    case 3:
                        if (Month <= 0 || Month >= 13)
                            return new BaseResultDataValue() { ErrorInfo = "同比月份参数错误！", success = true };
                        break;
                }
                if (string.IsNullOrEmpty(MsgTypeCodes))
                {
                    return new BaseResultDataValue() { ErrorInfo = "消息类型参数错误！", success = true };
                }
                DataTable dt = new DataTable();
                if (DeptType == 0)
                {
                    dt=IDSCMsgStaticDao.Static_SCMsg_AllHandleFinish_YOYTimeRange_Dept(LabCode, RangType, Year, Quarter, Month, SickTypeId, MsgTypeCodes, DId, DataType);
                }
                else
                {
                    dt=IDSCMsgStaticDao.Static_SCMsg_AllHandleFinish_YOYTimeRange_District(LabCode, RangType, Year, Quarter, Month, SickTypeId, MsgTypeCodes, DId, DataType);
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    brdv.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
                }

                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ".Static_SCMsg_AllHandleFinish_YOYTimeRange.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                return brdv;
            }

        }

        /// <summary>
        /// 全部处理完成时间环比统计
        /// </summary>
        /// <param name="LabCode"></param>
        /// <param name="RangType">1年\2季度\3月</param>
        /// <param name="Year">年</param>
        /// <param name="Quarter">季度</param>
        /// <param name="Month">月</param>
        /// <param name="SickTypeId">就诊类型</param>
        /// <param name="MsgTypeCodes">消息编码</param>
        /// <param name="DeptType">部门病区类型</param>
        /// <param name="DId">部门\病区ID</param>
        /// <param name="DataType">数据类型,1总数量,2及时率</param>
        /// <returns></returns>
        public BaseResultDataValue Static_SCMsg_Confirm_Handle_MOMTimeRange(string LabCode, int RangType, int Year, int Quarter, int Month, string SickTypeId, string MsgTypeCodes, int DeptType, string DId, int DataType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(LabCode))
                {
                    LabCode = "";
                }
                if (RangType <= 0)
                {
                    return new BaseResultDataValue() { ErrorInfo = "环比周期类型参数错误！", success = true };
                }
                if (Year <= 0)
                {
                    return new BaseResultDataValue() { ErrorInfo = "环比年份参数错误！", success = true };
                }
                if (DataType <= 0)
                {
                    return new BaseResultDataValue() { ErrorInfo = "环比数据类型参数错误！", success = true };
                }
                switch (RangType)
                {
                    case 2:
                        if (Quarter <= 0 || Quarter >= 5)
                            return new BaseResultDataValue() { ErrorInfo = "环比季度参数错误！", success = true };
                        break;
                    case 3:
                        if (Month <= 0 || Month >= 13)
                            return new BaseResultDataValue() { ErrorInfo = "环比月份参数错误！", success = true };
                        break;
                }
                if (string.IsNullOrEmpty(MsgTypeCodes))
                {
                    return new BaseResultDataValue() { ErrorInfo = "消息类型参数错误！", success = true };
                }
                DataTable dt = new DataTable();
                if (DeptType == 0)
                {
                    dt = IDSCMsgStaticDao.Static_SCMsg_AllHandleFinish_MOMTimeRange_Dept(LabCode, RangType, Year, Quarter, Month, SickTypeId, MsgTypeCodes, DId, DataType);
                }
                else
                {
                    dt = IDSCMsgStaticDao.Static_SCMsg_AllHandleFinish_MOMTimeRange_District(LabCode, RangType, Year, Quarter, Month, SickTypeId, MsgTypeCodes, DId, DataType);
                }
                if (dt != null && dt.Rows.Count > 0)
                {
                    brdv.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
                }

                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error(this.GetType().FullName + ".Static_SCMsg_Confirm_Handle_MOMTimeRange.异常：" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "程序异常！";
                return brdv;
            }

        }
    }
}
