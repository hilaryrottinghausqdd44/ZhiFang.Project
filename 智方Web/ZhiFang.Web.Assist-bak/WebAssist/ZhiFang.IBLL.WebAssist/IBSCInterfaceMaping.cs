using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.WebAssist
{
    /// <summary>
    ///
    /// </summary>
    public interface IBSCInterfaceMaping : IBGenericManager<SCInterfaceMaping>
    {
        /// <summary>
        /// 根据条件查询实体列表
        /// </summary>
        /// <param name="deveWhere">对照字典的查询条件</param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <param name="deveCode"></param>
        /// <param name="useCode">如果deveCode等于BDict,useCode为字典类型编码(作为过滤字典数据使用)*</param>
        /// <param name="mapingWhere"></param>
        /// <returns></returns>
        EntityList<BDictMapingVO> GetBDictMapingVOListByHQL(string deveWhere, string order, int page, int count, string deveCode, string useCode, string mapingWhere, long objectTypeId);
        void AddSCOperation(SCInterfaceMaping serverEntity, string[] arrFields, long empID, string empName);
    }
}
