using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Model;
using System.Data;

namespace ZhiFang.IDAL
{
    public interface IDReportItem : IDataBase<ZhiFang.Model.ReportItem>
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int ItemNo, string FormNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int ItemNo, string FormNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.ReportItem GetModel(int ItemNo, string FormNo);
        /// <summary>
        /// 根据FormNo返回ReportForm包含的ReportItem列表
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <returns></returns>
        DataTable GetReportItemList(string FormNo);
        /// <summary>
        /// 根据FormNo返回ReportForm包含的ReportItem列表
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <returns></returns>
        DataTable GetReportItemFullList(string FormNo);
    }
}
