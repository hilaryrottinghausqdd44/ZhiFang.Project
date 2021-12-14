using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Common.BaseDictionary
{
    public interface IBClientProfile : IBBase<ZhiFang.Model.ClientProfile>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string ClIENTControlNo);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(string ClIENTControlNo);

        int DeleteList(string Idlist);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.ClientProfile GetModel(string ClientProfile);
        bool Add(List<Model.ClientProfile> l);

        /// <summary>
        /// 获取总记录数
        /// </summary>
        int GetTotalCount();
        int GetTotalCount(ZhiFang.Model.ClientProfile model);
        bool AddList(List<Model.ClientProfile> l);
        DataSet GetClientNo();
        #endregion  成员方法
    }
}
