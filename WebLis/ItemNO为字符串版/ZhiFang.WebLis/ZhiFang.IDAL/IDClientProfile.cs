using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IDAL
{
    public interface IDClientProfile : IDataBase<ZhiFang.Model.ClientProfile>
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string ClIENTControlNo);
        bool Exists(System.Collections.Hashtable ht);
        /// <summary>
        /// 数据同步时增加数据
        /// </summary>
        int AddByDataRow(DataRow dr);
        /// <summary>
        /// 数据同步时修改数据
        /// </summary>
        int UpdateByDataRow(DataRow dr);
        DataSet  GetClientNo();
        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(string ClIENTControlNo);

        int DeleteList(string Idlist);
        bool Add(List<Model.ClientProfile> l);
        bool AddList(List<Model.ClientProfile> l);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.ClientProfile GetModel(string ClientProfile);

        #endregion  成员方法
    }
}
