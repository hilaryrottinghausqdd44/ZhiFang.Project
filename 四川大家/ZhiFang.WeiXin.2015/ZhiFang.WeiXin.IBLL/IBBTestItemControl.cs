

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
	public  interface IBBTestItemControl : ZhiFang.IBLL.Base.IBGenericManager<BTestItemControl>
	{
        /// <summary>
        /// 根据客户端实验室编码和客户端项的编码获取中心端项的编码
        /// </summary>
        ///<param name="LabCode">实验室编码</param>
        ///<param name="LabPrimaryNo">实验室项的编码</param>
        /// <returns>中心端项的编码</returns>
        string GetCenterNo(string LabCode, string LabPrimaryNo);
    }
}