using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Model;
using System.Data;

namespace ZhiFang.IDAL
{
	/// <summary>
	/// 接口层IReportMicro 的摘要说明。
	/// </summary>
    public interface IDReportMicro : IDataBase<ZhiFang.Model.ReportMicro>
	{
		#region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int ResultNo,int ItemNo,string FormNo);
		/// <summary>
		/// 删除一条数据
		/// </summary>
        int Delete(int ResultNo, int ItemNo, string FormNo);
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        Model.ReportMicro GetModel(int ResultNo, int ItemNo, string FormNo);
        /// <summary>
        /// 根据FormNo返回ReportForm包含的ReportItem列表
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <returns></returns>
        DataTable GetReportMicroList(string FormNo);
        /// <summary>
        /// 根据FormNo返回ReportForm包含的ReportItem列表
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <returns></returns>
        DataTable GetReportMicroList(string FormNo,string ItemNo);
        /// <summary>
        /// 根据FormNo返回ReportForm包含的ReportItem列表
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <returns></returns>
        DataTable GetReportMicroList(string FormNo,string ItemNo,string MicroNo);
		#endregion  成员方法
	}
}
