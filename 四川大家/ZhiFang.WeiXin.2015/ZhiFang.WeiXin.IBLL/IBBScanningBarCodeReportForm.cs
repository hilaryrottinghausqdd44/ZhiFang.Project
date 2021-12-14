

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
	public  interface IBBScanningBarCodeReportForm : ZhiFang.IBLL.Base.IBGenericManager<BScanningBarCodeReportForm>
	{

        bool AddSearch(string Barcode, string SearchUserName, out int count);

        bool UpdateReadFlag(long BScanningBarCodeReportFormID);
    }
}