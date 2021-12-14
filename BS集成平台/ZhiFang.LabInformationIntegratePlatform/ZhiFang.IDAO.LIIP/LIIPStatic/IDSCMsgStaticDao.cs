using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.IDAO.Base;


namespace ZhiFang.IDAO.LIIP
{
	public interface IDSCMsgStaticDao
	{
		/// <summary>
		/// 统计消息的确认和处理的及时率
		/// </summary>
		/// <param name="LabCode"></param>
		/// <param name="StartDate"></param>
		/// <param name="EndDdate"></param>
		/// <param name="SickTypeId"></param>
		/// <param name="MsgTypeCodes"></param>
		/// <param name="DeptType"></param>
		/// <param name="DId"></param>
		/// <returns></returns>
		DataTable Static_SCMsg_Confirm_Handle_TimelyPerc(string LabCode, string StartDate, string EndDdate, string SickTypeId, string MsgTypeCodes, int DeptType, string DId);

		/// <summary>
		/// 统计消息的确认和处理的时长的平均值和中位数
		/// </summary>
		/// <param name="LabCode"></param>
		/// <param name="StartDate"></param>
		/// <param name="EndDdate"></param>
		/// <param name="SickTypeId"></param>
		/// <param name="MsgTypeCodes"></param>
		/// <param name="DeptType"></param>
		/// <param name="DId"></param>
		/// <returns></returns>
		DataTable Static_SCMsg_Confirm_Handle_TimeRange(string LabCode, string StartDate, string EndDdate, string SickTypeId, string MsgTypeCodes, int DeptType, string DId);
        DataTable Static_SCMsg_AllHandleFinish_YOYTimeRange_Dept(string labCode, int rangType, int year, int quarter, int month, string sickTypeId, string msgTypeCodes, string DeptId, int dataType);

		DataTable Static_SCMsg_AllHandleFinish_YOYTimeRange_District(string labCode, int rangType, int year, int quarter, int month, string sickTypeId, string msgTypeCodes, string DistrictId, int dataType);

		DataTable Static_SCMsg_AllHandleFinish_MOMTimeRange_Dept(string LabCode, int RangType, int Year, int Quarter, int Month, string SickTypeId, string MsgTypeCodes, string DId, int DataType);

		DataTable Static_SCMsg_AllHandleFinish_MOMTimeRange_District(string LabCode, int RangType, int Year, int Quarter, int Month, string SickTypeId, string MsgTypeCodes, string DId, int DataType);
	} 
}