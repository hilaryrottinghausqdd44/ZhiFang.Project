
using ZhiFang.BLL.Base;
using ZhiFang.Entity.SA;
using ZhiFang.IBLL.SA;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.SA;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;

namespace ZhiFang.BLL.SA
{
    /// <summary>
    ///
    /// </summary>
    public class BBUserUIConfig : BaseBLL<BUserUIConfig>, IBBUserUIConfig
    {
        public override bool Add()
        {
            bool isAdd = true;
            if (string.IsNullOrEmpty(this.Entity.UITypeName) && this.Entity.UITypeID > 0)
                this.Entity.UITypeName = UserUIConfigUIType.GetStatusDic()[this.Entity.UITypeID.ToString()].Name;
            if (!string.IsNullOrEmpty(this.Entity.UserUIKey))
            {
                //((IDBUserUIConfigDao)base.DBDao)
                IList<BUserUIConfig> tempList = ((IDBUserUIConfigDao)base.DBDao).GetListByHQL("buseruiconfig.IsUse=1 and buseruiconfig.UserUIKey='" + this.Entity.UserUIKey + "'");
                if (tempList != null && tempList.Count > 0)
                {
                    isAdd = false;
                    BUserUIConfig userUIConfig = tempList.OrderByDescending(p => p.DataAddTime).ElementAt(0);
                    userUIConfig.Comment = this.Entity.Comment;
                    this.Entity = userUIConfig;
                }
            }
            if (isAdd)
            {
                //return ((IDBUserUIConfigDao)base.DBDao).Save(this.Entity);
                return base.Add();
            }
            else
            {
                return this.Edit();
            }
        }
    }
}