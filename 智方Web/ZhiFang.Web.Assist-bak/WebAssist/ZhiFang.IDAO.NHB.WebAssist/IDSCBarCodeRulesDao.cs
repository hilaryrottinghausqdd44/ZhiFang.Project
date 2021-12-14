using ZhiFang.IDAO.Base;
using ZhiFang.Entity.WebAssist;
using System.Collections.Generic;

namespace ZhiFang.IDAO.NHB.WebAssist
{
	public interface IDSCBarCodeRulesDao : IDBaseDao<SCBarCodeRules, long>
	{
        /// <summary>
        /// 按LabID加条码类型,获取一维条码的当前最大序号
        /// 如果不存在,当前条码值为1,并新添加一条机构的条码类型记录;
        /// 如果已存在该机构的条码类型记录,将当前条码值累加1,将累加后的条码值更新保存,并返回累加后的条码值,当前条码值允许跳号
        /// </summary>
        /// <param name="labID"></param>
        /// <param name="bmsType"></param>
        /// <returns></returns>
        long GetMaxBarCode(long labID, string bmsType);
        /// <summary>
        /// 默认不添加按LabID的过滤条件获取数据
        /// </summary>
        IList<SCBarCodeRules> GetListOfNoLabIDByHql(string hqlWhere);
    } 
}