using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.Common.Public;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaBmsSerialDao : BaseDaoNHB<ReaBmsSerial, long>, IDReaBmsSerialDao
    {
        public long GetMaxBarCode(long labID, string bmsType)
        {
            long nextBarCode = -1;
            nextBarCode = GetMaxBarCodeByHql(labID, bmsType);
            //nextBarCode = GetMaxBarCodeByPROC(labID, bmsType);
            return nextBarCode;
        }
        private long GetMaxBarCodeByHql(long labID, string bmsType)
        {
            long nextBarCode = -1;
            string hql = "reabmsserial.BmsType='" + bmsType + "'";
            if (labID>0) {
                hql= hql+" and reabmsserial.LabID=" + labID;
            }
            EntityList<ReaBmsSerial> tempEntityList =this.GetListByHQL(hql, "reabmsserial.CurBarCode desc", 1,1);

            if (tempEntityList.count > 0)
            {
                ReaBmsSerial entity=tempEntityList.list[0];
                DateTime curDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                DateTime lastUpdateTime = DateTime.Parse(entity.DataUpdateTime.Value.ToString("yyyy-MM-dd"));
                TimeSpan ts = curDate - lastUpdateTime;
                if (ts.Days > 0)
                {
                    //当前时间-最后更新时间大于1天重新从1开始
                    nextBarCode = 1;
                }
                else {
                    nextBarCode = entity.CurBarCode+1;
                }
                entity.CurBarCode = nextBarCode;
                entity.DataUpdateTime = DateTime.Now;
                this.Save(entity);

            } else {
                #region 在同一事务里不生效
                //nextBarCode = 1;
                //ReaBmsSerial searial = new ReaBmsSerial();
                //searial.LabID = labID;
                //searial.BmsType = bmsType;
                //searial.CurBarCode = nextBarCode;
                //searial.DataUpdateTime = DateTime.Now;
                //this.Save(searial); 
                #endregion
                throw new Exception(" Error:labID为:"+ labID+ ",bmsType为:" + bmsType + ",在ReaBmsSerial不存在记录,请联系系统管理员到【机构初始化】功能模块先进行系统初始化第五步!");
            }

            return nextBarCode;
        }
        public long GetMaxBarCodeByPROC(long labID, string bmsType)
        {
            long nextBarCode = -1;
           
            List<string> paranamea = new List<string> { "rulesId", "labId", "bmsType" };
            object[] paravaluea = new string[paranamea.Count];
            for (int i = 0; i < paravaluea.Length; i++)
            {
                paravaluea[i] = "";
            }
            if (paranamea.IndexOf("rulesId") >= 0)
                paravaluea[paranamea.IndexOf("rulesId")] = "" + GUIDHelp.GetGUIDLong();
            if (paranamea.IndexOf("labId") >= 0)
                paravaluea[paranamea.IndexOf("labId")] = ""+labID;
            if (paranamea.IndexOf("bmsType") >= 0)
                paravaluea[paranamea.IndexOf("bmsType")] = "" + bmsType;

            IList<ReaBmsSerial> tempEntityList = base.HibernateTemplate.FindByNamedQueryAndNamedParam<ReaBmsSerial>("P_GetReaBmsSerial", paranamea.ToArray(), paravaluea);
            if (tempEntityList.Count > 0) nextBarCode = tempEntityList[0].CurBarCode;

            return nextBarCode;
        }
        /// <summary>
        /// 默认不添加按LabID的过滤条件获取数据
        /// </summary>
        public IList<ReaBmsSerial> GetListOfNoLabIDByHql(string hqlWhere)
        {
            string strHQL = "select reabmsserial from ReaBmsSerial reabmsserial where 1=1 ";
            if (hqlWhere != null && hqlWhere.Length > 0)
            {
                strHQL += "and " + hqlWhere;
            }
            IList<ReaBmsSerial> tempList = this.HibernateTemplate.Find<ReaBmsSerial>(strHQL);
            return tempList;
        }
        //private long GetNextBarCode(long labId, string bmsType)
        //{
        //    if (string.IsNullOrEmpty(bmsType)) return -1;

        //    IList<long> list = this.Find<long>(string.Format("select max(reabmsserial.CurBarCode) as CurBarCode  from ReaBmsSerial reabmsserial where 1=1 "));
        //    if (list != null && list.Count > 0)
        //    {
        //        long curBarCode = list[0];
        //        curBarCode = curBarCode <= 0 ? 1 : ++curBarCode;
        //        return curBarCode;
        //    }
        //    else
        //    {
        //        //当该机构的条码类型不存在行记录时,先新增一条行记录
        //        ReaBmsSerial codeRules = new ReaBmsSerial();
        //        codeRules.LabID = labId;
        //        codeRules.BmsType = bmsType;
        //        codeRules.DataUpdateTime = DateTime.Now;
        //        codeRules.CurBarCode = 1;

        //        return 1;
        //    }
        //}
    }
}