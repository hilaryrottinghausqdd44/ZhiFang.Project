using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;
using System.Data;

namespace ZhiFang.ReportFormQueryPrint.IDAL
{
	/// <summary>
	/// 接口层IPUser 的摘要说明。
	/// </summary>
    public interface IDEmpDeptLinks : IDataBase<Model.EmpDeptLinks>
	{
        int Delete(long EDLID);
    }
}
