using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Report
{
    public interface IBWhoNet_FormData
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(ZhiFang.Model.WhoNet_FormData model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        int Update(ZhiFang.Model.WhoNet_FormData model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete();

        DataSet JoinCount(string LABORATORY, DateTime? SPEC_DATE, string SPEC_TYPE, string ORGANISM);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.WhoNet_FormData GetModel();

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
        List<ZhiFang.Model.WhoNet_FormData> GetModelList(string strWhere);

           /// <summary>
        /// 获得数据列表
        /// </summary>
        List<ZhiFang.Model.WhoNet_FormData> DataTableToList(DataTable dt);

         /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetAllList();
    }
}
