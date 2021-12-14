

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.Entity.ViewObject.Request;

namespace ZhiFang.WeiXin.IBLL
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBarCodeForm : ZhiFang.IBLL.Base.IBGenericManager<BarCodeForm>
	{
        /// <summary>
        /// 条码是否存在
        /// </summary>
        /// <param name="flag"></param>
        /// <param name="barcodelist"></param>
        /// <returns></returns>
        bool IsExistBarCode(string flag, List<UiBarCode> barcodelist, out string repeatbarcodestr);
    }
}