
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IBLL.Business;
using ZhiFang.Common.Public;

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
        /// <param name="groupNo">小组ID</param>
        /// <param name="nodeName">站点名称</param>
        /// <returns>参数对象</returns>
        public BParameter GetParameterByParaNoAndGroupNoAndNodeName(string paraNo, long? groupNo, string nodeName)
        {
            EntityList<BParameter> tempList = this.SearchListByHQL("bparameter.ParaNo='" + paraNo + "'", 1, 1000);
            IEnumerable<BParameter> tpg;
            IEnumerable<BParameter> tpn;
            if (tempList.count > 0)
            {
                if (groupNo.HasValue)//是否传入小组ID
                {
                    #region
                    tpg = tempList.list.Where(p => p.GroupNo == groupNo);
                    if (tpg.Count() > 0)//是否有小组配置
                    {
                        if (nodeName != null && nodeName.Trim() != "")//是否传入站点ID
                        {
                            tpn = tpg.Where(p => p.BNodeTable != null && p.BNodeTable.Name == nodeName.Trim());
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
                        if (nodeName != null && nodeName.Trim() != "")
                        {
                            tpn = tempList.list.Where(p => p.BNodeTable != null && p.BNodeTable.Name == nodeName.Trim());
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
                    if (nodeName != null && nodeName.Trim() != "")
                    {
                        tpn = tempList.list.Where(p => p.BNodeTable != null && p.BNodeTable.Name == nodeName.Trim());
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
            }
            else
                return null;
        }

        public IBSCOperation IBSCOperation { get; set; }
        public bool AddAndSetCache()
        {
            bool a = ((IDBParameterDao)base.DBDao).Save(this.Entity);
            //添加操作记录
            //IBSCOperation.AddOperation(this.Entity.Id, this.Entity.LabID, (int)SCOperationType.系统参数添加, "系统参数添加");

            //设置系统参数缓存
            SetCache(this.Entity.ParaNo, this.Entity.ParaValue);
            return a;
        }
        public bool UpdateAndSetCache(string[] strParas)
        {
            bool a = ((IDBParameterDao)base.DBDao).Update(strParas);
            //添加操作记录
            //int type = (this.Entity.IsUse == true ? (int)SCOperationType.系统参数修改 : (int)SCOperationType.系统参数禁用);
            //string operationMemo = (this.Entity.IsUse == true ? "系统参数修改" : "系统参数禁用");
            //IBSCOperation.AddOperation(this.Entity.Id, this.Entity.LabID, type, operationMemo);

            //设置系统参数缓存
            SetCache(this.Entity.ParaNo, this.Entity.ParaValue);
            return a;
        }
        /// <summary>
        /// 同步webconfig的系统参数配置项(从webconfig过渡到数据库的系统参数表使用)
        /// </summary>
        public void SyncWebConfigToBParameter()
        {
            IList<BParameter> list = new List<BParameter>();
            //从缓存里判断是否已经同步处理
            #region 判断是否已经同步处理了
            string isSyncWebConfig = (string)this.GetCache(BParameterParaNoClass.IsSyncWebConfig.ToString());
            bool isSync = false;
            bool.TryParse(isSyncWebConfig, out isSync);
            if (isSync == false)
            {
                list = this.LoadAll();
                if (list != null && list.Count > 0)
                {
                    var IsSyncWebConfigList = list.Where(a => a.ParaNo == BParameterParaNoClass.IsSyncWebConfig.ToString());
                    if (IsSyncWebConfigList != null && IsSyncWebConfigList.Count() > 0)
                    {
                        string paraValue = IsSyncWebConfigList.ElementAt(0).ParaValue;
                        if (paraValue == "1" || paraValue.ToLower() == "true")
                        {
                            BParameterCache.IsSyncWebConfig = true;
                            SetCache(BParameterParaNoClass.IsSyncWebConfig.ToString(), true);
                        }
                        else
                        {
                            BParameterCache.IsSyncWebConfig = false;
                            SetCache(BParameterParaNoClass.IsSyncWebConfig.ToString(), false);
                        }
                    }

                    isSyncWebConfig = (string)this.GetCache(BParameterParaNoClass.IsSyncWebConfig.ToString());
                    bool.TryParse(isSyncWebConfig, out isSync);
                    ZhiFang.Common.Log.Log.Debug("是否已经同步:" + isSync);
                    #region 同步缓存及数据库中
                    if (isSync == false)
                    {
                        string uploadFilesPath = "", upLoadPicturePath = "", uploadConfigIson = "";
                        uploadFilesPath = ConfigHelper.GetConfigString(BParameterParaNoClass.UploadFilesPath.ToString()).Trim();
                        upLoadPicturePath = ConfigHelper.GetConfigString(BParameterParaNoClass.UpLoadPicturePath.ToString()).Trim();
                        uploadConfigIson = ConfigHelper.GetConfigString(BParameterParaNoClass.uploadConfigJson.ToString()).Trim();

                        //ZhiFang.Common.Log.Log.Debug("UploadFilesPath值:" + uploadFilesPath);
                        //ZhiFang.Common.Log.Log.Debug("UpLoadPicturePath值:" + upLoadPicturePath);
                        //ZhiFang.Common.Log.Log.Debug("uploadConfigIson值:" + uploadConfigIson);
                        //同步UploadFilesPath到数据库及缓存中
                        SetCacheAndUpdate(list, BParameterParaNoClass.UploadFilesPath.ToString(), uploadFilesPath);
                        SetCacheAndUpdate(list, BParameterParaNoClass.UpLoadPicturePath.ToString(), upLoadPicturePath);
                        SetCacheAndUpdate(list, BParameterParaNoClass.uploadConfigJson.ToString(), uploadConfigIson);
                        //设置是否已经同步为true
                        SetCacheAndUpdate(list, BParameterParaNoClass.IsSyncWebConfig.ToString(), "true");
                        BParameterCache.IsSyncWebConfig = true;
                    }
                    #endregion
                }
            }
            #endregion
        }
        /// <summary>
        /// 同步缓存及数据库中
        /// </summary>
        /// <param name="list"></param>
        /// <param name="paraNo"></param>
        /// <param name="paraValue"></param>
        private void SetCacheAndUpdate(IList<BParameter> list, string paraNo, string paraValue)
        {
            if (!String.IsNullOrEmpty(paraValue))
            {
                paraValue = paraValue.ToString().Replace(@"\", "&#92");
                //设置系统参数缓存
                SetCache(paraNo, paraValue);
                string isSyncWebConfig = (string)this.GetCache(BParameterParaNoClass.IsSyncWebConfig.ToString());
                bool isSync = false;
                bool.TryParse(isSyncWebConfig, out isSync);
                if (isSync == false)
                {
                    var tempList = list.Where(a => a.ParaNo == paraNo);
                    if (tempList != null && tempList.Count() > 0)
                    {
                        BParameter tempEntity = tempList.ElementAt(0);
                        tempEntity.ParaValue = paraValue;
                        ((IDBParameterDao)base.DBDao).Update(tempEntity);
                        //设置系统参数缓存
                        SetCache(paraNo, tempEntity.ParaValue);

                    }
                }
            }
        }

        /// <summary>
        /// 依参数编码获取系统参数信息
        /// </summary>
        /// <param name="paraNo"></param>
        /// <returns></returns>
        public IList<BParameter> SearchListByParaNo(string paraNo)
        {
            IList<BParameter> list = new List<BParameter>();
            if (!String.IsNullOrEmpty(paraNo))
                list = this.SearchListByHQL("bparameter.ParaNo='" + paraNo + "'");
            return list;
        }
        /// <summary>
        /// 获取当前应用程序指定CacheKey的Cache值
        /// </summary>
        /// <param name="CacheKey"></param>
        /// <returns></returns>
        public object GetCache(string cacheKey)
        {
            string cacheValue = "";
            if (BParameterCache.ApplicationCache != null)
                cacheValue = (string)BParameterCache.ApplicationCache.Application.Get(cacheKey);
            //ZhiFang.Common.Log.Log.Error("缓存值:" + cacheValue);
            //如果为空,从数据库读取
            if (String.IsNullOrEmpty(cacheValue))
            {
                IList<BParameter> list = SearchListByParaNo(cacheKey);
                if (list.Count > 0)
                {
                    cacheValue = list[0].ParaValue;
                    //ZhiFang.Common.Log.Log.Error("系统参数值:" + cacheValue);
                    //设置系统参数缓存
                    if (!String.IsNullOrEmpty(cacheValue))
                        cacheValue = cacheValue.ToString().Replace("&#92", @"\");
                    //如果从数据库里读取为空,再从web.config读取
                    if (String.IsNullOrEmpty(cacheValue))
                    {
                        cacheValue = ConfigHelper.GetConfigString(cacheKey).Trim();
                        //ZhiFang.Common.Log.Log.Error("web.config读取值:" + cacheValue);
                        //同步web.config的cacheKey到数据库及缓存中
                        if (!String.IsNullOrEmpty(cacheValue))
                            cacheValue = cacheValue.ToString().Replace("&#92", @"\");
                        SetCacheAndUpdate(list, cacheKey, cacheValue);
                    }
                }
                else
                {
                    cacheValue = ConfigHelper.GetConfigString(cacheKey).Trim();
                    if (!String.IsNullOrEmpty(cacheValue))
                        cacheValue = cacheValue.ToString().Replace("&#92", @"\");
                    //ZhiFang.Common.Log.Log.Error("web.config读取值:" + cacheValue);
                }
                SetCache(cacheKey, cacheValue);
            }
            return cacheValue;
        }

        /// <summary>
        /// 添加或者更新指定CacheKey的Cache值
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="objObject"></param>
        public void SetCache(string cacheKey, object objObject)
        {
            //
            string objValue = "";
            if (objObject != null && objObject.GetType().ToString() == "System.String")
            {
                objValue = objObject.ToString().Replace("&#92", @"\");
                //ZhiFang.Common.Log.Log.Debug("cacheValue:" + objValue);
            }

            if (BParameterCache.ApplicationCache.Application.AllKeys.Contains(cacheKey))
            {
                BParameterCache.ApplicationCache.Application.Set(cacheKey, String.IsNullOrEmpty(objValue) ? objValue : objValue);
            }
            else
            {
                BParameterCache.ApplicationCache.Application.Add(cacheKey, String.IsNullOrEmpty(objValue) ? objValue : objValue);
            }
        }

        /// <summary>
        /// 清除单一键缓存
        /// </summary>
        /// <param name="key"></param>
        public void RemoveOneCache(string CacheKey)
        {
            BParameterCache.ApplicationCache.Application.Remove(CacheKey);
        }
        /// <summary>
        /// 清除所有缓存WS
        /// </summary>
        public void RemoveAllCache()
        {
            if (BParameterCache.ApplicationCache.Application.Count > 0)
            {
                BParameterCache.ApplicationCache.Application.RemoveAll();
                //foreach (string key in HttpApplication.Application.Keys)
                //{
                //    HttpApplication.Application.Remove(key);
                //}
            }
        }
    }
}