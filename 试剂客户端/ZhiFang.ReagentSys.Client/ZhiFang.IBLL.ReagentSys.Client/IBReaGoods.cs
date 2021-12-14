using System.Data;
using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Response;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaGoods : IBGenericManager<ReaGoods>
    {
        /// <summary>
        /// 依分类类型获取机构货品的一级分类或二级分类List信息
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="whereHql"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IList<ReaGoodsClassVO> SearchGoodsClassListByClassTypeAndHQL(string classType, bool hasNull, string whereHql, string sort, int page, int limit);
        /// <summary>
        /// 依分类类型获取机构货品的一级分类或二级分类EntityList信息
        /// </summary>
        /// <param name="classType"></param>
        /// <param name="whereHql"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<ReaGoodsClassVO> SearchGoodsClassEntityListByClassTypeAndHQL(string classType, bool hasNull, string whereHql, string sort, int page, int limit);
        BaseResultDataValue CheckGoodsExcelFormat(string excelFilePath, string serverPath);

        BaseResultDataValue AddGoodsDataFormExcel(string prodID, string excelFilePath, string serverPath);

        int GetMaxGoodsSort();

        /// <summary>
        /// 获取导出货品信息列表
        /// </summary>
        /// <param name="idList">货品ID字符串列表</param>
        /// <param name="where">查询条件</param>
        /// <param name="sort">排序</param>
        /// <param name="xmlPath">导出信息配置文件路径</param>
        /// <returns></returns>
        DataSet GetReaGoodsInfoByID(string idList, string where, string sort, string xmlPath);
        /// <summary>
        /// 对机构货品的修改信息添加修改记录
        /// </summary>
        /// <param name="serverReaGoods"></param>
        /// <param name="arrFields"></param>
        void AddSCOperation(ReaGoods serverReaGoods, string[] arrFields);

        BaseResultData AddReaGoodsSyncByInterface(string syncField, string syncFieldValue, Dictionary<string, object> dicFieldAndValue);

        BaseResultData AddReaGoodsSyncByInterface(string syncField, string syncFieldValue, Dictionary<string, object> dicFieldAndValue, ref ReaCenOrg reaCenOrg, ref ReaGoods reaGoods);

        /// <summary>
        /// 保存同步物资接口的机构货品信息
        /// </summary>
        /// <param name="editReaGoodsList"></param>
        /// <returns></returns>
        BaseResultData SaveReaGoodsByMatchInterface(IList<ReaGoods> editReaGoodsList, long empID, string empName);

        BaseResultData SaveReaGoodsByMatchInterface(IList<ReaGoods> editReaGoodsList, long empID, string empName, ref ReaGoods reaGoods);

        /// <summary>
        /// 机构货品保存时,验证货品平台编码是否在当前机构内惟一
        /// </summary>
        /// <param name="reaGoods"></param>
        /// <returns></returns>
        bool EditVerificationGoodsNo(ReaGoods reaGoods);
<<<<<<< .mine
        /// <summary>
        /// 获取产品编号最大值
        /// </summary>
        /// <returns></returns>
        long GetMaxGoodsId(long labID);
||||||| .r2673
=======

        /// <summary>
        /// 获取最大的时间戳，接口同步货品使用
        /// </summary>
        /// <returns></returns>
        string GetMaxTS();

        /// <summary>
        /// 货品表结合库存表，查询二级分类并返回
        /// </summary>
        /// <returns></returns>
        EntityList<ReaGoodsClassVO> SearchGoodsClassTypeJoinQtyDtl(string where, string sort, int page, int limit);
>>>>>>> .r2783
    }
}