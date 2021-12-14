

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
	public  interface IBBSampleOperateType : IBGenericManager<BSampleOperateType>
	{
        /// <summary>
        /// 根据对象操作类型编码返回改对象操作类型(目前查询ShortCode字段)
        /// 如果存在多个,则取第一个(数据库中此字段应设置唯一)
        /// </summary>
        /// <param name="operateTypeCode">对象操作类型编码</param>
        /// <returns>BSampleOperateType</returns>
        BSampleOperateType GetSampleOperateTypeByCode(string operateTypeCode);

        BSampleOperateType GetOrAddSampleOperateTypeByCode(string operateTypeCode, string operateMemo);

    }
}