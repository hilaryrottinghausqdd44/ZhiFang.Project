using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public  interface IBBObjectOperateType : IBGenericManager<BObjectOperateType>
	{
        /// <summary>
        /// 根据对象操作类型编码返回改对象操作类型(目前查询ShortCode字段)
        /// 如果存在多个,则取第一个(数据库中此字段应设置唯一)
        /// </summary>
        /// <param name="operateTypeCode">对象操作类型编码</param>
        /// <returns>BSampleOperateType</returns>
        BObjectOperateType GetObjectOperateTypeByCode(string operateTypeCode);

        BObjectOperateType GetOrAddObjectOperateTypeByCode(string operateTypeCode, string operateMemo);

    }
}