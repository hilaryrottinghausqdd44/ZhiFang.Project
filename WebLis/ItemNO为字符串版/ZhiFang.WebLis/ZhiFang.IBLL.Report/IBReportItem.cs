using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;

namespace ZhiFang.IBLL.Report
{
    public interface IBReportItem:IBLLBase<Model.ReportItem>
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
        /// 得到一个对象实体，从缓存中。
        /// </summary>
        Model.ReportItem GetModelByCache(int ItemNo, string FormNo);
        /// <summary>
        /// 根据FormNo返回ReportForm包含的ReportItem列表
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <returns></returns>
        DataTable GetReportItemList_DataTable(string FormNo);
        /// <summary>
        /// 根据FormNo返回ReportForm包含的ReportItem列表
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <returns></returns>
        SortedList GetReportItemList_SortedList(string FormNo);
        /// <summary>
        /// 根据FormNo返回ReportForm包含的ParItem列表
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <returns></returns>
        SortedList GetReportItemList_ItemGroup(string FormNo);

      
    }
}
