using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Model;
using System.Data;

namespace ZhiFang.IDAL
{
    public interface IDReportForm : IDataBase<ZhiFang.Model.ReportForm>
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string FormNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(string FormNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Model.ReportForm GetModel(string FormNo);
        /// <summary>
        /// 根据FormNo数组返回ReportForm列表
        /// </summary>
        /// <param name="FormNo">FormNo数组</param>
        /// <returns></returns>
        //DataTable GetReportFormList(string[] FormNo);
        /// <summary>
        /// 根据FormNo数组返回ReportForm列表
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <returns></returns>
        DataTable GetReportFormFullList(string FormNo);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(Model.ReportForm model, DateTime? StartDay, DateTime? EndDay);
    }
}
