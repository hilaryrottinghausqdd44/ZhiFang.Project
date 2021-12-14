using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.Entity.Common;

namespace ZhiFang.Entity.LIIP
{
    #region 系统字典
    /// <summary>
    /// 门诊系统用户消费类型
    /// </summary>
    public static class ZFSystem
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 智方_检验之星 = new KeyValuePair<string, BaseClassDicEntity>("1001", new BaseClassDicEntity() { Id = "1001", Name = "智方_检验之星", Code = "ZF_LAB_START", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 智方_报告系统 = new KeyValuePair<string, BaseClassDicEntity>("1002", new BaseClassDicEntity() { Id = "1002", Name = "智方_报告系统", Code = "ZF_ReportFormQueryPrint", FontColor = "#ffffff", BGColor = "#71ba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 智方_检验之星_前处理 = new KeyValuePair<string, BaseClassDicEntity>("1003", new BaseClassDicEntity() { Id = "1003", Name = "智方_检验之星_前处理", Code = "ZF_PRE", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 智方_试剂系统 = new KeyValuePair<string, BaseClassDicEntity>("1011", new BaseClassDicEntity() { Id = "1011", Name = "智方_试剂系统", Code = "ZF_REA", FontColor = "#ffffff", BGColor = "#7cba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 智方_QMS系统 = new KeyValuePair<string, BaseClassDicEntity>("1021", new BaseClassDicEntity() { Id = "1021", Name = "智方_QMS系统", Code = "ZF_QMS", FontColor = "#ffffff", BGColor = "#7cba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 智方_统计分析 = new KeyValuePair<string, BaseClassDicEntity>("1031", new BaseClassDicEntity() { Id = "1031", Name = "智方_统计分析", Code = "ZF_SAS", FontColor = "#ffffff", BGColor = "#7cba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 智方_检验之星6 = new KeyValuePair<string, BaseClassDicEntity>("1041", new BaseClassDicEntity() { Id = "1041", Name = "智方_检验之星6", Code = "ZF_LabStar6", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 智方_财务统计 = new KeyValuePair<string, BaseClassDicEntity>("1051", new BaseClassDicEntity() { Id = "1051", Name = "智方_财务统计", Code = "ZF_BA", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 质控平台 = new KeyValuePair<string, BaseClassDicEntity>("1061", new BaseClassDicEntity() { Id = "1061", Name = "质控平台", Code = "ZF_IEQA", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> WebLis平台 = new KeyValuePair<string, BaseClassDicEntity>("1071", new BaseClassDicEntity() { Id = "1071", Name = "WebLis平台", Code = "ZF_WEBLIS", FontColor = "#ffffff", BGColor = "#f4c600" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 智方_第三方系统 = new KeyValuePair<string, BaseClassDicEntity>("2001", new BaseClassDicEntity() { Id = "2001", Name = "智方_第三方系统", Code = "ZF_OTTH", FontColor = "#ffffff", BGColor = "#f4c600" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 智方_LIS平台 = new KeyValuePair<string, BaseClassDicEntity>("9999", new BaseClassDicEntity() { Id = "9999", Name = "智方_LIS平台", Code = "ZF_LIIP", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();

            dic.Add(ZFSystem.智方_检验之星.Key, ZFSystem.智方_检验之星.Value);
            dic.Add(ZFSystem.智方_报告系统.Key, ZFSystem.智方_报告系统.Value);
            dic.Add(ZFSystem.智方_试剂系统.Key, ZFSystem.智方_试剂系统.Value);
            dic.Add(ZFSystem.智方_QMS系统.Key, ZFSystem.智方_QMS系统.Value);
            dic.Add(ZFSystem.智方_统计分析.Key, ZFSystem.智方_统计分析.Value);
            dic.Add(ZFSystem.智方_检验之星_前处理.Key, ZFSystem.智方_检验之星_前处理.Value);
            dic.Add(ZFSystem.智方_检验之星6.Key, ZFSystem.智方_检验之星6.Value);
            dic.Add(ZFSystem.智方_LIS平台.Key, ZFSystem.智方_LIS平台.Value);
            dic.Add(ZFSystem.质控平台.Key, ZFSystem.质控平台.Value);
            dic.Add(ZFSystem.WebLis平台.Key, ZFSystem.WebLis平台.Value);
            return dic;
        }
    }
    public static class ZFSystemRole
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 智方_系统角色_检验技师 = new KeyValuePair<string, BaseClassDicEntity>("1001", new BaseClassDicEntity() { Id = "1001", Name = "检验技师", Code = "ZF_Tech", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 智方_系统角色_护士 = new KeyValuePair<string, BaseClassDicEntity>("2001", new BaseClassDicEntity() { Id = "2001", Name = "护士", Code = "ZF_Nurse", FontColor = "#ffffff", BGColor = "#71ba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 智方_系统角色_医生 = new KeyValuePair<string, BaseClassDicEntity>("3001", new BaseClassDicEntity() { Id = "3001", Name = "医生", Code = "ZF_Doctor", FontColor = "#ffffff", BGColor = "#7cba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 智方_系统角色_物流护工 = new KeyValuePair<string, BaseClassDicEntity>("4001", new BaseClassDicEntity() { Id = "4001", Name = "物流护工", Code = "ZF_Logistics", FontColor = "#ffffff", BGColor = "#7dba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 智方_系统角色_危急值用户 = new KeyValuePair<string, BaseClassDicEntity>("5001", new BaseClassDicEntity() { Id = "5001", Name = "智方_系统角色_危急值用户", Code = "ZF_Logistics", FontColor = "#ffffff", BGColor = "#7dba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 智方_系统角色_管理员 = new KeyValuePair<string, BaseClassDicEntity>("9001", new BaseClassDicEntity() { Id = "9001", Name = "管理员", Code = "ZF_Admin", FontColor = "#ffffff", BGColor = "#7cea59" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();

            dic.Add(智方_系统角色_检验技师.Key, 智方_系统角色_检验技师.Value);
            dic.Add(智方_系统角色_护士.Key, 智方_系统角色_护士.Value);
            dic.Add(智方_系统角色_医生.Key, 智方_系统角色_医生.Value);
            dic.Add(智方_系统角色_物流护工.Key, 智方_系统角色_物流护工.Value);
            dic.Add(智方_系统角色_危急值用户.Key, 智方_系统角色_危急值用户.Value);
            dic.Add(智方_系统角色_管理员.Key, 智方_系统角色_管理员.Value);
            return dic;
        }
    }

    #region 消息
    /// <summary>
    /// 消息类型
    /// 普通消息1001-1900
    /// 第三方1900-1999
    /// 其它待定
    /// </summary>
    public static class ZFSCMsgType
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> LIS危急值 = new KeyValuePair<string, BaseClassDicEntity>("1001", new BaseClassDicEntity() { Id = "1001", Name = "LIS危急值", Code = "ZF_LAB_START_CV", FontColor = "#ffffff", BGColor = "#f41600" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 前处理样本拒收 = new KeyValuePair<string, BaseClassDicEntity>("1002", new BaseClassDicEntity() { Id = "1002", Name = "前处理样本拒收", Code = "ZF_PRE_REJECTION", FontColor = "#ffffff", BGColor = "#aa1600" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 检验报告延迟 = new KeyValuePair<string, BaseClassDicEntity>("1003", new BaseClassDicEntity() { Id = "1003", Name = "检验报告延迟", Code = "ZF_LAB_START_REPORT_DELAY", FontColor = "#ffffff", BGColor = "#a71600" });


        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> LIS平台系统消息 = new KeyValuePair<string, BaseClassDicEntity>("9001", new BaseClassDicEntity() { Id = "9001", Name = "LIS平台系统消息", Code = "ZF_LIIP_MSG", FontColor = "#ffffff", BGColor = "#733a59" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> LIS平台即时通讯系统消息 = new KeyValuePair<string, BaseClassDicEntity>("9002", new BaseClassDicEntity() { Id = "9002", Name = "LIS平台即时通讯系统消息", Code = "ZF_LIIP_IM", FontColor = "#ffffff", BGColor = "#733a59" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 第三方病理系统消息 = new KeyValuePair<string, BaseClassDicEntity>("1911", new BaseClassDicEntity() { Id = "1911", Name = "第三方病理系统消息", Code = "OTTH_PATHOLOGY", FontColor = "#ffffff", BGColor = "#7c2259" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 第三方病理危急值消息 = new KeyValuePair<string, BaseClassDicEntity>("1912", new BaseClassDicEntity() { Id = "1912", Name = "第三方病理危急值消息", Code = "OTTH_PATHOLOGY_CV", FontColor = "#ffffff", BGColor = "#7c2259" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 第三方影像系统消息 = new KeyValuePair<string, BaseClassDicEntity>("1921", new BaseClassDicEntity() { Id = "1921", Name = "第三方影像系统消息", Code = "OTTH_PACS", FontColor = "#ffffff", BGColor = "#7c2329" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 第三方影像危急值消息 = new KeyValuePair<string, BaseClassDicEntity>("1922", new BaseClassDicEntity() { Id = "1922", Name = "第三方影像危急值消息", Code = "OTTH_PACS_CV", FontColor = "#ffffff", BGColor = "#7c2329" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 第三方超声系统消息 = new KeyValuePair<string, BaseClassDicEntity>("1931", new BaseClassDicEntity() { Id = "1931", Name = "第三方超声系统消息", Code = "OTTH_USR", FontColor = "#ffffff", BGColor = "#7c2259" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 第三方超声危急值消息 = new KeyValuePair<string, BaseClassDicEntity>("1932", new BaseClassDicEntity() { Id = "1932", Name = "第三方超声危急值消息", Code = "OTTH_USR_CV", FontColor = "#ffffff", BGColor = "#7c2259" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 第三方内镜系统消息 = new KeyValuePair<string, BaseClassDicEntity>("1941", new BaseClassDicEntity() { Id = "1941", Name = "第三方内镜系统消息", Code = "OTTH_ENDOSCOPY", FontColor = "#ffffff", BGColor = "#7c2259" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 第三方内镜危急值消息 = new KeyValuePair<string, BaseClassDicEntity>("1942", new BaseClassDicEntity() { Id = "1942", Name = "第三方内镜危急值消息", Code = "OTTH_HOLTER_CV", FontColor = "#ffffff", BGColor = "#7c2259" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 第三方心电图系统消息 = new KeyValuePair<string, BaseClassDicEntity>("1951", new BaseClassDicEntity() { Id = "1951", Name = "第三方心电图系统消息", Code = "OTTH_HOLTER", FontColor = "#ffffff", BGColor = "#7c2259" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 第三方心电图危急值消息 = new KeyValuePair<string, BaseClassDicEntity>("1952", new BaseClassDicEntity() { Id = "1952", Name = "第三方心电图危急值消息", Code = "OTTH_HOLTER_CV", FontColor = "#ffffff", BGColor = "#7c2259" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 第三方其它消息 = new KeyValuePair<string, BaseClassDicEntity>("1999", new BaseClassDicEntity() { Id = "1999", Name = "第三方其它消息", Code = "OTTH_Other", FontColor = "#ffffff", BGColor = "#733a59" });


        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();

            dic.Add(LIS危急值.Key, LIS危急值.Value);
            dic.Add(前处理样本拒收.Key, 前处理样本拒收.Value);
            dic.Add(检验报告延迟.Key, 检验报告延迟.Value);
            dic.Add(LIS平台系统消息.Key, LIS平台系统消息.Value);
            dic.Add(LIS平台即时通讯系统消息.Key, LIS平台即时通讯系统消息.Value);
            dic.Add(第三方病理系统消息.Key, 第三方病理系统消息.Value);
            dic.Add(第三方病理危急值消息.Key, 第三方病理危急值消息.Value);
            dic.Add(第三方影像系统消息.Key, 第三方影像系统消息.Value);
            dic.Add(第三方影像危急值消息.Key, 第三方影像危急值消息.Value);
            dic.Add(第三方超声系统消息.Key, 第三方超声系统消息.Value);
            dic.Add(第三方超声危急值消息.Key, 第三方超声危急值消息.Value);
            dic.Add(第三方内镜系统消息.Key, 第三方内镜系统消息.Value);
            dic.Add(第三方内镜危急值消息.Key, 第三方内镜危急值消息.Value);
            dic.Add(第三方心电图系统消息.Key, 第三方心电图系统消息.Value);
            dic.Add(第三方心电图危急值消息.Key, 第三方心电图危急值消息.Value);
            dic.Add(第三方其它消息.Key, 第三方其它消息.Value);
            return dic;
        }
    }
    public static class ZFSCMsgStatus_ZF_LIIP_MSG
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> LIS平台消息发送 = new KeyValuePair<string, BaseClassDicEntity>("1001", new BaseClassDicEntity() { Id = "1001", Name = "LIS平台消息发送", Code = "ZF_LIIP_MSG_Send", FontColor = "#ffffff", BGColor = "#f41600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> LIS平台消息撤回 = new KeyValuePair<string, BaseClassDicEntity>("1002", new BaseClassDicEntity() { Id = "1002", Name = "LIS平台消息撤回", Code = "ZF_LIIP_MSG_ReBack", FontColor = "#ffffff", BGColor = "#f41600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> LIS平台消息处理 = new KeyValuePair<string, BaseClassDicEntity>("1003", new BaseClassDicEntity() { Id = "1003", Name = "LIS平台消息处理", Code = "ZF_LIIP_MSG_Handle", FontColor = "#ffffff", BGColor = "#f41600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> LIS平台消息处理撤销 = new KeyValuePair<string, BaseClassDicEntity>("1004", new BaseClassDicEntity() { Id = "1004", Name = "LIS平台消息处理撤销", Code = "ZF_LIIP_MSG_HandleCancel", FontColor = "#ffffff", BGColor = "#f41600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> LIS平台消息确认 = new KeyValuePair<string, BaseClassDicEntity>("1005", new BaseClassDicEntity() { Id = "1005", Name = "LIS平台消息确认", Code = "ZF_LIIP_MSG_Confirm", FontColor = "#ffffff", BGColor = "#f41600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> LIS平台消息确认撤销 = new KeyValuePair<string, BaseClassDicEntity>("1006", new BaseClassDicEntity() { Id = "1006", Name = "LIS平台消息确认撤销", Code = "ZF_LIIP_MSG_ConfirmCancel", FontColor = "#ffffff", BGColor = "#f41600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> LIS平台消息超时重发 = new KeyValuePair<string, BaseClassDicEntity>("1007", new BaseClassDicEntity() { Id = "1007", Name = "LIS平台消息超时重发", Code = "ZF_LIIP_MSG_Send", FontColor = "#ffffff", BGColor = "#f41600" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(LIS平台消息发送.Key, LIS平台消息发送.Value);
            dic.Add(LIS平台消息撤回.Key, LIS平台消息撤回.Value);
            dic.Add(LIS平台消息处理.Key, LIS平台消息处理.Value);
            dic.Add(LIS平台消息处理撤销.Key, LIS平台消息处理撤销.Value);
            dic.Add(LIS平台消息确认.Key, LIS平台消息确认.Value);
            dic.Add(LIS平台消息确认撤销.Key, LIS平台消息确认撤销.Value);
            dic.Add(LIS平台消息超时重发.Key, LIS平台消息超时重发.Value);
            return dic;
        }
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> ZFSCMsgType = new KeyValuePair<string, BaseClassDicEntity>("9999", new BaseClassDicEntity() { Id = "9999", Name = "LIS平台系统消息", Code = "ZF_LIIP_MSG", FontColor = "#ffffff", BGColor = "#733a59" });
    }
    public static class ZFSCMsgStatus_OTTH_PATHOLOGY
    {
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(ZFSCMsgStatus_ZF_LIIP_MSG.LIS平台消息发送.Key, ZFSCMsgStatus_ZF_LIIP_MSG.LIS平台消息发送.Value);
            dic.Add(ZFSCMsgStatus_ZF_LIIP_MSG.LIS平台消息撤回.Key, ZFSCMsgStatus_ZF_LIIP_MSG.LIS平台消息撤回.Value);
            dic.Add(ZFSCMsgStatus_ZF_LIIP_MSG.LIS平台消息处理.Key, ZFSCMsgStatus_ZF_LIIP_MSG.LIS平台消息处理.Value);
            dic.Add(ZFSCMsgStatus_ZF_LIIP_MSG.LIS平台消息处理撤销.Key, ZFSCMsgStatus_ZF_LIIP_MSG.LIS平台消息处理撤销.Value);
            dic.Add(ZFSCMsgStatus_ZF_LIIP_MSG.LIS平台消息确认.Key, ZFSCMsgStatus_ZF_LIIP_MSG.LIS平台消息确认.Value);
            dic.Add(ZFSCMsgStatus_ZF_LIIP_MSG.LIS平台消息确认撤销.Key, ZFSCMsgStatus_ZF_LIIP_MSG.LIS平台消息确认撤销.Value);
            dic.Add(ZFSCMsgStatus_ZF_LIIP_MSG.LIS平台消息超时重发.Key, ZFSCMsgStatus_ZF_LIIP_MSG.LIS平台消息超时重发.Value);
            return dic;
        }
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> ZFSCMsgType = new KeyValuePair<string, BaseClassDicEntity>("1911", new BaseClassDicEntity() { Id = "1911", Name = "第三方病理系统消息", Code = "OTTH_PATHOLOGY", FontColor = "#ffffff", BGColor = "#7c2259" });
    }

    /// <summary>
    /// 消息类型对应的状态
    /// 普通消息0-999
    /// 危急值1000-1999
    /// 其它待定
    /// </summary>
    public static class ZFSCMsgStatus_ZF_LAB_START_CV
    {
        private const string V = "CV_Confirm";
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 危急值发送 = new KeyValuePair<string, BaseClassDicEntity>("1001", new BaseClassDicEntity() { Id = "1001", Name = "危急值发送", Code = "CV", FontColor = "#ffffff", BGColor = "#f41600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 危急值撤回 = new KeyValuePair<string, BaseClassDicEntity>("1002", new BaseClassDicEntity() { Id = "1002", Name = "危急值撤回", Code = "CV_ReBack", FontColor = "#ffffff", BGColor = "#7c2a59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 危急值上报 = new KeyValuePair<string, BaseClassDicEntity>("1003", new BaseClassDicEntity() { Id = "1003", Name = "危急值上报", Code = "CV_WarningUpload", FontColor = "#ffffff", BGColor = "#71ba59" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 危急值处理 = new KeyValuePair<string, BaseClassDicEntity>("1004", new BaseClassDicEntity() { Id = "1004", Name = "危急值处理", Code = "CV_Handle", FontColor = "#ffffff", BGColor = "#5d3a59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 危机值处理撤销 = new KeyValuePair<string, BaseClassDicEntity>("1005", new BaseClassDicEntity() { Id = "1005", Name = "危机值处理撤销", Code = "CV_HandleCancel", FontColor = "#ffffff", BGColor = "#6c4a59" });

        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 危急值确认 = new KeyValuePair<string, BaseClassDicEntity>("1006", new BaseClassDicEntity() { Id = "1006", Name = "危急值确认", Code = "CV_Confirm", FontColor = "#ffffff", BGColor = "#7db559" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 危机值确认撤销 = new KeyValuePair<string, BaseClassDicEntity>("1007", new BaseClassDicEntity() { Id = "1007", Name = "危机值确认撤销", Code = "CV_ConfirmCancel", FontColor = "#ffffff", BGColor = "#ace659" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 危急值超时重发 = new KeyValuePair<string, BaseClassDicEntity>("1008", new BaseClassDicEntity() { Id = "1008", Name = "危急值超时重发", Code = V, FontColor = "#ffffff", BGColor = "#7db559" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();

            dic.Add(危急值发送.Key, 危急值发送.Value);
            dic.Add(危急值撤回.Key, 危急值撤回.Value);
            dic.Add(危急值上报.Key, 危急值上报.Value);
            dic.Add(危急值处理.Key, 危急值处理.Value);
            dic.Add(危机值处理撤销.Key, 危机值处理撤销.Value);
            dic.Add(危急值确认.Key, 危急值确认.Value);
            dic.Add(危机值确认撤销.Key, 危机值确认撤销.Value);
            dic.Add(危急值超时重发.Key, 危急值超时重发.Value);
            return dic;
        }
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> ZFSCMsgType = new KeyValuePair<string, BaseClassDicEntity>("1001", new BaseClassDicEntity() { Id = "1001", Name = "LIS危急值", Code = "ZF_LAB_START_CV", FontColor = "#ffffff", BGColor = "#f41600" });
    }
    public static class ZFSCMsgStatus_OTTH_PATHOLOGY_CV
    {
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();

            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值发送.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值发送.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值撤回.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值撤回.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值上报.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值上报.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值处理.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值处理.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危机值处理撤销.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危机值处理撤销.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值确认.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值确认.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危机值确认撤销.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危机值确认撤销.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值超时重发.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值超时重发.Value);
            return dic;
        }
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> ZFSCMsgType = new KeyValuePair<string, BaseClassDicEntity>("1922", new BaseClassDicEntity() { Id = "1922", Name = "第三方病理危急值消息", Code = "OTTH_PATHOLOGY_CV", FontColor = "#ffffff", BGColor = "#f41600" });
    }
    public static class ZFSCMsgStatus_OTTH_PACS_CV
    {
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();

            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值发送.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值发送.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值撤回.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值撤回.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值上报.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值上报.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值处理.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值处理.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危机值处理撤销.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危机值处理撤销.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值确认.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值确认.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危机值确认撤销.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危机值确认撤销.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值超时重发.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值超时重发.Value);
            return dic;
        }
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> ZFSCMsgType = new KeyValuePair<string, BaseClassDicEntity>("1922", new BaseClassDicEntity() { Id = "1922", Name = "第三方影像危急值消息", Code = "OTTH_PACS_CV", FontColor = "#ffffff", BGColor = "#7c2329" });
    }
    public static class ZFSCMsgStatus_OTTH_USR_CV
    {
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();

            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值发送.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值发送.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值撤回.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值撤回.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值上报.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值上报.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值处理.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值处理.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危机值处理撤销.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危机值处理撤销.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值确认.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值确认.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危机值确认撤销.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危机值确认撤销.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值超时重发.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值超时重发.Value);
            return dic;
        }
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> ZFSCMsgType = new KeyValuePair<string, BaseClassDicEntity>("1932", new BaseClassDicEntity() { Id = "1932", Name = "第三方超声危急值消息", Code = "OTTH_USR_CV", FontColor = "#ffffff", BGColor = "#7c2259" });
    }
    public static class ZFSCMsgStatus_OTTH_ENDOSCOPY_CV
    {
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();

            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值发送.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值发送.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值撤回.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值撤回.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值上报.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值上报.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值处理.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值处理.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危机值处理撤销.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危机值处理撤销.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值确认.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值确认.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危机值确认撤销.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危机值确认撤销.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值超时重发.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值超时重发.Value);
            return dic;
        }
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> ZFSCMsgType = new KeyValuePair<string, BaseClassDicEntity>("1942", new BaseClassDicEntity() { Id = "1942", Name = "第三方内镜危急值消息", Code = "OTTH_ENDOSCOPY_CV", FontColor = "#ffffff", BGColor = "#7c2259" });
    }
    public static class ZFSCMsgStatus_OTTH_HOLTER_CV
    {
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();

            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值发送.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值发送.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值撤回.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值撤回.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值上报.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值上报.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值处理.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值处理.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危机值处理撤销.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危机值处理撤销.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值确认.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值确认.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危机值确认撤销.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危机值确认撤销.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值超时重发.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值超时重发.Value);
            return dic;
        }
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> ZFSCMsgType = new KeyValuePair<string, BaseClassDicEntity>("1942", new BaseClassDicEntity() { Id = "1942", Name = "第三方心电图危急值消息", Code = "OTTH_HOLTER_CV", FontColor = "#ffffff", BGColor = "#7c2259" });
    }
    public static class ZFSCMsgStatus_ZF_LIIP_IM
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 发送成功 = new KeyValuePair<string, BaseClassDicEntity>("1001", new BaseClassDicEntity() { Id = "1001", Name = "发送成功", Code = "ZF_LIIP_IM_Send", FontColor = "#ffffff", BGColor = "#f41600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 发送失败 = new KeyValuePair<string, BaseClassDicEntity>("1002", new BaseClassDicEntity() { Id = "1002", Name = "发送失败", Code = "ZF_LIIP_IM_UnSend", FontColor = "#ffffff", BGColor = "#f41600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 已读 = new KeyValuePair<string, BaseClassDicEntity>("1003", new BaseClassDicEntity() { Id = "1003", Name = "已读", Code = "ZF_LIIP_MSG_Read", FontColor = "#ffffff", BGColor = "#f41600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 未读 = new KeyValuePair<string, BaseClassDicEntity>("1004", new BaseClassDicEntity() { Id = "1004", Name = "未读", Code = "ZF_LIIP_MSG_UnRead", FontColor = "#ffffff", BGColor = "#f41600" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();

            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值发送.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值发送.Value);
            dic.Add(ZFSCMsgStatus_ZF_LAB_START_CV.危急值撤回.Key, ZFSCMsgStatus_ZF_LAB_START_CV.危急值撤回.Value);
            return dic;
        }
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> ZFSCMsgType = new KeyValuePair<string, BaseClassDicEntity>("1942", new BaseClassDicEntity() { Id = "1942", Name = "第三方内镜危急值消息", Code = "OTTH_ENDOSCOPY_CV", FontColor = "#ffffff", BGColor = "#7c2259" });
    }
    #endregion

    /// <summary>
    /// 人员与医疗机构关系类型
    /// </summary>
    public static class ZFHospitalEmpLinkType
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 所属 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "所属", Code = "1", FontColor = "#ffffff", BGColor = "#f41600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 管理 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "管理", Code = "2", FontColor = "#ffffff", BGColor = "#7c2a59" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();

            dic.Add(所属.Key, 所属.Value);
            dic.Add(管理.Key, 管理.Value);
            return dic;
        }
    }
    /// <summary>
    /// 系统文件路径
    /// </summary>
    public static class SystemFilePath
    {
        /// <summary>
        /// 系统消息声音文件地址
        /// </summary>
        public static string SCMsgVodioFilePath = "SCMsgVodioFile";
        /// <summary>
        /// 员工电子签名图片地址
        /// </summary>
        public static string EmpImages = "EmpImages";
        /// <summary>
        /// 导入部门数据文件地址
        /// </summary>
        public static string UPDBFilePath_Dept = "UPDBFile/Dept";

        /// <summary>
        /// 导入员工数据文件地址
        /// </summary>
        public static string UPDBFilePath_Emp = "UPDBFile/Emp";

    }

    public static class SystemParameter_LIIP
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 是否开启HIS接口用户密码验证 = new KeyValuePair<string, BaseClassDicEntity>("99991001", new BaseClassDicEntity() { Id = "99991001", Name = "智方_LIS平台_是否开启HIS接口用户密码验证", Code = "ZF_LIIP_CheckAccountPWDByHIS", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> HIS接口地址 = new KeyValuePair<string, BaseClassDicEntity>("99991002", new BaseClassDicEntity() { Id = "99991002", Name = "智方_LIS平台_HIS接口用户密码验证地址", Code = "ZF_LIIP_CheckAccountPWDByHIS_Url", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 危急值消息处理是否调用接口服务 = new KeyValuePair<string, BaseClassDicEntity>("99991003", new BaseClassDicEntity() { Id = "99991003", Name = "危急值消息处理是否调用接口服务", Code = "ZF_LIIP_Msg_CV_After_Handle", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 消息要求确认时间间隔_分钟 = new KeyValuePair<string, BaseClassDicEntity>("99991004", new BaseClassDicEntity() { Id = "99991004", Name = "消息要求确认时间间隔_分钟", Code = "Msg_CV_ConfirmOutTimes", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 消息要求处理时间间隔_分钟 = new KeyValuePair<string, BaseClassDicEntity>("99991005", new BaseClassDicEntity() { Id = "99991005", Name = "消息要求处理时间间隔_分钟", Code = "Msg_CV_HandleOutTimes", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(是否开启HIS接口用户密码验证.Key, 是否开启HIS接口用户密码验证.Value);
            dic.Add(HIS接口地址.Key, HIS接口地址.Value);
            dic.Add(危急值消息处理是否调用接口服务.Key, 危急值消息处理是否调用接口服务.Value);
            dic.Add(消息要求确认时间间隔_分钟.Key, 消息要求确认时间间隔_分钟.Value);
            dic.Add(消息要求处理时间间隔_分钟.Key, 消息要求处理时间间隔_分钟.Value);
            return dic;
        }
    }

    /// <summary>
    /// S_Type系统类型表
    /// </summary>
    public static class SystemDetailType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 员工身份类型 = new KeyValuePair<string, BaseClassDicEntity>("9001", new BaseClassDicEntity() { Id = "9001", Name = "员工身份类型", Code = "ZF_LIIP_EmpDetailType", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 部分身份类型 = new KeyValuePair<string, BaseClassDicEntity>("9002", new BaseClassDicEntity() { Id = "9002", Name = "部分身份类型", Code = "ZF_LIIP_DeptDetailType", FontColor = "#ffffff", BGColor = "#f4c600" });
        //public static KeyValuePair<string, BaseClassDicEntity> 项目身份类型 = new KeyValuePair<string, BaseClassDicEntity>("9003", new BaseClassDicEntity() { Id = "9003", Name = "项目身份类型", Code = "ZF_LIIP_TestItemDetailType", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(员工身份类型.Key, 员工身份类型.Value);
            dic.Add(部分身份类型.Key, 部分身份类型.Value);
            //dic.Add(危急值消息处理是否调用接口服务.Key, 危急值消息处理是否调用接口服务.Value);
            return dic;
        }
    }

    /// <summary>
    /// S_Type系统类型表
    /// </summary>
    public static class SystemDetailTypeList
    {
        //人员身份可能和人眼角色有点重叠，优先使用人员角色。
        #region 人员身份
        public static KeyValuePair<string, BaseClassDicEntity> 检验人员 = new KeyValuePair<string, BaseClassDicEntity>("90010001", new BaseClassDicEntity() { Id = "90010001", Name = "检验人员", Code = "ZF_LIIP_EmpDetail_Tech", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 录入人员 = new KeyValuePair<string, BaseClassDicEntity>("90010002", new BaseClassDicEntity() { Id = "90010002", Name = "录入人员", Code = "ZF_LIIP_EmpDetail_Input", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 医生 = new KeyValuePair<string, BaseClassDicEntity>("90010003", new BaseClassDicEntity() { Id = "90010003", Name = "医生", Code = "ZF_LIIP_EmpDetail_Doctor", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 护士 = new KeyValuePair<string, BaseClassDicEntity>("90010004", new BaseClassDicEntity() { Id = "90010004", Name = "护士", Code = "ZF_LIIP_EmpDetail_Nurse", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 财务人员 = new KeyValuePair<string, BaseClassDicEntity>("90010005", new BaseClassDicEntity() { Id = "90010005", Name = "财务人员", Code = "ZF_LIIP_EmpDetail_Finance", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 物流人员 = new KeyValuePair<string, BaseClassDicEntity>("90010006", new BaseClassDicEntity() { Id = "90010006", Name = "物流人员", Code = "ZF_LIIP_EmpDetail_Delivery", FontColor = "#ffffff", BGColor = "#f4c600" });
        #endregion        
    }

    public static class DeptSystemType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 检验科 = new KeyValuePair<string, BaseClassDicEntity>("90020001", new BaseClassDicEntity() { Id = "90020001", Name = "检验科", Code = "ZF_LIIP_DeptDetail_Lab", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 检验小组 = new KeyValuePair<string, BaseClassDicEntity>("90020002", new BaseClassDicEntity() { Id = "90020002", Name = "检验小组", Code = "ZF_LIIP_DeptDetail_TestGroup", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 病区 = new KeyValuePair<string, BaseClassDicEntity>("90020003", new BaseClassDicEntity() { Id = "90020003", Name = "病区", Code = "ZF_LIIP_DeptDetail_District", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 病房 = new KeyValuePair<string, BaseClassDicEntity>("90020004", new BaseClassDicEntity() { Id = "90020004", Name = "病房", Code = "ZF_LIIP_DeptDetail_Ward", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 送检单位 = new KeyValuePair<string, BaseClassDicEntity>("90020005", new BaseClassDicEntity() { Id = "90020005", Name = "送检单位", Code = "ZF_LIIP_DeptDetail_SourceOrg", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 接检单位 = new KeyValuePair<string, BaseClassDicEntity>("90020006", new BaseClassDicEntity() { Id = "90020006", Name = "接检单位", Code = "ZF_LIIP_DeptDetail_Org", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 分公司 = new KeyValuePair<string, BaseClassDicEntity>("90020007", new BaseClassDicEntity() { Id = "90020007", Name = "分公司", Code = "ZF_LIIP_DeptDetail_Branch", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 集团 = new KeyValuePair<string, BaseClassDicEntity>("90020008", new BaseClassDicEntity() { Id = "90020008", Name = "集团", Code = "ZF_LIIP_DeptDetail_Corporation", FontColor = "#ffffff", BGColor = "#f4c600" });


        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(检验科.Key, 检验科.Value);
            dic.Add(病区.Key, 病区.Value);
            dic.Add(病房.Key, 病房.Value);
            dic.Add(送检单位.Key, 送检单位.Value);
            dic.Add(接检单位.Key, 接检单位.Value);
            dic.Add(分公司.Key, 分公司.Value);
            dic.Add(集团.Key, 集团.Value);
            return dic;
        }
    }

    public static class ModuleType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 统计系统 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "统计系统", Code = "ZF_SAS", FontColor = "#ffffff", BGColor = "#f4c600" });
       
        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(统计系统.Key, 统计系统.Value);
            return dic;
        }
    }

    public static class ResutlCode
    {
        public static KeyValuePair<string, BaseClassDicEntity> 成功 = new KeyValuePair<string, BaseClassDicEntity>("2000", new BaseClassDicEntity() { Id = "2000", Name = "成功", Code = "ResutlCode_Success", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 失败 = new KeyValuePair<string, BaseClassDicEntity>("5001", new BaseClassDicEntity() { Id = "5001", Name = "失败", Code = "ResutlCode_Fail", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 异常 = new KeyValuePair<string, BaseClassDicEntity>("6001", new BaseClassDicEntity() { Id = "6001", Name = "异常", Code = "ResutlCode_Error", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> Session过期 = new KeyValuePair<string, BaseClassDicEntity>("6101", new BaseClassDicEntity() { Id = "6101", Name = "Session过期", Code = "ResutlCode_SessionExpire", FontColor = "#ffffff", BGColor = "#f4c600" });


        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(成功.Key, 成功.Value);
            dic.Add(失败.Key, 失败.Value);
            dic.Add(异常.Key, 异常.Value);
            dic.Add(Session过期.Key, Session过期.Value);
            return dic;
        }
    }

    public static class WXSourceType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 公众号 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "公众号", Code = "CommonPlat", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 小程序 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "小程序", Code = "Mini", FontColor = "#ffffff", BGColor = "#f4c600" });


        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(公众号.Key, 公众号.Value);
            dic.Add(小程序.Key, 小程序.Value);
            return dic;
        }
    }

    public static class AccountApplySourceType
    {
        public static KeyValuePair<string, BaseClassDicEntity> PC = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "PC", Code = "PC", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 公众号 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "公众号", Code = "WXCommonPlat", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 小程序 = new KeyValuePair<string, BaseClassDicEntity>("3", new BaseClassDicEntity() { Id = "3", Name = "小程序", Code = "WXMini", FontColor = "#ffffff", BGColor = "#f4c600" });


        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(PC.Key, PC.Value);
            dic.Add(公众号.Key, 公众号.Value);
            dic.Add(小程序.Key, 小程序.Value);
            return dic;
        }
    }

    public static class AccountRegisterApprovalType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 申请中 = new KeyValuePair<string, BaseClassDicEntity>("0", new BaseClassDicEntity() { Id = "0", Name = "申请中", Code = "Apply", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 审批通过 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "审批通过", Code = "Finish", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 审批打回 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "审批打回", Code = "Back", FontColor = "#ffffff", BGColor = "#f4c600" });


        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(申请中.Key, 申请中.Value);
            dic.Add(审批通过.Key, 审批通过.Value);
            dic.Add(审批打回.Key, 审批打回.Value);
            return dic;
        }
    }

    public static class ZF_LAB_START_CV_HandleType
    {
        public static KeyValuePair<string, BaseClassDicEntity> 非紧急 = new KeyValuePair<string, BaseClassDicEntity>("1", new BaseClassDicEntity() { Id = "1", Name = "非紧急", Code = "UnUrgent", FontColor = "#ffffff", BGColor = "#f4c600" });
        public static KeyValuePair<string, BaseClassDicEntity> 紧急 = new KeyValuePair<string, BaseClassDicEntity>("2", new BaseClassDicEntity() { Id = "2", Name = "紧急", Code = "Urgent", FontColor = "#ffffff", BGColor = "#f4c600" });


        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(非紧急.Key, 非紧急.Value);
            dic.Add(紧急.Key, 紧急.Value);
            return dic;
        }
    }

    /// <summary>
    /// 珠海项目用 日志记录
    /// </summary>
    public static class ZF_SLog_LogType
    {
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 权限管理 = new KeyValuePair<string, BaseClassDicEntity>("10000001", new BaseClassDicEntity() { Id = "10000001", Name = "权限管理", Code = "", FontColor = "#ffffff", BGColor = "#71ba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 检验仪器 = new KeyValuePair<string, BaseClassDicEntity>("10000002", new BaseClassDicEntity() { Id = "10000002", Name = "检验仪器", Code = "", FontColor = "#ffffff", BGColor = "#71ba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 审核规则 = new KeyValuePair<string, BaseClassDicEntity>("10000003", new BaseClassDicEntity() { Id = "10000003", Name = "审核规则", Code = "", FontColor = "#ffffff", BGColor = "#71ba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 标本种类 = new KeyValuePair<string, BaseClassDicEntity>("10000004", new BaseClassDicEntity() { Id = "10000004", Name = "标本种类", Code = "", FontColor = "#ffffff", BGColor = "#71ba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 数据交换备份 = new KeyValuePair<string, BaseClassDicEntity>("10000006", new BaseClassDicEntity() { Id = "10000006", Name = "数据交换备份", Code = "", FontColor = "#ffffff", BGColor = "#71ba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 数据交换恢复 = new KeyValuePair<string, BaseClassDicEntity>("10000007", new BaseClassDicEntity() { Id = "10000007", Name = "数据交换恢复", Code = "", FontColor = "#ffffff", BGColor = "#71ba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 系统日志 = new KeyValuePair<string, BaseClassDicEntity>("10000008", new BaseClassDicEntity() { Id = "10000008", Name = "系统日志", Code = "", FontColor = "#ffffff", BGColor = "#71ba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 应用程序日志 = new KeyValuePair<string, BaseClassDicEntity>("10000009", new BaseClassDicEntity() { Id = "10000009", Name = "应用程序日志", Code = "", FontColor = "#ffffff", BGColor = "#71ba59" });
        public static System.Collections.Generic.KeyValuePair<string, BaseClassDicEntity> 安全日志 = new KeyValuePair<string, BaseClassDicEntity>("10000010", new BaseClassDicEntity() { Id = "10000010", Name = "安全日志", Code = "", FontColor = "#ffffff", BGColor = "#71ba59" });

        public static Dictionary<string, BaseClassDicEntity> GetStatusDic()
        {
            Dictionary<string, BaseClassDicEntity> dic = new Dictionary<string, BaseClassDicEntity>();
            dic.Add(权限管理.Key, 权限管理.Value);
            dic.Add(检验仪器.Key, 检验仪器.Value);
            dic.Add(审核规则.Key, 审核规则.Value);
            dic.Add(标本种类.Key, 标本种类.Value);
            dic.Add(数据交换备份.Key, 数据交换备份.Value);
            dic.Add(数据交换恢复.Key, 数据交换恢复.Value);
            dic.Add(系统日志.Key, 系统日志.Value);
            dic.Add(应用程序日志.Key, 应用程序日志.Value);
            dic.Add(安全日志.Key, 安全日志.Value);
            return dic;
        }
    }

    //IEQA外部用户所属部门编码：IEQAYH
    #endregion
}
