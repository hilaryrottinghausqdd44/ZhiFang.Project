using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Model;

namespace ZhiFang.IBLL.Common.BaseDictionary
{
    /// <summary>
    /// 接口层IBBusinessLogicClientControl	
    /// </summary>
    public interface IBBusinessLogicClientControl : IBBase<ZhiFang.Model.BusinessLogicClientControl>, IBDataPage<ZhiFang.Model.BusinessLogicClientControl>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string Account, string ClientNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(string Account, string ClientNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(string Id);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.BusinessLogicClientControl GetModel(string Account, string ClientNo);
        #endregion  成员方法
        List<ZhiFang.Model.BusinessLogicClientControl> DataTableToList(DataTable dt);
        string GetClientList_String(Model.BusinessLogicClientControl l_m);
        DataSet GetClientList_DataSet(Model.BusinessLogicClientControl l_m);
        EntityList<Model.CLIENTELE> GetBusinessLogicClientList(Model.BusinessLogicClientControl l_m, int page, int limit, string fields, string where, string sort);
        bool Add(List<Model.BusinessLogicClientControl> l);

        DataSet GetClientList_DataSet(int p, Model.BusinessLogicClientControl businessLogicClientControl);
    } 
}
