using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ZhiFang.Entity.WebAssist
{
    /// <summary>
    /// 公共配置类
    /// </summary>
    [DataContract]
    public class CommonConfig
    {
        #region Constructors
        public CommonConfig() { }
        #endregion

        /// <summary>
        /// 是否调用CS服务登录及验证用户信息
        /// </summary>
        [DataMember]
        public virtual bool ISHASCSLOGIN { get; set; }

        /// <summary>
        /// 登录成功后,是否调用数据库升级服务
        /// </summary>
        [DataMember]
        public virtual bool LOGIN_AFTER_ISUPDATEDB { get; set; }

        /// <summary>
        /// 第三方外部调用时的默认登录帐号
        /// </summary>
        [DataMember]
        public virtual string DEFAULT_ACCOUNT { get; set; }

        /// <summary>
        /// 第三方外部调用时的默认登录密码
        /// </summary>
        [DataMember]
        public virtual string DEFAULT_PWD { get; set; }

        /// <summary>
        /// HIS调用时,依传入HIS医生ID,获取到的医生信息
        /// </summary>
        [DataMember]
        public virtual string HISCALL_URL { get; set; }

        /// <summary>
        /// 按PUser的帐号及密码登录
        /// </summary>
        [DataMember]
        public virtual string LOGINOFPUSER_URL { get; set; }

        /// <summary>
        /// 按HIS医生对照码获取PUser的帐号及密码登录
        /// </summary>
        [DataMember]
        public virtual string LOGINOFPUSERBYHISCODE_URL { get; set; }

        //PUser用户注销服务
        [DataMember]
        public virtual string LOGOUTOFPUSER_URL { get; set; }

        //数据库手工升级服务
        [DataMember]
        public virtual string DBUPDATE_URL { get; set; }

        //PDF存放路径
        [DataMember]
        public virtual string PDF_SAVE_URL { get; set; }

        //PDFJS预览URL
        [DataMember]
        public virtual string PDFJS_URL { get; set; }
    }
    /// <summary>
    /// CS服务配置类
    /// </summary>
    /// 
    [DataContract]
    public class CSServerConfig
    {
        #region Constructors
        public CSServerConfig() { }
        #endregion
        //CS服务端口
        [DataMember]
        public virtual string CS_PORT { get; set; }

        //CS机构Id
        [DataMember]
        public virtual string CS_LABID { get; set; }

        //CS服务访问域名称domain
        [DataMember]
        public virtual string CS_DOMAIN { get; set; }

        //CS依传入的帐号及密码验证及返回用户帐号服务名称
        [DataMember]
        public virtual string CS_LONIN { get; set; }

        //CS密钥
        [DataMember]
        public virtual string CS_PMOPERTYPEKEY { get; set; }

        //CS血库服务端的域名或服务访问的IP
        [DataMember]
        public virtual string CS_BASE_URL { get; set; }


    }
    /// <summary>
    /// HIS接口配置类
    /// </summary>
    [DataContract]
    public class HisInterfaceConfig
    {
        #region Constructors
        public HisInterfaceConfig() { }
        #endregion

    }

    /// <summary>
    /// 血库系统运行配置类,对应前台的layui/config/bloodsconfig.js
    /// </summary>
    [DataContract]
    public class BloodsConfig
    {
        #region Constructors
        public BloodsConfig() { }
        #endregion

        [DataMember]
        public virtual CommonConfig Common { get; set; }

        [DataMember]
        public virtual CSServerConfig CSServer { get; set; }

        [DataMember]
        public virtual HisInterfaceConfig HisInterface { get; set; }
    }
}