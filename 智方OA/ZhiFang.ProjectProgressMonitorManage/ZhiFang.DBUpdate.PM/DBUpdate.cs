using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System.Configuration;
using System.Collections.Specialized;


namespace ZhiFang.DBUpdate.PM
{
    public class DBUpdate
    {
        public static string ADOConnectStr = GetADODataBaseSettings(ZhiFang.Common.Public.ConfigHelper.GetDataBaseSettings("databaseSettings", "db.connectionString"));

        static Dictionary<string, string> DicVersion = GetVersionComparison();//

        static string MainAssemblyFile = "ZhiFang.ProjectProgressMonitorManage.dll";//可以从配置文件获取

        /// <summary>
        /// 初始化数据库版本和主程序集版本关系，key数据库版本，value主程序集版本
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, string> GetVersionComparison()
        {
            //每更新一次版本，需要手工在这里添加对应关系
            Dictionary<string, string> dicVersion = new Dictionary<string, string>();
            //dicVersion.Add("1.0.0.1", "1.0.0.1");
            //dicVersion.Add("1.0.0.2", "1.0.0.2");
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
            dicVersion.Add("1.0.0.14", "1.0.0.14");//QMS升级质量记录审核存储过程和修改对应的实体与配置文件
            dicVersion.Add("1.0.0.15", "1.0.0.15");
            dicVersion.Add("1.0.0.16", "1.0.0.16");//QMS升级质量记录新增列表存储过程和增加保存批号字段
            dicVersion.Add("1.0.0.17", "1.0.0.17");//QMS升级质量记录修改模板表的数据类型
            dicVersion.Add("1.0.0.18", "1.0.0.18");
            dicVersion.Add("1.0.0.19", "1.0.0.19");
            dicVersion.Add("1.0.0.20", "1.0.0.20");
            dicVersion.Add("1.0.0.21", "1.0.0.21");
            dicVersion.Add("1.0.0.22", "1.0.0.22");
            dicVersion.Add("1.0.0.23", "1.0.0.23");
            dicVersion.Add("1.0.0.24", "1.0.0.24");
            dicVersion.Add("1.0.0.25", "1.0.0.25");
            dicVersion.Add("1.0.0.26", "1.0.0.26");
            dicVersion.Add("1.0.0.27", "1.0.0.27");
            dicVersion.Add("1.0.0.28", "1.0.0.28");
            dicVersion.Add("1.0.0.29", "1.0.0.29");
            dicVersion.Add("1.0.0.30", "1.0.0.30");
            dicVersion.Add("1.0.0.31", "1.0.0.31");
            dicVersion.Add("1.0.0.32", "1.0.0.32");
            dicVersion.Add("1.0.0.33", "1.0.0.33");
            dicVersion.Add("1.0.0.34", "1.0.0.34");
            dicVersion.Add("1.0.0.35", "1.0.0.35");
            dicVersion.Add("1.0.0.36", "1.0.0.36");
            dicVersion.Add("1.0.0.37", "1.0.0.37");
            dicVersion.Add("1.0.0.38", "1.0.0.38");
            dicVersion.Add("1.0.0.39", "1.0.0.39");
            dicVersion.Add("1.0.0.40", "1.0.0.40");
            dicVersion.Add("1.0.0.41", "1.0.0.41");
            dicVersion.Add("1.0.0.42", "1.0.0.42");
            dicVersion.Add("1.0.0.43", "1.0.0.43");
            dicVersion.Add("1.0.0.44", "1.0.0.44");
            dicVersion.Add("1.0.0.45", "1.0.0.45");
            dicVersion.Add("1.0.0.46", "1.0.0.46");
            dicVersion.Add("1.0.0.47", "1.0.0.47");
            dicVersion.Add("1.0.0.48", "1.0.0.48");
            dicVersion.Add("1.0.0.49", "1.0.0.49");//QMS数据库升级
            dicVersion.Add("1.0.0.50", "1.0.0.50");//QMS质量记录查询审核存储过程修改
            dicVersion.Add("1.0.0.51", "1.0.0.51");//QMS质量记录增加职责表、职责模板关系表、职责员工关系表
            dicVersion.Add("1.0.0.52", "1.0.0.52");//QMS升级质量记录修改模板表的数据类型
            dicVersion.Add("1.0.0.53", "1.0.0.53");//QMS质量记录查询审核存储过程修改
            dicVersion.Add("1.0.0.54", "1.0.0.54");//QMS质量记录增加审核意见字段的长度
            dicVersion.Add("1.0.0.55", "1.0.0.55");//QMS质量记录增加参数表
            dicVersion.Add("1.0.0.56", "1.0.0.56");//员工表NameL，NameF字段修改为可以为空
            dicVersion.Add("1.0.0.57", "1.0.0.57");//删除P_Dict表外键FK_P_Dict_S_ServiceClient
            dicVersion.Add("1.0.0.58", "1.0.0.58");//QMS质量记录查询审核存储过程修改
            dicVersion.Add("1.0.0.59", "1.0.0.59");
            dicVersion.Add("1.0.0.60", "1.0.0.60");//QMS质量记录模板表和审核表增加审核类型字段、增加按天审核存储过程
            dicVersion.Add("1.0.0.61", "1.0.0.61");
            dicVersion.Add("1.0.0.62", "1.0.0.62");//RBAC_User加帐号唯一索引
            dicVersion.Add("1.0.0.63", "1.0.0.63");
            dicVersion.Add("1.0.0.64", "1.0.0.64");//QMS质量记录查询列表数据存储过程修改
            dicVersion.Add("1.0.0.65", "1.0.0.65");//QMS质量记录增加参数
            dicVersion.Add("1.0.0.66", "1.0.0.66");//QMS质量审核查询存储过程修改和增加参数
            dicVersion.Add("1.0.0.67", "1.0.0.67");//QMS质量模板增加字段和增加查询一个模板多份质量记录的存储过程
            dicVersion.Add("1.0.0.68", "1.0.0.68");
            dicVersion.Add("1.0.0.69", "1.0.0.69");
            dicVersion.Add("1.0.0.70", "1.0.0.70");
            dicVersion.Add("1.0.0.71", "1.0.0.71");
            dicVersion.Add("1.0.0.72", "1.0.0.72");
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
            //if (IsUpdateDataBase(oldVersion, "1.0.0.1"))
            //{
            //    updateSql = " update B_Parameter set Shortcode=\'" + oldVersion + "\'" + " where ParaType=\'SYS\' and ParaNo=\'SYS_DBVersion\'";
            //    result = ExecuteUpdateSQL(updateSql);
            //    result = UpateCompareVersionInfo("1.0.0.1");
            //}
            //if (IsUpdateDataBase(oldVersion, "1.0.0.2"))
            //{
            //    updateSql = "CREATE VIEW [dbo].[View_111] AS SELECT dbo.B_City.* FROM  dbo.B_City ";
            //    result = ExecuteUpdateSQL(updateSql);
            //    result = UpateCompareVersionInfo("1.0.0.2");
            //}
            #region 1.0.0.3
            if (IsUpdateDataBase(oldVersion, "1.0.0.3"))
            {
                //updateSql = " if Not Exists(Select * from SysColumns where [Name]= \'DispOrder\'" +
                //            " and ID = (Select [ID] from SysObjects where Name = \'RBAC_User\'" +
                //            " )) Alter Table RBAC_User Add DispOrder int ";
                updateSql = " if not exists (select * from dbo.sysobjects where id = object_id(N\'[dbo].[S_Log]\')" +
                " and OBJECTPROPERTY(id, N\'IsUserTable\') = 1) BEGIN " +
                " CREATE TABLE[dbo].[S_Log]( " +
                " [LabID][dbo].[D_实验室ID] NOT NULL, " +
                " [LogID] [dbo].[D_系统主键]  NOT NULL, " +
                " [EmpID] [dbo].[D_系统主键]   NOT NULL, " +
                " [EmpName] [varchar](50) NULL, " +
                " [OperateName] [varchar](512) NOT NULL, " +
                " [OperateType] [varchar](200) NOT NULL, " +
                " [IP] [varchar](50) NOT NULL, " +
                " [InfoLevel] [int] NOT NULL, " +
                " [Comment] [ntext]   NULL, " +
                " [DataAddTime][datetime]  NULL, " +
                " [DataTimeStamp]  [timestamp] NULL, " +
                " CONSTRAINT[PK_S_LOG] PRIMARY KEY CLUSTERED " +
                " ( " +
                "    [LogID] ASC " +
                " )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] " +
                " ) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY] " +
                " end";
                result = ExecuteUpdateSQL(updateSql);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.3");
                else
                    ZhiFang.Common.Log.Log.Error("Update Error");
            }
            #endregion
            #region 1.0.0.4
            if (IsUpdateDataBase(oldVersion, "1.0.0.4"))
            {
                if (!CheckDataObjectIsExists("E_Attachment", "U"))
                {
                    List<string> listSQL = new List<string>();
                    updateSql = " if not exists (select * from dbo.sysobjects where id = object_id(N\'[dbo].[E_Attachment]\')" +
                    " and OBJECTPROPERTY(id, N\'IsUserTable\') = 1) BEGIN " +
                    " CREATE TABLE[dbo].[E_Attachment]( " +
                    " [LabID] [bigint] NOT NULL, " +
                    " [EAttachmentID] [bigint] NOT NULL, " +
                    " [TempletID] [bigint] NULL, " +
                    " [TempletType] [varchar](200) NULL, " +
                    " [TempletTypeCode] [varchar](100) NULL, " +
                    " [FileName] [varchar](100) NULL, " +
                    " [FileExt] [varchar](100) NULL, " +
                    " [FileSize] [bigint]  NULL, " +
                    " [FilePath] [varchar](200) NULL, " +
                    " [FileNewName] [varchar](100) NULL, " +
                    " [FileUploadDate] [datetime] NULL, " +
                    " [FileType] [varchar](100) NULL, " +
                    " [Memo] [varchar](500) NULL, " +
                    " [DispOrder] [int] NULL, " +
                    " [IsUse] [bit]  NULL, " +
                    " [CreatorID] [bigint]  NULL, " +
                    " [CreatorName]   [varchar](50) NULL, " +
                    " [DataAddTime]  [datetime]  NULL, " +
                    " [DataUpdateTime] [datetime]  NULL, " +
                    " [DataTimeStamp] [timestamp]  NULL, " +
                    " CONSTRAINT[PK_E_Attachment] PRIMARY KEY CLUSTERED " +
                    " ( " +
                    "    [EAttachmentID] ASC " +
                    " )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] " +
                    " ) ON[PRIMARY] " +
                    " end";
                    listSQL.Add(updateSql);

                    updateSql = "ALTER TABLE[dbo].[E_Attachment]  WITH CHECK ADD CONSTRAINT[FK_E_Attachment_F_File] FOREIGN KEY([TempletID]) REFERENCES[dbo].[E_Templet] ([TempletID])";
                    listSQL.Add(updateSql);

                    updateSql = "ALTER TABLE[dbo].[E_Attachment]  CHECK CONSTRAINT[FK_E_Attachment_F_File]";
                    listSQL.Add(updateSql);

                    updateSql = "ALTER TABLE[dbo].[E_Attachment] WITH CHECK ADD CONSTRAINT[FK_E_Attachment_HR_Employee] FOREIGN KEY([CreatorID]) REFERENCES[dbo].[HR_Employee] ([EmpID])";
                    listSQL.Add(updateSql);

                    updateSql = "ALTER TABLE[dbo].[E_Attachment] CHECK CONSTRAINT[FK_E_Attachment_HR_Employee]";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'实验室ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'E_Attachment\', @level2type = N\'COLUMN\', @level2name = N\'LabID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'文件附件主键ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'E_Attachment\', @level2type = N\'COLUMN\', @level2name = N\'EAttachmentID\'";
                    listSQL.Add(updateSql);

                    updateSql = " sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'模板主键ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'E_Attachment\', @level2type = N\'COLUMN\', @level2name = N\'TempletID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'模板类型名称\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'E_Attachment\', @level2type = N\'COLUMN\', @level2name = N\'TempletType\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'模板类型编码\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'E_Attachment\', @level2type = N\'COLUMN\', @level2name = N\'TempletTypeCode\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'文件名\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'E_Attachment\', @level2type = N\'COLUMN\', @level2name = N\'FileName\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'文件扩展名\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'E_Attachment\', @level2type = N\'COLUMN\', @level2name = N\'FileExt\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'文件大小\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'E_Attachment\', @level2type = N\'COLUMN\', @level2name = N\'FileSize\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'文件路径\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'E_Attachment\', @level2type = N\'COLUMN\', @level2name = N\'FilePath\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'文件自定义名称\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'E_Attachment\', @level2type = N\'COLUMN\', @level2name = N\'FileNewName\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'文档上传日期\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'E_Attachment\', @level2type = N\'COLUMN\', @level2name = N\'FileUploadDate\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'文件内容类型\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'E_Attachment\', @level2type = N\'COLUMN\', @level2name = N\'FileType\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'备注\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'E_Attachment\', @level2type = N\'COLUMN\', @level2name = N\'Memo\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'是否使用\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'E_Attachment\', @level2type = N\'COLUMN\', @level2name = N\'IsUse\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'创建者\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'E_Attachment\', @level2type = N\'COLUMN\', @level2name = N\'CreatorID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'创建者姓名\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'E_Attachment\', @level2type = N\'COLUMN\', @level2name = N\'CreatorName\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'创建时间\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'E_Attachment\', @level2type = N\'COLUMN\', @level2name = N\'DataAddTime\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'数据修改时间\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'E_Attachment\', @level2type = N\'COLUMN\', @level2name = N\'DataUpdateTime\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'时间戳\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'E_Attachment\', @level2type = N\'COLUMN\', @level2name = N\'DataTimeStamp\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'文档附件表\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'E_Attachment\'";
                    listSQL.Add(updateSql);

                    result = ExecuteUpdateSQL(listSQL);

                    if (result)
                        result = UpateCompareVersionInfo("1.0.0.4");
                    else
                        ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.4) Update Error, Please Check The Log!");
                }
            }
            #endregion
            #region 1.0.0.5
            if (IsUpdateDataBase(oldVersion, "1.0.0.5"))
            {
                if (!CheckDataObjectIsExists("AH_SingleLicence", "U"))
                {
                    List<string> listSQL = new List<string>();
                    updateSql = " if exists (select 1 from sysobjects where id = object_id('AH_SingleLicence') and type = 'U') drop table AH_SingleLicence  CREATE TABLE [dbo].[AH_SingleLicence]( [LabID] [dbo].[D_系统主键] NULL, [PClientID] [dbo].[D_系统主键] NULL, [PClientName] [nvarchar](100) NULL, [SingleLicenceID] [dbo].[D_系统主键] NOT NULL, [PContractID] [dbo].[D_系统主键] NULL, [ProgramID] [dbo].[D_系统主键] NULL, [ProgramName] [nvarchar](50) NULL, [EquipID] [bigint] NULL, [EquipName] [nvarchar](100) NULL, [LicenceTypeId] [dbo].[D_系统主键] NULL, [SQH] [nvarchar](20) NULL, [LicenceKey] [nvarchar](50) NULL, [MacAddress] [nvarchar](50) NULL, [StartDate] [datetime] NULL, [EndDate] [datetime] NULL, [Status] [bigint] NULL, [ApplyID] [bigint] NULL, [ApplyName] [nvarchar](50) NULL, [ApplyDataTime] [datetime] NULL, [ApplyInfo] [varchar](300) NULL, [OneAuditID] [bigint] NULL, [OneAuditName] [nvarchar](50) NULL, [OneAuditDataTime] [datetime] NULL, [OneAuditInfo] [varchar](300) NULL, [TwoAuditID] [bigint] NULL, [TwoAuditName] [nvarchar](50) NULL, [TwoAuditDataTime] [datetime] NULL, [TwoAuditInfo] [varchar](300) NULL, [GenDateTime] [datetime] NULL, [Comment] [varchar](300) NULL, [IsUse] [bit] NULL, [DataAddTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_AH_SINGLELICENCE] PRIMARY KEY CLUSTERED ( [SingleLicenceID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] SET ANSI_PADDING OFF EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台客户ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_SingleLicence', @level2type=N'COLUMN',@level2name=N'LabID' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_SingleLicence', @level2type=N'COLUMN',@level2name=N'PClientID' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户名称', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_SingleLicence', @level2type=N'COLUMN',@level2name=N'PClientName' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单站点授权ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_SingleLicence', @level2type=N'COLUMN',@level2name=N'SingleLicenceID' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_SingleLicence', @level2type=N'COLUMN',@level2name=N'PContractID' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'程序主键ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_SingleLicence', @level2type=N'COLUMN',@level2name=N'ProgramID' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仪器ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_SingleLicence', @level2type=N'COLUMN',@level2name=N'EquipID' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请人ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_SingleLicence', @level2type=N'COLUMN',@level2name=N'ApplyID' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请人名称', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_SingleLicence', @level2type=N'COLUMN',@level2name=N'ApplyName' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请备注', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_SingleLicence', @level2type=N'COLUMN',@level2name=N'ApplyInfo' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一审人ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_SingleLicence', @level2type=N'COLUMN',@level2name=N'OneAuditID' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一审人名称', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_SingleLicence', @level2type=N'COLUMN',@level2name=N'OneAuditName' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一审时间', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_SingleLicence', @level2type=N'COLUMN',@level2name=N'OneAuditDataTime' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一审意见', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_SingleLicence', @level2type=N'COLUMN',@level2name=N'OneAuditInfo' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'特殊审批人ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_SingleLicence', @level2type=N'COLUMN',@level2name=N'TwoAuditID' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'特殊审批人名称', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_SingleLicence', @level2type=N'COLUMN',@level2name=N'TwoAuditName' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'特殊审批时间', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_SingleLicence', @level2type=N'COLUMN',@level2name=N'TwoAuditDataTime' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'特殊审批意见', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_SingleLicence', @level2type=N'COLUMN',@level2name=N'TwoAuditInfo' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权时间', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_SingleLicence', @level2type=N'COLUMN',@level2name=N'GenDateTime' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_SingleLicence', @level2type=N'COLUMN',@level2name=N'Comment' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_SingleLicence', @level2type=N'COLUMN',@level2name=N'IsUse' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_SingleLicence', @level2type=N'COLUMN',@level2name=N'DataAddTime' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_SingleLicence', @level2type=N'COLUMN',@level2name=N'DataTimeStamp' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单站点授权授权流程Status：1暂存、2申请、3授权中、4商务授权通过、5商务授权退回、6特批授权中、7特批授权通过、8特批授权退回、9授权完成授权类型：1商业、2评估、3测试、4临时授权状态：1有效、2警告（10天内到期）、3失效', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_SingleLicence'";
                    listSQL.Add(updateSql);

                    result = ExecuteUpdateSQL(listSQL);

                    if (result)
                        result = UpateCompareVersionInfo("1.0.0.5");
                    else
                        ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.5) Update Error, Please Check The Log!");
                }
            }
            #endregion
            #region 1.0.0.6
            if (IsUpdateDataBase(oldVersion, "1.0.0.6"))
            {
                List<string> listSQL = new List<string>();
                //添加服务器程序授权明细表
                if (!CheckDataObjectIsExists("AH_ServerProgramLicence", "U"))
                {
                    updateSql = " if exists (select 1 from sysobjects where id = object_id('AH_ServerProgramLicence') and type = 'U') drop table AH_ServerProgramLicence CREATE TABLE [dbo].[AH_ServerProgramLicence]( [LabID] [dbo].[D_系统主键] NULL, [ServerProgramLicenceID] [dbo].[D_系统主键] NOT NULL, [ProgramID] [dbo].[D_系统主键] NULL, [ProgramName] [nvarchar](50) NULL, [SNo] [bigint] NOT NULL, [LicenceTypeId] [dbo].[D_系统主键] NULL, [SQH] [nvarchar](20) NULL, [LicenceKey] [nvarchar](50) NULL, [LicenceDate] [datetime] NULL, [Comment] [varchar](300) NULL, [IsUse] [bit] NULL, [DataAddTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, [DispOrder] [int] NULL, CONSTRAINT [PK_AH_SERVERPROGRAMLICENCE] PRIMARY KEY CLUSTERED ( [ServerProgramLicenceID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] SET ANSI_PADDING OFF EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台客户ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerProgramLicence', @level2type=N'COLUMN',@level2name=N'LabID' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'程序主键ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerProgramLicence', @level2type=N'COLUMN',@level2name=N'ProgramID' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'程序名称', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerProgramLicence', @level2type=N'COLUMN',@level2name=N'ProgramName' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权类型', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerProgramLicence', @level2type=N'COLUMN',@level2name=N'LicenceTypeId' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权号', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerProgramLicence', @level2type=N'COLUMN',@level2name=N'SQH' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权Key', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerProgramLicence', @level2type=N'COLUMN',@level2name=N'LicenceKey' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerProgramLicence', @level2type=N'COLUMN',@level2name=N'Comment' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerProgramLicence', @level2type=N'COLUMN',@level2name=N'IsUse' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerProgramLicence', @level2type=N'COLUMN',@level2name=N'DataAddTime' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerProgramLicence', @level2type=N'COLUMN',@level2name=N'DataTimeStamp' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示次序', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerProgramLicence', @level2type=N'COLUMN',@level2name=N'DispOrder' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务器程序授权明细', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerProgramLicence' ";
                    listSQL.Add(updateSql);
                }
                //添加服务器授权表
                if (!CheckDataObjectIsExists("AH_ServerLicence", "U"))
                {
                    updateSql = " if exists (select 1 from sysobjects where id = object_id('AH_ServerLicence') and type = 'U') drop table AH_ServerLicence CREATE TABLE [dbo].[AH_ServerLicence]( [LabID] [dbo].[D_系统主键] NULL, [ServerLicenceID] [dbo].[D_系统主键] NOT NULL, [PClientID] [dbo].[D_系统主键] NULL, [PClientName] [nvarchar](100) NULL, [PContractID] [dbo].[D_系统主键] NULL, [LicenceKey1] [nvarchar](30) NULL, [LRNo1] [char](10) NULL, [LicenceKey2] [char](10) NULL, [LRNo2] [char](10) NULL, [Status] [bigint] NULL, [ApplyID] [bigint] NULL, [ApplyName] [nvarchar](50) NULL, [ApplyDataTime] [datetime] NULL, [ApplyInfo] [varchar](300) NULL, [OneAuditID] [bigint] NULL, [OneAuditName] [nvarchar](50) NULL, [OneAuditDataTime] [datetime] NULL, [OneAuditInfo] [varchar](300) NULL, [TwoAuditID] [bigint] NULL, [TwoAuditName] [nvarchar](50) NULL, [TwoAuditDataTime] [datetime] NULL, [TwoAuditInfo] [varchar](300) NULL, [GenDateTime] [datetime] NULL, [Comment] [varchar](300) NULL, [IsUse] [bit] NULL, [DataAddTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_AH_ServerLicence] PRIMARY KEY CLUSTERED ( [ServerLicenceID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] SET ANSI_PADDING OFF EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台客户ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'LabID' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务器授权ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'ServerLicenceID' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'PClientID' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户名称', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'PClientName' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'PContractID' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主服务器授权Key', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'LicenceKey1' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主服务器授权申请号', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'LRNo1' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备份服务器授权Key', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'LicenceKey2' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备份服务器授权申请号', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'LRNo2' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流程状态', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'Status' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请人ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'ApplyID' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请人名称', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'ApplyName' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请备注', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'ApplyInfo' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一审人ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'OneAuditID' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一审人名称', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'OneAuditName' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一审时间', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'OneAuditDataTime' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'一审意见', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'OneAuditInfo' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'特殊审批人ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'TwoAuditID' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'特殊审批人名称', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'TwoAuditName' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'特殊审批时间', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'TwoAuditDataTime' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'特殊审批意见', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'TwoAuditInfo' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权时间', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'GenDateTime' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'Comment' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'IsUse' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'DataAddTime' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence', @level2type=N'COLUMN',@level2name=N'DataTimeStamp' EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务器授权', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerLicence' ";
                    listSQL.Add(updateSql);
                }
                //添加服务器仪器授权信息表
                if (!CheckDataObjectIsExists("AH_ServerEquipLicence", "U"))
                {
                    updateSql = " if exists (select 1 from sysobjects where id = object_id('AH_ServerEquipLicence') and type = 'U') drop table AH_ServerEquipLicence CREATE TABLE [dbo].[AH_ServerEquipLicence]( [LabID] [dbo].[D_系统主键] NULL, [ServerEquipLicenceID] [dbo].[D_系统主键] NOT NULL, [EquipID] [bigint] NULL, [EquipName] [nvarchar](100) NULL, [SNo] [bigint] NOT NULL, [LicenceTypeId] [dbo].[D_系统主键] NULL, [SQH] [nvarchar](20) NULL, [LicenceKey] [nvarchar](50) NULL, [LicenceDate] [datetime] NULL, [Comment] [varchar](300) NULL, [IsUse] [bit] NULL, [DataAddTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, [DispOrder] [int] NULL, CONSTRAINT [PK_AH_ServerEquipLicence] PRIMARY KEY CLUSTERED ( [ServerEquipLicenceID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]  SET ANSI_PADDING OFF  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台客户ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerEquipLicence', @level2type=N'COLUMN',@level2name=N'LabID'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仪器ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerEquipLicence', @level2type=N'COLUMN',@level2name=N'EquipID'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仪器名称', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerEquipLicence', @level2type=N'COLUMN',@level2name=N'EquipName'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权类型', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerEquipLicence', @level2type=N'COLUMN',@level2name=N'LicenceTypeId'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权号', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerEquipLicence', @level2type=N'COLUMN',@level2name=N'SQH'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权Key', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerEquipLicence', @level2type=N'COLUMN',@level2name=N'LicenceKey'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'截至日期', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerEquipLicence', @level2type=N'COLUMN',@level2name=N'LicenceDate'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerEquipLicence', @level2type=N'COLUMN',@level2name=N'Comment'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerEquipLicence', @level2type=N'COLUMN',@level2name=N'IsUse'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerEquipLicence', @level2type=N'COLUMN',@level2name=N'DataAddTime'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerEquipLicence', @level2type=N'COLUMN',@level2name=N'DataTimeStamp'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示次序', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerEquipLicence', @level2type=N'COLUMN',@level2name=N'DispOrder'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务器仪器授权', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_ServerEquipLicence'  ";
                    listSQL.Add(updateSql);
                }
                //添加授权记录表
                if (!CheckDataObjectIsExists("AH_Operation", "U"))
                {
                    updateSql = " if exists (select 1 from sysobjects where id = object_id('AH_Operation') and type = 'U') drop table AH_Operation CREATE TABLE [dbo].[AH_Operation]( [LabID] [dbo].[D_系统主键] NOT NULL, [AHOperationID] [bigint] NOT NULL, [BobjectID] [dbo].[D_系统主键] NOT NULL, [Type] [bigint] NULL, [TypeName] [varchar](20) NULL, [BusinessModuleCode] [varchar](20) NULL, [Memo] [varchar](500) NULL, [DispOrder] [int] NULL, [IsUse] [bit] NULL, [CreatorID] [dbo].[D_系统主键] NULL, [CreatorName] [varchar](50) NULL, [DataAddTime] [datetime] NULL, [DataUpdateTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_AH_Operation] PRIMARY KEY CLUSTERED ( [AHOperationID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]  SET ANSI_PADDING OFF  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实验室ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_Operation', @level2type=N'COLUMN',@level2name=N'LabID'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权操作记录主键ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_Operation', @level2type=N'COLUMN',@level2name=N'AHOperationID'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务对象ID', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_Operation', @level2type=N'COLUMN',@level2name=N'BobjectID'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作类型', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_Operation', @level2type=N'COLUMN',@level2name=N'Type'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作类型名', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_Operation', @level2type=N'COLUMN',@level2name=N'TypeName'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'业务对象代码', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_Operation', @level2type=N'COLUMN',@level2name=N'BusinessModuleCode'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_Operation', @level2type=N'COLUMN',@level2name=N'Memo'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_Operation', @level2type=N'COLUMN',@level2name=N'IsUse'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_Operation', @level2type=N'COLUMN',@level2name=N'CreatorID'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建者姓名', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_Operation', @level2type=N'COLUMN',@level2name=N'CreatorName'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_Operation', @level2type=N'COLUMN',@level2name=N'DataAddTime'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据修改时间', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_Operation', @level2type=N'COLUMN',@level2name=N'DataUpdateTime'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_Operation', @level2type=N'COLUMN',@level2name=N'DataTimeStamp'  EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权操作记录', @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AH_Operation'   ";
                    listSQL.Add(updateSql);
                }
                //程序信息表添加SQH列
                updateSql = " IF COL_LENGTH('PGM_Program', 'SQH') IS NOT NULL   PRINT N'PGM_Program已存在SQH'  ELSE  alter table PGM_Program add SQH varchar(100) ";
                listSQL.Add(updateSql);
                //'字典表添加导出帮助文档记录行'
                //'系统参数表添加导出帮助文档记录行'
                updateSql = " if exists (select * from P_Dict where DictID=4784659538853660916) print '导出帮助文档记录行已经存在字典表' else INSERT [P_Dict] ([DictID],[LabID],[DictTypeID],[Shortcode],[CName],[DispOrder],[IsUse],[DataAddTime]) VALUES ( 4784659538853660916,1,4658943850913198560,N'SaveHelpHtmlAndJson',N'导出帮助文档保存路径',9,1,N'2016/12/12 12:55:33') if exists (select * from B_Parameter where ParameterID=5534894644551449999) print '导出帮助文档记录行已经存在系统参数表' else INSERT [B_Parameter] ([LabID],[ParameterID],[PDictId],[Name],[ParaType],[ParaNo],[ParaValue],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 1,5534894644551449999,4784659538853660916,N'导出帮助文档保存路径',N'SYS',N'SaveHelpHtmlAndJson','',0,1,0,N'2016/12/12 12:56:27') ";
                listSQL.Add(updateSql);
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.6");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.6) Update Error, Please Check The Log!");

            }
            #endregion
            #region 1.0.0.7
            if (IsUpdateDataBase(oldVersion, "1.0.0.7"))
            {
                List<string> listSQL = new List<string>();

                //--服务器授权添加当前服务器授权Key列
                updateSql = " IF COL_LENGTH('AH_ServerLicence', 'LicenceKey') IS NULL ALTER TABLE AH_ServerLicence ADD LicenceKey varchar(30) ";
                listSQL.Add(updateSql);

                //--服务器授权添加当前服务器授权申请号列
                updateSql = " IF COL_LENGTH('AH_ServerLicence', 'LRNo') IS NULL alter table AH_ServerLicence add LRNo varchar(30) ";
                listSQL.Add(updateSql);

                //--服务器程序授权明细添加服务器授权ID列
                updateSql = " IF COL_LENGTH('AH_ServerProgramLicence', 'ServerLicenceID') IS NULL alter table AH_ServerProgramLicence add ServerLicenceID [dbo].[D_系统主键] NOT NULL ";
                listSQL.Add(updateSql);

                //--服务器仪器授权添加服务器授权ID列
                updateSql = " IF COL_LENGTH('AH_ServerEquipLicence', 'ServerLicenceID') IS NULL alter table AH_ServerEquipLicence add ServerLicenceID [dbo].[D_系统主键] NOT NULL ";
                listSQL.Add(updateSql);

                //--服务器仪器授权明细添加程序名列
                updateSql = " IF COL_LENGTH('AH_ServerEquipLicence', 'ProgramName') IS NULL alter table AH_ServerEquipLicence add ProgramName varchar(50) ";
                listSQL.Add(updateSql);

                //--服务器程序授权明细添加站点名称列
                updateSql = " IF COL_LENGTH('AH_ServerProgramLicence', 'NodeTableName') IS NULL alter table AH_ServerProgramLicence add NodeTableName varchar(50) ";
                listSQL.Add(updateSql);

                //--服务器仪器授权明细添加站点名称列
                updateSql = " IF COL_LENGTH('AH_ServerEquipLicence', 'NodeTableName') IS NULL alter table AH_ServerEquipLicence add NodeTableName varchar(50) ";
                listSQL.Add(updateSql);

                //--服务器仪器授权添加外键关系FK_AH_ServerEquipLicence_AH_ServerLicence
                updateSql = " if exists(select 1 from sysobjects where name= 'FK_AH_ServerEquipLicence_AH_ServerLicence ' and xtype= 'F ') print 'FK_AH_ServerEquipLicence_AH_ServerLicence已存在' else ALTER TABLE [dbo].[AH_ServerEquipLicence] WITH CHECK ADD CONSTRAINT [FK_AH_ServerEquipLicence_AH_ServerLicence] FOREIGN KEY([ServerLicenceID]) REFERENCES [dbo].[AH_ServerLicence] ([ServerLicenceID]) ALTER TABLE [dbo].[AH_ServerEquipLicence] CHECK CONSTRAINT [FK_AH_ServerEquipLicence_AH_ServerLicence] ";
                listSQL.Add(updateSql);

                //--服务器程序明细添加外键关系FK_AH_ServerProgramLicence_AH_ServerLicence
                updateSql = " if exists(select 1 from sysobjects where name= 'FK_AH_ServerProgramLicence_AH_ServerLicence ' and xtype= 'F ') print 'FK_AH_ServerProgramLicence_AH_ServerLicence已存在' else ALTER TABLE [dbo].[AH_ServerProgramLicence] WITH CHECK ADD CONSTRAINT [FK_AH_ServerProgramLicence_AH_ServerLicence] FOREIGN KEY([ServerLicenceID]) REFERENCES [dbo].[AH_ServerLicence] ([ServerLicenceID]) ALTER TABLE [dbo].[AH_ServerProgramLicence] CHECK CONSTRAINT [FK_AH_ServerProgramLicence_AH_ServerLicence] ";
                listSQL.Add(updateSql);

                //--服务器授权添加特殊审批标记列
                updateSql = " IF COL_LENGTH('AH_ServerLicence', 'IsSpecially') IS NULL ALTER TABLE AH_ServerLicence ADD IsSpecially bit ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.7");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.7) Update Error, Please Check The Log!");

            }
            #endregion
            #region 1.0.0.8
            if (IsUpdateDataBase(oldVersion, "1.0.0.8"))
            {
                List<string> listSQL = new List<string>();
                //--服务器仪器授权明细添加系统程序名称(仪器型号)列
                updateSql = " IF COL_LENGTH('AH_ServerEquipLicence', 'Equipversion') IS NULL alter table AH_ServerEquipLicence add Equipversion varchar(50) ";
                listSQL.Add(updateSql);

                //--修改服务器程序授权明细程序名称长度为100
                updateSql = " IF COL_LENGTH('AH_ServerProgramLicence', 'ProgramName') IS NOT NULL alter table AH_ServerProgramLicence alter column ProgramName varchar(100) ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.8");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.8) Update Error, Please Check The Log!");

            }
            #endregion
            #region 1.0.0.9
            if (IsUpdateDataBase(oldVersion, "1.0.0.9"))
            {
                List<string> listSQL = new List<string>();
                //--服务器仪器授权明细添加系统程序名称(仪器型号)列
                updateSql = " IF COL_LENGTH('P_Contract', 'EquipOneWayCount') IS NULL ALTER TABLE P_Contract ADD EquipOneWayCount int ";
                listSQL.Add(updateSql);

                //--修改服务器程序授权明细程序名称长度为100
                updateSql = " IF COL_LENGTH('P_Contract', 'EquipTwoWayCount') IS NULL ALTER TABLE P_Contract ADD EquipTwoWayCount int ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.9");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.9) Update Error, Please Check The Log!");

            }
            #endregion
            #region 1.0.0.10
            if (IsUpdateDataBase(oldVersion, "1.0.0.10"))
            {
                List<string> listSQL = new List<string>();
                //--单站点授权添加计划收款时间列
                updateSql = " IF COL_LENGTH('AH_SingleLicence', 'PlannReceiptDate') IS NULL ALTER TABLE AH_SingleLicence ADD PlannReceiptDate datetime ";
                listSQL.Add(updateSql);

                //--文档表添加是否同步到微信服务器列
                updateSql = " IF COL_LENGTH('F_File', 'IsSyncWeiXin') IS NULL ALTER TABLE F_File ADD IsSyncWeiXin bit ";
                listSQL.Add(updateSql);

                //--文档表添加微信内容列
                updateSql = " IF COL_LENGTH('F_File', 'WeiXinContent') IS NULL ALTER TABLE F_File ADD WeiXinContent varchar(max) ";
                listSQL.Add(updateSql);

                //--文档表添加新闻缩略图上传保存路径列
                updateSql = " IF COL_LENGTH('F_File', 'ThumbnailsPath') IS NULL ALTER TABLE F_File ADD ThumbnailsPath varchar(100) ";
                listSQL.Add(updateSql);

                //--文档表添加新闻缩略图描述列
                updateSql = " IF COL_LENGTH('F_File', 'ThumbnailsMemo') IS NULL ALTER TABLE F_File ADD ThumbnailsMemo varchar(500) ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.10");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.10) Update Error, Please Check The Log!");

            }
            #endregion
            #region 1.0.0.11
            if (IsUpdateDataBase(oldVersion, "1.0.0.11"))
            {
                List<string> listSQL = new List<string>();
                //--发票管理添加硬件金额
                updateSql = " IF COL_LENGTH('P_Invoice', 'HardwareAmount') IS NULL ALTER TABLE P_Invoice ADD HardwareAmount float ";
                listSQL.Add(updateSql);

                //--发票管理添加软件金额
                updateSql = " IF COL_LENGTH('P_Invoice', 'SoftwareAmount') IS NULL ALTER TABLE P_Invoice ADD SoftwareAmount float ";
                listSQL.Add(updateSql);

                //--发票管理添加服务金额
                updateSql = " IF COL_LENGTH('P_Invoice', 'ServerAmount') IS NULL ALTER TABLE P_Invoice ADD ServerAmount float ";
                listSQL.Add(updateSql);

                //--发票管理添加软件套数
                updateSql = " IF COL_LENGTH('P_Invoice', 'SoftwareCount') IS NULL ALTER TABLE P_Invoice ADD SoftwareCount float ";
                listSQL.Add(updateSql);

                //--发票管理添加硬件数量
                updateSql = " IF COL_LENGTH('P_Invoice', 'HardwareCount') IS NULL ALTER TABLE P_Invoice ADD HardwareCount float ";
                listSQL.Add(updateSql);

                //--发票管理添加本次开票金额占合同额百分比
                updateSql = " IF COL_LENGTH('P_Invoice', 'PercentAmount') IS NULL ALTER TABLE P_Invoice ADD PercentAmount varchar(50) ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.11");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.11) Update Error, Please Check The Log!");

            }
            #endregion
            #region 1.0.0.12
            if (IsUpdateDataBase(oldVersion, "1.0.0.12"))
            {
                List<string> listSQL = new List<string>();
                //--发票管理添加硬件金额
                updateSql = " IF COL_LENGTH('P_Client', 'LicenceCode') IS NULL ALTER TABLE P_Client ADD LicenceCode varchar(20) ";
                listSQL.Add(updateSql);

                //--发票管理添加软件金额
                updateSql = " IF COL_LENGTH('P_Client', 'LicenceKey1') IS NULL ALTER TABLE P_Client ADD LicenceKey1 nvarchar(30) ";
                listSQL.Add(updateSql);

                //--发票管理添加服务金额
                updateSql = " IF COL_LENGTH('P_Client', 'LRNo1') IS NULL ALTER TABLE P_Client ADD LRNo1 nvarchar(30) ";
                listSQL.Add(updateSql);

                //--发票管理添加软件套数
                updateSql = " IF COL_LENGTH('P_Client', 'LicenceKey2') IS NULL ALTER TABLE P_Client ADD LicenceKey2 nvarchar(30) ";
                listSQL.Add(updateSql);

                //--发票管理添加硬件数量
                updateSql = " IF COL_LENGTH('P_Client', 'LRNo2') IS NULL ALTER TABLE P_Client ADD LRNo2 nvarchar(30) ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.12");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.12) Update Error, Please Check The Log!");

            }
            #endregion
            #region 1.0.0.13
            if (IsUpdateDataBase(oldVersion, "1.0.0.13"))
            {
                List<string> listSQL = new List<string>();
                //--给文档表的IsSyncWeiXin更新设置默认值为0
                updateSql = " update[F_File] set[IsSyncWeiXin] = 0; ALTER TABLE[F_File] ADD DEFAULT ((0)) FOR[IsSyncWeiXin] ;";
                listSQL.Add(updateSql);

                //--文档表添加微信MEDIA_ID
                updateSql = " IF COL_LENGTH('F_File', 'Media_id') IS NULL ALTER TABLE F_File ADD Media_id nvarchar(100) ";
                listSQL.Add(updateSql);

                //--文档表添加微信缩略图Thumb_media_id
                updateSql = " IF COL_LENGTH('F_File', 'Thumb_media_id') IS NULL ALTER TABLE F_File ADD Thumb_media_id nvarchar(100) ";
                listSQL.Add(updateSql);

                //--文档表理添加微信Title
                updateSql = " IF COL_LENGTH('F_File', 'WeiXinTitle') IS NULL ALTER TABLE F_File ADD WeiXinTitle nvarchar(100) ";
                listSQL.Add(updateSql);

                //--文档表理添加微信Author
                updateSql = " IF COL_LENGTH('F_File', 'WeiXinAuthor') IS NULL ALTER TABLE F_File ADD WeiXinAuthor nvarchar(100) ";
                listSQL.Add(updateSql);

                //--文档表理添加微信Digest
                updateSql = " IF COL_LENGTH('F_File', 'WeiXinDigest') IS NULL ALTER TABLE F_File ADD WeiXinDigest nvarchar(100) ";
                listSQL.Add(updateSql);

                //--文档表理添加微信Url
                updateSql = " IF COL_LENGTH('F_File', 'WeiXinUrl') IS NULL ALTER TABLE F_File ADD WeiXinUrl nvarchar(200) ";
                listSQL.Add(updateSql);

                //--文档表理添加微信Content_source_url
                updateSql = " IF COL_LENGTH('F_File', 'WeiXinContent_source_url') IS NULL ALTER TABLE F_File ADD WeiXinContent_source_url nvarchar(2100) ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.13");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.13) Update Error, Please Check The Log!");

            }
            #endregion
            #region 1.0.0.14 QMS 升级质量记录审核存储过程
            if (IsUpdateDataBase(oldVersion, "1.0.0.14"))
            {
                List<string> listSQL = new List<string>();
                updateSql = " if exists(select * from dbo.sysobjects where id = object_id(N\'[dbo].[P_GetReportData]\') and OBJECTPROPERTY(id, N\'IsProcedure\') = 1)" + "\r" +
                            " DROP PROCEDURE[dbo].[P_GetReportData] ";
                listSQL.Add(updateSql);

                updateSql = " CREATE PROCEDURE[dbo].[P_GetReportData] " + "\r" +
                              " @EmpID varchar(50), " + "\r" +
                              " @BeginDate datetime, " + "\r" +
                              " @EndDate datetime " + "\r" +
                            " AS " + "\r" +
                            " BEGIN " + "\r" +
                                " declare  @PreMonth varchar(50) " + "\r" +
                                " declare  @CurMonth varchar(50) " + "\r" +
                                " set @CurMonth = CONVERT(varchar(8), @BeginDate, 120) + \'01\' " + "\r" +
                                " set @PreMonth = CONVERT(varchar(8), DATEADD(MONTH, -1, @BeginDate), 120) + \'01\' " + "\r" +
                                " select ROW_NUMBER() OVER(ORDER BY TempletID ASC) AS Id, * " + "\r" +
                                " from( " + "\r" +
                                "     select ETP.TempletID, ETP.ReportDate, ET.CName as ReportName, " + "\r" +
                                "      ERD.ReportDataID, ERD.LabID, ERD.ReportFilePath, ERD.ReportFileExt, ERD.IsCheck, " + "\r" +
                                "      ERD.Checker, ERD.CheckTime, ERD.CheckView, ERD.Comment, ERD.IsUse, " + "\r" +
                                "      ERD.DispOrder, ERD.DataAddTime, ERD.DataUpdateTime, EE.CName as EquipName, " + "\r" +
                                "      EE.UseCode as EUseCode, EE.Shortcode as EShortcode, EE.EName as EEName, " + "\r" +
                                "      PD.CName as EquipTypeName, ET.SectionID, HRD.CName as SectionName, " + "\r" +
                                "      HRD.UseCode, HRD.Shortcode, HRD.StandCode, HRD.EName " + "\r" +
                                "     from(select TempletID, EmpID, @PreMonth as ReportDate from E_TempletEmp where EmpID = @EmpID " + "\r" +
                                "            union " + "\r" +
                                "            select TempletID, EmpID, @CurMonth as ReportDate from E_TempletEmp where EmpID = @EmpID " + "\r" +
                                "          ) ETP " + "\r" +
                                "     left join E_ReportData ERD on ETP.TempletID = ERD.TempletID and ETP.ReportDate = ERD.ReportDate " + "\r" +
                                "     left join E_Templet ET on ET.TempletID = ETP.TempletID " + "\r" +
                                "     left join E_Equip EE on EE.EquipID = ET.EquipID " + "\r" +
                                "     left join P_Dict PD on EE.EquipTypeID = PD.DictID " + "\r" +
                                "     left join HR_Dept HRD on ET.SectionID = HRD.DeptID " + "\r" +
                                " ) MM  order by ReportDate, ReportName ASC " + "\r" +
                            " END ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.14");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.14) Update Error, Please Check The Log!");
            }
            #endregion
            #region 1.0.0.15 OA 授权文件升级
            if (IsUpdateDataBase(oldVersion, "1.0.0.15"))
            {
                List<string> listSQL = new List<string>();
                //--服务器授权删除LicenceKey1
                updateSql = " IF COL_LENGTH('AH_ServerLicence', 'LicenceKey1') IS NOT NULL ALTER TABLE AH_ServerLicence DROP COLUMN LicenceKey1 ";
                listSQL.Add(updateSql);

                //--服务器授权删除LicenceKey2
                updateSql = " IF COL_LENGTH('AH_ServerLicence', 'LicenceKey2') IS NOT NULL ALTER TABLE AH_ServerLicence DROP COLUMN LicenceKey2 ";
                listSQL.Add(updateSql);

                //--服务器程序授权明细添加主服务器授权Key
                updateSql = " IF COL_LENGTH('AH_ServerProgramLicence', 'LicenceKey1') IS NULL ALTER TABLE AH_ServerProgramLicence ADD LicenceKey1 nvarchar(50) ";
                listSQL.Add(updateSql);

                //--服务器程序授权明细添加备份服务器授权Key
                updateSql = " IF COL_LENGTH('AH_ServerProgramLicence', 'LicenceKey2') IS NULL ALTER TABLE AH_ServerProgramLicence ADD LicenceKey2 nvarchar(50) ";
                listSQL.Add(updateSql);

                //--服务器程序授权明细删除LicenceKey
                updateSql = " IF COL_LENGTH('AH_ServerProgramLicence', 'LicenceKey') IS NOT NULL ALTER TABLE AH_ServerProgramLicence DROP COLUMN LicenceKey ";
                listSQL.Add(updateSql);

                //--服务器授权删除LicenceKey
                updateSql = " IF COL_LENGTH('AH_ServerLicence', 'LicenceKey') IS NOT NULL ALTER TABLE AH_ServerLicence DROP COLUMN LicenceKey ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.15");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.15) Update Error, Please Check The Log!");
            }
            #endregion
            #region 1.0.0.16 //QMS升级质量记录新增列表存储过程和增加保存批号字段
            if (IsUpdateDataBase(oldVersion, "1.0.0.16"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " if Not Exists(Select * from SysColumns where [Name]= \'BatchNumber\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_MaintenanceData\' ))" + "\r" +
                            " Alter Table E_MaintenanceData Add BatchNumber varchar(50) ";
                listSQL.Add(updateSql);
                updateSql = " if exists(select * from dbo.sysobjects where id = object_id(N\'[dbo].[P_GetMaintenanceDataTB]\') and OBJECTPROPERTY(id, N\'IsProcedure\') = 1)" + "\r" +
                            " DROP PROCEDURE[dbo].[P_GetMaintenanceDataTB] ";
                listSQL.Add(updateSql);

                updateSql = " CREATE PROCEDURE[dbo].[P_GetMaintenanceDataTB] " + "\r" +
                             " @TempletID varchar(50), " + "\r" +
                             " @BatchNumber varchar(50), " + "\r" +
                             " @SQLPara varchar(2000), " + "\r" +
                             " @BeginDate varchar(50), " + "\r" +
                             " @EndDate varchar(50) " + "\r" +
                             " AS " + "\r" +
                             " BEGIN " + "\r" +
                             "   declare  @SQL varchar(5000) " + "\r" +
                             "   declare  @where varchar(200) " + "\r" +
                             "   set @SQL = \'\' " + "\r" +
                             "   set @where = \' where TempletID=\' + @TempletID " + "\r" +
                             "   if (@BatchNumber <> \'\') " + "\r" +
                             "     set @where = @where + \' and @BatchNumber=\'\'\' + @BatchNumber + \'\'\'\' " + "\r" +
                             "   set @SQL = \' Select TempletID,BatchNumber,ItemDate as \'\'操作日期\'\' \' + @SQLPara + " + "\r" +
                             "              \' from (Select * from E_MaintenanceData \' + @where + " + "\r" +
                             "              \' and ItemDate<\'\'\' + @EndDate + \'\'\'and ItemDate>=\'\'\' + @BeginDate + \'\'\') mm\' + " + "\r" +
                             "              \' group by TempletID,ItemDate,BatchNumber	\' " + "\r" +
                             "    exec(@sql) " + "\r" +
                            " END ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.16");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.16) Update Error, Please Check The Log!");
            }
            #endregion
            #region 1.0.0.17 ///QMS升级质量记录修改模板表的数据类型
            if (IsUpdateDataBase(oldVersion, "1.0.0.17"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " if Exists(Select * from SysColumns where [Name]= \'TempletStruct\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_Templet\' ))" + "\r" +
                            " alter table E_Templet alter column TempletStruct varchar(max) ";
                listSQL.Add(updateSql);

                updateSql = " if Exists(Select * from SysColumns where [Name]= \'TempletFillStruct\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_Templet\')) " + "\r" +
                            " alter table E_Templet alter column TempletFillStruct varchar(max) ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.17");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.17) Update Error, Please Check The Log!");
            }
            #endregion
            #region 1.0.0.18 程序发布修改
            if (IsUpdateDataBase(oldVersion, "1.0.0.18"))
            {
                List<string> listSQL = new List<string>();
                //--修改文档附件表文件FileName名称长度为500
                updateSql = " IF COL_LENGTH('F_File_Attachment', 'FileName') IS NOT NULL alter table F_File_Attachment alter column FileName varchar(500) ";
                listSQL.Add(updateSql);

                //--修改文档附件表NewFileName长度为500
                updateSql = " IF COL_LENGTH('F_File_Attachment', 'NewFileName') IS NOT NULL alter table F_File_Attachment alter column NewFileName varchar(500) ";
                listSQL.Add(updateSql);

                //--修改文档附件表FilePath长度为500
                updateSql = " IF COL_LENGTH('F_File_Attachment', 'FilePath') IS NOT NULL alter table F_File_Attachment alter column FilePath varchar(500) ";
                listSQL.Add(updateSql);

                //--修改程序信息表FileName长度为500
                updateSql = " IF COL_LENGTH('PGM_Program', 'FileName') IS NOT NULL alter table PGM_Program alter column FileName varchar(500) ";
                listSQL.Add(updateSql);

                //--修改程序信息表FilePath长度为500
                updateSql = " IF COL_LENGTH('PGM_Program', 'FilePath') IS NOT NULL alter table PGM_Program alter column FilePath varchar(500) ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.18");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.18) Update Error, Please Check The Log!");
            }
            #endregion
            #region 1.0.0.19 合同增加邮寄信息
            if (IsUpdateDataBase(oldVersion, "1.0.0.19"))
            {
                List<string> listSQL = new List<string>();
                updateSql = " ALTER TABLE P_Contract ADD ReceiveName varchar(100) NULL ;EXECUTE sp_addextendedproperty  N'MS_Description', N'收货人姓名', N'SCHEMA', N'dbo', N'TABLE', N'P_Contract', N'COLUMN', N'ReceiveName' ";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE P_Contract ADD ReceiveAddress varchar(200) NULL ;EXECUTE sp_addextendedproperty  N'MS_Description', N'收货人地址', N'SCHEMA', N'dbo', N'TABLE', N'P_Contract', N'COLUMN', N'ReceiveAddress' ";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE P_Contract ADD ReceivePhoneNumbers varchar(20) NULL ;EXECUTE sp_addextendedproperty  N'MS_Description', N'收货人电话', N'SCHEMA', N'dbo', N'TABLE', N'P_Contract', N'COLUMN', N'ReceivePhoneNumbers' ";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE P_Contract ADD FreightName varchar(200) NULL ;EXECUTE sp_addextendedproperty  N'MS_Description', N'货运公司名称', N'SCHEMA', N'dbo', N'TABLE', N'P_Contract', N'COLUMN', N'FreightName' ";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE P_Contract ADD FreightOddNumbers varchar(200) NULL ;EXECUTE sp_addextendedproperty  N'MS_Description', N'货运单号', N'SCHEMA', N'dbo', N'TABLE', N'P_Contract', N'COLUMN', N'FreightOddNumbers' ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.19");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.19) Update Error, Please Check The Log!");
            }
            #endregion
            #region 1.0.0.20 合同增加邮寄信息
            if (IsUpdateDataBase(oldVersion, "1.0.0.20"))
            {
                List<string> listSQL = new List<string>();
                updateSql = " IF COL_LENGTH('SC_Interaction', 'IsCommunication') IS NULL ALTER TABLE SC_Interaction ADD IsCommunication bit ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('SC_Interaction', 'LastReplyDateTime') IS NULL ALTER TABLE SC_Interaction ADD LastReplyDateTime datetime ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('SC_Interaction', 'ReplyCount') IS NULL ALTER TABLE SC_Interaction ADD ReplyCount bigint ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.20");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.20) Update Error, Please Check The Log!");
            }
            #endregion
            #region 1.0.0.21 新增项目表
            if (IsUpdateDataBase(oldVersion, "1.0.0.21"))
            {
                List<string> listSQL = new List<string>();
                updateSql = " CREATE TABLE [dbo].[P_Project]( [LabID] [dbo].[D_系统主键] NULL, [ProjectID] [dbo].[D_系统主键] NOT NULL, [CName] [varchar](100) NULL, [PContractID] [dbo].[D_系统主键] NULL, [PContractName] [varchar](100) NULL, [PClientID] [dbo].[D_系统主键] NULL, [PClientName] [varchar](100) NULL, [PhaseID] [dbo].[D_系统主键] NULL, [PhaseName] [varchar](50) NULL, [RiskLevelID] [dbo].[D_系统主键] NULL, [RiskLevelName] [varchar](50) NULL, [PaceID] [dbo].[D_系统主键] NULL, [PaceName] [varchar](50) NULL, [DynamicRiskLevelID] [dbo].[D_系统主键] NULL, [DynamicRiskLevelName] [varchar](50) NULL, [DelayLevelID] [dbo].[D_系统主键] NULL, [DelayLevelName] [varchar](50) NULL, [EntryTime] [datetime] NULL, [SignDate] [datetime] NULL, [PrincipalID] [dbo].[D_系统主键] NULL, [Principal] [varchar](50) NULL, [ProjectLeaderID] [dbo].[D_系统主键] NULL, [ProjectLeader] [varchar](50) NULL, [ProjectExecID] [dbo].[D_系统主键] NULL, [ProjectExec] [varchar](50) NULL, [PhaseManagerID] [dbo].[D_系统主键] NULL, [PhaseManager] [varchar](50) NULL, [EstiStartTime] [datetime] NULL, [EstiEndTime] [datetime] NULL, [StartTime] [datetime] NULL, [EndTime] [datetime] NULL, [AcceptTime] [datetime] NULL, [EstiAllDays] [float] NULL, [DynamicRemainingWorkDays] [float] NULL, [AllDays] [float] NULL, [ScheduleDelayPercent] [float] NULL, [ScheduleDelayDays] [float] NULL, [MoreWorkPercent] [float] NULL, [MoreWorkDays] [float] NULL, [CreatEmpIdID] [dbo].[D_系统主键] NULL, [DataAddTime] [datetime] NULL,[OtherMsg] [varchar](max) NULL, [ExtraMsg] [varchar](max) NULL,[IsUse] [bit] NULL, [Memo] [varchar](500) NULL,[Contents] [varchar](max) NULL,[DataTimeStamp] [timestamp] NULL, [ReqEndTime] [datetime] NULL,[EstiWorkload] [float] NULL, [Workload] [float] NULL,[TeamworkEvalID] [dbo].[D_系统主键] NULL, [TeamworkEvalName] [varchar](50) NULL,[PaceEvalID] [dbo].[D_系统主键] NULL, [PaceEvalName] [varchar](50) NULL,[EfficiencyEvalID] [dbo].[D_系统主键] NULL, [EfficiencyEvalName] [varchar](50) NULL,[QualityEvalID] [dbo].[D_系统主键] NULL, [QualityEvalName] [varchar](50) NULL,[TotalityEvalID] [dbo].[D_系统主键] NULL, [TotalityEvalName] [varchar](50) NULL,[UrgencyID] [dbo].[D_系统主键] NULL, [UrgencyName] [varchar](50) NULL,[TypeID] [dbo].[D_系统主键] NULL, [SubCount] [int] NULL,[InteractionCount] [bigint] NULL, [OperLogCount] [bigint] NULL,[WorkLogCount] [bigint] NULL, CONSTRAINT [PK_P_Project] PRIMARY KEY CLUSTERED ([ProjectID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台客户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'LabID'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'ProjectID'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'CName'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'PContractID'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'PContractName'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'PClientID'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'PClientName'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目阶段ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'PhaseID'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目阶段名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'PhaseName'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目风险等级ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'RiskLevelID'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目风险等级名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'RiskLevelName'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目进度ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'PaceID'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目进度名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'PaceName'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动态评估风险等级ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'DynamicRiskLevelID'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动态评估风险等级名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'DynamicRiskLevelName'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'延期程度ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'DelayLevelID'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'延期程度名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'DelayLevelName'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'进场时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'EntryTime'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'签署时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'SignDate'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'销售负责人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'PrincipalID'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目负责人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'ProjectLeaderID'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实施人员ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'ProjectExecID'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实施人员' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'ProjectExec'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'进度管理人员ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'PhaseManagerID'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'进度管理人员' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'PhaseManager'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'EstiStartTime'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'EstiEndTime'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实际开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'StartTime'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实际结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'EndTime'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实际验收时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'AcceptTime';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划人工作量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'EstiAllDays'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'动态剩余工作量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'DynamicRemainingWorkDays'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实际人工作量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'AllDays'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'进度延期百分比' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'ScheduleDelayPercent'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'进度延期天数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'ScheduleDelayDays';EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作量超百分比' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'MoreWorkPercent'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作量超天数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'MoreWorkDays'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'CreatEmpIdID'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'DataAddTime'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'其他信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'OtherMsg'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'附加信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'ExtraMsg'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'IsUse'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'Memo'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'Contents'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'DataTimeStamp'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'预计工作量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'EstiWorkload'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实际工作量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'Workload'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'协作评估' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'TeamworkEvalID'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'进度评估' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'PaceEvalID'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'效率评估' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'EfficiencyEvalID'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'质量评估' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'QualityEvalID'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'总体评估' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'TotalityEvalID'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'紧急程度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'UrgencyID'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目类别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'TypeID'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'子任务数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project', @level2type=N'COLUMN',@level2name=N'SubCount'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Project'; ";
                listSQL.Add(updateSql);

                updateSql = "ALTER TABLE [dbo].[P_Project] WITH CHECK ADD CONSTRAINT [FK_P_Project_P_Client] FOREIGN KEY([PClientID]) REFERENCES [dbo].[P_Client] ([PClientID]); ALTER TABLE [dbo].[P_Project] CHECK CONSTRAINT [FK_P_Project_P_Client]; ALTER TABLE [dbo].[P_Project] WITH CHECK ADD CONSTRAINT [FK_P_Project_P_Contract] FOREIGN KEY([PContractID]) REFERENCES [dbo].[P_Contract] ([PContractID]); ALTER TABLE [dbo].[P_Project] CHECK CONSTRAINT [FK_P_Project_P_Contract]; ALTER TABLE [dbo].[P_Project] WITH CHECK ADD CONSTRAINT [FK_F_P_Project_HR_Principal] FOREIGN KEY([PrincipalID])REFERENCES [dbo].[HR_Employee] ([EmpID]);ALTER TABLE [dbo].[P_Project] CHECK CONSTRAINT [FK_F_P_Project_HR_Principal]; ALTER TABLE [dbo].[P_Project] WITH CHECK ADD CONSTRAINT [FK_F_P_Project_HR_ProjectLeader] FOREIGN KEY([ProjectLeaderID])REFERENCES [dbo].[HR_Employee] ([EmpID]);ALTER TABLE [dbo].[P_Project] CHECK CONSTRAINT [FK_F_P_Project_HR_ProjectLeader]; ALTER TABLE [dbo].[P_Project] WITH CHECK ADD CONSTRAINT [FK_F_P_Project_HR_ProjectExec] FOREIGN KEY([ProjectExecID])REFERENCES [dbo].[HR_Employee] ([EmpID]);ALTER TABLE [dbo].[P_Project] CHECK CONSTRAINT [FK_F_P_Project_HR_ProjectExec]; ALTER TABLE [dbo].[P_Project] WITH CHECK ADD CONSTRAINT [FK_F_P_Project_HR_PhaseManager] FOREIGN KEY([PhaseManagerID])REFERENCES [dbo].[HR_Employee] ([EmpID]);ALTER TABLE [dbo].[P_Project] CHECK CONSTRAINT [FK_F_P_Project_HR_PhaseManager]; ALTER TABLE [dbo].[P_Project] WITH CHECK ADD CONSTRAINT [FK_F_P_Project_HR_CreatEmpId] FOREIGN KEY([CreatEmpIdID])REFERENCES [dbo].[HR_Employee] ([EmpID]);ALTER TABLE [dbo].[P_Project] CHECK CONSTRAINT [FK_F_P_Project_HR_CreatEmpId]; ALTER TABLE [dbo].[P_Project] WITH CHECK ADD CONSTRAINT [FK_P_Project_P_Dict_Phase] FOREIGN KEY([PhaseID])REFERENCES [dbo].[P_Dict] ([DictID]);ALTER TABLE [dbo].[P_Project] CHECK CONSTRAINT [FK_P_Project_P_Dict_Phase]; ALTER TABLE [dbo].[P_Project] WITH CHECK ADD CONSTRAINT [FK_P_Project_P_Dict_RiskLevel] FOREIGN KEY([RiskLevelID])REFERENCES [dbo].[P_Dict] ([DictID]);ALTER TABLE [dbo].[P_Project] CHECK CONSTRAINT [FK_P_Project_P_Dict_RiskLevel]; ALTER TABLE [dbo].[P_Project] WITH CHECK ADD CONSTRAINT [FK_P_Project_P_Dict_Pace] FOREIGN KEY([PaceID])REFERENCES [dbo].[P_Dict] ([DictID]);ALTER TABLE [dbo].[P_Project] CHECK CONSTRAINT [FK_P_Project_P_Dict_Pace]; ALTER TABLE [dbo].[P_Project] WITH CHECK ADD CONSTRAINT [FK_P_Project_P_Dict_DynamicRiskLevel] FOREIGN KEY([DynamicRiskLevelID])REFERENCES [dbo].[P_Dict] ([DictID]);ALTER TABLE [dbo].[P_Project] CHECK CONSTRAINT [FK_P_Project_P_Dict_DynamicRiskLevel]; ALTER TABLE [dbo].[P_Project] WITH CHECK ADD CONSTRAINT [FK_P_Project_P_Dict_DelayLevel] FOREIGN KEY([DelayLevelID])REFERENCES [dbo].[P_Dict] ([DictID]);ALTER TABLE [dbo].[P_Project] CHECK CONSTRAINT [FK_P_Project_P_Dict_DelayLevel]; ALTER TABLE [dbo].[P_Project] WITH CHECK ADD CONSTRAINT [FK_P_Project_P_Dict_TeamworkEval] FOREIGN KEY([TeamworkEvalID])REFERENCES [dbo].[P_Dict] ([DictID]);ALTER TABLE [dbo].[P_Project] CHECK CONSTRAINT [FK_P_Project_P_Dict_TeamworkEval]; ALTER TABLE [dbo].[P_Project] WITH CHECK ADD CONSTRAINT [FK_P_Project_P_Dict_PaceEval] FOREIGN KEY([PaceEvalID])REFERENCES [dbo].[P_Dict] ([DictID]);ALTER TABLE [dbo].[P_Project] CHECK CONSTRAINT [FK_P_Project_P_Dict_PaceEval]; ALTER TABLE [dbo].[P_Project] WITH CHECK ADD CONSTRAINT [FK_P_Project_P_Dict_EfficiencyEval] FOREIGN KEY([EfficiencyEvalID])REFERENCES [dbo].[P_Dict] ([DictID]);ALTER TABLE [dbo].[P_Project] CHECK CONSTRAINT [FK_P_Project_P_Dict_EfficiencyEval]; ALTER TABLE [dbo].[P_Project] WITH CHECK ADD CONSTRAINT [FK_P_Project_P_Dict_QualityEval] FOREIGN KEY([QualityEvalID])REFERENCES [dbo].[P_Dict] ([DictID]);ALTER TABLE [dbo].[P_Project] CHECK CONSTRAINT [FK_P_Project_P_Dict_QualityEval]; ALTER TABLE [dbo].[P_Project] WITH CHECK ADD CONSTRAINT [FK_P_Project_P_Dict_TotalityEval] FOREIGN KEY([TotalityEvalID])REFERENCES [dbo].[P_Dict] ([DictID]);ALTER TABLE [dbo].[P_Project] CHECK CONSTRAINT [FK_P_Project_P_Dict_TotalityEval]; ALTER TABLE [dbo].[P_Project] WITH CHECK ADD CONSTRAINT [FK_P_Project_P_Dict_Urgency] FOREIGN KEY([UrgencyID])REFERENCES [dbo].[P_Dict] ([DictID]);ALTER TABLE [dbo].[P_Project] CHECK CONSTRAINT [FK_P_Project_P_Dict_Urgency]; ALTER TABLE [dbo].[P_Project] WITH CHECK ADD CONSTRAINT [FK_P_Project_P_Dict_Type] FOREIGN KEY([TypeID])REFERENCES [dbo].[P_Dict] ([DictID]);ALTER TABLE [dbo].[P_Project] CHECK CONSTRAINT [FK_P_Project_P_Dict_Type];";
                listSQL.Add(updateSql);

                updateSql = "ALTER TABLE P_Project ADD ProjectManagerID [dbo].[D_系统主键] NULL ;EXECUTE sp_addextendedproperty  N'MS_Description', N'项目负责人ID', N'SCHEMA', N'dbo', N'TABLE', N'P_Project', N'COLUMN', N'ProjectManagerID';ALTER TABLE P_Project ADD ProjectManager varchar(50) NULL ;EXECUTE sp_addextendedproperty  N'MS_Description', N'项目负责人', N'SCHEMA', N'dbo', N'TABLE', N'P_Project', N'COLUMN', N'ProjectManager';ALTER TABLE [dbo].[P_Project]  WITH CHECK ADD  CONSTRAINT [FK_F_P_Project_HR_ProjectManager] FOREIGN KEY([ProjectManagerID])REFERENCES [dbo].[HR_Employee] ([EmpID]);ALTER TABLE [dbo].[P_Project] CHECK CONSTRAINT [FK_F_P_Project_HR_ProjectManager];";
                listSQL.Add(updateSql);

                updateSql = "ALTER TABLE AH_SingleLicence ADD IsCharLicenceByMAC bit NULL;EXECUTE sp_addextendedproperty  N'MS_Description', N'是否是字符串授权码', N'SCHEMA', N'dbo', N'TABLE', N'AH_SingleLicence', N'COLUMN', N'IsCharLicenceByMAC';";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.21");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.21) Update Error, Please Check The Log!");
            }
            #endregion
            #region 1.0.0.22 新增项目表
            if (IsUpdateDataBase(oldVersion, "1.0.0.22"))
            {
                List<string> listSQL = new List<string>();
                updateSql = " UPDATE [AH_SingleLicence] SET [IsCharLicenceByMAC] =0 where IsCharLicenceByMAC IS NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('P_Client', 'ClientNo') IS NULL begin ALTER TABLE P_Client ADD ClientNo bigint NULL;";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('P_Client', 'IsRepeat') IS NULL begin ALTER TABLE P_Client ADD IsRepeat bit NULL;";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('P_Client', 'IsRepeat') IS NULL begin update P_Client  SET ClientNo=NULL;update P_Client  SET IsRepeat =0,ClientNo=cast(rownum as bigint) from(select IsRepeat,PClientID,ClientNo,10000+ROW_NUMBER() over(order by PClientID) rownum from P_Client where ClientNo IS NULL) P_Client where ClientNo IS NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('P_CustomerService', 'StatusCName') IS NULL begin ALTER TABLE P_CustomerService ADD StatusCName varchar(50) NULL; ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.22");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.22) Update Error, Please Check The Log!");
            }
            #endregion
            #region 1.0.0.23 
            if (IsUpdateDataBase(oldVersion, "1.0.0.23"))
            {
                List<string> listSQL = new List<string>();
                updateSql = " ALTER TABLE P_Contract ADD ContentID bigint NULL;EXECUTE sp_addextendedproperty  N'MS_Description', N'合同类型Id', N'SCHEMA', N'dbo', N'TABLE', N'P_Contract', N'COLUMN', N'ContentID';";
                listSQL.Add(updateSql);

                updateSql = "ALTER TABLE [dbo].[P_Contract]  WITH CHECK ADD  CONSTRAINT [FK_P_Contract_P_Dict_ContentID] FOREIGN KEY([ContentID])REFERENCES [dbo].[P_Dict] ([DictID]);ALTER TABLE [dbo].[P_Contract] CHECK CONSTRAINT [FK_P_Contract_P_Dict_ContentID];";
                listSQL.Add(updateSql);

                updateSql = "ALTER TABLE P_Project ADD ContentID bigint NULL;EXECUTE sp_addextendedproperty  N'MS_Description', N'合同类型Id', N'SCHEMA', N'dbo', N'TABLE', N'P_Project', N'COLUMN', N'ContentID';";
                listSQL.Add(updateSql);

                updateSql = "ALTER TABLE P_Project ADD Content varchar(200) NULL;EXECUTE sp_addextendedproperty  N'MS_Description', N'同类型名称', N'SCHEMA', N'dbo', N'TABLE', N'P_Project', N'COLUMN', N'Content';";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE [dbo].[P_Project]  WITH CHECK ADD  CONSTRAINT [FK_P_Project_P_Dict_ContentID] FOREIGN KEY([ContentID])REFERENCES [dbo].[P_Dict] ([DictID]);ALTER TABLE [dbo].[P_Project] CHECK CONSTRAINT [FK_P_Project_P_Dict_ContentID]; ";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE P_Project ADD ProvinceID bigint NULL;EXECUTE sp_addextendedproperty  N'MS_Description', N'省份ID', N'SCHEMA', N'dbo', N'TABLE', N'P_Project', N'COLUMN', N'ProvinceID';";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE P_Project ADD ProvinceName varchar(200) NULL;EXECUTE sp_addextendedproperty  N'MS_Description', N'同类型名称', N'SCHEMA', N'dbo', N'TABLE', N'P_Project', N'COLUMN', N'ProvinceName';";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE [dbo].[P_Project]  WITH CHECK ADD  CONSTRAINT [FK_P_Project_P_Dict_Province] FOREIGN KEY([ProvinceID])REFERENCES [dbo].[P_Dict] ([DictID]);ALTER TABLE [dbo].[P_Project] CHECK CONSTRAINT [FK_P_Project_P_Dict_Province];";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.23");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.23) Update Error, Please Check The Log!");
            }
            #endregion
            #region 1.0.0.24 
            if (IsUpdateDataBase(oldVersion, "1.0.0.24"))
            {
                List<string> listSQL = new List<string>();
                updateSql = " if exists(select * from syscolumns where id=object_id('P_Contract') and name='ContrastId') begin ALTER TABLE P_Contract DROP COLUMN ContrastId end;ALTER TABLE P_Contract ADD ContrastId bigint NULL; EXECUTE sp_addextendedproperty  N'MS_Description', N'对比人Id', N'SCHEMA', N'dbo', N'TABLE', N'P_Contract', N'COLUMN', N'ContrastId';";
                listSQL.Add(updateSql);

                updateSql = "if exists(select * from syscolumns where id=object_id('P_Contract') and name='ContrastCName') begin ALTER TABLE P_Contract DROP COLUMN ContrastCName end;ALTER TABLE P_Contract ADD ContrastCName varchar(60) NULL; EXECUTE sp_addextendedproperty  N'MS_Description', N'对比人名称', N'SCHEMA', N'dbo', N'TABLE', N'P_Contract', N'COLUMN', N'ContrastCName';";
                listSQL.Add(updateSql);

                updateSql = "if exists(select * from syscolumns where id=object_id('P_Contract') and name='CheckId') begin ALTER TABLE P_Contract DROP COLUMN CheckId end;ALTER TABLE P_Contract ADD CheckId bigint NULL; EXECUTE sp_addextendedproperty  N'MS_Description', N'审核人Id', N'SCHEMA', N'dbo', N'TABLE', N'P_Contract', N'COLUMN', N'CheckId';";
                listSQL.Add(updateSql);

                updateSql = "if exists(select * from syscolumns where id=object_id('P_Contract') and name='CheckCName') begin ALTER TABLE P_Contract DROP COLUMN CheckCName end;ALTER TABLE P_Contract ADD CheckCName varchar(60) NULL; EXECUTE sp_addextendedproperty  N'MS_Description', N'审核人名称', N'SCHEMA', N'dbo', N'TABLE', N'P_Contract', N'COLUMN', N'CheckCName';";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from syscolumns where id=object_id('P_Invoice') and name='ContrastId') begin ALTER TABLE P_Invoice DROP COLUMN ContrastId end;ALTER TABLE P_Invoice ADD ContrastId bigint NULL; EXECUTE sp_addextendedproperty  N'MS_Description', N'对比人Id', N'SCHEMA', N'dbo', N'TABLE', N'P_Invoice', N'COLUMN', N'ContrastId';";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from syscolumns where id=object_id('P_Invoice') and name='ContrastCName') begin ALTER TABLE P_Invoice DROP COLUMN ContrastCName end;ALTER TABLE P_Invoice ADD ContrastCName varchar(60) NULL; EXECUTE sp_addextendedproperty  N'MS_Description', N'对比人名称', N'SCHEMA', N'dbo', N'TABLE', N'P_Invoice', N'COLUMN', N'ContrastCName';";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from syscolumns where id=object_id('P_Invoice') and name='CheckId') begin ALTER TABLE P_Invoice DROP COLUMN CheckId end;ALTER TABLE P_Invoice ADD CheckId bigint NULL; EXECUTE sp_addextendedproperty  N'MS_Description', N'审核人Id', N'SCHEMA', N'dbo', N'TABLE', N'P_Invoice', N'COLUMN', N'CheckId';";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from syscolumns where id=object_id('P_Invoice') and name='CheckCName') begin ALTER TABLE P_Invoice DROP COLUMN CheckCName end;ALTER TABLE P_Invoice ADD CheckCName varchar(60) NULL; EXECUTE sp_addextendedproperty  N'MS_Description', N'审核人名称', N'SCHEMA', N'dbo', N'TABLE', N'P_Invoice', N'COLUMN', N'CheckCName';";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from syscolumns where id=object_id('P_Finance_Receive') and name='ContrastId') begin ALTER TABLE P_Finance_Receive DROP COLUMN ContrastId end;ALTER TABLE P_Finance_Receive ADD ContrastId bigint NULL; EXECUTE sp_addextendedproperty  N'MS_Description', N'对比人Id', N'SCHEMA', N'dbo', N'TABLE', N'P_Finance_Receive', N'COLUMN', N'ContrastId';";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from syscolumns where id=object_id('P_Finance_Receive') and name='ContrastCName') begin ALTER TABLE P_Finance_Receive DROP COLUMN ContrastCName end;ALTER TABLE P_Finance_Receive ADD ContrastCName varchar(60) NULL; EXECUTE sp_addextendedproperty  N'MS_Description', N'对比人名称', N'SCHEMA', N'dbo', N'TABLE', N'P_Finance_Receive', N'COLUMN', N'ContrastCName';";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from syscolumns where id=object_id('P_Finance_Receive') and name='CheckId') begin ALTER TABLE P_Finance_Receive DROP COLUMN CheckId end;ALTER TABLE P_Finance_Receive ADD CheckId bigint NULL; EXECUTE sp_addextendedproperty  N'MS_Description', N'审核人Id', N'SCHEMA', N'dbo', N'TABLE', N'P_Finance_Receive', N'COLUMN', N'CheckId';";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from syscolumns where id=object_id('P_Finance_Receive') and name='CheckCName') begin ALTER TABLE P_Finance_Receive DROP COLUMN CheckCName end;ALTER TABLE P_Finance_Receive ADD CheckCName varchar(60) NULL; EXECUTE sp_addextendedproperty  N'MS_Description', N'审核人名称', N'SCHEMA', N'dbo', N'TABLE', N'P_Finance_Receive', N'COLUMN', N'CheckCName';";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.24");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.24) Update Error, Please Check The Log!");
            }
            #endregion
            #region 1.0.0.25 
            if (IsUpdateDataBase(oldVersion, "1.0.0.25"))
            {
                List<string> listSQL = new List<string>();
                updateSql = " IF COL_LENGTH('P_Client', 'ClientClassID') IS NULL ALTER TABLE P_Client ADD ClientClassID bigint ;";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('P_Client', 'ClientClassName') IS NULL ALTER TABLE P_Client ADD ClientClassName nvarchar(30);";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('P_Client', 'EquipManager') IS NULL ALTER TABLE P_Client ADD EquipManager varchar(50);";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('P_Client', 'EquipPhone') IS NULL ALTER TABLE P_Client ADD EquipPhone varchar(50);";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('P_Client', 'InformationManager') IS NULL ALTER TABLE P_Client ADD InformationManager varchar(50);";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('P_Client', 'InformationPhone') IS NULL ALTER TABLE P_Client ADD InformationPhone varchar(50);";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('P_Client', 'LabManager') IS NULL ALTER TABLE P_Client ADD LabManager varchar(50);";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('P_Client', 'LabPhone') IS NULL ALTER TABLE P_Client ADD LabPhone varchar(50);";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('P_Client', 'Fax') IS NULL ALTER TABLE P_Client ADD Fax varchar(50);";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('P_Client', 'OperationMan') IS NULL ALTER TABLE P_Client ADD OperationMan varchar(50);";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('P_Client', 'OperationPhone') IS NULL ALTER TABLE P_Client ADD OperationPhone varchar(50);";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('P_Client', 'HISFactory') IS NULL ALTER TABLE P_Client ADD HISFactory varchar(150);";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE [dbo].[P_Project] DROP CONSTRAINT [FK_P_Project_P_Dict_Province];";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE [dbo].[P_Project]  WITH CHECK ADD  CONSTRAINT [FK_P_Project_B_Province] FOREIGN KEY([ProvinceID])REFERENCES [dbo].[B_Province] ([ProvinceID]);ALTER TABLE [dbo].[P_Project] CHECK CONSTRAINT [FK_P_Project_B_Province]; ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.25");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.25) Update Error, Please Check The Log!");
            }
            #endregion
            #region 1.0.0.26 
            if (IsUpdateDataBase(oldVersion, "1.0.0.26"))
            {
                List<string> listSQL = new List<string>();
                if (!CheckDataObjectIsExists("CUser", "U"))
                {

                    updateSql = " CREATE TABLE [dbo].[CUser](    [UserNo][bigint] NOT NULL,   [UserName] [nvarchar](100) NULL,	[UserAddress]        [nvarchar](250) NULL,	[UserPostcode]     [nvarchar](10) NULL,	[UserTelephone]        [nvarchar](20) NULL,	[UserFax]        [nvarchar](20) NULL,	[UserLinkman]        [nvarchar](50) NULL,	[UserArea]        [nvarchar](50) NULL,	[UserCName]        [nvarchar](50) NULL,	[UserFWNo]        [nvarchar](6) NULL,	[PTechSQH]        [nvarchar](4) NULL,	[LRNo1]        [nvarchar](20) NULL,	[LRNo2]        [nvarchar](20) NULL,	[LicenceInfo]        [nvarchar](255) NULL,	[LicenceText]        [nvarchar](max) NULL,	[bHint]        [bit]        NOT NULL,    [LHint] [nvarchar](255) NULL,	[IsMapping]        [bit]        NULL,	[DataTimeStamp]        [timestamp]        NULL, CONSTRAINT[PK_CUser] PRIMARY KEY CLUSTERED(   [UserNo] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]; ";
                    listSQL.Add(updateSql);

                    updateSql = " if exists(select * from syscolumns where id=object_id('CUser') and name='ContrastId') begin ALTER TABLE CUser DROP COLUMN ContrastId end;ALTER TABLE CUser ADD ContrastId bigint NULL; EXECUTE sp_addextendedproperty  N'MS_Description', N'对比人Id', N'SCHEMA', N'dbo', N'TABLE', N'CUser', N'COLUMN', N'ContrastId';";
                    listSQL.Add(updateSql);

                    updateSql = " if exists(select * from syscolumns where id=object_id('CUser') and name='ContrastCName') begin ALTER TABLE CUser DROP COLUMN ContrastCName end;ALTER TABLE CUser ADD ContrastCName varchar(60) NULL; EXECUTE sp_addextendedproperty  N'MS_Description', N'对比人名称', N'SCHEMA', N'dbo', N'TABLE', N'CUser', N'COLUMN', N'ContrastCName';";
                    listSQL.Add(updateSql);

                    updateSql = " if exists(select * from syscolumns where id=object_id('CUser') and name='CheckId') begin ALTER TABLE CUser DROP COLUMN CheckId end;ALTER TABLE CUser ADD CheckId bigint NULL; EXECUTE sp_addextendedproperty  N'MS_Description', N'审核人Id', N'SCHEMA', N'dbo', N'TABLE', N'CUser', N'COLUMN', N'CheckId';";
                    listSQL.Add(updateSql);

                    updateSql = " if exists(select * from syscolumns where id=object_id('CUser') and name='CheckCName') begin ALTER TABLE CUser DROP COLUMN CheckCName end;ALTER TABLE CUser ADD CheckCName varchar(60) NULL; EXECUTE sp_addextendedproperty  N'MS_Description', N'审核人名称', N'SCHEMA', N'dbo', N'TABLE', N'CUser', N'COLUMN', N'CheckCName';";
                    listSQL.Add(updateSql);

                    result = ExecuteUpdateSQL(listSQL);

                }
                else
                {
                    updateSql = " if exists(select * from syscolumns where id=object_id('CUser') and name='ContrastId') begin ALTER TABLE CUser DROP COLUMN ContrastId end;ALTER TABLE CUser ADD ContrastId bigint NULL; EXECUTE sp_addextendedproperty  N'MS_Description', N'对比人Id', N'SCHEMA', N'dbo', N'TABLE', N'CUser', N'COLUMN', N'ContrastId';";
                    listSQL.Add(updateSql);

                    updateSql = " if exists(select * from syscolumns where id=object_id('CUser') and name='ContrastCName') begin ALTER TABLE CUser DROP COLUMN ContrastCName end;ALTER TABLE CUser ADD ContrastCName varchar(60) NULL; EXECUTE sp_addextendedproperty  N'MS_Description', N'对比人名称', N'SCHEMA', N'dbo', N'TABLE', N'CUser', N'COLUMN', N'ContrastCName';";
                    listSQL.Add(updateSql);

                    updateSql = " if exists(select * from syscolumns where id=object_id('CUser') and name='CheckId') begin ALTER TABLE CUser DROP COLUMN CheckId end;ALTER TABLE CUser ADD CheckId bigint NULL; EXECUTE sp_addextendedproperty  N'MS_Description', N'审核人Id', N'SCHEMA', N'dbo', N'TABLE', N'CUser', N'COLUMN', N'CheckId';";
                    listSQL.Add(updateSql);

                    updateSql = " if exists(select * from syscolumns where id=object_id('CUser') and name='CheckCName') begin ALTER TABLE CUser DROP COLUMN CheckCName end;ALTER TABLE CUser ADD CheckCName varchar(60) NULL; EXECUTE sp_addextendedproperty  N'MS_Description', N'审核人名称', N'SCHEMA', N'dbo', N'TABLE', N'CUser', N'COLUMN', N'CheckCName';";
                    listSQL.Add(updateSql);

                    updateSql = " IF COL_LENGTH('CUser', 'IsMapping') IS NULL  ALTER TABLE CUser ADD IsMapping bit NULL;";
                    listSQL.Add(updateSql);

                    updateSql = " IF COL_LENGTH('CUser', 'DataTimeStamp') IS NULL  ALTER TABLE CUser ADD DataTimeStamp timestamp NULL; ";
                    listSQL.Add(updateSql);

                    result = ExecuteUpdateSQL(listSQL);
                }

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.26");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.26) Update Error, Please Check The Log!");
            }
            #endregion
            #region 1.0.0.27 新增项目任务表
            if (IsUpdateDataBase(oldVersion, "1.0.0.27"))
            {
                if (!CheckDataObjectIsExists("P_ProjectTask", "U"))
                {
                    List<string> listSQL = new List<string>();
                    updateSql = "CREATE TABLE [dbo].[P_ProjectTask]([LabID][dbo].[D_系统主键] NULL,  [ProjectTaskID][dbo].[D_系统主键]  NOT NULL,[CName] [varchar](500) NULL,  [TaskHelp] [varchar](500) NULL,[ProjectID] [dbo].[D_系统主键]   NULL,[PTaskID]   [dbo].[D_系统主键]    NULL,[StandWorkload]  [float] NULL,	[EstiWorkload] [float] NULL,[Workload] [float] NULL,[EstiStartTime]  [datetime]  NULL,[EstiEndTime]  [datetime]   NULL,[StartTime] [datetime]  NULL,[EndTime]  [datetime]    NULL,[RemainWorkDays]  [float] NULL,	[EstiAllDays]  [float] NULL,[AllDays]   [float] NULL,[OtherMsg]   [varchar](max) NULL,[ExtraMsg]  [varchar](max) NULL,	[IsStandard]  [bit]  NULL,[IsUse] [bit]   NULL,	[Memo] [varchar](500) NULL,[Contents]   [varchar](max) NULL,[CreatEmpID]  [dbo].[D_系统主键]  NULL,[DataAddTime]  [datetime] NULL,[DataTimeStamp]  [timestamp] NULL,CONSTRAINT[PK_P_PROJECTTASK] PRIMARY KEY CLUSTERED(   [ProjectTaskID] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY] TEXTIMAGE_ON [PRIMARY] ";
                    listSQL.Add(updateSql);

                    updateSql = "ALTER TABLE[dbo].[P_ProjectTask]  WITH CHECK ADD CONSTRAINT[FK_P_PROJEC_REFERENCE_HR_EMPLO] FOREIGN KEY([CreatEmpID]) REFERENCES[dbo].[HR_Employee] ([EmpID])";
                    listSQL.Add(updateSql);

                    updateSql = "ALTER TABLE[dbo].[P_ProjectTask]  CHECK CONSTRAINT[FK_P_PROJEC_REFERENCE_HR_EMPLO]";
                    listSQL.Add(updateSql);

                    updateSql = "ALTER TABLE[dbo].[P_ProjectTask] WITH CHECK ADD CONSTRAINT[FK_P_PROJEC_REFERENCE_P_PROJEC] FOREIGN KEY([ProjectID]) REFERENCES[dbo].[P_Project] ([ProjectID])";
                    listSQL.Add(updateSql);

                    updateSql = "ALTER TABLE[dbo].[P_ProjectTask] CHECK CONSTRAINT[FK_P_PROJEC_REFERENCE_P_PROJEC]";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'平台客户ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\', @level2type = N\'COLUMN\', @level2name = N\'LabID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'项目名称\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\', @level2type = N\'COLUMN\', @level2name = N\'ProjectTaskID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'合同名称\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\', @level2type = N\'COLUMN\', @level2name = N\'CName\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'项目进度名称\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\', @level2type = N\'COLUMN\', @level2name = N\'TaskHelp\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'项目主键ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\', @level2type = N\'COLUMN\', @level2name = N\'ProjectID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'合同ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\', @level2type = N\'COLUMN\', @level2name = N\'PTaskID\'";

                    listSQL.Add(updateSql);
                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'预计工作量\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\', @level2type = N\'COLUMN\', @level2name = N\'StandWorkload\'";

                    listSQL.Add(updateSql);
                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'预计工作量\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\', @level2type = N\'COLUMN\', @level2name = N\'EstiWorkload\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'实际工作量\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\', @level2type = N\'COLUMN\', @level2name = N\'Workload\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'计划开始时间\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\', @level2type = N\'COLUMN\', @level2name = N\'EstiStartTime\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'计划结束时间\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\', @level2type = N\'COLUMN\', @level2name = N\'EstiEndTime\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'实际开始时间\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\', @level2type = N\'COLUMN\', @level2name = N\'StartTime\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'实际结束时间\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\', @level2type = N\'COLUMN\', @level2name = N\'EndTime\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'动态剩余工作量\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\', @level2type = N\'COLUMN\', @level2name = N\'RemainWorkDays\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'计划人工作量\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\', @level2type = N\'COLUMN\', @level2name = N\'EstiAllDays\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'实际人工作量\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\', @level2type = N\'COLUMN\', @level2name = N\'AllDays\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'其他信息\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\', @level2type = N\'COLUMN\', @level2name = N\'OtherMsg\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'附加信息\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\', @level2type = N\'COLUMN\', @level2name = N\'ExtraMsg\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'是否使用\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\', @level2type = N\'COLUMN\', @level2name = N\'IsUse\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'备注\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\', @level2type = N\'COLUMN\', @level2name = N\'Memo\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'内容\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\', @level2type = N\'COLUMN\', @level2name = N\'Contents\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'创建人\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\', @level2type = N\'COLUMN\', @level2name = N\'CreatEmpID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'创建时间\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\', @level2type = N\'COLUMN\', @level2name = N\'DataAddTime\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'时间戳\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\', @level2type = N\'COLUMN\', @level2name = N\'DataTimeStamp\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\' 项目任务表\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTask\'";
                    listSQL.Add(updateSql);

                    result = ExecuteUpdateSQL(listSQL);

                    if (result)
                        result = UpateCompareVersionInfo("1.0.0.27");
                    else
                        ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.27) Update Error, Please Check The Log!");
                }
            }
            #endregion
            #region 1.0.0.28 项目任务进度表
            if (IsUpdateDataBase(oldVersion, "1.0.0.28"))
            {
                if (!CheckDataObjectIsExists("P_ProjectTaskProgress", "U"))
                {
                    List<string> listSQL = new List<string>();
                    updateSql = "CREATE TABLE [dbo].[P_ProjectTaskProgress]([LabID][dbo].[D_系统主键] NULL, [TaskProgressID] [dbo].[D_系统主键] NOT NULL,[ProjectID] [dbo].[D_系统主键]  NULL,[ProjectTaskID] [dbo].[D_系统主键] NULL,[Workload] [float] NULL,[RegisterEmpID] [bigint] NULL,[RegisterTime] [datetime] NULL,[ExecuteTime]  [datetime] NULL,[TaskWorkInfo] [varchar](max) NULL,[TaskRisk] [varchar](max) NULL,[TaskTypeDict]  [varchar](500) NULL,[IsUse] [bit] NULL,[Memo]  [varchar](500) NULL,[DataAddTime] [datetime] NULL,[DataTimeStamp] [timestamp]  NULL,CONSTRAINT[PK_P_PROJECTTASKPROGRESS] PRIMARY KEY CLUSTERED( [TaskProgressID] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]";

                    listSQL.Add(updateSql);

                    updateSql = "ALTER TABLE[dbo].[P_ProjectTaskProgress] WITH CHECK ADD CONSTRAINT[FK_P_PROJEC_REFERENCE_HR_EMP] FOREIGN KEY([RegisterEmpID]) REFERENCES[dbo].[HR_Employee] ([EmpID])";
                    listSQL.Add(updateSql);

                    updateSql = "ALTER TABLE[dbo].[P_ProjectTaskProgress] CHECK CONSTRAINT[FK_P_PROJEC_REFERENCE_HR_EMP]";
                    listSQL.Add(updateSql);

                    updateSql = "ALTER TABLE[dbo].[P_ProjectTaskProgress] WITH CHECK ADD CONSTRAINT[FK_P_PROJEC_REFERENCE_P_PROJECT] FOREIGN KEY([ProjectID]) REFERENCES[dbo].[P_Project] ([ProjectID])";
                    listSQL.Add(updateSql);

                    updateSql = "ALTER TABLE[dbo].[P_ProjectTaskProgress] CHECK CONSTRAINT[FK_P_PROJEC_REFERENCE_P_PROJECT]";
                    listSQL.Add(updateSql);

                    updateSql = "ALTER TABLE[dbo].[P_ProjectTaskProgress] WITH CHECK ADD CONSTRAINT[FK_P_PROJEC_REFERENCE_P_PROJECTTask] FOREIGN KEY([ProjectTaskID]) REFERENCES[dbo].[P_ProjectTask] ([ProjectTaskID])";
                    listSQL.Add(updateSql);

                    updateSql = "ALTER TABLE[dbo].[P_ProjectTaskProgress] CHECK CONSTRAINT[FK_P_PROJEC_REFERENCE_P_PROJECTTask]";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'平台客户ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTaskProgress\', @level2type = N\'COLUMN\', @level2name = N\'LabID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'项目名称\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTaskProgress\', @level2type = N\'COLUMN\', @level2name = N\'TaskProgressID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'项目主键ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTaskProgress\', @level2type = N\'COLUMN\', @level2name = N\'ProjectID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'合同ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTaskProgress\', @level2type = N\'COLUMN\', @level2name = N\'ProjectTaskID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'实际工作量\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTaskProgress\', @level2type = N\'COLUMN\', @level2name = N\'Workload\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'实际开始时间\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTaskProgress\', @level2type = N\'COLUMN\', @level2name = N\'RegisterEmpID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'实际开始时间\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTaskProgress\', @level2type = N\'COLUMN\', @level2name = N\'RegisterTime\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'实际结束时间\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTaskProgress\', @level2type = N\'COLUMN\', @level2name = N\'ExecuteTime\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'其他信息\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTaskProgress\', @level2type = N\'COLUMN\', @level2name = N\'TaskWorkInfo\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'附加信息\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTaskProgress\', @level2type = N\'COLUMN\', @level2name = N\'TaskRisk\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'内容\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTaskProgress\', @level2type = N\'COLUMN\', @level2name = N\'TaskTypeDict\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'是否使用\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTaskProgress\', @level2type = N\'COLUMN\', @level2name = N\'IsUse\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'备注\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTaskProgress\', @level2type = N\'COLUMN\', @level2name = N\'Memo\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'创建时间\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTaskProgress\', @level2type = N\'COLUMN\', @level2name = N\'DataAddTime\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'时间戳\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTaskProgress\', @level2type = N\'COLUMN\', @level2name = N\'DataTimeStamp\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\' 项目任务进度表\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'P_ProjectTaskProgress\'";
                    listSQL.Add(updateSql);

                    result = ExecuteUpdateSQL(listSQL);

                    if (result)
                        result = UpateCompareVersionInfo("1.0.0.28");
                    else
                        ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.28) Update Error, Please Check The Log!");
                }
            }
            #endregion
            #region 1.0.0.29 项目表增加标准项目标志字段
            if (IsUpdateDataBase(oldVersion, "1.0.0.29"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " IF COL_LENGTH('P_Project', 'IsStandard') IS NULL alter table P_Project add IsStandard bit ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('P_Project', 'DispOrder') IS NULL alter table P_Project add DispOrder int ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('P_ProjectTask', 'DispOrder') IS NULL alter table P_ProjectTask add DispOrder int ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.29");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.29) Update Error, Please Check The Log!");

            }
            #endregion
            #region 1.0.0.30 项目表增加标准项目标志字段
            if (IsUpdateDataBase(oldVersion, "1.0.0.30"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " IF COL_LENGTH('P_Client', 'LicenceClientName') IS NULL ALTER TABLE P_Client ADD LicenceClientName nvarchar(500) ";
                listSQL.Add(updateSql);

                updateSql = " update P_Client set LicenceClientName=Name; ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.30");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.30) Update Error, Please Check The Log!");

            }
            #endregion

            #region 1.0.0.31 增加项目文档表
            if (IsUpdateDataBase(oldVersion, "1.0.0.31"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " CREATE TABLE[dbo].[P_ProjectDocument]( [LabID] [bigint] NULL, [ProjectDocumentID] [bigint]  NOT NULL, [ProjectID] [bigint]  NULL, [ProjectTaskID] [bigint] NULL, " +
                            " [DocumentName] [varchar](500) NULL,[DocumentLink] [varchar](500) NULL,[Content] [varchar](max) NULL,[Memo] [varchar](500) NULL,  " +
                            " [DispOrder] [int] NULL,[IsUse]   [bit]  NULL,[CreatEmpID] [dbo].[D_系统主键] NULL,[DataAddTime]  [datetime]  NULL,[FileDateTime] [datetime]  NULL,[DataTimeStamp]  [timestamp]  NULL, " +
                            " CONSTRAINT[PK_P_ProjectDocument] PRIMARY KEY CLUSTERED( [ProjectDocumentID] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY] ";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE [dbo].[P_ProjectDocument]  WITH CHECK ADD  CONSTRAINT [FK_P_PROJECT_HR_Employee] FOREIGN KEY([CreatEmpID])  REFERENCES[dbo].[HR_Employee] ([EmpID]) ";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE [dbo].[P_ProjectDocument] CHECK CONSTRAINT [FK_P_PROJECT_HR_Employee] ";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE [dbo].[P_ProjectDocument]  WITH CHECK ADD  CONSTRAINT [FK_P_PROJECT_P_PROJECT] FOREIGN KEY([ProjectID]) REFERENCES[dbo].[P_Project] ([ProjectID]) ";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE [dbo].[P_ProjectDocument] CHECK CONSTRAINT [FK_P_PROJECT_P_PROJECT] ";
                listSQL.Add(updateSql);

                updateSql = " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'文档ID\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'P_ProjectDocument\', @level2type=N\'COLUMN\',@level2name=N\'ProjectDocumentID\' ";
                listSQL.Add(updateSql);

                updateSql = " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'项目ID\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'P_ProjectDocument\', @level2type=N\'COLUMN\',@level2name=N\'ProjectID\' ";
                listSQL.Add(updateSql);

                updateSql = " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'任务ID\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'P_ProjectDocument\', @level2type=N\'COLUMN\',@level2name=N\'ProjectTaskID\' ";
                listSQL.Add(updateSql);

                updateSql = " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'文档名称\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'P_ProjectDocument\', @level2type=N\'COLUMN\',@level2name=N\'DocumentName\' ";
                listSQL.Add(updateSql);

                updateSql = " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'文档模板连接\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'P_ProjectDocument\', @level2type=N\'COLUMN\',@level2name=N\'DocumentLink\' ";
                listSQL.Add(updateSql);

                updateSql = " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'文档内容\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'P_ProjectDocument\', @level2type=N\'COLUMN\',@level2name=N\'Content\' ";
                listSQL.Add(updateSql);

                updateSql = " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'备注\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'P_ProjectDocument\', @level2type=N\'COLUMN\',@level2name=N\'Memo\' ";
                listSQL.Add(updateSql);

                updateSql = " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'次序\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'P_ProjectDocument\', @level2type=N\'COLUMN\',@level2name=N\'DispOrder\' ";
                listSQL.Add(updateSql);

                updateSql = " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'是否使用\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'P_ProjectDocument\', @level2type=N\'COLUMN\',@level2name=N\'IsUse\' ";
                listSQL.Add(updateSql);

                updateSql = " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'创建人\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'P_ProjectDocument\', @level2type=N\'COLUMN\',@level2name=N\'CreatEmpID\' ";
                listSQL.Add(updateSql);

                updateSql = " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'创建时间\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'P_ProjectDocument\', @level2type=N\'COLUMN\',@level2name=N\'DataAddTime\' ";
                listSQL.Add(updateSql);

                updateSql = " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'归档时间\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'P_ProjectDocument\', @level2type=N\'COLUMN\',@level2name=N\'FileDateTime\' ";
                listSQL.Add(updateSql);

                updateSql = " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'时间戳\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'P_ProjectDocument\', @level2type=N\'COLUMN\',@level2name=N\'DataTimeStamp\' ";
                listSQL.Add(updateSql);

                updateSql = " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'项目文档表\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'P_ProjectDocument\' ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.31");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.31) Update Error, Please Check The Log!");

            }
            #endregion

            #region 1.0.0.32 发票表新增发票内容字段
            if (IsUpdateDataBase(oldVersion, "1.0.0.32"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " IF COL_LENGTH('P_Invoice', 'InvoiceContentID') IS NULL ALTER TABLE P_Invoice ADD InvoiceContentID bigint ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('P_Invoice', 'InvoiceContentName') IS NULL ALTER TABLE P_Invoice ADD InvoiceContentName varchar(200) ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.32");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.32) Update Error, Please Check The Log!");

            }
            #endregion

            #region 1.0.0.33 发票表新增发票内容字段
            if (IsUpdateDataBase(oldVersion, "1.0.0.33"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " IF COL_LENGTH('P_Client', 'IsContract ') IS NULL ALTER TABLE P_Client ADD IsContract  bit  ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.33");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.33) Update Error, Please Check The Log!");

            }
            #endregion

            #region 数据权限升级

            #region 1.0.0.34 创建预置条件表
            if (IsUpdateDataBase(oldVersion, "1.0.0.34"))
            {
                List<string> listSQL = new List<string>();
                //创建预置条件表
                updateSql = " SET ANSI_NULLS ON; SET QUOTED_IDENTIFIER ON; CREATE TABLE [dbo].[RBAC_Preconditions]( [LabID] [dbo].[D_实验室ID] NULL, [Id] [dbo].[D_系统主键] NOT NULL, [ModuleID] [dbo].[D_系统主键] NOT NULL, [CName] [nvarchar](200) NULL, [EName] [nvarchar](200) NULL, [ServiceURLCName] [nvarchar](200) NULL, [ServiceURLEName] [nvarchar](200) NULL, [IsUse] [bit] NULL, [DataAddTime] [datetime] NULL, [DataUpdateTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_RBAC_Preconditions] PRIMARY KEY CLUSTERED ( [Id] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY]; ALTER TABLE [dbo].[RBAC_Preconditions] WITH CHECK ADD CONSTRAINT [FK_RBAC_Preconditions_RBAC_Module] FOREIGN KEY([ModuleID]) REFERENCES [dbo].[RBAC_Module] ([ModuleID]); ALTER TABLE [dbo].[RBAC_Preconditions] CHECK CONSTRAINT [FK_RBAC_Preconditions_RBAC_Module]; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'LabID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RBAC_Preconditions', @level2type=N'COLUMN',@level2name=N'LabID'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RBAC_Preconditions', @level2type=N'COLUMN',@level2name=N'Id'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属模块' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RBAC_Preconditions', @level2type=N'COLUMN',@level2name=N'ModuleID'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'预置条件显示名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RBAC_Preconditions', @level2type=N'COLUMN',@level2name=N'CName'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'预置条件名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RBAC_Preconditions', @level2type=N'COLUMN',@level2name=N'EName'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务显示名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RBAC_Preconditions', @level2type=N'COLUMN',@level2name=N'ServiceURLCName'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RBAC_Preconditions', @level2type=N'COLUMN',@level2name=N'ServiceURLEName'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RBAC_Preconditions', @level2type=N'COLUMN',@level2name=N'IsUse'; EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'预置条件' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RBAC_Preconditions'; ";
                listSQL.Add(updateSql);
                //行数据条件添预置条件Id
                updateSql = " IF COL_LENGTH('RBAC_RowFilter', 'PreconditionsId ') IS NULL ALTER TABLE RBAC_RowFilter ADD PreconditionsId  D_系统主键 ;";
                listSQL.Add(updateSql);
                //行数据条件创建预置条件Id外键关系
                updateSql = " ALTER TABLE [dbo].[RBAC_RowFilter]  WITH CHECK ADD  CONSTRAINT [FK_RBAC_RowFilter_RBAC_Preconditions] FOREIGN KEY([PreconditionsId]) REFERENCES [dbo].[RBAC_Preconditions] ([Id]) ALTER TABLE [dbo].[RBAC_RowFilter] CHECK CONSTRAINT [FK_RBAC_RowFilter_RBAC_Preconditions];";
                listSQL.Add(updateSql);

                //模块操作添加服务URL显示名称
                updateSql = " IF COL_LENGTH('RBAC_ModuleOper', 'ServiceURLCName') IS NULL ALTER TABLE RBAC_ModuleOper ADD ServiceURLCName  nvarchar(200);";
                listSQL.Add(updateSql);

                //模块操作添加服务URL名称
                updateSql = " IF COL_LENGTH('RBAC_ModuleOper', 'ServiceURLEName') IS NULL ALTER TABLE RBAC_ModuleOper ADD ServiceURLEName  nvarchar(200);";
                listSQL.Add(updateSql);
                //模块操作添加行过滤依据对象中文名称
                updateSql = "IF COL_LENGTH('RBAC_ModuleOper', 'RowFilterBaseCName') IS NULL ALTER TABLE RBAC_ModuleOper ADD RowFilterBaseCName  nvarchar(100);";
                listSQL.Add(updateSql);
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.34");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.34) Update Error, Please Check The Log!");

            }
            #endregion

            #region 1.0.0.35  预置条件新增值类型字段
            if (IsUpdateDataBase(oldVersion, "1.0.0.35"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " IF COL_LENGTH('RBAC_Preconditions', 'ValueType ') IS NULL ALTER TABLE RBAC_Preconditions ADD ValueType  nvarchar(50); ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.35");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.35) Update Error, Please Check The Log!");

            }
            #endregion

            #region 1.0.0.36 角色权限表新增预置条件Id字段及创建预置条件Id外键关系
            if (IsUpdateDataBase(oldVersion, "1.0.0.36"))
            {
                List<string> listSQL = new List<string>();
                //角色权限添加预置条件Id字段
                updateSql = " IF COL_LENGTH('RBAC_RoleRight', 'PreconditionsId') IS NULL ALTER TABLE RBAC_RoleRight ADD PreconditionsId D_系统主键; ";
                listSQL.Add(updateSql);
                //角色权限创建预置条件Id与预置条件表的外键关系
                updateSql = " ALTER TABLE [dbo].[RBAC_RoleRight]  WITH CHECK ADD  CONSTRAINT [FK_RBAC_RoleRight_RBAC_Preconditions] FOREIGN KEY([PreconditionsId]) REFERENCES [dbo].[RBAC_Preconditions] ([Id]);ALTER TABLE [dbo].[RBAC_RoleRight] CHECK CONSTRAINT [FK_RBAC_RoleRight_RBAC_Preconditions]; ";
                listSQL.Add(updateSql);
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.36");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.36) Update Error, Please Check The Log!");

            }
            #endregion

            #endregion

            #region 1.0.0.37 项目任务信息表添加计划从第几天起
            if (IsUpdateDataBase(oldVersion, "1.0.0.37"))
            {
                List<string> listSQL = new List<string>();
                //项目任务信息表添加计划从第几天起
                updateSql = " IF COL_LENGTH('P_ProjectTask', 'PlanTheNextFewDays') IS NULL ALTER TABLE P_ProjectTask ADD PlanTheNextFewDays int; ";
                listSQL.Add(updateSql);
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.37");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.37) Update Error, Please Check The Log!");

            }
            #endregion

            #region 数据权限升级

            #region 1.0.0.38 预置条件表更新
            if (IsUpdateDataBase(oldVersion, "1.0.0.38"))
            {
                List<string> listSQL = new List<string>();
                //预置条件表删除ServiceURLEName字段
                updateSql = " IF COL_LENGTH('RBAC_Preconditions', 'ServiceURLEName') IS NOT NULL ALTER TABLE RBAC_Preconditions drop column ServiceURLEName;  ";
                listSQL.Add(updateSql);
                //预置条件表添加DispOrder字段
                updateSql = " IF COL_LENGTH('RBAC_Preconditions', 'DispOrder') IS NULL ALTER TABLE RBAC_Preconditions ADD DispOrder int;  ";
                listSQL.Add(updateSql);
                //预置条件表添加模块操作Id字段
                updateSql = " IF COL_LENGTH('RBAC_Preconditions', 'ModuleOperID') IS NULL ALTER TABLE RBAC_Preconditions ADD ModuleOperID D_系统主键;  ";
                listSQL.Add(updateSql);
                //预置条件表创建模块操作Id的外键关系
                updateSql = " ALTER TABLE [dbo].[RBAC_Preconditions]  WITH CHECK ADD  CONSTRAINT [FK_RBAC_Preconditions_RBAC_ModuleOper] FOREIGN KEY([ModuleOperID]) REFERENCES [dbo].[RBAC_ModuleOper] ([ModuleOperID]);ALTER TABLE [dbo].[RBAC_Preconditions] CHECK CONSTRAINT [FK_RBAC_Preconditions_RBAC_ModuleOper]; ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.38");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.38) Update Error, Please Check The Log!");

            }
            #endregion

            #region 1.0.0.39 预置条件表添加字段
            if (IsUpdateDataBase(oldVersion, "1.0.0.39"))
            {
                List<string> listSQL = new List<string>();

                //预置条件表添加调用查询的业务对象
                updateSql = " IF COL_LENGTH('RBAC_Preconditions', 'BaseIBLL') IS NULL ALTER TABLE RBAC_Preconditions ADD BaseIBLL  nvarchar(60); ";
                listSQL.Add(updateSql);

                //预置条件表添加关联实体的属性
                updateSql = " IF COL_LENGTH('RBAC_Preconditions', 'EntityAttribute') IS NULL ALTER TABLE RBAC_Preconditions ADD EntityAttribute  nvarchar(60); ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.39");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.39) Update Error, Please Check The Log!");

            }
            #endregion

            #region 1.0.0.40 预置条件表删除/添加字段
            if (IsUpdateDataBase(oldVersion, "1.0.0.40"))
            {
                List<string> listSQL = new List<string>();
                //预置条件表删除ServiceURLEName字段
                updateSql = " IF COL_LENGTH('RBAC_Preconditions', 'ServiceURLEName') IS NOT NULL ALTER TABLE RBAC_Preconditions drop column ServiceURLEName;  ";
                listSQL.Add(updateSql);
                //预置条件表删除ServiceURLCName字段
                updateSql = " IF COL_LENGTH('RBAC_Preconditions', 'ServiceURLCName') IS NOT NULL ALTER TABLE RBAC_Preconditions drop column ServiceURLCName;  ";
                listSQL.Add(updateSql);
                //预置条件表删除EntityAttribute字段
                updateSql = " IF COL_LENGTH('RBAC_Preconditions', 'EntityAttribute') IS NOT NULL ALTER TABLE RBAC_Preconditions drop column EntityAttribute; ";
                listSQL.Add(updateSql);

                //预置条件表添加执行的HQL的属性
                updateSql = " IF COL_LENGTH('RBAC_Preconditions', 'ExecHQL') IS NULL ALTER TABLE RBAC_Preconditions ADD ExecHQL  nvarchar(200); ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.40");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.40) Update Error, Please Check The Log!");

            }
            #endregion

            #region 1.0.0.41 预置条件表删除字段及删除外键约束
            if (IsUpdateDataBase(oldVersion, "1.0.0.41"))
            {
                List<string> listSQL = new List<string>();
                //预置条件表删除外键约束FK_RBAC_Preconditions_RBAC_Module
                updateSql = " if(select name  from  sys.foreign_key_columns f join sys.objects o on f.constraint_object_id=o.object_id  where f.parent_object_id=object_id('RBAC_Preconditions') and name='FK_RBAC_Preconditions_RBAC_Module') is not null alter table RBAC_Preconditions drop constraint FK_RBAC_Preconditions_RBAC_Module; ";
                listSQL.Add(updateSql);
                //预置条件表删除ModuleID字段
                updateSql = " IF COL_LENGTH('RBAC_Preconditions', 'ModuleID') IS NOT NULL ALTER TABLE RBAC_Preconditions drop column ModuleID;  ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.41");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.41) Update Error, Please Check The Log!");

            }
            #endregion

            #region 1.0.0.42 模块服务,预置条件表,行数据条件表,删除外键约束
            if (IsUpdateDataBase(oldVersion, "1.0.0.42"))
            {
                List<string> listSQL = new List<string>();
                //模块服务删除ServiceURLCName字段
                updateSql = " IF COL_LENGTH('RBAC_ModuleOper', 'ServiceURLCName') IS NOT NULL ALTER TABLE RBAC_ModuleOper drop column ServiceURLCName;  ";
                listSQL.Add(updateSql);

                //模块服务删除BaseIBLL字段
                updateSql = " IF COL_LENGTH('RBAC_Preconditions', 'BaseIBLL') IS NOT NULL ALTER TABLE RBAC_Preconditions drop column BaseIBLL;  ";
                listSQL.Add(updateSql);

                //模块服务删除ExecHQL字段
                updateSql = " IF COL_LENGTH('RBAC_Preconditions', 'ExecHQL') IS NOT NULL ALTER TABLE RBAC_Preconditions drop column ExecHQL;  ";
                listSQL.Add(updateSql);

                //预置条件表添加实体编码名称
                updateSql = " IF COL_LENGTH('RBAC_Preconditions', 'EntityCName') IS NULL ALTER TABLE RBAC_Preconditions ADD EntityCName  nvarchar(60); ";
                listSQL.Add(updateSql);
                //预置条件表添加实体编码
                updateSql = " IF COL_LENGTH('RBAC_Preconditions', 'EntityCode') IS NULL ALTER TABLE RBAC_Preconditions ADD EntityCode  nvarchar(60); ";
                listSQL.Add(updateSql);

                //行数据条件表删除外键约束FK_RBAC_RowFilter_RBAC_Preconditions
                //updateSql = " if(select name  from  sys.foreign_key_columns f join sys.objects o on f.constraint_object_id=o.object_id  where f.parent_object_id=object_id('RBAC_RowFilter') and name='FK_RBAC_RowFilter_RBAC_Preconditions') is not null alter table RBAC_RowFilter drop constraint FK_RBAC_RowFilter_RBAC_Preconditions;";
                //listSQL.Add(updateSql);

                ////行数据条件表删除PreconditionsId
                //updateSql = " IF COL_LENGTH('RBAC_RowFilter', 'PreconditionsId') IS NOT NULL ALTER TABLE RBAC_RowFilter drop column PreconditionsId; ";
                //listSQL.Add(updateSql);

                //行数据条件表添加实体编码名称
                updateSql = " IF COL_LENGTH('RBAC_RowFilter', 'EntityCName') IS NULL ALTER TABLE RBAC_RowFilter ADD EntityCName  nvarchar(60);";
                listSQL.Add(updateSql);
                //行数据条件表添加实体编码
                updateSql = " IF COL_LENGTH('RBAC_RowFilter', 'EntityCode') IS NULL ALTER TABLE RBAC_RowFilter ADD EntityCode  nvarchar(60); ";
                listSQL.Add(updateSql);

                //行数据条件表添加是否是预置条件
                updateSql = " IF COL_LENGTH('RBAC_RowFilter', 'IsPreconditions') IS NULL ALTER TABLE RBAC_RowFilter ADD IsPreconditions  bit; ";
                listSQL.Add(updateSql);

                //RBAC_RoleRight删除外键约束FK_RBAC_RoleRight_RBAC_Preconditions
                updateSql = " if(select name  from  sys.foreign_key_columns f join sys.objects o on f.constraint_object_id=o.object_id  where f.parent_object_id=object_id('RBAC_RoleRight') and name='FK_RBAC_RoleRight_RBAC_Preconditions') is not null alter table RBAC_RoleRight drop constraint FK_RBAC_RoleRight_RBAC_Preconditions;";
                listSQL.Add(updateSql);

                //RBAC_RoleRight删除PreconditionsId
                updateSql = " IF COL_LENGTH('RBAC_RoleRight', 'PreconditionsId') IS NOT NULL ALTER TABLE RBAC_RoleRight drop column PreconditionsId; ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.42");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.42) Update Error, Please Check The Log!");

            }
            #endregion

            #endregion

            #region 1.0.0.43 P_Task表添加字段
            if (IsUpdateDataBase(oldVersion, "1.0.0.43"))
            {
                List<string> listSQL = new List<string>();

                //P_Task添加任务分类ID
                updateSql = " IF COL_LENGTH('P_Task', 'PClassID') IS NULL ALTER TABLE P_Task ADD PClassID  D_系统主键; ";
                listSQL.Add(updateSql);

                //P_Task添加任务分类ID
                updateSql = " IF COL_LENGTH('P_Task', 'PClassName') IS NULL ALTER TABLE P_Task ADD PClassName  nvarchar(50); ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.43");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.43) Update Error, Please Check The Log!");

            }
            #endregion

            #region 1.0.0.44 P_ProjectTask表添加字段
            if (IsUpdateDataBase(oldVersion, "1.0.0.44"))
            {
                List<string> listSQL = new List<string>();

                //P_ProjectTask添加PlanTheEndFewDays
                updateSql = " IF COL_LENGTH('P_ProjectTask', 'PlanTheEndFewDays') IS NULL ALTER TABLE P_ProjectTask ADD PlanTheEndFewDays int; ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.44");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.44) Update Error, Please Check The Log!");

            }
            #endregion

            #region 1.0.0.45 P_Contract表添加字段
            if (IsUpdateDataBase(oldVersion, "1.0.0.45"))
            {
                List<string> listSQL = new List<string>();

                //P_ProjectTask添加PlanTheEndFewDays
                updateSql = " IF COL_LENGTH('P_Contract', 'PContractAttrID') IS NULL ALTER TABLE P_Contract ADD PContractAttrID D_系统主键; ";
                listSQL.Add(updateSql);
                updateSql = " IF COL_LENGTH('P_Contract', 'PContractAttrName') IS NULL ALTER TABLE P_Contract ADD PContractAttrName  nvarchar(200); ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.45");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.45) Update Error, Please Check The Log!");

            }
            #endregion

            #region 1.0.0.46 P_Contract表添加字段
            if (IsUpdateDataBase(oldVersion, "1.0.0.46"))
            {
                List<string> listSQL = new List<string>();

                //P_ProjectTask添加PlanTheEndFewDays
                updateSql = " IF COL_LENGTH('P_Contract', 'ServerContractStartDateTime') IS NULL ALTER TABLE P_Contract ADD ServerContractStartDateTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('P_Contract', 'ServerContractEndDateTime') IS NULL ALTER TABLE P_Contract ADD ServerContractEndDateTime  datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('P_Contract', 'OriginalMoneyTotal') IS NULL ALTER TABLE P_Contract ADD OriginalMoneyTotal float; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('P_Contract', 'ServerChargeRatio') IS NULL ALTER TABLE P_Contract ADD ServerChargeRatio  float; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('P_Contract', 'PlanSignDateTime') IS NULL ALTER TABLE P_Contract ADD PlanSignDateTime datetime; ";
                listSQL.Add(updateSql);


                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.46");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.46) Update Error, Please Check The Log!");

            }
            #endregion

            #region 1.0.0.47 角色权限表新增预置条件Id字段及创建预置条件Id外键关系
            #region 数据权限升级
            if (IsUpdateDataBase(oldVersion, "1.0.0.47"))
            {
                List<string> listSQL = new List<string>();
                //角色权限添加预置条件Id字段
                updateSql = " IF COL_LENGTH('RBAC_RoleRight', 'PreconditionsId') IS NULL ALTER TABLE RBAC_RoleRight ADD PreconditionsId D_系统主键; ";
                listSQL.Add(updateSql);
                //角色权限创建预置条件Id与预置条件表的外键关系
                updateSql = " if(select name  from  sys.foreign_key_columns f join sys.objects o on f.constraint_object_id=o.object_id  where f.parent_object_id=object_id('RBAC_RoleRight') and name='FK_RBAC_RoleRight_RBAC_Preconditions') is  null ALTER TABLE [dbo].[RBAC_RoleRight]  WITH CHECK ADD  CONSTRAINT [FK_RBAC_RoleRight_RBAC_Preconditions] FOREIGN KEY([PreconditionsId]) REFERENCES [dbo].[RBAC_Preconditions] ([Id]);ALTER TABLE [dbo].[RBAC_RoleRight] CHECK CONSTRAINT [FK_RBAC_RoleRight_RBAC_Preconditions];";
                listSQL.Add(updateSql);
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.47");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.47) Update Error, Please Check The Log!");

            }
            #endregion

            #endregion

            #region 1.0.0.48 新增表P_Contract_Follow,P_Contract_Follow_Interaction
            if (IsUpdateDataBase(oldVersion, "1.0.0.48"))
            {
                List<string> listSQL = new List<string>();

                //P_ProjectTask添加PlanTheEndFewDays
                updateSql = "  CREATE TABLE [dbo].[P_Contract_Follow]( 	[LabID] [dbo].[D_系统主键] NULL, 	[PContractFollowID] [dbo].[D_系统主键] NOT NULL, 	[PClientID] [dbo].[D_系统主键] NULL, 	[PClientName] [varchar](200) NULL, 	[PayOrgID] [bigint] NULL, 	[PayOrg] [varchar](200) NULL, 	[ContractNumber] [varchar](200) NULL, 	[Name] [varchar](40) NULL, 	[SName] [varchar](40) NULL, 	[Shortcode] [dbo].[D_快捷码] NULL, 	[PinYinZiTou] [dbo].[D_汉语拼音字头] NULL, 	[DispOrder] [int] NULL, 	[Comment] [varchar](max) NULL, 	[IsUse] [bit] NULL, 	[DataAddTime] [datetime] NULL, 	[DataTimeStamp] [timestamp] NULL, 	[Amount] [float] NULL, 	[PayedMoney] [float] NULL, 	[LeftMoney] [float] NULL, 	[RcvedRecord] [varchar](200) NULL, 	[RcvedRatio] [float] NULL, 	[InvoiceMemoHTML] [varchar](max) NULL, 	[InvoiceMemo] [varchar](200) NULL, 	[Attention] [varchar](200) NULL, 	[SpecialDemand] [varchar](200) NULL, 	[NewLinkEquipCost] [varchar](200) NULL, 	[ImplStartTime] [datetime] NULL, 	[ImplFinishTime] [datetime] NULL, 	[Contract] [varchar](200) NULL, 	[AttachModule] [varchar](200) NULL, 	[FeeInCarrying] [float] NULL, 	[Profit] [float] NULL, 	[FeeBefore] [float] NULL, 	[Content] [varchar](200) NULL, 	[IsChange] [bit] NULL, 	[ChangeDesc] [varchar](200) NULL, 	[LabFinishTime] [datetime] NULL, 	[RemainingProblems] [varchar](200) NULL, 	[HasHIS] [varchar](200) NULL, 	[CustomCost] [float] NULL, 	[PManagerID] [dbo].[D_系统主键] NULL, 	[PManager] [varchar](200) NULL, 	[HISFirm] [varchar](200) NULL, 	[PurchaseDescHTML] [varchar](max) NULL, 	[PurchaseDesc] [varchar](200) NULL, 	[ImplFirm] [varchar](200) NULL, 	[PaidServiceStartTime] [datetime] NULL, 	[RegularLinkEquip] [float] NULL, 	[PartyAName] [varchar](200) NULL, 	[Software] [float] NULL, 	[Hardware] [float] NULL, 	[SignedAnnualService] [varchar](200) NULL, 	[AnnualServiceCharge] [float] NULL, 	[BusinessExpenses] [float] NULL, 	[Emphases] [varchar](200) NULL, 	[SystemStartRunTime] [datetime] NULL, 	[PurchaseFee] [float] NULL, 	[AcceptDate] [datetime] NULL, 	[LinkEquipInfoListHTML] [nvarchar](max) NULL, 	[LinkEquipInfoList] [varchar](200) NULL, 	[HISDemand] [varchar](200) NULL, 	[AcceptanceDescNo] [varchar](200) NULL, 	[NoteBfImplHTML] [varchar](max) NULL, 	[NoteBfImpl] [varchar](200) NULL, 	[NeedStartDate] [datetime] NULL, 	[NeedEndDate] [datetime] NULL, 	[ImplDays] [float] NULL, 	[MiddleFee] [float] NULL, 	[ContractServiceCharge] [varchar](200) NULL, 	[ProjectLinkMan] [varchar](200) NULL, 	[LinkMan] [varchar](200) NULL, 	[LinkPhoneNo] [varchar](200) NULL, 	[InstallationTime] [datetime] NULL, 	[AgreedPayTime] [datetime] NULL, 	[RealityPayTime] [datetime] NULL, 	[IsInvoices] [varchar](200) NULL, 	[LicenseExpirationTime] [datetime] NULL, 	[LicenseDeferredTime] [datetime] NULL, 	[TotalDelay] [varchar](200) NULL, 	[NewLinkEquipName] [varchar](200) NULL, 	[ServiceYear] [varchar](56) NULL, 	[FreeServiceDate] [varchar](200) NULL, 	[OtherPaidExpenses] [float] NULL, 	[ProvinceID] [dbo].[D_系统主键] NULL, 	[ProvinceName] [varchar](200) NULL, 	[DeptID] [dbo].[D_系统主键] NULL, 	[DeptName] [varchar](200) NULL, 	[CompnameID] [dbo].[D_系统主键] NULL, 	[Compname] [nvarchar](100) NULL, 	[PrincipalID] [dbo].[D_系统主键] NULL, 	[Principal] [varchar](200) NULL, 	[ContractStatus] [varchar](200) NULL, 	[ApplyManID] [dbo].[D_系统主键] NULL, 	[ApplyMan] [varchar](200) NULL, 	[ApplyDate] [datetime] NULL, 	[ReviewManID] [dbo].[D_系统主键] NULL, 	[ReviewMan] [varchar](200) NULL, 	[ReviewDate] [datetime] NULL, 	[ReviewInfo] [varchar](500) NULL, 	[SignManID] [dbo].[D_系统主键] NULL, 	[SignMan] [varchar](200) NULL, 	[SignDate] [datetime] NULL, 	[AllHtmlInfo] [varchar](max) NULL, 	[InvoiceMoney] [float] NULL, 	[TechReviewManID] [bigint] NULL, 	[TechReviewMan] [varchar](200) NULL, 	[TechReviewDate] [datetime] NULL, 	[TechReviewInfo] [varchar](500) NULL, 	[EquipOneWayCount] [int] NULL, 	[EquipTwoWayCount] [int] NULL, 	[ReceiveName] [varchar](100) NULL, 	[ReceiveAddress] [varchar](200) NULL, 	[ReceivePhoneNumbers] [varchar](20) NULL, 	[FreightName] [varchar](200) NULL, 	[FreightOddNumbers] [varchar](200) NULL, 	[ContentID] [bigint] NULL, 	[ContrastId] [bigint] NULL, 	[ContrastCName] [varchar](60) NULL, 	[CheckId] [bigint] NULL, 	[CheckCName] [varchar](60) NULL, 	[PContractAttrID] [dbo].[D_系统主键] NULL, 	[PContractAttrName] [nvarchar](200) NULL, 	[ServerContractStartDateTime] [datetime] NULL, 	[ServerContractEndDateTime] [datetime] NULL, 	[OriginalMoneyTotal] [float] NULL, 	[ServerChargeRatio] [float] NULL, 	[PlanSignDateTime] [datetime] NULL,  CONSTRAINT [PK_P_Contract_Follow_1] PRIMARY KEY CLUSTERED  ( 	[PContractFollowID] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]    ALTER TABLE [dbo].[P_Contract_Follow]  WITH CHECK ADD  CONSTRAINT [FK_P_Contract_Follow_B_Province] FOREIGN KEY([ProvinceID]) REFERENCES [dbo].[B_Province] ([ProvinceID])   ALTER TABLE [dbo].[P_Contract_Follow] CHECK CONSTRAINT [FK_P_Contract_Follow_B_Province]   ALTER TABLE [dbo].[P_Contract_Follow]  WITH CHECK ADD  CONSTRAINT [FK_P_Contract_Follow_HR_Dept] FOREIGN KEY([DeptID]) REFERENCES [dbo].[HR_Dept] ([DeptID])   ALTER TABLE [dbo].[P_Contract_Follow] CHECK CONSTRAINT [FK_P_Contract_Follow_HR_Dept]   ALTER TABLE [dbo].[P_Contract_Follow]  WITH CHECK ADD  CONSTRAINT [FK_P_Contract_Follow_HR_Employee] FOREIGN KEY([PManagerID]) REFERENCES [dbo].[HR_Employee] ([EmpID])   ALTER TABLE [dbo].[P_Contract_Follow] CHECK CONSTRAINT [FK_P_Contract_Follow_HR_Employee]   ALTER TABLE [dbo].[P_Contract_Follow]  WITH CHECK ADD  CONSTRAINT [FK_P_Contract_Follow_HR_Employee1] FOREIGN KEY([PrincipalID]) REFERENCES [dbo].[HR_Employee] ([EmpID])   ALTER TABLE [dbo].[P_Contract_Follow] CHECK CONSTRAINT [FK_P_Contract_Follow_HR_Employee1]   ALTER TABLE [dbo].[P_Contract_Follow]  WITH CHECK ADD  CONSTRAINT [FK_P_Contract_Follow_HR_Employee2] FOREIGN KEY([SignManID]) REFERENCES [dbo].[HR_Employee] ([EmpID])   ALTER TABLE [dbo].[P_Contract_Follow] CHECK CONSTRAINT [FK_P_Contract_Follow_HR_Employee2]   ALTER TABLE [dbo].[P_Contract_Follow]  WITH CHECK ADD  CONSTRAINT [FK_P_Contract_Follow_HR_Employee3] FOREIGN KEY([ApplyManID]) REFERENCES [dbo].[HR_Employee] ([EmpID])   ALTER TABLE [dbo].[P_Contract_Follow] CHECK CONSTRAINT [FK_P_Contract_Follow_HR_Employee3]   ALTER TABLE [dbo].[P_Contract_Follow]  WITH CHECK ADD  CONSTRAINT [FK_P_Contract_Follow_HR_Employee4] FOREIGN KEY([ReviewManID]) REFERENCES [dbo].[HR_Employee] ([EmpID])   ALTER TABLE [dbo].[P_Contract_Follow] CHECK CONSTRAINT [FK_P_Contract_Follow_HR_Employee4]   ALTER TABLE [dbo].[P_Contract_Follow]  WITH CHECK ADD  CONSTRAINT [FK_P_Contract_Follow_HR_Employee5] FOREIGN KEY([TechReviewManID]) REFERENCES [dbo].[HR_Employee] ([EmpID])   ALTER TABLE [dbo].[P_Contract_Follow] CHECK CONSTRAINT [FK_P_Contract_Follow_HR_Employee5]   ALTER TABLE [dbo].[P_Contract_Follow]  WITH CHECK ADD  CONSTRAINT [FK_P_Contract_Follow_P_Client] FOREIGN KEY([PayOrgID]) REFERENCES [dbo].[P_Client] ([PClientID])   ALTER TABLE [dbo].[P_Contract_Follow] CHECK CONSTRAINT [FK_P_Contract_Follow_P_Client]   ALTER TABLE [dbo].[P_Contract_Follow]  WITH CHECK ADD  CONSTRAINT [FK_P_Contract_Follow_P_Client1] FOREIGN KEY([PClientID]) REFERENCES [dbo].[P_Client] ([PClientID])   ALTER TABLE [dbo].[P_Contract_Follow] CHECK CONSTRAINT [FK_P_Contract_Follow_P_Client1]   ALTER TABLE [dbo].[P_Contract_Follow]  WITH CHECK ADD  CONSTRAINT [FK_P_Contract_Follow_P_Dict] FOREIGN KEY([CompnameID]) REFERENCES [dbo].[P_Dict] ([DictID])   ALTER TABLE [dbo].[P_Contract_Follow] CHECK CONSTRAINT [FK_P_Contract_Follow_P_Dict]   ALTER TABLE [dbo].[P_Contract_Follow]  WITH CHECK ADD  CONSTRAINT [FK_P_Contract_Follow_P_Dict_ContentID] FOREIGN KEY([ContentID]) REFERENCES [dbo].[P_Dict] ([DictID])   ALTER TABLE [dbo].[P_Contract_Follow] CHECK CONSTRAINT [FK_P_Contract_Follow_P_Dict_ContentID]   ALTER TABLE [dbo].[P_Contract_Follow]  WITH CHECK ADD  CONSTRAINT [FK_P_Contract_Follow_S_ServiceClient1] FOREIGN KEY([LabID]) REFERENCES [dbo].[S_ServiceClient] ([LabID])   ALTER TABLE [dbo].[P_Contract_Follow] CHECK CONSTRAINT [FK_P_Contract_Follow_S_ServiceClient1]   ALTER TABLE [dbo].[P_Contract_Follow]  WITH CHECK ADD  CONSTRAINT [FK_P_Contract_Follow_S_ServiceClient2] FOREIGN KEY([LabID]) REFERENCES [dbo].[S_ServiceClient] ([LabID])   ALTER TABLE [dbo].[P_Contract_Follow] CHECK CONSTRAINT [FK_P_Contract_Follow_S_ServiceClient2]   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台客户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'LabID'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'客户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'PClientID'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'PClientName'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'付款单位ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'PayOrgID'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'付款单位' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'PayOrg'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'ContractNumber'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'Name'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'简称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'SName'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'快捷码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'Shortcode'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'汉字拼音字头' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'PinYinZiTou'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示次序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'DispOrder'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'Comment'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'IsUse'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'DataAddTime'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'DataTimeStamp'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同总额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'Amount'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已收金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'PayedMoney'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'剩余款项' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'LeftMoney'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收付款记录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'RcvedRecord'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收款比例' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'RcvedRatio'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发票备注HTML' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'InvoiceMemoHTML'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发票备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'InvoiceMemo'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关注程度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'Attention'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'特殊功能' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'SpecialDemand'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同新联机费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'NewLinkEquipCost'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实施起始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'ImplStartTime'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实施结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'ImplFinishTime'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同文本' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'Contract'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'附加模块' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'AttachModule'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实施费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'FeeInCarrying'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目毛利' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'Profit'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'售前费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'FeeBefore'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目类别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'Content'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同有无变更' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'IsChange'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'变更说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'ChangeDesc'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'科内完成时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'LabFinishTime'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'遗留问题' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'RemainingProblems'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否有HIS接口' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'HasHIS'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'定制费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'CustomCost'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实施负责人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'PManagerID'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实施负责人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'PManager'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'HIS厂商' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'HISFirm'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采购说明HTML' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'PurchaseDescHTML'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采购说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'PurchaseDesc'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实施方' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'ImplFirm'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'有偿服务应开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'PaidServiceStartTime'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'规定连机数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'RegularLinkEquip'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'甲方名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'PartyAName'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'软件' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'Software'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'硬件' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'Hardware'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务签署年数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'SignedAnnualService'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'每年服务费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'AnnualServiceCharge'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'商务费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'BusinessExpenses'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'重点' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'Emphases'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统投入运行时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'SystemStartRunTime'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'采购费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'PurchaseFee'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'验收时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'AcceptDate'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仪器连机清单HTML' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'LinkEquipInfoListHTML'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仪器连机清单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'LinkEquipInfoList'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'HIS接口要求' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'HISDemand'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'验收说明(编号)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'AcceptanceDescNo'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工程注意事项HTML' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'NoteBfImplHTML'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工程注意事项' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'NoteBfImpl'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'要求实施时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'NeedStartDate'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'要求完成时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'NeedEndDate'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实际实施天数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'ImplDays'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'其它费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'MiddleFee'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同服务费用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'ContractServiceCharge'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目联系人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'ProjectLinkMan'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'LinkMan'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'联系电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'LinkPhoneNo'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'安装时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'InstallationTime'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'约定回款时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'AgreedPayTime'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实际回款时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'RealityPayTime'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否开具发票' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'IsInvoices'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权到期时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'LicenseExpirationTime'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权延期至' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'LicenseDeferredTime'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'累积延期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'TotalDelay'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'新连机仪器名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'NewLinkEquipName'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'服务年份' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'ServiceYear'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'免费服务期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'FreeServiceDate'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'其它费用已付' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'OtherPaidExpenses'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'省份ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'ProvinceID'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'省份' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'ProvinceName'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属部门' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'DeptName'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本公司名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'Compname'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'销售负责人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'PrincipalID'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'销售负责人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'Principal'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'ContractStatus'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'ApplyMan'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'申请时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'ApplyDate'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评审人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'ReviewManID'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评审人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'ReviewMan'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评审时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'ReviewDate'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'评审人意见' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'ReviewInfo'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'签署人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'SignManID'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'签署人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'SignMan'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'签署日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'SignDate'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'附加信息' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'AllHtmlInfo'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'已开发票金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'InvoiceMoney'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'技术评审人ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'TechReviewManID'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'技术评审人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'TechReviewMan'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'技术评审时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'TechReviewDate'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'技术评审人意见' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'TechReviewInfo'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收货人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'ReceiveName'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收货人地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'ReceiveAddress'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收货人电话' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'ReceivePhoneNumbers'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'货运公司名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'FreightName'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'货运单号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow', @level2type=N'COLUMN',@level2name=N'FreightOddNumbers'    ";
                listSQL.Add(updateSql);

                updateSql = "  CREATE TABLE [dbo].[P_Contract_Follow_Interaction]( 	[LabID] [dbo].[D_系统主键] NULL, 	[PContractFollowInteractionID] [dbo].[D_系统主键] NOT NULL, 	[SenderID] [dbo].[D_系统主键] NOT NULL, 	[ReceiverID] [dbo].[D_系统主键] NULL, 	[PContractFollowID] [dbo].[D_系统主键] NULL, 	[Contents] [varchar](max) NULL, 	[DataAddTime] [datetime] NOT NULL, 	[IsUse] [bit] NULL, 	[Memo] [varchar](500) NULL, 	[HasAttachment] [bit] NULL, 	[SenderName] [varchar](50) NULL, 	[ReceiverName] [varchar](50) NULL, 	[DataTimeStamp] [timestamp] NULL,  CONSTRAINT [PK_F_P_Contract_Follow_Interaction] PRIMARY KEY CLUSTERED  ( 	[PContractFollowInteractionID] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]        ALTER TABLE [dbo].[P_Contract_Follow_Interaction]  WITH CHECK ADD  CONSTRAINT [FK_P_Contract_Follow_Interaction_HR_Employee] FOREIGN KEY([ReceiverID]) REFERENCES [dbo].[HR_Employee] ([EmpID])   ALTER TABLE [dbo].[P_Contract_Follow_Interaction] CHECK CONSTRAINT [FK_P_Contract_Follow_Interaction_HR_Employee]   ALTER TABLE [dbo].[P_Contract_Follow_Interaction]  WITH CHECK ADD  CONSTRAINT [FK_P_Contract_Follow_Interaction_HR_Employee1] FOREIGN KEY([SenderID]) REFERENCES [dbo].[HR_Employee] ([EmpID])   ALTER TABLE [dbo].[P_Contract_Follow_Interaction] CHECK CONSTRAINT [FK_P_Contract_Follow_Interaction_HR_Employee1]   ALTER TABLE [dbo].[P_Contract_Follow_Interaction]  WITH CHECK ADD  CONSTRAINT [FK_P_Contract_Follow_Interaction_S_ServiceClient] FOREIGN KEY([LabID]) REFERENCES [dbo].[S_ServiceClient] ([LabID])   ALTER TABLE [dbo].[P_Contract_Follow_Interaction] CHECK CONSTRAINT [FK_P_Contract_Follow_Interaction_S_ServiceClient]   ALTER TABLE [dbo].[P_Contract_Follow_Interaction]  WITH CHECK ADD  CONSTRAINT [FK_P_Contract_Follow_Interaction_P_Contract_Follow] FOREIGN KEY([PContractFollowID]) REFERENCES [dbo].[P_Contract_Follow] ([PContractFollowID])   ALTER TABLE [dbo].[P_Contract_Follow_Interaction] CHECK CONSTRAINT [FK_P_Contract_Follow_Interaction_P_Contract_Follow]   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台客户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow_Interaction', @level2type=N'COLUMN',@level2name=N'LabID'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'互动主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow_Interaction', @level2type=N'COLUMN',@level2name=N'PContractFollowInteractionID'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发件人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow_Interaction', @level2type=N'COLUMN',@level2name=N'SenderID'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow_Interaction', @level2type=N'COLUMN',@level2name=N'ReceiverID'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目主键ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow_Interaction', @level2type=N'COLUMN',@level2name=N'PContractFollowID'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow_Interaction', @level2type=N'COLUMN',@level2name=N'Contents'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'新增时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow_Interaction', @level2type=N'COLUMN',@level2name=N'DataAddTime'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow_Interaction', @level2type=N'COLUMN',@level2name=N'IsUse'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow_Interaction', @level2type=N'COLUMN',@level2name=N'Memo'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否带附件' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow_Interaction', @level2type=N'COLUMN',@level2name=N'HasAttachment'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发件人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow_Interaction', @level2type=N'COLUMN',@level2name=N'SenderName'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收件人姓名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow_Interaction', @level2type=N'COLUMN',@level2name=N'ReceiverName'   EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'P_Contract_Follow_Interaction', @level2type=N'COLUMN',@level2name=N'DataTimeStamp'      ";
                listSQL.Add(updateSql);


                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.48");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.48) Update Error, Please Check The Log!");

            }
            #endregion

            #region 1.0.0.49 //QMS升级质量记录升级存储过程和增加仪器字段
            if (IsUpdateDataBase(oldVersion, "1.0.0.49"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " if Not Exists(Select * from SysColumns where [Name]= \'EquipID\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_Attachment\' ))" + "\r" +
                            " begin " + "\r" +
                            "   Alter Table E_Attachment Add EquipID bigint " + "\r" +
                            "   EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'仪器\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                            "   @level1type = N\'TABLE\',@level1name = N\'E_Attachment\', @level2type = N\'COLUMN\',@level2name = N\'EquipID\'" + "\r" +
                            "   ALTER TABLE[dbo].[E_Attachment]  WITH CHECK ADD CONSTRAINT[FK_E_Attachment_Equip] FOREIGN KEY([EquipID]) REFERENCES[dbo].[E_Equip] ([EquipID]) " + "\r" +
                            " end ";
                listSQL.Add(updateSql);

                updateSql = " if Not Exists(Select * from SysColumns where [Name]= \'EquipID\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_ReportData\' ))" + "\r" +
                            " begin " + "\r" +
                            "   Alter Table E_ReportData Add EquipID bigint " + "\r" +
                            "   EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'仪器\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                            "   @level1type = N\'TABLE\',@level1name = N\'E_ReportData\', @level2type = N\'COLUMN\',@level2name = N\'EquipID\'" + "\r" +
                            "   ALTER TABLE[dbo].[E_ReportData] WITH CHECK ADD CONSTRAINT[FK_E_ReportData_E_EquipID] FOREIGN KEY([EquipID]) REFERENCES[dbo].[E_Equip] ([EquipID]) " + "\r" +
                            " end ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from dbo.sysobjects where id = object_id(N\'[dbo].[P_GetReportData]\') and OBJECTPROPERTY(id, N\'IsProcedure\') = 1)" + "\r" +
                            " DROP PROCEDURE [dbo].[P_GetReportData] ";
                listSQL.Add(updateSql);

                updateSql =  " CREATE PROCEDURE [dbo].[P_GetReportData] " + "\r" +
                             " @TempletID varchar(50), " + "\r" +
                             " @EquipID varchar(50), " + "\r" +
                             " @EmpID varchar(50), " + "\r" +
                             " @BeginDate datetime, " + "\r" +
                             " @EndDate datetime, " + "\r" +
                             " @IsCheck varchar(50) " + "\r" +
                             " AS " + "\r" +
                             " BEGIN " + "\r" +
                             "     declare  @StrMonth varchar(50) " + "\r" +
                             "     declare  @Where varchar(500) " + "\r" +
                             "     declare  @SQL varchar(5000) " + "\r" +
                             "     declare  @MonthSQL varchar(5000) " + "\r" +
                             "     declare  @MonthCount int " + "\r" +
                             "     set @Where = '' " + "\r" +
                             "     set @MonthSQL = '' " + "\r" +
                             "     set @MonthCount = cast((datediff(month, @BeginDate, @EndDate)) as int) " + "\r" +
                             "     if (@MonthCount >= 0) " + "\r" +
                             "     begin " + "\r" +
                             "       while (@MonthCount >= 0) " + "\r" +
                             "       begin " + "\r" +
                             "         set @StrMonth = CONVERT(varchar(8), DATEADD(MONTH, @MonthCount, @BeginDate), 120) + '01' " + "\r" +
                             "            if (@MonthSQL = '') " + "\r" +
                             "              set @MonthSQL = 'select TempletID, EmpID, ''' + @StrMonth + ''' as ReportDate from E_TempletEmp where EmpID =' + @EmpID " + "\r" +
                             " 		   else " + "\r" +
                             "              set @MonthSQL = @MonthSQL + ' union select TempletID, EmpID, ''' + @StrMonth + ''' as ReportDate from E_TempletEmp where EmpID =' + @EmpID " + "\r" +
                             "            set @MonthCount = @MonthCount - 1 " + "\r" +
                             "       end " + "\r" +
                             "       if (@MonthSQL <> '') " + "\r" +
                             "       begin " + "\r" +
                             "         if (@TempletID <> '') " + "\r" +
                             "           set @Where = @Where + ' and ETP.TempletID=' + @TempletID " + "\r" +
                             "         if (@EquipID <> '') " + "\r" +
                             "           set @Where = @Where + ' and ERD.EquipID=' + @EquipID " + "\r" +
                             "         if (@IsCheck <> '') " + "\r" +
                             "         begin " + "\r" +
                             "            if (@IsCheck = '0') " + "\r" +
                             "              set @Where = @Where + ' and (ERD.IsCheck is null or ERD.IsCheck=' + @IsCheck + ')' " + "\r" +
                             "  		  else " + "\r" +
                             "              set @Where = @Where + ' and ERD.IsCheck=' + @IsCheck " + "\r" +
                             "         end " + "\r" +
                             "         set @SQL = \'select ROW_NUMBER() OVER(ORDER BY TempletID ASC) AS Id, *  \' + " + "\r" +
                             "                     \'from(  \' + " + "\r" +
                             "                         \'select ETP.TempletID, ET.EquipID, ETP.ReportDate, ET.CName as ReportName, \' + " + "\r" +
                             "                         \'ERD.ReportDataID, ERD.LabID, ERD.ReportFilePath, ERD.ReportFileExt, ERD.IsCheck, \' + " + "\r" +
                             "                         \'ERD.Checker, ERD.CheckTime, ERD.CheckView, ERD.Comment, ERD.IsUse, \' + " + "\r" +
                             "                         \'ERD.DispOrder, ERD.DataAddTime, ERD.DataUpdateTime, EE.CName as EquipName, \' + " + "\r" +
                             "                         \'EE.UseCode as EUseCode, EE.Shortcode as EShortcode, EE.EName as EEName, \' + " + "\r" +
                             "                         \'PD.CName as EquipTypeName, ET.SectionID, HRD.CName as SectionName, \' + " + "\r" +
                             "                         \'HRD.UseCode, HRD.Shortcode, HRD.StandCode, HRD.EName, \' + " + "\r" +
                             "                         \'dbo.F_GetReportAttachment(ETP.TempletID, ET.EquipID, ETP.ReportDate, ETP.ReportDate) as IsAttachment  \' + " + "\r" +
                             "                         \'from(\' + @MonthSQL + \') ETP   \' + " + "\r" +
                             "                         \'left join E_ReportData ERD on ETP.TempletID = ERD.TempletID and ETP.ReportDate = ERD.ReportDate  \' + " + "\r" +
                             "                         \'left join E_Templet ET on ET.TempletID = ETP.TempletID  \' + " + "\r" +
                             "                         \'left join E_Equip EE on EE.EquipID = ET.EquipID  \' + " + "\r" +
                             "                         \'left join P_Dict PD on EE.EquipTypeID = PD.DictID  \' + " + "\r" +
                             "                         \'left join HR_Dept HRD on ET.SectionID = HRD.DeptID   \' + " + "\r" +
                             "                         \'where 1=1 \' + @Where + " + "\r" +
                             "                     \') MM order by ReportDate, ReportName ASC \' " + "\r" +
                             "         exec(@SQL) " + "\r" +
                             "       end " + "\r" +
                             "     end " + "\r" +
                             " END ";
                listSQL.Add(updateSql);

                updateSql = " IF OBJECT_ID (N\'F_GetReportAttachment\') IS NOT NULL " + "\r" +
                            " DROP FUNCTION [F_GetReportAttachment] ";
                listSQL.Add(updateSql);

                updateSql = " CREATE FUNCTION [dbo].[F_GetReportAttachment] " + "\r" +
                            " (@TempletID bigint, @EquipID bigint, @BeginDate datetime, @EndDate datetime) " + "\r" +
                            " RETURNS VARCHAR(50) AS " + "\r" +
                            " BEGIN " + "\r" +
                            "   Declare @AttachmentCount  Int " + "\r" +
                            "   set @BeginDate = CONVERT(varchar(8), @BeginDate, 120) + '01' " + "\r" +
                            "   set @EndDate = dateadd(d, -day(@BeginDate), dateadd(m, 1, @BeginDate)) " + "\r" +
                            "   set @AttachmentCount = 0 " + "\r" +
                            "   select @AttachmentCount = count(*) from E_Attachment " + "\r" +
                            "   where TempletID = @TempletID and(EquipID is null or EquipID = @EquipID) " + "\r" +
                            "   and FileUploadDate>@BeginDate and FileUploadDate<@EndDate " + "\r" +
                            "   return @AttachmentCount " + "\r" +
                            " end ";
                listSQL.Add(updateSql);
                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.49");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.49) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.50 //QMS升级质量记录修改查询列表数据存储过程
            if (IsUpdateDataBase(oldVersion, "1.0.0.50"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " if exists(select * from dbo.sysobjects where id = object_id(N\'[dbo].[P_GetMaintenanceDataTB]\') and OBJECTPROPERTY(id, N\'IsProcedure\') = 1)" + "\r" +
                            " DROP PROCEDURE[dbo].[P_GetMaintenanceDataTB] ";
                listSQL.Add(updateSql);

                updateSql = " CREATE PROCEDURE[dbo].[P_GetMaintenanceDataTB] " + "\r" +
                             " @TempletID varchar(50), " + "\r" +
                             " @BatchNumber varchar(50), " + "\r" +
                             " @SQLPara varchar(8000), " + "\r" +
                             " @BeginDate varchar(50), " + "\r" +
                             " @EndDate varchar(50) " + "\r" +
                             " AS " + "\r" +
                             " BEGIN " + "\r" +
                             "   declare  @SQL varchar(8000) " + "\r" +
                             "   declare  @where varchar(2000) " + "\r" +
                             "   set @SQL = \'\' " + "\r" +
                             "   set @where = \' where TempletID=\' + @TempletID " + "\r" +
                             "   if (@BatchNumber <> \'\') " + "\r" +
                             "     set @where = @where + \' and @BatchNumber=\'\'\' + @BatchNumber + \'\'\'\' " + "\r" +
                             "   set @SQL = \' Select TempletID,BatchNumber,ItemDate as \'\'操作日期\'\' \' + @SQLPara + " + "\r" +
                             "              \' from (Select * from E_MaintenanceData \' + @where + " + "\r" +
                             "              \' and ItemDate<\'\'\' + @EndDate + \'\'\'and ItemDate>=\'\'\' + @BeginDate + \'\'\') mm\' + " + "\r" +
                             "              \' group by TempletID,ItemDate,BatchNumber	\' " + "\r" +
                             "    exec(@sql) " + "\r" +
                            " END ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.50");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.50) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.51 //QMS升级质量记录增加职责表
            if (IsUpdateDataBase(oldVersion, "1.0.0.51"))
            {
                List<string> listSQL = new List<string>();
                updateSql = " if not exists (select * from dbo.sysobjects where id = object_id(N\'[dbo].[E_Responsibility]\')" + "\r" +
                            " and OBJECTPROPERTY(id, N\'IsUserTable\') = 1) BEGIN " + "\r" +
                            " CREATE TABLE [dbo].[E_Responsibility]( " + "\r" +
                                " [LabID] [bigint] NOT NULL,[ResponsibilityID] [bigint] NOT NULL,[CName] [varchar](200) NULL,[EName] [varchar](200) NULL,[SName] [varchar](200) NULL,[Shortcode] [varchar](200) NULL,[PinYinZiTou] [varchar](200) NULL,[Comment] [ntext] NULL,[IsUse] [bit] NULL,[DispOrder] [int] NULL,[DataAddTime] [datetime] NULL,[DataUpdateTime] [datetime] NULL,[DataTimeStamp] [timestamp] NULL, " + "\r" +
                            " CONSTRAINT [PK_E_Responsibility] PRIMARY KEY CLUSTERED " + "\r" +
                            " ( [ResponsibilityID] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] " + "\r" +
                            " ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'实验室ID\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'E_Responsibility\', @level2type=N\'COLUMN\',@level2name=N\'LabID\' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'职责ID\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'E_Responsibility\', @level2type=N\'COLUMN\',@level2name=N\'ResponsibilityID\' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'职责名称\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'E_Responsibility\', @level2type=N\'COLUMN\',@level2name=N\'CName\' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'英文名称\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'E_Responsibility\', @level2type=N\'COLUMN\',@level2name=N\'EName\' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'简称\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'E_Responsibility\', @level2type=N\'COLUMN\',@level2name=N\'SName\' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'快捷码\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'E_Responsibility\', @level2type=N\'COLUMN\',@level2name=N\'Shortcode\' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'汉字拼音字头\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'E_Responsibility\', @level2type=N\'COLUMN\',@level2name=N\'PinYinZiTou\' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'备注\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'E_Responsibility\', @level2type=N\'COLUMN\',@level2name=N\'Comment\' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'是否使用\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'E_Responsibility\', @level2type=N\'COLUMN\',@level2name=N\'IsUse\' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'显示次序\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'E_Responsibility\', @level2type=N\'COLUMN\',@level2name=N\'DispOrder\' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'创建时间\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'E_Responsibility\', @level2type=N\'COLUMN\',@level2name=N\'DataAddTime\' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'修改时间\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'E_Responsibility\', @level2type=N\'COLUMN\',@level2name=N\'DataUpdateTime\' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'时间戳\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'E_Responsibility\', @level2type=N\'COLUMN\',@level2name=N\'DataTimeStamp\' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'质量记录人员职责表\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'E_Responsibility\' " + "\r" +
                            " end  ";
                listSQL.Add(updateSql);
                updateSql = " if not exists (select * from dbo.sysobjects where id = object_id(N\'[dbo].[E_ResEmp]\')" + "\r" +
                            " and OBJECTPROPERTY(id, N\'IsUserTable\') = 1) BEGIN " + "\r" +
                            " CREATE TABLE [dbo].[E_ResEmp](  " + "\r" +
                            " 	[LabID] [bigint] NULL,[ResEmpID] [bigint] NOT NULL,[ResponsibilityID] [bigint] NULL,[EmpID] [bigint] NULL,[IsUse] [bit] NULL,[DispOrder] [int] NULL,[DataAddTime] [datetime] NULL,[DataUpdateTime] [datetime] NULL,[DataTimeStamp] [timestamp] NULL, " + "\r" +
                            "  CONSTRAINT [PK_E_ResEmp] PRIMARY KEY NONCLUSTERED  " + "\r" +
                            " ([ResEmpID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]  " + "\r" +
                            " ) ON [PRIMARY]  " + "\r" +
                            " ALTER TABLE [dbo].[E_ResEmp]  WITH CHECK ADD  CONSTRAINT [FK_E_ResEmp_REFERENCE_E_Responsibility] FOREIGN KEY([ResponsibilityID]) REFERENCES [dbo].[E_Responsibility] ([ResponsibilityID]) " + "\r" +
                            " ALTER TABLE [dbo].[E_ResEmp] CHECK CONSTRAINT [FK_E_ResEmp_REFERENCE_E_Responsibility] " + "\r" +
                            " ALTER TABLE [dbo].[E_ResEmp]  WITH CHECK ADD  CONSTRAINT [FK_E_ResEmp_REFERENCE_HR_Employee] FOREIGN KEY([EmpID]) REFERENCES [dbo].[HR_Employee] ([EmpID])  " + "\r" +
                            " ALTER TABLE [dbo].[E_ResEmp] CHECK CONSTRAINT [FK_E_ResEmp_REFERENCE_HR_Employee] " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'实验室ID\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'E_ResEmp\', @level2type=N\'COLUMN\',@level2name=N\'LabID\' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'职责与员工关系ID\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'E_ResEmp\', @level2type=N\'COLUMN\',@level2name=N\'ResEmpID\' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'职责ID\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'E_ResEmp\', @level2type=N\'COLUMN\',@level2name=N\'ResponsibilityID\'  " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'仪器模板ID\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'E_ResEmp\', @level2type=N\'COLUMN\',@level2name=N\'EmpID\' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'是否使用\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'E_ResEmp\', @level2type=N\'COLUMN\',@level2name=N\'IsUse\'  " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'显示次序\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'E_ResEmp\', @level2type=N\'COLUMN\',@level2name=N\'DispOrder\' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'数据加入时间\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'E_ResEmp\', @level2type=N\'COLUMN\',@level2name=N\'DataAddTime\' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'数据更新时间\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'E_ResEmp\', @level2type=N\'COLUMN\',@level2name=N\'DataUpdateTime\'  " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'时间戳\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'E_ResEmp\', @level2type=N\'COLUMN\',@level2name=N\'DataTimeStamp\'  " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N\'MS_Description\', @value=N\'职责与员工关系表\' , @level0type=N\'SCHEMA\',@level0name=N\'dbo\', @level1type=N\'TABLE\',@level1name=N\'E_ResEmp\' " + "\r" +

                            " end";
                listSQL.Add(updateSql);
                updateSql = " if not exists (select * from dbo.sysobjects where id = object_id(N\'[dbo].[E_TempletRes]\')" + "\r" +
                            " and OBJECTPROPERTY(id, N\'IsUserTable\') = 1) BEGIN " + "\r" +
                            " CREATE TABLE [dbo].[E_TempletRes]( " + "\r" +
                            " 	[LabID] [bigint] NULL,[TempletResID] [bigint] NOT NULL,[ResponsibilityID] [bigint] NULL,[TempletID] [bigint] NULL,[IsUse] [bit] NULL,[DispOrder] [int] NULL,[DataAddTime] [datetime] NULL,[DataUpdateTime] [datetime] NULL,[DataTimeStamp] [timestamp] NULL, " + "\r" +
                            "  CONSTRAINT [PK_E_TempletRes] PRIMARY KEY NONCLUSTERED " + "\r" +
                            " ([TempletResID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] " + "\r" +
                            " ) ON [PRIMARY] " + "\r" +
                            " ALTER TABLE [dbo].[E_TempletRes]  WITH CHECK ADD  CONSTRAINT [FK_E_TempletRes_REFERENCE_E_Responsibility] FOREIGN KEY([ResponsibilityID]) REFERENCES [dbo].[E_Responsibility] ([ResponsibilityID]) " + "\r" +
                            " ALTER TABLE [dbo].[E_TempletRes] CHECK CONSTRAINT [FK_E_TempletRes_REFERENCE_E_Responsibility] " + "\r" +
                            " ALTER TABLE [dbo].[E_TempletRes]  WITH CHECK ADD  CONSTRAINT [FK_E_TempletRes_REFERENCE_E_TEMPLE] FOREIGN KEY([TempletID]) REFERENCES [dbo].[E_Templet] ([TempletID]) " + "\r" +
                            " ALTER TABLE [dbo].[E_TempletRes] CHECK CONSTRAINT [FK_E_TempletRes_REFERENCE_E_TEMPLE] " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'实验室ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'E_TempletRes', @level2type=N'COLUMN',@level2name=N'LabID' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模板与职责关系ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'E_TempletRes', @level2type=N'COLUMN',@level2name=N'TempletResID' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'职责ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'E_TempletRes', @level2type=N'COLUMN',@level2name=N'ResponsibilityID' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'仪器模板ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'E_TempletRes', @level2type=N'COLUMN',@level2name=N'TempletID' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'E_TempletRes', @level2type=N'COLUMN',@level2name=N'IsUse' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'显示次序' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'E_TempletRes', @level2type=N'COLUMN',@level2name=N'DispOrder' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据加入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'E_TempletRes', @level2type=N'COLUMN',@level2name=N'DataAddTime' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数据更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'E_TempletRes', @level2type=N'COLUMN',@level2name=N'DataUpdateTime'  " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'E_TempletRes', @level2type=N'COLUMN',@level2name=N'DataTimeStamp' " + "\r" +
                            " EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模板与职责关系表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'E_TempletRes' " + "\r" +
                            " end";
                listSQL.Add(updateSql);
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.51");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.51) Update Error, Please Check The Log!");
            }
            #endregion //QMS升级质量记录修改查询列表数据存储过程

            #region 1.0.0.52 ///QMS升级质量记录修改模板表的数据类型
            if (IsUpdateDataBase(oldVersion, "1.0.0.52"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " if Exists(Select * from SysColumns where [Name]= \'TempletStruct\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_Templet\' ))" + "\r" +
                            " alter table E_Templet alter column TempletStruct nvarchar(max) ";
                listSQL.Add(updateSql);

                updateSql = " if Exists(Select * from SysColumns where [Name]= \'TempletFillStruct\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_Templet\')) " + "\r" +
                            " alter table E_Templet alter column TempletFillStruct nvarchar(max) ";
                listSQL.Add(updateSql);

                updateSql = " if Exists(Select * from SysColumns where [Name]= \'Comment\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_Templet\')) " + "\r" +
                            " alter table E_Templet alter column Comment nvarchar(max) ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

            if (result)
                    result = UpateCompareVersionInfo("1.0.0.52");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.52) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.53 //QMS升级质量记录升级查询审核存储过程
            if (IsUpdateDataBase(oldVersion, "1.0.0.53"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " if exists(select * from dbo.sysobjects where id = object_id(N\'[dbo].[P_GetReportData]\') and OBJECTPROPERTY(id, N\'IsProcedure\') = 1)" + "\r" +
                            " DROP PROCEDURE [dbo].[P_GetReportData] ";
                listSQL.Add(updateSql);

                updateSql = " CREATE PROCEDURE [dbo].[P_GetReportData] " + "\r" +
                             " @TempletType int=0, " + "\r" +
                             " @TempletID varchar(50), " + "\r" +
                             " @EquipID varchar(50), " + "\r" +
                             " @EmpID varchar(50), " + "\r" +
                             " @BeginDate datetime, " + "\r" +
                             " @EndDate datetime, " + "\r" +
                             " @IsCheck varchar(50) " + "\r" +
                             " AS " + "\r" +
                             " BEGIN " + "\r" +
                             "     declare  @StrMonth varchar(50) " + "\r" +
                             "     declare  @Where varchar(500) " + "\r" +
                             "     declare  @SQL varchar(5000) " + "\r" +
                             "     declare  @TempletSQL varchar(500) " + "\r" +
                             "     declare  @TempSQL varchar(5000) " + "\r" +
                             "     declare  @MonthSQL varchar(5000) " + "\r" +
                             "     declare  @MonthCount int " + "\r" +
                             "     set @Where = '' " + "\r" +
                             "     set @MonthSQL = '' " + "\r" +
                             "     set @MonthCount = cast((datediff(month, @BeginDate, @EndDate)) as int) " + "\r" +
                             "     if @TempletType = 0 " + "\r" +
                             "       set @TempletSQL = \' Select * from (select TempletID, EmpID, \'\'[@ReportDate]\'\' as ReportDate from E_TempletEmp where EmpID=[@EmpID]\' + " + "\r" +
                             "                         \' union select  b.TempletID, a.EmpID, \'\'[@ReportDate]\'\' as ReportDate from E_ResEmp a \' + " + "\r" +
                             "                         \' inner join E_TempletRes b on a.ResponsibilityID=b.ResponsibilityID where a.EmpID=[@EmpID] Group by b.TempletID, a.EmpID ) KK[@MonthCount]\' " + "\r" +
                             "     else if @TempletType = 1 " + "\r" +
                             "       Set @TempletSQL = \' select TempletID, EmpID, \'\'[@ReportDate]\'\' as ReportDate from E_TempletEmp where EmpID=[@EmpID]\' " + "\r" +
                             "     else if @TempletType = 2 " + "\r" +
                             "       Set @TempletSQL = \' Select  b.TempletID, a.EmpID, \'\'[@ReportDate]\'\' as ReportDate from E_ResEmp a \' + " + "\r" +
                             "                         \' inner join E_TempletRes b on a.ResponsibilityID=b.ResponsibilityID where a.EmpID=[@EmpID] Group by b.TempletID, a.EmpID \' " + "\r" +
                             "     if (@MonthCount >= 0) " + "\r" +
                             "     begin " + "\r" +
                             "       while (@MonthCount >= 0) " + "\r" +
                             "       begin " + "\r" +
                             "         set @StrMonth = CONVERT(varchar(8), DATEADD(MONTH, @MonthCount, @BeginDate), 120) + \'01\' " + "\r" +
                             "         set @TempSQL = REPLACE(@TempletSQL, \'[@EmpID]\', @EmpID) " + "\r" +
                             "         set @TempSQL = REPLACE(@TempSQL, \'[@ReportDate]\', @StrMonth) " + "\r" +
                             "         set @TempSQL = REPLACE(@TempSQL, \'[@MonthCount]\', Cast(@MonthCount as varchar)) " + "\r" +
                             "        if (@MonthSQL = \'\') " + "\r" +
                             "          set @MonthSQL = @TempSQL " + "\r" +
                             "        else " + "\r" +
                             "          set @MonthSQL = @MonthSQL + \' union \' + @TempSQL " + "\r" +
                             "          set @MonthCount = @MonthCount - 1 " + "\r" +
                             "       end " + "\r" +
                             "       if (@MonthSQL <> '') " + "\r" +
                             "       begin " + "\r" +
                             "         if (@TempletID <> '') " + "\r" +
                             "           set @Where = @Where + ' and ETP.TempletID=' + @TempletID " + "\r" +
                             "         if (@EquipID <> '') " + "\r" +
                             "           set @Where = @Where + ' and ERD.EquipID=' + @EquipID " + "\r" +
                             "         if (@IsCheck <> '') " + "\r" +
                             "         begin " + "\r" +
                             "            if (@IsCheck = '0') " + "\r" +
                             "              set @Where = @Where + ' and (ERD.IsCheck is null or ERD.IsCheck=' + @IsCheck + ')' " + "\r" +
                             "  		  else " + "\r" +
                             "              set @Where = @Where + ' and ERD.IsCheck=' + @IsCheck " + "\r" +
                             "         end " + "\r" +
                             "         set @SQL = \'select ROW_NUMBER() OVER(ORDER BY TempletID ASC) AS Id, *  \' + " + "\r" +
                             "                     \'from(  \' + " + "\r" +
                             "                         \'select ETP.TempletID, ET.EquipID, ETP.ReportDate, ET.CName as ReportName, \' + " + "\r" +
                             "                         \'ERD.ReportDataID, ERD.LabID, ERD.ReportFilePath, ERD.ReportFileExt, ERD.IsCheck, \' + " + "\r" +
                             "                         \'ERD.Checker, ERD.CheckTime, ERD.CheckView, ERD.Comment, ERD.IsUse, \' + " + "\r" +
                             "                         \'ERD.DispOrder, ERD.DataAddTime, ERD.DataUpdateTime, EE.CName as EquipName, \' + " + "\r" +
                             "                         \'EE.UseCode as EUseCode, EE.Shortcode as EShortcode, EE.EName as EEName, \' + " + "\r" +
                             "                         \'PD.CName as EquipTypeName, ET.SectionID, HRD.CName as SectionName, \' + " + "\r" +
                             "                         \'HRD.UseCode, HRD.Shortcode, HRD.StandCode, HRD.EName, \' + " + "\r" +
                             "                         \'dbo.F_GetReportAttachment(ETP.TempletID, ET.EquipID, ETP.ReportDate, ETP.ReportDate) as IsAttachment  \' + " + "\r" +
                             "                         \'from(\' + @MonthSQL + \') ETP   \' + " + "\r" +
                             "                         \'left join E_ReportData ERD on ETP.TempletID = ERD.TempletID and ETP.ReportDate = ERD.ReportDate  \' + " + "\r" +
                             "                         \'left join E_Templet ET on ET.TempletID = ETP.TempletID  \' + " + "\r" +
                             "                         \'left join E_Equip EE on EE.EquipID = ET.EquipID  \' + " + "\r" +
                             "                         \'left join P_Dict PD on EE.EquipTypeID = PD.DictID  \' + " + "\r" +
                             "                         \'left join HR_Dept HRD on ET.SectionID = HRD.DeptID   \' + " + "\r" +
                             "                         \'where 1=1 \' + @Where + " + "\r" +
                             "                     \') MM order by ReportDate, ReportName ASC \' " + "\r" +
                             "         exec(@SQL) " + "\r" +
                             "       end " + "\r" +
                             "     end " + "\r" +
                             " END ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.53");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.53) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.54 //QMS质量记录增加审核意见字段的长度
            if (IsUpdateDataBase(oldVersion, "1.0.0.54"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " if Exists(Select * from SysColumns where [Name]= \'CheckView\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_ReportData\' ))" + "\r" +
                            " alter table E_ReportData alter column CheckView nvarchar(max) ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.54");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.54) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.55 //QMS升级质量记录增加参数表
            if (IsUpdateDataBase(oldVersion, "1.0.0.55"))
            {
                List<string> listSQL = new List<string>();
                updateSql = " if not exists (select * from dbo.sysobjects where id = object_id(N\'[dbo].[E_Parameter]\')" + "\r" +
                            " and OBJECTPROPERTY(id, N\'IsUserTable\') = 1) BEGIN " + "\r" +
                            " CREATE TABLE[dbo].[E_Parameter]([LabID] [bigint] NOT NULL,[ParameterID]  [bigint] NOT NULL,[CName] [varchar](100) NULL,[SName] " + "\r" +
                            " [varchar](100) NULL,[ParaType] [varchar](100) NULL,[ParaNo] [varchar](50) NULL,[ParaValue] [nvarchar](1000) NULL,[ParaDesc] " + "\r" +
                            " [nvarchar](1000) NULL,[Shortcode] [varchar](100) NULL,[DispOrder] [int] NULL, [PinYinZiTou] [varchar](100)  NULL,[IsUse] [bit] NULL,[IsUserSet] [bit] NULL,[DataAddTime] [datetime] NULL,[DataUpdateTime] [datetime] NULL,[DataTimeStamp] [timestamp] NULL," + "\r" +
                            " CONSTRAINT[PK_E_PARAMETER] PRIMARY KEY CLUSTERED([ParameterID] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON [PRIMARY] " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'实验室ID\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'LabID\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'参数ID\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'ParameterID\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'名称\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'CName\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'简称\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'SName\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'参数类型\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'ParaType\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'参数编码\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'ParaNo\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'参数值\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'ParaValue\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'参数说明\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'ParaDesc\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'快捷码\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'Shortcode\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'显示次序\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'DispOrder\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'汉字拼音字头\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'PinYinZiTou\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'是否使用\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'IsUse\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'是否允许用户设置\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'IsUserSet\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'创建时间\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'DataAddTime\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'数据更新时间\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'DataUpdateTime\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'时间戳\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'DataTimeStamp\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'质量记录参数表\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\' " + "\r" +
                            " end  ";
                listSQL.Add(updateSql);
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.55");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.55) Update Error, Please Check The Log!");
            }
            #endregion //QMS升级质量记录修改查询列表数据存储过程

            #region 1.0.0.56 //员工表NameL，NameF字段修改为可以为空
            if (IsUpdateDataBase(oldVersion, "1.0.0.56"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " if Exists(Select * from SysColumns where [Name]= \'NameL\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'HR_Employee\' ))" + "\r" +
                            " alter table HR_Employee alter column NameL nvarchar(30) null ";
                listSQL.Add(updateSql);

                updateSql = " if Exists(Select * from SysColumns where [Name]= \'NameF\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'HR_Employee\')) " + "\r" +
                            " alter table HR_Employee alter column NameF nvarchar(30) null ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.56");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.56) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.57 //删除P_Dict表外键FK_P_Dict_S_ServiceClient
            if (IsUpdateDataBase(oldVersion, "1.0.0.57"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " if Exists(Select 1 from sysobjects where [Name]= \'FK_P_Dict_S_ServiceClient\'" + "\r" +
                            " and xtype = \'F \')" + "\r" +
                            " alter table P_Dict DROP CONSTRAINT FK_P_Dict_S_ServiceClient ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.57");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.57) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.58 //QMS升级质量记录升级查询审核存储过程
            if (IsUpdateDataBase(oldVersion, "1.0.0.58"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " if exists(select * from dbo.sysobjects where id = object_id(N\'[dbo].[P_GetReportData]\') and OBJECTPROPERTY(id, N\'IsProcedure\') = 1)" + "\r" +
                            " DROP PROCEDURE [dbo].[P_GetReportData] ";
                listSQL.Add(updateSql);

                updateSql = " CREATE PROCEDURE [dbo].[P_GetReportData] " + "\r" +
                             " @TempletType int=0, " + "\r" +
                             " @TempletID varchar(50), " + "\r" +
                             " @EquipID varchar(50), " + "\r" +
                             " @EmpID varchar(50), " + "\r" +
                             " @BeginDate datetime, " + "\r" +
                             " @EndDate datetime, " + "\r" +
                             " @OtherPara varchar(500) " + "\r" +
                             " AS " + "\r" +
                             " BEGIN " + "\r" +
                             "     declare  @StrMonth varchar(50) " + "\r" +
                             "     declare  @Where varchar(500) " + "\r" +
                             "     declare  @SQL varchar(5000) " + "\r" +
                             "     declare  @TempletSQL varchar(500) " + "\r" +
                             "     declare  @TempSQL varchar(5000) " + "\r" +
                             "     declare  @MonthSQL varchar(5000) " + "\r" +
                             "     declare  @MonthCount int " + "\r" +
                             "     declare  @IsCheck varchar(50) " + "\r" +
                             "     set @Where = '' " + "\r" +
                             "     set @IsCheck = '' " + "\r" +
                             "     set @MonthSQL = '' " + "\r" +
                             "     set @MonthCount = cast((datediff(month, @BeginDate, @EndDate)) as int) " + "\r" +
                             "     if @TempletType = 0 " + "\r" +
                             "       set @TempletSQL = \' Select * from (select TempletID, EmpID, \'\'[@ReportDate]\'\' as ReportDate from E_TempletEmp where EmpID=[@EmpID]\' + " + "\r" +
                             "                         \' union select  b.TempletID, a.EmpID, \'\'[@ReportDate]\'\' as ReportDate from E_ResEmp a \' + " + "\r" +
                             "                         \' inner join E_TempletRes b on a.ResponsibilityID=b.ResponsibilityID where a.EmpID=[@EmpID] Group by b.TempletID, a.EmpID ) KK[@MonthCount]\' " + "\r" +
                             "     else if @TempletType = 1 " + "\r" +
                             "       Set @TempletSQL = \' select TempletID, EmpID, \'\'[@ReportDate]\'\' as ReportDate from E_TempletEmp where EmpID=[@EmpID]\' " + "\r" +
                             "     else if @TempletType = 2 " + "\r" +
                             "       Set @TempletSQL = \' Select  b.TempletID, a.EmpID, \'\'[@ReportDate]\'\' as ReportDate from E_ResEmp a \' + " + "\r" +
                             "                         \' inner join E_TempletRes b on a.ResponsibilityID=b.ResponsibilityID where a.EmpID=[@EmpID] Group by b.TempletID, a.EmpID \' " + "\r" +
                             "     if (@MonthCount >= 0) " + "\r" +
                             "     begin " + "\r" +
                             "       while (@MonthCount >= 0) " + "\r" +
                             "       begin " + "\r" +
                             "         set @StrMonth = CONVERT(varchar(8), DATEADD(MONTH, @MonthCount, @BeginDate), 120) + \'01\' " + "\r" +
                             "         set @TempSQL = REPLACE(@TempletSQL, \'[@EmpID]\', @EmpID) " + "\r" +
                             "         set @TempSQL = REPLACE(@TempSQL, \'[@ReportDate]\', @StrMonth) " + "\r" +
                             "         set @TempSQL = REPLACE(@TempSQL, \'[@MonthCount]\', Cast(@MonthCount as varchar)) " + "\r" +
                             "        if (@MonthSQL = \'\') " + "\r" +
                             "          set @MonthSQL = @TempSQL " + "\r" +
                             "        else " + "\r" +
                             "          set @MonthSQL = @MonthSQL + \' union \' + @TempSQL " + "\r" +
                             "          set @MonthCount = @MonthCount - 1 " + "\r" +
                             "       end " + "\r" +
                             "       if (@MonthSQL <> '') " + "\r" +
                             "       begin " + "\r" +
                             "         if (@TempletID <> '') " + "\r" +
                             "           set @Where = @Where + ' and ETP.TempletID=' + @TempletID " + "\r" +
                             "         if (@EquipID <> '') " + "\r" +
                             "           set @Where = @Where + ' and ERD.EquipID=' + @EquipID " + "\r" +
                             "         if (@OtherPara <> '') " + "\r" +
                             "         begin " + "\r" +
                             "              --参数顺序为：是否审核&&质量记录模板名称&&质量记录模板代码&&质量记录仪器名称&&质量记录小组名称&&质量记录模板类型&&质量记录仪器类型 " + "\r" +
                             "           declare @StrPara varchar(500) " + "\r" +
                             "           declare @Para varchar(500) " + "\r" +
                             "           declare @Split varchar(50) " + "\r" +
                             "           declare @i int " + "\r" +
                             "           declare  @index int " + "\r" +
                             "           set @Split = '&&' " + "\r" +
                             "           set @StrPara = @OtherPara " + "\r" +
                             "           set @Para = '' " + "\r" +
                             "           set @i = 0 " + "\r" +
                             "           set @index = 0 " + "\r" +
                             "           set @index = CHARINDEX(@Split, @StrPara) " + "\r" +
                             "           while (@index >= 0) " + "\r" +
                             "           begin " + "\r" +
                             "             set @i = @i + 1 " + "\r" +
                             "             if (@index = 0 and @i> 0) " + "\r" +
                             "             begin " + "\r" +
                             "               set @index = -1 " + "\r" +
                             "               set @Para = @StrPara " + "\r" +
                             "             end else " + "\r" +
                             "             begin " + "\r" +
                             "               set @Para = SUBSTRING(@StrPara, 0, @index) " + "\r" +
                             "               set @StrPara = SUBSTRING(@StrPara, @index + 2, len(@StrPara)) " + "\r" +
                             "               set @index = CHARINDEX(@Split, @StrPara) " + "\r" +
                             "             end " + "\r" +
                             "             if @i = 1 " + "\r" +
                             "               set @IsCheck = @Para " + "\r" +
                             "             else if @i = 2 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and ET.CName like ''%' + @Para + '%''' " + "\r" +
                             "             else if @i = 3 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and ET.UseCode = ''' + @Para + '''' " + "\r" +
                             "             else if @i = 4 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and EE.CName like ''%' + @Para + '%''' " + "\r" +
                             "             else if @i = 5 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and ET.SectionID = ' + @Para " + "\r" +
                             "             else if @i = 6 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and ET.TempletTypeID = ' + @Para " + "\r" +
                             "             else if @i = 7 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and EE.EquipTypeID = ' + @Para " + "\r" +
                             "             else if @i = 8 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and HRD.UseCode = ''' + @Para + '''' " + "\r" +
                             "           end " + "\r" +
                             "         end " + "\r" +
                             "         if (@IsCheck <> '') " + "\r" +
                             "         begin " + "\r" +
                             "            if (@IsCheck = '0') " + "\r" +
                             "              set @Where = @Where + ' and (ERD.IsCheck is null or ERD.IsCheck=' + @IsCheck + ')' " + "\r" +
                             "  		  else " + "\r" +
                             "              set @Where = @Where + ' and ERD.IsCheck=' + @IsCheck " + "\r" +
                             "         end " + "\r" +
                             "         set @SQL = \'select ROW_NUMBER() OVER(ORDER BY TempletID ASC) AS Id, *  \' + " + "\r" +
                             "                     \'from(  \' + " + "\r" +
                             "                         \'select ETP.TempletID, ET.EquipID,ET.UseCode as TempletCode, ETP.ReportDate, ET.CName as ReportName, ET.SName as ReportSName,\' + " + "\r" +
                             "                         \'ERD.ReportDataID, ERD.LabID, ERD.ReportFilePath, ERD.ReportFileExt, ERD.IsCheck, \' + " + "\r" +
                             "                         \'ERD.Checker, ERD.CheckTime, ERD.CheckView, ERD.Comment, ERD.IsUse, \' + " + "\r" +
                             "                         \'ERD.DispOrder, ERD.DataAddTime, ERD.DataUpdateTime, EE.CName as EquipName, \' + " + "\r" +
                             "                         \'EE.UseCode as EquipUseCode, EE.Shortcode as EquipShortcode, EE.EName as EquipEName, \' + " + "\r" +
                             "                         \'PD.CName as EquipTypeName, ET.SectionID, HRD.CName as SectionName, \' + " + "\r" +
                             "                         \'HRD.UseCode as SectionUseCode, HRD.Shortcode as SectionShortcode, HRD.StandCode as SectionStandCode, HRD.EName as SectionEName, PDI.CName as TempletTypeName, \' + " + "\r" +
                             "                         \'dbo.F_GetReportAttachment(ETP.TempletID, ET.EquipID, ETP.ReportDate, ETP.ReportDate) as IsAttachment  \' + " + "\r" +
                             "                         \'from(\' + @MonthSQL + \') ETP   \' + " + "\r" +
                             "                         \'left join E_ReportData ERD on ETP.TempletID = ERD.TempletID and ETP.ReportDate = ERD.ReportDate and ERD.IsUse=1 \' + " + "\r" +
                             "                         \'left join E_Templet ET on ET.TempletID = ETP.TempletID  \' + " + "\r" +
                             "                         \'left join P_Dict PDI on ET.TempletTypeID = PDI.DictID  \' + " + "\r" +
                             "                         \'left join E_Equip EE on EE.EquipID = ET.EquipID  \' + " + "\r" +
                             "                         \'left join P_Dict PD on EE.EquipTypeID = PD.DictID  \' + " + "\r" +
                             "                         \'left join HR_Dept HRD on ET.SectionID = HRD.DeptID   \' + " + "\r" +
                             "                         \'where 1=1 \' + @Where + " + "\r" +
                             "                     \') MM order by ReportDate, ReportName ASC \' " + "\r" +
                             "         exec(@SQL) " + "\r" +
                             "       end " + "\r" +
                             "     end " + "\r" +
                             " END ";
                listSQL.Add(updateSql);

                updateSql = " update E_ReportData set IsUse=1 ";
                listSQL.Add(updateSql);

                updateSql = " if Not Exists(Select * from SysColumns where [Name]= \'CancelCheckerID\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_ReportData\' ))" + "\r" +
                            " begin " + "\r" +
                            "   Alter Table E_ReportData Add CancelCheckerID bigint " + "\r" +
                            "   EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'反审人ID\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                            "   @level1type = N\'TABLE\',@level1name = N\'E_ReportData\', @level2type = N\'COLUMN\',@level2name = N\'CancelCheckerID\'" + "\r" +
                            " end ";
                listSQL.Add(updateSql);

                updateSql = " if Not Exists(Select * from SysColumns where [Name]= \'CancelChecker\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_ReportData\' ))" + "\r" +
                            " begin " + "\r" +
                            "   Alter Table E_ReportData Add CancelChecker varchar(50) " + "\r" +
                            "   EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'反审人\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                            "   @level1type = N\'TABLE\',@level1name = N\'E_ReportData\', @level2type = N\'COLUMN\',@level2name = N\'CancelChecker\'" + "\r" +
                            " end ";
                listSQL.Add(updateSql);

                updateSql = " if Not Exists(Select * from SysColumns where [Name]= \'CancelCheckTime\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_ReportData\' ))" + "\r" +
                            " begin " + "\r" +
                            "   Alter Table E_ReportData Add CancelCheckTime datetime " + "\r" +
                            "   EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'反审时间\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                            "   @level1type = N\'TABLE\',@level1name = N\'E_ReportData\', @level2type = N\'COLUMN\',@level2name = N\'CancelCheckTime\'" + "\r" +
                            " end ";
                listSQL.Add(updateSql);

                updateSql = " if Not Exists(Select * from SysColumns where [Name]= \'CancelCheckView\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_ReportData\' ))" + "\r" +
                            " begin " + "\r" +
                            "   Alter Table E_ReportData Add CancelCheckView varchar(5000) " + "\r" +
                            "   EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'反审意见\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                            "   @level1type = N\'TABLE\',@level1name = N\'E_ReportData\', @level2type = N\'COLUMN\',@level2name = N\'CancelCheckView\'" + "\r" +
                            " end ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.58");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.58) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.59 OA客户表
            if (IsUpdateDataBase(oldVersion, "1.0.0.59"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " IF COL_LENGTH('P_Client', 'ProjectSourceName') IS NULL  alter table P_Client add ProjectSourceName varchar(50)  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('P_Client', 'AgentName') IS NULL  alter table P_Client add AgentName varchar(100)  ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.59");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.59) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.60 
            if (IsUpdateDataBase(oldVersion, "1.0.0.60"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " if Not Exists(Select * from SysColumns where [Name]= \'CheckType\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_Templet\' ))" + "\r" +
                            " begin " + "\r" +
                            "   Alter Table E_Templet Add CheckType int default 0 " + "\r" +
                            "   EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'审核类型\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                            "   @level1type = N\'TABLE\',@level1name = N\'E_Templet\', @level2type = N\'COLUMN\',@level2name = N\'CheckType\'" + "\r" +
                            " end ";
                listSQL.Add(updateSql);


                updateSql = " if Not Exists(Select * from SysColumns where [Name]= \'CheckType\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_ReportData\' ))" + "\r" +
                            " begin " + "\r" +
                            "   Alter Table E_ReportData Add CheckType int default 0 " + "\r" +
                            "   EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'审核类型\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                            "   @level1type = N\'TABLE\',@level1name = N\'E_ReportData\', @level2type = N\'COLUMN\',@level2name = N\'CheckType\'" + "\r" +
                            " end ";
                listSQL.Add(updateSql);

                updateSql = " update E_Templet set CheckType=0 where CheckType is null ";
                listSQL.Add(updateSql);

                updateSql = " update E_ReportData set CheckType=0 where CheckType is null ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from dbo.sysobjects where id = object_id(N\'[dbo].[P_GetReportData]\') and OBJECTPROPERTY(id, N\'IsProcedure\') = 1)" + "\r" +
                           " DROP PROCEDURE [dbo].[P_GetReportData] ";
                listSQL.Add(updateSql);

                updateSql = " CREATE PROCEDURE [dbo].[P_GetReportData] " + "\r" +
                             " @TempletType int=0, " + "\r" +
                             " @TempletID varchar(50), " + "\r" +
                             " @EquipID varchar(50), " + "\r" +
                             " @EmpID varchar(50), " + "\r" +
                             " @BeginDate datetime, " + "\r" +
                             " @EndDate datetime, " + "\r" +
                             " @CheckType varchar(50), " + "\r" +
                             " @OtherPara varchar(500) " + "\r" +
                             " AS " + "\r" +
                             " BEGIN " + "\r" +
                             "     declare  @Where varchar(5000) " + "\r" +
                             "     declare  @SQL varchar(8000) " + "\r" +
                             "     declare  @TempletSQL varchar(5000) " + "\r" +
                             "     declare  @MonthCount int " + "\r" +
                             "     declare  @DateCalcType varchar(500) " + "\r" +
                             "     declare  @ReportDate varchar(500) " + "\r" +
                             "     declare  @IsCheck varchar(50) " + "\r" +
                             "     set @Where = '' " + "\r" +
                             "     set @IsCheck = '' " + "\r" +
                             "     set @DateCalcType = 'month' " + "\r" +
                             "     set @ReportDate = '' " + "\r" +
                             "     if @CheckType = '1' " + "\r" +
                             "     begin " + "\r" +
                             "       set @MonthCount = cast((datediff(day, @BeginDate, @EndDate)) as int) " + "\r" +
                             "       set @DateCalcType = 'day' " + "\r" +
                             "       set @ReportDate = ' CONVERT(varchar(10), dateadd(day,number,[@BeginDate]), 120) '" + "\r" +
                             "     end else " + "\r" +
                             "     begin " + "\r" +
                             "       set @MonthCount = cast((datediff(month, @BeginDate, @EndDate)) as int) " + "\r" +
                             "       set @DateCalcType = 'month' " + "\r" +
                             "       set @ReportDate = ' CONVERT(varchar(8), dateadd(month,number,[@BeginDate]), 120) + ''01'' '" + "\r" +
                             "     end " + "\r" +
                             "    if @TempletType = 0 " + "\r" +
                             "      set @TempletSQL = ' Select * from (select TempletID, EmpID from E_TempletEmp where EmpID=[@EmpID]' + " + "\r" +
                             "                        ' union select  b.TempletID, a.EmpID from E_ResEmp a ' + " + "\r" +
                             "                        ' inner join E_TempletRes b on a.ResponsibilityID=b.ResponsibilityID ' + " + "\r" +
                             "                        ' where a.EmpID=[@EmpID] Group by b.TempletID, a.EmpID) MM ' + " + "\r" +
                             "                        ' cross join (select [@ReportDate] as ReportDate ' + " + "\r" +
                             "                        ' from master.dbo.spt_values  where type =''P'' and number <=DATEDIFF([@DateCalcType], [@BeginDate],[@EndDate])) NN ' " + "\r" +
                             "    else if @TempletType = 1 " + "\r" +
                             "      Set @TempletSQL = ' Select * from (select TempletID, EmpID from E_TempletEmp where EmpID=[@EmpID]) MM ' + " + "\r" +
                             "                        ' cross join (select [@ReportDate] as ReportDate ' + " + "\r" +
                             "                        ' from master.dbo.spt_values  where type =''P'' and number <=DATEDIFF([@DateCalcType], [@BeginDate],[@EndDate])) NN ' " + "\r" +
                             "    else if @TempletType = 2 " + "\r" +
                             "      Set @TempletSQL = ' Select * from (Select  b.TempletID, a.EmpID  from E_ResEmp a ' + " + "\r" +
                             "                        ' inner join E_TempletRes b on a.ResponsibilityID=b.ResponsibilityID ' + " + "\r" +
                             "                        ' where a.EmpID=[@EmpID] Group by b.TempletID, a.EmpID) MM ' + " + "\r" +
                             "                        ' cross join (select [@ReportDate] as ReportDate ' + " + "\r" +
                             "                        ' from master.dbo.spt_values  where type =''P'' and number <=DATEDIFF([@DateCalcType], [@BeginDate],[@EndDate])) NN ' " + "\r" +
                             "    if (@MonthCount >= 0) " + "\r" +
                             "    begin " + "\r" +
                             "      set @TempletSQL = REPLACE(@TempletSQL, '[@EmpID]', @EmpID) " + "\r" +
                             "      set @TempletSQL = REPLACE(@TempletSQL, '[@ReportDate]', @ReportDate) " + "\r" +
                             "      set @TempletSQL = REPLACE(@TempletSQL, '[@BeginDate]', '''' + CONVERT(varchar(10), @BeginDate, 120) + '''') " + "\r" +
                             "      set @TempletSQL = REPLACE(@TempletSQL, '[@EndDate]', '''' + CONVERT(varchar(10), @EndDate, 120) + '''') " + "\r" +
                             "      set @TempletSQL = REPLACE(@TempletSQL, '[@DateCalcType]', @DateCalcType) " + "\r" +
                             "      if (@TempletSQL <> '') " + "\r" +
                             "      begin " + "\r" +
                             "         if (@CheckType <> '') " + "\r" +
                             "           set @Where = @Where + ' and ET.CheckType=' + cast(@CheckType as varchar(10)) " + "\r" +
                             "         if (@TempletID <> '') " + "\r" +
                             "           set @Where = @Where + ' and ETP.TempletID=' + @TempletID " + "\r" +
                             "         if (@EquipID <> '') " + "\r" +
                             "           set @Where = @Where + ' and ERD.EquipID=' + @EquipID " + "\r" +
                             "         if (@OtherPara <> '') " + "\r" +
                             "         begin " + "\r" +
                             "              --参数顺序为：是否审核&&质量记录模板名称&&质量记录模板代码&&质量记录仪器名称&&质量记录小组名称&&质量记录模板类型&&质量记录仪器类型 " + "\r" +
                             "           declare @StrPara varchar(500) " + "\r" +
                             "           declare @Para varchar(500) " + "\r" +
                             "           declare @Split varchar(50) " + "\r" +
                             "           declare @i int " + "\r" +
                             "           declare  @index int " + "\r" +
                             "           set @Split = '&&' " + "\r" +
                             "           set @StrPara = @OtherPara " + "\r" +
                             "           set @Para = '' " + "\r" +
                             "           set @i = 0 " + "\r" +
                             "           set @index = 0 " + "\r" +
                             "           set @index = CHARINDEX(@Split, @StrPara) " + "\r" +
                             "           while (@index >= 0) " + "\r" +
                             "           begin " + "\r" +
                             "             set @i = @i + 1 " + "\r" +
                             "             if (@index = 0 and @i> 0) " + "\r" +
                             "             begin " + "\r" +
                             "               set @index = -1 " + "\r" +
                             "               set @Para = @StrPara " + "\r" +
                             "             end else " + "\r" +
                             "             begin " + "\r" +
                             "               set @Para = SUBSTRING(@StrPara, 0, @index) " + "\r" +
                             "               set @StrPara = SUBSTRING(@StrPara, @index + 2, len(@StrPara)) " + "\r" +
                             "               set @index = CHARINDEX(@Split, @StrPara) " + "\r" +
                             "             end " + "\r" +
                             "             if @i = 1 " + "\r" +
                             "               set @IsCheck = @Para " + "\r" +
                             "             else if @i = 2 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and ET.CName like ''%' + @Para + '%''' " + "\r" +
                             "             else if @i = 3 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and ET.UseCode = ''' + @Para + '''' " + "\r" +
                             "             else if @i = 4 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and EE.CName like ''%' + @Para + '%''' " + "\r" +
                             "             else if @i = 5 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and ET.SectionID = ' + @Para " + "\r" +
                             "             else if @i = 6 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and ET.TempletTypeID = ' + @Para " + "\r" +
                             "             else if @i = 7 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and EE.EquipTypeID = ' + @Para " + "\r" +
                             "             else if @i = 8 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and HRD.UseCode = ''' + @Para + '''' " + "\r" +
                             "           end " + "\r" +
                             "         end " + "\r" +
                             "         if (@IsCheck <> '') " + "\r" +
                             "         begin " + "\r" +
                             "            if (@IsCheck = '0') " + "\r" +
                             "              set @Where = @Where + ' and (ERD.IsCheck is null or ERD.IsCheck=' + @IsCheck + ')' " + "\r" +
                             "  		  else " + "\r" +
                             "              set @Where = @Where + ' and ERD.IsCheck=' + @IsCheck " + "\r" +
                             "         end " + "\r" +
                             "         set @SQL = \'select ROW_NUMBER() OVER(ORDER BY TempletID ASC) AS Id, *  \' + " + "\r" +
                             "                     \'from(  \' + " + "\r" +
                             "                         \'select ETP.TempletID, ET.EquipID,ET.UseCode as TempletCode, ETP.ReportDate, ET.CName as ReportName, ET.SName as ReportSName,\' + " + "\r" +
                             "                         \'ERD.ReportDataID, ERD.LabID, ERD.ReportFilePath, ERD.ReportFileExt, ERD.IsCheck, \' + " + "\r" +
                             "                         \'ERD.Checker, ERD.CheckTime, ERD.CheckView, ERD.Comment, ERD.IsUse, \' + " + "\r" +
                             "                         \'ERD.DispOrder, ERD.DataAddTime, ERD.DataUpdateTime, EE.CName as EquipName, \' + " + "\r" +
                             "                         \'EE.UseCode as EquipUseCode, EE.Shortcode as EquipShortcode, EE.EName as EquipEName, \' + " + "\r" +
                             "                         \'PD.CName as EquipTypeName, ET.SectionID, HRD.CName as SectionName, \' + " + "\r" +
                             "                         \'HRD.UseCode as SectionUseCode, HRD.Shortcode as SectionShortcode, HRD.StandCode as SectionStandCode, HRD.EName as SectionEName, PDI.CName as TempletTypeName, \' + " + "\r" +
                             "                         \'dbo.F_GetReportAttachment(ETP.TempletID, ET.EquipID, ETP.ReportDate, ETP.ReportDate,\'+@CheckType+\') as IsAttachment  \' + " + "\r" +
                             "                         \'from(\' + @TempletSQL + \') ETP   \' + " + "\r" +
                             "                         \'left join E_ReportData ERD on ETP.TempletID = ERD.TempletID and ETP.ReportDate = ERD.ReportDate and ERD.IsUse=1 \' + " + "\r" +
                             "                         \'left join E_Templet ET on ET.TempletID = ETP.TempletID  \' + " + "\r" +
                             "                         \'left join P_Dict PDI on ET.TempletTypeID = PDI.DictID  \' + " + "\r" +
                             "                         \'left join E_Equip EE on EE.EquipID = ET.EquipID  \' + " + "\r" +
                             "                         \'left join P_Dict PD on EE.EquipTypeID = PD.DictID  \' + " + "\r" +
                             "                         \'left join HR_Dept HRD on ET.SectionID = HRD.DeptID   \' + " + "\r" +
                             "                         \'where 1=1 \' + @Where + " + "\r" +
                             "                     \') MM order by ReportDate, ReportName ASC \' " + "\r" +
                             "         exec(@SQL) " + "\r" +
                             "       end " + "\r" +
                             "     end " + "\r" +
                             " END ";
                listSQL.Add(updateSql);

                updateSql = " IF OBJECT_ID (N\'F_GetReportAttachment\') IS NOT NULL " + "\r" +
                            " DROP FUNCTION [F_GetReportAttachment] ";
                listSQL.Add(updateSql);

                updateSql = " CREATE FUNCTION [dbo].[F_GetReportAttachment] " + "\r" +
                            " (@TempletID bigint, @EquipID bigint, @BeginDate datetime, @EndDate datetime, @CheckType varchar(50)) " + "\r" +
                            " RETURNS VARCHAR(50) AS " + "\r" +
                            " BEGIN " + "\r" +
                            "   Declare @AttachmentCount  Int " + "\r" +
                            "   if @CheckType <> '1' " + "\r" +
                            "   begin " + "\r" +
                            "     set @BeginDate = CONVERT(varchar(8), @BeginDate, 120) + '01' " + "\r" +
                            "     set @EndDate = dateadd(d, -day(@BeginDate), dateadd(m, 1, @BeginDate)) " + "\r" +
                            "   end else" + "\r" +
                            "   begin " + "\r" +
                            "     set @BeginDate = CONVERT(varchar(10), @BeginDate, 120) " + "\r" +
                            "     set @EndDate = CONVERT(varchar(10), @BeginDate+1, 120) " + "\r" +
                            "   end " + "\r" +
                            "   set @AttachmentCount = 0 " + "\r" +
                            "   select @AttachmentCount = count(*) from E_Attachment " + "\r" +
                            "   where TempletID = @TempletID and(EquipID is null or EquipID = @EquipID) " + "\r" +
                            "   and FileUploadDate>@BeginDate and FileUploadDate<@EndDate " + "\r" +
                            "   return @AttachmentCount " + "\r" +
                            " end ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.60");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.60) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.61 
            if (IsUpdateDataBase(oldVersion, "1.0.0.61"))
            {
                List<string> listSQL = new List<string>();
                updateSql = " if Not Exists(Select * from SysColumns where [Name]= \'FillStruct\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_Templet\' ))" + "\r" +
                            " begin " + "\r" +
                            "   Alter Table E_Templet Add FillStruct nvarchar(max) " + "\r" +
                            "   EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'填充规则\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                            "   @level1type = N\'TABLE\',@level1name = N\'E_Templet\', @level2type = N\'COLUMN\',@level2name = N\'FillStruct\'" + "\r" +
                            " end ";
                listSQL.Add(updateSql);

                updateSql = " if not exists (select * from dbo.sysobjects where id = object_id(N\'[dbo].[E_Parameter]\')" + "\r" +
                            " and OBJECTPROPERTY(id, N\'IsUserTable\') = 1) BEGIN " + "\r" +
                            " CREATE TABLE[dbo].[E_Parameter]([LabID] [bigint] NOT NULL,[ParameterID]  [bigint] NOT NULL,[CName] [varchar](100) NULL,[SName] " + "\r" +
                            " [varchar](100) NULL,[ParaType] [varchar](100) NULL,[ParaNo] [varchar](50) NULL,[ParaValue] [nvarchar](1000) NULL,[ParaDesc] " + "\r" +
                            " [nvarchar](1000) NULL,[Shortcode] [varchar](100) NULL,[DispOrder] [int] NULL, [PinYinZiTou] [varchar](100)  NULL,[IsUse] [bit] NULL,[IsUserSet] [bit] NULL,[DataAddTime] [datetime] NULL,[DataUpdateTime] [datetime] NULL,[DataTimeStamp] [timestamp] NULL," + "\r" +
                            " CONSTRAINT[PK_E_PARAMETER] PRIMARY KEY CLUSTERED([ParameterID] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON [PRIMARY] " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'实验室ID\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'LabID\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'参数ID\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'ParameterID\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'名称\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'CName\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'简称\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'SName\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'参数类型\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'ParaType\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'参数编码\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'ParaNo\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'参数值\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'ParaValue\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'参数说明\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'ParaDesc\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'快捷码\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'Shortcode\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'显示次序\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'DispOrder\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'汉字拼音字头\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'PinYinZiTou\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'是否使用\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'IsUse\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'是否允许用户设置\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'IsUserSet\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'创建时间\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'DataAddTime\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'数据更新时间\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'DataUpdateTime\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'时间戳\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\', @level2type = N\'COLUMN\',@level2name = N\'DataTimeStamp\' " + "\r" +
                            "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'质量记录参数表\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', @level1type = N\'TABLE\',@level1name = N\'E_Parameter\' " + "\r" +
                            " end  ";
                listSQL.Add(updateSql);

                updateSql = "if (not Exists(Select * from E_Parameter where ParaType = 'QualityRecord' and ParaNo = 'FillDataType'))" + "\r" +
                            "INSERT INTO E_Parameter(LabID, ParameterID, CName, SName, ParaType, ParaNo,  " + "\r" +
                            "ParaValue, ParaDesc, Shortcode, DispOrder, PinYinZiTou, IsUse, IsUserSet,  " + "\r" +
                            "DataAddTime, DataUpdateTime) VALUES(  " + "\r" +
                            "0" + "," + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "," +
                            "\'质量记录数据填充规则解析类型\',null,\'QualityRecord\',\'FillDataType\',0" + "," +
                            "\'空或0为兼容旧解析方式，1为当前解析方式\',null,0,null,1,1," +
                            "\'" + DateTime.Now.ToString("yyyy -MM-dd HH:mm:ss") + "\'," +
                            "\'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\')";
                listSQL.Add(updateSql);
                updateSql = "if (not Exists(Select * from E_Parameter where ParaType = 'QualityRecord' and ParaNo = 'FillEmptyCellType'))" + "\r" +
                            "INSERT INTO E_Parameter(LabID, ParameterID, CName, SName, ParaType, ParaNo,  " + "\r" +
                            "ParaValue, ParaDesc, Shortcode, DispOrder, PinYinZiTou, IsUse, IsUserSet,  " + "\r" +
                            "DataAddTime, DataUpdateTime) VALUES(  " + "\r" +
                            "0" + "," + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "," +
                            "\'质量记录数据空值填充类型\',null,\'QualityRecord\',\'FillEmptyCellType\',0" + "," +
                            "\'空或0即单元格为空值时填充原始值或默认值，1即单元格为空值时填充斜线(右上角-左下角)，2即单元格为空值时填充斜线(左上角-右下角)\',null,0,null,1,1," +
                            "\'" + DateTime.Now.ToString("yyyy -MM-dd HH:mm:ss") + "\'," +
                            "\'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\')";
                listSQL.Add(updateSql);
                updateSql = "if (not Exists(Select * from E_Parameter where ParaType = 'QualityRecord' and ParaNo = 'TypeCodeSort'))" + "\r" +
                            "INSERT INTO E_Parameter(LabID, ParameterID, CName, SName, ParaType, ParaNo,  " + "\r" +
                            "ParaValue, ParaDesc, Shortcode, DispOrder, PinYinZiTou, IsUse, IsUserSet,  " + "\r" +
                            "DataAddTime, DataUpdateTime) VALUES(  " + "\r" +
                            "0" + "," + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "," +
                            "\'质量记录模板规则类别排序\',null,\'QualityRecord\',\'TypeCodeSort\',0" + "," +
                            "\'空或0为不排序，1为排序空或0为不排序，1为排序。例如：根据模板单元格位置读取项目代码的顺序为MD1、MD2、MD3、MD5、MD6、MD4，如果不排序，页面显示输入项的顺序即上述顺序，如果排序，则显示顺序为MD1、MD2、MD3、MD4、MD5、MD6。\',null,0,null,1,1," +
                            "\'" + DateTime.Now.ToString("yyyy -MM-dd HH:mm:ss") + "\'," +
                            "\'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\')";
                listSQL.Add(updateSql);
                updateSql = "if (not Exists(Select * from E_Parameter where ParaType = 'QualityRecord' and ParaNo = 'IsShowLoadDataButton'))" + "\r" +
                            "INSERT INTO E_Parameter(LabID, ParameterID, CName, SName, ParaType, ParaNo,  " + "\r" +
                            "ParaValue, ParaDesc, Shortcode, DispOrder, PinYinZiTou, IsUse, IsUserSet,  " + "\r" +
                            "DataAddTime, DataUpdateTime) VALUES(  " + "\r" +
                            "0" + "," + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "," +
                            "\'质量记录登记页面是否显示载入按钮\',null,\'QualityRecord\',\'IsShowLoadDataButton\',0" + "," +
                            "\'空或0为不显示【载入上次数据】按钮，1为显示【载入上次数据】按钮\',null,0,null,1,1," +
                            "\'" + DateTime.Now.ToString("yyyy -MM-dd HH:mm:ss") + "\'," +
                            "\'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\')";
                listSQL.Add(updateSql);
                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.61");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.61) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.62 
            if (IsUpdateDataBase(oldVersion, "1.0.0.62"))
            {
                List<string> listSQL = new List<string>();
                updateSql = " CREATE UNIQUE NONCLUSTERED INDEX IX_RBAC_User_Account ON dbo.RBAC_User  (    Account    ) WITH(STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY];ALTER TABLE dbo.RBAC_User SET (LOCK_ESCALATION = TABLE) ;";
                listSQL.Add(updateSql);
               
                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.62");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.62) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.63 
            if (IsUpdateDataBase(oldVersion, "1.0.0.63"))
            {
                List<string> listSQL = new List<string>();
                updateSql = "if (not Exists(Select * from E_Parameter where ParaType = 'QualityRecord' and ParaNo = 'IsShowLoadDataButton'))" + "\r" +
                            "INSERT INTO E_Parameter(LabID, ParameterID, CName, SName, ParaType, ParaNo,  " + "\r" +
                            "ParaValue, ParaDesc, Shortcode, DispOrder, PinYinZiTou, IsUse, IsUserSet,  " + "\r" +
                            "DataAddTime, DataUpdateTime) VALUES(  " + "\r" +
                            "0" + "," + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "," +
                            "\'质量记录登记页面是否显示载入按钮\',null,\'QualityRecord\',\'IsShowLoadDataButton\',0" + "," +
                            "\'空或0为不显示【载入上次数据】按钮，1为显示【载入上次数据】按钮\',null,0,null,1,1," +
                            "\'" + DateTime.Now.ToString("yyyy -MM-dd HH:mm:ss") + "\'," +
                            "\'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\')";
                listSQL.Add(updateSql);
                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.63");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.63) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.64 //QMS质量记录查询列表数据存储过程修改
            if (IsUpdateDataBase(oldVersion, "1.0.0.64"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " if exists(select * from dbo.sysobjects where id = object_id(N\'[dbo].[P_GetMaintenanceDataTB]\') and OBJECTPROPERTY(id, N\'IsProcedure\') = 1)" + "\r" +
                            " DROP PROCEDURE[dbo].[P_GetMaintenanceDataTB] ";
                listSQL.Add(updateSql);

                updateSql = " CREATE PROCEDURE[dbo].[P_GetMaintenanceDataTB] " + "\r" +
                             " @TempletID varchar(50), " + "\r" +
                             " @TypeCode varchar(50), " + "\r" +
                             " @BatchNumber varchar(50), " + "\r" +
                             " @SQLPara varchar(2000), " + "\r" +
                             " @BeginDate varchar(50), " + "\r" +
                             " @EndDate varchar(50) " + "\r" +
                             " AS " + "\r" +
                             " BEGIN " + "\r" +
                             "   declare  @SQL varchar(8000) " + "\r" +
                             "   declare  @where varchar(2000) " + "\r" +
                             "   set @SQL = '' " + "\r" +
                             "   set @where = ' where TempletID=' + @TempletID + ' and TempletTypeCode in (''TA'',''TB'',''TC'',''TD'',''TE'',''TF'',''TG''' + " + "\r" +
                             "                '''TH'',''TI'',''TJ'',''TK'',''TL'',''TM'',''TN''' + " + "\r" +
                             "                '''TO'',''TP'',''TQ'',''TR'',''TS'',''TT''' + " + "\r" +
                             "                '''TU'',''TV'',''TW'',''TX'',''TY'',''TZ''' + ')' " + "\r" +
                             "  if (@BatchNumber <> '') " + "\r" +
                             "    set @where = @where + ' and @BatchNumber=''' + @BatchNumber + '''' " + "\r" +
                             "   set @SQL = ' Select TempletID,BatchNumber,TempletTypeCode,ItemDate as ''操作日期'' ' + @SQLPara + " + "\r" +
                             "              ' from (Select Top 1000 * from E_MaintenanceData ' + @where + " + "\r" +
                             "              ' and TempletDataType=2 and TempletTypeCode=''' + @TypeCode + '''' + " + "\r" +
                             "              ' and ItemDate>=''' + @BeginDate + '''and ItemDate<''' + @EndDate + ''' order by DataAddTime) mm' + " + "\r" +
                             "              ' group by TempletID,TempletTypeCode,ItemDate,BatchNumber	' " + "\r" +
                             "    exec(@sql) " + "\r" +
                             " END ";
                listSQL.Add(updateSql);

                updateSql = "if (not Exists(Select * from E_Parameter where ParaType = 'QualityRecord' and ParaNo = 'IsAutoSaveLoadData'))" + "\r" +
                            "INSERT INTO E_Parameter(LabID, ParameterID, CName, SName, ParaType, ParaNo,  " + "\r" +
                            "ParaValue, ParaDesc, Shortcode, DispOrder, PinYinZiTou, IsUse, IsUserSet,  " + "\r" +
                            "DataAddTime, DataUpdateTime) VALUES(  " + "\r" +
                            "0" + "," + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "," +
                            "\'质量记录登记页面是否显示载入按钮\',null,\'QualityRecord\',\'IsAutoSaveLoadData\',0" + "," +
                            "\'空或0为不自动保存载入数据，1为自动保存载入数据\',null,0,null,1,1," +
                            "\'" + DateTime.Now.ToString("yyyy -MM-dd HH:mm:ss") + "\'," +
                            "\'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\')";
                listSQL.Add(updateSql);

                updateSql = "if (not Exists(Select * from E_Parameter where ParaType = 'QualityRecord' and ParaNo = 'IsSaveDataPreview'))" + "\r" +
                            "INSERT INTO E_Parameter(LabID, ParameterID, CName, SName, ParaType, ParaNo,  " + "\r" +
                            "ParaValue, ParaDesc, Shortcode, DispOrder, PinYinZiTou, IsUse, IsUserSet,  " + "\r" +
                            "DataAddTime, DataUpdateTime) VALUES(  " + "\r" +
                            "0" + "," + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "," +
                            "\'质量记录登记页面保存数据后是否直接预览\',null,\'QualityRecord\',\'IsSaveDataPreview\',0" + "," +
                            "\'空或0为保存后不预览数据，1保存后为预览数据\',null,0,null,1,1," +
                            "\'" + DateTime.Now.ToString("yyyy -MM-dd HH:mm:ss") + "\'," +
                            "\'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\')";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.64");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.64) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.65 //QMS质量记录增加参数
            if (IsUpdateDataBase(oldVersion, "1.0.0.65"))
            {
                List<string> listSQL = new List<string>();

                updateSql = "if (not Exists(Select * from E_Parameter where ParaType = 'QualityRecord' and ParaNo = 'IsSaveAllData'))" + "\r" +
                            "INSERT INTO E_Parameter(LabID, ParameterID, CName, SName, ParaType, ParaNo,  " + "\r" +
                            "ParaValue, ParaDesc, Shortcode, DispOrder, PinYinZiTou, IsUse, IsUserSet,  " + "\r" +
                            "DataAddTime, DataUpdateTime) VALUES(  " + "\r" +
                            "0" + "," + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "," +
                            "\'质量记录登记页面保存按钮保存数据的范围\',null,\'QualityRecord\',\'IsSaveAllData\',0" + "," +
                            "\'空或0为保存当前页签数据，1为保存全部页签数据\',null,0,null,1,1," +
                            "\'" + DateTime.Now.ToString("yyyy -MM-dd HH:mm:ss") + "\'," +
                            "\'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\')";
                listSQL.Add(updateSql);

                updateSql = "if (not Exists(Select * from E_Parameter where ParaType = 'QualityRecord' and ParaNo = 'LoadDataDays'))" + "\r" +
                            "INSERT INTO E_Parameter(LabID, ParameterID, CName, SName, ParaType, ParaNo,  " + "\r" +
                            "ParaValue, ParaDesc, Shortcode, DispOrder, PinYinZiTou, IsUse, IsUserSet,  " + "\r" +
                            "DataAddTime, DataUpdateTime) VALUES(  " + "\r" +
                            "0" + "," + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "," +
                            "\'质量记录登记页面载入数据的时间范围\',null,\'QualityRecord\',\'LoadDataDays\',7" + "," +
                            "\'默认载入7天之内的数据\',null,0,null,1,1," +
                            "\'" + DateTime.Now.ToString("yyyy -MM-dd HH:mm:ss") + "\'," +
                            "\'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\')";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.65");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.65) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.66 
            if (IsUpdateDataBase(oldVersion, "1.0.0.66"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " if exists(select * from dbo.sysobjects where id = object_id(N\'[dbo].[P_GetReportData]\') and OBJECTPROPERTY(id, N\'IsProcedure\') = 1)" + "\r" +
                            " DROP PROCEDURE [dbo].[P_GetReportData] ";
                listSQL.Add(updateSql);

                updateSql = " CREATE PROCEDURE [dbo].[P_GetReportData] " + "\r" +
                             " @TempletType int=0, " + "\r" +
                             " @TempletID varchar(50), " + "\r" +
                             " @EquipID varchar(50), " + "\r" +
                             " @EmpID varchar(50), " + "\r" +
                             " @BeginDate datetime, " + "\r" +
                             " @EndDate datetime, " + "\r" +
                             " @CheckType varchar(50), " + "\r" +
                             " @OtherPara varchar(500) " + "\r" +
                             " AS " + "\r" +
                             " BEGIN " + "\r" +
                             "     declare  @Where varchar(5000) " + "\r" +
                             "     declare  @SQL varchar(8000) " + "\r" +
                             "     declare  @TempletSQL varchar(5000) " + "\r" +
                             "     declare  @MonthCount int " + "\r" +
                             "     declare  @DateCalcType varchar(500) " + "\r" +
                             "     declare  @ReportDate varchar(500) " + "\r" +
                             "     declare  @IsCheck varchar(50) " + "\r" +
                             "     declare  @DataDate varchar(500) " + "\r" +
                             "     declare  @ItemDate varchar(500) " + "\r" +
                             "     set @Where = '' " + "\r" +
                             "     set @IsCheck = '' " + "\r" +
                             "     set @DateCalcType = 'month' " + "\r" +
                             "     set @ReportDate = '' " + "\r" +
                             "     set @DataDate='' " + "\r" +
                             "     set @ItemDate='' " + "\r" +
                             "     if @CheckType = '1' " + "\r" +
                             "     begin " + "\r" +
                             "       set @MonthCount = cast((datediff(day, @BeginDate, @EndDate)) as int) " + "\r" +
                             "       set @DateCalcType = 'day' " + "\r" +
                             "       set @ReportDate = ' CONVERT(varchar(10), dateadd(day,number,[@BeginDate]), 120) '" + "\r" +
                             "       set @DataDate = ' and ItemDate>='''+CONVERT(varchar(10),@BeginDate,120)+''' and ItemDate<=''' + CONVERT(varchar(10), @EndDate, 120) + ''''" + "\r" +
                             "       set @ItemDate = ',ItemDate'" + "\r" +
                             "     end else " + "\r" +
                             "     begin " + "\r" +
                             "       set @MonthCount = cast((datediff(month, @BeginDate, @EndDate)) as int) " + "\r" +
                             "       set @DateCalcType = 'month' " + "\r" +
                             "       set @EndDate=DATEADD(DAY,-1,DATEADD(MM,DATEDIFF(MM,0,@EndDate)+1,0)) " + "\r" +
                             "       set @ReportDate = ' CONVERT(varchar(8), dateadd(month,number,[@BeginDate]), 120) + ''01'' '" + "\r" +
                             "       set @DataDate = ' and ItemDate>='''+CONVERT(varchar(10),@BeginDate,120)+''' and ItemDate<=''' + CONVERT(varchar(10), @EndDate, 120) + ''''" + "\r" +
                             "       set @ItemDate = ',CONVERT(varchar(8), ItemDate, 120) + ''01'' as ItemDate'" + "\r" +
                             "     end " + "\r" +
                             "    if @TempletType = 0 " + "\r" +
                             "      set @TempletSQL = ' Select * from (select TempletID, EmpID from E_TempletEmp where EmpID=[@EmpID]' + " + "\r" +
                             "                        ' union select  b.TempletID, a.EmpID from E_ResEmp a ' + " + "\r" +
                             "                        ' inner join E_TempletRes b on a.ResponsibilityID=b.ResponsibilityID ' + " + "\r" +
                             "                        ' where a.EmpID=[@EmpID] Group by b.TempletID, a.EmpID) MM ' + " + "\r" +
                             "                        ' cross join (select [@ReportDate] as ReportDate ' + " + "\r" +
                             "                        ' from master.dbo.spt_values  where type =''P'' and number <=DATEDIFF([@DateCalcType], [@BeginDate],[@EndDate])) NN ' " + "\r" +
                             "    else if @TempletType = 1 " + "\r" +
                             "      Set @TempletSQL = ' Select * from (select TempletID, EmpID from E_TempletEmp where EmpID=[@EmpID]) MM ' + " + "\r" +
                             "                        ' cross join (select [@ReportDate] as ReportDate ' + " + "\r" +
                             "                        ' from master.dbo.spt_values  where type =''P'' and number <=DATEDIFF([@DateCalcType], [@BeginDate],[@EndDate])) NN ' " + "\r" +
                             "    else if @TempletType = 2 " + "\r" +
                             "      Set @TempletSQL = ' Select * from (Select  b.TempletID, a.EmpID  from E_ResEmp a ' + " + "\r" +
                             "                        ' inner join E_TempletRes b on a.ResponsibilityID=b.ResponsibilityID ' + " + "\r" +
                             "                        ' where a.EmpID=[@EmpID] Group by b.TempletID, a.EmpID) MM ' + " + "\r" +
                             "                        ' cross join (select [@ReportDate] as ReportDate ' + " + "\r" +
                             "                        ' from master.dbo.spt_values  where type =''P'' and number <=DATEDIFF([@DateCalcType], [@BeginDate],[@EndDate])) NN ' " + "\r" +
                             "    if (@MonthCount >= 0) " + "\r" +
                             "    begin " + "\r" +
                             "      set @TempletSQL = REPLACE(@TempletSQL, '[@EmpID]', @EmpID) " + "\r" +
                             "      set @TempletSQL = REPLACE(@TempletSQL, '[@ReportDate]', @ReportDate) " + "\r" +
                             "      set @TempletSQL = REPLACE(@TempletSQL, '[@BeginDate]', '''' + CONVERT(varchar(10), @BeginDate, 120) + '''') " + "\r" +
                             "      set @TempletSQL = REPLACE(@TempletSQL, '[@EndDate]', '''' + CONVERT(varchar(10), @EndDate, 120) + '''') " + "\r" +
                             "      set @TempletSQL = REPLACE(@TempletSQL, '[@DateCalcType]', @DateCalcType) " + "\r" +
                             "      if (@TempletSQL <> '') " + "\r" +
                             "      begin " + "\r" +
                             "         if (@CheckType <> '') " + "\r" +
                             "           set @Where = @Where + ' and ET.CheckType=' + cast(@CheckType as varchar(10)) " + "\r" +
                             "         if (@TempletID <> '') " + "\r" +
                             "           set @Where = @Where + ' and ETP.TempletID=' + @TempletID " + "\r" +
                             "         if (@EquipID <> '') " + "\r" +
                             "           set @Where = @Where + ' and ERD.EquipID=' + @EquipID " + "\r" +
                             "         if (@OtherPara <> '') " + "\r" +
                             "         begin " + "\r" +
                             "              --参数顺序为：是否审核&&质量记录模板名称&&质量记录模板代码&&质量记录仪器名称&&质量记录小组名称&&质量记录模板类型&&质量记录仪器类型 " + "\r" +
                             "           declare @StrPara varchar(500) " + "\r" +
                             "           declare @Para varchar(500) " + "\r" +
                             "           declare @Split varchar(50) " + "\r" +
                             "           declare @i int " + "\r" +
                             "           declare  @index int " + "\r" +
                             "           set @Split = '&&' " + "\r" +
                             "           set @StrPara = @OtherPara " + "\r" +
                             "           set @Para = '' " + "\r" +
                             "           set @i = 0 " + "\r" +
                             "           set @index = 0 " + "\r" +
                             "           set @index = CHARINDEX(@Split, @StrPara) " + "\r" +
                             "           while (@index >= 0) " + "\r" +
                             "           begin " + "\r" +
                             "             set @i = @i + 1 " + "\r" +
                             "             if (@index = 0 and @i> 0) " + "\r" +
                             "             begin " + "\r" +
                             "               set @index = -1 " + "\r" +
                             "               set @Para = @StrPara " + "\r" +
                             "             end else " + "\r" +
                             "             begin " + "\r" +
                             "               set @Para = SUBSTRING(@StrPara, 0, @index) " + "\r" +
                             "               set @StrPara = SUBSTRING(@StrPara, @index + 2, len(@StrPara)) " + "\r" +
                             "               set @index = CHARINDEX(@Split, @StrPara) " + "\r" +
                             "             end " + "\r" +
                             "             if @i = 1 " + "\r" +
                             "               set @IsCheck = @Para " + "\r" +
                             "             else if @i = 2 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and ET.CName like ''%' + @Para + '%''' " + "\r" +
                             "             else if @i = 3 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and ET.UseCode = ''' + @Para + '''' " + "\r" +
                             "             else if @i = 4 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and EE.CName like ''%' + @Para + '%''' " + "\r" +
                             "             else if @i = 5 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and ET.SectionID = ' + @Para " + "\r" +
                             "             else if @i = 6 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and ET.TempletTypeID = ' + @Para " + "\r" +
                             "             else if @i = 7 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and EE.EquipTypeID = ' + @Para " + "\r" +
                             "             else if @i = 8 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and HRD.UseCode = ''' + @Para + '''' " + "\r" +
                             "           end " + "\r" +
                             "         end " + "\r" +
                             "         if (@IsCheck <> '') " + "\r" +
                             "         begin " + "\r" +
                             "            if (@IsCheck = '0') " + "\r" +
                             "              set @Where = @Where + ' and (ERD.IsCheck is null or ERD.IsCheck=' + @IsCheck + ')' " + "\r" +
                             "  		  else " + "\r" +
                             "              set @Where = @Where + ' and ERD.IsCheck=' + @IsCheck " + "\r" +
                             "         end " + "\r" +
                             "         set @SQL = \'select ROW_NUMBER() OVER(ORDER BY TempletID ASC) AS Id, *  \' + " + "\r" +
                             "                     \'from(  \' + " + "\r" +
                             "                         \'select ETP.TempletID, ET.EquipID,ET.UseCode as TempletCode, ETP.ReportDate, ET.CName as ReportName, ET.SName as ReportSName,\' + " + "\r" +
                             "                         \'ERD.ReportDataID, ERD.LabID, ERD.ReportFilePath, ERD.ReportFileExt, ERD.IsCheck, \' + " + "\r" +
                             "                         \'ERD.Checker, ERD.CheckTime, ERD.CheckView, ERD.Comment, ERD.IsUse, \' + " + "\r" +
                             "                         \'ERD.DispOrder, ERD.DataAddTime, ERD.DataUpdateTime, EE.CName as EquipName, \' + " + "\r" +
                             "                         \'EE.UseCode as EquipUseCode, EE.Shortcode as EquipShortcode, EE.EName as EquipEName, \' + " + "\r" +
                             "                         \'PD.CName as EquipTypeName, ET.SectionID, HRD.CName as SectionName, EM.IsContainData, \' + " + "\r" +
                             "                         \'HRD.UseCode as SectionUseCode, HRD.Shortcode as SectionShortcode, HRD.StandCode as SectionStandCode, HRD.EName as SectionEName, PDI.CName as TempletTypeName, \' + " + "\r" +
                             "                         \'dbo.F_GetReportAttachment(ETP.TempletID, ET.EquipID, ETP.ReportDate, ETP.ReportDate,\'+@CheckType+\') as IsAttachment  \' + " + "\r" +
                             "                         \'from(\' + @TempletSQL + \') ETP   \' + " + "\r" +
                             "                         \'left join E_ReportData ERD on ETP.TempletID = ERD.TempletID '++' and ETP.ReportDate = ERD.ReportDate and ERD.IsUse=1 \' + " + "\r" +
                             "                         \'left join E_Templet ET on ET.TempletID = ETP.TempletID  \' + " + "\r" +
                             "                         \'left join (select TempletID,ItemDate, 1 as IsContainData from (Select TempletID '+@ItemDate+' from E_MaintenanceData where TempletDataType=2 '+@DataDate+') ZZ group by TempletID,ItemDate) EM on EM.TempletID = ETP.TempletID and EM.ItemDate=ETP.ReportDate \' + " + "\r" +
                             "                         \'left join P_Dict PDI on ET.TempletTypeID = PDI.DictID  \' + " + "\r" +
                             "                         \'left join E_Equip EE on EE.EquipID = ET.EquipID  \' + " + "\r" +
                             "                         \'left join P_Dict PD on EE.EquipTypeID = PD.DictID  \' + " + "\r" +
                             "                         \'left join HR_Dept HRD on ET.SectionID = HRD.DeptID   \' + " + "\r" +
                             "                         \'where 1=1 \' + @Where + " + "\r" +
                             "                     \') MM order by ReportDate, ReportName ASC \' " + "\r" +
                             "         exec(@SQL) " + "\r" +
                             "       end " + "\r" +
                             "     end " + "\r" +
                             " END ";
                listSQL.Add(updateSql);

                updateSql = " if Not Exists(Select * from SysColumns where [Name]= \'EquipNo\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_Equip\' ))" + "\r" +
                            " begin " + "\r" +
                            "   Alter Table E_Equip Add EquipNo varchar(200) " + "\r" +
                            "   EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'仪器编码\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                            "   @level1type = N\'TABLE\',@level1name = N\'E_Equip\', @level2type = N\'COLUMN\',@level2name = N\'EquipNo\'" + "\r" +
                            " end ";
                listSQL.Add(updateSql);

                updateSql = " if Not Exists(Select * from SysColumns where [Name]= \'FactoryName\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_Equip\' ))" + "\r" +
                            " begin " + "\r" +
                            "   Alter Table E_Equip Add FactoryName varchar(200) " + "\r" +
                            "   EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'厂商\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                            "   @level1type = N\'TABLE\',@level1name = N\'E_Equip\', @level2type = N\'COLUMN\',@level2name = N\'FactoryName\'" + "\r" +
                            " end ";
                listSQL.Add(updateSql);

                updateSql = " if Not Exists(Select * from SysColumns where [Name]= \'FactoryOutNo\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_Equip\' ))" + "\r" +
                            " begin " + "\r" +
                            "   Alter Table E_Equip Add FactoryOutNo varchar(200) " + "\r" +
                            "   EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'出厂编号\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                            "   @level1type = N\'TABLE\',@level1name = N\'E_Equip\', @level2type = N\'COLUMN\',@level2name = N\'FactoryOutNo\'" + "\r" +
                            " end ";
                listSQL.Add(updateSql);

                updateSql = " if Not Exists(Select * from SysColumns where [Name]= \'StoreArea\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_Equip\' ))" + "\r" +
                            " begin " + "\r" +
                            "   Alter Table E_Equip Add StoreArea varchar(200) " + "\r" +
                            "   EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'放置区域\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                            "   @level1type = N\'TABLE\',@level1name = N\'E_Equip\', @level2type = N\'COLUMN\',@level2name = N\'StoreArea\'" + "\r" +
                            " end ";
                listSQL.Add(updateSql);

                updateSql = " if Not Exists(Select * from SysColumns where [Name]= \'EnableDate\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_Equip\' ))" + "\r" +
                            " begin " + "\r" +
                            "   Alter Table E_Equip Add EnableDate datetime " + "\r" +
                            "   EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'启用日期\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                            "   @level1type = N\'TABLE\',@level1name = N\'E_Equip\', @level2type = N\'COLUMN\',@level2name = N\'EnableDate\'" + "\r" +
                            " end ";
                listSQL.Add(updateSql);

                updateSql = " if Not Exists(Select * from SysColumns where [Name]= \'CalibrateDate\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_Equip\' ))" + "\r" +
                            " begin " + "\r" +
                            "   Alter Table E_Equip Add CalibrateDate datetime " + "\r" +
                            "   EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'校准效期\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                            "   @level1type = N\'TABLE\',@level1name = N\'E_Equip\', @level2type = N\'COLUMN\',@level2name = N\'CalibrateDate\'" + "\r" +
                            " end ";
                listSQL.Add(updateSql);

                updateSql = "if (not Exists(Select * from E_Parameter where ParaType = 'QualityRecord' and ParaNo = 'CellIsWrapText'))" + "\r" +
                            "INSERT INTO E_Parameter(LabID, ParameterID, CName, SName, ParaType, ParaNo,  " + "\r" +
                            "ParaValue, ParaDesc, Shortcode, DispOrder, PinYinZiTou, IsUse, IsUserSet,  " + "\r" +
                            "DataAddTime, DataUpdateTime) VALUES(  " + "\r" +
                            "0" + "," + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "," +
                            "\'质量记录填充单元内容是否换行\',null,\'QualityRecord\',\'CellIsWrapText\',1" + "," +
                            "\'0为填充时不换行，1为填充时换行\',null,0,null,1,1," +
                            "\'" + DateTime.Now.ToString("yyyy -MM-dd HH:mm:ss") + "\'," +
                            "\'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\')";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.66");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.66) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.67 
            if (IsUpdateDataBase(oldVersion, "1.0.0.67"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " if exists(select * from dbo.sysobjects where id = object_id(N\'[dbo].[P_GetTempletGroupData]\') and OBJECTPROPERTY(id, N\'IsProcedure\') = 1)" + "\r" +
                            " DROP PROCEDURE[dbo].[P_GetTempletGroupData] ";
                listSQL.Add(updateSql);

                updateSql =   " CREATE PROCEDURE[dbo].[P_GetTempletGroupData] " + "\r" +
                              " @TempletID varchar(50), " + "\r" +
                              " @SQLField varchar(5000), " + "\r" +
                              " @SQLWhere varchar(5000), " + "\r" +
                              " @BeginDate varchar(50), " + "\r" +
                              " @EndDate varchar(50) " + "\r" +
                              " AS " + "\r" +
                              " BEGIN " + "\r" +
                                " declare  @SQL varchar(8000) " + "\r" +
                                " set @SQL = '' " + "\r" +
                                " set @SQL = ' Select TempletID,TempletBatNo,ItemDate as ''日期'' ' + @SQLField + " + "\r" +
                                          "  ' from (select top 1000 * from E_MaintenanceData ' + " + "\r" +
                                           " ' where TempletDataType=2 and TempletID=' + @TempletID + @SQLWhere + " + "\r" +
                                          "  ' and ItemDate>=''' + @BeginDate + '''and ItemDate<=''' + @EndDate + '''' + " + "\r" +
                                           " ' order by DataAddTime) MM ' + " + "\r" + 
                                           " 'group by TempletID,ItemDate,TempletBatNo' " + "\r" +
                                 " exec(@sql) " + "\r" +
                              " END ";
                listSQL.Add(updateSql);

                updateSql = " if Not Exists(Select * from SysColumns where [Name]= \'ShowFillItem\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_Templet\' ))" + "\r" +
                            " begin " + "\r" +
                            "   Alter Table E_Templet Add ShowFillItem varchar(500) " + "\r" +
                            "   EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'要显示的填充项目\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                            "   @level1type = N\'TABLE\',@level1name = N\'E_Templet\', @level2type = N\'COLUMN\',@level2name = N\'ShowFillItem\'" + "\r" +
                            " end ";
                listSQL.Add(updateSql);

                updateSql = " if Not Exists(Select * from SysColumns where [Name]= \'FillType\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_Templet\' ))" + "\r" +
                            " begin " + "\r" +
                            "   Alter Table E_Templet Add FillType int  " + "\r" +
                            "   EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'填充类型\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                            "   @level1type = N\'TABLE\',@level1name = N\'E_Templet\', @level2type = N\'COLUMN\',@level2name = N\'FillType\'" + "\r" +
                            " end ";
                listSQL.Add(updateSql);

                listSQL.Add("update E_Templet set FillType = CheckType where FillType is null ");

                updateSql = " if Not Exists(Select * from SysColumns where [Name]= \'TempletBatNo\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'E_MaintenanceData\' ))" + "\r" +
                            " begin " + "\r" +
                            "   Alter Table E_MaintenanceData Add TempletBatNo varchar(50)  " + "\r" +
                            "   EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'模板数据批次号\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                            "   @level1type = N\'TABLE\',@level1name = N\'E_MaintenanceData\', @level2type = N\'COLUMN\',@level2name = N\'TempletBatNo\'" + "\r" +
                            " end ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.67");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.67) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.68 
            if (IsUpdateDataBase(oldVersion, "1.0.0.68"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " if exists(select * from dbo.sysobjects where id = object_id(N\'[dbo].[P_GetReportData]\') and OBJECTPROPERTY(id, N\'IsProcedure\') = 1)" + "\r" +
                            " DROP PROCEDURE [dbo].[P_GetReportData] ";
                listSQL.Add(updateSql);

                updateSql = " CREATE PROCEDURE [dbo].[P_GetReportData] " + "\r" +
                             " @TempletType int=0, " + "\r" +
                             " @TempletID varchar(50), " + "\r" +
                             " @EquipID varchar(50), " + "\r" +
                             " @EmpID varchar(50), " + "\r" +
                             " @BeginDate datetime, " + "\r" +
                             " @EndDate datetime, " + "\r" +
                             " @CheckType varchar(50), " + "\r" +
                             " @OtherPara varchar(500) " + "\r" +
                             " AS " + "\r" +
                             " BEGIN " + "\r" +
                             "     declare  @Where varchar(5000) " + "\r" +
                             "     declare  @SQL varchar(8000) " + "\r" +
                             "     declare  @TempletSQL varchar(5000) " + "\r" +
                             "     declare  @MonthCount int " + "\r" +
                             "     declare  @DateCalcType varchar(500) " + "\r" +
                             "     declare  @ReportDate varchar(500) " + "\r" +
                             "     declare  @IsCheck varchar(50) " + "\r" +
                             "     declare  @DataDate varchar(500) " + "\r" +
                             "     declare  @ItemDate varchar(500) " + "\r" +
                             "     set @Where = '' " + "\r" +
                             "     set @IsCheck = '' " + "\r" +
                             "     set @DateCalcType = 'month' " + "\r" +
                             "     set @ReportDate = '' " + "\r" +
                             "     set @DataDate='' " + "\r" +
                             "     set @ItemDate='' " + "\r" +
                             "     if @CheckType = '1' " + "\r" +
                             "     begin " + "\r" +
                             "       set @MonthCount = cast((datediff(day, @BeginDate, @EndDate)) as int) " + "\r" +
                             "       set @DateCalcType = 'day' " + "\r" +
                             "       set @ReportDate = ' CONVERT(varchar(10), dateadd(day,number,[@BeginDate]), 120) '" + "\r" +
                             "       set @DataDate = ' and ItemDate>='''+CONVERT(varchar(10),@BeginDate,120)+''' and ItemDate<=''' + CONVERT(varchar(10), @EndDate, 120) + ''''" + "\r" +
                             "       set @ItemDate = ',ItemDate'" + "\r" +
                             "     end else " + "\r" +
                             "     begin " + "\r" +
                             "       set @MonthCount = cast((datediff(month, @BeginDate, @EndDate)) as int) " + "\r" +
                             "       set @DateCalcType = 'month' " + "\r" +
                             "       set @EndDate=DATEADD(DAY,-1,DATEADD(MM,DATEDIFF(MM,0,@EndDate)+1,0)) " + "\r" +
                             "       set @ReportDate = ' CONVERT(varchar(8), dateadd(month,number,[@BeginDate]), 120) + ''01'' '" + "\r" +
                             "       set @DataDate = ' and ItemDate>='''+CONVERT(varchar(10),@BeginDate,120)+''' and ItemDate<=''' + CONVERT(varchar(10), @EndDate, 120) + ''''" + "\r" +
                             "       set @ItemDate = ',CONVERT(varchar(8), ItemDate, 120) + ''01'' as ItemDate'" + "\r" +
                             "     end " + "\r" +
                             "    if @TempletType = 0 " + "\r" +
                             "      set @TempletSQL = ' Select * from (select TempletID, EmpID from E_TempletEmp where EmpID=[@EmpID]' + " + "\r" +
                             "                        ' union select  b.TempletID, a.EmpID from E_ResEmp a ' + " + "\r" +
                             "                        ' inner join E_TempletRes b on a.ResponsibilityID=b.ResponsibilityID ' + " + "\r" +
                             "                        ' where a.EmpID=[@EmpID] Group by b.TempletID, a.EmpID) MM ' + " + "\r" +
                             "                        ' cross join (select [@ReportDate] as ReportDate ' + " + "\r" +
                             "                        ' from master.dbo.spt_values  where type =''P'' and number <=DATEDIFF([@DateCalcType], [@BeginDate],[@EndDate])) NN ' " + "\r" +
                             "    else if @TempletType = 1 " + "\r" +
                             "      Set @TempletSQL = ' Select * from (select TempletID, EmpID from E_TempletEmp where EmpID=[@EmpID]) MM ' + " + "\r" +
                             "                        ' cross join (select [@ReportDate] as ReportDate ' + " + "\r" +
                             "                        ' from master.dbo.spt_values  where type =''P'' and number <=DATEDIFF([@DateCalcType], [@BeginDate],[@EndDate])) NN ' " + "\r" +
                             "    else if @TempletType = 2 " + "\r" +
                             "      Set @TempletSQL = ' Select * from (Select  b.TempletID, a.EmpID  from E_ResEmp a ' + " + "\r" +
                             "                        ' inner join E_TempletRes b on a.ResponsibilityID=b.ResponsibilityID ' + " + "\r" +
                             "                        ' where a.EmpID=[@EmpID] Group by b.TempletID, a.EmpID) MM ' + " + "\r" +
                             "                        ' cross join (select [@ReportDate] as ReportDate ' + " + "\r" +
                             "                        ' from master.dbo.spt_values  where type =''P'' and number <=DATEDIFF([@DateCalcType], [@BeginDate],[@EndDate])) NN ' " + "\r" +
                             "    if (@MonthCount >= 0) " + "\r" +
                             "    begin " + "\r" +
                             "      set @TempletSQL = REPLACE(@TempletSQL, '[@EmpID]', @EmpID) " + "\r" +
                             "      set @TempletSQL = REPLACE(@TempletSQL, '[@ReportDate]', @ReportDate) " + "\r" +
                             "      set @TempletSQL = REPLACE(@TempletSQL, '[@BeginDate]', '''' + CONVERT(varchar(10), @BeginDate, 120) + '''') " + "\r" +
                             "      set @TempletSQL = REPLACE(@TempletSQL, '[@EndDate]', '''' + CONVERT(varchar(10), @EndDate, 120) + '''') " + "\r" +
                             "      set @TempletSQL = REPLACE(@TempletSQL, '[@DateCalcType]', @DateCalcType) " + "\r" +
                             "      if (@TempletSQL <> '') " + "\r" +
                             "      begin " + "\r" +
                             "         if (@CheckType <> '') " + "\r" +
                             "           set @Where = @Where + ' and ET.CheckType=' + cast(@CheckType as varchar(10)) " + "\r" +
                             "         if (@TempletID <> '') " + "\r" +
                             "           set @Where = @Where + ' and ETP.TempletID=' + @TempletID " + "\r" +
                             "         if (@EquipID <> '') " + "\r" +
                             "           set @Where = @Where + ' and ERD.EquipID=' + @EquipID " + "\r" +
                             "         if (@OtherPara <> '') " + "\r" +
                             "         begin " + "\r" +
                             "              --参数顺序为：是否审核&&质量记录模板名称&&质量记录模板代码&&质量记录仪器名称&&质量记录小组名称&&质量记录模板类型&&质量记录仪器类型 " + "\r" +
                             "           declare @StrPara varchar(500) " + "\r" +
                             "           declare @Para varchar(500) " + "\r" +
                             "           declare @Split varchar(50) " + "\r" +
                             "           declare @i int " + "\r" +
                             "           declare  @index int " + "\r" +
                             "           set @Split = '&&' " + "\r" +
                             "           set @StrPara = @OtherPara " + "\r" +
                             "           set @Para = '' " + "\r" +
                             "           set @i = 0 " + "\r" +
                             "           set @index = 0 " + "\r" +
                             "           set @index = CHARINDEX(@Split, @StrPara) " + "\r" +
                             "           while (@index >= 0) " + "\r" +
                             "           begin " + "\r" +
                             "             set @i = @i + 1 " + "\r" +
                             "             if (@index = 0 and @i> 0) " + "\r" +
                             "             begin " + "\r" +
                             "               set @index = -1 " + "\r" +
                             "               set @Para = @StrPara " + "\r" +
                             "             end else " + "\r" +
                             "             begin " + "\r" +
                             "               set @Para = SUBSTRING(@StrPara, 0, @index) " + "\r" +
                             "               set @StrPara = SUBSTRING(@StrPara, @index + 2, len(@StrPara)) " + "\r" +
                             "               set @index = CHARINDEX(@Split, @StrPara) " + "\r" +
                             "             end " + "\r" +
                             "             if @i = 1 " + "\r" +
                             "               set @IsCheck = @Para " + "\r" +
                             "             else if @i = 2 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and ET.CName like ''%' + @Para + '%''' " + "\r" +
                             "             else if @i = 3 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and ET.UseCode = ''' + @Para + '''' " + "\r" +
                             "             else if @i = 4 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and EE.CName like ''%' + @Para + '%''' " + "\r" +
                             "             else if @i = 5 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and ET.SectionID = ' + @Para " + "\r" +
                             "             else if @i = 6 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and ET.TempletTypeID = ' + @Para " + "\r" +
                             "             else if @i = 7 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and EE.EquipTypeID = ' + @Para " + "\r" +
                             "             else if @i = 8 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and HRD.UseCode = ''' + @Para + '''' " + "\r" +
                             "           end " + "\r" +
                             "         end " + "\r" +
                             "         if (@IsCheck <> '') " + "\r" +
                             "         begin " + "\r" +
                             "            if (@IsCheck = '0') " + "\r" +
                             "              set @Where = @Where + ' and (ERD.IsCheck is null or ERD.IsCheck=' + @IsCheck + ')' " + "\r" +
                             "  		  else " + "\r" +
                             "              set @Where = @Where + ' and ERD.IsCheck=' + @IsCheck " + "\r" +
                             "         end " + "\r" +
                             "         set @SQL = \'select ROW_NUMBER() OVER(ORDER BY TempletID ASC) AS Id, *  \' + " + "\r" +
                             "                     \'from(  \' + " + "\r" +
                             "                         \'select ETP.TempletID, ET.EquipID,ET.UseCode as TempletCode, ETP.ReportDate, ET.CName as ReportName, ET.SName as ReportSName,\' + " + "\r" +
                             "                         \'ERD.ReportDataID, ERD.LabID, ERD.ReportFilePath, ERD.ReportFileExt, ERD.IsCheck, \' + " + "\r" +
                             "                         \'ERD.Checker, ERD.CheckTime, ERD.CheckView, ERD.Comment, ERD.IsUse, \' + " + "\r" +
                             "                         \'ERD.DispOrder, ERD.DataAddTime, ERD.DataUpdateTime, EE.CName as EquipName, \' + " + "\r" +
                             "                         \'EE.UseCode as EquipUseCode, EE.Shortcode as EquipShortcode, EE.EName as EquipEName, \' + " + "\r" +
                             "                         \'PD.CName as EquipTypeName, ET.SectionID, HRD.CName as SectionName, EM.IsContainData, EM.TempletBatNo, \' + " + "\r" +
                             "                         \'HRD.UseCode as SectionUseCode, HRD.Shortcode as SectionShortcode, HRD.StandCode as SectionStandCode, HRD.EName as SectionEName, PDI.CName as TempletTypeName, \' + " + "\r" +
                             "                         \'dbo.F_GetReportAttachment(ETP.TempletID, ET.EquipID, ETP.ReportDate, ETP.ReportDate,\'+@CheckType+\') as IsAttachment  \' + " + "\r" +
                             "                         \'from(\' + @TempletSQL + \') ETP   \' + " + "\r" +
                             "                         \'left join E_ReportData ERD on ETP.TempletID = ERD.TempletID and ETP.ReportDate = ERD.ReportDate and ERD.IsUse=1 \' + " + "\r" +
                             "                         \'left join E_Templet ET on ET.TempletID = ETP.TempletID  \' + " + "\r" +
                             "                         \'left join (select TempletID,ItemDate,TempletBatNo, 1 as IsContainData from (Select TempletID,TempletBatNo '+@ItemDate+' from E_MaintenanceData where TempletDataType=2 '+@DataDate+') ZZ group by TempletID,ItemDate,TempletBatNo) EM on EM.TempletID = ETP.TempletID and EM.ItemDate=ETP.ReportDate \' + " + "\r" +
                             "                         \'left join P_Dict PDI on ET.TempletTypeID = PDI.DictID  \' + " + "\r" +
                             "                         \'left join E_Equip EE on EE.EquipID = ET.EquipID  \' + " + "\r" +
                             "                         \'left join P_Dict PD on EE.EquipTypeID = PD.DictID  \' + " + "\r" +
                             "                         \'left join HR_Dept HRD on ET.SectionID = HRD.DeptID   \' + " + "\r" +
                             "                         \'where 1=1 \' + @Where + " + "\r" +
                             "                     \') MM order by ReportDate, ReportName ASC \' " + "\r" +
                             "         exec(@SQL) " + "\r" +
                             "       end " + "\r" +
                             "     end " + "\r" +
                             " END ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.68");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.68) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.69 
            if (IsUpdateDataBase(oldVersion, "1.0.0.69"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " if not exists (select * from P_Dict where DictID=5159055013066268159) INSERT[P_Dict]([DictID],[LabID],[DictTypeID],[Shortcode],[CName],[DispOrder],[IsUse],[Memo],[DataAddTime]) VALUES( 5159055013066268159,0,4658943850913198560, N'QMSFFReviseNoRule', N'QMS文档修订号规则',100,1, N'修订号规则:固定前缀|变化前缀(默认取年份)|当前序号', N'2019/01/17 12:27:06');";
                listSQL.Add(updateSql);

                updateSql = " if not exists (select * from [B_Parameter] where ParameterID=5572684236403160299) INSERT[B_Parameter]([LabID],[ParameterID],[PDictId],[Name],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES( 1,5572684236403160299,5159055013066268159, N'QMS文档修订号规则', N'SYS', N'QMSFFReviseNoRule', N'FF|0000|0000', N'修订号规则:固定前缀|变化前缀(默认取年份)|当前序号;<br />默认值为:FF|0000|0000',10,1,1, N'2019/01/17 12:28:34');";
                listSQL.Add(updateSql);


                updateSql = "if (not Exists(Select * from P_DictType where DictTypeCode = 'ReviseReason'))" + "\r" +
                       "INSERT INTO P_DictType(LabID,DictTypeID,DictTypeCode,CName,DispOrder,IsUse,Memo,StandCode,DeveCode,DataAddTime" + "\r" +
                       ") VALUES(  " + "\r" +
                       "0" + "," + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "," +
                       "\'ReviseReason\','修订原因',\'0\'" + "," +
                        "\'1\',null,\'NULL\',NULL" + "," +
                       "\'" + DateTime.Now.ToString("yyyy -MM-dd HH:mm:ss") +  "\')";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.69");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.69) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.70 //QMS质量记录增加参数
            if (IsUpdateDataBase(oldVersion, "1.0.0.70"))
            {
                List<string> listSQL = new List<string>();

                updateSql = "if (not Exists(Select * from E_Parameter where ParaType = 'QualityRecord' and ParaNo = 'IsQRDate'))" + "\r" +
                            "INSERT INTO E_Parameter(LabID, ParameterID, CName, SName, ParaType, ParaNo,  " + "\r" +
                            "ParaValue, ParaDesc, Shortcode, DispOrder, PinYinZiTou, IsUse, IsUserSet,  " + "\r" +
                            "DataAddTime, DataUpdateTime) VALUES(  " + "\r" +
                            "0" + "," + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "," +
                            "\'质量记录操作日期取质量记录日期\',null,\'QualityRecord\',\'IsQRDate\',0" + "," +
                            "\'空或0为取默认的操作日期，1为取质量记录日期\',null,0,null,1,1," +
                            "\'" + DateTime.Now.ToString("yyyy -MM-dd HH:mm:ss") + "\'," +
                            "\'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\')";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.70");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.70) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.71 //QMS质量记录增加参数
            if (IsUpdateDataBase(oldVersion, "1.0.0.71"))
            {
                List<string> listSQL = new List<string>();

                updateSql = "if (not Exists(Select * from E_Parameter where ParaType = 'QualityRecord' and ParaNo = 'IsJudgeDayBeforeData'))" + "\r" +
                            "INSERT INTO E_Parameter(LabID, ParameterID, CName, SName, ParaType, ParaNo,  " + "\r" +
                            "ParaValue, ParaDesc, Shortcode, DispOrder, PinYinZiTou, IsUse, IsUserSet,  " + "\r" +
                            "DataAddTime, DataUpdateTime) VALUES(  " + "\r" +
                            "0" + "," + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "," +
                            "\'质量记录按日登记页面保存时判断前一日是否登记数据\',null,\'QualityRecord\',\'IsJudgeDayBeforeData\',0" + "," +
                            "\'空或0时不做判断，为1时判断。如未做登记，则提示用户登记数据\',null,0,null,1,1," +
                            "\'" + DateTime.Now.ToString("yyyy -MM-dd HH:mm:ss") + "\'," +
                            "\'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\')";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.71");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.71) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.72
            if (IsUpdateDataBase(oldVersion, "1.0.0.72"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " if exists(select * from dbo.sysobjects where id = object_id(N\'[dbo].[P_GetReportData]\') and OBJECTPROPERTY(id, N\'IsProcedure\') = 1)" + "\r" +
                            " DROP PROCEDURE [dbo].[P_GetReportData] ";
                listSQL.Add(updateSql);

                updateSql = " CREATE PROCEDURE [dbo].[P_GetReportData] " + "\r" +
                             " @TempletType int=0, " + "\r" +
                             " @TempletID varchar(50), " + "\r" +
                             " @EquipID varchar(50), " + "\r" +
                             " @EmpID varchar(50), " + "\r" +
                             " @BeginDate datetime, " + "\r" +
                             " @EndDate datetime, " + "\r" +
                             " @CheckType varchar(50), " + "\r" +
                             " @OtherPara varchar(500) " + "\r" +
                             " AS " + "\r" +
                             " BEGIN " + "\r" +
                             "     declare  @Where varchar(5000) " + "\r" +
                             "     declare  @SQL varchar(8000) " + "\r" +
                             "     declare  @TempletSQL varchar(5000) " + "\r" +
                             "     declare  @MonthCount int " + "\r" +
                             "     declare  @DateCalcType varchar(500) " + "\r" +
                             "     declare  @ReportDate varchar(500) " + "\r" +
                             "     declare  @IsCheck varchar(50) " + "\r" +
                             "     declare  @DataDate varchar(500) " + "\r" +
                             "                                     " + "\r" +
                             "     set @Where = '' " + "\r" +
                             "     set @IsCheck = '' " + "\r" +
                             "     set @DateCalcType = 'month' " + "\r" +
                             "     set @ReportDate = '' " + "\r" +
                             "     set @DataDate='' " + "\r" +
                             "                      " + "\r" +
                             "     if @CheckType = '1' " + "\r" +
                             "     begin " + "\r" +
                             "       set @MonthCount = cast((datediff(day, @BeginDate, @EndDate)) as int) " + "\r" +
                             "       set @DateCalcType = 'day' " + "\r" +
                             "       set @ReportDate = ' CONVERT(varchar(10), dateadd(day,number,[@BeginDate]), 120) '" + "\r" +
                             "       set @DataDate = ' and ItemDate='''+CONVERT(varchar(10),@BeginDate,120) + ''''" + "\r" +
                             "     end else " + "\r" +
                             "     begin " + "\r" +
                             "       set @MonthCount = cast((datediff(month, @BeginDate, @EndDate)) as int) " + "\r" +
                             "       set @DateCalcType = 'month' " + "\r" +
                             "       set @EndDate=DATEADD(DAY,-1,DATEADD(MM,DATEDIFF(MM,0,@EndDate)+1,0)) " + "\r" +
                             "       set @ReportDate = ' CONVERT(varchar(8), dateadd(month,number,[@BeginDate]), 120) + ''01'' ' " + "\r" +
                             "       set @DataDate = ' and ItemDate>=''' + CONVERT(varchar(10), @BeginDate, 120) + '''' + ' and ItemDate<=''' + CONVERT(varchar(10), @EndDate, 120) + '''' " + "\r" +
                             "     end " + "\r" +
                             "    if @TempletType = 0 " + "\r" +
                             "      set @TempletSQL = ' Select * from (select TempletID, EmpID from E_TempletEmp where EmpID=[@EmpID]' + " + "\r" +
                             "                        ' union select  b.TempletID, a.EmpID from E_ResEmp a ' + " + "\r" +
                             "                        ' inner join E_TempletRes b on a.ResponsibilityID=b.ResponsibilityID ' + " + "\r" +
                             "                        ' where a.EmpID=[@EmpID] Group by b.TempletID, a.EmpID) MM ' + " + "\r" +
                             "                        ' cross join (select [@ReportDate] as ReportDate ' + " + "\r" +
                             "                        ' from master.dbo.spt_values  where type =''P'' and number <=DATEDIFF([@DateCalcType], [@BeginDate],[@EndDate])) NN ' " + "\r" +
                             "    else if @TempletType = 1 " + "\r" +
                             "      Set @TempletSQL = ' Select * from (select TempletID, EmpID from E_TempletEmp where EmpID=[@EmpID]) MM ' + " + "\r" +
                             "                        ' cross join (select [@ReportDate] as ReportDate ' + " + "\r" +
                             "                        ' from master.dbo.spt_values  where type =''P'' and number <=DATEDIFF([@DateCalcType], [@BeginDate],[@EndDate])) NN ' " + "\r" +
                             "    else if @TempletType = 2 " + "\r" +
                             "      Set @TempletSQL = ' Select * from (Select  b.TempletID, a.EmpID  from E_ResEmp a ' + " + "\r" +
                             "                        ' inner join E_TempletRes b on a.ResponsibilityID=b.ResponsibilityID ' + " + "\r" +
                             "                        ' where a.EmpID=[@EmpID] Group by b.TempletID, a.EmpID) MM ' + " + "\r" +
                             "                        ' cross join (select [@ReportDate] as ReportDate ' + " + "\r" +
                             "                        ' from master.dbo.spt_values  where type =''P'' and number <=DATEDIFF([@DateCalcType], [@BeginDate],[@EndDate])) NN ' " + "\r" +
                             "    if (@MonthCount >= 0) " + "\r" +
                             "    begin " + "\r" +
                             "      set @TempletSQL = REPLACE(@TempletSQL, '[@EmpID]', @EmpID) " + "\r" +
                             "      set @TempletSQL = REPLACE(@TempletSQL, '[@ReportDate]', @ReportDate) " + "\r" +
                             "      set @TempletSQL = REPLACE(@TempletSQL, '[@BeginDate]', '''' + CONVERT(varchar(10), @BeginDate, 120) + '''') " + "\r" +
                             "      set @TempletSQL = REPLACE(@TempletSQL, '[@EndDate]', '''' + CONVERT(varchar(10), @EndDate, 120) + '''') " + "\r" +
                             "      set @TempletSQL = REPLACE(@TempletSQL, '[@DateCalcType]', @DateCalcType) " + "\r" +
                             "      if (@TempletSQL <> '') " + "\r" +
                             "      begin " + "\r" +
                             "         if (@CheckType <> '') " + "\r" +
                             "           set @Where = @Where + ' and ET.CheckType=' + cast(@CheckType as varchar(10)) " + "\r" +
                             "         if (@TempletID <> '') " + "\r" +
                             "           set @Where = @Where + ' and ETP.TempletID=' + @TempletID " + "\r" +
                             "         if (@EquipID <> '') " + "\r" +
                             "           set @Where = @Where + ' and ERD.EquipID=' + @EquipID " + "\r" +
                             "         if (@OtherPara <> '') " + "\r" +
                             "         begin " + "\r" +
                             "              --参数顺序为：是否审核&&质量记录模板名称&&质量记录模板代码&&质量记录仪器名称&&质量记录小组名称&&质量记录模板类型&&质量记录仪器类型 " + "\r" +
                             "           declare @StrPara varchar(500) " + "\r" +
                             "           declare @Para varchar(500) " + "\r" +
                             "           declare @Split varchar(50) " + "\r" +
                             "           declare @i int " + "\r" +
                             "           declare  @index int " + "\r" +
                             "           set @Split = '&&' " + "\r" +
                             "           set @StrPara = @OtherPara " + "\r" +
                             "           set @Para = '' " + "\r" +
                             "           set @i = 0 " + "\r" +
                             "           set @index = 0 " + "\r" +
                             "           set @index = CHARINDEX(@Split, @StrPara) " + "\r" +
                             "           while (@index >= 0) " + "\r" +
                             "           begin " + "\r" +
                             "             set @i = @i + 1 " + "\r" +
                             "             if (@index = 0 and @i> 0) " + "\r" +
                             "             begin " + "\r" +
                             "               set @index = -1 " + "\r" +
                             "               set @Para = @StrPara " + "\r" +
                             "             end else " + "\r" +
                             "             begin " + "\r" +
                             "               set @Para = SUBSTRING(@StrPara, 0, @index) " + "\r" +
                             "               set @StrPara = SUBSTRING(@StrPara, @index + 2, len(@StrPara)) " + "\r" +
                             "               set @index = CHARINDEX(@Split, @StrPara) " + "\r" +
                             "             end " + "\r" +
                             "             if @i = 1 " + "\r" +
                             "               set @IsCheck = @Para " + "\r" +
                             "             else if @i = 2 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and ET.CName like ''%' + @Para + '%''' " + "\r" +
                             "             else if @i = 3 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and ET.UseCode = ''' + @Para + '''' " + "\r" +
                             "             else if @i = 4 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and EE.CName like ''%' + @Para + '%''' " + "\r" +
                             "             else if @i = 5 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and ET.SectionID = ' + @Para " + "\r" +
                             "             else if @i = 6 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and ET.TempletTypeID = ' + @Para " + "\r" +
                             "             else if @i = 7 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and EE.EquipTypeID = ' + @Para " + "\r" +
                             "             else if @i = 8 and @Para<> '' " + "\r" +
                             "               set @Where = @Where + ' and HRD.UseCode = ''' + @Para + '''' " + "\r" +
                             "           end " + "\r" +
                             "         end " + "\r" +
                             "         if (@IsCheck <> '') " + "\r" +
                             "         begin " + "\r" +
                             "            if (@IsCheck = '0') " + "\r" +
                             "              set @Where = @Where + ' and (ERD.IsCheck is null or ERD.IsCheck=' + @IsCheck + ')' " + "\r" +
                             "  		  else " + "\r" +
                             "              set @Where = @Where + ' and ERD.IsCheck=' + @IsCheck " + "\r" +
                             "         end " + "\r" +
                             "         set @SQL = \'select ROW_NUMBER() OVER(ORDER BY TempletID ASC) AS Id, *  \' + " + "\r" +
                             "                     \'from(  \' + " + "\r" +
                             "                         \'select ETP.TempletID, ET.EquipID,ET.UseCode as TempletCode, ETP.ReportDate, ET.CName as ReportName, ET.SName as ReportSName,\' + " + "\r" +
                             "                         \'ERD.ReportDataID, ERD.LabID, ERD.ReportFilePath, ERD.ReportFileExt, ERD.IsCheck, \' + " + "\r" +
                             "                         \'ERD.Checker, ERD.CheckTime, ERD.CheckView, ERD.Comment, ERD.IsUse, \' + " + "\r" +
                             "                         \'ERD.DispOrder, ERD.DataAddTime, ERD.DataUpdateTime, EE.CName as EquipName, \' + " + "\r" +
                             "                         \'EE.UseCode as EquipUseCode, EE.Shortcode as EquipShortcode, EE.EName as EquipEName, \' + " + "\r" +
                             "                         \'PD.CName as EquipTypeName, ET.SectionID, HRD.CName as SectionName, EM.IsContainData,'''' as TempletBatNo, \' + " + "\r" +
                             "                         \'HRD.UseCode as SectionUseCode, HRD.Shortcode as SectionShortcode, HRD.StandCode as SectionStandCode, HRD.EName as SectionEName, PDI.CName as TempletTypeName, \' + " + "\r" +
                             "                         \'dbo.F_GetReportAttachment(ETP.TempletID, ET.EquipID, ETP.ReportDate, ETP.ReportDate,\'+@CheckType+\') as IsAttachment  \' + " + "\r" +
                             "                         \'from(\' + @TempletSQL + \') ETP   \' + " + "\r" +
                             "                         \'left join E_ReportData ERD on ETP.TempletID = ERD.TempletID and ETP.ReportDate = ERD.ReportDate and ERD.IsUse=1 \' + " + "\r" +
                             "                         \'left join E_Templet ET on ET.TempletID = ETP.TempletID  \' + " + "\r" +
                             "                         \'left join (Select TempletID, 1 as IsContainData from E_MaintenanceData where TempletDataType=2 and ItemResult is not null ' + @DataDate + ' group by TempletID) EM on EM.TempletID = ET.TempletID \' + " + "\r" +
                             "                         \'left join P_Dict PDI on ET.TempletTypeID = PDI.DictID  \' + " + "\r" +
                             "                         \'left join E_Equip EE on EE.EquipID = ET.EquipID  \' + " + "\r" +
                             "                         \'left join P_Dict PD on EE.EquipTypeID = PD.DictID  \' + " + "\r" +
                             "                         \'left join HR_Dept HRD on ET.SectionID = HRD.DeptID   \' + " + "\r" +
                             "                         \'where 1=1 \' + @Where + " + "\r" +
                             "                     \') MM order by ReportDate, ReportName ASC \' " + "\r" +
                             "         exec(@SQL) " + "\r" +
                             "       end " + "\r" +
                             "     end " + "\r" +
                             " END ";
                listSQL.Add(updateSql);

                updateSql = "if (not Exists(Select * from E_Parameter where ParaType = 'QualityRecord' and ParaNo = 'FileDeptUpDownType'))" + "\r" +
                            "INSERT INTO E_Parameter(LabID, ParameterID, CName, SName, ParaType, ParaNo,  " + "\r" +
                            "ParaValue, ParaDesc, Shortcode, DispOrder, PinYinZiTou, IsUse, IsUserSet,  " + "\r" +
                            "DataAddTime, DataUpdateTime) VALUES(  " + "\r" +
                            "1" + "," + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "," +
                            "\'文档查看是否获取员工所属部门的上下级部门文档\',null,\'QualityRecord\',\'FileDeptUpDownType\',1" + "," +
                            "\'空或0时不获取上下级部门ID，为1获取上级部门ID列表，为2时获取下级部门ID列表，为3时获取上下级部门ID列表。默认为1。" +
                            "文档查看页面，查询按部门发布的文档时，是否获取员工所属部门的上下级部门文档。\',null,310,null,1,1," +
                            "\'" + DateTime.Now.ToString("yyyy -MM-dd HH:mm:ss") + "\'," +
                            "\'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\')";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);

                if (result)
                    result = UpateCompareVersionInfo("1.0.0.72");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.72) Update Error, Please Check The Log!");
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
                string mainAssemblyVersion = DicVersion[newVersion];
                if (CompareAssemblyVersion(curAssemblyVersion, mainAssemblyVersion))
                    result = true;
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
                            if (int.Parse(oldVersionList[i]) < int.Parse(newVersionList[i]))
                            {
                                result = true;
                                break;
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
            string curVersion = "";
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
