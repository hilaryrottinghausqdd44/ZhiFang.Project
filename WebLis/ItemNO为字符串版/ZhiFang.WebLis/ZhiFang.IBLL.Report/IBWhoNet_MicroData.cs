using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Report
{
    public interface IBWhoNet_MicroData
    {
        /// <summary>
        /// 增加一条数据
        /// </summary>
        void Add(ZhiFang.Model.WhoNet_MicroData model);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        int Update(ZhiFang.Model.WhoNet_MicroData model);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(long MicroID);

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.WhoNet_MicroData GetModel(long MicroID);

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
        List<ZhiFang.Model.WhoNet_MicroData> GetModelList(string strWhere);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        List<ZhiFang.Model.WhoNet_MicroData> DataTableToList(DataTable dt);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetAllList();
    }
}
