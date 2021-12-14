using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.RBAC;
using ZhiFang.Entity.RBAC;
using ZhiFang.IDAO.Base;

namespace ZhiFang.BLL.RBAC
{
	/// <summary>
	///
	/// </summary>
    public class BBTDMacroCommand : ZhiFang.IBLL.RBAC.IBBTDMacroCommand
	{
        public IDBTDMacroCommandDao DBDao { get; set; }
        private BTDMacroCommand entity;
        public BTDMacroCommand Entity
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
        #region IBBTDMacroCommand 成员

        public virtual bool Add(string key)
        {
            bool a = DBDao.Save(key, this.Entity);
            return a;
        }

        public virtual bool Edit(string key)
        {
            return DBDao.Update(key,this.entity);
        }
        public virtual bool Remove(string key)
        {
            return DBDao.Delete(key);
        }

        public virtual Dictionary<string, BTDMacroCommand> Search()
        {
            return DBDao.GetObjects();
        }

        public virtual BTDMacroCommand Get(string key)
        {
            return DBDao.Get(key);
        }

        public virtual BTDMacroCommand Load(string key)
        {
            return DBDao.Load(key);
        }

        public virtual int GetTotalCount()
        {
            return DBDao.GetTotalCount();
        }

        #endregion
	}
}