

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.Entity.ViewObject.Response;

namespace ZhiFang.WeiXin.IBLL
{
	/// <summary>
	///
	/// </summary>
	public  interface IBClientEleArea : ZhiFang.IBLL.Base.IBGenericManager<ClientEleArea>
	{
        EntityList<ClientEleAreaVO> SearchClientEleAreaAndCLIENTELE(string where,string order,int page,int limit);
        EntityList<ClientEleAreaVO> SearchClientEleAreaAndCLIENTELE(int id,string where,int page, int limit);
    }
}