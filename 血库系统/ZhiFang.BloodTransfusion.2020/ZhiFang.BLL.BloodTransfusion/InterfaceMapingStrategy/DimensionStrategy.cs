using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.BloodTransfusion;

namespace ZhiFang.BLL.BloodTransfusion.InterfaceMapingStrategy
{
    /// <summary>
    /// 业务接口对照关系信息的Stategy  
    /// </summary>
    abstract class DimensionStrategy<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ovList"></param>
        /// <param name="mapingList"></param>
        /// <param name="deveCode"></param>
        /// <param name="bobjectType"></param>
        /// <param name="addList">待新增保存的对照关系集合</param>
        /// <returns></returns>
        public abstract IList<BDictMapingVO> GetBDictMapingVOList(IList<T> ovList, IList<BloodInterfaceMaping> mapingList, string deveCode, BDict bobjectType,ref IList<BDictMapingVO> addList);
    }
}
