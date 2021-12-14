using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using ZhiFang.Model;

namespace ZhiFang.IBLL.Common.BaseDictionary
{
    public interface IBBClientPara
    {
        /// <summary>
        /// 新增数据
        /// </summary>
        int Add(B_ClientPara entity);

        /// <summary>
        /// 修改数据
        /// </summary>
        int Edit(B_ClientPara entity);

        ZhiFang.Model.B_ClientPara GetModel(long ClientNo, string ParaCode);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(long Id);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        int Delete(long ClientNo, string ParaCode);

        /// <summary>
        /// 分组查询
        /// </summary>
        DataSet SearchGroupByParaNo(string where);

        List<B_ClientPara> SearchByParaNo(string paraNo);
        List<B_ClientPara> SearchBBClientParaGroupByName(string name);
        List<B_ClientPara> SearchBBClientParaByParaNoAndLabIDAndLabName(string paraNo, string labID, string labName);
    }
}
