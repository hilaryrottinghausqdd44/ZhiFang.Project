using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.LabStar
{
    public interface IDLBEquipItemDao : IDBaseDao<LBEquipItem, long>
    {

        EntityList<LBEquipItem> QueryLBEquipItemDao(string strHqlWhere, string Order, int start, int count);

        EntityList<LBEquipItemVO> QueryLBEquipItemVODao(string strHqlWhere, string Order, int start, int count);

        /// <summary>
        /// 查找小组项目是否已经设置了仪器项目
        /// </summary>
        /// <param name="sectionID">小组ID</param>
        /// <param name="itemID">项目ID</param>
        /// <returns></returns>
        IList<LBEquipItem> QueryIsExistSectionItemDao(long sectionID, long itemID);

    }

    public interface IDLBEquipItemVODao : IDBaseDao<LBEquipItemVO, long>
    {
        EntityList<LBEquipItemVO> QueryLBEquipItemVODao(string strHqlWhere, string Order, int start, int count);
    }
}