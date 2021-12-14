using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Common.Log;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.Entity.ViewObject.Response;
using ZhiFang.WeiXin.IDAO;

namespace ZhiFang.WeiXin.DAO.NHB
{
    public class ClientEleAreaDao : ZhiFang.DAO.NHB.Base.BaseDaoNHB<ClientEleArea, long>, IDClientEleArea
    {
        //public void getListClientEleAreaAndClientEle(string strHqlWhere, int start, int count)
        //{
        //    GetDataRowRoleHQLString();
        //    EntityList<ClientEleAreaVO> entityList = new EntityList<ClientEleAreaVO>();
        //    string strHQL = "select " + typeof(ClientEleArea).Name.ToLower() + " clientele.AreaID as CLIENTELE_Name from " + typeof(ClientEleArea).Name + " " + typeof(ClientEleArea).Name.ToLower() + " LEFT JOIN clientelearea.clientele clientele with clientele.id=:clientno where 1=1 ";
        //    if (strHqlWhere != null && strHqlWhere.Length > 0)
        //    {
              
        //        strHQL += "and " + strHqlWhere;
        //    }
        //    strHQL += " and " + DataRowRoleHQLString;
        //    strHQL = FilterMacroCommand(strHQL);//宏命令过滤
        //    int? start1 = null;
        //    int? count1 = null;
        //    if (start > 0)
        //    {
        //        start1 = start;
        //    }
        //    if (count > 0)
        //    {
        //        count1 = count;
        //    }
        //    DaoNHBSearchByHqlAction<List<ClientEleAreaVO>, ClientEleAreaVO> action = new DaoNHBSearchByHqlAction<List<ClientEleAreaVO>, ClientEleAreaVO>(strHQL, start1, count1);

        //    entityList.list = this.HibernateTemplate.Execute<List<ClientEleAreaVO>>(action);

        //    Log.Debug(entityList.list.ToString());
        //    entityList.count = this.GetListCountByHQL(strHqlWhere);
        //}
    }
}