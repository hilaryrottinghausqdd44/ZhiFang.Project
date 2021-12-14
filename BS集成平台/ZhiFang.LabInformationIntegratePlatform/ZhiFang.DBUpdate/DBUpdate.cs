using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System.Configuration;
using System.Collections.Specialized;
namespace ZhiFang.DBUpdate
{
    public class DBUpdate
    {
        public static string ADOConnectStr = GetADODataBaseSettings(ZhiFang.Common.Public.ConfigHelper.GetDataBaseSettings("databaseSettings", "db.connectionString"));

        static Dictionary<string, string> DicVersion = GetVersionComparison();

        static string MainAssemblyFile = "ZhiFang.LabInformationIntegratePlatform.dll";//可以从配置文件获取

        /// <summary>
        /// 初始化数据库版本和主程序集版本关系，key数据库版本，value主程序集版本
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, string> GetVersionComparison()
        {
            //每更新一次版本，需要手工在这里添加对应关系
            Dictionary<string, string> dicVersion = new Dictionary<string, string>();
            dicVersion.Add("1.0.0.1", "1.0.0.1");
            dicVersion.Add("1.0.0.2", "1.0.0.2");
            dicVersion.Add("1.0.0.3", "1.0.0.3");
            dicVersion.Add("1.0.0.4", "1.0.0.4");
            dicVersion.Add("1.0.0.5", "1.0.0.5");
            dicVersion.Add("1.0.0.6", "1.0.0.6");
            dicVersion.Add("1.0.0.7", "1.0.0.7");
            dicVersion.Add("1.0.0.8", "1.0.0.8");
            dicVersion.Add("1.0.0.9", "1.0.0.9");
            dicVersion.Add("1.0.0.10", "1.0.0.10");
            dicVersion.Add("1.0.0.11", "1.0.0.11");
            dicVersion.Add("1.0.0.12", "1.0.0.12");
            dicVersion.Add("1.0.0.13", "1.0.0.13");
            dicVersion.Add("1.0.0.14", "1.0.0.14");
            dicVersion.Add("1.0.0.15", "1.0.0.15");
            dicVersion.Add("1.0.0.16", "1.0.0.16");
            dicVersion.Add("1.0.0.17", "1.0.0.17");
            dicVersion.Add("1.0.0.18", "1.0.0.18");
            dicVersion.Add("1.0.0.19", "1.0.0.19");
            dicVersion.Add("1.0.0.20", "1.0.0.20");
            dicVersion.Add("1.0.0.21", "1.0.0.21");
            dicVersion.Add("1.0.0.22", "1.0.0.22");

            dicVersion.Add("1.0.1.1", "1.0.1.1"); //添加微信消息系统
            dicVersion.Add("1.0.1.2", "1.0.1.2"); //添加用户申请系统
            dicVersion.Add("1.0.1.3", "1.0.1.3");
            dicVersion.Add("1.0.1.4", "1.0.1.4");
            dicVersion.Add("1.0.1.5", "1.0.1.5");
            dicVersion.Add("1.0.1.6", "1.0.1.6");
            dicVersion.Add("1.0.1.7", "1.0.1.7");
            dicVersion.Add("1.0.1.8", "1.0.1.8");
            dicVersion.Add("1.0.1.9", "1.0.1.9");
            dicVersion.Add("1.0.1.10", "1.0.1.10");
            dicVersion.Add("1.0.1.11", "1.0.1.11");
            dicVersion.Add("1.0.1.12", "1.0.1.12");
            //dicVersion.Add("1.0.1.13", "1.0.1.13");
            return dicVersion;
        }

        /// <summary>
        /// 根据databaseSettings数据库链接配置，得到ADO数据链接字符串
        /// </summary>
        /// <param name="connectString"></param>
        /// <returns></returns>
        private static string GetADODataBaseSettings(string connectString)
        {
            string result = "";
            if (!string.IsNullOrEmpty(connectString))
            {
                string[] strList = connectString.Split(';');
                foreach (string tempStr in strList)
                {
                    if (!string.IsNullOrEmpty(tempStr))
                    {
                        string[] strArray = tempStr.Split('=');
                        if (strArray[0].ToUpper() == "SERVER")
                            result += "data source=" + tempStr.Remove(0, tempStr.IndexOf("=") + 1) + ";";
                        else if (strArray[0].ToUpper() == "DATABASE")
                            result += "initial catalog=" + tempStr.Remove(0, tempStr.IndexOf("=") + 1) + ";";
                        else if (strArray[0].ToUpper() == "UID")
                            result += "user id=" + tempStr.Remove(0, tempStr.IndexOf("=") + 1) + ";";
                        else if (strArray[0].ToUpper() == "PWD")
                            result += "password=" + tempStr.Remove(0, tempStr.IndexOf("=") + 1) + ";";
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 数据库升级
        /// </summary>
        /// <param name="oldVersion"></param>
        /// <returns></returns>
        public static bool DataBaseUpdate(string oldVersion)
        {
            bool result = false;
            string updateSql = "";
            CheckBParameterIsIsExists();
            oldVersion = GetDataBaseCurVersion();
            #region 1.0.0.1
            if (IsUpdateDataBase(oldVersion, "1.0.0.1"))
            {
                List<string> listSQL = new List<string>();
                updateSql = " update B_Parameter set Shortcode=\'" + oldVersion + "\'" + " where ParaType=\'SYS\' and ParaNo=\'SYS_DBVersion\'";
                result = ExecuteUpdateSQL(updateSql);
                result = UpateCompareVersionInfo("1.0.0.1");
            }
            #endregion

            #region 1.0.0.2
            if (IsUpdateDataBase(oldVersion, "1.0.0.2"))
            {
                List<string> listSQL = new List<string>();
                #region 数据表
                updateSql = "CREATE TABLE [dbo].[SC_MsgType](	[LabID] [bigint] NULL,	[MsgTypeID] [bigint] NOT NULL,	[CName] [varchar](100) NULL,	[Code] [varchar](50) NULL,	[EName] [varchar](100) NULL,	[ShortCode] [varchar](20) NULL,	[SystemID] [bigint] NULL,	[SystemCName] [varchar](100) NULL,	[SystemCode] [varchar](50) NULL,	[Url] [varchar](1000) NULL,	[Visible] [bit] NULL,	[Memo] [varchar](500) NULL,	[DispOrder] [int] NULL,	[IsUse] [bit] NULL,	[CreatorID] [dbo].[D_系统主键] NULL,	[CreatorName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_PubMsgType] PRIMARY KEY CLUSTERED (	[MsgTypeID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY];CREATE TABLE [dbo].[SC_MsgPhraseDic](	[LabID] [bigint] NULL,	[MsgPhraseID] [int] NOT NULL,	[Context] [varchar](500) NULL,	[Code] [varchar](20) NULL,	[Visible] [int] NULL,	[MsgTypeID] [bigint] NULL,	[MsgTypeCName] [varchar](100) NULL,	[MsgTypeCode] [varchar](50) NULL,	[Memo] [varchar](500) NULL,	[DispOrder] [int] NULL,	[IsUse] [bit] NULL,	[CreatorID] [dbo].[D_系统主键] NULL,	[CreatorName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_PubMsgValueDesc] PRIMARY KEY CLUSTERED (	[MsgPhraseID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY];CREATE TABLE [dbo].[SC_MsgHandle](	[LabID] [bigint] NULL,	[MsgHandleID] [bigint] NOT NULL,	[MsgTypeID] [bigint] NULL,	[MsgID] [bigint] NOT NULL,	[MsgTypeCName] [varchar](100) NULL,	[MsgTypeCode] [varchar](50) NULL,	[SystemID] [bigint] NULL,	[SystemCName] [varchar](100) NULL,	[SystemCode] [varchar](50) NULL,	[HandleTypeID] [bigint] NULL,	[HandleTypeName] [varchar](100) NULL,	[HandleSysCode] [varchar](100) NULL,	[HandleSysName] [varchar](100) NULL,	[HandleTime] [datetime] NOT NULL,	[HandlerID] [bigint] NOT NULL,	[HandlerName] [varchar](100) NOT NULL,	[HandleDesc] [varchar](5000) NOT NULL,	[HandleDeptID] [bigint] NULL,	[HandleDeptName] [varchar](100) NULL,	[HandleNodeName] [varchar](50) NOT NULL,	[HandleNodeIPAddress] [varchar](20) NOT NULL,	[Memo] [varchar](500) NULL,	[DispOrder] [int] NULL,	[IsUse] [bit] NULL,	[CreatorID] [dbo].[D_系统主键] NULL,	[CreatorName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_PubMsgTrade] PRIMARY KEY CLUSTERED (	[MsgHandleID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY];CREATE TABLE [dbo].[SC_Msg](	[LabID] [bigint] NULL,	[MsgID] [bigint] NOT NULL,	[MsgContent] [varchar](5000) NOT NULL,	[MsgTypeID] [bigint] NOT NULL,	[MsgTypeName] [varchar](100) NULL,	[MsgTypeCode] [varchar](50) NULL,	[SystemID] [bigint] NULL,	[SystemCName] [varchar](100) NULL,	[SystemCode] [varchar](50) NULL,	[MsgLevel] [smallint]  NULL,	[SendNodeName] [varchar](100) NULL,	[SendIPAddress] [varchar](20) NULL,	[SenderID] [bigint] NULL,	[SenderName] [varchar](50) NULL,	[RecNodeName] [varchar](100) NULL,	[RecIPAddress] [varchar](20) NULL,	[RecSectionNo] [bigint] NULL,	[RecSectionName] [varchar](50) NULL,	[RecLabID] [bigint] NULL,	[RecLabName] [varchar](100) NULL,	[RecDeptID] [bigint] NULL,	[RecDeptName] [varchar](100) NULL,	[ReceiverID] [bigint] NULL,	[ReceiverName] [varchar](50) NULL,	[RecSickTypeID] [bigint] NULL,	[RecSickTypeName] [varchar](100) NULL,	[RecDistrictID] [bigint] NULL,	[RecDistrictName] [varchar](100) NULL,	[RecDoctorID] [bigint] NULL,	[RecDoctorName] [varchar](100) NULL,	[RequireReplyTime] [datetime] NULL,	[UnRecSectorTypeID] [bigint] NULL,	[UnRecSectorTypeName] [varchar](100) NULL,	[ReadFlag] [bigint] NULL,	[ConfirmDateTime] [datetime] NULL,	[ConfirmMemo] [varchar](5000) NULL,	[ConfirmFlag] [bigint] NULL,	[RequireConfirmTime] [datetime] NULL,	[ConfirmNotifyDoctorID] [bigint] NULL,	[ConfirmIPAddress] [varchar](20) NULL,	[ConfirmerID] [bigint] NULL,	[ConfirmerName] [varchar](100) NULL,	[RecDeptPhoneCode] [varchar](50) NULL,	[WarningUploaderFlag] [bigint] NULL,	[WarningUploaderID] [bigint] NULL,	[WarningUploaderName] [varchar](100) NULL,	[WarningUpLoadNotifyNurseID] [bigint] NULL,	[WarningUpLoadNotifyNurseName] [varchar](100) NULL,	[WarningUpLoadDateTime] [datetime] NULL,	[LoginReadUserID] [bigint] NULL,	[LoginReadUserName] [varchar](100) NULL,	[LoginReadDateTime] [datetime] NULL,	[SendToMsgCentre] [bigint] NULL,	[HandlingFlag] [bigint] NULL,	[HandlingNodeName] [varchar](200) NULL,	[HandlingDateTime] [datetime] NULL,	[RequireHandleTime] [datetime] NULL,	[HandleFlag] [bigint] NULL,	[FirstHandleDateTime] [datetime] NULL,	[TimeOutCallFlag] [bigint] NULL,	[TimeOutCallUserID] [bigint] NULL,	[TimeOutCallUserName] [varchar](100) NULL,	[TimeOutCallRecUserID] [bigint] NULL,	[TimeOutCallRecUserName] [varchar](100) NULL,	[TimeOutCallDateTime] [datetime] NULL,	[SendToWebService] [bigint] NULL,	[SendToHisFlag] [bigint] NULL,	[SendLinkMsg] [bigint] NULL,	[CreateToCheckTakeTime] [varchar](100) NULL,	[SendToPhone] [bigint] NULL,	[Memo] [varchar](500) NULL,	[DispOrder] [int] NULL,	[RebackerID] [bigint] NULL,	[RebackerName] [varchar](20) NULL,	[RebackTime] [datetime] NULL,	[RebackMemo] [varchar](500) NULL,	[RebackFalg] [bigint] NULL,	[IsUse] [bit] NULL,	[CreatorID] [dbo].[D_系统主键] NULL,	[CreatorName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_PubMsg] PRIMARY KEY CLUSTERED (	[MsgID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY];CREATE TABLE [dbo].[SC_IM_InfomationContent](	[LabID] [bigint] NULL,	[IMICID] [bigint] NOT NULL,	[InfomationContent] [nvarchar](max) NULL,	[SenderID] [bigint] NULL,	[SenderName] [nvarchar](50) NULL,	[SenderNickName] [nvarchar](50) NULL,	[ReceiverID] [bigint] NULL,	[ReceiverName] [nvarchar](50) NULL,	[ReceiverNickName] [nvarchar](50) NULL,	[SendDateTime] [datetime] NULL,	[ReceiveDateTime] [datetime] NULL,	[IMICTypeID] [bigint] NULL,	[IMICTypeName] [nvarchar](50) NULL,	[ReadFlag] [bit] NULL,	[BackFlag] [bit] NULL,	[IsUse] [bit] NULL,	[DataAddTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_SC_IM_INFOMATIONCONTENT] PRIMARY KEY CLUSTERED (	[IMICID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];CREATE TABLE [dbo].[IntergrateSystemSet](	[SystemId] [bigint] NOT NULL,	[SystemName] [nvarchar](100) NULL,	[SystemCode] [nvarchar](50) NULL,	[SystemHost] [nvarchar](500) NULL,	[SystemEntryAddress] [nvarchar](500) NULL,	[PinYinZiTou] [nvarchar](50) NULL,	[Memo] [nvarchar](max) NULL,	[DispOrder] [int] NULL,	[IsUse] [bit] NULL,	[DataAddTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_DISTRIBUTEDSYSTEMSET] PRIMARY KEY CLUSTERED (	[SystemId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];ALTER TABLE [dbo].[SC_Msg] ADD  CONSTRAINT [DF_PubMsg_MsgGrade]  DEFAULT ((1)) FOR [MsgLevel];ALTER TABLE [dbo].[SC_Msg] ADD  CONSTRAINT [DF_PubMsg_ReadFlag]  DEFAULT ((0)) FOR [ReadFlag];ALTER TABLE [dbo].[SC_Msg] ADD  CONSTRAINT [DF_PubMsg_NoticeFlag]  DEFAULT ((0)) FOR [WarningUploaderFlag];ALTER TABLE [dbo].[SC_Msg] ADD  CONSTRAINT [DF_PubMsg_SendToMsgCentre]  DEFAULT ((0)) FOR [SendToMsgCentre];ALTER TABLE [dbo].[SC_Msg] ADD  CONSTRAINT [DF_PubMsg_WorkingFlag]  DEFAULT ((0)) FOR [HandlingFlag];ALTER TABLE [dbo].[SC_Msg] ADD  CONSTRAINT [DF_PubMsg_TradeFlag]  DEFAULT ((0)) FOR [HandleFlag];ALTER TABLE [dbo].[SC_Msg] ADD  CONSTRAINT [DF_PubMsg_TimeOutCallFlag]  DEFAULT ((0)) FOR [TimeOutCallFlag];ALTER TABLE [dbo].[SC_Msg] ADD  CONSTRAINT [DF_PubMsg_SendToWebService]  DEFAULT ((0)) FOR [SendToWebService];ALTER TABLE [dbo].[SC_Msg] ADD  CONSTRAINT [DF_PubMsg_SendToHis]  DEFAULT ((0)) FOR [SendToHisFlag];ALTER TABLE [dbo].[SC_Msg] ADD  CONSTRAINT [DF_PubMsg_SendLinkMsg]  DEFAULT ((0)) FOR [SendLinkMsg];ALTER TABLE [dbo].[SC_Msg] ADD  CONSTRAINT [DF_PubMsg_SendToPhone]  DEFAULT ((0)) FOR [SendToPhone];ALTER TABLE [dbo].[SC_MsgType]  WITH CHECK ADD  CONSTRAINT [FK_SC_MsgType_IntergrateSystemSet] FOREIGN KEY([SystemID])REFERENCES [dbo].[IntergrateSystemSet] ([SystemId]);ALTER TABLE [dbo].[SC_MsgType] CHECK CONSTRAINT [FK_SC_MsgType_IntergrateSystemSet];ALTER TABLE [dbo].[SC_MsgPhraseDic]  WITH CHECK ADD  CONSTRAINT [FK_SC_MSGPH_REFERENCE_SC_MSGTY] FOREIGN KEY([MsgTypeID])REFERENCES [dbo].[SC_MsgType] ([MsgTypeID]);ALTER TABLE [dbo].[SC_MsgPhraseDic] CHECK CONSTRAINT [FK_SC_MSGPH_REFERENCE_SC_MSGTY];ALTER TABLE [dbo].[SC_MsgHandle]  WITH CHECK ADD  CONSTRAINT [FK_SC_MSGHA_REFERENCE_SC_MSG] FOREIGN KEY([MsgID])REFERENCES [dbo].[SC_Msg] ([MsgID]);ALTER TABLE [dbo].[SC_MsgHandle] CHECK CONSTRAINT [FK_SC_MSGHA_REFERENCE_SC_MSG];ALTER TABLE [dbo].[SC_MsgHandle]  WITH CHECK ADD  CONSTRAINT [FK_SC_MSGHA_REFERENCE_SC_MSGTY] FOREIGN KEY([MsgTypeID])REFERENCES [dbo].[SC_MsgType] ([MsgTypeID]);ALTER TABLE [dbo].[SC_MsgHandle] CHECK CONSTRAINT [FK_SC_MSGHA_REFERENCE_SC_MSGTY];ALTER TABLE [dbo].[SC_MsgHandle]  WITH CHECK ADD  CONSTRAINT [FK_SC_MsgHandle_IntergrateSystemSet] FOREIGN KEY([SystemID])REFERENCES [dbo].[IntergrateSystemSet] ([SystemId]);ALTER TABLE [dbo].[SC_MsgHandle] CHECK CONSTRAINT [FK_SC_MsgHandle_IntergrateSystemSet];ALTER TABLE [dbo].[SC_Msg]  WITH CHECK ADD  CONSTRAINT [FK_SC_Msg_IntergrateSystemSet] FOREIGN KEY([SystemID])REFERENCES [dbo].[IntergrateSystemSet] ([SystemId]);ALTER TABLE [dbo].[SC_Msg] CHECK CONSTRAINT [FK_SC_Msg_IntergrateSystemSet];ALTER TABLE [dbo].[SC_Msg]  WITH CHECK ADD  CONSTRAINT [FK_SC_MSG_REFERENCE_SC_MSGTY] FOREIGN KEY([MsgTypeID])REFERENCES [dbo].[SC_MsgType] ([MsgTypeID]);ALTER TABLE [dbo].[SC_Msg] CHECK CONSTRAINT [FK_SC_MSG_REFERENCE_SC_MSGTY]; ";
                listSQL.Add(updateSql);
                #endregion                
                #region 数据表字段备注
                updateSql = "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息类型ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgType', @level2type=N'COLUMN',@level2name=N'MsgTypeID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息类型名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgType', @level2type=N'COLUMN',@level2name=N'CName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息类型代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgType', @level2type=N'COLUMN',@level2name=N'Code';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息类型英文名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgType', @level2type=N'COLUMN',@level2name=N'EName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息类型简码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgType', @level2type=N'COLUMN',@level2name=N'ShortCode';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属系统ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgType', @level2type=N'COLUMN',@level2name=N'SystemID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属系统名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgType', @level2type=N'COLUMN',@level2name=N'SystemCName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属系统代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgType', @level2type=N'COLUMN',@level2name=N'SystemCode';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'展现程序地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgType', @level2type=N'COLUMN',@level2name=N'Url';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否可用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgType', @level2type=N'COLUMN',@level2name=N'Visible';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgType', @level2type=N'COLUMN',@level2name=N'Memo';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示次序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgType', @level2type=N'COLUMN',@level2name=N'DispOrder';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgType', @level2type=N'COLUMN',@level2name=N'IsUse';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgType', @level2type=N'COLUMN',@level2name=N'CreatorID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgType', @level2type=N'COLUMN',@level2name=N'CreatorName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgType', @level2type=N'COLUMN',@level2name=N'DataAddTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgType', @level2type=N'COLUMN',@level2name=N'DataUpdateTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgType', @level2type=N'COLUMN',@level2name=N'DataTimeStamp';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公共消息类型表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgType';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息短语ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgPhraseDic', @level2type=N'COLUMN',@level2name=N'MsgPhraseID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'短语内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgPhraseDic', @level2type=N'COLUMN',@level2name=N'Context';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'短语编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgPhraseDic', @level2type=N'COLUMN',@level2name=N'Code';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否可用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgPhraseDic', @level2type=N'COLUMN',@level2name=N'Visible';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息类型名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgPhraseDic', @level2type=N'COLUMN',@level2name=N'MsgTypeCName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息类型代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgPhraseDic', @level2type=N'COLUMN',@level2name=N'MsgTypeCode';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgPhraseDic', @level2type=N'COLUMN',@level2name=N'Memo';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgPhraseDic', @level2type=N'COLUMN',@level2name=N'IsUse';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgPhraseDic', @level2type=N'COLUMN',@level2name=N'CreatorID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgPhraseDic', @level2type=N'COLUMN',@level2name=N'CreatorName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgPhraseDic', @level2type=N'COLUMN',@level2name=N'DataAddTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgPhraseDic', @level2type=N'COLUMN',@level2name=N'DataUpdateTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgPhraseDic', @level2type=N'COLUMN',@level2name=N'DataTimeStamp';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公共消息短语字典' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgPhraseDic';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息处理ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'MsgHandleID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息类型ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'MsgTypeID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'MsgID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息类型名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'MsgTypeCName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息类型代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'MsgTypeCode';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属系统ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'SystemID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属系统名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'SystemCName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属系统代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'SystemCode';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'处理类型ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'HandleTypeID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'处理类型名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'HandleTypeName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'处理系统编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'HandleSysCode';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'处理系统名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'HandleSysName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'处理时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'HandleTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'处理人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'HandlerID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'处理人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'HandlerName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'处理意见' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'HandleDesc';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'处理科室ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'HandleDeptID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'处理科室名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'HandleDeptName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'处理站点名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'HandleNodeName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'处理站点IP地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'HandleNodeIPAddress';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'Memo';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'IsUse';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'CreatorID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'CreatorName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'DataAddTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'DataUpdateTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle', @level2type=N'COLUMN',@level2name=N'DataTimeStamp';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公共消息处理表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_MsgHandle';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'MsgID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'MsgContent';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息类型ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'MsgTypeID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息类型名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'MsgTypeName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息类型代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'MsgTypeCode';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属系统ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'SystemID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属系统名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'SystemCName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属系统代码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'SystemCode';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息级别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'MsgLevel';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发送站点名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'SendNodeName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发送IP地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'SendIPAddress';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息发送者ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'SenderID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息发送者姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'SenderName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收站点名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'RecNodeName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收IP地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'RecIPAddress';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收小组ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'RecSectionNo';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收小组姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'RecSectionName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收机构ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'RecLabID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收机构名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'RecLabName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收科室ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'RecDeptID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收科室名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'RecDeptName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收者ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'ReceiverID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收者姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'ReceiverName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息接收就诊类型编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'RecSickTypeID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息接收就诊类型名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'RecSickTypeName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息接收病区ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'RecDistrictID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息接收病区名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'RecDistrictName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收消息的医生ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'RecDoctorID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收消息的医生姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'RecDoctorName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'要求回复时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'RequireReplyTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'拒收消息接收实验类型ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'UnRecSectorTypeID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'拒收消息接收实验类型名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'UnRecSectorTypeName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已读标志，0未查阅，1已查阅。  ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'ReadFlag';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息确认时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'ConfirmDateTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息确认备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'ConfirmMemo';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息确认标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'ConfirmFlag';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'要求确认时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'RequireConfirmTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'确认时通知医生ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'ConfirmNotifyDoctorID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息确认IP地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'ConfirmIPAddress';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'确认人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'ConfirmerID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'确认人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'ConfirmerName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收科室电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'RecDeptPhoneCode';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'警告上报标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'WarningUploaderFlag';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'警告上报者ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'WarningUploaderID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'警告上报者姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'WarningUploaderName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'警告上报时通知护士ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'WarningUpLoadNotifyNurseID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'警告上报时通知护士编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'WarningUpLoadNotifyNurseName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'警告上报时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'WarningUpLoadDateTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'查阅人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'LoginReadUserID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'查阅人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'LoginReadUserName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'查阅时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'LoginReadDateTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'处理中标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'HandlingFlag';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'处理中站点名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'HandlingNodeName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'处理时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'HandlingDateTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'要求处理时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'RequireHandleTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'处理标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'HandleFlag';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'第一次处理消息时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'FirstHandleDateTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'超时未处理消息时提示标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'TimeOutCallFlag';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'超时未处理消息时提示人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'TimeOutCallUserID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'超时未处理消息时提示人名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'TimeOutCallUserName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'超时未处理消息时提示接收人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'TimeOutCallRecUserID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'超时未处理消息时提示接收人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'TimeOutCallRecUserName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'超时未处理消息时提示时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'TimeOutCallDateTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发送到His数据库标志   0未发送   1已发送   ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'SendToHisFlag';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发送关联危机值标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'SendLinkMsg';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'产生结果到审核时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'CreateToCheckTakeTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发送到手机标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'SendToPhone';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'Memo';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'撤回人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'RebackerID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'撤回人名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'RebackerName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'撤回时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'RebackTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'撤回原因' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'RebackMemo';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'撤回标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'RebackFalg';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'IsUse';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'CreatorID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'CreatorName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'DataAddTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'DataUpdateTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg', @level2type=N'COLUMN',@level2name=N'DataTimeStamp';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公共消息表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_Msg';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息内容ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_IM_InfomationContent', @level2type=N'COLUMN',@level2name=N'IMICID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_IM_InfomationContent', @level2type=N'COLUMN',@level2name=N'InfomationContent';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发送者ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_IM_InfomationContent', @level2type=N'COLUMN',@level2name=N'SenderID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发送者名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_IM_InfomationContent', @level2type=N'COLUMN',@level2name=N'SenderName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发送者昵称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_IM_InfomationContent', @level2type=N'COLUMN',@level2name=N'SenderNickName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收者ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_IM_InfomationContent', @level2type=N'COLUMN',@level2name=N'ReceiverID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收者名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_IM_InfomationContent', @level2type=N'COLUMN',@level2name=N'ReceiverName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收者昵称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_IM_InfomationContent', @level2type=N'COLUMN',@level2name=N'ReceiverNickName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发送时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_IM_InfomationContent', @level2type=N'COLUMN',@level2name=N'SendDateTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_IM_InfomationContent', @level2type=N'COLUMN',@level2name=N'ReceiveDateTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息类型ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_IM_InfomationContent', @level2type=N'COLUMN',@level2name=N'IMICTypeID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'信息类型名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_IM_InfomationContent', @level2type=N'COLUMN',@level2name=N'IMICTypeName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'阅读标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_IM_InfomationContent', @level2type=N'COLUMN',@level2name=N'ReadFlag';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'撤回标志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_IM_InfomationContent', @level2type=N'COLUMN',@level2name=N'BackFlag';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_IM_InfomationContent', @level2type=N'COLUMN',@level2name=N'IsUse';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_IM_InfomationContent', @level2type=N'COLUMN',@level2name=N'DataAddTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_IM_InfomationContent', @level2type=N'COLUMN',@level2name=N'DataTimeStamp';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'公共即时通讯信息内容表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'SC_IM_InfomationContent';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IntergrateSystemSet', @level2type=N'COLUMN',@level2name=N'SystemId';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IntergrateSystemSet', @level2type=N'COLUMN',@level2name=N'SystemName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IntergrateSystemSet', @level2type=N'COLUMN',@level2name=N'SystemCode';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统主地址：192.168.0.102/ZhiFang.OA   后面不带斜杠。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IntergrateSystemSet', @level2type=N'COLUMN',@level2name=N'SystemHost';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统入口地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IntergrateSystemSet', @level2type=N'COLUMN',@level2name=N'SystemEntryAddress';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'汉字拼音字头' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IntergrateSystemSet', @level2type=N'COLUMN',@level2name=N'PinYinZiTou';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IntergrateSystemSet', @level2type=N'COLUMN',@level2name=N'Memo';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IntergrateSystemSet', @level2type=N'COLUMN',@level2name=N'IsUse';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IntergrateSystemSet', @level2type=N'COLUMN',@level2name=N'DataAddTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IntergrateSystemSet', @level2type=N'COLUMN',@level2name=N'DataTimeStamp';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'分布式系统设置表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'IntergrateSystemSet'; ";
                listSQL.Add(updateSql);
                #endregion                
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.2");
                else
                    ZhiFang.Common.Log.Log.Error("Update Error");
            }
            #endregion

            #region 1.0.0.3
            if (IsUpdateDataBase(oldVersion, "1.0.0.3"))
            {
                List<string> listSQL = new List<string>();
                #region 数据表
                updateSql = "CREATE TABLE [dbo].[CV_CriticalValueEmpIdDeptLink](	[LabID] [bigint] NULL,	[CVEmpIdDeptLinkID] [bigint] NOT NULL,	[EmpID] [dbo].[D_系统主键] NULL,	[EmpName] [varchar](50) NULL,	[DeptID] [bigint] NULL,	[DeptName] [varchar](50) NULL,	[Memo] [varchar](500) NULL,	[DispOrder] [int] NULL,	[IsUse] [bit] NULL,	[CreatorID] [dbo].[D_系统主键] NULL,	[CreatorName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_CV_CRITICALVALUEEMPIDDEPTLI] PRIMARY KEY CLUSTERED (	[CVEmpIdDeptLinkID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] ";
                listSQL.Add(updateSql);
                #endregion
                #region 数据表外键索引
                updateSql = "ALTER TABLE [dbo].[CV_CriticalValueEmpIdDeptLink]  WITH CHECK ADD  CONSTRAINT [FK_CV_CriticalValueEmpIdDeptLink_HR_Dept] FOREIGN KEY([DeptID])REFERENCES [dbo].[HR_Dept] ([DeptID]);ALTER TABLE [dbo].[CV_CriticalValueEmpIdDeptLink] CHECK CONSTRAINT [FK_CV_CriticalValueEmpIdDeptLink_HR_Dept];ALTER TABLE [dbo].[CV_CriticalValueEmpIdDeptLink]  WITH CHECK ADD  CONSTRAINT [FK_CV_CriticalValueEmpIdDeptLink_HR_Employee] FOREIGN KEY([EmpID])REFERENCES [dbo].[HR_Employee] ([EmpID]);ALTER TABLE [dbo].[CV_CriticalValueEmpIdDeptLink] CHECK CONSTRAINT [FK_CV_CriticalValueEmpIdDeptLink_HR_Employee];";
                listSQL.Add(updateSql);
                #endregion             
                #region 数据表字段备注
                updateSql = "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CV_CriticalValueEmpIdDeptLink', @level2type=N'COLUMN',@level2name=N'EmpID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CV_CriticalValueEmpIdDeptLink', @level2type=N'COLUMN',@level2name=N'EmpName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CV_CriticalValueEmpIdDeptLink', @level2type=N'COLUMN',@level2name=N'DeptID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CV_CriticalValueEmpIdDeptLink', @level2type=N'COLUMN',@level2name=N'DeptName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CV_CriticalValueEmpIdDeptLink', @level2type=N'COLUMN',@level2name=N'Memo';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示次序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CV_CriticalValueEmpIdDeptLink', @level2type=N'COLUMN',@level2name=N'DispOrder';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CV_CriticalValueEmpIdDeptLink', @level2type=N'COLUMN',@level2name=N'IsUse';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CV_CriticalValueEmpIdDeptLink', @level2type=N'COLUMN',@level2name=N'CreatorID';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CV_CriticalValueEmpIdDeptLink', @level2type=N'COLUMN',@level2name=N'CreatorName';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CV_CriticalValueEmpIdDeptLink', @level2type=N'COLUMN',@level2name=N'DataAddTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据修改时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CV_CriticalValueEmpIdDeptLink', @level2type=N'COLUMN',@level2name=N'DataUpdateTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CV_CriticalValueEmpIdDeptLink', @level2type=N'COLUMN',@level2name=N'DataTimeStamp';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'危急值员工和部门关系' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'CV_CriticalValueEmpIdDeptLink'; ";
                listSQL.Add(updateSql);
                #endregion                
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.3");
                else
                    ZhiFang.Common.Log.Log.Error("Update Error");
            }
            #endregion

            #region 1.0.0.4
            if (IsUpdateDataBase(oldVersion, "1.0.0.4"))
            {
                List<string> listSQL = new List<string>();
                #region SC_Msg
                updateSql = "IF COL_LENGTH('SC_Msg', 'SendSectionID') IS NULL ALTER TABLE SC_Msg ADD SendSectionID bigint ;   ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'SendSectionName') IS NULL ALTER TABLE SC_Msg ADD SendSectionName varchar(50) ;   ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'RecDeptCode') IS NULL ALTER TABLE SC_Msg ADD RecDeptCode varchar(100) ;   ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'RecDeptCodeHIS') IS NULL ALTER TABLE SC_Msg ADD RecDeptCodeHIS  varchar(100) ;   ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'RecDoctorCodeHIS') IS NULL ALTER TABLE SC_Msg ADD RecDoctorCodeHIS varchar(100)  ;   ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'RecDoctorCode') IS NULL ALTER TABLE SC_Msg ADD RecDoctorCode varchar(100)  ;   ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'WarningUpLoadMemo') IS NULL ALTER TABLE SC_Msg ADD WarningUpLoadMemo varchar(MAX) ;   ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'RecSectionNo') IS NOT NULL EXEC sp_rename   'SC_Msg.RecSectionNo' , 'RecSectionID' ;   ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'WarningUpLoadNotifyNurseCode') IS NULL ALTER TABLE SC_Msg ADD WarningUpLoadNotifyNurseCode varchar(100)  ;   ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'WarningUpLoadNotifyNurseCodeHIS') IS NULL ALTER TABLE SC_Msg ADD WarningUpLoadNotifyNurseCodeHIS varchar(100)  ;   ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'ConfirmerCode') IS NULL ALTER TABLE SC_Msg ADD ConfirmerCode varchar(100)  ;   ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'ConfirmerCodeHIS') IS NULL ALTER TABLE SC_Msg ADD ConfirmerCodeHIS varchar(100)  ;   ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.4");
                else
                    ZhiFang.Common.Log.Log.Error("Update Error");
            }
            #endregion

            #region 1.0.0.5
            if (IsUpdateDataBase(oldVersion, "1.0.0.5"))
            {
                List<string> listSQL = new List<string>();
                #region SC_Msg
                updateSql = "IF COL_LENGTH('SC_Msg', 'ConfirmNotifyDoctorCodeHIS') IS NULL ALTER TABLE SC_Msg ADD ConfirmNotifyDoctorCodeHIS varchar(100) ;   ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'ConfirmNotifyDoctorCode') IS NULL ALTER TABLE SC_Msg ADD ConfirmNotifyDoctorCode varchar(100) ;   ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'ConfirmNotifyDoctorName') IS NULL ALTER TABLE SC_Msg ADD ConfirmNotifyDoctorName varchar(100) ;   ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.5");
                else
                    ZhiFang.Common.Log.Log.Error("Update Error");
            }
            #endregion

            #region 1.0.0.6
            if (IsUpdateDataBase(oldVersion, "1.0.0.6"))
            {
                List<string> listSQL = new List<string>();
                #region HR_EmpIdentity
                updateSql = "IF COL_LENGTH('HR_EmpIdentity', 'TSysCode') IS NULL ALTER TABLE HR_EmpIdentity ADD TSysCode varchar(100) ;   ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('HR_EmpIdentity', 'TSysName') IS NULL ALTER TABLE HR_EmpIdentity ADD TSysName varchar(100) ;   ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('HR_EmpIdentity', 'SystemID') IS NULL ALTER TABLE HR_EmpIdentity ADD SystemID bigint ;   ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('HR_EmpIdentity', 'SystemCode') IS NULL ALTER TABLE HR_EmpIdentity ADD SystemCode varchar(100) ;   ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('HR_EmpIdentity', 'SystemName') IS NULL ALTER TABLE HR_EmpIdentity ADD SystemName varchar(100) ;   ";
                listSQL.Add(updateSql);

                #endregion

                #region HR_DeptIdentity
                updateSql = "IF COL_LENGTH('HR_DeptIdentity', 'TSysCode') IS NULL ALTER TABLE HR_DeptIdentity ADD TSysCode varchar(100) ;   ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('HR_DeptIdentity', 'TSysName') IS NULL ALTER TABLE HR_DeptIdentity ADD TSysName varchar(100) ;   ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('HR_DeptIdentity', 'SystemID') IS NULL ALTER TABLE HR_DeptIdentity ADD SystemID bigint ;   ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('HR_DeptIdentity', 'SystemCode') IS NULL ALTER TABLE HR_DeptIdentity ADD SystemCode varchar(100) ;   ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('HR_DeptIdentity', 'SystemName') IS NULL ALTER TABLE HR_DeptIdentity ADD SystemName varchar(100) ;   ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.6");
                else
                    ZhiFang.Common.Log.Log.Error("Update Error");
            }
            #endregion

            #region 1.0.0.7
            if (IsUpdateDataBase(oldVersion, "1.0.0.7"))
            {
                List<string> listSQL = new List<string>();
                #region RBAC_ModuleOper
                updateSql = "IF COL_LENGTH('RBAC_ModuleOper', 'ServiceURLEName') IS NULL ALTER TABLE RBAC_ModuleOper ADD ServiceURLEName varchar(200) ;   ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('RBAC_ModuleOper', 'RowFilterBaseCName') IS NULL ALTER TABLE RBAC_ModuleOper ADD RowFilterBaseCName varchar(100) ;   ";
                listSQL.Add(updateSql);

                #endregion                

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.7");
                else
                    ZhiFang.Common.Log.Log.Error("Update Error");
            }
            #endregion

            #region 1.0.0.8
            if (IsUpdateDataBase(oldVersion, "1.0.0.8"))
            {
                List<string> listSQL = new List<string>();
                #region SCMsg
                updateSql = "IF COL_LENGTH('SC_Msg', 'SendDeptID') IS NULL ALTER TABLE SC_Msg ADD SendDeptID bigint ;   ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'SendDeptName') IS NULL ALTER TABLE SC_Msg ADD SendDeptName varchar(100) ;   ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'SendDeptCode') IS NULL ALTER TABLE SC_Msg ADD SendDeptCode varchar(200) ;   ";
                listSQL.Add(updateSql);

                #endregion
                #region SCMsg
                updateSql = "IF COL_LENGTH('RBAC_RoleRight', 'PreconditionsId') IS NULL ALTER TABLE RBAC_RoleRight ADD PreconditionsId bigint ;   ";
                listSQL.Add(updateSql);
                #endregion


                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.8");
                else
                    ZhiFang.Common.Log.Log.Error("Update Error");
            }
            #endregion

            #region 1.0.0.9
            if (IsUpdateDataBase(oldVersion, "1.0.0.9"))
            {
                List<string> listSQL = new List<string>();
                #region 删除医院旧表
                updateSql = "IF OBJECT_ID (N'dbo.B_HospitalDept', N'U') IS NOT NULL DROP TABLE dbo.B_HospitalDept;  ";
                listSQL.Add(updateSql);
                updateSql = "IF OBJECT_ID (N'dbo.B_Hospital', N'U') IS NOT NULL DROP TABLE dbo.B_Hospital;  ";
                listSQL.Add(updateSql);
                updateSql = "IF OBJECT_ID (N'dbo.B_HospitalArea', N'U') IS NOT NULL DROP TABLE dbo.B_HospitalArea;  ";
                listSQL.Add(updateSql);
                updateSql = "IF OBJECT_ID (N'dbo.B_HospitalLevel', N'U') IS NOT NULL DROP TABLE dbo.B_HospitalLevel;  ";
                listSQL.Add(updateSql);
                updateSql = "IF OBJECT_ID (N'dbo.B_HospitalType', N'U') IS NOT NULL DROP TABLE dbo.B_HospitalType;  ";
                listSQL.Add(updateSql);
                #endregion

                #region B_Hospital
                updateSql = "CREATE TABLE[dbo].[B_Hospital] (    [LabID][bigint] NULL,  [AreaID][bigint] NULL, [HospitalID][bigint] NOT NULL,   [Name] [nvarchar] (30) NULL,	[EName] [nvarchar] (50) NULL,	[SName] [nvarchar] (30) NULL,	[Shortcode] [nvarchar] (50) NULL,	[PinYinZiTou] [varchar] (50) NULL,	[Comment] [nvarchar] (300) NULL,	[IsUse] [bit] NULL,	[DataAddTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL,	[DataUpdateTime] [datetime] NULL,	[HospitalCode] [nvarchar] (20) NOT NULL, [LevelID] [bigint] NULL,	[HTypeID] [bigint] NULL,	[IconsID] [bigint] NULL,	[ProvinceID] [bigint] NULL,	[CityID] [bigint] NULL,	[LevelName] [nvarchar] (20) NULL,	[HTypeName] [nvarchar] (20) NULL,	[Postion] [nvarchar] (20) NULL,	[ProvinceName] [nvarchar] (20) NULL,	[CityName] [nvarchar] (20) NULL,	[IsBloodSamplingPoint] [bit] NULL,	[DealerID] [bigint] NULL,	[LinkMan] [varchar] (50) NULL,	[LinkManPosition] [varchar] (50) NULL,	[PhoneNum1] [varchar] (50) NULL,	[PhoneNum2] [varchar] (50) NULL,	[Address] [varchar] (50) NULL,	[EMAIL] [varchar] (50) NULL,	[MAILNO] [varchar] (50) NULL,	[FaxNo] [varchar] (50) NULL,	[AutoFax] [varchar] (50) NULL, CONSTRAINT[PK_B_HOSPITAL] PRIMARY KEY CLUSTERED([HospitalID] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY]";
                listSQL.Add(updateSql);
                #endregion

                #region B_HospitalArea
                updateSql = "CREATE TABLE [dbo].[B_HospitalArea](	[LabID] [bigint] NULL,	[HospitalArealID] [dbo].[D_系统主键] NOT NULL,	[PHospitalArealID] [bigint] NULL,	[PHospitalArealName] [nvarchar](50) NULL,	[PHospitalAreaCode] [nvarchar](50) NULL,	[CenterHospitalID] [bigint] NULL,	[CenterHospitalName] [nvarchar](50) NULL,	[AreaTypeID] [bigint] NULL,	[AreaTypeName] [nvarchar](30) NULL,	[Name] [nvarchar](30) NULL,	[Code] [nvarchar](10) NULL,	[SName] [nvarchar](30) NULL,	[Shortcode] [nvarchar](50) NULL,	[PinYinZiTou] [nvarchar](50) NULL,	[DispOrder] [int] NULL,	[Comment] [nvarchar](300) NULL,	[IsUse] [bit] NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_B_HOSPITALAREA] PRIMARY KEY CLUSTERED (	[HospitalArealID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
                listSQL.Add(updateSql);
                #endregion

                #region B_HospitalDept
                updateSql = "CREATE TABLE [dbo].[B_HospitalDept](	[LabID] [bigint] NOT NULL,	[HospitalDeptID] [bigint] NOT NULL,	[HospitalID] [bigint] NULL,	[HospitalName] [varchar](50) NULL,	[Name] [varchar](40) NULL,	[SName] [varchar](40) NULL,	[Shortcode] [nvarchar](50) NULL,	[PinYinZiTou] [nvarchar](50) NULL,	[Comment] [ntext] NULL,	[IsUse] [bit] NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_B_HOSPITALDEPT] PRIMARY KEY CLUSTERED (	[HospitalDeptID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]";
                listSQL.Add(updateSql);
                #endregion

                #region B_HospitalLevel
                updateSql = "CREATE TABLE [dbo].[B_HospitalLevel](	[LabID] [bigint] NULL,	[LevelID] [dbo].[D_系统主键] NOT NULL,	[Name] [nvarchar](30) NULL,	[Code] [nvarchar](10) NULL,	[SName] [nvarchar](30) NULL,	[Shortcode] [dbo].[D_快捷码] NULL,	[PinYinZiTou] [dbo].[D_汉语拼音字头] NULL,	[Comment] [nvarchar](300) NULL,	[IsUse] [bit] NULL,	[DataAddTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL,	[DataUpdateTime] [datetime] NULL, CONSTRAINT [PK_B_HOSPITALLEVEL] PRIMARY KEY CLUSTERED (	[LevelID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
                listSQL.Add(updateSql);
                #endregion

                #region B_HospitalType
                updateSql = "CREATE TABLE [dbo].[B_HospitalType](	[LabID] [bigint] NULL,	[HTypeID] [dbo].[D_系统主键] NOT NULL,	[Name] [nvarchar](30) NULL,	[Code] [nvarchar](10) NULL,	[SName] [nvarchar](30) NULL,	[Shortcode] [dbo].[D_快捷码] NULL,	[PinYinZiTou] [dbo].[D_汉语拼音字头] NULL,	[Comment] [nvarchar](300) NULL,	[IsUse] [bit] NULL,	[DataAddTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL,	[DataUpdateTime] [datetime] NULL, CONSTRAINT [PK_B_HOSPITALTYPE] PRIMARY KEY CLUSTERED (	[HTypeID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
                listSQL.Add(updateSql);
                #endregion

                #region 添加外键
                updateSql = "ALTER TABLE [dbo].[B_Hospital]  WITH CHECK ADD  CONSTRAINT [FK_B_HOSPIT_REFERENCE_B_HOSPIT] FOREIGN KEY([LevelID])REFERENCES [dbo].[B_HospitalLevel] ([LevelID]);ALTER TABLE [dbo].[B_Hospital] CHECK CONSTRAINT [FK_B_HOSPIT_REFERENCE_B_HOSPIT];ALTER TABLE [dbo].[B_Hospital]  WITH CHECK ADD  CONSTRAINT [FK_B_HOSPIT_REFERENCE_B_HospitalType] FOREIGN KEY([HTypeID])REFERENCES [dbo].[B_HospitalType] ([HTypeID]);ALTER TABLE [dbo].[B_Hospital] CHECK CONSTRAINT [FK_B_HOSPIT_REFERENCE_B_HospitalType];ALTER TABLE [dbo].[B_Hospital]  WITH CHECK ADD  CONSTRAINT [FK_B_Hospital_B_DealerShip_DealerID] FOREIGN KEY([DealerID])REFERENCES [dbo].[B_DealerShip] ([DealerID]);ALTER TABLE [dbo].[B_Hospital] CHECK CONSTRAINT [FK_B_Hospital_B_DealerShip_DealerID];ALTER TABLE [dbo].[B_Hospital]  WITH CHECK ADD  CONSTRAINT [FK_B_Hospital_B_HospitalArea_HospitalArealID] FOREIGN KEY([AreaID])REFERENCES [dbo].[B_HospitalArea] ([HospitalArealID]);ALTER TABLE [dbo].[B_Hospital] CHECK CONSTRAINT [FK_B_Hospital_B_HospitalArea_HospitalArealID];ALTER TABLE [dbo].[B_HospitalArea]  WITH CHECK ADD  CONSTRAINT [FK_B_HospitalArea_B_Hospital] FOREIGN KEY([CenterHospitalID])REFERENCES [dbo].[B_Hospital] ([HospitalID]);ALTER TABLE [dbo].[B_HospitalArea] CHECK CONSTRAINT [FK_B_HospitalArea_B_Hospital];ALTER TABLE [dbo].[B_HospitalDept]  WITH CHECK ADD  CONSTRAINT [FK_B_HospitalDept_B_Hospital] FOREIGN KEY([HospitalID])REFERENCES [dbo].[B_Hospital] ([HospitalID]);ALTER TABLE [dbo].[B_HospitalDept] CHECK CONSTRAINT [FK_B_HospitalDept_B_Hospital];";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.9");
                else
                    ZhiFang.Common.Log.Log.Error("Update Error");
            }
            #endregion

            #region 1.0.0.10
            if (IsUpdateDataBase(oldVersion, "1.0.0.10"))
            {
                List<string> listSQL = new List<string>();

                #region B_HospitalTypeLink
                updateSql = "CREATE TABLE [dbo].[B_HospitalTypeLink](	[LabID] [dbo].[D_实验室ID] NOT NULL,	[HospitalTypeLinkID] [bigint] NOT NULL,	[HospitalID] [dbo].[D_系统主键] NULL,	[HospitalName] [varchar](50) NULL,	[HTypeID] [dbo].[D_系统主键] NULL,	[HTypeName] [nvarchar](20) NULL,	[IsUse] [bit] NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_B_HOSPITALTYPELINK] PRIMARY KEY CLUSTERED (	[HospitalTypeLinkID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
                listSQL.Add(updateSql);
                #endregion

                #region B_HospitalEmpLink
                updateSql = "CREATE TABLE [dbo].[B_HospitalEmpLink](	[LabID] [dbo].[D_实验室ID] NOT NULL,	[HospitalTypeLinkID] [bigint] NOT NULL,	[HospitalID] [dbo].[D_系统主键] NULL,	[HospitalName] [varchar](50) NULL,	[EmpID] [dbo].[D_系统主键] NULL,	[EmpName] [varchar](50) NULL,	[LinkTypeID] [bigint] NULL,	[LinkTypeName] [varchar](50) NULL,	[IsUse] [bit] NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_B_HOSPITALEMPLINK] PRIMARY KEY CLUSTERED (	[HospitalTypeLinkID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
                listSQL.Add(updateSql);
                #endregion

                #region B_HospitalArea添加区域级别名称
                updateSql = "IF COL_LENGTH('B_HospitalArea', 'HospitalArealLevelName') IS NULL ALTER TABLE B_HospitalArea ADD HospitalArealLevelName varchar(500);";
                listSQL.Add(updateSql);
                #endregion

                #region 外键关系
                updateSql = "ALTER TABLE [dbo].[B_HospitalTypeLink]  WITH CHECK ADD  CONSTRAINT [FK_B_HospitalTypeLink_B_Hospital] FOREIGN KEY([HospitalID])REFERENCES [dbo].[B_Hospital] ([HospitalID]);ALTER TABLE [dbo].[B_HospitalTypeLink] CHECK CONSTRAINT [FK_B_HospitalTypeLink_B_Hospital];ALTER TABLE [dbo].[B_HospitalTypeLink]  WITH CHECK ADD  CONSTRAINT [FK_B_HospitalTypeLink_B_HospitalType] FOREIGN KEY([HTypeID])REFERENCES [dbo].[B_HospitalType] ([HTypeID]);ALTER TABLE [dbo].[B_HospitalTypeLink] CHECK CONSTRAINT [FK_B_HospitalTypeLink_B_HospitalType];ALTER TABLE [dbo].[B_HospitalEmpLink]  WITH CHECK ADD  CONSTRAINT [FK_B_HospitalEmpLink_B_Hospital] FOREIGN KEY([HospitalID])REFERENCES [dbo].[B_Hospital] ([HospitalID]);ALTER TABLE [dbo].[B_HospitalEmpLink] CHECK CONSTRAINT [FK_B_HospitalEmpLink_B_Hospital];ALTER TABLE [dbo].[B_HospitalEmpLink]  WITH CHECK ADD  CONSTRAINT [FK_B_HospitalEmpLink_HR_Employee] FOREIGN KEY([EmpID])REFERENCES [dbo].[HR_Employee] ([EmpID]);ALTER TABLE [dbo].[B_HospitalEmpLink] CHECK CONSTRAINT [FK_B_HospitalEmpLink_HR_Employee]";
                listSQL.Add(updateSql);
                #endregion

                #region 去掉B_Hospital去掉标识列属性
                updateSql = "BEGIN TRANSACTION;SET QUOTED_IDENTIFIER ON;SET ARITHABORT ON;SET NUMERIC_ROUNDABORT OFF;SET CONCAT_NULL_YIELDS_NULL ON;SET ANSI_NULLS ON;SET ANSI_PADDING ON;SET ANSI_WARNINGS ON;COMMIT;BEGIN TRANSACTION;ALTER TABLE dbo.B_Hospital	DROP CONSTRAINT FK_B_Hospital_B_DealerShip_DealerID;ALTER TABLE dbo.B_DealerShip SET (LOCK_ESCALATION = TABLE);COMMIT;BEGIN TRANSACTION;ALTER TABLE dbo.B_Hospital	DROP CONSTRAINT FK_B_HOSPIT_REFERENCE_B_HospitalType;ALTER TABLE dbo.B_HospitalType SET (LOCK_ESCALATION = TABLE);COMMIT;BEGIN TRANSACTION;ALTER TABLE dbo.B_Hospital	DROP CONSTRAINT FK_B_HOSPIT_REFERENCE_B_HOSPIT;ALTER TABLE dbo.B_HospitalLevel SET (LOCK_ESCALATION = TABLE);COMMIT;BEGIN TRANSACTION;CREATE TABLE dbo.Tmp_B_Hospital	(	LabID bigint NULL,	AreaID bigint NULL,	HospitalID bigint NOT NULL,	Name nvarchar(30) NULL,	EName nvarchar(50) NULL,	SName nvarchar(30) NULL,	Shortcode nvarchar(50) NULL,	PinYinZiTou varchar(50) NULL,	Comment nvarchar(300) NULL,	IsUse bit NULL,	DataAddTime datetime NULL,	DataTimeStamp timestamp NULL,	DataUpdateTime datetime NULL,	HospitalCode nvarchar(20) NOT NULL,	LevelID bigint NULL,	HTypeID bigint NULL,	IconsID bigint NULL,	ProvinceID bigint NULL,	CityID bigint NULL,	LevelName nvarchar(20) NULL,	HTypeName nvarchar(20) NULL,	Postion nvarchar(20) NULL,	ProvinceName nvarchar(20) NULL,	CityName nvarchar(20) NULL,	IsBloodSamplingPoint bit NULL,	DealerID bigint NULL,	LinkMan varchar(50) NULL,	LinkManPosition varchar(50) NULL,	PhoneNum1 varchar(50) NULL,	PhoneNum2 varchar(50) NULL,	Address varchar(50) NULL,	EMAIL varchar(50) NULL,	MAILNO varchar(50) NULL,	FaxNo varchar(50) NULL,	AutoFax varchar(50) NULL	)  ON [PRIMARY];ALTER TABLE dbo.Tmp_B_Hospital SET (LOCK_ESCALATION = TABLE);IF EXISTS(SELECT * FROM dbo.B_Hospital)	 EXEC('INSERT INTO dbo.Tmp_B_Hospital (LabID, AreaID, HospitalID, Name, EName, SName, Shortcode, PinYinZiTou, Comment, IsUse, DataAddTime, DataUpdateTime, HospitalCode, LevelID, HTypeID, IconsID, ProvinceID, CityID, LevelName, HTypeName, Postion, ProvinceName, CityName, IsBloodSamplingPoint, DealerID, LinkMan, LinkManPosition, PhoneNum1, PhoneNum2, Address, EMAIL, MAILNO, FaxNo, AutoFax)		SELECT LabID, AreaID, HospitalID, Name, EName, SName, Shortcode, PinYinZiTou, Comment, IsUse, DataAddTime, DataUpdateTime, HospitalCode, LevelID, HTypeID, IconsID, ProvinceID, CityID, LevelName, HTypeName, Postion, ProvinceName, CityName, IsBloodSamplingPoint, DealerID, LinkMan, LinkManPosition, PhoneNum1, PhoneNum2, Address, EMAIL, MAILNO, FaxNo, AutoFax FROM dbo.B_Hospital WITH (HOLDLOCK TABLOCKX)');ALTER TABLE dbo.B_HospitalEmpLink	DROP CONSTRAINT FK_B_HospitalEmpLink_B_Hospital;ALTER TABLE dbo.B_HospitalTypeLink	DROP CONSTRAINT FK_B_HospitalTypeLink_B_Hospital;ALTER TABLE dbo.B_HospitalDept	DROP CONSTRAINT FK_B_HospitalDept_B_Hospital;ALTER TABLE dbo.B_Hospital	DROP CONSTRAINT FK_B_Hospital_B_HospitalArea_HospitalArealID;ALTER TABLE dbo.B_HospitalArea	DROP CONSTRAINT FK_B_HospitalArea_B_Hospital;DROP TABLE dbo.B_Hospital;EXECUTE sp_rename N'dbo.Tmp_B_Hospital', N'B_Hospital', 'OBJECT' ;ALTER TABLE dbo.B_Hospital ADD CONSTRAINT	PK_B_HOSPITAL PRIMARY KEY CLUSTERED 	(	HospitalID	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];ALTER TABLE dbo.B_Hospital ADD CONSTRAINT	FK_B_HOSPIT_REFERENCE_B_HOSPIT FOREIGN KEY	(	LevelID	) REFERENCES dbo.B_HospitalLevel	(	LevelID	) ON UPDATE  NO ACTION 	 ON DELETE  NO ACTION ;ALTER TABLE dbo.B_Hospital ADD CONSTRAINT	FK_B_HOSPIT_REFERENCE_B_HospitalType FOREIGN KEY	(	HTypeID	) REFERENCES dbo.B_HospitalType	(	HTypeID	) ON UPDATE  NO ACTION 	 ON DELETE  NO ACTION ;ALTER TABLE dbo.B_Hospital ADD CONSTRAINT	FK_B_Hospital_B_DealerShip_DealerID FOREIGN KEY	(	DealerID	) REFERENCES dbo.B_DealerShip	(	DealerID	) ON UPDATE  NO ACTION 	 ON DELETE  NO ACTION ;COMMIT;BEGIN TRANSACTION;ALTER TABLE dbo.B_Hospital ADD CONSTRAINT	FK_B_Hospital_B_HospitalArea_HospitalArealID FOREIGN KEY	(	AreaID	) REFERENCES dbo.B_HospitalArea	(	HospitalArealID	) ON UPDATE  NO ACTION 	 ON DELETE  NO ACTION ;ALTER TABLE dbo.B_HospitalArea ADD CONSTRAINT	FK_B_HospitalArea_B_Hospital FOREIGN KEY	(	CenterHospitalID	) REFERENCES dbo.B_Hospital	(	HospitalID	) ON UPDATE  NO ACTION 	 ON DELETE  NO ACTION ;ALTER TABLE dbo.B_HospitalArea SET (LOCK_ESCALATION = TABLE);COMMIT;BEGIN TRANSACTION;ALTER TABLE dbo.B_HospitalDept ADD CONSTRAINT	FK_B_HospitalDept_B_Hospital FOREIGN KEY(	HospitalID	) REFERENCES dbo.B_Hospital	(	HospitalID	) ON UPDATE  NO ACTION 	 ON DELETE  NO ACTION ;ALTER TABLE dbo.B_HospitalDept SET (LOCK_ESCALATION = TABLE);COMMIT;BEGIN TRANSACTION;ALTER TABLE dbo.B_HospitalTypeLink ADD CONSTRAINT	FK_B_HospitalTypeLink_B_Hospital FOREIGN KEY	(	HospitalID	) REFERENCES dbo.B_Hospital	(	HospitalID	) ON UPDATE  NO ACTION 	 ON DELETE  NO ACTION ;ALTER TABLE dbo.B_HospitalTypeLink SET (LOCK_ESCALATION = TABLE);COMMIT;BEGIN TRANSACTION;ALTER TABLE dbo.B_HospitalEmpLink ADD CONSTRAINT	FK_B_HospitalEmpLink_B_Hospital FOREIGN KEY	(	HospitalID	) REFERENCES dbo.B_Hospital	(	HospitalID	) ON UPDATE  NO ACTION 	 ON DELETE  NO ACTION 	;ALTER TABLE dbo.B_HospitalEmpLink SET (LOCK_ESCALATION = TABLE);COMMIT;";
                listSQL.Add(updateSql);
                #endregion

                #region SC_Msg添加字段
                updateSql = "IF COL_LENGTH('SC_Msg', 'CenterBarCode') IS NULL ALTER TABLE SC_Msg ADD CenterBarCode varchar(20);";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'LabBarCode') IS NULL ALTER TABLE SC_Msg ADD LabBarCode varchar(20);";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'RecLabCode') IS NULL ALTER TABLE SC_Msg ADD RecLabCode varchar(20);";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.10");
                else
                    ZhiFang.Common.Log.Log.Error("Update Error");
            }
            #endregion

            #region 1.0.0.11
            if (IsUpdateDataBase(oldVersion, "1.0.0.11"))
            {
                List<string> listSQL = new List<string>();

                #region SC_Msg添加字段
                updateSql = "IF COL_LENGTH('SC_Msg', 'CenterBarCode') IS NULL ALTER TABLE SC_Msg ADD CenterBarCode varchar(20);";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'LabBarCode') IS NULL ALTER TABLE SC_Msg ADD LabBarCode varchar(20);";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'RecLabCode') IS NULL ALTER TABLE SC_Msg ADD RecLabCode varchar(20);";
                listSQL.Add(updateSql);
                #endregion

                #region B_Hospital删除字段
                updateSql = "IF COL_LENGTH('B_Hospital', 'CenterBarCode') IS not NULL ALTER TABLE B_Hospital drop column CenterBarCode;";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('B_Hospital', 'LabBarCode') IS not NULL ALTER TABLE B_Hospital drop column LabBarCode;";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('B_Hospital', 'RecLabCode') IS not NULL ALTER TABLE B_Hospital drop column RecLabCode;";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.11");
                else
                    ZhiFang.Common.Log.Log.Error("Update Error");
            }
            #endregion

            #region 1.0.0.12
            if (IsUpdateDataBase(oldVersion, "1.0.0.12"))
            {
                List<string> listSQL = new List<string>();

                #region B_HospitalEmpLink医院人员关系添加字段
                updateSql = "IF COL_LENGTH('B_HospitalEmpLink', 'HospitalCode') IS NULL ALTER TABLE B_HospitalEmpLink ADD HospitalCode varchar(50);";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.12");
                else
                    ZhiFang.Common.Log.Log.Error("Update Error");
            }
            #endregion

            #region 1.0.0.13
            if (IsUpdateDataBase(oldVersion, "1.0.0.13"))
            {
                List<string> listSQL = new List<string>();

                #region S_LIIPSystemRBACCloneLog_平台系统组织机构及权限克隆记录
                updateSql = "CREATE TABLE [dbo].[S_LIIPSystemRBACCloneLog](	[RBACCloneLogId] [bigint] NOT NULL,	[SystemId] [bigint] NULL,	[SystemName] [nvarchar](100) NULL,	[SystemCode] [nvarchar](50) NULL,	[ForwardFlag] [bit] NULL,	[DataJson] [nvarchar](max) NULL,	[DataName] [nvarchar](50) NULL,	[OperId] [bigint] NULL,	[OperName] [nvarchar](50) NULL,	[DataCount] [bigint] NULL,	[DataVersion] [nvarchar](50) NULL,	[Memo] [nvarchar](max) NULL,	[DataAddTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_S_LIIPSYSTEMRBACCLONELOG] PRIMARY KEY CLUSTERED (	[RBACCloneLogId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.13");
                else
                    ZhiFang.Common.Log.Log.Error("Update Error");
            }
            #endregion

            #region 1.0.0.14
            if (IsUpdateDataBase(oldVersion, "1.0.0.14"))
            {
                List<string> listSQL = new List<string>();

                #region S_LIIPSystemRBACCloneLog_平台系统组织机构及权限克隆记录
                updateSql = "CREATE UNIQUE NONCLUSTERED INDEX IX_SC_MsgType ON dbo.SC_MsgType	(	Code	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];ALTER TABLE dbo.SC_MsgType SET (LOCK_ESCALATION = TABLE); ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.14");
                else
                    ZhiFang.Common.Log.Log.Error("Update Error");
            }
            #endregion

            #region 1.0.0.15
            if (IsUpdateDataBase(oldVersion, "1.0.0.15"))
            {
                List<string> listSQL = new List<string>();

                #region 新增区县表
                updateSql = "CREATE TABLE [dbo].[B_County](    [LabID][bigint] NOT NULL,   [CountyID] [bigint]        NOT NULL,   [CityID] [bigint]        NOT NULL,   [Name] [varchar] (40) NULL,	[SName] [varchar] (40) NULL,	[Shortcode] [varchar] (40) NULL,	[PinYinZiTou] [varchar] (50) NULL,	[Comment] [ntext] NULL,	[IsUse] [bit] NULL,	[DataAddTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT[PK_B_County] PRIMARY KEY CLUSTERED(   [CountyID] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                #region 新增区县表外键
                updateSql = "ALTER TABLE [dbo].[B_County]  WITH CHECK ADD  CONSTRAINT [FK_B_County_B_City] FOREIGN KEY([CityID])REFERENCES[dbo].[B_City]([CityID]); ALTER TABLE [dbo].[B_County] CHECK CONSTRAINT [FK_B_County_B_City];";
                listSQL.Add(updateSql);
                #endregion

                #region 医院加区县字段
                updateSql = "IF COL_LENGTH('B_Hospital', 'CountyName') IS NULL ALTER TABLE B_Hospital ADD CountyName varchar(20); ";
                listSQL.Add(updateSql);
                updateSql = "IF COL_LENGTH('B_Hospital', 'CountyID') IS NULL ALTER TABLE B_Hospital ADD CountyID bigint; ";
                listSQL.Add(updateSql);
                updateSql = "IF COL_LENGTH('B_Hospital', 'IsReceiveSamplePoint') IS NULL ALTER TABLE B_Hospital ADD IsReceiveSamplePoint bit; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.15");
                else
                    ZhiFang.Common.Log.Log.Error("Update Error");
            }
            #endregion

            #region 1.0.0.16
            if (IsUpdateDataBase(oldVersion, "1.0.0.16"))
            {
                List<string> listSQL = new List<string>();

                #region 医院区域加部门字段
                updateSql = "IF COL_LENGTH('B_HospitalArea', 'DeptID') IS NULL ALTER TABLE B_HospitalArea ADD DeptID bigint; ";
                listSQL.Add(updateSql);
                updateSql = "IF COL_LENGTH('B_HospitalArea', 'DeptName') IS NULL ALTER TABLE B_HospitalArea ADD DeptName varchar(50); ";
                listSQL.Add(updateSql);
                #endregion

                #region 新增医院区域表外键
                updateSql = "ALTER TABLE [dbo].[B_HospitalArea]  WITH CHECK ADD  CONSTRAINT [FK_B_HospitalArea_B_HospitalDept] FOREIGN KEY([DeptID])REFERENCES[dbo].[B_HospitalDept]([HospitalDeptID]); ALTER TABLE [dbo].[B_HospitalArea] CHECK CONSTRAINT [FK_B_HospitalArea_B_HospitalDept];";
                listSQL.Add(updateSql);
                #endregion                

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.16");
                else
                    ZhiFang.Common.Log.Log.Error("Update Error");
            }
            #endregion

            #region 1.0.0.17
            if (IsUpdateDataBase(oldVersion, "1.0.0.17"))
            {
                List<string> listSQL = new List<string>();

                #region 医院新增字段
                updateSql = "IF COL_LENGTH('B_Hospital', 'LabIdentityTypeID') IS NULL ALTER TABLE B_Hospital ADD LabIdentityTypeID bigint; ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('B_Hospital', 'BranchId') IS NULL ALTER TABLE B_Hospital ADD BranchId bigint; ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('B_Hospital', 'TINumber') IS NULL ALTER TABLE B_Hospital ADD TINumber varchar(80); ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('B_Hospital', 'BusinessAnalysisDateType') IS NULL ALTER TABLE B_Hospital ADD BusinessAnalysisDateType bigint; ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('B_Hospital', 'BusinessAnalysisDate') IS NULL ALTER TABLE B_Hospital ADD BusinessAnalysisDate varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('B_Hospital', 'BillNumber') IS NULL ALTER TABLE B_Hospital ADD BillNumber int; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.17 ");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.17) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.18
            if (IsUpdateDataBase(oldVersion, "1.0.0.18"))
            {
                List<string> listSQL = new List<string>();

                #region 修改医院区域字典
                updateSql = "IF COL_LENGTH('B_HospitalArea', 'HospitalArealID') IS not  NULL exec sp_rename 'B_HospitalArea.HospitalArealID','HospitalAreaID','column'; ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('B_HospitalArea', 'PHospitalArealID') IS not  NULL exec sp_rename 'B_HospitalArea.PHospitalArealID','PHospitalAreaID','column'; ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('B_HospitalArea', 'PHospitalArealName') IS not  NULL exec sp_rename 'B_HospitalArea.PHospitalArealName','PHospitalAreaName','column'; ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('B_HospitalArea', 'HospitalArealLevelName') IS not  NULL exec sp_rename 'B_HospitalArea.HospitalArealLevelName','HospitalAreaLevelName','column'; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.18 ");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.18) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.19
            if (IsUpdateDataBase(oldVersion, "1.0.0.19"))
            {
                List<string> listSQL = new List<string>();

                #region 修改新闻阅读人表
                updateSql = "IF COL_LENGTH('F_File_ReadingUser', 'AreaId') IS NULL ALTER TABLE F_File_ReadingUser ADD AreaId bigint; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.19 ");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.19) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.20
            if (IsUpdateDataBase(oldVersion, "1.0.0.20"))
            {
                List<string> listSQL = new List<string>();

                #region 修改医院字典表
                updateSql = "IF COL_LENGTH('B_Hospital', 'CountyName') IS NULL ALTER TABLE B_Hospital ADD CountyName varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('B_Hospital', 'CountyID') IS NULL ALTER TABLE B_Hospital ADD CountyID bigint; ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('B_Hospital', 'IsReceiveSamplePoint') IS NULL ALTER TABLE B_Hospital ADD IsReceiveSamplePoint bit; ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('B_Hospital', 'IsCooperation') IS NULL ALTER TABLE B_Hospital ADD IsCooperation  bit; ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('B_Hospital', 'PersonFixedCharges') IS NULL ALTER TABLE B_Hospital ADD PersonFixedCharges decimal(18,2); ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.20 ");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.20) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.21
            if (IsUpdateDataBase(oldVersion, "1.0.0.21"))
            {
                List<string> listSQL = new List<string>();

                #region 修改医院区域表外键
                updateSql = "if exists(select * from sysobjects where name='FK_B_HospitalArea_B_HospitalDept')alter table B_HospitalArea drop constraint FK_B_HospitalArea_B_HospitalDept;";
                listSQL.Add(updateSql);
                updateSql = "ALTER TABLE [dbo].[B_HospitalArea]  WITH CHECK ADD  CONSTRAINT [FK_B_HospitalArea_HR_Dept] FOREIGN KEY([DeptID])REFERENCES[dbo].[HR_Dept]([DeptID]); ALTER TABLE [dbo].[B_HospitalArea] CHECK CONSTRAINT [FK_B_HospitalArea_HR_Dept];";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.21 ");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.21) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.22
            if (IsUpdateDataBase(oldVersion, "1.0.0.22"))
            {
                List<string> listSQL = new List<string>();

                #region 消息表增加字段
                updateSql = "IF COL_LENGTH('SC_Msg', 'ConfirmDeptID') IS NULL ALTER TABLE SC_Msg ADD ConfirmDeptID bigint; ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'ConfirmDeptName') IS NULL ALTER TABLE SC_Msg ADD ConfirmDeptName varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'ConfirmDeptCode') IS NULL ALTER TABLE SC_Msg ADD ConfirmDeptCode varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'ConfirmDeptCodeHIS') IS NULL ALTER TABLE SC_Msg ADD ConfirmDeptCodeHIS varchar(100);";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.22");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.22) Update Error, Please Check The Log!");
            }
            #endregion


            #region 1.0.1.1
            if (IsUpdateDataBase(oldVersion, "1.0.1.1"))
            {
                List<string> listSQL = new List<string>();

                #region 增加微信消息模版表
                updateSql = "CREATE TABLE [dbo].[WX_WeiXin_PushMessageTemplate](	[LabID] [bigint] NULL,	[PMTID] [bigint] NOT NULL,	[PMTKey] [varchar](500) NULL,	[Name] [varchar](40) NULL,	[SName] [varchar](40) NULL,	[Shortcode] [varchar](20) NULL,	[PinYinZiTou] [nvarchar](50) NULL,	[Comment] [ntext] NULL,	[IsUse] [bit] NULL,	[DataAddTime] [datetime] NULL,[DataUpdateTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_WX_WEIXIN_PUSHMESSAGETEMPLA] PRIMARY KEY CLUSTERED (	[PMTID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                #region 增加微信员工帐号绑定表
                updateSql = "CREATE TABLE [dbo].[WX_WeiXin_Emp_Link](	[LabID] [bigint] NULL,	[WeiXin_Emp_LinkID] [bigint] NOT NULL,	[WeiXinAccountID] [bigint] NULL,	[EmpID] [bigint] NULL,	[EmpName] [varchar](50) NULL,	[DataAddTime] [datetime] NOT NULL,	[DataTimeStamp] [timestamp] NOT NULL, CONSTRAINT [PK_WX_WEIXIN_EMP_LINK] PRIMARY KEY CLUSTERED (	[WeiXin_Emp_LinkID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                #region 增加微信帐号分组表
                updateSql = "CREATE TABLE [dbo].[WX_WeiXinUserGroup](	[LabID] [bigint] NULL,	[B_WeiXinUserGroupID] [bigint] NOT NULL,	[GroupName] [varchar](20) NOT NULL,	[Comment] [varchar](300) NULL,	[OperaterId] [bigint] NULL,	[OperaterName] [varchar](20) NULL,	[Count] [int] NULL,	[AddTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_WX_WEIXINUSERGROUP] PRIMARY KEY CLUSTERED (	[B_WeiXinUserGroupID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                #region 增加微信帐号表
                updateSql = "CREATE TABLE [dbo].[WX_WeiXinAccount](	[LabID] [bigint] NULL,	[WeiXinAccountID] [bigint] NOT NULL,	[B_WeiXinUserGroupID] [bigint] NULL,	[IconsID] [bigint] NULL,	[AccountTypeID] [bigint] NULL,	[OpenID] [varchar](50) NOT NULL,	[UserName] [nvarchar](20) NOT NULL,	[SexID] [bigint] NULL,	[PassWord] [varchar](20) NULL,	[Name] [nvarchar](20) NULL,	[Brithday] [datetime] NULL,	[MobileCode] [char](11) NULL,	[IDNumber] [char](18) NULL,	[MediCare] [char](20) NULL,	[Email] [varchar](30) NULL,	[CountryID] [bigint] NULL,	[ProvinceID] [bigint] NULL,	[CityID] [bigint] NULL,	[CountryName] [varchar](30) NULL,	[ProvinceName] [varchar](30) NULL,	[CityName] [varchar](30) NULL,	[Language] [varchar](20) NULL,	[LoginInputPasswordFlag] [bit] NULL,	[Comment] [nvarchar](300) NULL,	[ConcernTime] [datetime] NULL,	[AddTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NOT NULL,	[LastLoginTime] [datetime] NULL,	[LastLoginAddressCoordinate] [varchar](20) NULL,	[HeadImgUrl] [varchar](500) NULL, CONSTRAINT [PK_WX_WEIXINACCOUNT] PRIMARY KEY CLUSTERED (	[WeiXinAccountID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                #region 增加微信消息发送日志表
                updateSql = "CREATE TABLE [dbo].[WX_MsgSend_Log](	[LabID] [bigint] NOT NULL,	[WXMsgSendLogID] [bigint] NOT NULL,	[BobjectID] [bigint] NOT NULL,	[Type] [bigint] NULL,	[TypeName] [varchar](20) NULL,	[BusinessModuleCode] [varchar](20) NULL,	[ReceiveObjectID] [bigint] NULL,	[ReceiveObjectName] [varchar](20) NULL,[ReceiveWeiXinAccountID] [bigint] NULL,	[ReceiveOpenID] [varchar](50) NULL,	[SenderID] [bigint] NULL,	[SenderName] [varchar](20) NULL,[SenderWeiXinAccountID] [bigint] NULL,	[SenderOpenID] [varchar](50) NULL,	[Memo] [varchar](500) NULL,	[DispOrder] [int] NULL,	[IsUse] [bit] NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_WX_MSGSEND_LOG] PRIMARY KEY CLUSTERED (	[WXMsgSendLogID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                #region 增加微信消息任务表
                updateSql = "CREATE TABLE [dbo].[WX_MessageSendTask](	[LabID] [bigint] NULL,	[WX_MessageSendTaskId] [bigint] NOT NULL,	[HospitalID] [bigint] NULL,	[PMTID] [bigint] NULL,	[HospitalCode] [nvarchar](20) NOT NULL,	[HospitalName] [nvarchar](20) NOT NULL,	[TaskName] [varchar](50) NULL,	[TaskCode] [varchar](50) NULL,	[TaskTypeID] [bigint] NULL,	[TaskTypeName] [varchar](50) NULL,	[SendTypeID] [bigint] NULL,	[SendTypeName] [varchar](50) NULL,	[Count] [int] NULL,	[Context] [ntext] NULL,	[AttachmentName] [varchar](50) NULL,	[AttachmentURL] [varchar](500) NULL,	[AttachmentSize] [varchar](500) NULL,	[Comment] [varchar](300) NULL,	[CreaterID] [bigint] NULL,	[CreaterName] [varchar](50) NULL,[ReceiveObjectID] [bigint] NULL,	[ReceiveObjectName] [varchar](20) NULL,[ReceiveWeiXinAccountID] [bigint] NULL,	[ReceiveOpenID] [varchar](50) NULL,	[DataUpdateTime] [datetime] NULL,	[DataAddTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL,CONSTRAINT [PK_WX_MESSAGESENDTASK] PRIMARY KEY CLUSTERED (	[WX_MessageSendTaskId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                #region 增加微信账户图标表
                updateSql = "CREATE TABLE [dbo].[WX_Icons](	[LabID] [bigint] NULL,	[IconsID] [bigint] NOT NULL,	[IconsTypeID] [bigint] NULL,	[Width] [int] NULL,	[Height] [int] NULL,	[Size] [bigint] NULL,	[Url] [nvarchar](100) NOT NULL,	[DataAddTime] [datetime] NOT NULL,	[DataTimeStamp] [timestamp] NOT NULL,	[IsLocal] [bit] NOT NULL,	[Comment] [nvarchar](300) NULL, CONSTRAINT [PK_WX_ICONS] PRIMARY KEY CLUSTERED (	[IconsID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                #region 增加微信账户类型表
                updateSql = "CREATE TABLE [dbo].[WX_AccountType](	[LabID] [bigint] NULL,	[AccountTypeID] [bigint] NOT NULL,	[Name] [varchar](40) NULL,	[EName] [varchar](50) NULL,	[SName] [varchar](40) NULL,	[Shortcode] [dbo].[D_快捷码] NULL,	[PinYinZiTou] [dbo].[D_汉语拼音字头] NULL,	[Comment] [varchar](300) NULL,	[IsUse] [bit] NULL,	[DataAddTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_WX_ACCOUNTTYPE] PRIMARY KEY CLUSTERED (	[AccountTypeID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                #region 增加微信表索引及外键
                updateSql = "ALTER TABLE [dbo].[WX_WeiXin_Emp_Link]  WITH CHECK ADD  CONSTRAINT [FK_WX_WEIXI_REFERENCE_WX_WEIXI1] FOREIGN KEY([WeiXinAccountID])REFERENCES [dbo].[WX_WeiXinAccount] ([WeiXinAccountID]);ALTER TABLE [dbo].[WX_WeiXin_Emp_Link] CHECK CONSTRAINT [FK_WX_WEIXI_REFERENCE_WX_WEIXI1];ALTER TABLE [dbo].[WX_WeiXin_Emp_Link]  WITH CHECK ADD  CONSTRAINT [FK_WX_WeiXin_Emp_Link_HR_Employee] FOREIGN KEY([EmpID])REFERENCES [dbo].[HR_Employee] ([EmpID]);ALTER TABLE [dbo].[WX_WeiXin_Emp_Link] CHECK CONSTRAINT [FK_WX_WeiXin_Emp_Link_HR_Employee];ALTER TABLE [dbo].[WX_WeiXinAccount]  WITH CHECK ADD  CONSTRAINT [FK_WX_WEIXI_REFERENCE_WX_ACCOU] FOREIGN KEY([AccountTypeID])REFERENCES [dbo].[WX_AccountType] ([AccountTypeID]);ALTER TABLE [dbo].[WX_WeiXinAccount] CHECK CONSTRAINT [FK_WX_WEIXI_REFERENCE_WX_ACCOU];ALTER TABLE [dbo].[WX_WeiXinAccount]  WITH CHECK ADD  CONSTRAINT [FK_WX_WEIXI_REFERENCE_WX_ICONS] FOREIGN KEY([IconsID])REFERENCES [dbo].[WX_Icons] ([IconsID]);ALTER TABLE [dbo].[WX_WeiXinAccount] CHECK CONSTRAINT [FK_WX_WEIXI_REFERENCE_WX_ICONS];ALTER TABLE [dbo].[WX_WeiXinAccount]  WITH CHECK ADD  CONSTRAINT [FK_WX_WEIXI_REFERENCE_WX_WEIXI] FOREIGN KEY([B_WeiXinUserGroupID])REFERENCES [dbo].[WX_WeiXinUserGroup] ([B_WeiXinUserGroupID]);ALTER TABLE [dbo].[WX_WeiXinAccount] CHECK CONSTRAINT [FK_WX_WEIXI_REFERENCE_WX_WEIXI];ALTER TABLE [dbo].[WX_MessageSendTask]  WITH CHECK ADD  CONSTRAINT [FK_WX_MESSA_REFERENCE_WX_WEIXI] FOREIGN KEY([PMTID])REFERENCES [dbo].[WX_WeiXin_PushMessageTemplate] ([PMTID]);ALTER TABLE [dbo].[WX_MessageSendTask] CHECK CONSTRAINT [FK_WX_MESSA_REFERENCE_WX_WEIXI]; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.1.1");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.1.1) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.1.2
            if (IsUpdateDataBase(oldVersion, "1.0.1.2"))
            {
                List<string> listSQL = new List<string>();

                #region 增加账户申请表
                updateSql = "CREATE TABLE [dbo].[S_AccountRegister](	[LabID] [bigint] NOT NULL,	[AARId] [bigint] NOT NULL,	[EmpID] [bigint] NULL,	[WeiXinAccountID] [bigint] NULL,	[Name] [nvarchar](30) NULL,	[EName] [nvarchar](50) NULL,	[IdNumber] [nvarchar](50) NULL,	[Birthday] [datetime] NULL,	[SexID] [bigint] NULL,	[SexName] [nvarchar](20) NULL,	[HospitalID] [bigint] NULL,	[HospitalCode] [nvarchar](20) NULL,	[IconsID] [bigint] NULL,	[ProvinceName] [nvarchar](20) NULL,	[CityName] [nvarchar](20) NULL,	[ProvinceID] [bigint] NULL,	[CountyID] [bigint] NULL,	[CountyName] [nvarchar](20) NULL,	[CityID] [bigint] NULL,	[MobileTel] [nvarchar](20) NULL,	[Address] [nvarchar](200) NULL,	[EMAIL] [nvarchar](20) NULL,	[MAILNO] [nvarchar](20) NULL,	[FaxNo] [nvarchar](20) NULL,	[Comment] [nvarchar](1000) NULL,	[IsUse] [bit] NULL,	[DataTimeStamp] [timestamp] NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NULL,	[ApprovalID] [bigint] NULL,	[ApprovalName] [nvarchar](20) NULL,	[ApprovalDateTime] [datetime] NULL,	[ApprovalInfo] [nvarchar](300) NULL,	[ApplySourceTypeID] [bigint] NULL,	[ApplySourceTypeName] [nvarchar](20) NULL,	[IdInfoNo] [nvarchar](50) NULL,	[IdInfoTypeId] [bigint] NULL,	[StatusId] [bigint] NULL,	[StatusName] [nvarchar](20) NULL,	[Account] [nvarchar](50) NULL,	[PassWord] [nvarchar](50) NULL, CONSTRAINT [PK_B_ACCOUNTREGISTER] PRIMARY KEY CLUSTERED (	[AARId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]";
                listSQL.Add(updateSql);
                #endregion

                #region 增加账户申请表外键
                updateSql = "ALTER TABLE [dbo].[S_AccountRegister]  WITH CHECK ADD  CONSTRAINT [FK_S_AccountRegister_HR_Employee] FOREIGN KEY([EmpID]) REFERENCES[dbo].[HR_Employee]([EmpID]); ALTER TABLE[dbo].[S_AccountRegister] CHECK CONSTRAINT[FK_S_AccountRegister_HR_Employee];        ALTER TABLE[dbo].[S_AccountRegister] WITH CHECK ADD CONSTRAINT[FK_S_AccountRegister_WX_WeiXinAccount] FOREIGN KEY([WeiXinAccountID]) REFERENCES[dbo].[WX_WeiXinAccount] ([WeiXinAccountID]);ALTER TABLE[dbo].[S_AccountRegister]     CHECK CONSTRAINT[FK_S_AccountRegister_WX_WeiXinAccount]";
                listSQL.Add(updateSql);
                #endregion


                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.1.2");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.1.2) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.1.3
            if (IsUpdateDataBase(oldVersion, "1.0.1.3"))
            {
                List<string> listSQL = new List<string>();

                #region 增加账户申请表申请备注信息字段
                updateSql = "IF COL_LENGTH('S_AccountRegister', 'ApplyInfo') IS NULL ALTER TABLE S_AccountRegister ADD ApplyInfo ntext;";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.1.3");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.1.3) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.1.4
            if (IsUpdateDataBase(oldVersion, "1.0.1.4"))
            {
                List<string> listSQL = new List<string>();

                #region 增加UnionID
                updateSql = "IF COL_LENGTH('WX_WeiXinAccount', 'UnionID') IS NULL ALTER TABLE WX_WeiXinAccount ADD UnionID varchar(100);";
                listSQL.Add(updateSql);
                #endregion

                #region 增加账户类型区分用户来源，null或0微信公众号，1微信小程序
                updateSql = "IF COL_LENGTH('WX_WeiXinAccount', 'SourceTypeID') IS NULL ALTER TABLE WX_WeiXinAccount ADD SourceTypeID bigint;";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.1.4");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.1.4) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.1.5
            if (IsUpdateDataBase(oldVersion, "1.0.1.5"))
            {
                List<string> listSQL = new List<string>();

                #region B_AccountRegister增加HospitalName
                updateSql = "IF COL_LENGTH('S_AccountRegister', 'HospitalName') IS NULL ALTER TABLE S_AccountRegister ADD HospitalName nvarchar(50);";
                listSQL.Add(updateSql);
                #endregion


                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.1.5");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.1.5) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.1.6
            if (IsUpdateDataBase(oldVersion, "1.0.1.6"))
            {
                List<string> listSQL = new List<string>();

                #region B_Hospital表增加EditerId,EditerName
                updateSql = "IF COL_LENGTH('B_Hospital', 'EditerId') IS NULL ALTER TABLE B_Hospital ADD EditerId bigint;";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('B_Hospital', 'EditerName') IS NULL ALTER TABLE B_Hospital ADD EditerName varchar(50);";
                listSQL.Add(updateSql);
                #endregion


                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.1.6");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.1.6) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.1.7
            if (IsUpdateDataBase(oldVersion, "1.0.1.7"))
            {
                List<string> listSQL = new List<string>();

                #region SC_Msg表增加NotifyDoctorCount,NotifyDoctorLastDateTime,NotifyDoctorByEmpName,NotifyDoctorByEmpID,ConfirmUseTime,HandleUseTime,ConfirmHandleUseTime

                updateSql = "IF COL_LENGTH('SC_Msg', 'NotifyDoctorCount') IS NULL ALTER TABLE SC_Msg ADD NotifyDoctorCount bigint;";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'NotifyDoctorLastDateTime') IS NULL ALTER TABLE SC_Msg ADD NotifyDoctorLastDateTime datetime;";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'NotifyDoctorByEmpName') IS NULL ALTER TABLE SC_Msg ADD NotifyDoctorByEmpName varchar(50);";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'NotifyDoctorByEmpID') IS NULL ALTER TABLE SC_Msg ADD NotifyDoctorByEmpID bigint;";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'ConfirmUseTime') IS NULL ALTER TABLE SC_Msg ADD ConfirmUseTime bigint;";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'HandleUseTime') IS NULL ALTER TABLE SC_Msg ADD HandleUseTime bigint;";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'ConfirmHandleUseTime') IS NULL ALTER TABLE SC_Msg ADD ConfirmHandleUseTime bigint;";
                listSQL.Add(updateSql);

                #endregion
                //update SC_Msg set ConfirmUseTime=DATEDIFF(SECOND,DataAddTime,ConfirmDateTime)
                //update SC_Msg set HandleUseTime = DATEDIFF(SECOND, ConfirmDateTime, FirstHandleDateTime)
                //update SC_Msg set ConfirmHandleUseTime = DATEDIFF(SECOND, DataAddTime, FirstHandleDateTime)


                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.1.7");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.1.7) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.1.8
            if (IsUpdateDataBase(oldVersion, "1.0.1.8"))
            {
                List<string> listSQL = new List<string>();

                #region SC_Msg表增加HandleTypeID,HandleTypeName

                updateSql = "IF COL_LENGTH('SC_Msg', 'HandleTypeID') IS NULL ALTER TABLE SC_Msg ADD HandleTypeID bigint;";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'HandleTypeName') IS NULL ALTER TABLE SC_Msg ADD HandleTypeName varchar(50);";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.1.8");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.1.8) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.1.9
            if (IsUpdateDataBase(oldVersion, "1.0.1.9"))
            {
                List<string> listSQL = new List<string>();

                #region SC_Msg表增加LastRequireHandleTime

                updateSql = "IF COL_LENGTH('SC_Msg', 'LastRequireHandleTime') IS NULL ALTER TABLE SC_Msg ADD LastRequireHandleTime datetime;";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.1.9");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.1.9) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.1.10
            if (IsUpdateDataBase(oldVersion, "1.0.1.10"))
            {
                List<string> listSQL = new List<string>();

                #region SC_Msg表增加检验项目信息

                updateSql = "IF COL_LENGTH('SC_Msg', 'TestItemNoList') IS NULL ALTER TABLE SC_Msg ADD TestItemNoList varchar(1000);";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('SC_Msg', 'TestItemNameList') IS NULL ALTER TABLE SC_Msg ADD TestItemNameList varchar(2000);";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.1.10");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.1.10) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.1.11
            if (IsUpdateDataBase(oldVersion, "1.0.1.11"))
            {
                List<string> listSQL = new List<string>();

                #region SC_Msg表增加检验项目信息

                updateSql = "IF COL_LENGTH('SC_Msg', 'RequireAllFinishTime') IS NULL ALTER TABLE SC_Msg ADD RequireAllFinishTime datetime;";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.1.11");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.1.11) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.1.12
            if (IsUpdateDataBase(oldVersion, "1.0.1.12"))
            {
                List<string> listSQL = new List<string>();

                #region 模块表加组件列表字段

                updateSql = "IF COL_LENGTH('RBAC_Module', 'ComponentsListJson') IS NULL ALTER TABLE RBAC_Module ADD ComponentsListJson varchar(8000);";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.1.12");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.1.12) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.1.13
            if (IsUpdateDataBase(oldVersion, "1.0.1.13"))
            {
                List<string> listSQL = new List<string>();

                #region 模块表加组件列表字段

                updateSql = "IF COL_LENGTH('RBAC_Module', 'ComponentsListJson') IS NULL ALTER TABLE RBAC_Module ADD ComponentsListJson varchar(8000);";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.1.13");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.1.13) Update Error, Please Check The Log!");
            }
            #endregion

            return result;
        }

        private static bool IsUpdateDataBase(string oldVersion, string newVersion)
        {
            bool result = false;
            //比较数据库版本号，判断是否可升级
            if (CompareDBVersion(oldVersion, newVersion))
            {
                string curAssemblyVersion = GetMainAssemblyVersion();
                if (DicVersion.Keys.Contains(newVersion))
                {
                    string mainAssemblyVersion = DicVersion[newVersion];
                    if (CompareAssemblyVersion(curAssemblyVersion, mainAssemblyVersion))
                        result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// 获取某一程序集版本
        /// </summary>
        /// <param name="mainAssemblyFile">程序集名称</param>
        /// <returns></returns>
        private static string GetMainAssemblyVersion()
        {
            string mainPath = AppDomain.CurrentDomain.BaseDirectory;
            Assembly assembly = Assembly.LoadFile(mainPath + "\\Bin\\" + MainAssemblyFile);
            AssemblyName assemblyName = assembly.GetName();
            return assemblyName.Version.ToString();
        }

        /// <summary>
        /// 比较数据库版本
        /// </summary>
        /// <param name="oldVersion"></param>
        /// <param name="newVersion"></param>
        /// <returns></returns>
        private static bool CompareDBVersion(string oldVersion, string newVersion)
        {
            bool result = false;
            if ((!string.IsNullOrEmpty(oldVersion.Trim())) && (!string.IsNullOrEmpty(newVersion.Trim())))
            {
                try
                {
                    string[] oldVersionList = oldVersion.Split('.');
                    string[] newVersionList = newVersion.Split('.');
                    if (oldVersionList.Length == newVersionList.Length)
                    {
                        for (int i = 0; i < newVersionList.Length; i++)
                        {
                            if (int.Parse(oldVersionList[i]) != int.Parse(newVersionList[i]))
                            {
                                if (int.Parse(oldVersionList[i]) < int.Parse(newVersionList[i]))
                                {
                                    result = true;
                                    break;
                                }
                                else
                                {
                                    result = false;
                                    break;
                                }
                            }
                        }
                    }
                    else if (oldVersionList.Length < newVersionList.Length)
                        result = true;
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Info("Update CompareVersion Error：" + ex.Message);
                }
            }
            else if (string.IsNullOrEmpty(oldVersion.Trim()) && (!string.IsNullOrEmpty(newVersion.Trim())))
                result = true;
            // result default false
            //else if ((!string.IsNullOrEmpty(oldVersion.Trim())) && string.IsNullOrEmpty(newVersion.Trim()))
            //    result = false;
            //else if (string.IsNullOrEmpty(oldVersion.Trim()) || string.IsNullOrEmpty(newVersion.Trim()))
            //    result = false;
            return result;
        }

        /// <summary>
        /// 比较程序集版本
        /// </summary>
        /// <param name="oldVersion"></param>
        /// <param name="newVersion"></param>
        /// <returns></returns>
        private static bool CompareAssemblyVersion(string oldVersion, string newVersion)
        {
            bool result = false;
            if ((!string.IsNullOrEmpty(oldVersion.Trim())) && (!string.IsNullOrEmpty(newVersion.Trim())))
            {
                try
                {
                    string[] oldVersionList = oldVersion.Split('.');
                    string[] newVersionList = newVersion.Split('.');
                    if (oldVersionList.Length == newVersionList.Length)
                    {
                        int equalCount = 0;
                        for (int i = 0; i < newVersionList.Length; i++)
                        {
                            if (int.Parse(oldVersionList[i]) > int.Parse(newVersionList[i]))
                            {
                                result = true;
                                break;
                            }
                            else if (int.Parse(oldVersionList[i]) == int.Parse(newVersionList[i]))
                            {
                                equalCount++;
                            }
                        }
                        if (equalCount == oldVersionList.Length)
                            result = true;
                    }
                    else if (oldVersionList.Length < newVersionList.Length)
                        result = true;
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Info("Update CompareVersion Error：" + ex.Message);
                }
            }
            else if (string.IsNullOrEmpty(oldVersion.Trim()) && (!string.IsNullOrEmpty(newVersion.Trim())))
                result = true;
            // result default false
            //else if ((!string.IsNullOrEmpty(oldVersion.Trim())) && string.IsNullOrEmpty(newVersion.Trim()))
            //    result = false;
            //else if (string.IsNullOrEmpty(oldVersion.Trim()) || string.IsNullOrEmpty(newVersion.Trim()))
            //    result = false;
            if (!result)
                ZhiFang.Common.Log.Log.Info("当前主程序集版本低于配置版本，无法升级！CurAssemblyVersion：" + oldVersion
                    + "；NewAssemblyVersion：" + newVersion);
            return result;
        }

        /// <summary>
        /// 升级后更新版本相关信息
        /// </summary>
        /// <param name="newVersion"></param>
        /// <returns></returns>
        private static bool UpateCompareVersionInfo(string newVersion)
        {
            string updateSql = " update B_Parameter set ParaValue=\'" + newVersion + "\'" + " where ParaType=\'SYS\' and ParaNo=\'SYS_DBVersion\'";
            return ExecuteUpdateSQL(updateSql);
        }

        /// <summary>
        /// 获取数据库当前版本
        /// </summary>
        /// <returns></returns>
        private static string GetDataBaseCurVersion()
        {
            string curVersion = "1.0.0.0";
            DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Parameter where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                curVersion = ds.Tables[0].Rows[0][0].ToString();
            }
            return curVersion;
        }

        /// <summary>
        /// 检查数据库对象是否存在（表-U、视图-V、存储过程-P、触发器-TR、标量函数-FN等）
        /// </summary>
        /// <param name="objectname"></param>
        /// <param name="objectType"></param>
        /// <returns></returns>
        private static bool CheckDataObjectIsExists(string objectname, string objectType)
        {
            string objectID = "";
            DataSet ds = SqlServerHelper.QuerySql("select object_id(\'" + objectname + "\', \'" + objectType + "\') as ObjectID", ADOConnectStr);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                objectID = ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                return false;
            }
            if (objectID != null && objectID != "")
            {
                ZhiFang.Common.Log.Log.Debug("objectID:" + objectID);
            }
            else
            {
                return false;
            }
            return (!string.IsNullOrEmpty(objectID));
        }

        /// <summary>
        /// 检查参数表是否存在，不存在则创建
        /// </summary>
        private static void CheckBParameterIsIsExists()
        {
            string updateSql = "";
            if (!CheckDataObjectIsExists("B_Parameter", "U"))
            {
                List<string> listSQL = new List<string>();
                updateSql = " CREATE TABLE [dbo].[B_Parameter]( " +
                            " [LabID]           [bigint] NOT NULL, " +
                            " [ParameterID]     [bigint] NOT NULL, " +
                            " [PDictId]         [bigint]       NULL, " +
                            " [Name]            [varchar](100) NULL, " +
                            " [SName]           [varchar](100) NULL, " +
                            " [ParaType]        [varchar](100) NULL, " +
                            " [ParaNo]          [varchar](50) NULL, " +
                            " [ParaValue]       [nvarchar](max) NULL, " +
                            " [ParaDesc]        [nvarchar](1000) NULL, " +
                            " [Shortcode]       [varchar](100) NULL, " +
                            " [DispOrder]       [int] NULL, " +
                            " [PinYinZiTou]     [varchar](100) NULL, " +
                            " [IsUse]           [bit]        NULL, " +
                            " [IsUserSet]       [bit]        NULL, " +
                            " [DataAddTime]     [datetime]        NULL, " +
                            " [DataUpdateTime]  [datetime]        NULL, " +
                            " [DataTimeStamp]   [timestamp]        NULL, " +
                            "  CONSTRAINT[PK_B_PARAMETER] PRIMARY KEY CLUSTERED " +
                            " ( " +
                            "    [ParameterID] ASC " +
                            " )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] " +
                            " ) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY] ";

                listSQL.Add(updateSql);

                updateSql = "ALTER TABLE[dbo].[B_Parameter] WITH CHECK ADD CONSTRAINT[FK_B_Parameter_P_Dict] FOREIGN KEY([PDictId]) REFERENCES[dbo].[P_Dict] ([DictID])";
                listSQL.Add(updateSql);

                updateSql = "ALTER TABLE[dbo].[B_Parameter] CHECK CONSTRAINT[FK_B_Parameter_P_Dict]";
                listSQL.Add(updateSql);

                updateSql = "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'实验室ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'B_Parameter\', @level2type = N\'COLUMN\', @level2name = N\'LabID\'";
                listSQL.Add(updateSql);

                updateSql = "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'参数ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'B_Parameter\', @level2type = N\'COLUMN\', @level2name = N\'ParameterID\'";
                listSQL.Add(updateSql);

                updateSql = "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'字典Id\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'B_Parameter\', @level2type = N\'COLUMN\', @level2name = N\'PDictId\'";
                listSQL.Add(updateSql);

                updateSql = "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'名称\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'B_Parameter\', @level2type = N\'COLUMN\', @level2name = N\'Name\'";
                listSQL.Add(updateSql);

                updateSql = "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'简称\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'B_Parameter\', @level2type = N\'COLUMN\', @level2name = N\'SName\'";
                listSQL.Add(updateSql);

                updateSql = "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'参数类型用于查询方便可根据用户习惯对参数分类。\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'B_Parameter\', @level2type=N\'COLUMN\',@level2name=N\'ParaType\'";
                listSQL.Add(updateSql);

                updateSql = "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'参数编码\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'B_Parameter\', @level2type = N\'COLUMN\', @level2name = N\'ParaNo\'";
                listSQL.Add(updateSql);

                updateSql = "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'参数值\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'B_Parameter\', @level2type = N\'COLUMN\', @level2name = N\'ParaValue\'";
                listSQL.Add(updateSql);

                updateSql = "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'参数说明\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'B_Parameter\', @level2type = N\'COLUMN\', @level2name = N\'ParaDesc\'";
                listSQL.Add(updateSql);

                updateSql = "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'快捷码\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'B_Parameter\', @level2type = N\'COLUMN\', @level2name = N\'Shortcode\'";
                listSQL.Add(updateSql);

                updateSql = "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'显示次序\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'B_Parameter\', @level2type = N\'COLUMN\', @level2name = N\'DispOrder\'";
                listSQL.Add(updateSql);

                updateSql = "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'汉字拼音字头\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'B_Parameter\', @level2type = N\'COLUMN\', @level2name = N\'PinYinZiTou\'";
                listSQL.Add(updateSql);

                updateSql = "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'是否使用\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'B_Parameter\', @level2type = N\'COLUMN\', @level2name = N\'IsUse\'";
                listSQL.Add(updateSql);

                updateSql = "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'是否允许用户设置\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'B_Parameter\', @level2type = N\'COLUMN\', @level2name = N\'IsUserSet\'";
                listSQL.Add(updateSql);

                updateSql = "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'创建时间\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'B_Parameter\', @level2type = N\'COLUMN\', @level2name = N\'DataAddTime\'";
                listSQL.Add(updateSql);

                updateSql = "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'数据更新时间\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'B_Parameter\', @level2type = N\'COLUMN\', @level2name = N\'DataUpdateTime\'";
                listSQL.Add(updateSql);

                updateSql = "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'时间戳\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'B_Parameter\', @level2type = N\'COLUMN\', @level2name = N\'DataTimeStamp\'";
                listSQL.Add(updateSql);

                updateSql = "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'参数表\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'B_Parameter\'";
                listSQL.Add(updateSql);

                ExecuteUpdateSQL(listSQL);

                ExecuteUpdateSQL("insert into [B_Parameter]([LabID],[ParameterID],[PDictId],[Name],[SName],[ParaType],[ParaNo],[ParaValue]," +
                    "[ParaDesc],[Shortcode],[DispOrder],[PinYinZiTou],[IsUse],[IsUserSet],[DataAddTime],[DataUpdateTime]) " +
                    " Values(1," +//LabID
                    ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "," +//ParameterID
                    "null," +//PDictId
                    "\'数据库版本号\'," +//Name
                    "null," +//SName
                    "\'SYS\'," +//ParaType
                     "\'SYS_DBVersion\'," +//ParaNo
                     "null," +//ParaValue
                     "\'数据库版本号\'," +//ParaDesc
                     "null," +//Shortcode
                     "0," +//DispOrder
                     "null," +//PinYinZiTou
                     "1," +//IsUse
                     "null," +//IsUserSet
                     "\'" + DateTime.Now.ToString() + "\'," +//DataAddTime
                     "null" + //DataUpdateTime
                     ")");
            }
        }

        private static bool ExecuteUpdateSQL(string sql)
        {
            return SqlServerHelper.ExecuteSql(sql, ADOConnectStr);
        }

        private static bool ExecuteUpdateSQL(List<string> listSQL)
        {
            return SqlServerHelper.ExecuteSqlList(listSQL, ADOConnectStr);
        }
    }
}
