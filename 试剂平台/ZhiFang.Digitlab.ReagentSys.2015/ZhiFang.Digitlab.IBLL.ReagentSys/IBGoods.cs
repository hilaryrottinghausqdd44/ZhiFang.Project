using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;

namespace ZhiFang.Digitlab.IBLL.ReagentSys
{
	/// <summary>
	///
	/// </summary>
	public interface IBGoods : IBGenericManager<Goods>
	{
        BaseResultDataValue AddGoodsDataFormExcel(string labID, string compID, string prodID, string excelFilePath, string serverPath);

        EntityList<Goods> EditBaronGetGoods(long compID, long cenOrgID, string jsonGoods);

        BaseResultDataValue CopyGoodsByID(string listID, long compId, long cenOrgId, int goodsNoType);

        DataSet GetGoodsInfoByID(string idList, string where, string sort, string xmlPath);

        BaseResultDataValue CheckGoodsExcelFormat(string excelFilePath, string serverPath);

        EntityList<Goods> SearchGoodsByCompID(string labCenOrgID, string compCenOrgID, string where, string sort, int page, int limit);

        bool JudgeGoodsIsExist(string compId, string cenOrgId, string goodsNo);

        BaseResultDataValue JudgeGoodsIsExist(string compId, string labId, string goodsNo, ref Goods goods);

        BaseResultData EditGoodsDownloadFlagByLabID(string labID, string startDate, string endDate);
    }
}