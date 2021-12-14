using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Report
{
    public interface IBReportMicro : IBLLBase<Model.ReportMicro>
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int ResultNo, int ItemNo, string FormNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int ResultNo, int ItemNo, string FormNo);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.ReportMicro GetModel(int ResultNo, int ItemNo, string FormNo);

        /// <summary>
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        Model.ReportMicro GetModelByCache(int ResultNo, int ItemNo, string FormNo);
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
        DataTable GetReportMicroList(string FormNo, string ItemNo);
        /// <summary>
        /// 根据FormNo返回ReportForm包含的ReportItem列表
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <returns></returns>
        DataTable GetReportMicroList(string FormNo, string ItemNo, string MicroNo);
    }
}
