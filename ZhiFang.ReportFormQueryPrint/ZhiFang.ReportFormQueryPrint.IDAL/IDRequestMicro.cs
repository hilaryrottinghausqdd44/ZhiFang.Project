using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
	/// <summary>
	/// 接口层IRequestMicro 的摘要说明。
	/// </summary>
    public interface IDRequestMicro : IDataBase<Model.RequestMicro>
	{
        /// <summary>
        /// 根据FormNo返回ReportForm包含的RequestMicro列表
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <returns></returns>
        DataTable GetRequestMicroList(string FormNo);

        DataTable GetRequestMicroGroupListForSTestType(string FormNo);

        /// <summary>
        /// 根据FormNo返回ReportForm包含的RequestItem列表(xslt模板使用)
        /// </summary>
        /// <param name="FormNo">FormNo</param>
        /// <returns></returns>
        DataTable GetRequestMicroGroupList(string FormNo);
    }
}
