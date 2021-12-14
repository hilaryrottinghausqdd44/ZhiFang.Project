using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IDAL
{
    public interface IDALLReportForm
    {
        /// <summary>
        /// 骨髓项目表单信息
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        DataTable GetMarrowItemList(string FormNo);
        /// <summary>
        /// 返回报告单信息
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        DataTable GetFromInfo(string FormNo);

        /// <summary>
        /// 返回报告单信息(实体类)
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        Model.ReportForm GetFromInfoModel(string FormNo);

        /// <summary>
        /// 返回报告单内项目列表
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        DataTable GetFromItemList(string FormNo);
        /// <summary>
        /// 返回报告单所在小组信息
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        DataTable GetFromPGroupInfo(int SectionNo);
        /// <summary>
        /// 返回报告单所内的图片列表
        /// </summary>
        /// <param name="FormNo"></param>
        /// <returns></returns>
        DataTable GetFromGraphList(string FormNo);
    }
}
