using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.Entity.LIIP.ViewObject.DicReceive;
using ZhiFang.Entity.LIIP.ViewObject.Request;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LIIP
{
	/// <summary>
	///
	/// </summary>
	public  interface IBBHospitalArea : IBGenericManager<BHospitalArea>
	{
        List<long> SearchListFiltrationById(List<long> ids);

        IList<BHospitalArea> SearchBHospitalAreaLevelNameTreeByID(long id);
		BaseResultTree<BHospitalArea> SearchBHospitalAreaListTree();
        string GetBHospitalAreaCenterHospitalLabCodeByLabCode(string labCode);
    
        EntityList<BHospitalAreaVO> ST_UDTO_SearchTreeGridBHospitalAreaByHQL(string where, string v, int page, int limit);
        EntityList<BHospitalAreaVO> ST_UDTO_SearchTreeGridBHospitalAreaByHQL(string where, int page, int limit);
        bool ST_UDTO_UpdateBHospitalAreaByWhere(string[] pardata,string where);
        BaseResultBool ReceiveAndAdd(List<AreaVO> tmpo);
    }
}