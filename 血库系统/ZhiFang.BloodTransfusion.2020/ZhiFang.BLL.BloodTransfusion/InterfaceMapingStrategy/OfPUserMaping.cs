using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.BLL.BloodTransfusion.InterfaceMapingStrategy
{
    /// <summary>
    ///业务接口对照关系信息-PUser
    /// </summary>
    class OfPUserMaping : DimensionStrategy<PUser>
    {
        public override IList<BDictMapingVO> GetBDictMapingVOList(IList<PUser> deveList, IList<BloodInterfaceMaping> mapingList, string deveCode, BDict bobjectType, ref IList<BDictMapingVO> addList)
        {
            //对照关系集合
            IList<BDictMapingVO> ovList = new List<BDictMapingVO>();
            foreach (var deve in deveList)
            {
                BDictMapingVO vo = new BDictMapingVO();
                vo.PUser = deve;

                var tempList = mapingList.Where(p => p.BobjectID == deve.Id && p.BobjectType.Id == bobjectType.Id);
                if (tempList != null && tempList.Count() > 0)
                {
                    vo.BloodInterfaceMaping = tempList.ElementAt(0);
                }
                else
                {
                    //对照关系不存在
                    BloodInterfaceMaping entity = new BloodInterfaceMaping();
                    entity.BobjectID = deve.Id;
                    entity.BobjectType = bobjectType;
                    entity.IsUse = true;
                    entity.DispOrder = deve.DispOrder;

                    vo.BloodInterfaceMaping = entity;
                    addList.Add(vo);
                }
                ovList.Add(vo);
            }
            return ovList;
        }
    }
}
