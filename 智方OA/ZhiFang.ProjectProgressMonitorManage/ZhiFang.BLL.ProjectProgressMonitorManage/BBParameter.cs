using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;
using ZhiFang.IBLL.ProjectProgressMonitorManage;
using System.Web;
using ZhiFang.Common.Public;

namespace ZhiFang.BLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public class BBParameter : BaseBLL<BParameter>, ZhiFang.IBLL.ProjectProgressMonitorManage.IBBParameter
    {
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
            string isSyncWebConfig = (string)this.GetCache(BParameterParaNo.IsSyncWebConfig.ToString());
            bool isSync = false;
            bool.TryParse(isSyncWebConfig, out isSync);
            if (isSync == false)
            {
                list = this.LoadAll();
                if (list != null && list.Count > 0)
                {
                    var IsSyncWebConfigList = list.Where(a => a.ParaNo == BParameterParaNo.IsSyncWebConfig.ToString());
                    if (IsSyncWebConfigList != null && IsSyncWebConfigList.Count() > 0)
                    {
                        string paraValue = IsSyncWebConfigList.ElementAt(0).ParaValue;
                        if (paraValue == "1" || paraValue.ToLower() == "true")
                        {
                            BParameterCache.IsSyncWebConfig = true;
                            SetCache(BParameterParaNo.IsSyncWebConfig.ToString(), true);
                        }
                        else
                        {
                            BParameterCache.IsSyncWebConfig = false;
                            SetCache(BParameterParaNo.IsSyncWebConfig.ToString(), false);
                        }
                    }

                    isSyncWebConfig = (string)this.GetCache(BParameterParaNo.IsSyncWebConfig.ToString());
                    bool.TryParse(isSyncWebConfig, out isSync);
                    ZhiFang.Common.Log.Log.Debug("是否已经同步:" + isSync);
                    #region 同步缓存及数据库中
                    if (isSync == false)
                    {
                        string uploadFilesPath = "", upLoadPicturePath = "", uploadConfigIson = "";
                        uploadFilesPath = ConfigHelper.GetConfigString(BParameterParaNo.UploadFilesPath.ToString()).Trim();
                        upLoadPicturePath = ConfigHelper.GetConfigString(BParameterParaNo.UpLoadPicturePath.ToString()).Trim();
                        uploadConfigIson = ConfigHelper.GetConfigString(BParameterParaNo.uploadConfigIson.ToString()).Trim();

                        //ZhiFang.Common.Log.Log.Debug("UploadFilesPath值:" + uploadFilesPath);
                        //ZhiFang.Common.Log.Log.Debug("UpLoadPicturePath值:" + upLoadPicturePath);
                        //ZhiFang.Common.Log.Log.Debug("uploadConfigIson值:" + uploadConfigIson);
                        //同步UploadFilesPath到数据库及缓存中
                        SetCacheAndUpdate(list, BParameterParaNo.UploadFilesPath.ToString(), uploadFilesPath);
                        SetCacheAndUpdate(list, BParameterParaNo.UpLoadPicturePath.ToString(), upLoadPicturePath);
                        SetCacheAndUpdate(list, BParameterParaNo.uploadConfigIson.ToString(), uploadConfigIson);
                        //设置是否已经同步为true
                        SetCacheAndUpdate(list, BParameterParaNo.IsSyncWebConfig.ToString(), "true");
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
                string isSyncWebConfig = (string)this.GetCache(BParameterParaNo.IsSyncWebConfig.ToString());
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