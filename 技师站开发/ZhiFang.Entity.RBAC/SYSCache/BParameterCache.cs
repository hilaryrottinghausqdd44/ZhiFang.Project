using System.Web;

namespace ZhiFang.Entity.RBAC
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
        QMSFFReviseNoRule//QMS文档修订号规则:修订号规则:固定前缀|变化前缀(默认取年份)|当前序号
    }
}
