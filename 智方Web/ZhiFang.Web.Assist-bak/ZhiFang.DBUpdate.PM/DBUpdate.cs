using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;


namespace ZhiFang.DBUpdate.PM
{
    public class DBUpdate
    {
        public static string ADOConnectStr = GetADODataBaseSettings(ZhiFang.Common.Public.ConfigHelper.GetDataBaseSettings("databaseSettings", "db.connectionString"));

        static Dictionary<string, string> DicVersion = GetVersionComparison();//

        static string MainAssemblyFile = "ZhiFang.WebAssist.dll";//可以从配置文件获取

        /// <summary>
        /// 初始化数据库版本和主程序集版本关系，key数据库版本，value主程序集版本
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, string> GetVersionComparison()
        {
            //每更新一次版本，需要手工在这里添加对应关系
            Dictionary<string, string> dicVersion = new Dictionary<string, string>();
            dicVersion.Add("1.0.0.1", "1.0.0.1");//1.添加数据库版本升级运行参数;
            dicVersion.Add("1.0.0.2", "1.0.0.2");//公共部分
            dicVersion.Add("1.0.0.3", "1.0.0.3");//权限部分
            dicVersion.Add("1.0.0.4", "1.0.0.4");//初始化角色及角色权限
            dicVersion.Add("1.0.0.5", "1.0.0.5");//初始化功能模块
            dicVersion.Add("1.0.0.6", "1.0.0.6");//院感部分
            dicVersion.Add("1.0.0.7", "1.0.0.7");//院感部分
            dicVersion.Add("1.0.0.8", "1.0.0.8");//院感部分
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
            CheckBParameterIsIsExists();
            oldVersion = GetDataBaseCurVersion();

            #region 1.0.0.1 
            if (IsUpdateDataBase(oldVersion, "1.0.0.1"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region B_Parameter
                updateSql = "  if object_id('B_Parameter') is null CREATE TABLE [dbo].[B_Parameter]( [LabID] [bigint] NOT NULL, [ParameterID] [bigint] NOT NULL, [NodeID] [bigint] NULL, [GroupNo] [bigint] NULL, [Name] [varchar](200) NULL, [SName] [varchar](40) NULL, [ParaNo] [varchar](100) NULL, [ParaType] [varchar](100) NULL, [ParaValue] [varchar](max) NULL, [ParaDesc] [ntext] NULL, [Shortcode] [varchar](100) NULL, [PinYinZiTou] [varchar](100) NULL, [IsUse] [bit] NULL, [DataAddTime] [datetime] NULL, [DataUpdateTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, [DispOrder] [int] NULL, [PDictId] [bigint] NULL, CONSTRAINT [PK_B_PARAMETER] PRIMARY KEY CLUSTERED( [ParameterID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]; ";
                listSQL.Add(updateSql);

                updateSql = "  if not exists(select * from B_Parameter where ParameterID=5233849820005451764) INSERT [B_Parameter]([LabID],[ParameterID],[Name],[SName],[ParaNo],[ParaType],[ParaValue],[ParaDesc],[Shortcode],[IsUse],[DataAddTime],[DispOrder]) VALUES ( 1,5233849820005451764,N'数据库版本号',N'全系统',N'SYS_DBVersion',N'SYS',N'1.0.0.1',N'数据库版本号',N'-1000',1,N'2017/08/03 23:52:02',-1000); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_Parameter', 'NodeID') IS NULL ALTER TABLE B_Parameter ADD NodeID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_Parameter', 'GroupNo') IS NULL ALTER TABLE B_Parameter ADD GroupNo bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5081884485807967905) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,5081884485807967905,N'登录后升级数据库',N'系统',N'CONFIG',N'BL-SYSE-UDAL-0001',N'1',N'1:是;0:否;',10,1,1,N'2020-02-15 10:13:44'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5085688375696300791) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,5085688375696300791,N'列表默认分页记录数',N'UI',N'CONFIG',N'BL-LRMP-UIPA-0003',N'10',N'列表默认分页记录数',10,1,1,N'2020-02-15 10:15:29'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5214493501844260801) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,5214493501844260801,N'集成平台服务访问URL',N'集成平台',N'CONFIG',N'BL-SYSE-LURL-0002',N'http://localhost/ZhiFang.LabInformationIntegratePlatform',N'配置调用集成平台服务的URL',10,1,1,N'2020-02-15 10:14:38'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5524339378542032882) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,5524339378542032882,N'启用用户UI配置',N'UI',N'CONFIG',N'BL-EUSE-UICF-0008',N'0',N'1:是;0:否;',1,1,1,N'2020-02-15 10:15:59'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5214493501844260811) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,5214493501844260811,N'CS服务访问URL',N'CS接口',N'CONFIG',N'BL-SYSE-CSRL-0011',N'http://localhost',N'配置调用CS服务的URL',11,1,1,N'2020-03-16 10:14:38'); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_Parameter', 'ItemEditInfo') IS NULL ALTER TABLE B_Parameter ADD ItemEditInfo ntext; ";
                listSQL.Add(updateSql);

                updateSql = " update B_Parameter set SName='医生站',ItemEditInfo=N'{\"ItemXType\":\"textfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"\",\"ItemDataSet\":\"\"}' where ParameterID=5691564273202278074 and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"0\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''是''},{''0'':''否''}]\"}' where ParameterID=5524339378542032882 and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"0\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''是''},{''0'':''否''}]\"}' where ParameterID=5214493501844260901 and ItemEditInfo is null; ";
                listSQL.Add(updateSql);

                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"1\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''是''},{''0'':''否''}]\"}' where ParameterID=5214493501844260900 and ItemEditInfo is null; ";
                listSQL.Add(updateSql);

                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"textfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"\",\"ItemDataSet\":\"\"}' where ParameterID=5214493501844260811 and ItemEditInfo is null; ";
                listSQL.Add(updateSql);

                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"textfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"\",\"ItemDataSet\":\"\"}' where ParameterID=5214493501844260801 and ItemEditInfo is null; ";
                listSQL.Add(updateSql);

                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"0\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''是''},{''0'':''否''}]\"}' where ParameterID=5101233254823494116 and ItemEditInfo is null; ";
                listSQL.Add(updateSql);

                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"10\",\"ItemUnit\":\"\",\"ItemDataSet\":\"\"}' where ParameterID=5085688375696300791 and ItemEditInfo is null; ";
                listSQL.Add(updateSql);

                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"1\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''是''},{''0'':''否''}]\"}' where ParameterID=5081884485807967905 and ItemEditInfo is null; ";
                listSQL.Add(updateSql);

                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"textfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"\",\"ItemDataSet\":\"\"}' where ParameterID=5000278512090896900 and ItemEditInfo is null; ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 10000) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[ItemEditInfo],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,10000,N'完成评估后超过天数未归档',N'院感',N'CONFIG',N'GK-CPEV-DAYS-0009',N'30',N'完成评估后超过天数未归档',N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"30\",\"ItemUnit\":\"天\",\"ItemDataSet\":\"\"}',10000,1,0,N'2020-11-13 15:41:44'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 10010) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaDesc],[ItemEditInfo],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,10010,N'指定自动核收检验小组',N'院感',N'CONFIG',N'GK-AUTO-TEAM-0010',N'指定自动核收检验小组',N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"\",\"ItemDataSet\":\"\"}',10010,1,0,N'2020-11-13 15:42:58'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 10030) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaDesc],[ItemEditInfo],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,10030,N'指定自动核收默认检验者编码',N'院感',N'CONFIG',N'GK-AUTO-ACCE-0011',N'指定自动核收默认检验者编码',N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"\",\"ItemDataSet\":\"\"}',10030,1,0,N'2020-11-13 15:44:02'); ";
                listSQL.Add(updateSql);

                #endregion

                #region Department
                updateSql = " IF COL_LENGTH('Department', 'LabID') IS NULL ALTER TABLE Department ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Department', 'DataAddTime') IS NULL ALTER TABLE Department ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Department', 'DataTimeStamp') IS NULL ALTER TABLE Department ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " Update Department set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Department set DispOrder=DeptNo; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Department', 'ParentID') IS NULL ALTER TABLE Department ADD ParentID int; ";
                listSQL.Add(updateSql);

                updateSql = " update Department set ParentID=0 where ParentID is null; ";
                listSQL.Add(updateSql);

                //updateSql = " IF COL_LENGTH('Department', 'CName') IS NOT NULL ALTER TABLE Department ALTER COLUMN CName NVARCHAR(50); ";
                //listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Department', 'ShortName') IS NOT NULL ALTER TABLE Department ALTER COLUMN ShortName NVARCHAR(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Department', 'ShortCode') IS NOT NULL ALTER TABLE Department ALTER COLUMN ShortCode NVARCHAR(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Department', 'MonitorType') IS NULL ALTER TABLE Department ADD MonitorType bigint; ";
                listSQL.Add(updateSql);

                //科室信息表添加“感控监测类型”：1:感控监测;0:科室监测;默认为科室监测
                updateSql = " Update Department set MonitorType=0 where MonitorType is null; ";
                listSQL.Add(updateSql);

                #endregion

                #region DepartmentUser
                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DepartmentUser]') AND type in (N'U')) CREATE TABLE [dbo].[DepartmentUser]( [LabID] [bigint] NULL, [DeptEmpID] [bigint] NOT NULL, [DeptNo] [int] NULL, [UserNo] [int] NULL, [IsUse] [bit] NULL, [DispOrder] [int] NULL, [DataAddTime] [datetime] NULL, [DataUpdateTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_DepartmentUser] PRIMARY KEY CLUSTERED ( [DeptEmpID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY]; ";
                listSQL.Add(updateSql);

                #endregion

                #region PUser
                updateSql = " IF COL_LENGTH('PUser', 'LabID') IS NULL ALTER TABLE PUser ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('PUser', 'DataTimeStamp') IS NULL ALTER TABLE PUser ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " Update PUser set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('PUser', 'DoctorNo') IS NULL ALTER TABLE PUser ADD DoctorNo int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('PUser', 'DispOrder') IS NULL ALTER TABLE PUser ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " update PUser set DispOrder=UserNo; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('PUser', 'LabID') IS NULL ALTER TABLE PUser ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('PUser', 'DataAddTime') IS NULL ALTER TABLE PUser ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('PUser', 'DataTimeStamp') IS NULL ALTER TABLE PUser ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " Update PUser set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " alter table PUser alter column ShortCode varchar(20); ";
                listSQL.Add(updateSql);

                #endregion

                #region Doctor
                updateSql = " IF COL_LENGTH('Doctor', 'LabID') IS NULL ALTER TABLE Doctor ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Doctor', 'DataAddTime') IS NULL ALTER TABLE Doctor ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Doctor', 'DataTimeStamp') IS NULL ALTER TABLE Doctor ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " Update Doctor set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Doctor', 'DispOrder') IS NULL ALTER TABLE Doctor ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " update Doctor set DispOrder=DoctorNo; ";
                listSQL.Add(updateSql);
                #endregion

                #region NPUser
                updateSql = " IF COL_LENGTH('NPUser', 'LabID') IS NULL ALTER TABLE NPUser ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('NPUser', 'DataAddTime') IS NULL ALTER TABLE NPUser ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('NPUser', 'DataTimeStamp') IS NULL ALTER TABLE NPUser ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " Update NPUser set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.1");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.1) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.2
            //公共部分
            if (IsUpdateDataBase(oldVersion, "1.0.0.2"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region SC_Operation
                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SC_Operation]') AND type in (N'U')) CREATE TABLE [dbo].[SC_Operation]( [LabID] [bigint] NOT NULL, [SCOperationID] [bigint] NOT NULL, [BobjectID] [bigint] NOT NULL, [Type] [bigint] NULL, [TypeName] [varchar](20) NULL, [BusinessModuleCode] [varchar](20) NULL, [Memo] [varchar](max) NULL, [DispOrder] [int] NULL, [IsUse] [bit] NULL, [CreatorID] [bigint] NULL, [CreatorName] [varchar](50) NULL, [DataAddTime] [datetime] NULL, [DataUpdateTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_SC_OPERATION] PRIMARY KEY CLUSTERED ( [SCOperationID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY]; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('SC_Operation', 'TypeName') IS NOT NULL ALTER TABLE SC_Operation ALTER COLUMN TypeName varchar(50);  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('SC_Operation', 'BusinessModuleCode') IS NOT NULL ALTER TABLE SC_Operation ALTER COLUMN BusinessModuleCode varchar(50);  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('SC_Operation', 'Memo') IS NOT NULL ALTER TABLE SC_Operation ALTER COLUMN Memo text;  ";
                listSQL.Add(updateSql);
                #endregion

                #region B_DictTree
                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[B_DictTree]') AND type in (N'U')) CREATE TABLE [dbo].[B_DictTree]( [LabID] bigint NOT NULL, [TypeTreeID] bigint NOT NULL, [ParentID] bigint NULL, [CName] [varchar](100) NOT NULL, [SName] [varchar](40) NULL, [Shortcode] [varchar](40) NULL, [Memo] [varchar](500) NULL, [DispOrder] [int] NULL, [IsUse] [bit] NULL, [CreatorID] bigint NULL, [CreatorName] [varchar](50) NULL, [DataAddTime] [datetime] NULL, [DataUpdateTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, [StandCode] [varchar](50) NULL, [DeveCode] [varchar](50) NULL, CONSTRAINT [PK_F_TYPETREE] PRIMARY KEY CLUSTERED ( [TypeTreeID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY];  ";
                listSQL.Add(updateSql);
                #endregion

                #region B_DictType
                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[B_DictType]') AND type in (N'U')) CREATE TABLE [dbo].[B_DictType]( [LabID] [bigint] NULL, [DCId] [bigint] NOT NULL, [DictTypeCode] [varchar](100) NULL, [CName] [varchar](40) NULL, [SName] [varchar](80) NULL, [ShortCode] [varchar](40) NULL, [PinYinZiTou] [varchar](50) NULL, [UseCode] [varchar](60) NULL, [DeveCode] [varchar](60) NULL, [StandCode] [varchar](60) NULL, [IsUse] [bit] NULL, [DispOrder] [int] NULL, [Memo] [ntext] NULL, [DataAddTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_B_DictType] PRIMARY KEY NONCLUSTERED( [DCId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]; ";
                listSQL.Add(updateSql);

                //字典类型添加医嘱作废原因
                updateSql = " IF NOT EXISTS(SELECT * FROM B_DictType WHERE [DCId] = 10) INSERT [B_DictType] ([LabID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime],[DictTypeCode]) VALUES ( 0,10,N'医嘱作废原因',100,N'医生站医嘱申请作废原因',1,N'2019/08/21 09:29:26',N'BReqFormObsolete'); ";
                listSQL.Add(updateSql);

                //字典类型添加输血目的
                updateSql = " IF NOT EXISTS(SELECT * FROM B_DictType WHERE [DCId] = 5586711528094669430) INSERT [B_DictType] ([LabID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime],[DictTypeCode]) VALUES ( 0,5586711528094669430,N'输血目的',260,N'输血目的',1,N'2019-09-03 17:08:46',N'UsePurpose'); ";
                listSQL.Add(updateSql);

                //字典类型添加用血方式
                updateSql = " IF NOT EXISTS(SELECT * FROM B_DictType WHERE [DCId] = 5586711528094669431) INSERT [B_DictType] ([LabID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime],[DictTypeCode]) VALUES ( 0,5586711528094669431,N'用血方式',260,N'用血方式',1,N'2019-10-08 16:06:52',N'BloodWay'); ";
                listSQL.Add(updateSql);

                //接口对照映射类型
                updateSql = " IF NOT EXISTS(SELECT * FROM B_DictType WHERE [DCId] = 300) INSERT [B_DictType] ([LabID],[DCId],[DictTypeCode],[CName],[SName],[ShortCode],[PinYinZiTou],[IsUse],[DispOrder],[Memo],[DataAddTime]) VALUES ( 0,300,N'SCInterfaceMaping',N'接口对照映射类型',N'JKDZYSLX',N'JKDZYSLX',N'JKDZYSLX',1,300,N'在该类型下维护各种业务接口的对照',N'2020-07-30 10:07:34'); ";
                listSQL.Add(updateSql);

                #endregion

                #region B_Dict
                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[B_Dict]') AND type in (N'U')) CREATE TABLE [dbo].[B_Dict]( [LabID] [bigint] NULL, [DID] [bigint] NOT NULL, [DCId] [bigint] NULL, [CName] [varchar](80) NULL, [SName] [varchar](80) NULL, [ShortCode] [varchar](40) NULL, [UseCode] [varchar](60) NULL, [DeveCode] [varchar](60) NULL, [StandCode] [varchar](60) NULL, [PinYinZiTou] [varchar](50) NULL, [HisOrderCode] [varchar](100) NULL, [IsUse] [bit] NULL, [DispOrder] [int] NULL, [DataAddTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, [Memo] [ntext] NULL, CONSTRAINT [PK_B_Dict] PRIMARY KEY NONCLUSTERED( [DID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]; ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 4713710510004576645) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime]) VALUES ( 0,4713710510004576645,10,N'患者出院',10,N'患者出院',1,N'2019/08/21 09:35:00'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 5418855651502951451) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime]) VALUES ( 0,5418855651502951451,10,N'其他原因',30,N'其他原因',1,N'2019/08/21 09:35:37'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 5753580128494855321) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime]) VALUES ( 0,5753580128494855321,10,N'患者转院',20,N'患者转院',1,N'2019/08/21 09:35:14'); ";
                listSQL.Add(updateSql);


                updateSql = " IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 300120) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[SName],[ShortCode],[PinYinZiTou],[DeveCode],[IsUse],[DispOrder],[DataAddTime],[Memo]) VALUES ( 0,300120,300,N'人员HIS对照',N'RYHISDZ',N'RYHISDZ',N'RYHISDZ',N'PUser',1,30012000,N'2020-07-30 10:10:15',N'300120:人员对照;'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 300130) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[SName],[ShortCode],[PinYinZiTou],[DeveCode],[IsUse],[DispOrder],[DataAddTime],[Memo]) VALUES ( 0,300130,300,N'科室HIS对照',N'KSHISDZ',N'KSHISDZ',N'KSHISDZ',N'Department',1,30013000,N'2020-07-30 10:10:37',N'300130:科室对照;'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 300160) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[SName],[ShortCode],[PinYinZiTou],[DeveCode],[IsUse],[DispOrder],[DataAddTime],[Memo]) VALUES ( 0,300160,300,N'检验项目LIS对照',N'JYXMLISDZ',N'JYXMLISDZ',N'JYXMLISDZ',N'TestItem',1,300160,N'2020-07-30 10:13:55',N'300160:检验项目LIS对照;'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 300200) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[SName],[ShortCode],[PinYinZiTou],[UseCode],[DeveCode],[IsUse],[DispOrder],[DataAddTime],[Memo]) VALUES ( 0,300200,300,N'申请类型HIS对照',N'SQLXHISDZ',N'SQLXHISDZ',N'SQLXHISDZ',N'20',N'BDict',1,300200,N'2020-07-30 10:23:35',N'300200:申请类型HIS对照;'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 300210) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[SName],[ShortCode],[PinYinZiTou],[UseCode],[DeveCode],[IsUse],[DispOrder],[DataAddTime],[Memo]) VALUES ( 0,300210,300,N'就诊类型HIS对照',N'JZLXHISDZ',N'JZLXHISDZ',N'JZLXHISDZ',N'25',N'BDict',1,300210,N'2020-07-30 10:24:23',N'300210:就诊类型HIS对照;'); ";
                listSQL.Add(updateSql);

                #endregion

                #region B_UserUIConfig
                updateSql = " if exists(select 1 from sysobjects where id = object_id('B_UserUIConfig') and type = 'U') drop table B_UserUIConfig create table B_UserUIConfig ( LabID bigint null, UserUIID bigint not null, UserUIKey varchar(100) null, UserUIName varchar(100) null, TemplateTypeID bigint null, TemplateTypeCName varchar(100) null, UITypeID bigint null, UITypeName varchar(100) null, ModuleId bigint null, EmpID bigint null, IsDefault bit null, Comment ntext collate Chinese_PRC_CI_AS null, DispOrder int null, IsUse bit null, DataAddTime datetime null, DataUpdateTime datetime null, DataTimeStamp timestamp null, constraint PK_B_USERUICONFIG primary key (UserUIID)); ";
                listSQL.Add(updateSql);

                #endregion

                #region B_Template                
                updateSql = "IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[B_Template]') AND type in(N'U')) CREATE TABLE [dbo].[B_Template]( [LabID] [bigint] NULL, [TemplateID] [bigint] NOT NULL, [CName] [varchar](100) NULL, [TypeID] [bigint] NULL, [TypeName] [varchar](50) NULL, [FilePath] [varchar](500) NULL, [FileExt] [varchar](50) NULL, [ContentType] [varchar](100) NULL, [FileSize] [float] NULL, [SName] [varchar](80) NULL, [Shortcode] [varchar](80) NULL, [PinYinZiTou] [varchar](50) NULL, [DispOrder] [int] NULL, [Comment] [ntext] NULL, [IsUse] [bit] NULL, [DataAddTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, [IsDefault] [bit] NULL, [FileName] [varchar](100) NULL, [ExcelRuleInfo] [ntext] NULL, [TemplateType] [varchar](40) NULL, CONSTRAINT [PK_B_TEMPLATE] PRIMARY KEY CLUSTERED( [TemplateID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                #region SC_RecordType
                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SC_RecordType]') AND type in(N'U')) CREATE TABLE [dbo].[SC_RecordType]( [LabID] [bigint] NULL, [RecordTypeID] [bigint] NOT NULL, [ContentTypeID] [int] NULL, [CName] [varchar](50) NULL, [TypeCode] [varchar](50) NULL, [TestItemCode] [varchar](60) NULL, [SampleTypeCode] [varchar](60) NULL, [SName] [varchar](80) NULL, [ShortCode] [varchar](50) NULL, [PinYinZiTou] [varchar](50) NULL, [DispOrder] [int] NULL, [Description] [ntext] NULL, [BGColor] [varchar](60) NULL, [Memo] [ntext] NULL, [IsUse] [bit] NULL, [DataAddTime] [datetime] NULL, [DataUpdateTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_SC_RecordType] PRIMARY KEY NONCLUSTERED( [RecordTypeID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];  ";
                listSQL.Add(updateSql);

                #endregion

                #region SC_RecordTypeItem
                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SC_RecordTypeItem]') AND type in(N'U')) CREATE TABLE [dbo].[SC_RecordTypeItem]( [LabID] [bigint] NULL, [RecordTypeItemID] [bigint] NOT NULL, [ItemCode] [varchar](50) NULL, [CName] [varchar](50) NULL, [SName] [varchar](80) NULL, [ShortCode] [varchar](50) NULL, [PinYinZiTou] [varchar](50) NULL, [ItemEditInfo] [ntext] NULL, [DefaultValue] [varchar](100) NULL, [ItemXType] [varchar](100) NULL, [ItemUnit] [varchar](100) NULL, [Description] [ntext] NULL, [BGColor] [varchar](60) NULL, [Memo] [ntext] NULL, [DispOrder] [int] NULL, [IsUse] [bit] NULL, [DataAddTime] [datetime] NULL, [DataUpdateTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_SC_RecordTypeItem] PRIMARY KEY NONCLUSTERED( [RecordTypeItemID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];   ";
                listSQL.Add(updateSql);

                #endregion

                #region SC_RecordPhrase
                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SC_RecordPhrase]') AND type in(N'U')) CREATE TABLE [dbo].[SC_RecordPhrase]( [LabID] [bigint] NULL, [PhraseID] [bigint] NOT NULL, [PhraseType] [bigint] NULL,[TypeObjectId] [bigint] NULL,[BObjectId] [bigint] NULL, [CName] [varchar](80) NULL, [SName] [varchar](80) NULL, [ShortCode] [varchar](40) NULL, [PinYinZiTou] [varchar](50) NULL, [IsUse] [bit] NULL, [Memo] [ntext] NULL, [DispOrder] [int] NULL, [DataAddTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_SC_RecordPhrase] PRIMARY KEY NONCLUSTERED( [PhraseID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];  ";
                listSQL.Add(updateSql);

                #endregion

                #region SC_RecordItemLink
                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SC_RecordItemLink]') AND type in(N'U')) CREATE TABLE [dbo].[SC_RecordItemLink]( [LabID] [bigint] NULL, [RecordLinkId] [bigint] NOT NULL, [RecordTypeID] [bigint] NOT NULL, [RecordTypeItemID] [bigint] NOT NULL, [TestItemCode] [varchar](50) NULL, [IsBillVisible] [bit] NULL, [DispOrder] [int] NULL, [IsUse] [bit] NULL, [DataAddTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_SC_RecordItemLink] PRIMARY KEY CLUSTERED( [RecordLinkId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY]; ";
                listSQL.Add(updateSql);

                #endregion

                #region SC_BarCodeRules
                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SC_BarCodeRules]') AND type in(N'U')) CREATE TABLE [dbo].[SC_BarCodeRules]( [LabID] [bigint] NULL, [RulesId] [bigint] NOT NULL, [BmsType] [varchar](60) NULL, [CurBarCode] [bigint] NULL, [DataAddTime] [datetime] NULL, [DataUpdateTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_SC_BarCodeRules] PRIMARY KEY CLUSTERED( [RulesId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY];  ";
                listSQL.Add(updateSql);

                #endregion

                #region B_LodopTemplet
                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[B_LodopTemplet]') AND type in(N'U')) CREATE TABLE [dbo].[B_LodopTemplet]( [LabID] [bigint] NOT NULL, [LTId] [bigint] NOT NULL, [CName] [varchar](40) NULL, [TypeCode] [varchar](50) NULL, [TypeCName] [varchar](50) NULL, [PaperType] [varchar](50) NULL, [PrintingDirection] [varchar](50) NULL, [PaperWidth] [float] NULL, [PaperHigh] [float] NULL, [PaperUnit] [varchar](50) NULL, [TemplateCode] [ntext] NULL, [DataTestValue] [ntext] NULL, [DispOrder] [int] NULL, [Memo] [ntext] NULL, [IsUse] [bit] NULL, [DataAddTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_B_LODOPTEMPLET] PRIMARY KEY CLUSTERED( [LTId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]; ";
                listSQL.Add(updateSql);

                #endregion

                #region SC_InterfaceMaping
                updateSql = "  IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SC_InterfaceMaping]') AND type in(N'U')) CREATE TABLE [dbo].[SC_InterfaceMaping]( [LabID] [bigint] NULL, [MappingId] [bigint] NOT NULL, [BobjectType] [bigint] NOT NULL, [BobjectID] [bigint] NOT NULL, [MapingCode] [varchar](60) NULL, [IsUse] [bit] NULL, [DispOrder] [int] NULL, [DataAddTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_SC_InterfaceMaping] PRIMARY KEY CLUSTERED( [MappingId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY]; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.2");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.2) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.3
            //权限部分
            if (IsUpdateDataBase(oldVersion, "1.0.0.3"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region RBAC_Module
                updateSql = "    IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RBAC_Module]') AND type in(N'U')) create table RBAC_Module ( LabID bigint null, ModuleID bigint not null, AppComID bigint null, ParentID bigint null, LevelNum int null, TreeCatalog int null, IsLeaf bit null, ModuleType int null, PicFile nvarchar(200) null, URL nvarchar(150) null, Para nvarchar(500) null, Owner varchar(50) null, UseCode varchar(50) null, StandCode varchar(50) null, DeveCode varchar(50) null, CName nvarchar(50) null, EName varchar(50) null, SName varchar(50) null, Shortcode varchar(20) null, PinYinZiTou varchar(50) null, Comment nvarchar(500) null, IsUse bit null, DispOrder int null, DataAddTime datetime null, DataUpdateTime datetime null, DataTimeStamp timestamp null, constraint PK_RBAC_MODULE primary key clustered (ModuleID)); ";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_Role
                updateSql = "   IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RBAC_Role]') AND type in(N'U'))  CREATE TABLE [dbo].[RBAC_Role]( [LabID] [bigint] NULL, [RoleID] [bigint] NOT NULL, [ParentID] [bigint] NULL, [LevelNum] [int] NULL, [TreeCatalog] [int] NULL, [UseCode] [varchar](50) NULL, [StandCode] [varchar](50) NULL, [DeveCode] [varchar](50) NULL, [CName] [nvarchar](50) NULL, [EName] [varchar](50) NULL, [SName] [varchar](50) NULL, [Shortcode] [varchar](20) NULL, [PinYinZiTou] [varchar](50) NULL, [Comment] [ntext] NULL, [IsUse] [bit] NULL, [DispOrder] [int] NULL, [DataAddTime] [datetime] NULL, [DataUpdateTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, [SySType] [bigint] NULL, CONSTRAINT [PK_RBAC_ROLE] PRIMARY KEY CLUSTERED( [RoleID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]; ";
                listSQL.Add(updateSql);

                #endregion

                #region RBAC_EmpOptions
                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RBAC_EmpOptions]') AND type in(N'U')) create table RBAC_EmpOptions ( LabID bigint null, EmpOptionsID bigint not null, EmpID bigint null, DefaultModuleID bigint null, ModuleIconSize int null, NewModuleLookTime datetime null, AllModuleIconSize int null, IsLock bit null, ModuleInfoSize bigint null, NewInfoModuleLookTime datetime null, DataAddTime datetime null, DataUpdateTime datetime null, DataTimeStamp timestamp null, constraint PK_RBAC_EMPOPTIONS primary key clustered (EmpOptionsID)); ";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_EmpRoles
                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RBAC_EmpRoles]') AND type in(N'U')) create table RBAC_EmpRoles ( LabID bigint null, EmpRoleID bigint not null, EmpID bigint null, RoleID bigint null, IsUse bit null, DispOrder int null, DataAddTime datetime null, DataUpdateTime datetime null, DataTimeStamp timestamp null, Validity varchar(500) null, constraint PK_RBAC_EMPROLES primary key clustered (EmpRoleID)); ";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_ModuleOper
                updateSql = "  IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RBAC_ModuleOper]') AND type in(N'U')) create table RBAC_ModuleOper ( LabID bigint null, ModuleOperID bigint not null, AppComOperateID bigint null, ModuleID bigint null, RowFilterID bigint null, InvisibleOrDisable int not null, UseCode varchar(50) null, StandCode varchar(50) null, DeveCode varchar(50) null, CName nvarchar(50) null, EName varchar(50) null, SName varchar(50) null, Shortcode varchar(20) null, PinYinZiTou varchar(50) null, Comment ntext null, IsUse bit null, DispOrder int null, DefaultChecked bit null, OperateURL varchar(200) null, RowFilterURL varchar(200) null, RowFilterBase varchar(500) null, FilterCondition varchar(500) null, ColFilterURL varchar(200) null, ColFilterBase varchar(500) null, ColFilterDesc varchar(50) null, DataAddTime datetime null, DataUpdateTime datetime null, DataTimeStamp timestamp null, UseRowFilter bit null, PredefinedField ntext null, constraint PK_RBAC_MODULEOPER primary key clustered (ModuleOperID)); ";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_RoleModule
                updateSql = "  IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RBAC_RoleModule]') AND type in(N'U')) create table RBAC_RoleModule ( LabID bigint null, ModuleVisiteID bigint not null, ModuleID bigint null, RoleID bigint null, IsUse bit null, DispOrder int null, IsOftenUse int null, IsDefaultOpen int null, DataAddTime datetime null, DataUpdateTime datetime null, DataTimeStamp timestamp null, constraint PK_RBAC_ROLEMODULE primary key clustered (ModuleVisiteID)); ";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_RoleRight
                updateSql = "   IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RBAC_RoleRight]') AND type in(N'U')) create table RBAC_RoleRight ( LabID bigint null, RightID bigint not null, RoleID bigint null, ModuleOperID bigint null, RowFilterID bigint null, Comment ntext null, IsUse bit null, DispOrder int null, DataAddTime datetime null, DataUpdateTime datetime null, DataTimeStamp timestamp null, constraint PK_RBAC_ROLERIGHT primary key clustered (RightID)); ";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_RowFilter
                updateSql = "   IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RBAC_RowFilter]') AND type in(N'U')) create table RBAC_RowFilter( LabID bigint null, RowFilterID bigint not null, CName nvarchar(50) null, EName varchar(50) null, SName varchar(50) null, RowFilterCondition ntext null, StandCode varchar(50) null, DeveCode varchar(50) null, PinYinZiTou varchar(50) null, Shortcode varchar(20) null, Comment ntext null, IsUse bit null, DispOrder int null, DataAddTime datetime null, DataUpdateTime datetime null, DataTimeStamp timestamp null, RowFilterConstruction varchar(8000) null, constraint PK_RBAC_ROWFILTER primary key clustered (RowFilterID)); ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.3");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.3) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.4
            //初始化角色
            if (IsUpdateDataBase(oldVersion, "1.0.0.4"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region 初始化角色
                updateSql = "  IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 1000) INSERT [RBAC_Role]([LabID],[RoleID],[LevelNum],[TreeCatalog],[UseCode],[StandCode],[DeveCode],[CName],[SName],[Shortcode],[PinYinZiTou],[Comment],[IsUse],[DispOrder],[DataAddTime],[SySType]) VALUES ( 0,1000,0,0,N'1000',N'1000',N'1000',N'系统管理员',N'系统',N'XTGLY',N'XTGLY',N'系统管理员',1,1000,N'2020-04-02 11:03:13',1); IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 10000) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[UseCode],[StandCode],[DeveCode],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime],[SySType]) VALUES ( 0,10000,0,0,N'10000',N'10000',N'10000',N'智方管理',N'智方',N'ZFGL',N'ZFGL',1,-1000,N'2020-04-03 14:28:19',1); IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20010) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[UseCode],[StandCode],[DeveCode],[CName],[SName],[Shortcode],[PinYinZiTou],[Comment],[IsUse],[DispOrder],[DataAddTime],[SySType]) VALUES ( 0,20010,0,0,N'20010',N'20010',N'20010',N'医生角色',N'医生站',N'YSJS',N'YSJS',N'20060',1,20010,N'2020-04-03 14:24:23',2); IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20020) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[UseCode],[StandCode],[DeveCode],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime],[SySType]) VALUES ( 0,20020,0,0,N'20020',N'20020',N'20020',N'护士角色',N'护士站',N'HSJS',N'HSJS',1,20020,N'2020-04-03 14:24:38',3); IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20030) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[UseCode],[StandCode],[DeveCode],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime],[SySType]) VALUES ( 0,20030,0,0,N'20030',N'20030',N'20030',N'输血科',N'输血科',N'SXK',N'SXK',1,20030,N'2020-04-03 14:25:49',7); IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20040) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[UseCode],[StandCode],[DeveCode],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime],[SySType]) VALUES ( 0,20040,0,0,N'20040',N'20040',N'20040',N'护工角色',N'护工',N'HGJS',N'HGJS',1,20040,N'2020-04-03 14:25:03',5); IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20050) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[UseCode],[StandCode],[DeveCode],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime],[SySType]) VALUES ( 0,20050,0,0,N'20050',N'20050',N'20050',N'统计和报表',N'统计',N'TJHBB',N'TJHBB',1,20050,N'2020-04-03 14:27:42',8); IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20060) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[UseCode],[StandCode],[DeveCode],[CName],[SName],[Shortcode],[PinYinZiTou],[Comment],[IsUse],[DispOrder],[DataAddTime],[SySType]) VALUES ( 0,20060,0,0,N'20060',N'20060',N'20060',N'感染管理部',N'科室',N'GRGLB',N'GRGLB',N'感染管理部',1,20060,N'2020-11-11 10:25:28',6);  ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.4");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.4) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.5
            //初始化功能模块
            if (IsUpdateDataBase(oldVersion, "1.0.0.5"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region 初始化功能模块

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 20) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,20,0,0,0,0,0,N'package.PNG',N'基础维护',N'JCWH',N'JCWH',1,-200,N'2020-11-02 15:34:25'); IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30,0,0,0,0,3,N'package.PNG',N'院感系统',N'YGXT',N'YGXT',1,30,N'2020-11-02 15:36:43'); IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100,0,0,0,0,3,N'package.PNG',N'血库系统',N'XKXT',N'XKXT',1,100,N'2020-04-02 10:36:15'); IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 1000) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,1000,0,0,0,0,3,N'package.PNG',N'权限系统',N'QXXT',N'QXXT',1,10000,N'2016-03-22 17:54:17'); IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 2000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,2000,100,0,0,0,3,N'package.PNG',N'医生站',1,500,N'2020-04-03 10:21:18'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 7000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,7000,20,0,0,0,3,N'package.PNG',N'系统参数',N'XTCS',N'XTCS',1,100,N'2020-04-03 10:21:18'); IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 7100) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,7100,7000,0,0,0,0,N'configuration.PNG',N'#Shell.class.assist.bparameters.runparams.App',N'运行参数',N'YXCS',N'YXCS',1,20,N'2020-04-03 14:04:34'); IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 7200) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,7200,7000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.bparameter.App?issys=1',N'全局参数',N'QJCS',N'QJCS',1,10,N'2020-04-03 14:04:02'); IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 7300) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,7300,7000,0,0,0,0,N'configuration.PNG',N'#Shell.class.assist.bparameters.setparams.Form',N'用户设置',N'YHSZ',N'YHSZ',1,30,N'2020-04-03 14:05:10');  ";
                listSQL.Add(updateSql);

                updateSql = "  IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8000) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8000,20,0,0,0,3,N'package.PNG',N'基础维护6.6',N'JCWH6.6',N'JCWH6.6',1,200,N'2020-04-03 10:21:18'); IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8010) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8010,8000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.doctor.App',N'医生维护',N'YSWH',N'YSWH',1,30,N'2020-04-03 14:07:51'); IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8020) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8020,8000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.puser.App',N'人员维护',N'RYWH',N'RYWH',1,20,N'2020-04-03 14:07:29'); IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8030,8000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.deptuser.App',N'科室人员维护',N'KSRYWH',N'KSRYWH',1,40,N'2020-04-03 14:08:18'); IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8100) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8100,8000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.department.App',N'科室维护',N'KSWH',N'KSWH',1,10,N'2020-04-03 14:06:58'); ";
                listSQL.Add(updateSql);

                updateSql = "  IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9000,20,0,0,0,3,N'package.PNG',N'基础维护',N'JCWH',N'JCWH',1,300,N'2020-04-03 10:21:18') IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9010) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9010,9000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.dicttype.App',N'公共字典类型',N'GGZDLX',N'GGZDLX',1,10,N'2020-04-03 14:08:54'); IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9020) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9020,9000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.dict.App',N'公共字典维护',N'GGZDWH',N'GGZDWH',1,20,N'2020-04-03 14:09:17'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30000) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30000,30,0,0,0,3,N'package.PNG',N'基础维护',N'JCWH',N'JCWH',1,30000,N'2020-11-11 10:18:29'); IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30001) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30001,30000,0,0,0,0,N'dictionary.PNG',N'#Shell.class.sysbase.screcordtype.App',N'监测类型维护',N'JCLXWH',N'JCLXWH',1,30001,N'2020-11-11 10:19:44'); IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30002) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30002,30000,0,0,0,0,N'dictionary.PNG',N'#Shell.class.sysbase.screcordtypeitem.App',N'样品信息维护',N'XYJLXWH',N'XYJLXWH',1,30002,N'2020-11-11 10:20:21'); IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30003) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30003,30000,0,0,0,0,N'dictionary.PNG',N'#Shell.class.sysbase.screcordphrase.ofdept.App',N'记录项短语-按科室',N'JLXDY-AKS',N'JLXDY-AKS',1,30003,N'2020-11-11 10:21:00'); IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30004) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30004,30000,0,0,0,0,N'list.PNG',N'#Shell.class.sysbase.deptautochecklink.App',N'科室自动核收维护',N'KSZDHS',N'KSZDHS',1,30004,N'2020-11-11 10:21:51');IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] =30005) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30005,30000,0,0,0,0,N'list.PNG',N'#Shell.class.sysbase.screcorditemlink.App',N'记录项类型关系',N'JLXLXGX',N'JLXLXGX',1,30002,N'2020-11-24 17:08:12');  ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30010) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30010,9000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.screcordtype.App',N'记录项类型',N'JLXLX',N'JLXLX',1,30010,N'2020-11-02 15:46:32');IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30020) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30020,9000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.screcordtypeitem.App',N'记录项字典',N'JLXZD',N'JLXZD',1,30020,N'2020-11-02 15:46:48'); IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30100,30,0,0,0,0,N'list.PNG',N'#Shell.class.assist.infection.apply.App',N'院感登记',N'YGDJ',N'YGDJ',1,100,N'2020-11-02 15:44:49'); IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30200) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30200,30,0,0,0,0,N'list.PNG',N'#Shell.class.assist.infection.assess.App',N'院感评估',N'YGPG',N'YGPG',1,30200,N'2020-11-02 15:45:19'); IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30300) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30300,30,0,0,0,0,N'list.PNG',N'#Shell.class.assist.statistics.infection.App',N'报表统计(全部科室)',N'YGPG',N'YGPG',1,30300,N'2020-11-02 15:45:19');IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100010) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100010,1000,0,0,0,3,N'package.PNG',N'#Shell.class.sysbase.module.App',N'功能模块',N'XTJCSZ',N'XTJCSZ',1,10,N'2016-03-22 17:54:17'); IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30211) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30211,30,0,0,0,0,N'search.PNG',N'#Shell.class.assist.infection.alertinfo.App',N'院感待处理',N'YGDCL',N'YGDCL',1,30211,N'2020-12-11 16:23:11');";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100020) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100020,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.jurisdiction.modulerole.App',N'模块角色',1,30,N'2020-04-02 16:31:36'); IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100030) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100030,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.role.App',N'角色管理',1,20,N'2020-04-02 16:31:18'); IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100040) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100040,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.jurisdiction.userrole.App',N'员工角色',1,40,N'2020-04-02 16:33:52'); IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100050) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100050,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.jurisdiction.roleuser.App',N'角色员工',1,50,N'2020-04-02 16:34:22'); IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100060) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100060,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.role.ModuleApp',N'角色权限',1,60,N'2020-04-02 16:35:11');  ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30015) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30015,9000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.screcordphrase.App1',N'记录项类型短语',N'JLXLXDY',N'JLXLXDY',1,30015,N'2020-11-03 15:06:50');IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30025) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30025,9000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.screcordphrase.App2',N'记录项短语',N'JLXDY',N'JLXDY',1,30025,N'2020-11-03 15:07:36');IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30030) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30030,9000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.screcorditemlink.App',N'记录项类型关系',N'JLXLXGX',N'JLXLXGX',1,30030,N'2020-11-04 14:27:22');IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] =30040) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30040,9000,0,0,0,0,N'dictionary.PNG',N'#Shell.class.sysbase.screcordphrase.ofdept.App',N'记录项结语-按科室',N'JLXJY-AKS',N'JLXJY-AKS',1,30040,N'2020-11-10 14:21:26'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 31000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,31000,30,0,0,0,3,N'package.PNG',N'登录科室',N'31000',N'DLKS',N'DLKS',1,30010,N'2020-12-14 14:39:45');IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30400) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30400,31000,0,0,0,0,N'list.PNG',N'#Shell.class.assist.statistics.infection.ofcurdept.App',N'报表统计',N'BBTJ',N'BBTJ',1,30400,N'2020-11-25 16:19:15');IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30410) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30410,31000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.screcordphrase.ofcurdept.App',N'记录项结果短语',N'30410',N'JLXJGDY',N'JLXJGDY',1,30410,N'2020-12-15 15:01:06'); ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.5");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.5) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.6
            //院感部分
            if (IsUpdateDataBase(oldVersion, "1.0.0.6"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region GK_SampleRequestForm
                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GK_SampleRequestForm]') AND type in(N'U')) CREATE TABLE [dbo].[GK_SampleRequestForm]( [LabID] [bigint] NULL, [ReqDocId] [bigint] NOT NULL, [ReqDocNo] [varchar](50) NULL, [MonitorType] [bigint] NULL, [DeptId] [bigint] NULL, [DeptCName] [varchar](50) NULL, [SamplerId] [bigint] NULL, [Sampler] [varchar](50) NULL, [SampleDate] [datetime] NULL, [SampleTime] [datetime] NULL, [RecordTypeID] [bigint] NULL, [StatusID] [int] NULL, [CName] [varchar](40) NULL, [BarCode] [varchar](50) NULL, [PrintCount] [int] NULL, [SampleNo] [varchar](20) NULL, [IsAutoReceive] [bit] NULL, [ReceiveFlag] [bit] NULL, [ResultFlag] [bit] NULL, [CreatorID] [bigint] NULL, [CreatorName] [varchar](50) NULL, [TesterId] [bigint] NULL, [TesterName] [varchar](50) NULL, [TestTime] [datetime] NULL, [TestResult] [varchar](500) NULL, [BacteriaTotal] [varchar](500) NULL, [ReceiveId] [bigint] NULL, [ReceiveCName] [varchar](50) NULL, [ReceiveDate] [datetime] NULL, [CheckId] [bigint] NULL, [CheckCName] [varchar](50) NULL, [CheckDate] [datetime] NULL, [EvaluatorId] [bigint] NULL, [Evaluators] [varchar](50) NULL, [EvaluationDate] [datetime] NULL, [EvaluatorFlag] [bit] NULL, [Judgment] [varchar](50) NULL, [Archived] [bit] NULL, [Visible] [bit] NULL, [ObsoleteID] [bigint] NULL, [ObsoleteName] [varchar](50) NULL, [ObsoleteTime] [datetime] NULL, [ObsoleteMemoId] [bigint] NULL, [ObsoleteMemo] [varchar](200) NULL, [Memo] [ntext] NULL, [DispOrder] [int] NULL, [DataAddTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_GK_SampleRequestForm_1] PRIMARY KEY CLUSTERED( [ReqDocId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]; ";
                listSQL.Add(updateSql);

                #endregion

                #region SC_RecordDtl
                updateSql = "  IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SC_RecordDtl]') AND type in(N'U')) CREATE TABLE [dbo].[SC_RecordDtl]( [LabID] [bigint] NULL, [RecordDtlId] [bigint] NOT NULL, [RecordDtlNo] [varchar](50) NULL, [BObjectID] [bigint] NULL, [ContentTypeID] [bigint] NULL, [RecordTypeID] [bigint] NULL, [RecordTypeItemID] [bigint] NULL, [TestItemCode] [varchar](50) NULL, [ItemResult] [varchar](300) NULL, [NumberItemResult] [float] NULL, [Visible] [bit] NULL, [Memo] [ntext] NULL, [DispOrder] [int] NULL, [DataAddTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_SC_RecordDtl] PRIMARY KEY CLUSTERED( [RecordDtlId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                #region FK_GK_SampleRequestForm_SC_RecordType
                updateSql = " IF EXISTS (SELECT * FROM information_schema.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'FK_GK_SampleRequestForm_SC_RecordType') ALTER TABLE [dbo].[GK_SampleRequestForm] DROP CONSTRAINT [FK_GK_SampleRequestForm_SC_RecordType]; ALTER TABLE [dbo].[GK_SampleRequestForm] WITH CHECK ADD CONSTRAINT [FK_GK_SampleRequestForm_SC_RecordType] FOREIGN KEY([RecordTypeID]) REFERENCES [dbo].[SC_RecordType]([RecordTypeID]); ALTER TABLE [dbo].[GK_SampleRequestForm] CHECK CONSTRAINT [FK_GK_SampleRequestForm_SC_RecordType];  ";
                listSQL.Add(updateSql);
                #endregion

                #region SC_RecordPhrase
                updateSql = " IF COL_LENGTH('SC_RecordPhrase', 'PhraseType') IS NULL ALTER TABLE SC_RecordPhrase ADD PhraseType bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('SC_RecordPhrase', 'TypeObjectId') IS NULL ALTER TABLE SC_RecordPhrase ADD TypeObjectId bigint; ";
                listSQL.Add(updateSql);

                #endregion

                #region GK_DeptAutoCheckLink
                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GK_DeptAutoCheckLink]') AND type in(N'U')) CREATE TABLE [dbo].[GK_DeptAutoCheckLink]( [LabID] [bigint] NULL, [LinkId] [bigint] NOT NULL, [DeptNo] [bigint] NULL, [IsUse] [bit] NULL, [DispOrder] [int] NULL, [DataAddTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_GK_DEPTAUTOCHECKLINK] PRIMARY KEY CLUSTERED( [LinkId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.6");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.6) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.7
            //院感部分
            if (IsUpdateDataBase(oldVersion, "1.0.0.7"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Department
                updateSql = " IF COL_LENGTH('Department', 'MonitorType') IS NULL ALTER TABLE Department ADD MonitorType bigint;";
                listSQL.Add(updateSql);
                #endregion

                #region AntiGroup
                updateSql = " IF COL_LENGTH('AntiGroup', 'LabID') IS NULL ALTER TABLE AntiGroup ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('AntiGroup', 'DataAddTime') IS NULL ALTER TABLE AntiGroup ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('AntiGroup', 'DataTimeStamp') IS NULL ALTER TABLE AntiGroup ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);
                #endregion

                #region AntiGroup
                updateSql = " IF COL_LENGTH('ChargeType', 'LabID') IS NULL ALTER TABLE ChargeType ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('ChargeType', 'DataAddTime') IS NULL ALTER TABLE ChargeType ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('ChargeType', 'DataTimeStamp') IS NULL ALTER TABLE ChargeType ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);
                #endregion

                #region SamplingGroup
                updateSql = " IF COL_LENGTH('SamplingGroup', 'LabID') IS NULL ALTER TABLE SamplingGroup ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('SamplingGroup', 'DataAddTime') IS NULL ALTER TABLE SamplingGroup ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('SamplingGroup', 'DataTimeStamp') IS NULL ALTER TABLE SamplingGroup ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);
                #endregion

                #region Samplingitem
                updateSql = " IF COL_LENGTH('Samplingitem', 'LabID') IS NULL ALTER TABLE Samplingitem ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Samplingitem', 'DataAddTime') IS NULL ALTER TABLE Samplingitem ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Samplingitem', 'DataTimeStamp') IS NULL ALTER TABLE Samplingitem ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);
                #endregion

                #region SectionItem
                updateSql = " IF COL_LENGTH('SectionItem', 'LabID') IS NULL ALTER TABLE SectionItem ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('SectionItem', 'DataAddTime') IS NULL ALTER TABLE SectionItem ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('SectionItem', 'DataTimeStamp') IS NULL ALTER TABLE SectionItem ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);
                #endregion

                #region StatusType
                updateSql = " IF COL_LENGTH('StatusType', 'LabID') IS NULL ALTER TABLE StatusType ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('StatusType', 'DataAddTime') IS NULL ALTER TABLE StatusType ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('StatusType', 'DataTimeStamp') IS NULL ALTER TABLE StatusType ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);
                #endregion

                #region PUser
                updateSql = " IF COL_LENGTH('PUser', 'LabID') IS NULL ALTER TABLE PUser ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('PUser', 'DataAddTime') IS NULL ALTER TABLE PUser ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('PUser', 'DataTimeStamp') IS NULL ALTER TABLE PUser ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_Module
                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 30400) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,30400,30,0,0,0,0,N'list.PNG',N'#Shell.class.assist.statistics.infection.ofcurdept.App',N'报表统计(登录科室)',N'YGPG',N'YGPG',1,30400,N'2020-11-02 15:45:19');";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_Role
                updateSql = "   IF COL_LENGTH('RBAC_Role', 'SySType') IS NULL ALTER TABLE RBAC_Role ADD SySType bigint;  ";
                listSQL.Add(updateSql);

                #endregion

                #region SickType
                updateSql = "   IF NOT EXISTS(SELECT * FROM [SickType] WHERE [SickTypeNo] = 8) INSERT [SickType] ([SickTypeNo],[CName],[ShortCode],[DispOrder],[ContractCode]) VALUES ( 8,N'院感申请',N'8',8,N'Code_4'); ";
                listSQL.Add(updateSql);
                #endregion

                #region SampleType
                updateSql = " IF COL_LENGTH('SampleType', 'LabID') IS NULL ALTER TABLE SampleType ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('SampleType', 'DataAddTime') IS NULL ALTER TABLE SampleType ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('SampleType', 'DataTimeStamp') IS NULL ALTER TABLE SampleType ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);
                #endregion

                #region TestItem
                updateSql = " IF COL_LENGTH('TestItem', 'LabID') IS NULL ALTER TABLE TestItem ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('TestItem', 'DataAddTime') IS NULL ALTER TABLE TestItem ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('TestItem', 'DataTimeStamp') IS NULL ALTER TABLE TestItem ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);
                #endregion

                #region PGroup
                updateSql = " IF COL_LENGTH('PGroup', 'LabID') IS NULL ALTER TABLE PGroup ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('PGroup', 'DataAddTime') IS NULL ALTER TABLE PGroup ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('PGroup', 'DataTimeStamp') IS NULL ALTER TABLE PGroup ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.7");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.7) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.8
            //院感部分
            if (IsUpdateDataBase(oldVersion, "1.0.0.8"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region GK_SampleRequestForm
                updateSql = "  IF COL_LENGTH('GK_SampleRequestForm', 'ReceiveId') IS NULL ALTER TABLE GK_SampleRequestForm ADD ReceiveId bigint; ";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('GK_SampleRequestForm', 'ReceiveCName') IS NULL ALTER TABLE GK_SampleRequestForm ADD ReceiveCName varchar(50); ";
                listSQL.Add(updateSql);

                #endregion

                #region SC_RecordDtl
                updateSql = "  IF COL_LENGTH('SC_RecordDtl', 'TestItemCode') IS NULL ALTER TABLE SC_RecordDtl ADD TestItemCode varchar(50); ";
                listSQL.Add(updateSql);
                #endregion

                #region FK_GK_SampleRequestForm_SC_RecordType
                updateSql = " IF EXISTS (SELECT * FROM information_schema.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'FK_GK_SampleRequestForm_SC_RecordType') ALTER TABLE [dbo].[GK_SampleRequestForm] DROP CONSTRAINT [FK_GK_SampleRequestForm_SC_RecordType]; ALTER TABLE [dbo].[GK_SampleRequestForm] WITH CHECK ADD CONSTRAINT [FK_GK_SampleRequestForm_SC_RecordType] FOREIGN KEY([RecordTypeID]) REFERENCES [dbo].[SC_RecordType]([RecordTypeID]); ALTER TABLE [dbo].[GK_SampleRequestForm] CHECK CONSTRAINT [FK_GK_SampleRequestForm_SC_RecordType];  ";
                listSQL.Add(updateSql);

                #endregion

                #region SC_RecordTypeItem
                //检验项目对照码为空
                updateSql = " update SC_RecordTypeItem set ItemCode=NULL where RecordTypeItemID=120010; ";
                listSQL.Add(updateSql);

                #endregion

                #region SC_RecordItemLink
                //检验项目对照码为空
                updateSql = " update SC_RecordItemLink set TestItemCode=NULL where RecordTypeID=15 and RecordTypeItemID=120010; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.8");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.8) Update Error, Please Check The Log!");
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
                    ZhiFang.Common.Log.Log.Info("Update CompareVersion Error1：" + ex.Message);
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

            }
            else
            {

            }
        }

        private static bool ExecuteUpdateSQL(string sql)
        {
            return SqlServerHelper.ExecuteSqlBool(sql, ADOConnectStr);
        }

        private static bool ExecuteUpdateSQL(List<string> listSQL)
        {
            return SqlServerHelper.ExecuteSqlList(listSQL, ADOConnectStr);
        }
    }
}
