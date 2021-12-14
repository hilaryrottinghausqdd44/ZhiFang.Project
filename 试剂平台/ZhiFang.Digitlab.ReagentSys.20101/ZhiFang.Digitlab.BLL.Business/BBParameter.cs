
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.BLL.Business
{
    /// <summary>
    ///
    /// </summary>
    public class BBParameter : BaseBLL<BParameter>, ZhiFang.Digitlab.IBLL.Business.IBBParameter
    {
        public bool GetOperLogPara()
        {
            bool r = false;
            BParameter p = Get(1000);
            if (p != null)
            {
                r = (p.ParaValue == "1" ? true : false);
            }
            return r;
        }

        public bool SetOperLogPara()
        {
            bool r = false;
            BParameter entity = Get(1000);
            if (entity != null)
            {
                entity = new BParameter();
                entity.LabID = 1;
                entity.Id = 1000;
                entity.Name = "是否开启样本操作日志";
                entity.SName = "是否开启样本操作日志";
                entity.ParaType = "是否开启样本操作日志";
                entity.ParaValue = "0";
                entity.ParaDesc = "是否开启样本操作日志";
                entity.IsUse = true;

                this.Entity = entity;
                r = this.Add();
            }
            else
            {
                if (entity.ParaValue == "0")
                {
                    entity.ParaValue = "1";
                }
                else
                {
                    entity.ParaValue = "0";
                }
                r = this.Edit();
            }

            return r;
        }

        public BParameter GetParameterByParaNo(string paraNo)
        {
            EntityList<BParameter> tempList = this.SearchListByHQL("bparameter.ParaNo='" + paraNo + "'", 1, 1000);
            if (tempList.count > 0)
                return tempList.list[0];
            else
                return null;
        }
        /// <summary>
        /// 根据参数paraNo、小组ID、站点ID获取参数信息
        /// </summary>
        /// <param name="paraNo">参数编码</param>
        /// <param name="GroupNo">小组ID</param>
        /// <param name="NodeID">站点ID</param>
        /// <returns>参数对象</returns>
        public BParameter GetParameterByParaNoAndGroupNoAndNodeID(string paraNo, long? GroupNo, long? NodeID)
        {
            EntityList<BParameter> tempList = this.SearchListByHQL("bparameter.ParaNo='" + paraNo + "'", 1, 1000);
            IEnumerable<BParameter> tpg;
            IEnumerable<BParameter> tpn;
            if (tempList.count > 0)
            {
                if (GroupNo.HasValue)//是否传入小组ID
                {
                    #region
                    tpg = tempList.list.Where(p => p.GroupNo == GroupNo);
                    if (tpg.Count() > 0)//是否有小组配置
                    {
                        if (NodeID.HasValue)//是否传入站点ID
                        {
                            tpn = tpg.Where(p => p.BNodeTable != null && p.BNodeTable.Id == NodeID);
                            if (tpn.Count() > 0)
                            {
                                return tpn.ElementAt(0);
                            }
                            else
                            {
                                tpn = tpg.Where(p => p.BNodeTable == null || p.BNodeTable.Id == 100000);//等于null时即为全站点
                                if (tpn.Count() > 0)
                                {
                                    return tpn.ElementAt(0);
                                }
                            }
                        }
                        else
                        {
                            tpn = tpg.Where(p => p.BNodeTable == null || p.BNodeTable.Id == 100000);//等于null时即为全站点
                            if (tpn.Count() > 0)
                            {
                                return tpn.ElementAt(0);
                            }
                        }
                        return tpg.ElementAt(0);
                    }
                    else
                    {
                        if (NodeID.HasValue)
                        {
                            tpn = tempList.list.Where(p => p.BNodeTable != null && p.BNodeTable.Id == NodeID);
                            if (tpn.Count() > 0)
                            {
                                return tpn.ElementAt(0);
                            }
                            else
                            {
                                tpn = tempList.list.Where(p => (p.BNodeTable == null || p.BNodeTable.Id == 100000) && p.GroupNo == null);//等于null时即为全站点
                                if (tpn.Count() > 0)
                                {
                                    return tpn.ElementAt(0);
                                }
                                //else
                                //{
                                //    return null;
                                //}
                            }
                        }
                        else
                        {
                            tpn = tempList.list.Where(p => (p.BNodeTable == null || p.BNodeTable.Id == 100000) && p.GroupNo == null);//等于null时即为全站点
                            if (tpn.Count() > 0)
                            {
                                return tpn.ElementAt(0);
                            }
                            //else
                            //{
                            //    return null;
                            //}
                        }
                    }
                    #endregion
                }
                else
                {
                    #region
                    if (NodeID.HasValue)
                    {
                        tpn = tempList.list.Where(p => p.BNodeTable != null && p.BNodeTable.Id == NodeID);
                        if (tpn.Count() > 0)
                        {
                            return tpn.ElementAt(0);
                        }
                        else
                        {
                            tpn = tempList.list.Where(p => (p.BNodeTable == null || p.BNodeTable.Id == 100000) && p.GroupNo == null);//等于null时即为全站点
                            if (tpn.Count() > 0)
                            {
                                return tpn.ElementAt(0);
                            }
                            //else
                            //{
                            //    return null;
                            //}
                        }
                    }
                    else
                    {
                        tpn = tempList.list.Where(p => (p.BNodeTable == null || p.BNodeTable.Id == 100000) && p.GroupNo == null);//等于null时即为全站点
                        if (tpn.Count() > 0)
                        {
                            return tpn.ElementAt(0);
                        }
                        //else
                        //{
                        //    return null;
                        //}
                    }
                    #endregion
                }
                return tempList.list[0];
            }
            else
                return null;
        }
        /// <summary>
        /// 根据参数paraNo、小组ID、站点名称获取参数信息
        /// </summary>
        /// <param name="paraNo">参数编码</param>
        /// <param name="GroupNo">小组ID</param>
        /// <param name="NodeName">站点名称</param>
        /// <returns>参数对象</returns>
        public BParameter GetParameterByParaNoAndGroupNoAndNodeName(string paraNo, long? GroupNo, string NodeName)
        {
            EntityList<BParameter> tempList = this.SearchListByHQL("bparameter.ParaNo='" + paraNo + "'", 1, 1000);
            IEnumerable<BParameter> tpg;
            IEnumerable<BParameter> tpn;
            if (tempList.count > 0)
            {
                if (GroupNo.HasValue)//是否传入小组ID
                {
                    #region
                    tpg = tempList.list.Where(p => p.GroupNo == GroupNo);
                    if (tpg.Count() > 0)//是否有小组配置
                    {
                        if (NodeName != null && NodeName.Trim() != "")//是否传入站点ID
                        {
                            tpn = tpg.Where(p => p.BNodeTable != null && p.BNodeTable.Name == NodeName.Trim());
                            if (tpn.Count() > 0)
                            {
                                return tpn.ElementAt(0);
                            }
                            else
                            {
                                tpn = tpg.Where(p => p.BNodeTable == null || p.BNodeTable.Id == 100000);//等于null时即为全站点
                                if (tpn.Count() > 0)
                                {
                                    return tpn.ElementAt(0);
                                }
                            }
                        }
                        else
                        {
                            tpn = tpg.Where(p => p.BNodeTable == null || p.BNodeTable.Id == 100000);//等于null时即为全站点
                            if (tpn.Count() > 0)
                            {
                                return tpn.ElementAt(0);
                            }
                        }
                        return tpg.ElementAt(0);
                    }
                    else
                    {
                        if (NodeName != null && NodeName.Trim() != "")
                        {
                            tpn = tempList.list.Where(p => p.BNodeTable != null && p.BNodeTable.Name == NodeName.Trim());
                            if (tpn.Count() > 0)
                            {
                                return tpn.ElementAt(0);
                            }
                            else
                            {
                                tpn = tempList.list.Where(p => (p.BNodeTable == null || p.BNodeTable.Id == 100000) && p.GroupNo == null);//等于null时即为全站点
                                if (tpn.Count() > 0)
                                {
                                    return tpn.ElementAt(0);
                                }
                                else
                                {
                                    return null;
                                }
                            }
                        }
                        else
                        {
                            tpn = tempList.list.Where(p => (p.BNodeTable == null || p.BNodeTable.Id == 100000) && p.GroupNo == null);//等于null时即为全站点
                            if (tpn.Count() > 0)
                            {
                                return tpn.ElementAt(0);
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region
                    if (NodeName != null && NodeName.Trim() != "")
                    {
                        tpn = tempList.list.Where(p => p.BNodeTable != null && p.BNodeTable.Name == NodeName.Trim());
                        if (tpn.Count() > 0)
                        {
                            return tpn.ElementAt(0);
                        }
                        else
                        {
                            tpn = tempList.list.Where(p => (p.BNodeTable == null || p.BNodeTable.Id == 100000) && p.GroupNo == null);//等于null时即为全站点
                            if (tpn.Count() > 0)
                            {
                                return tpn.ElementAt(0);
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                    else
                    {
                        tpn = tempList.list.Where(p => (p.BNodeTable == null || p.BNodeTable.Id == 100000) && p.GroupNo == null);//等于null时即为全站点
                        if (tpn.Count() > 0)
                        {
                            return tpn.ElementAt(0);
                        }
                        else
                        {
                            return null;
                        }
                    }
                    #endregion
                }
                return tempList.list[0];
            }
            else
                return null;
        }
    }
}