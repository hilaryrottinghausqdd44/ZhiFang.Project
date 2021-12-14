
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.RBAC;
using ZhiFang.Entity.RBAC;
using ZhiFang.BLL.Base;

namespace ZhiFang.BLL.RBAC
{
	/// <summary>
	///
	/// </summary>
	public  class BBSex : Base.BaseBLL<BSex>, ZhiFang.IBLL.RBAC.IBBSex
	{
        public BSex GetSexByName(IList<BSex> listSex, string sexName)
        {
            BSex sex = null;
            if (listSex == null || listSex.Count == 0)
                listSex = this.LoadAll();
            IList<BSex> listName = listSex.Where(p => p.Name == sexName).ToList();
            if (listName != null && listName.Count > 0)
                sex = listName[0];
            if (sex == null)
            {
                IList<BSex> listSName = listSex.Where(p => p.SName == sexName).ToList();
                if (listSName != null && listSName.Count > 0)
                    sex = listSName[0];
            }
            return sex;
        }

    }
}