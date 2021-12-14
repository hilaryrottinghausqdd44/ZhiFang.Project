

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.Entity;

namespace ZhiFang.WeiXin.IBLL
{
	/// <summary>
	///
	/// </summary>
	public  interface IBNRequestForm : ZhiFang.IBLL.Base.IBGenericManager<NRequestForm>
	{
		SortedList<string, string> StatisticsNRequestForm_Frx(string where, string where1, long LabId, string empID, string employeeName, Dictionary<string, string> title = null, string FileType = "PDF");
	}
}