using System;
using System.Data;
using ZhiFang.Model;
namespace ZhiFang.IBLL.Common.BaseDictionary
{
    /// <summary>
    /// 接口层IBPGroupPrint	
    /// </summary>
    public interface IBPGroupPrint : IBBase<ZhiFang.Model.PGroupPrint>, IBDataPage<ZhiFang.Model.PGroupPrint>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string PGroupPrintNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(string PGroupPrintNo);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.PGroupPrint GetModel(string PGroupPrintNo);
        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList_No_Name(Model.PGroupPrint model);
        		
        #endregion  成员方法
        string PrintFormatFilter_Weblis(DataRow[] dra, int itemcount);
        string GetFormatPrint(string str5, DataTable table4);

        int PrintFormatNo(int sectionno, int clientno, string speciallyitemno);

        string GetFormatPrint(int p, string str5, DataTable table4);

        string GetFormatPrint(int? nullable, DataTable table4);

        EntityList<Model.PGroupFormat> GetAllReportGroupModelSet(Model.PGroupFormat l_m, int page, int limit, string fields, string where, string sort);

        Model.PGroupFormat GetModelByID(string id);
    }
}