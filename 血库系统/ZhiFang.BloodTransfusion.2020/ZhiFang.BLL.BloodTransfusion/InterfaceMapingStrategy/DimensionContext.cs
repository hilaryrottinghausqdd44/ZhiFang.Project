using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.BloodTransfusion;

namespace ZhiFang.BLL.BloodTransfusion.InterfaceMapingStrategy
{
    /// <summary>
    /// 业务接口对照关系信息的Context
    /// </summary>
    class DimensionContext<T>
    {
        private IList<T> _deveList = new List<T>();
        private string _deveCode = "";
        private BDict _bobjectType =null;//对照字典类型Id
        private DimensionStrategy<T> dimensionStrategy = null;  //对象组合

        /// <summary>
        /// 获取List集合使用
        /// </summary>
        /// <param name="deveList"></param>
        /// <param name="deveCode"></param>
        /// <param name="strategy"></param>
        public DimensionContext(IList<T> deveList, string deveCode, BDict bobjectType, DimensionStrategy<T> strategy)
        {
            this._deveList = deveList;
            this._deveCode = deveCode;
            this._bobjectType = bobjectType;
            this.dimensionStrategy = strategy;
        }
        public IList<BDictMapingVO> GetBDictMapingVOList(IList<BloodInterfaceMaping> mapingList,ref IList<BDictMapingVO> addList)
        {
            IList<BDictMapingVO> nfList2 = this.dimensionStrategy.GetBDictMapingVOList(this._deveList, mapingList, this._deveCode, this._bobjectType,ref addList);
            return nfList2;
        }
    }
}
