using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.BLL.Base;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Common;
using ZhiFang.IBLL.Common;
using ZhiFang.IDAO.Common;

namespace ZhiFang.BLL.Common
{
    /// <summary>
    ///
    /// </summary>
    public class BBParameter : BaseBLL<BParameter>, IBBParameter
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
            IList<BParameter> tempList = this.SearchListByHQL("bparameter.IsUse=1 and bparameter.ParaNo='" + paraNo + "'");
            if (tempList.Count > 0)
                return tempList[0];
            else
                return null;
        }

        public bool AddAndSetCache()
        {
            bool a = ((IDBParameterDao)base.DBDao).Save(this.Entity);
            //设置系统参数缓存
            SetCache(this.Entity.ParaNo, this.Entity.ParaValue);
            return a;
        }
        public bool UpdateAndSetCache(string[] strParas)
        {
            bool a = ((IDBParameterDao)base.DBDao).Update(strParas);
            //设置系统参数缓存
            //SetCache(this.Entity.ParaNo, this.Entity.ParaValue);
            return a;
        }
        /// <summary>
        /// 同步webconfig的系统参数配置项(从webconfig过渡到数据库的系统参数表使用)
        /// </summary>
        public void SyncWebConfigToBParameter()
        {
            IList<BParameter> list = new List<BParameter>();
            
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
                string isSyncWebConfig = "";
                //string isSyncWebConfig = (string)this.GetCache(BParameterParaNoClass.IsSyncWebConfig.ToString());
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
            //if (BParameterCache.ApplicationCache != null)
            //    cacheValue = (string)BParameterCache.ApplicationCache.Application.Get(cacheKey);
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

            //if (BParameterCache.ApplicationCache.Application.AllKeys.Contains(cacheKey))
            //{
            //    BParameterCache.ApplicationCache.Application.Set(cacheKey, String.IsNullOrEmpty(objValue) ? objValue : objValue);
            //}
            //else
            //{
            //    BParameterCache.ApplicationCache.Application.Add(cacheKey, String.IsNullOrEmpty(objValue) ? objValue : objValue);
            //}
        }

        /// <summary>
        /// 清除单一键缓存
        /// </summary>
        /// <param name="key"></param>
        public void RemoveOneCache(string CacheKey)
        {
            //BParameterCache.ApplicationCache.Application.Remove(CacheKey);
        }
        /// <summary>
        /// 清除所有缓存WS
        /// </summary>
        public void RemoveAllCache()
        {
            //if (BParameterCache.ApplicationCache.Application.Count > 0)
            //{
            //    BParameterCache.ApplicationCache.Application.RemoveAll();
            //}
        }
    }
}