using System;
using System.Collections.Generic;
using System.Data;
using ZhiFang.Model;

namespace ZhiFang.IBLL.Common.BaseDictionary
{
    /// <summary>
    /// 接口层IBModules	
    /// </summary>
    public interface IBModules : IBBase<ZhiFang.Model.Modules>, IBDataPage<ZhiFang.Model.Modules>
    {
        #region  成员方法

        /// <summary>
        /// 增加一条数据
        /// </summary>
        BaseResultDataValue Addentity(ZhiFang.Model.Modules model);
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int ID);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        BaseResultDataValue Delete(int ID);

        bool DeleteList(string IDlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.Modules GetModel(int ID);
        		
        #endregion  成员方法

        DataSet GetListByRBACModulesList(System.Collections.Generic.List<string> rbac_moduleslist);
        DataSet GetListByRBACModulesListAll();

        bool CheckModuleCode(string p);
        DataSet GetListByPage(string where, int page, int limit, string Sort);
        int GetRecordCount(string wherestr);
        List<ZhiFang.Model.Modules> DataTableToList(DataTable dt);
    }
}