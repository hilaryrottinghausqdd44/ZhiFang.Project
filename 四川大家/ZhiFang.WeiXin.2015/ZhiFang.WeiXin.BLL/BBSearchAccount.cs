
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.WeiXin.IDAO;
using ZhiFang.WeiXin.Entity;

namespace ZhiFang.WeiXin.BLL
{
	/// <summary>
	///
	/// </summary>
	public  class BBSearchAccount : ZhiFang.BLL.Base.BaseBLL<BSearchAccount>, ZhiFang.WeiXin.IBLL.IBBSearchAccount
	{
        public IDAO.IDBAccountHospitalSearchContextDao IDBAccountHospitalSearchContextDao { get; set; }
        public bool Add(List<BAccountHospitalSearchContext> bahscl)
        {           
            if (this.Add())
            {
                if(bahscl!=null && bahscl.Count>0)
                {
                    foreach(var bahsc in bahscl)
                    {
                        bahsc.AccountID = this.Entity.Id;
                        IDBAccountHospitalSearchContextDao.Save(bahsc);
                    }
                    return true;
                } 
            }
            return false;
        }

        public bool Update(List<BAccountHospitalSearchContext> bahscl)
        {
            List<string> p = new List<string>();
            if (this.Entity != null)
            {
                if (this.Entity.Id != null)
                {
                    p.Add(" Id=" + this.Entity.Id);
                    //ZhiFang.Common.Log.Log.Debug(" Id=" + this.Entity.Id);
                }
                if (this.Entity.Name != null)
                {
                    p.Add(" Name='" + this.Entity.Name+"' ");
                    //ZhiFang.Common.Log.Log.Debug(" Name=" + this.Entity.Name);
                }
                if (this.Entity.SexID != null)
                {
                    p.Add(" SexID=" + this.Entity.SexID);
                    //ZhiFang.Common.Log.Log.Debug(" SexID=" + this.Entity.SexID);
                }
                if (this.Entity.IDNumber != null)
                {
                    p.Add(" IDNumber='" + this.Entity.IDNumber + "' ");
                    //ZhiFang.Common.Log.Log.Debug(" IDNumber=" + this.Entity.IDNumber);
                }
                if (this.Entity.MediCare != null)
                {
                    p.Add(" MediCare='" + this.Entity.MediCare + "' ");
                    //ZhiFang.Common.Log.Log.Debug(" MediCare=" + this.Entity.MediCare);
                }
                if (this.Entity.MobileCode != null)
                {
                    p.Add(" MobileCode='" + this.Entity.MobileCode + "' ");
                    //ZhiFang.Common.Log.Log.Debug(" MobileCode=" + this.Entity.MobileCode);
                }
               
                if (this.Update(p.ToArray()))
                {
                    IDBAccountHospitalSearchContextDao.DeleteByAccountID(this.Entity.Id);
                    if (bahscl != null && bahscl.Count > 0)
                    {
                        foreach (var bahsc in bahscl)
                        {
                            bahsc.AccountID = this.Entity.Id;

                            IDBAccountHospitalSearchContextDao.Save(bahsc);
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        public List<BSearchAccount> SearchSearchAccountVOListByHQL(long WeiXinUserID, string OpenID)
        {
            List<BSearchAccount> lbsa = new List<BSearchAccount>();
            lbsa = this.SearchListByHQL(" WeiXinUserID=" + WeiXinUserID + " and WeiXinAccount='" + OpenID + "' order by DataAddTime asc ").ToList();
            if(lbsa!=null)
            {
                foreach (var bsa in lbsa)
                {
                    bsa.ListBAccountHospitalSearchContext = new List<BAccountHospitalSearchContext>();
                    bsa.ListBAccountHospitalSearchContext = IDBAccountHospitalSearchContextDao.GetListByHQL(" AccountID=" + bsa.Id + " order by DispOrder asc ").ToList();
                }
                return lbsa;
            }
            return null;            
        }

        public override bool Remove(long longID)
        {
            IDBAccountHospitalSearchContextDao.DeleteByAccountID(longID);
            return base.Remove(longID);
        }
    }
}