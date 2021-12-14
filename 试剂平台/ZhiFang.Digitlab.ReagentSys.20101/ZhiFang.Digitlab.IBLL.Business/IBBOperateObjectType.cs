

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
	public  interface IBBOperateObjectType : IBGenericManager<BOperateObjectType>
	{
        /// <summary>
        /// 根据操作对象编码返回改操作对象类型(目前查询ShortCode字段)
        /// 如果存在多个,则取第一个(数据库中此字段应设置唯一)
        /// </summary>
        /// <param name="operateObjectTypeCode">操作对象编码</param>
        /// <returns>BOperateObjectType</returns>
        BOperateObjectType GetBOperateObjectTypeByCode(string operateObjectTypeCode);
	}
}