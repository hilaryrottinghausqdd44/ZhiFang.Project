using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ZhiFang.BloodTransfusion.hisinterface
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

        //医嘱申请获取患者的多少天内LIS检验结果的配置
        [DataMember]
        public virtual int GET_LISRESULT_DAYS { get; set; }

        //新增医嘱申请时,检验项目LIS结果为空时,设置的默认值
        [DataMember]
        public virtual string LIS_DEFAULT_ITEMSRESULT { get; set; }

        //是否调用CS服务登录及验证用户信息
        [DataMember]
        public virtual bool ISHASCSLOGIN { get; set; }

        //登录成功后,是否调用数据库升级服务
        [DataMember]
        public virtual bool LOGIN_AFTER_ISUPDATEDB { get; set; }

        //第三方外部调用时的默认登录帐号
        [DataMember]
        public virtual string DEFAULT_ACCOUNT { get; set; }

        //第三方外部调用时的默认登录密码
        [DataMember]
        public virtual string DEFAULT_PWD { get; set; }

        //HIS调用时,依传入HIS医生ID,获取到的医生信息
        [DataMember]
        public virtual string HISCALL_URL { get; set; }

        //按PUser的帐号及密码登录
        [DataMember]
        public virtual string LOGINOFPUSER_URL { get; set; }

        //按HIS医生对照码获取PUser的帐号及密码登录
        [DataMember]
        public virtual string LOGINOFPUSERBYHISCODE_URL { get; set; }

        //PUser用户注销服务
        [DataMember]
        public virtual string LOGOUTOFPUSER_URL { get; set; }

        //数据库手工升级服务
        [DataMember]
        public virtual string DBUPDATE_URL { get; set; }

        //用血申请PDF存放路径
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

        //按医嘱申请单号打印医嘱申请单
        [DataMember]
        public virtual string CS_BREQ_PRINT_URL { get; set; }

        //按患者病历号调用CS服务获取HIS病人信息
        [DataMember]
        public virtual string CS_GETPATINFO_URL { get; set; }

        //按医嘱申请单号调用CS服务返回医嘱申请信息给HIS
        [DataMember]
        public virtual string CS_TOHISDATA_URL { get; set; }

        //按医嘱申请单号调用CS服务作废HIS处理
        [DataMember]
        public virtual string CS_TOHISOBSOLETE_URL { get; set; }

        //查看当前库存信息
        [DataMember]
        public virtual string CS_GETBLOODQTY_URL { get; set; }

        //按申请单号+类型获取配血记录或发血记录信息
        [DataMember]
        public virtual string CS_GEBLOODBILLINFO_URL { get; set; }

        //按申请单号+类型+记录Id获取配血记录PDF或发血记录PDF信息
        [DataMember]
        public virtual string CS_GEBLOODREPORTPDF_URL { get; set; }

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

        //医嘱申请传入的患者参数非空字段 //就诊号:admId或病历号:patNo
        [DataMember]
        public virtual string NONEMPTYFIELD { get; set; }

        //医嘱申请单在审核完成后是否返回给HIS
        [DataMember]
        public virtual bool ISTOHISDATA { get; set; }

        //医嘱申请作废时是否调用HIS作废接口
        [DataMember]
        public virtual bool ISTOHISOBSOLETE { get; set; }

        //用血申请+,是否隐藏科室查询项
        [DataMember]
        public virtual bool ISHIDEPTNOOFSEARCH { get; set; }

        //用血申请+,是否隐藏医生查询项
        [DataMember]
        public virtual bool ISHIDEDOCTORNOOFSEARCH { get; set; }

        //用血申请+,是否隐藏就诊类型查询项
        [DataMember]
        public virtual bool ISHIDEBReqType { get; set; }

        //用血申请+,是否隐藏申请类型查询项
        [DataMember]
        public virtual bool ISHIDEBloodUseType { get; set; }

        //用血申请+,是否隐藏模糊查询项
        [DataMember]
        public virtual bool ISHIDELikeSearch { get; set; }

        //用血申请登记时,是否隐藏医生录入项
        [DataMember]
        public virtual bool ISHIDEDOCTORNOOFADD { get; set; }

        //用血申请登记时,是否隐藏入院日期录入项
        [DataMember]
        public virtual bool ISHIDETOHOSDETEOFADD { get; set; }

        //在用血申请确认提交时,紧急用血是否自动上传数据到HIS
        [DataMember]
        public virtual bool ISBUSETIMETYPEIDAUTOUPLOADADD { get; set; }

        //获取LIS检验结果时,是否同按门诊号及住院号获取:门诊号:PatID；就诊号：AdmId; 住院号：PatNo (PatNo=00000123 or PatNo= 123) 
        [DataMember]
        public virtual bool ISGETLISRESULTOFPATIDORPATNO { get; set; }

        //紧急用血是否设置为不可操作或者只读(不进行控制,已停用,后续应该弃用)
        [DataMember]
        public virtual bool ISBUSETIMETYPEIDREADONLY { get; set; }

        //(患者ABO及患者Rh从LIS获取为空时)是否允许患者ABO及患者Rh手工选择
        [DataMember]
        public virtual bool ISALLOWPATABOANDRHOPT { get; set; }

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