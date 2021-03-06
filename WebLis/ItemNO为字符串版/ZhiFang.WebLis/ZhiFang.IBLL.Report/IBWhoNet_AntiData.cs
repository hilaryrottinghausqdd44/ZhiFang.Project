using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Report
{
    public interface IBWhoNet_AntiData
    {
        #region  Method
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(ZhiFang.Model.WhoNet_AntiData model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        int Update(ZhiFang.Model.WhoNet_AntiData model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(long AntiID);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.WhoNet_AntiData GetModel(long AntiID);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        List<ZhiFang.Model.WhoNet_AntiData> GetModelList(string strWhere);

         /// <summary>
        /// 获得数据列表
        /// </summary>
         DataSet GetAllList();
        #endregion
    }
}
