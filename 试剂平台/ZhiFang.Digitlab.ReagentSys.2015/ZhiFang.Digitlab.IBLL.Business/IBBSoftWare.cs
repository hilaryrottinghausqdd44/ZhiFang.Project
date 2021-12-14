

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IBLL.Business
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBSoftWare : IBGenericManager<BSoftWare>
	{
        /// <summary>
        /// 检查软件是否需要更新
        /// </summary>
        /// <param name="softWareCode">软件代码</param>
        /// <param name="softWareCurVersion">软件当前版本</param>
        /// <returns>需要更新的版本列表</returns>
        //BaseResultDataValue SearchCheckIsUpdateSoftWare(string softWareCode, string softWareCurVersion);      
        EntityList<BSoftWareVersionManager> SearchCheckIsUpdateSoftWare(string softWareCode, string softWareCurVersion);
        /// <summary>
        ///  根据软件的唯一Code删除软件
        /// </summary>
        /// <param name="BSoftWareCode"></param>
        /// <returns></returns>
        bool RemoveByCode(string BSoftWareCode);
	}
}