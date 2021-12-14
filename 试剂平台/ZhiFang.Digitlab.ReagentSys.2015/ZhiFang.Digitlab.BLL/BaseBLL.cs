using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.BLL
{
    public class BaseBLL<T> : ZhiFang.Digitlab.IBLL.IBGenericManager<T> where T :class
    {
        public IDAO.IDBaseDao<T,long> DBDao { get; set; }
        
        private T entity;
        public T Entity
        {
            get
            {
                return entity;
            }
            set
            {
                entity = value;
            }
        }
        
        public virtual bool Add()
        {
            bool a=DBDao.Save(this.Entity);
            return a;
        }

        public virtual bool Save()
        {
            bool a = DBDao.Save(this.Entity);
            //ZhiFang.Common.Log.Log.Debug("BaseBLL_Save:" + typeof(T).Name.ToLower());
            return a;
        }

        public virtual bool Edit()
        {
            return DBDao.Update(this.entity);
        }

        public virtual bool Remove()
        {
            return DBDao.Delete(this.entity);
        }
        public virtual bool Remove(long longID)
        {
            return DBDao.Delete(longID);
        }

        public virtual bool RemoveByHQL(long longID)
        {
            return DBDao.DeleteByHQL(longID);
        }

        public virtual IList<T> Search()
        {
            return DBDao.GetObjects(this.entity);
        }
        public virtual EntityList<T> SearchListByHQL(string strHqlWhere, int page, int count)
        {
            EntityList<T> el = DBDao.GetListByHQL(strHqlWhere, page, count);
            return el;
        }

        public virtual EntityList<T> SearchListByHQL(string strHqlWhere,string Order, int page, int count)
        {
            EntityList<T> el = DBDao.GetListByHQL(strHqlWhere,Order, page, count);
            return el;
        }

        public virtual T Get(long longID)
        {
            return DBDao.Get(longID);
        }

        public virtual int GetTotalCount()
        {
            return DBDao.GetTotalCount();
        }
       
        public virtual IList<T> LoadAll()
        {
            return DBDao.LoadAll();
        }

        public virtual bool Update(string[] strParas)
        {
            return DBDao.Update(strParas);
        }
        #region
        //public virtual int GetTotalCount(T entity)
        //{
        //    return DBDao.GetTotalCount(entity);
        //}
        //public virtual T Load(long longID)
        //{
        //    return DBDao.Load(longID);
        //}
        #endregion
        
        public IList<T> SearchListByHQL(string strHqlWhere)
        {
            IList<T> el = DBDao.GetListByHQL(strHqlWhere);
            return el;
        }

        public int DeleteByHql(string strHqlWhere)
        {
            int result = DBDao.DeleteByHql(strHqlWhere);
            return result;
        }
    }
    public class BaseBLL<T, T1> : ZhiFang.Digitlab.IBLL.IBGenericManager<T, T1> where T : class
    {
        public IDAO.IDBaseDao<T, T1> DBDao { get; set; }
        private T entity;
        public T Entity
        {
            get
            {
                return entity;
            }
            set
            {
                entity = value;
            }
        }

        public virtual bool Add()
        {
            bool a = DBDao.Save(this.Entity);
            return a;
        }

        public virtual bool Edit()
        {
            return DBDao.Update(this.entity);
        }

        public virtual bool Remove()
        {
            return DBDao.Delete(this.entity);
        }
        public virtual bool Remove(T1 longID)
        {
            return DBDao.Delete(longID);
        }

        public virtual IList<T> Search()
        {
            return DBDao.GetObjects(this.entity);
        }
        public virtual EntityList<T> SearchListByHQL(string strHqlWhere, int page, int count)
        {
            EntityList<T> el = DBDao.GetListByHQL(strHqlWhere, page, count);
            return el;
        }

        public virtual EntityList<T> SearchListByHQL(string strHqlWhere, string Order, int page, int count)
        {
            EntityList<T> el = DBDao.GetListByHQL(strHqlWhere, Order, page, count);
            return el;
        }

        public virtual T Get(T1 longID)
        {
            return DBDao.Get(longID);
        }        

        public virtual int GetTotalCount()
        {
            return DBDao.GetTotalCount();
        }
        
        public virtual IList<T> LoadAll()
        {
            return DBDao.LoadAll();
        }

        public virtual bool Update(string[] strParas)
        {
            return DBDao.Update(strParas);
        }

        #region
        //public virtual int GetTotalCount(T entity)
        //{
        //    return DBDao.GetTotalCount(entity);
        //}
        //public virtual T Load(T1 longID)
        //{
        //    return DBDao.Load(longID);
        //}
        #endregion
    }
}
