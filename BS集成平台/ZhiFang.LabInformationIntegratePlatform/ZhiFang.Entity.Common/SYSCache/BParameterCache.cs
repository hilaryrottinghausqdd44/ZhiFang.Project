using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace ZhiFang.Entity.Common
{
    public static class BParameterCache
    {
        /// <summary>
        /// BParameter是否已经同步webconfig
        /// </summary>
        public static bool IsSyncWebConfig { get; set; }
        private static HttpApplication _ApplicationCache;
        /// <summary>
        /// 系统参数缓存
        /// </summary>
        public static HttpApplication ApplicationCache
        {
            get
            {
                if (_ApplicationCache == null)
                {
                    _ApplicationCache = new HttpApplication();
                }
                return _ApplicationCache;
            }
            set { _ApplicationCache = value; }
        }

        /// <summary>
        /// 清除单一键缓存
        /// </summary>
        /// <param name="key"></param>
        public static void RemoveOneCache(string CacheKey)
        {
            BParameterCache.ApplicationCache.Application.Remove(CacheKey);
        }
        /// <summary>
        /// 清除所有缓存WS
        /// </summary>
        public static void RemoveAllCache()
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

        /// <summary>
        /// 系统参数编码
        /// </summary>
        public enum BParameterParaNo
        {
            IsSyncWebConfig,//系统参数表是否同步过web.config的相关配置项
            UploadFilesPath,//附件上传保存路径
            UpLoadPicturePath,
            uploadConfigIson,//在线编辑器的上传配置文件的路径
            ExcelExportSavePath,//execl导出的临时保存路径
            UploadEmpSignPath,//电子签名保存路径
            SaveHelpHtmlAndJson,//帮助系统生成html保存路径
            SecAccepterAccount//验收确认方式
        }
    }    
    public static class BParameterParaNoClass
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> IsSyncWebConfig = new KeyValuePair<string, BaseClassDicEntity>("IsSyncWebConfig", new BaseClassDicEntity() { Id = "IsSyncWebConfig", Name = "系统参数表是否同步过web.config的相关配置项", Code = "IsSyncWebConfig", FontColor = "", BGColor = "" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> UploadFilesPath = new KeyValuePair<string, BaseClassDicEntity>("UploadFilesPath", new BaseClassDicEntity() { Id = "UploadFilesPath", Name = "附件上传保存路径", Code = "UploadFilesPath", FontColor = "", BGColor = "" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> UpLoadPicturePath = new KeyValuePair<string, BaseClassDicEntity>("UpLoadPicturePath", new BaseClassDicEntity() { Id = "UpLoadPicturePath", Name = "图片上传保存路径", Code = "UpLoadPicturePath", FontColor = "", BGColor = "" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> uploadConfigJson = new KeyValuePair<string, BaseClassDicEntity>("uploadConfigJson", new BaseClassDicEntity() { Id = "uploadConfigJson", Name = "在线编辑器的上传配置文件的路径", Code = "uploadConfigJson", FontColor = "", BGColor = "" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> ExcelExportSavePath = new KeyValuePair<string, BaseClassDicEntity>("ExcelExportSavePath", new BaseClassDicEntity() { Id = "ExcelExportSavePath", Name = "execl导出的临时保存路径", Code = "ExcelExportSavePath", FontColor = "", BGColor = "" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> UploadEmpSignPath = new KeyValuePair<string, BaseClassDicEntity>("UploadEmpSignPath", new BaseClassDicEntity() { Id = "UploadEmpSignPath", Name = "电子签名保存路径", Code = "UploadEmpSignPath", FontColor = "", BGColor = "" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> SaveHelpHtmlAndJson = new KeyValuePair<string, BaseClassDicEntity>("SaveHelpHtmlAndJson", new BaseClassDicEntity() { Id = "SaveHelpHtmlAndJson", Name = "帮助系统生成html保存路径", Code = "SaveHelpHtmlAndJson", FontColor = "", BGColor = "" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> SecAccepterAccount = new KeyValuePair<string, BaseClassDicEntity>("SecAccepterAccount", new BaseClassDicEntity() { Id = "SecAccepterAccount", Name = "验收确认方式", Code = "SecAccepterAccount", FontColor = "", BGColor = "" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(BParameterParaNoClass.IsSyncWebConfig.Key, BParameterParaNoClass.IsSyncWebConfig.Value);
            dic.Add(BParameterParaNoClass.UploadFilesPath.Key, BParameterParaNoClass.UploadFilesPath.Value);
            dic.Add(BParameterParaNoClass.UpLoadPicturePath.Key, BParameterParaNoClass.UpLoadPicturePath.Value);
            dic.Add(BParameterParaNoClass.uploadConfigJson.Key, BParameterParaNoClass.uploadConfigJson.Value);

            dic.Add(BParameterParaNoClass.ExcelExportSavePath.Key, BParameterParaNoClass.ExcelExportSavePath.Value);
            dic.Add(BParameterParaNoClass.UploadEmpSignPath.Key, BParameterParaNoClass.UploadEmpSignPath.Value);
            dic.Add(BParameterParaNoClass.SaveHelpHtmlAndJson.Key, BParameterParaNoClass.SaveHelpHtmlAndJson.Value);
            dic.Add(BParameterParaNoClass.SecAccepterAccount.Key, BParameterParaNoClass.SecAccepterAccount.Value);
            return dic;
        }
    }
}
