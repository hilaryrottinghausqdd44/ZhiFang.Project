using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;

namespace ZhiFang.Digitlab.DAO.NHB
{
    public class BMicroTestItemUseValueDao : BaseDaoNHB<BMicroTestItemUseValue, long>, IDBMicroTestItemUseValueDao
    {
        private string strHQL = "select bmicrotestitemusevalue from BMicroTestItemUseValue bmicrotestitemusevalue left join bmicrotestitemusevalue.BMicroTestItemInfo ";
        public new Entity.EntityList<BMicroTestItemUseValue> GetListByHQL(string strHqlWhere, int start, int count)
        {
            Entity.EntityList<BMicroTestItemUseValue> entityList = new Entity.EntityList<BMicroTestItemUseValue>();
            entityList = base.GetListByHQL(strHqlWhere, start, count, strHQL);
            return entityList;
        }
        public new Entity.EntityList<BMicroTestItemUseValue> GetListByHQL(string strHqlWhere, string Order, int start, int count)
        {
            Entity.EntityList<BMicroTestItemUseValue> entityList = new Entity.EntityList<BMicroTestItemUseValue>();
            entityList = base.GetListByHQL(strHqlWhere, Order, start, count, strHQL);
            return entityList;
        }
        public new Entity.EntityList<BMicroTestItemUseValue> GetListBySQLAndHQL(string strHQL, string strHqlWhere, int start, int count)
        {
            Entity.EntityList<BMicroTestItemUseValue> entityList = new Entity.EntityList<BMicroTestItemUseValue>();
            entityList = base.GetListByHQL(strHqlWhere, start, count, strHQL);
            return entityList;
        }
    }
}