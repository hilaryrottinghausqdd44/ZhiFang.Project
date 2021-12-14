using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.WebAssist;

namespace ZhiFang.BLL.WebAssist.InterfaceMapingStrategy
{
    /// <summary>
    ///业务接口对照关系信息-Department
    /// </summary>
    class OfDepartmentMaping : DimensionStrategy<Department>
    {
        public override IList<BDictMapingVO> GetBDictMapingVOList(IList<Department> deveList, IList<SCInterfaceMaping> mapingList, string deveCode, BDict bobjectType, ref IList<BDictMapingVO> addList)
        {
            //对照关系集合
            IList<BDictMapingVO> ovList = new List<BDictMapingVO>();
            foreach (var deve in deveList)
            {
                BDictMapingVO vo = new BDictMapingVO();
                vo.Department = deve;

                var tempList = mapingList.Where(p => p.BobjectID == deve.Id && p.BobjectType.Id == bobjectType.Id);
                if (tempList != null && tempList.Count() > 0)
                {
                    vo.SCInterfaceMaping = tempList.ElementAt(0);
                }
                else
                {
                    //对照关系不存在
                    SCInterfaceMaping entity = new SCInterfaceMaping();
                    entity.BobjectID = deve.Id;
                    entity.BobjectType = bobjectType;
                    entity.IsUse = true;
                    entity.DispOrder = deve.DispOrder;

                    vo.SCInterfaceMaping = entity;
                    addList.Add(vo);
                }
                ovList.Add(vo);
            }
            return ovList;
        }
    }
}
