using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.Model;

namespace ZhiFang.IDAL
{
    public interface IDBClientPara
    {
        /// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Model.B_ClientPara model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        int Update(Model.B_ClientPara model);

        /// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        DataSet GetList(int Top, string strWhere, string filedOrder);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(long ID);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(long ID);


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        ZhiFang.Model.B_ClientPara GetModel(long ID);

        ZhiFang.Model.B_ClientPara Search(long ClientNo, string ParaCode);

        DataSet GetListLike(ZhiFang.Model.B_ClientPara model);

        /// <summary>
        /// 数据同步时增加数据
        /// </summary>
        int AddByDataRow(DataRow dr);

        /// <summary>
        /// 数据同步时修改数据
        /// </summary>
        int UpdateByDataRow(DataRow dr);
        int Delete(long clientNo, string paraCode);
        DataSet SearchGroupByParaNo(string where);
        List<B_ClientPara> SearchByParaNo(string paraNo);
        List<B_ClientPara> SearchBBClientParaGroupByName(string name);
        List<B_ClientPara> SearchBBClientParaByParaNoAndLabIDAndLabName(string paraNo, string labID, string labName);
    }
}
