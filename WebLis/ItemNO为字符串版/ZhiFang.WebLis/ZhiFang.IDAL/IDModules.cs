using System;
using System.Data;
namespace ZhiFang.IDAL
{
    /// <summary>
    /// 接口层IDModules	
    /// </summary>
    public interface IDModules : IDataBase<ZhiFang.Model.Modules>, IDataPage<ZhiFang.Model.Modules>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(int ID);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(int ID);

        bool DeleteList(string IDlist);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.Modules GetModel(int ID);

        
        #endregion  成员方法

        DataSet GetListByRBACModulesList(System.Collections.Generic.List<string> rbac_moduleslist);
    }
}