using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;


namespace ZhiFang.DBUpdate
{
    public class DBUpdate
    {
        public static string ADOConnectStr = GetADODataBaseSettings(ZhiFang.Common.Public.ConfigHelper.GetDataBaseSettings("databaseSettings", "db.connectionString"));

        static Dictionary<string, string> DicVersion = GetVersionComparison();//

        static string MainAssemblyFile = "ZhiFang.LabStar.TechnicianStation.dll";//可以从配置文件获取

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
                updateSql = " update B_Para set ParaValue=\'" + oldVersion + "\'" + " where ParaType=\'SYS\' and ParaNo=\'SYS_DBVersion\'";
                result = ExecuteUpdateSQL(updateSql);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.1");
                else
                    ZhiFang.LabStar.Common.LogHelp.Error("Update Error 1.0.0.1");

            }
            #endregion

            #region 1.0.0.2
            if (IsUpdateDataBase(oldVersion, "1.0.0.2"))
            {
                List<string> listSQL = new List<string>();
                #region 模块角色小组设置
                updateSql = "CREATE TABLE [dbo].[RBAC_EmpModuleRoleSectionSet](	[LabID] [bigint] NULL,	[EMRSSID] [bigint] NOT NULL,	[EmpID] [bigint] NULL,	[EmpName] [varchar](50) NULL,	[ModuleID] [bigint] NOT NULL,	[ModuleCode] [varchar](50) NULL,	[ModuleName] [varchar](50) NULL,	[RoleID] [bigint] NULL,	[RoleCode] [varchar](50) NULL,	[RoleName] [varchar](50) NULL,	[SectionID] [bigint] NULL,	[SectionCode] [varchar](50) NULL,	[SectionName] [varchar](50) NULL,	[Comment] [ntext] NULL,	[IsUse] [bit] NULL,	[DispOrder] [int] NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_RBAC_EMPMODULEROLESECTIONSE] PRIMARY KEY CLUSTERED (	[EMRSSID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];SET ANSI_PADDING OFF;ALTER TABLE [dbo].[RBAC_EmpModuleRoleSectionSet]  WITH CHECK ADD  CONSTRAINT [FK_RBAC_EMP_REFERENCE_LB_SECTI] FOREIGN KEY([SectionID])REFERENCES [dbo].[LB_Section] ([SectionID]);ALTER TABLE [dbo].[RBAC_EmpModuleRoleSectionSet] CHECK CONSTRAINT [FK_RBAC_EMP_REFERENCE_LB_SECTI];";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.2");
                else
                    ZhiFang.LabStar.Common.LogHelp.Error("Update Error 1.0.0.2");
            }
            #endregion

            #region 1.0.0.3
            if (IsUpdateDataBase(oldVersion, "1.0.0.3"))
            {
                List<string> listSQL = new List<string>();
                #region LB_SampleItem表增加LabID，LB_SamplingItem表修改MustItem字段为 IsMustItem

                updateSql = "IF COL_LENGTH('LB_SampleItem', 'LabID') IS NULL ALTER TABLE LB_SampleItem ADD LabID  bigint; ";
                listSQL.Add(updateSql);
                updateSql = "IF COL_LENGTH('LB_SamplingItem', 'MustItem') IS not  NULL exec sp_rename 'LB_SamplingItem.MustItem','IsMustItem','column';";
                listSQL.Add(updateSql);

                #endregion
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.3");
                else
                    ZhiFang.LabStar.Common.LogHelp.Error("Update Error 1.0.0.3");
            }
            #endregion

            #region 1.0.0.4 通讯日志文件表增加通讯文件名字段
            if (IsUpdateDataBase(oldVersion, "1.0.0.4"))
            {
                List<string> listSQL = new List<string>();

                updateSql = "IF COL_LENGTH('Lis_EquipComFile', 'ComFileName') IS NULL ALTER TABLE Lis_EquipComFile ADD ComFileName nvarchar(500) ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.4");
                else
                    ZhiFang.LabStar.Common.LogHelp.Error("Update Error 1.0.0.4");
            }
            #endregion

            #region 1.0.0.5 LB_ItemCodeLink表DicDataID字段数据类型由nvarchar改为bigint
            if (IsUpdateDataBase(oldVersion, "1.0.0.5"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " if Exists(Select * from SysColumns where [Name]= \'DicDataID\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'LB_ItemCodeLink\' ))" + "\r" +
                            " alter table LB_ItemCodeLink alter column DicDataID bigint ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.5");
                else
                    ZhiFang.LabStar.Common.LogHelp.Error("Update Error 1.0.0.5");
            }
            #endregion
            
            #region 1.0.0.6 新增表单列表配置
            if (IsUpdateDataBase(oldVersion, "1.0.0.6"))
            {
                List<string> listSQL = new List<string>();
                // 表单集合 表
                updateSql = "  create table B_Module_FormList (    LabID                bigint               null,    LabNo                varchar(50)          collate Chinese_PRC_CI_AS null,    FormID               bigint               not null,    FormCode             varchar(50)          null,    TypeID               bigint               null,    TypeName             varchar(50)          null,    ClassID              bigint               null,    ClassName            varchar(50)          null,    CName                varchar(50)          collate Chinese_PRC_CI_AS null,    ShortName            varchar(10)          collate Chinese_PRC_CI_AS null,    ShortCode            varchar(10)          collate Chinese_PRC_CI_AS null,    StandCode            varchar(50)          collate Chinese_PRC_CI_AS null,    ZFStandCode          varchar(50)          collate Chinese_PRC_CI_AS null,    PinYinZiTou          nvarchar(50)         collate Chinese_PRC_CI_AS null,    DispOrder            int                  null,    IsUse                bit                  null,    DataAddTime          datetime             null,    DataUpdateTime       datetime             null,    DataTimeStamp        timestamp            null,    SourceCodeUrl        nvarchar(500)        null,    SourceCode           ntext                null,    Memo                 ntext                null,    constraint PK_B_MODULE_FORMLIST primary key (FormID) );";
                if (!CheckDataObjectIsExists("[dbo].[B_Module_FormList]", "U"))
                {
                    listSQL.Add(updateSql);
                }
                // 列表集合 表
                updateSql = " create table B_Module_GridList (    LabID                bigint               null,    LabNo                varchar(50)          collate Chinese_PRC_CI_AS null,    GridID               bigint               not null,    GridCode             varchar(50)          null,    TypeID               bigint               null,    TypeName             varchar(50)          null,    ClassID              bigint               null,    ClassName            varchar(50)          null,    CName                varchar(50)          collate Chinese_PRC_CI_AS null,    ShortName            varchar(10)          collate Chinese_PRC_CI_AS null,    ShortCode            varchar(10)          collate Chinese_PRC_CI_AS null,    StandCode            varchar(50)          collate Chinese_PRC_CI_AS null,    ZFStandCode          varchar(50)          collate Chinese_PRC_CI_AS null,    PinYinZiTou          nvarchar(50)         collate Chinese_PRC_CI_AS null,    DispOrder            int                  null,    IsUse                bit                  null,    DataAddTime          datetime             null,    DataUpdateTime       datetime             null,    DataTimeStamp        timestamp            null,    SourceCodeUrl        nvarchar(500)        null,    SourceCode           ntext                null,    Memo                 ntext                null,    constraint PK_B_MODULE_GRIDLIST primary key (GridID) );";
                if (!CheckDataObjectIsExists("[dbo].[B_Module_GridList]", "U"))
                {
                    listSQL.Add(updateSql);
                }
                // 模块表单列表关系 表
                updateSql = "create table B_Module_ModuleFormGridLink(LabID                bigint               null, LabNo                varchar(50)          collate Chinese_PRC_CI_AS null, ModuleFormGridLinkID bigint               not null, FormID               bigint               null, GridID               bigint               null, ModuleID             bigint           null, ChartID              bigint               null, FormCode             varchar(50)          null, GridCode             varchar(50)          null, ChartCode            varchar(50)          null, CName                varchar(100)         null, TypeID               bigint               null, TypeName             varchar(50)          null, DispOrder            int                  null, IsUse                bit                  null, DataAddTime          datetime             null, DataUpdateTime       datetime             null, DataTimeStamp        timestamp            null, ShortName            varchar(20)          null, StandCode            varchar(20)          null, constraint PK_B_MODULE_MODULEFORMGRIDLINK primary key(ModuleFormGridLinkID));";
                if (!CheckDataObjectIsExists("[dbo].[B_Module_ModuleFormGridLink]", "U"))
                {
                    listSQL.Add(updateSql);
                }
                // 表单控件集合 表
                updateSql = "create table B_Module_FormControlList(LabID                bigint               null, LabNo                varchar(50)          collate Chinese_PRC_CI_AS null, FormControlID        bigint               not null, FormID               bigint               null, FormCode             varchar(50)          null, DefaultValue         varchar(500)         null, MapField             varchar(50)          null, TextField            varchar(50)          null, ValueField           varchar(50)          null, TypeID               bigint               null, TypeName             varchar(50)          null, ClassName            varchar(50)          null, StyleContent         varchar(1000)        null, Label                varchar(50)          null, CallBackFunc         ntext                null, Cols                 bigint               null, URL                  varchar(1000)        null, DataJSON             ntext                null, IsHasNull            bit                  null, CName                varchar(50)          collate Chinese_PRC_CI_AS null, ShortName            varchar(10)          collate Chinese_PRC_CI_AS null, ShortCode            varchar(10)          collate Chinese_PRC_CI_AS null, StandCode            varchar(50)          collate Chinese_PRC_CI_AS null, ZFStandCode          varchar(50)          collate Chinese_PRC_CI_AS null, PinYinZiTou          nvarchar(50)         collate Chinese_PRC_CI_AS null, DispOrder            int                  null, IsUse                bit                  null, DataAddTime          datetime             null, DataUpdateTime       datetime             null, DataTimeStamp        timestamp            null, constraint PK_B_MODULE_FORMCONTROLLIST primary key(FormControlID));";
                if (!CheckDataObjectIsExists("[dbo].[B_Module_FormControlList]", "U"))
                {
                    listSQL.Add(updateSql);
                }
                // 列表控件集合 表
                updateSql = "create table B_Module_GridControlList(LabID                bigint               null, LabNo                varchar(50)          collate Chinese_PRC_CI_AS null, GridControlID        bigint               not null, GridID               bigint               null, GridCode             varchar(50)          null, MapField             varchar(50)          null, TextField            varchar(50)          null, ValueField           varchar(50)          null, TypeID               bigint               null, TypeName             varchar(50)          null, ClassName            varchar(50)          null, StyleContent         varchar(1000)        null, ColName              varchar(50)          null, OrderType            varchar(50)          null, IsOrder              bit                  null, ColData              ntext                null, URL                  varchar(1000)        null, CName                varchar(50)          collate Chinese_PRC_CI_AS null, ShortName            varchar(10)          collate Chinese_PRC_CI_AS null, ShortCode            varchar(10)          collate Chinese_PRC_CI_AS null, StandCode            varchar(50)          collate Chinese_PRC_CI_AS null, ZFStandCode          varchar(50)          collate Chinese_PRC_CI_AS null, PinYinZiTou          nvarchar(50)         collate Chinese_PRC_CI_AS null, DispOrder            int                  null, IsUse                bit                  null, DataAddTime          datetime             null, DataUpdateTime       datetime             null, DataTimeStamp        timestamp            null, IsHide               bit                  null, Width                varchar(20)          null, MinWidth             varchar(20)          null, Edit                 varchar(50)          null, Toolbar              varchar(500)         null, Align                varchar(50)          null, Fixed                varchar(50)          null, SourceCode           ntext                null, constraint PK_B_MODULE_GRIDCONTROLLIST primary key(GridControlID));";
                if (!CheckDataObjectIsExists("[dbo].[B_Module_GridControlList]", "U"))
                {
                    listSQL.Add(updateSql);
                }
                // 模块表单配置 表
                updateSql = "create table B_Module_FormControlSet(LabID                bigint               null, LabNo                varchar(50)          collate Chinese_PRC_CI_AS null, FormControlSetID     bigint               not null, FormControlID        bigint               null, QFuncID              bigint               null, FormCode             varchar(50)          null, IsReadOnly           bit                  null, IsDisplay            bit                  null, DefaultValue         varchar(500)         null, Label                varchar(50)          null, URL                  varchar(1000)        null, DataJSON             ntext                null, IsHasNull            bit                  null, CName                varchar(50)          collate Chinese_PRC_CI_AS null, ShortName            varchar(10)          collate Chinese_PRC_CI_AS null, ShortCode            varchar(10)          collate Chinese_PRC_CI_AS null, StandCode            varchar(50)          collate Chinese_PRC_CI_AS null, ZFStandCode          varchar(50)          collate Chinese_PRC_CI_AS null, PinYinZiTou          nvarchar(50)         collate Chinese_PRC_CI_AS null, DispOrder            int                  null, IsUse                bit                  null, DataAddTime          datetime             null, DataUpdateTime       datetime             null, DataTimeStamp        timestamp            null, constraint PK_B_MODULE_FORMCONTROLSET primary key(FormControlSetID));";
                if (!CheckDataObjectIsExists("[dbo].[B_Module_FormControlSet]", "U"))
                {
                    listSQL.Add(updateSql);
                }
                // 列表控件设置 表
                updateSql = " create table B_Module_GridControlSet (    LabID                bigint               null,    LabNo                varchar(50)          collate Chinese_PRC_CI_AS null,    GridControSetlID     bigint               not null,    GridControlID        bigint               null,    QFuncID              bigint               null,    GridCode             varchar(50)          null,    MapField             varchar(50)          null,    TextField            varchar(50)          null,    ValueField           varchar(50)          null,    TypeID               bigint               null,    TypeName             varchar(50)          null,    ClassName            varchar(50)          null,    StyleContent         varchar(1000)        null,    ColName              varchar(50)          null,    IsOrder              bit                  null,    OrderType            varchar(50)          null,    ColData              ntext                null,    URL                  varchar(1000)        null,    CName                varchar(50)          collate Chinese_PRC_CI_AS null,    ShortName            varchar(10)          collate Chinese_PRC_CI_AS null,    ShortCode            varchar(10)          collate Chinese_PRC_CI_AS null,    StandCode            varchar(50)          collate Chinese_PRC_CI_AS null,    ZFStandCode          varchar(50)          collate Chinese_PRC_CI_AS null,    PinYinZiTou          nvarchar(50)         collate Chinese_PRC_CI_AS null,    DispOrder            int                  null,    IsUse                bit                  null,    DataAddTime          datetime             null,    DataUpdateTime       datetime             null,    DataTimeStamp        timestamp            null,    IsHide               bit                  null,    Width                varchar(20)          null,    constraint PK_B_MODULE_GRIDCONTROLSET primary key (GridControSetlID) );";
                if (!CheckDataObjectIsExists("[dbo].[B_Module_GridControlSet]", "U"))
                {
                    listSQL.Add(updateSql);
                }

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.6");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.6) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.7 表单控件集合表增加字段
            if (IsUpdateDataBase(oldVersion, "1.0.0.7"))
            {
                List<string> listSQL = new List<string>();
                //表单控件集合 表 是否只读 字段
                updateSql = "IF COL_LENGTH('B_Module_FormControlList', 'IsReadOnly') IS NULL ALTER TABLE B_Module_FormControlList ADD IsReadOnly  bit; ";
                listSQL.Add(updateSql);
                //表单控件集合 表 是否显示 字段
                updateSql = "IF COL_LENGTH('B_Module_FormControlList', 'IsDisplay') IS NULL ALTER TABLE B_Module_FormControlList ADD IsDisplay  bit; ";
                listSQL.Add(updateSql);
                //如果存在 模块表单列表关系 表 则删除
                if (CheckDataObjectIsExists("[dbo].[B_Module_ModuleFormGridLink]", "U"))
                {
                    updateSql = "drop table B_Module_ModuleFormGridLink; ";
                    listSQL.Add(updateSql);
                }
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.7");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.7) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.8 表单列表配置列表编码添加唯一索引
            if (IsUpdateDataBase(oldVersion, "1.0.0.8"))
            {
                List<string> listSQL = new List<string>();

                //表单集合 表 加 表单编码唯一索引
                updateSql = "IF not EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('B_Module_FormList', N'U') and NAME='IX_B_Module_FormList') BEGIN	CREATE UNIQUE NONCLUSTERED INDEX [IX_B_Module_FormList] ON [dbo].[B_Module_FormList] (	[FormCode] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] END; ";
                listSQL.Add(updateSql);
                //列表集合 表 加 列表编码唯一索引
                updateSql = "IF not EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('B_Module_GridList', N'U') and NAME='IX_B_Module_GridList') BEGIN	CREATE UNIQUE NONCLUSTERED INDEX [IX_B_Module_GridList] ON [dbo].[B_Module_GridList] (	[GridCode] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] END; ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.8");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.8) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.9 检验小组表增加小组类型ID和小组类型名称字段
            if (IsUpdateDataBase(oldVersion, "1.0.0.9"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " if Not Exists(Select * from SysColumns where [Name]= \'SectionType\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'LB_Section\' ))" + "\r" +
                            " begin " + "\r" +
                            "   Alter Table LB_Section Add SectionType nvarchar(100) " + "\r" +
                            "   EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'小组类型名称\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                            "   @level1type = N\'TABLE\',@level1name = N\'LB_Section\', @level2type = N\'COLUMN\',@level2name = N\'SectionType\'" + "\r" +
                            " end ";
                listSQL.Add(updateSql);


                updateSql = " if Not Exists(Select * from SysColumns where [Name]= \'SectionTypeID\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'LB_Section\' ))" + "\r" +
                            " begin " + "\r" +
                            "   Alter Table LB_Section Add SectionTypeID bigint " + "\r" +
                            "   EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'小组类型ID\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                            "   @level1type = N\'TABLE\',@level1name = N\'LB_Section\', @level2type = N\'COLUMN\',@level2name = N\'SectionTypeID\'" + "\r" +
                            " end ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.9");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.9) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.10 修改采样组说明字段类型长度
            if (IsUpdateDataBase(oldVersion, "1.0.0.10"))
            {
                List<string> listSQL = new List<string>();

                // 修改采样组说明字段类型长度
                updateSql = " IF COL_LENGTH('LB_SamplingGroup', 'Synopsis') IS not  NULL alter table LB_SamplingGroup alter column Synopsis nvarchar(max) ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.10");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.10) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.11 修改采样组说明字段类型长度
            if (IsUpdateDataBase(oldVersion, "1.0.0.11"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " if not exists (select * from dbo.sysobjects where id = object_id(N\'[dbo].[LB_ExpertRule]\') and OBJECTPROPERTY(id, N\'IsUserTable\') = 1) BEGIN " + "\r" +
                            " CREATE TABLE [dbo].[LB_ExpertRule]( [LabID] [bigint] NOT NULL, [ExpertRuleID] [bigint] NOT NULL, [CName] [nvarchar](400) NULL, [GName] [nvarchar](200) NULL, [ResultType] [int] NULL, [RuleRelation] [int] NULL, [SectionID] [bigint] NOT NULL, [ItemID] [bigint] NULL, [ItemAlarmInfo] [nvarchar](50) NULL, [AlarmLevel] [int] NULL, [AlarmInfo] [nvarchar](50) NULL, [ConditionName] [nvarchar](200) NULL, [GenderID] [bigint] NULL, [EquipID] [bigint] NULL, [DeptID] [bigint] NULL, [SampleTypeID] [bigint] NULL, [LowAge] [float] NULL, [HighAge] [float] NULL, [AgeUnitID] [bigint] NULL, [bCollectTime] [datetime] NULL, [eCollectTime] [datetime] NULL, [DiagID] [bigint] NULL, [PhyPeriodID] [bigint] NULL, [CollectPartID] [bigint] NULL, [Comment] [ntext] NULL, [IsUse] [bit] NULL, [DispOrder] [int] NULL, [DataAddTime] [datetime] NULL, [DataUpdateTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_BT_ExpertRule] PRIMARY KEY CLUSTERED ([ExpertRuleID] ASC) ON [PRIMARY] ) ON [PRIMARY] " + "\r" +
                            " end";
                listSQL.Add(updateSql);

                updateSql = " if not exists (select * from dbo.sysobjects where id = object_id(N\'[dbo].[LB_ExpertRuleList]\') and OBJECTPROPERTY(id, N\'IsUserTable\') = 1) BEGIN " + "\r" +
                            " CREATE TABLE [dbo].[LB_ExpertRuleList]( [LabID] [bigint] NOT NULL, [ExpertRuleID] [bigint] NOT NULL, [ExpertRuleListID] [bigint] NOT NULL, [RuleName] [nvarchar](200) NULL, [SysRuleName] [nvarchar](200) NULL, [ItemID] [bigint] NULL, [ItemName] [varchar](40) NULL, [ValueFieldType] [int] NULL, [bRelatedItemValue] [bit] NULL, [bHisItemValue] [bit] NULL, [bCalcItemValue] [bit] NULL, [ValueType] [int] NULL, [CompType] [int] NULL, [CompValue] [nvarchar](100) NULL, [CompFValue] [float] NULL, [CompFValue2] [float] NULL, [bValue] [bit] NULL, [bLastHisComp] [bit] NULL, [bLimitHisDate] [bit] NULL, [LimitHisDate] [int] NULL, [CalcFormula] [nvarchar](300) NULL, [DispOrder] [int] NULL, [DataAddTime] [datetime] NULL, [DataUpdateTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_BT_ExpertRuleList] PRIMARY KEY CLUSTERED ([ExpertRuleListID] ASC) ON [PRIMARY] ) ON [PRIMARY] " + "\r" +
                            " ALTER TABLE[dbo].[LB_ExpertRuleList]  WITH CHECK ADD CONSTRAINT[FK_LB_ExpertRuleList_Rule] FOREIGN KEY([ExpertRuleID]) REFERENCES[dbo].[LB_ExpertRule]([ExpertRuleID]) " + "\r" +
                            " ALTER TABLE[dbo].[LB_ExpertRuleList] CHECK CONSTRAINT[FK_LB_ExpertRuleList_Rule] " + "\r" +
                            " end";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.11");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.11) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.12 LB_ItemExp表TemplateInfo字段数据类型由image改为nvarchar
            if (IsUpdateDataBase(oldVersion, "1.0.0.12"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " if Exists(Select * from SysColumns where [xtype]=34 and [Name]= \'TemplateInfo\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'LB_ItemExp\' ))" + "\r" +
                            " begin " + "\r" +
                            " alter table LB_ItemExp drop column TemplateInfo " + "\r" +
                            " IF COL_LENGTH('LB_ItemExp', 'TemplateInfo') IS NULL ALTER TABLE LB_ItemExp ADD TemplateInfo nvarchar(max) " + "\r" +
                            " end";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.12");
                else
                    ZhiFang.LabStar.Common.LogHelp.Error("Update Error 1.0.0.12");
            }
            #endregion

            #region 1.0.0.13 表单列表配置Code字段长度修改
            if (IsUpdateDataBase(oldVersion, "1.0.0.13"))
            {
                List<string> listSQL = new List<string>();

                //表单集合 表 加 删除表单编码唯一索引
                updateSql = "IF  EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('B_Module_FormList', N'U') and NAME='IX_B_Module_FormList') BEGIN drop index IX_B_Module_FormList on B_Module_FormList END; ";
                listSQL.Add(updateSql);
                //列表集合 表 加 删除列表编码唯一索引
                updateSql = "IF  EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('B_Module_GridList', N'U') and NAME='IX_B_Module_GridList') BEGIN drop index IX_B_Module_GridList on B_Module_GridList END; ";
                listSQL.Add(updateSql);
                //表单控件集合 表 编码长度
                updateSql = "alter table B_Module_FormControlList alter column FormCode nvarchar(200) ";
                listSQL.Add(updateSql);
                //模块表单配置 表 编码长度
                updateSql = "alter table B_Module_FormControlSet alter column FormCode nvarchar(200) ";
                listSQL.Add(updateSql);
                //表单集合 表 编码长度
                updateSql = "alter table B_Module_FormList alter column FormCode nvarchar(200) ";
                listSQL.Add(updateSql);
                //列表控件集合 表 编码长度
                updateSql = "alter table B_Module_GridControlList alter column GridCode nvarchar(200) ";
                listSQL.Add(updateSql);
                //列表控件设置 表 编码长度
                updateSql = "alter table B_Module_GridControlSet alter column GridCode nvarchar(200) ";
                listSQL.Add(updateSql);
                //列表集合 表 编码长度
                updateSql = "alter table B_Module_GridList alter column GridCode nvarchar(200) ";
                listSQL.Add(updateSql);
                //表单集合 表 加 表单编码唯一索引
                updateSql = "IF not EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('B_Module_FormList', N'U') and NAME='IX_B_Module_FormList') BEGIN	CREATE UNIQUE NONCLUSTERED INDEX [IX_B_Module_FormList] ON [dbo].[B_Module_FormList] (	[FormCode] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] END; ";
                listSQL.Add(updateSql);
                //列表集合 表 加 列表编码唯一索引
                updateSql = "IF not EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('B_Module_GridList', N'U') and NAME='IX_B_Module_GridList') BEGIN	CREATE UNIQUE NONCLUSTERED INDEX [IX_B_Module_GridList] ON [dbo].[B_Module_GridList] (	[GridCode] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] END; ";
                listSQL.Add(updateSql);
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.13");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.13) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.14 表单列表配置表增加唯一索引
            if (IsUpdateDataBase(oldVersion, "1.0.0.14"))
            {
                List<string> listSQL = new List<string>();

                //表单集合 表 加 删除表单编码唯一索引
                updateSql = "IF  EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('B_Module_FormList', N'U') and NAME='IX_B_Module_FormList') BEGIN drop index IX_B_Module_FormList on B_Module_FormList END; ";
                listSQL.Add(updateSql);
                //列表集合 表 加 删除列表编码唯一索引
                updateSql = "IF  EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('B_Module_GridList', N'U') and NAME='IX_B_Module_GridList') BEGIN drop index IX_B_Module_GridList on B_Module_GridList END; ";
                listSQL.Add(updateSql);
                //表单集合 表 加 表单编码和labid 唯一索引
                updateSql = "IF not EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('B_Module_FormList', N'U') and NAME='IX_B_Module_FormList') BEGIN	CREATE UNIQUE NONCLUSTERED INDEX [IX_B_Module_FormList] ON [dbo].[B_Module_FormList] (	[FormCode] ASC ,[LabID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] END; ";
                listSQL.Add(updateSql);
                //列表集合 表 加 列表编码和labid 唯一索引
                updateSql = "IF not EXISTS (SELECT 1 FROM sys.indexes WHERE object_id=OBJECT_ID('B_Module_GridList', N'U') and NAME='IX_B_Module_GridList') BEGIN	CREATE UNIQUE NONCLUSTERED INDEX [IX_B_Module_GridList] ON [dbo].[B_Module_GridList] (	[GridCode] ASC ,[LabID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] END; ";
                listSQL.Add(updateSql);
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.14");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.14) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.15 Lis_TestItem表ReportInfo字段数据类型由image改为nvarchar并新增字段ReportInfoPrint
            if (IsUpdateDataBase(oldVersion, "1.0.0.15"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " if Exists(Select * from SysColumns where [xtype]=34 and [Name]= \'ReportInfo\'" + "\r" +
                            " and ID = (Select [ID] from SysObjects where Name = \'Lis_TestItem\' ))" + "\r" +
                            " begin " + "\r" +
                            "   ALTER TABLE Lis_TestItem drop column ReportInfo " + "\r" +
                            "   IF COL_LENGTH('Lis_TestItem', 'ReportInfo') IS NULL " + "\r" +
                            "   begin " + "\r" +
                            "     ALTER TABLE Lis_TestItem ADD ReportInfo nvarchar(max) " + "\r" +
                            "     EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'特殊报告值\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                            "     @level1type = N\'TABLE\',@level1name = N\'Lis_TestItem\', @level2type = N\'COLUMN\',@level2name = N\'ReportInfo\'" + "\r" +
                            "   end " + "\r" +
                            " end";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Lis_TestItem', 'ReportInfoPrint') IS NULL " +
                            " begin " + "\r" +
                            "   ALTER TABLE Lis_TestItem ADD ReportInfoPrint nvarchar(max) " + "\r" +
                            "   EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'特殊报告值打印格式\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                            "   @level1type = N\'TABLE\',@level1name = N\'Lis_TestItem\', @level2type = N\'COLUMN\',@level2name = N\'ReportInfoPrint\'" + "\r" +
                            " end ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.15");
                else
                    ZhiFang.LabStar.Common.LogHelp.Error("Update Error 1.0.0.15");
            }
            #endregion

            #region 1.0.0.16 LB_ExpertRule表增加检验小组和项目外键
            if (IsUpdateDataBase(oldVersion, "1.0.0.16"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " ALTER TABLE [dbo].[LB_ExpertRule]  WITH CHECK ADD  CONSTRAINT [FK_LB_ExpertRule_ItemID] FOREIGN KEY(ItemID) REFERENCES[dbo].[LB_Item](ItemID)";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE [dbo].[LB_ExpertRule] CHECK CONSTRAINT [FK_LB_ExpertRule_ItemID]";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE [dbo].[LB_ExpertRule]  WITH CHECK ADD  CONSTRAINT [FK_LB_ExpertRule_SectionID] FOREIGN KEY([SectionID]) REFERENCES[dbo].[LB_Section]([SectionID])";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE [dbo].[LB_ExpertRule] CHECK CONSTRAINT [FK_LB_ExpertRule_SectionID]";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.16");
                else
                    ZhiFang.LabStar.Common.LogHelp.Error("Update Error 1.0.0.16");
            }
            #endregion

            #region 1.0.0.17 Lis_TestForm表增加迁移标记字段
            if (IsUpdateDataBase(oldVersion, "1.0.0.17"))
            {
                List<string> listSQL = new List<string>();

                updateSql = " IF COL_LENGTH('Lis_TestForm', 'MigrationFlag') IS NULL " +
                            " begin " + "\r" +
                            "   ALTER TABLE Lis_TestForm ADD MigrationFlag int default 0 " + "\r" +
                            "   EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'迁移标记\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                            "   @level1type = N\'TABLE\',@level1name = N\'Lis_TestForm\', @level2type = N\'COLUMN\',@level2name = N\'MigrationFlag\'" + "\r" +
                            " end ";
                listSQL.Add(updateSql);

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.17");
                else
                    ZhiFang.LabStar.Common.LogHelp.Error("Update Error 1.0.0.17");
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
                    ZhiFang.LabStar.Common.LogHelp.Info("Update CompareVersion Error：" + ex.Message);
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
                    ZhiFang.LabStar.Common.LogHelp.Info("Update CompareVersion Error：" + ex.Message);
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
                ZhiFang.LabStar.Common.LogHelp.Info("当前主程序集版本低于配置版本，无法升级！CurAssemblyVersion：" + oldVersion
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
            string updateSql = " update B_Para set ParaValue=\'" + newVersion + "\'" + " where ParaType=\'SYS\' and ParaNo=\'SYS_DBVersion\'";
            return ExecuteUpdateSQL(updateSql);
        }

        /// <summary>
        /// 获取数据库当前版本
        /// </summary>
        /// <returns></returns>
        private static string GetDataBaseCurVersion()
        {
            string curVersion = "1.0.0.0";
            DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Para where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                curVersion = ds.Tables[0].Rows[0][0].ToString();
            }
            else
            {
                ExecuteUpdateSQL("insert into [B_Para]([LabID],[ParaID],[ParaNo],[CName],[SName],[TypeCode],[ParaType],[ParaGroup],[ParaValue],[ParaDesc],[ParaEditInfo],[SystemCode],[ShortCode],[DispOrder],[PinYinZiTou],[bVisible],[IsUse],[ParaVER],[OperaterID],[Operater],[DataAddTime],[DataUpdateTime],[ParaMainClassCode],[ParaMainClassName]) " +
                       " Values(1," +//LabID
                       ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "," +//ParaID
                       "\'SYS_DBVersion\'," +//ParaNo
                       "\'数据库版本号\'," +//Name
                       "null," +//SName
                       "null," +//TypeCode
                       "\'SYS\'," +//ParaType
                        "null," +//ParaGroup
                        "null," +//ParaValue
                        "\'数据库版本号\'," +//ParaDesc
                        "null," +//ParaEditInfo
                        "null," +//SystemCode
                        "null," +//Shortcode
                        "0," +//DispOrder
                        "null," +//PinYinZiTou
                         "1," +//bVisible
                        "1," +//IsUse
                        "null," +//ParaVER
                        "0," +//OperaterID
                        "null," +//Operater
                        "\'" + DateTime.Now.ToString() + "\'," +//DataAddTime
                        "null," + //DataUpdateTime
                        "null," + //ParaMainClassCode
                        "null" + //ParaMainClassName
                        ")");
            }

            return curVersion;
        }

        public static string GetExternalDataBaseCurVersion()
        {
            string curVersion = "1.0.0.0";
            DataSet ds = SqlServerHelper.QuerySql(" select ParaValue from B_Para where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\'", ADOConnectStr);
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
                ZhiFang.LabStar.Common.LogHelp.Debug("objectID:" + objectID);
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
            if (!CheckDataObjectIsExists("B_Para", "U"))
            {
                List<string> listSQL = new List<string>();
                updateSql = " create table dbo.B_Para (   " +
                            "   LabID                bigint               not null, " +
                            "   ParaID               bigint               not null, " +
                            "   ParaNo               nvarchar(100)        null, " +
                            "   CName                nvarchar(200)        null, " +
                            "   SName                nvarchar(200)        null, " +
                            "   TypeCode             nvarchar(100)        null, " +
                            "   ParaType             nvarchar(100)        null, " +
                            "   ParaGroup            nvarchar(100)        null, " +
                            "   ParaValue            nvarchar(Max)        null, " +
                            "   ParaDesc             nvarchar(2000)       null, " +
                            "   ParaEditInfo         nvarchar(Max)        null, " +
                            "   SystemCode           nvarchar(200)        null, " +
                            "   ShortCode            nvarchar(100)        null, " +
                            "   DispOrder            int                  null, " +
                            "   PinYinZiTou          nvarchar(100)        null, " +
                            "   bVisible             bit                  null, " +
                            "   IsUse                bit                  null, " +
                            "   ParaMainClassName    nvarchar(200)        null, " +
                            "   ParaMainClassCode    nvarchar(100)        null, " +
                            "   ParaVER              nvarchar(50)         null, " +
                            "   OperaterID           bigint               null, " +
                            "   Operater             nvarchar(50)         null, " +
                            "   DataAddTime          datetime             null, " +
                            "   DataUpdateTime       datetime             null, " +
                            "   DataTimeStamp        timestamp            null, " +
                            "   constraint PK_B_PARA primary key (ParaID) " +
                            " ) ";

                listSQL.Add(updateSql);

                ExecuteUpdateSQL(listSQL);

                ExecuteUpdateSQL("insert into [B_Para]([LabID],[ParaID],[ParaNo],[CName],[SName],[TypeCode],[ParaType],[ParaGroup],[ParaValue],[ParaDesc],[ParaEditInfo],[SystemCode],[ShortCode],[DispOrder],[PinYinZiTou],[bVisible],[IsUse],[ParaVER],[OperaterID],[Operater],[DataAddTime],[DataUpdateTime],[ParaMainClassCode],[ParaMainClassName]) " +
                    " Values(1," +//LabID
                    ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "," +//ParaID
                    "SYS_DBVersion," +//ParaNo
                    "\'数据库版本号\'," +//Name
                    "null," +//SName
                    "null," +//TypeCode
                    "\'SYS\'," +//ParaType
                     "null," +//ParaGroup
                     "null," +//ParaValue
                     "\'数据库版本号\'," +//ParaDesc
                     "null," +//ParaEditInfo
                     "null," +//SystemCode
                     "null," +//Shortcode
                     "0," +//DispOrder
                     "null," +//PinYinZiTou
                      "1," +//bVisible
                     "1," +//IsUse
                     "null," +//ParaVER
                     "0," +//OperaterID
                     "null," +//Operater
                     "\'" + DateTime.Now.ToString() + "\'," +//DataAddTime
                     "null" + //DataUpdateTime
                     "null" + //ParaMainClassCode
                     "null" + //ParaMainClassName
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
