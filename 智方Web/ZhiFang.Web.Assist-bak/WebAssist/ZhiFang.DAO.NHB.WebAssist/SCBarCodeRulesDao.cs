using System;
using System.Collections.Generic;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.WebAssist.Common;
using ZhiFang.IDAO.NHB.WebAssist;

namespace ZhiFang.DAO.NHB.WebAssist
{
    public class SCBarCodeRulesDao : BaseDaoNHB<SCBarCodeRules, long>, IDSCBarCodeRulesDao
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
            string hql = "scbarcoderules.BmsType='" + bmsType + "'";
            if (labID >= 0)
            {
                hql = hql + " and scbarcoderules.LabID=" + labID;
            }
            EntityList<SCBarCodeRules> tempEntityList = this.GetListByHQL(hql, "scbarcoderules.CurBarCode desc", 1, 1);

            if (tempEntityList.count > 0)
            {
                SCBarCodeRules entity = tempEntityList.list[0];
                DateTime curDate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd"));
                DateTime lastUpdateTime = DateTime.Parse(entity.DataUpdateTime.Value.ToString("yyyy-MM-dd"));
                TimeSpan ts = curDate - lastUpdateTime;
                if (ts.Days > 0)
                {
                    //当前时间-最后更新时间大于1天重新从1开始
                    if (bmsType == SCBarCodeRulesType.院感登记.Key)
                        nextBarCode = GetGKBarCode();
                    else
                        nextBarCode = 1;
                }
                else
                {
                    nextBarCode = entity.CurBarCode + 1;
                }
                entity.CurBarCode = nextBarCode;
                entity.DataUpdateTime = DateTime.Now;
                this.Save(entity);

            }
            else
            {
                #region 在同一事务里不生效
                nextBarCode = 1;
                SCBarCodeRules searial = new SCBarCodeRules();
                searial.LabID = labID;
                searial.BmsType = bmsType;
                searial.CurBarCode = nextBarCode;
                searial.DataUpdateTime = DateTime.Now;
                this.Save(searial);
                #endregion
                //throw new Exception(" Error:labID为:" + labID + ",bmsType为:" + bmsType + ",在ReaBmsSerial不存在记录,请联系系统管理员到【机构初始化】功能模块先进行系统初始化第五步!");
            }

            return nextBarCode;
        }
        private int GetGKBarCode()
        {
            int barCode = -1;

            string fristBarCode = JSONConfigHelp.GetString(SysConfig.GKSYS.Key, "FristBarCode");
            if (!string.IsNullOrEmpty(fristBarCode))
            {
                int.TryParse(fristBarCode, out barCode);
            }
            if (barCode < 0)
            {
                barCode = 900000;
            }
            return barCode;
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
                paravaluea[paranamea.IndexOf("rulesId")] = "" + Common.Public.GUIDHelp.GetGUIDLong();
            if (paranamea.IndexOf("labId") >= 0)
                paravaluea[paranamea.IndexOf("labId")] = "" + labID;
            if (paranamea.IndexOf("bmsType") >= 0)
                paravaluea[paranamea.IndexOf("bmsType")] = "" + bmsType;

            IList<SCBarCodeRules> tempEntityList = base.HibernateTemplate.FindByNamedQueryAndNamedParam<SCBarCodeRules>("P_GetReaBmsSerial", paranamea.ToArray(), paravaluea);
            if (tempEntityList.Count > 0) nextBarCode = tempEntityList[0].CurBarCode;

            return nextBarCode;
        }
        /// <summary>
        /// 默认不添加按LabID的过滤条件获取数据
        /// </summary>
        public IList<SCBarCodeRules> GetListOfNoLabIDByHql(string hqlWhere)
        {
            string strHQL = "select scbarcoderules from SCBarCodeRules scbarcoderules where 1=1 ";
            if (hqlWhere != null && hqlWhere.Length > 0)
            {
                strHQL += "and " + hqlWhere;
            }
            IList<SCBarCodeRules> tempList = this.HibernateTemplate.Find<SCBarCodeRules>(strHQL);
            return tempList;
        }

    }
}