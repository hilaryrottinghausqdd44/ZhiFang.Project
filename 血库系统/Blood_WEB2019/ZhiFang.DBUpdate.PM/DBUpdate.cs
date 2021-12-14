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

        static string MainAssemblyFile = "ZhiFang.BloodTransfusion.dll";//可以从配置文件获取

        /// <summary>
        /// 初始化数据库版本和主程序集版本关系，key数据库版本，value主程序集版本
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, string> GetVersionComparison()
        {
            //每更新一次版本，需要手工在这里添加对应关系
            Dictionary<string, string> dicVersion = new Dictionary<string, string>();
            dicVersion.Add("1.0.0.1", "1.0.0.1");//1.添加数据库版本升级运行参数;
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
            dicVersion.Add("1.0.0.13", "1.0.0.13");//添加输血目的字典信息
            dicVersion.Add("1.0.0.14", "1.0.0.14");
            dicVersion.Add("1.0.0.15", "1.0.0.15");
            dicVersion.Add("1.0.0.16", "1.0.0.16");
            dicVersion.Add("1.0.0.17", "1.0.0.17");
            dicVersion.Add("1.0.0.18", "1.0.0.18");
            dicVersion.Add("1.0.0.19", "1.0.0.19");
            dicVersion.Add("1.0.0.20", "1.0.0.20");
            dicVersion.Add("1.0.0.21", "1.0.0.21");
            dicVersion.Add("1.0.0.22", "1.0.0.22");
            dicVersion.Add("1.0.0.23", "1.0.0.23");
            dicVersion.Add("1.0.0.24", "1.0.0.24");
            dicVersion.Add("1.0.0.25", "1.0.0.25");//系统运行参数
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
            dicVersion.Add("1.0.0.37", "1.0.0.37");//系统运行参数,角色,功能模块
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

                #endregion

                #region SC_Operation
                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SC_Operation]') AND type in (N'U')) CREATE TABLE [dbo].[SC_Operation]( [LabID] [bigint] NOT NULL, [SCOperationID] [bigint] NOT NULL, [BobjectID] [bigint] NOT NULL, [Type] [bigint] NULL, [TypeName] [varchar](20) NULL, [BusinessModuleCode] [varchar](20) NULL, [Memo] [varchar](max) NULL, [DispOrder] [int] NULL, [IsUse] [bit] NULL, [CreatorID] [bigint] NULL, [CreatorName] [varchar](50) NULL, [DataAddTime] [datetime] NULL, [DataUpdateTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_SC_OPERATION] PRIMARY KEY CLUSTERED ( [SCOperationID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_LargeUseItem

                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Blood_LargeUseItem]') AND type in(N'U')) CREATE TABLE dbo.Blood_LargeUseItem ( LUFID NVARCHAR (20) NOT NULL , BReqFormID NVARCHAR (40) NOT NULL , LUIMemo NVARCHAR (50) NULL , CONSTRAINT PK_Blood_LargeUseItem PRIMARY KEY (LUFID, BReqFormID)); ";
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
            //
            if (IsUpdateDataBase(oldVersion, "1.0.0.2"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region B_Parameter
                updateSql = " IF COL_LENGTH('B_Parameter', 'NodeID') IS NULL ALTER TABLE B_Parameter ADD NodeID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_Parameter', 'GroupNo') IS NULL ALTER TABLE B_Parameter ADD GroupNo bigint; ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_BreqForm
                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'LabID') IS NULL ALTER TABLE Blood_BreqForm ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'DispOrder') IS NULL ALTER TABLE Blood_BreqForm ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'DataAddTime') IS NULL ALTER TABLE Blood_BreqForm ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BreqForm ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'Visible') IS NULL ALTER TABLE Blood_BreqForm ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'HisDeptID') IS NULL ALTER TABLE Blood_BreqForm ADD HisDeptID varchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'HisDoctorId') IS NULL ALTER TABLE Blood_BreqForm ADD HisDoctorId varchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'CheckCompleteFlag') IS NULL ALTER TABLE Blood_BreqForm ADD CheckCompleteFlag bit; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'CheckCompleteTime') IS NULL ALTER TABLE Blood_BreqForm ADD CheckCompleteTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'ApplyID') IS NULL ALTER TABLE Blood_BreqForm ADD ApplyID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'ApplyName') IS NULL ALTER TABLE Blood_BreqForm ADD ApplyName varchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'ApplyTime') IS NULL ALTER TABLE Blood_BreqForm ADD ApplyTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'ApplyMemo') IS NULL ALTER TABLE Blood_BreqForm ADD ApplyMemo varchar(200); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'SeniorID') IS NULL ALTER TABLE Blood_BreqForm ADD SeniorID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'SeniorName') IS NULL ALTER TABLE Blood_BreqForm ADD SeniorName varchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'SeniorTime') IS NULL ALTER TABLE Blood_BreqForm ADD SeniorTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'SeniorMemo') IS NULL ALTER TABLE Blood_BreqForm ADD SeniorMemo varchar(200); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'DirectorID') IS NULL ALTER TABLE Blood_BreqForm ADD DirectorID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'DirectorName') IS NULL ALTER TABLE Blood_BreqForm ADD DirectorName varchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'DirectorTime') IS NULL ALTER TABLE Blood_BreqForm ADD DirectorTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'DirectorMemo') IS NULL ALTER TABLE Blood_BreqForm ADD DirectorMemo varchar(200); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'MedicalID') IS NULL ALTER TABLE Blood_BreqForm ADD MedicalID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'MedicalName') IS NULL ALTER TABLE Blood_BreqForm ADD MedicalName varchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'MedicalTime') IS NULL ALTER TABLE Blood_BreqForm ADD MedicalTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'MedicalMemo') IS NULL ALTER TABLE Blood_BreqForm ADD MedicalMemo varchar(200); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'BreqStatusID') IS NULL ALTER TABLE Blood_BreqForm ADD BreqStatusID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'BreqStatusName') IS NULL ALTER TABLE Blood_BreqForm ADD BreqStatusName varchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BreqForm set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BreqForm set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BreqForm set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BreqForm set DataAddTime=getDate() where DataAddTime is null; ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_BReqItem

                updateSql = " IF COL_LENGTH('Blood_BReqItem', 'LabID') IS NULL ALTER TABLE Blood_BReqItem ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BReqItem', 'DispOrder') IS NULL ALTER TABLE Blood_BReqItem ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BReqItem', 'DataAddTime') IS NULL ALTER TABLE Blood_BReqItem ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BReqItem', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BReqItem ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BReqItem', 'Visible') IS NULL ALTER TABLE Blood_BReqItem ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BReqItem set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BReqItem set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BReqItem set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BReqItem set DataAddTime=getDate() where DataAddTime is null; ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_LargeUseitem
                updateSql = " IF COL_LENGTH('Blood_LargeUseitem', 'LabID') IS NULL ALTER TABLE Blood_LargeUseitem ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_LargeUseitem', 'DispOrder') IS NULL ALTER TABLE Blood_LargeUseitem ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_LargeUseitem', 'DataAddTime') IS NULL ALTER TABLE Blood_LargeUseitem ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_LargeUseitem', 'DataTimeStamp') IS NULL ALTER TABLE Blood_LargeUseitem ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_LargeUseitem', 'Visible') IS NULL ALTER TABLE Blood_LargeUseitem ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_LargeUseitem set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_LargeUseitem set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_LargeUseitem set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_LargeUseitem set DataAddTime=getDate() where DataAddTime is null; ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_BreqItemResult
                updateSql = " IF COL_LENGTH('Blood_BreqItemResult', 'LabID') IS NULL ALTER TABLE Blood_BreqItemResult ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqItemResult', 'DispOrder') IS NULL ALTER TABLE Blood_BreqItemResult ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqItemResult', 'DataAddTime') IS NULL ALTER TABLE Blood_BreqItemResult ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqItemResult', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BreqItemResult ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqItemResult', 'Visible') IS NULL ALTER TABLE Blood_BreqItemResult ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BreqItemResult set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BreqItemResult set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BreqItemResult set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BreqItemResult set DataAddTime=getDate() where DataAddTime is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_LargeUseForm
                updateSql = " IF COL_LENGTH('Blood_LargeUseForm', 'LabID') IS NULL ALTER TABLE Blood_LargeUseForm ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_LargeUseForm', 'DispOrder') IS NULL ALTER TABLE Blood_LargeUseForm ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_LargeUseForm', 'DataAddTime') IS NULL ALTER TABLE Blood_LargeUseForm ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_LargeUseForm', 'DataTimeStamp') IS NULL ALTER TABLE Blood_LargeUseForm ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_LargeUseForm', 'Visible') IS NULL ALTER TABLE Blood_LargeUseForm ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_LargeUseForm set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_LargeUseForm set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_LargeUseForm set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_LargeUseForm set DataAddTime=getDate() where DataAddTime is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_LargeUseItem
                //给Blood_LargeUseItem先创建一个自增字段
                updateSql = " IF COL_LENGTH('Blood_LargeUseItem', 'Id') IS NULL ALTER TABLE Blood_LargeUseItem ADD Id bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_LargeUseItem', 'LabID') IS NULL ALTER TABLE Blood_LargeUseItem ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_LargeUseItem', 'DispOrder') IS NULL ALTER TABLE Blood_LargeUseItem ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_LargeUseItem', 'DataAddTime') IS NULL ALTER TABLE Blood_LargeUseItem ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_LargeUseItem', 'DataTimeStamp') IS NULL ALTER TABLE Blood_LargeUseItem ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_LargeUseItem', 'Visible') IS NULL ALTER TABLE Blood_LargeUseItem ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_LargeUseItem set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_LargeUseItem set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_LargeUseItem set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_LargeUseItem set DataAddTime=getDate() where DataAddTime is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region blood_useType
                updateSql = " IF COL_LENGTH('blood_useType', 'LabID') IS NULL ALTER TABLE blood_useType ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('blood_useType', 'DispOrder') IS NULL ALTER TABLE blood_useType ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('blood_useType', 'DataAddTime') IS NULL ALTER TABLE blood_useType ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('blood_useType', 'DataTimeStamp') IS NULL ALTER TABLE blood_useType ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('blood_useType', 'Visible') IS NULL ALTER TABLE blood_useType ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " Update blood_useType set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update blood_useType set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update blood_useType set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update blood_useType set DataAddTime=getDate() where DataAddTime is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BReqEditItem
                updateSql = " IF COL_LENGTH('Blood_BReqEditItem', 'LabID') IS NULL ALTER TABLE Blood_BReqEditItem ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BReqEditItem', 'DispOrder') IS NULL ALTER TABLE Blood_BReqEditItem ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BReqEditItem', 'DataAddTime') IS NULL ALTER TABLE Blood_BReqEditItem ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BReqEditItem', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BReqEditItem ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BReqEditItem', 'Visible') IS NULL ALTER TABLE Blood_BReqEditItem ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BReqEditItem set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BReqEditItem set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BReqEditItem set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BReqEditItem set DataAddTime=getDate() where DataAddTime is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_style
                updateSql = " IF COL_LENGTH('Blood_style', 'LabID') IS NULL ALTER TABLE Blood_style ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_style', 'DispOrder') IS NULL ALTER TABLE Blood_style ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_style', 'DataAddTime') IS NULL ALTER TABLE Blood_style ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_style', 'DataTimeStamp') IS NULL ALTER TABLE Blood_style ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_style', 'Visible') IS NULL ALTER TABLE Blood_style ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_style set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_style set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_style set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_style set DataAddTime=getDate() where DataAddTime is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region blood_btestItem
                updateSql = " IF COL_LENGTH('blood_btestItem', 'LabID') IS NULL ALTER TABLE blood_btestItem ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('blood_btestItem', 'DataAddTime') IS NULL ALTER TABLE blood_btestItem ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('blood_btestItem', 'DataTimeStamp') IS NULL ALTER TABLE blood_btestItem ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " Update blood_btestItem set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update blood_btestItem set DataAddTime=getDate() where DataAddTime is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region blood_unit
                updateSql = " IF COL_LENGTH('blood_unit', 'LabID') IS NULL ALTER TABLE blood_unit ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('blood_unit', 'DataAddTime') IS NULL ALTER TABLE blood_unit ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('blood_unit', 'DataTimeStamp') IS NULL ALTER TABLE blood_unit ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " Update blood_unit set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update blood_unit set DataAddTime=getDate() where DataAddTime is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region blood_class
                updateSql = " IF COL_LENGTH('blood_class', 'LabID') IS NULL ALTER TABLE blood_class ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('blood_class', 'DispOrder') IS NULL ALTER TABLE blood_class ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('blood_class', 'DataAddTime') IS NULL ALTER TABLE blood_class ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('blood_class', 'DataTimeStamp') IS NULL ALTER TABLE blood_class ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('blood_class', 'Visible') IS NULL ALTER TABLE blood_class ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " Update blood_class set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update blood_class set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update blood_class set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update blood_class set DataAddTime=getDate() where DataAddTime is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region blood_useType
                updateSql = " IF COL_LENGTH('blood_useType', 'LabID') IS NULL ALTER TABLE blood_useType ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('blood_useType', 'DataAddTime') IS NULL ALTER TABLE blood_useType ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('blood_useType', 'DataTimeStamp') IS NULL ALTER TABLE blood_useType ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('blood_useType', 'Visible') IS NULL ALTER TABLE blood_useType ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " Update blood_useType set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update blood_useType set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update blood_useType set DataAddTime=getDate() where DataAddTime is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Department
                updateSql = " IF COL_LENGTH('Department', 'LabID') IS NULL ALTER TABLE Department ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Department', 'DataTimeStamp') IS NULL ALTER TABLE Department ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " Update Department set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region PUser
                updateSql = " IF COL_LENGTH('PUser', 'LabID') IS NULL ALTER TABLE PUser ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('PUser', 'DataTimeStamp') IS NULL ALTER TABLE PUser ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " Update PUser set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region doctor
                updateSql = " IF COL_LENGTH('doctor', 'LabID') IS NULL ALTER TABLE doctor ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('doctor', 'DataTimeStamp') IS NULL ALTER TABLE doctor ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('blood_useType', 'DataAddTime') IS NULL ALTER TABLE blood_useType ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " Update doctor set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update blood_useType set DataAddTime=getDate() where DataAddTime is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BReqType
                updateSql = " IF COL_LENGTH('Blood_BReqType', 'LabID') IS NULL ALTER TABLE Blood_BReqType ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BReqType', 'DispOrder') IS NULL ALTER TABLE Blood_BReqType ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BReqType', 'DataAddTime') IS NULL ALTER TABLE Blood_BReqType ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BReqType', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BReqType ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BReqType', 'Visible') IS NULL ALTER TABLE Blood_BReqType ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BReqType set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BReqType set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BReqType set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BReqType set DataAddTime=getDate() where DataAddTime is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BReqTypeItem
                updateSql = " IF COL_LENGTH('Blood_BReqTypeItem', 'LabID') IS NULL ALTER TABLE Blood_BReqTypeItem ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BReqTypeItem', 'DispOrder') IS NULL ALTER TABLE Blood_BReqTypeItem ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BReqTypeItem', 'DataAddTime') IS NULL ALTER TABLE Blood_BReqTypeItem ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BReqTypeItem', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BReqTypeItem ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BReqTypeItem', 'Visible') IS NULL ALTER TABLE Blood_BReqTypeItem ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BReqTypeItem set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BReqTypeItem set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BReqTypeItem set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BReqTypeItem set DataAddTime=getDate() where DataAddTime is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BReqEditItem
                updateSql = " IF COL_LENGTH('Blood_BReqEditItem', 'LisCode') IS NULL ALTER TABLE Blood_BReqEditItem ADD LisCode varchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BReqEditItem set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_BreqForm
                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'AeniorID') IS NOT NULL exec sp_rename 'Blood_BreqForm.AeniorID','SeniorID';";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'AeniorName') IS NOT NULL exec sp_rename 'Blood_BreqForm.AeniorName','SeniorName'; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'AeniorTime') IS NOT NULL exec sp_rename 'Blood_BreqForm.AeniorTime','SeniorTime'; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'AeniorMemo') IS NOT NULL exec sp_rename 'Blood_BreqForm.AeniorMemo','SeniorMemo'; ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_LargeUseForm
                updateSql = " IF COL_LENGTH('Blood_LargeUseForm', 'breqformIDLast') IS NULL ALTER TABLE Blood_LargeUseForm ADD breqformIDLast nvarchar(40); ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BreqForm
                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'CheckDoctorNo') IS NOT NULL ALTER TABLE Blood_BreqForm DROP COLUMN CheckDoctorNo; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'Checktime') IS NOT NULL ALTER TABLE Blood_BreqForm DROP COLUMN Checktime; ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_LargeUseForm
                updateSql = " IF COL_LENGTH('Blood_LargeUseForm', 'breqformIDLast') IS NULL ALTER TABLE Blood_LargeUseForm ADD breqformIDLast varchar(20); ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_BreqForm
                updateSql = " IF COL_LENGTH('Blood_BReqForm', 'ComNoIndex') IS not NULL ALTER TABLE Blood_BReqForm   DROP COLUMN ComNoIndex ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'PatABO') IS NULL ALTER TABLE Blood_BreqForm ADD PatABO varchar(10); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'PatRh') IS NULL ALTER TABLE Blood_BreqForm ADD PatRh varchar(10); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'BloodWay') IS NULL ALTER TABLE Blood_BreqForm ADD BloodWay varchar(20); ";
                listSQL.Add(updateSql);

                //updateSql = " IF COL_LENGTH('Blood_BreqForm', 'LisALB') IS NULL ALTER TABLE Blood_BreqForm ADD LisALB varchar(20); ";
                //listSQL.Add(updateSql);

                //updateSql = " IF COL_LENGTH('Blood_BreqForm', 'LisALT') IS NULL ALTER TABLE Blood_BreqForm ADD LisALT varchar(20); ";
                //listSQL.Add(updateSql);

                //updateSql = " IF COL_LENGTH('Blood_BreqForm', 'LisAPTT') IS NULL ALTER TABLE Blood_BreqForm ADD LisAPTT varchar(20); ";
                //listSQL.Add(updateSql);

                //updateSql = " IF COL_LENGTH('Blood_BreqForm', 'LisFbg') IS NULL ALTER TABLE Blood_BreqForm ADD LisFbg varchar(20); ";
                //listSQL.Add(updateSql);

                //updateSql = " IF COL_LENGTH('Blood_BreqForm', 'LisHBc') IS NULL ALTER TABLE Blood_BreqForm ADD LisHBc varchar(20); ";
                //listSQL.Add(updateSql);

                //updateSql = " IF COL_LENGTH('Blood_BreqForm', 'LisHBe') IS NULL ALTER TABLE Blood_BreqForm ADD LisHBe varchar(20); ";
                //listSQL.Add(updateSql);

                //updateSql = " IF COL_LENGTH('Blood_BreqForm', 'LisHBeAg') IS NULL ALTER TABLE Blood_BreqForm ADD LisHBeAg varchar(20); ";
                //listSQL.Add(updateSql);

                //updateSql = " IF COL_LENGTH('Blood_BreqForm', 'LisHCT') IS NULL ALTER TABLE Blood_BreqForm ADD LisHCT varchar(20); ";
                //listSQL.Add(updateSql);

                //updateSql = " IF COL_LENGTH('Blood_BreqForm', 'LisHCV') IS NULL ALTER TABLE Blood_BreqForm ADD LisHCV varchar(20); ";
                //listSQL.Add(updateSql);

                //updateSql = " IF COL_LENGTH('Blood_BreqForm', 'LisHGB') IS NULL ALTER TABLE Blood_BreqForm ADD LisHGB varchar(20); ";
                //listSQL.Add(updateSql);

                //updateSql = " IF COL_LENGTH('Blood_BreqForm', 'LisHIV') IS NULL ALTER TABLE Blood_BreqForm ADD LisHIV varchar(20); ";
                //listSQL.Add(updateSql);

                //updateSql = " IF COL_LENGTH('Blood_BreqForm', 'LisPLT') IS NULL ALTER TABLE Blood_BreqForm ADD LisPLT varchar(20); ";
                //listSQL.Add(updateSql);

                //updateSql = " IF COL_LENGTH('Blood_BreqForm', 'LisPT') IS NULL ALTER TABLE Blood_BreqForm ADD LisPT varchar(20); ";
                //listSQL.Add(updateSql);

                //updateSql = " IF COL_LENGTH('Blood_BreqForm', 'LisRBC') IS NULL ALTER TABLE Blood_BreqForm ADD LisRBC varchar(20); ";
                //listSQL.Add(updateSql);

                //updateSql = " IF COL_LENGTH('Blood_BreqForm', 'LisRPR') IS NULL ALTER TABLE Blood_BreqForm ADD LisRPR varchar(20); ";
                //listSQL.Add(updateSql);

                //updateSql = " IF COL_LENGTH('Blood_BreqForm', 'LisTT') IS NULL ALTER TABLE Blood_BreqForm ADD LisTT varchar(20); ";
                //listSQL.Add(updateSql);

                //updateSql = " IF COL_LENGTH('Blood_BreqForm', 'LisWBC') IS NULL ALTER TABLE Blood_BreqForm ADD LisWBC varchar(20); ";
                //listSQL.Add(updateSql);

                //updateSql = " IF COL_LENGTH('Blood_BreqForm', 'LisHBs') IS NULL ALTER TABLE Blood_BreqForm ADD LisHBs varchar(20); ";
                //listSQL.Add(updateSql);

                //updateSql = " IF COL_LENGTH('Blood_BreqForm', 'LisHBsAg') IS NULL ALTER TABLE Blood_BreqForm ADD LisHBsAg varchar(20); ";
                //listSQL.Add(updateSql);

                #endregion

                #region Blood_docGrade
                updateSql = " IF COL_LENGTH('Blood_docGrade', 'LabID') IS NULL ALTER TABLE Blood_docGrade ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_docGrade', 'DataAddTime') IS NULL ALTER TABLE Blood_docGrade ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_docGrade', 'DataTimeStamp') IS NULL ALTER TABLE Blood_docGrade ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_docGrade set DataAddTime=getDate() where DataAddTime is null; ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_LargeUseItem
                //给Blood_LargeUseItem新增的自增ID赋值
                //updateSql = " IF COL_LENGTH('Blood_LargeUseItem', 'Id') IS NOT NULL Update Blood_LargeUseItem set Id=NULL;Update Blood_LargeUseItem set Id = rownum from( select Id, ROW_NUMBER() over(order by Id) rownum from Blood_LargeUseItem ) Blood_LargeUseItem; ";
                //listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.2");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.2) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.3
            if (IsUpdateDataBase(oldVersion, "1.0.0.3"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_LargeUseItem
                //给Blood_LargeUseItem新增的Id创建自增关系,先删除Id,再创建
                updateSql = "IF COL_LENGTH('Blood_LargeUseItem', 'Id') IS NOT NULL alter table Blood_LargeUseItem drop column Id; ALTER TABLE Blood_LargeUseItem Add Id bigint IDENTITY(1,1) NOT NULL; ";
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
            if (IsUpdateDataBase(oldVersion, "1.0.0.4"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_BReqForm
                updateSql = " IF COL_LENGTH('Blood_BReqForm', 'ReqTotal') IS NULL ALTER TABLE Blood_BReqForm ADD ReqTotal float; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'ObsoleteID') IS NULL ALTER TABLE Blood_BreqForm ADD ObsoleteID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'ObsoleteName') IS NULL ALTER TABLE Blood_BreqForm ADD ObsoleteName varchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'ObsoleteTime') IS NULL ALTER TABLE Blood_BreqForm ADD ObsoleteTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BreqForm', 'ObsoleteMemo') IS NULL ALTER TABLE Blood_BreqForm ADD ObsoleteMemo varchar(200); ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_BReqEditItem
                updateSql = " IF COL_LENGTH('Blood_BReqEditItem', 'ReqCode') IS NULL ALTER TABLE Blood_BReqEditItem ADD ReqCode varchar(20); ";
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
            if (IsUpdateDataBase(oldVersion, "1.0.0.5"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_UseDesc
                updateSql = " IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Blood_UseDesc]') AND type in (N'U')) DROP TABLE [dbo].[Blood_UseDesc]; CREATE TABLE [dbo].[Blood_UseDesc]( [LabID] [bigint] NULL, [Id] [bigint] NOT NULL, [VersionNo] [varchar](50) NULL, [Contents] [text] NULL, [Visible] [bit] NULL, [DispOrder] [int] NULL, [DataAddTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_Blood_UseDesc] PRIMARY KEY CLUSTERED ( [Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]; ";
                listSQL.Add(updateSql);

                #endregion

                #region B_Parameter
                updateSql = " IF COL_LENGTH('B_Parameter', 'NodeID') IS NULL ALTER TABLE B_Parameter ADD NodeID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_Parameter', 'GroupNo') IS NULL ALTER TABLE B_Parameter ADD GroupNo bigint; ";
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
            if (IsUpdateDataBase(oldVersion, "1.0.0.6"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region B_Parameter
                updateSql = " IF COL_LENGTH('B_Parameter', 'NodeID') IS NULL ALTER TABLE B_Parameter ADD NodeID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_Parameter', 'GroupNo') IS NULL ALTER TABLE B_Parameter ADD GroupNo bigint; ";
                listSQL.Add(updateSql);
                #endregion

                #region PUser
                //给PUser添加BS血库系统的默认登录帐号(供第三方调用BS血库功能模块时,以该帐号进行免登录处理)
                updateSql = " IF NOT EXISTS (SELECT * FROM PUser WHERE [UserNo] = 19999) INSERT INTO [dbo].[PUser]([UserNo] ,[CName] ,[Password] ,[ShortCode] ,[Gender] ,[Birthday] ,[Role] ,[Resume] ,[Visible] ,[DispOrder] ,[HisOrderCode] ,[userimage] ,[usertype] ,[SectorTypeNo] ,[UserImeName] ,[IsManager] ,[PassWordS] ,[CAUserName] ,[CAContainerName] ,[CAUserID] ,[CAkeysn] ,[DeptNo] ,[PWDateTime] ,[LoginDateTime] ,[code_1] ,[code_2] ,[code_3] ,[code_4] ,[code_5] ,[CAUserAuthorised] ,[UserDataRights] ,[Email] ,[Imgsignature] ,[LabID]) VALUES (19999 ,'血库免登录' ,'=er5flP5' ,'19999' ,1 ,'1900-01-01 00:00:00.000' ,'医生站' ,1 ,1 ,19999  ,NULL ,NULL ,'医生' ,NULL ,NULL ,0 ,'=er5flP5' ,NULL ,NULL ,NULL ,NULL ,NULL ,'2019-07-08 13:26:18.243' ,'2019-07-08 13:26:18.243' ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,NULL ,0); ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_docGrade
                updateSql = " IF COL_LENGTH('Blood_docGrade', 'LowLimit') IS NULL ALTER TABLE Blood_docGrade ADD LowLimit float; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_docGrade', 'UpperLimit') IS NULL ALTER TABLE Blood_docGrade ADD UpperLimit float; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_docGrade', 'LabID') IS NOT NULL Update Blood_docGrade set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                //主治医师审核用血量范围
                updateSql = " Update Blood_docGrade set LowLimit=0,UpperLimit=800 where GradeNo=2 and LowLimit is null and UpperLimit is null;";
                listSQL.Add(updateSql);

                //科主任审核用血量范围
                updateSql = " Update Blood_docGrade set LowLimit=801,UpperLimit=1600 where GradeNo=3 and LowLimit is null and UpperLimit is null;";
                listSQL.Add(updateSql);

                //医务科审核用血量范围
                updateSql = " Update Blood_docGrade set LowLimit=1600,UpperLimit=100000000 where GradeNo=4 and LowLimit is null and UpperLimit is null;";
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
            if (IsUpdateDataBase(oldVersion, "1.0.0.7"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_UseDesc
                //添加默认用血说明信息
                updateSql = "  IF NOT EXISTS(SELECT * FROM Blood_UseDesc WHERE [Id] = 100) INSERT INTO [dbo].[Blood_UseDesc] ([LabID] ,[Id] ,[VersionNo] ,[Contents] ,[Visible] ,[DispOrder] ,[DataAddTime]) VALUES (0 ,100 ,'1' ,'' ,1 ,1 ,'2019-07-06 10:33:48.050'); ";
                listSQL.Add(updateSql);
                #endregion

                #region PUser
                //新增了医嘱检验结果表
                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Blood_BReqFormResult]') AND type IN (N'U')) CREATE TABLE [dbo].[Blood_BReqFormResult]( [LabID] [bigint] NULL, [ResultID] [bigint] NOT NULL, [BReqFormID] [nvarchar](40) NOT NULL, [BTestItemNo] [nvarchar](20) NOT NULL, [BTestTime] [datetime] NULL, [Barcode] [nvarchar](20) NULL, [ItemResult] [nvarchar](500) NULL, [ItemUnit] [nvarchar](500) NULL, [DispOrder] [int] NULL, [DataAddTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, [Visible] [bit] NULL, CONSTRAINT [PK_BReqFormResult] PRIMARY KEY CLUSTERED ( [ResultID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BReqFormResult
                updateSql = " IF COL_LENGTH('Blood_BReqFormResult', 'ItemLisResult') IS NULL ALTER TABLE Blood_BReqFormResult ADD ItemLisResult varchar(500); ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BReqFormResult
                //updateSql = " IF COL_LENGTH('Blood_BReqFormResult', 'ItemLisResult') IS NULL ALTER TABLE Blood_BReqFormResult ADD ItemLisResult varchar(500); ";
                //listSQL.Add(updateSql);
                #endregion

                #region Blood_BReqFormResult
                updateSql = " IF COL_LENGTH('Blood_BReqFormResult', 'BTestItemEName') IS NULL ALTER TABLE Blood_BReqFormResult ADD BTestItemEName varchar(150); ";
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
            if (IsUpdateDataBase(oldVersion, "1.0.0.8"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_BReqFormResult
                updateSql = " IF COL_LENGTH('Blood_BReqFormResult', 'ItemLisResult') IS NULL ALTER TABLE Blood_BReqFormResult ADD ItemLisResult varchar(500); ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BReqFormResult
                updateSql = " IF COL_LENGTH('Blood_BReqFormResult', 'BTestItemEName') IS NULL ALTER TABLE Blood_BReqFormResult ADD BTestItemEName varchar(150); ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BTestItem
                updateSql = " IF COL_LENGTH('Blood_BTestItem', 'IsResultItem') IS NULL ALTER TABLE Blood_BTestItem ADD IsResultItem bit; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BTestItem set IsResultItem=1 where IsResultItem is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BReqForm
                updateSql = " IF COL_LENGTH('Blood_BReqForm', 'ToHisFlag') IS NULL ALTER TABLE Blood_BReqForm ADD ToHisFlag int; ";
                listSQL.Add(updateSql);

                #endregion

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
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_Patinfo
                updateSql = " IF COL_LENGTH('Blood_Patinfo', 'AdmID') IS NULL ALTER TABLE Blood_Patinfo ADD AdmID varchar(20); ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BReqForm
                updateSql = " IF COL_LENGTH('Blood_BReqForm', 'AdmID') IS NULL ALTER TABLE Blood_BReqForm ADD AdmID varchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BReqForm set BReqFormFlag=0 where BReqFormFlag is null; ";
                listSQL.Add(updateSql);
                //更新医嘱申请主单的HIS数据标志默认值
                updateSql = " update Blood_BReqForm set ToHisFlag=0 where ToHisFlag is null and DataAddTime>'2019-08-01 00:00:00.000'; ";
                listSQL.Add(updateSql);
                #endregion

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
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region B_DictType
                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[B_DictType]') AND type in (N'U')) CREATE TABLE [dbo].[B_DictType]( [LabID] bigint NOT NULL, [DCId] bigint NOT NULL, [CName] [varchar](40) NULL, [DispOrder] [int] NULL, [Memo] [ntext] NULL, [IsUse] [bit] NULL, [DataAddTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, [DictTypeCode] [varchar](100) NULL, CONSTRAINT [PK_B_DICCLASS] PRIMARY KEY CLUSTERED ( [DCId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                #region B_Dict
                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[B_Dict]') AND type in (N'U')) CREATE TABLE [dbo].[B_Dict]( [LabID] bigint NOT NULL, [DID] bigint NOT NULL, [DCId] bigint NULL, [CName] [varchar](80) NULL, [SName] [varchar](80) NULL, [Shortcode] [varchar](40) NULL, [PinYinZiTou] [varchar](50) NULL, [DispOrder] [int] NULL, [Memo] [ntext] NULL, [IsUse] [bit] NULL, [DataAddTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_B_DIC] PRIMARY KEY CLUSTERED ( [DID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]; ";
                listSQL.Add(updateSql);

                #endregion

                #region B_DictTree
                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[B_DictTree]') AND type in (N'U')) CREATE TABLE [dbo].[B_DictTree]( [LabID] bigint NOT NULL, [TypeTreeID] bigint NOT NULL, [ParentID] bigint NULL, [CName] [varchar](100) NOT NULL, [SName] [varchar](40) NULL, [Shortcode] [varchar](40) NULL, [Memo] [varchar](500) NULL, [DispOrder] [int] NULL, [IsUse] [bit] NULL, [CreatorID] bigint NULL, [CreatorName] [varchar](50) NULL, [DataAddTime] [datetime] NULL, [DataUpdateTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, [StandCode] [varchar](50) NULL, [DeveCode] [varchar](50) NULL, CONSTRAINT [PK_F_TYPETREE] PRIMARY KEY CLUSTERED ( [TypeTreeID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY];  ";
                listSQL.Add(updateSql);

                #endregion

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
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region B_DictType
                //字典类型添加医嘱作废原因
                updateSql = " IF NOT EXISTS(SELECT * FROM B_DictType WHERE [DCId] = 10) INSERT [B_DictType] ([LabID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime],[DictTypeCode]) VALUES ( 0,10,N'医嘱作废原因',100,N'医生站医嘱申请作废原因',1,N'2019/08/21 09:29:26',N'BReqFormObsolete'); ";
                listSQL.Add(updateSql);
                #endregion

                #region B_Dict
                updateSql = " IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 4713710510004576645) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime]) VALUES ( 0,4713710510004576645,10,N'患者出院',10,N'患者出院',1,N'2019/08/21 09:35:00'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 5418855651502951451) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime]) VALUES ( 0,5418855651502951451,10,N'其他原因',30,N'其他原因',1,N'2019/08/21 09:35:37'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 5753580128494855321) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime]) VALUES ( 0,5753580128494855321,10,N'患者转院',20,N'患者转院',1,N'2019/08/21 09:35:14'); ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_B_DIC_REFERENCE_B_DICCLA]') AND parent_object_id = OBJECT_ID(N'[dbo].[B_Dict]')) ALTER TABLE [dbo].[B_Dict] DROP CONSTRAINT [FK_B_DIC_REFERENCE_B_DICCLA]; ALTER TABLE [dbo].[B_Dict] WITH CHECK ADD CONSTRAINT [FK_B_DIC_REFERENCE_B_DICCLA] FOREIGN KEY([DCId]) REFERENCES [dbo].[B_DictType] ([DCId]); ALTER TABLE [dbo].[B_Dict] CHECK CONSTRAINT [FK_B_DIC_REFERENCE_B_DICCLA]; ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_BReqForm
                updateSql = " IF COL_LENGTH('Blood_BReqForm', 'ObsoleteMemoId') IS NULL ALTER TABLE Blood_BReqForm ADD ObsoleteMemoId bigint; ";
                listSQL.Add(updateSql);

                #endregion

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
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_Patinfo
                updateSql = " IF COL_LENGTH('Blood_Patinfo', 'isagree') IS NULL ALTER TABLE Blood_Patinfo ADD isagree varchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Patinfo', 'WardNo') IS NULL ALTER TABLE Blood_Patinfo ADD WardNo varchar(50); ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_BReqForm
                updateSql = " IF COL_LENGTH('Blood_BReqForm', 'OrganTransplant') IS NULL ALTER TABLE Blood_BReqForm ADD OrganTransplant varchar(10); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BReqForm', 'MarrowTransplantation') IS NULL ALTER TABLE Blood_BReqForm ADD MarrowTransplantation varchar(10); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BReqForm', 'WardNo') IS NULL ALTER TABLE Blood_BReqForm ADD WardNo varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BReqForm', 'HisWardNo') IS NULL ALTER TABLE Blood_BReqForm ADD HisWardNo varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BReqForm', 'BLPreEvaluation') IS NULL ALTER TABLE Blood_BReqForm ADD BLPreEvaluation varchar(5000); ";
                listSQL.Add(updateSql);

                #endregion

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
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region B_DictType
                //字典类型添加输血目的
                updateSql = " IF NOT EXISTS(SELECT * FROM B_DictType WHERE [DCId] = 5586711528094669430) INSERT [B_DictType] ([LabID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime],[DictTypeCode]) VALUES ( 0,5586711528094669430,N'输血目的',260,N'输血目的',1,N'2019-09-03 17:08:46',N'UsePurpose'); ";
                listSQL.Add(updateSql);
                #endregion

                #region B_Dict
                updateSql = " IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 5715856889117279446) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime]) VALUES ( 0,5715856889117279446,5586711528094669430,N'增加携氧能力',1,N'增加携氧能力',1,N'2019-09-10 11:23:05'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 5751296562173100464) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime]) VALUES ( 0,5751296562173100464,5586711528094669430,N'增加凝血因子',2,N'增加凝血因子',1,N'2019-09-10 11:23:15'); ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.13");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.13) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.14
            if (IsUpdateDataBase(oldVersion, "1.0.0.14"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();
                #region B_Parameter
                //申请单号生成规则
                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5691564273202278074) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[DataUpdateTime]) VALUES ( 0,5691564273202278074,N'输血申请单当前流水号',N'医生站',N'CONFIG',N'BL-BRQF-CURN-0009',N'0',N'申请单号生成规则:年月日+4位顺序流水号(按天重新生成)',11,1,0,N'2019-10-08 16:06:52',N'2019-10-08 16:06:52');";
                listSQL.Add(updateSql);
                #endregion

                #region B_DictType
                //字典类型添加用血方式
                updateSql = " IF NOT EXISTS(SELECT * FROM B_DictType WHERE [DCId] = 5586711528094669431) INSERT [B_DictType] ([LabID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime],[DictTypeCode]) VALUES ( 0,5586711528094669431,N'用血方式',260,N'用血方式',1,N'2019-10-08 16:06:52',N'BloodWay'); ";
                listSQL.Add(updateSql);
                #endregion

                #region B_Dict
                //字典添默认的用血方式
                updateSql = " IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 10001) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime]) VALUES ( 0,10001,5586711528094669431,N'异型输血',1,N'异型输血',1,N'2019-10-08 16:06:52'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 10002) INSERT [B_Dict] ([LabID],[DID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime]) VALUES ( 0,10002,5586711528094669431,N'自体输血',2,N'自体输血',1,N'2019-10-08 16:06:52'); ";
                listSQL.Add(updateSql);

                #endregion


                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.14");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.14) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.15
            if (IsUpdateDataBase(oldVersion, "1.0.0.15"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_BReqForm

                updateSql = " IF COL_LENGTH('Blood_BReqForm', 'PrintTotal') IS NULL ALTER TABLE Blood_BReqForm ADD PrintTotal int; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BReqForm set PrintTotal=0 where PrintTotal<0 or PrintTotal is null; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.15");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.15) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.16
            if (IsUpdateDataBase(oldVersion, "1.0.0.16"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_BReqForm
                updateSql = " IF COL_LENGTH('Blood_BReqForm', 'HasAllergy') IS NULL ALTER TABLE Blood_BReqForm ADD HasAllergy bit; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BReqForm set HasAllergy=0 where HasAllergy is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BTestItem
                updateSql = " IF COL_LENGTH('Blood_BTestItem', 'IsPreTrransfusionEvaluationItem') IS NULL ALTER TABLE Blood_BTestItem ADD IsPreTrransfusionEvaluationItem bit; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BTestItem set IsPreTrransfusionEvaluationItem=0 where IsPreTrransfusionEvaluationItem is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BReqFormResult
                updateSql = " IF COL_LENGTH('Blood_BReqFormResult', 'IsPreTrransfusionEvaluationItem') IS NULL ALTER TABLE Blood_BReqFormResult ADD IsPreTrransfusionEvaluationItem bit; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BReqFormResult set IsPreTrransfusionEvaluationItem=0 where IsPreTrransfusionEvaluationItem is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region B_UserUIConfig
                updateSql = " if exists(select 1 from sysobjects where id = object_id('B_UserUIConfig') and type = 'U') drop table B_UserUIConfig create table B_UserUIConfig ( LabID bigint null, UserUIID bigint not null, UserUIKey varchar(100) null, UserUIName varchar(100) null, TemplateTypeID bigint null, TemplateTypeCName varchar(100) null, UITypeID bigint null, UITypeName varchar(100) null, ModuleId bigint null, EmpID bigint null, IsDefault bit null, Comment ntext collate Chinese_PRC_CI_AS null, DispOrder int null, IsUse bit null, DataAddTime datetime null, DataUpdateTime datetime null, DataTimeStamp timestamp null, constraint PK_B_USERUICONFIG primary key (UserUIID)); ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.16");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.16) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.17
            if (IsUpdateDataBase(oldVersion, "1.0.0.17"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_BUnit
                updateSql = " IF COL_LENGTH('Blood_BUnit', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BUnit ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.17");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.17) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.18
            if (IsUpdateDataBase(oldVersion, "1.0.0.18"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_BInItem(入库明细)
                updateSql = " IF COL_LENGTH('Blood_BInItem', 'LabID') IS NULL ALTER TABLE Blood_BInItem ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BInItem', 'DispOrder') IS NULL ALTER TABLE Blood_BInItem ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BInItem', 'DataAddTime') IS NULL ALTER TABLE Blood_BInItem ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BInItem', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BInItem ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BInItem', 'Visible') IS NULL ALTER TABLE Blood_BInItem ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BInItem set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BInItem set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BInItem set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BPreForm(配血主单)
                updateSql = " IF COL_LENGTH('Blood_BPreForm', 'LabID') IS NULL ALTER TABLE Blood_BPreForm ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BPreForm', 'DispOrder') IS NULL ALTER TABLE Blood_BPreForm ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BPreForm', 'DataAddTime') IS NULL ALTER TABLE Blood_BPreForm ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BPreForm', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BPreForm ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BPreForm', 'Visible') IS NULL ALTER TABLE Blood_BPreForm ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BPreForm set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                //DataAddTime
                updateSql = " update Blood_BPreForm set DataAddTime=getdate() where DataAddTime is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BPreForm set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BPreForm set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_BPreItem(配血明细)
                updateSql = " IF COL_LENGTH('Blood_BPreItem', 'LabID') IS NULL ALTER TABLE Blood_BPreItem ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BPreItem', 'DispOrder') IS NULL ALTER TABLE Blood_BPreItem ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BPreItem', 'DataAddTime') IS NULL ALTER TABLE Blood_BPreItem ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BPreItem', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BPreItem ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BPreItem', 'Visible') IS NULL ALTER TABLE Blood_BPreItem ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BPreItem set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                //DataAddTime
                updateSql = " update Blood_BPreItem set DataAddTime=getdate() where DataAddTime is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BPreItem set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BPreItem set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_BOutForm(出库主单)
                updateSql = " IF COL_LENGTH('Blood_BOutForm', 'LabID') IS NULL ALTER TABLE Blood_BOutForm ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutForm', 'DispOrder') IS NULL ALTER TABLE Blood_BOutForm ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutForm', 'DataAddTime') IS NULL ALTER TABLE Blood_BOutForm ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutForm', 'DataUpdateTime') IS NULL ALTER TABLE Blood_BOutForm ADD DataUpdateTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutForm', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BOutForm ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutForm', 'Visible') IS NULL ALTER TABLE Blood_BOutForm ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutForm', 'ConfirmCompletion') IS NULL ALTER TABLE Blood_BOutForm ADD ConfirmCompletion int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutForm', 'HandoverCompletion') IS NULL ALTER TABLE Blood_BOutForm ADD HandoverCompletion int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutForm', 'CourseCompletion') IS NULL ALTER TABLE Blood_BOutForm ADD CourseCompletion int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutForm', 'RecoverCompletion') IS NULL ALTER TABLE Blood_BOutForm ADD RecoverCompletion int; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BOutForm set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                //DataAddTime等于出库时间
                updateSql = " update Blood_BOutForm set DataAddTime=getdate() where DataAddTime is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BOutForm set DataUpdateTime=getdate() where DataUpdateTime is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BOutForm set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BOutForm set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_BOutItem(出库明细)
                updateSql = " IF COL_LENGTH('Blood_BOutItem', 'LabID') IS NULL ALTER TABLE Blood_BOutItem ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutItem', 'DispOrder') IS NULL ALTER TABLE Blood_BOutItem ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutItem', 'DataAddTime') IS NULL ALTER TABLE Blood_BOutItem ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutItem', 'DataUpdateTime') IS NULL ALTER TABLE Blood_BOutItem ADD DataUpdateTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutItem', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BOutItem ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutItem', 'Visible') IS NULL ALTER TABLE Blood_BOutItem ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutItem', 'ConfirmCompletion') IS NULL ALTER TABLE Blood_BOutItem ADD ConfirmCompletion int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutItem', 'HandoverCompletion') IS NULL ALTER TABLE Blood_BOutItem ADD HandoverCompletion int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutItem', 'CourseCompletion') IS NULL ALTER TABLE Blood_BOutItem ADD CourseCompletion int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutItem', 'RecoverCompletion') IS NULL ALTER TABLE Blood_BOutItem ADD RecoverCompletion int; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BOutItem set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                //DataAddTime等于出库时间
                updateSql = " update Blood_BOutItem set DataAddTime=BODate where DataAddTime is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BOutItem set DataUpdateTime=getdate() where DataUpdateTime is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BOutItem set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BOutItem set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_UsePlace(输血位置)
                updateSql = " IF COL_LENGTH('Blood_UsePlace', 'LabID') IS NULL ALTER TABLE Blood_UsePlace ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_UsePlace', 'DispOrder') IS NULL ALTER TABLE Blood_UsePlace ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_UsePlace', 'DataAddTime') IS NULL ALTER TABLE Blood_UsePlace ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_UsePlace', 'DataTimeStamp') IS NULL ALTER TABLE Blood_UsePlace ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_UsePlace', 'Visible') IS NULL ALTER TABLE Blood_UsePlace ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_UsePlace set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_UsePlace set DataAddTime=getdate() where DataAddTime is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_UsePlace set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_UsePlace set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_ABO(血型表)
                updateSql = " IF COL_LENGTH('Blood_ABO', 'LabID') IS NULL ALTER TABLE Blood_ABO ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_ABO', 'DispOrder') IS NULL ALTER TABLE Blood_ABO ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_ABO', 'DataAddTime') IS NULL ALTER TABLE Blood_ABO ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_ABO', 'DataTimeStamp') IS NULL ALTER TABLE Blood_ABO ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_ABO', 'Visible') IS NULL ALTER TABLE Blood_ABO ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_ABO set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_ABO set DataAddTime=getdate() where DataAddTime is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_ABO set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_ABO set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region 先删除添加的基础表及业务表的外键关系(18版本及19版本)
                updateSql = "  ";
                listSQL.Add(updateSql);
                //FK_BLOOD_BA_REFERENCE_BLOOD_BA
                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BLOOD_BA_REFERENCE_BLOOD_BA]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_BagOperationDtl]')) ALTER TABLE [dbo].[Blood_BagOperationDtl] DROP CONSTRAINT [FK_BLOOD_BA_REFERENCE_BLOOD_BA]; ";
                listSQL.Add(updateSql);
                //FK_Blood_TransItem_Blood_TransForm
                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_TransItem_Blood_TransForm]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_TransItem]')) ALTER TABLE [dbo].[Blood_TransItem] DROP CONSTRAINT [FK_Blood_TransItem_Blood_TransForm]; ";
                listSQL.Add(updateSql);
                //FK_Blood_TransItem_Blood_TransRecordTypeItem
                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_TransItem_Blood_TransRecordTypeItem]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_TransItem]')) ALTER TABLE [dbo].[Blood_TransItem] DROP CONSTRAINT [FK_Blood_TransItem_Blood_TransRecordTypeItem]; ";
                listSQL.Add(updateSql);
                //FK_BLOOD_TR_REFERENCE_BLOOD_TR
                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BLOOD_TR_REFERENCE_BLOOD_TR]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_TransRecordTypeItem]')) ALTER TABLE [dbo].[Blood_TransRecordTypeItem] DROP CONSTRAINT [FK_BLOOD_TR_REFERENCE_BLOOD_TR]; ";
                listSQL.Add(updateSql);

                //FK_Blood_BagOperation_Blood_BReqForm
                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_BagOperation_Blood_BReqForm]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_BagOperation]')) ALTER TABLE [dbo].[Blood_BagOperation] DROP CONSTRAINT [FK_Blood_BagOperation_Blood_BReqForm]; ";
                listSQL.Add(updateSql);

                //FK_Blood_BagOperationDtl_Blood_BagOperation
                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_BagOperationDtl_Blood_BagOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_BagOperationDtl]')) ALTER TABLE [dbo].[Blood_BagOperationDtl] DROP CONSTRAINT [FK_Blood_BagOperationDtl_Blood_BagOperation]; ";
                listSQL.Add(updateSql);

                //FK_Blood_TransForm_Blood_BReqForm
                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_TransForm_Blood_BReqForm]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_TransForm]')) ALTER TABLE [dbo].[Blood_TransForm] DROP CONSTRAINT [FK_Blood_TransForm_Blood_BReqForm]; ";
                listSQL.Add(updateSql);

                //FK_Blood_TransForm_Blood_Style
                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_TransForm_Blood_Style]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_TransForm]')) ALTER TABLE [dbo].[Blood_TransForm] DROP CONSTRAINT [FK_Blood_TransForm_Blood_Style]; ";
                listSQL.Add(updateSql);

                //FK_Blood_BagOperation_Blood_BReqForm
                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_BagOperation_Blood_BReqForm]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_BagOperation]')) ALTER TABLE [dbo].[Blood_BagOperation] DROP CONSTRAINT [FK_Blood_BagOperation_Blood_BReqForm]; ";
                listSQL.Add(updateSql);

                //FK_Blood_BagOperation_Blood_Style
                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_BagOperation_Blood_Style]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_BagOperation]')) ALTER TABLE [dbo].[Blood_BagOperation] DROP CONSTRAINT [FK_Blood_BagOperation_Blood_Style]; ";
                listSQL.Add(updateSql);

                //FK_Blood_BagOperationDtl_B_Dict
                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_BagOperationDtl_B_Dict]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_BagOperationDtl]')) ALTER TABLE [dbo].[Blood_BagOperationDtl] DROP CONSTRAINT [FK_Blood_BagOperationDtl_B_Dict]; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_TransRecordType(输血过程记录分类字典表)
                updateSql = " if exists(select 1 from dbo.sysreferences r join dbo.sysobjects o on (o.id = r.constid and o.type = 'F') where r.fkeyid = object_id('Blood_TransItem') and o.name = 'FK_BLOOD_TR_REFERENCE_BLOOD_TR') alter table Blood_TransItem drop constraint FK_BLOOD_TR_REFERENCE_BLOOD_TR; if exists (select 1 from dbo.sysreferences r join dbo.sysobjects o on (o.id = r.constid and o.type = 'F') where r.fkeyid = object_id('Blood_TransItem') and o.name = 'FK_BLOOD_TR_REFERENCE_BLOOD_TR') alter table Blood_TransItem drop constraint FK_BLOOD_TR_REFERENCE_BLOOD_TR; if exists (select 1 from dbo.sysreferences r join dbo.sysobjects o on (o.id = r.constid and o.type = 'F') where r.fkeyid = object_id('Blood_TransRecordTypeItem') and o.name = 'FK_BLOOD_TR_REFERENCE_BLOOD_TR') alter table Blood_TransRecordTypeItem drop constraint FK_BLOOD_TR_REFERENCE_BLOOD_TR; if exists (select 1 from sysobjects where id = object_id('Blood_TransRecordType') and type = 'U') drop table Blood_TransRecordType; create table Blood_TransRecordType ( LabID bigint null, TransRecordTypeID bigint not null, ContentTypeID int null, TransRecordType varchar(50) null, TypeCode varchar(50) null, DispOrder int null, IsVisible bit null, Memo ntext null, DataAddTime datetime null, DataUpdateTime datetime null, DataTimeStamp timestamp null, constraint PK_BLOOD_TRANSRECORDTYPE primary key (TransRecordTypeID)); ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_TransRecordTypeItem(输血过程记录明细字典表)
                updateSql = "  if exists(select 1 from dbo.sysreferences r join dbo.sysobjects o on (o.id = r.constid and o.type = 'F') where r.fkeyid = object_id('Blood_TransRecordTypeItem') and o.name = 'FK_BLOOD_TR_REFERENCE_BLOOD_TR') alter table Blood_TransRecordTypeItem drop constraint FK_BLOOD_TR_REFERENCE_BLOOD_TR; if exists (select 1 from sysobjects where id = object_id('Blood_TransRecordTypeItem') and type = 'U') drop table Blood_TransRecordTypeItem; create table Blood_TransRecordTypeItem ( LabID bigint null, TransRecordTypeItemID bigint not null, TransRecordTypeID bigint null, TransItemCode varchar(50) null, TransItemName varchar(50) null, SName varchar(80) null, Shortcode varchar(50) null, PinYinZiTou varchar(50) null, TransItemEditInfo ntext null, DispOrder int null, IsVisible bit null, DataAddTime datetime null, DataUpdateTime datetime null, DataTimeStamp timestamp null, constraint PK_BLOOD_TRANSRECORDTYPEITEM primary key (TransRecordTypeItemID)); ";
                listSQL.Add(updateSql);

                //输血过程记录明细字典表关联输血过程记录分类字典表
                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_BLOOD_TR_REFERENCE_BLOOD_TR]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_TransRecordTypeItem]')) ALTER TABLE [dbo].[Blood_TransRecordTypeItem] DROP CONSTRAINT [FK_BLOOD_TR_REFERENCE_BLOOD_TR]; ALTER TABLE [dbo].[Blood_TransRecordTypeItem] WITH CHECK ADD CONSTRAINT [FK_BLOOD_TR_REFERENCE_BLOOD_TR] FOREIGN KEY([TransRecordTypeID]) REFERENCES [dbo].[Blood_TransRecordType] ([TransRecordTypeID]); ALTER TABLE [dbo].[Blood_TransRecordTypeItem] CHECK CONSTRAINT [FK_BLOOD_TR_REFERENCE_BLOOD_TR]; ; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.18");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.18) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.19
            if (IsUpdateDataBase(oldVersion, "1.0.0.19"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_BagOperation(交接记录主单表)
                updateSql = " if exists(select 1 from dbo.sysreferences r join dbo.sysobjects o on (o.id = r.constid and o.type = 'F') where r.fkeyid = object_id('Blood_BagOperationDtl') and o.name = 'FK_BLOOD_BA_FK_BLOOD__BLOOD_BA') alter table Blood_BagOperationDtl drop constraint FK_BLOOD_BA_FK_BLOOD__BLOOD_BA; if exists (select 1 from sysobjects where id = object_id('Blood_BagOperation') and type = 'U') drop table Blood_BagOperation; create table Blood_BagOperation ( LabID bigint null, BagOperationID bigint not null, BagOperationNo varchar(20) null, BReqFormID nvarchar(40) null, BOutFormID varchar(20) null, BOutItemID varchar(20) null, BloodNo int null, BagOperTypeID bigint null, BagOperResultID bigint null, DeptID bigint null, DeptCName varchar(50) null, BBagCode varchar(20) null, PCode varchar(10) null, BagOperID bigint null, BagOper varchar(50) null, BagOperTime datetime null, CarrierID bigint null, Carrier varchar(50) null, IsVisible bit null, DataAddTime datetime null, DataUpdateTime datetime null, DataTimeStamp timestamp null, constraint PK_BLOOD_BAGOPERATION primary key (BagOperationID)); ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_BagOperationDtl(交接记录明细表)
                updateSql = " if exists(select 1 from dbo.sysreferences r join dbo.sysobjects o on (o.id = r.constid and o.type = 'F') where r.fkeyid = object_id('Blood_BagOperationDtl') and o.name = 'FK_BLOOD_BA_FK_BLOOD__BLOOD_BA') alter table Blood_BagOperationDtl drop constraint FK_BLOOD_BA_FK_BLOOD__BLOOD_BA; if exists (select 1 from sysobjects where id = object_id('Blood_BagOperationDtl') and type = 'U') drop table Blood_BagOperationDtl; create table Blood_BagOperationDtl ( LabID bigint null, BagOperationDtlID bigint not null, BagOperationID bigint null, DCId bigint null, BagOperResult varchar(500) null, DataAddTime datetime null, DataUpdateTime datetime null, DataTimeStamp timestamp null, constraint PK_BLOOD_BAGOPERATIONDTL primary key (BagOperationDtlID)); ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_TransForm(输血记录主单表)
                updateSql = "  if exists(select 1 from dbo.sysreferences r join dbo.sysobjects o on (o.id = r.constid and o.type = 'F') where r.fkeyid = object_id('Blood_TransItem') and o.name = 'FK_BLOOD_TR_FK_BLOOD__BLOOD_TR') alter table Blood_TransItem drop constraint FK_BLOOD_TR_FK_BLOOD__BLOOD_TR; if exists (select 1 from sysobjects where id = object_id('Blood_TransForm') and type = 'U') drop table Blood_TransForm; create table Blood_TransForm ( LabID bigint null, TransFormID bigint not null, TransFormNo varchar(20) null, BReqFormID nvarchar(40) null, BOutFormID varchar(20) null, BOutItemID varchar(20) null, BloodNo int null, BBagCode varchar(20) null, PCode varchar(10) null, BeforeCheckID1 bigint null, BeforeCheck1 varchar(50) null, BeforeCheckID2 bigint null, BeforeCheck2 varchar(50) null, TransBeginTime datetime null, TransCheckID1 bigint null, TransCheck1 varchar(50) null, TransCheckID2 bigint null, TransCheck2 varchar(50) null, TransEndTime datetime null, Visible bit null, DispOrder int null, DataAddTime datetime null, DataUpdateTime datetime null, DataTimeStamp timestamp null, constraint PK_BLOOD_TRANSFORM primary key (TransFormID)); ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_TransItem(输血记录明细表)
                updateSql = "  if exists(select 1 from dbo.sysreferences r join dbo.sysobjects o on (o.id = r.constid and o.type = 'F') where r.fkeyid = object_id('Blood_TransItem') and o.name = 'FK_BLOOD_TR_FK_BLOOD__BLOOD_TR') alter table Blood_TransItem drop constraint FK_BLOOD_TR_FK_BLOOD__BLOOD_TR; if exists (select 1 from dbo.sysreferences r join dbo.sysobjects o on (o.id = r.constid and o.type = 'F') where r.fkeyid = object_id('Blood_TransItem') and o.name = 'FK_BLOOD_TR_FK_BLOOD__BLOOD_TR') alter table Blood_TransItem drop constraint FK_BLOOD_TR_FK_BLOOD__BLOOD_TR; if exists (select 1 from sysobjects where id = object_id('Blood_TransItem') and type = 'U') drop table Blood_TransItem; create table Blood_TransItem ( LabID bigint null, TransItemID bigint not null, TransFormID bigint null, ContentTypeID int null, TransRecordTypeItemID bigint null, TransItemResult varchar(200) null, Visible bit null, DispOrder int null, DataAddTime datetime null, DataTimeStamp timestamp null, constraint PK_BLOOD_TRANSITEM primary key (TransItemID)); ";
                listSQL.Add(updateSql);

                #endregion

                #region 统一调整申请单号数据类型
                //申请主单表的申请单号数据类型: varchar(20) 其他的业务表部分是nvarchar(40)
                //决方案:BS将用到的业务表的申请单号的数据类型统一调整为nvarchar(40);

                updateSql = " IF COL_LENGTH('Blood_BOutForm', 'BReqFormID') IS NOT NULL ALTER TABLE Blood_BOutForm ALTER COLUMN BReqFormID nvarchar(40); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BPreForm', 'BReqFormID') IS NOT NULL ALTER TABLE Blood_BPreForm ALTER COLUMN BReqFormID nvarchar(40); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BPreItem', 'BReqFormID') IS NOT NULL ALTER TABLE Blood_BPreItem ALTER COLUMN BReqFormID nvarchar(40); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BagOperation', 'BReqFormID') IS NOT NULL ALTER TABLE Blood_BagOperation ALTER COLUMN BReqFormID nvarchar(40); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_TransForm', 'BReqFormID') IS NOT NULL ALTER TABLE Blood_TransForm ALTER COLUMN BReqFormID nvarchar(40); ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.19");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.19) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.20
            if (IsUpdateDataBase(oldVersion, "1.0.0.20"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region 护士站外键关联
                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_TransForm_Blood_Style]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_TransForm]')) ALTER TABLE [dbo].[Blood_TransForm] DROP CONSTRAINT [FK_Blood_TransForm_Blood_Style]; ALTER TABLE [dbo].[Blood_TransForm] WITH CHECK ADD CONSTRAINT [FK_Blood_TransForm_Blood_Style] FOREIGN KEY([BloodNo]) REFERENCES [dbo].[blood_style] ([BloodNo]); ALTER TABLE [dbo].[Blood_TransForm] CHECK CONSTRAINT [FK_Blood_TransForm_Blood_Style]; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_TransForm_Blood_BReqForm]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_TransForm]')) ALTER TABLE [dbo].[Blood_TransForm] DROP CONSTRAINT [FK_Blood_TransForm_Blood_BReqForm]; ALTER TABLE [dbo].[Blood_TransForm] WITH CHECK ADD CONSTRAINT [FK_Blood_TransForm_Blood_BReqForm] FOREIGN KEY([BReqFormID]) REFERENCES [dbo].[Blood_BReqForm] ([BReqFormID]); ALTER TABLE [dbo].[Blood_TransForm] CHECK CONSTRAINT [FK_Blood_TransForm_Blood_BReqForm]; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_BagOperation_Blood_Style]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_BagOperation]')) ALTER TABLE [dbo].[Blood_BagOperation] DROP CONSTRAINT [FK_Blood_BagOperation_Blood_Style]; ALTER TABLE [dbo].[Blood_BagOperation] WITH CHECK ADD CONSTRAINT [FK_Blood_BagOperation_Blood_Style] FOREIGN KEY([BloodNo]) REFERENCES [dbo].[blood_style] ([BloodNo]); ALTER TABLE [dbo].[Blood_BagOperation] CHECK CONSTRAINT [FK_Blood_BagOperation_Blood_Style]; ";
                listSQL.Add(updateSql);

                updateSql = "  IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_BagOperationDtl_B_Dict]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_BagOperationDtl]')) ALTER TABLE [dbo].[Blood_BagOperationDtl] DROP CONSTRAINT [FK_Blood_BagOperationDtl_B_Dict]; ALTER TABLE [dbo].[Blood_BagOperationDtl] WITH CHECK ADD CONSTRAINT [FK_Blood_BagOperationDtl_B_Dict] FOREIGN KEY([DCId]) REFERENCES [dbo].[B_Dict] ([DID]); ALTER TABLE [dbo].[Blood_BagOperationDtl] CHECK CONSTRAINT [FK_Blood_BagOperationDtl_B_Dict];  ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.20");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.20) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.21
            if (IsUpdateDataBase(oldVersion, "1.0.0.21"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region B_DictType
                //血液外观
                updateSql = " IF NOT EXISTS(SELECT * FROM B_DictType WHERE [DCId] = 5253700719899753669) INSERT [B_DictType]([LabID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime],[DictTypeCode]) VALUES ( 0,5253700719899753669,N'血液外观',300,N'血液制品交接的血液外观',1,N'2020-02-12 17:37:24',N'BloodAppearance'); ";
                listSQL.Add(updateSql);
                //血液完整性
                updateSql = " IF NOT EXISTS(SELECT * FROM B_DictType WHERE [DCId] = 5579397214888897985) INSERT [B_DictType]([LabID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime],[DictTypeCode]) VALUES ( 0,5579397214888897985,N'血液完整性',400,N'血液制品交接',1,N'2020-02-12 17:38:34',N'BloodIntegrity'); ";
                listSQL.Add(updateSql);
                #endregion

                #region B_Dict
                //血液外观
                updateSql = " IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 4713710510004576645) INSERT [B_Dict]([LabID],[DID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime]) VALUES ( 0,5604792992372522814,5253700719899753669,N'正常',10,N'正常',1,N'2020-02-12 17:39:12'); ";
                listSQL.Add(updateSql);
                //血液外观
                updateSql = " IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 5743312823943732018) INSERT [B_Dict]([LabID],[DID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime]) VALUES ( 0,5743312823943732018,5253700719899753669,N'不正常',20,N'不正常',1,N'2020-02-12 17:39:27'); ";
                listSQL.Add(updateSql);

                //血液完整性
                updateSql = " IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 5340449447509386973) INSERT [B_Dict]([LabID],[DID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime]) VALUES ( 0,5340449447509386973,5579397214888897985,N'无破损',10,N'无破损',1,N'2020-02-12 17:39:45'); ";
                listSQL.Add(updateSql);
                //血液完整性
                updateSql = " IF NOT EXISTS(SELECT * FROM B_Dict WHERE [DID] = 4729701409042549644) INSERT [B_Dict]([LabID],[DID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime]) VALUES ( 0,4729701409042549644,5579397214888897985,N'有渗漏',20,N'有渗漏',1,N'2020-02-12 17:40:00'); ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_TransRecordType
                //将TransRecordType更名为CName
                updateSql = " IF COL_LENGTH('Blood_TransRecordType', 'TransRecordType') IS NOT NULL exec sp_rename 'Blood_TransRecordType.TransRecordType','CName'; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.21");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.21) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.22
            if (IsUpdateDataBase(oldVersion, "1.0.0.22"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_BInForm(入库主单)
                updateSql = " IF COL_LENGTH('Blood_BInForm', 'LabID') IS NULL ALTER TABLE Blood_BInForm ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BInForm', 'DispOrder') IS NULL ALTER TABLE Blood_BInForm ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BInForm', 'DataAddTime') IS NULL ALTER TABLE Blood_BInForm ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BInForm', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BInForm ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BInForm', 'Visible') IS NULL ALTER TABLE Blood_BInForm ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BInForm set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BInForm set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BInForm set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BInItemState(入库状态)
                updateSql = " IF COL_LENGTH('Blood_BInItemState', 'LabID') IS NULL ALTER TABLE Blood_BInItemState ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BInItemState', 'DispOrder') IS NULL ALTER TABLE Blood_BInItemState ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BInItemState', 'DataAddTime') IS NULL ALTER TABLE Blood_BInItemState ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BInItemState', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BInItemState ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BInItemState', 'Visible') IS NULL ALTER TABLE Blood_BInItemState ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BInItemState set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BInItemState set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BInItemState set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_Recei(送达主单)
                updateSql = " IF COL_LENGTH('Blood_Recei', 'LabID') IS NULL ALTER TABLE Blood_Recei ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'DispOrder') IS NULL ALTER TABLE Blood_Recei ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'DataAddTime') IS NULL ALTER TABLE Blood_Recei ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'DataTimeStamp') IS NULL ALTER TABLE Blood_Recei ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'Visible') IS NULL ALTER TABLE Blood_Recei ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_Recei set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_Recei set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_Recei set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_ReceiItem(送达明细)
                updateSql = " IF COL_LENGTH('Blood_ReceiItem', 'LabID') IS NULL ALTER TABLE Blood_ReceiItem ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_ReceiItem', 'DispOrder') IS NULL ALTER TABLE Blood_ReceiItem ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_ReceiItem', 'DataAddTime') IS NULL ALTER TABLE Blood_ReceiItem ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_ReceiItem', 'DataTimeStamp') IS NULL ALTER TABLE Blood_ReceiItem ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_ReceiItem', 'Visible') IS NULL ALTER TABLE Blood_ReceiItem ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_ReceiItem set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_ReceiItem set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_ReceiItem set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_refuse(拒收字典)
                updateSql = " IF COL_LENGTH('Blood_refuse', 'LabID') IS NULL ALTER TABLE Blood_refuse ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_refuse', 'DispOrder') IS NULL ALTER TABLE Blood_refuse ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_refuse', 'DataAddTime') IS NULL ALTER TABLE Blood_refuse ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_refuse', 'DataTimeStamp') IS NULL ALTER TABLE Blood_refuse ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_refuse', 'Visible') IS NULL ALTER TABLE Blood_refuse ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_refuse set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_refuse set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_refuse set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_refuseDispose(拒收处理)
                updateSql = " IF COL_LENGTH('Blood_refuseDispose', 'LabID') IS NULL ALTER TABLE Blood_refuseDispose ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_refuseDispose', 'DispOrder') IS NULL ALTER TABLE Blood_refuseDispose ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_refuseDispose', 'DataAddTime') IS NULL ALTER TABLE Blood_refuseDispose ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_refuseDispose', 'DataTimeStamp') IS NULL ALTER TABLE Blood_refuseDispose ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_refuseDispose', 'Visible') IS NULL ALTER TABLE Blood_refuseDispose ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_refuseDispose set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_refuseDispose set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_refuseDispose set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BagABOCheck(血袋血型复核)
                updateSql = " IF COL_LENGTH('Blood_BagABOCheck', 'LabID') IS NULL ALTER TABLE Blood_BagABOCheck ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BagABOCheck', 'DispOrder') IS NULL ALTER TABLE Blood_BagABOCheck ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BagABOCheck', 'DataAddTime') IS NULL ALTER TABLE Blood_BagABOCheck ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BagABOCheck', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BagABOCheck ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BagABOCheck', 'Visible') IS NULL ALTER TABLE Blood_BagABOCheck ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BagABOCheck set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BagABOCheck set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BagABOCheck set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BagABOCheck_LisItem(血袋血型复核项目结果)
                updateSql = " IF COL_LENGTH('Blood_BagABOCheck_LisItem', 'LabID') IS NULL ALTER TABLE Blood_BagABOCheck_LisItem ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BagABOCheck_LisItem', 'DispOrder') IS NULL ALTER TABLE Blood_BagABOCheck_LisItem ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BagABOCheck_LisItem', 'DataAddTime') IS NULL ALTER TABLE Blood_BagABOCheck_LisItem ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BagABOCheck_LisItem', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BagABOCheck_LisItem ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BagABOCheck_LisItem', 'Visible') IS NULL ALTER TABLE Blood_BagABOCheck_LisItem ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BagABOCheck_LisItem set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BagABOCheck_LisItem set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BagABOCheck_LisItem set DispOrder=0 where DispOrder is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_TransRecordType(输血临床处理措施)
                //输血临床处理措施
                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 4780816011182130210) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES( 0,4780816011182130210,3,N'输血临床处理措施',N'ClinicalMeasures',100000,1,N'ClinicalMeasures',N'2020-02-13 10:54:00'); ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_TransRecordType(输血记录项)
                //输血前
                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 4712222272559814781) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,4712222272559814781,1,N'输血前',N'1001',1001,1,N'输血记录项的输血前分类',N'2020-02-13 10:52:32'); ";
                listSQL.Add(updateSql);

                //输血15分钟
                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 5510188224824882839) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,5510188224824882839,1,N'输血15分钟',N'1002',1002,1,N'输血记录项的输血15分钟分类',N'2020-02-13 11:00:07'); ";
                listSQL.Add(updateSql);

                //输血60分钟
                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 5109775278160421817) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,5109775278160421817,1,N'输血60分钟',N'1003',1003,1,N'输血记录项的输血60分钟分类',N'2020-02-13 11:01:04'); ";
                listSQL.Add(updateSql);

                //输血2小时
                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 5660775821937029735) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,5660775821937029735,1,N'输血2小时',N'1004',1004,1,N'输血记录项的输血2小时分类',N'2020-02-13 11:01:38'); ";
                listSQL.Add(updateSql);

                //输血3小时
                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 4959979330910815717) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,4959979330910815717,1,N'输血3小时',N'1005',1005,1,N'输血记录项的输血3小时分类',N'2020-02-13 11:02:06'); ";
                listSQL.Add(updateSql);

                //输血4小时
                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 4748574105933710009) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,4748574105933710009,1,N'输血4小时',N'1006',1006,1,N'输血记录项的输血4小时分类',N'2020-02-13 11:02:39'); ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_TransRecordType(输血不良反应)
                //输血前
                //updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 20001) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,20001,2,N'输血前',N'20001',20001,1,N'输血不良反应的输血前分类',N'2020-02-13 10:52:32'); ";
                //listSQL.Add(updateSql);

                //输血15分钟
                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 20002) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,20002,2,N'输血15分钟',N'20002',20002,1,N'输血不良反应的输血15分钟分类',N'2020-02-13 11:00:07'); ";
                listSQL.Add(updateSql);

                //输血60分钟
                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 20003) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,20003,2,N'输血60分钟',N'20003',20003,1,N'输血不良反应的输血60分钟分类',N'2020-02-13 11:01:04'); ";
                listSQL.Add(updateSql);

                //输血2小时
                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 20004) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,20004,2,N'输血2小时',N'20004',20004,1,N'输血不良反应的输血2小时分类',N'2020-02-13 11:01:38'); ";
                listSQL.Add(updateSql);

                //输血3小时
                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 20005) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,20005,2,N'输血3小时',N'20005',20005,1,N'输血不良反应的输血3小时分类',N'2020-02-13 11:02:06'); ";
                listSQL.Add(updateSql);

                //输血4小时
                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 20006) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,20006,2,N'输血4小时',N'20006',20006,1,N'输血不良反应的输血4小时分类',N'2020-02-13 11:02:39'); ";
                listSQL.Add(updateSql);

                #endregion

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
                string updateSql = "";
                List<string> listSQL = new List<string>();
                #region 统一调整申请单号数据类型
                //申请主单表的申请单号数据类型: varchar(20) 其他的业务表部分是nvarchar(40)
                //决方案:BS将用到的业务表的申请单号的数据类型统一调整为nvarchar(40);

                updateSql = " IF COL_LENGTH('Blood_BOutForm', 'BReqFormID') IS NOT NULL ALTER TABLE Blood_BOutForm ALTER COLUMN BReqFormID nvarchar(40); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BPreForm', 'BReqFormID') IS NOT NULL ALTER TABLE Blood_BPreForm ALTER COLUMN BReqFormID nvarchar(40); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BPreItem', 'BReqFormID') IS NOT NULL ALTER TABLE Blood_BPreItem ALTER COLUMN BReqFormID nvarchar(40); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BagOperation', 'BReqFormID') IS NOT NULL ALTER TABLE Blood_BagOperation ALTER COLUMN BReqFormID nvarchar(40); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_TransForm', 'BReqFormID') IS NOT NULL ALTER TABLE Blood_TransForm ALTER COLUMN BReqFormID nvarchar(40); ";
                listSQL.Add(updateSql);

                #endregion

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
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_BagABOCheck_LisItem
                updateSql = " if not Exists(Select * from SysColumns where [Name]='ID' and ID =(Select [ID] from SysObjects where Name = 'Blood_BagABOCheck_LisItem')) ALTER TABLE Blood_BagABOCheck_LisItem ADD ID BIGINT IDENTITY NOT NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Blood_BagABOCheck_LisItem]') AND name = N'PK_Blood_BagABOCheck_LisItem') ALTER TABLE [dbo].[Blood_BagABOCheck_LisItem] DROP CONSTRAINT [PK_Blood_BagABOCheck_LisItem]; ALTER TABLE [dbo].[Blood_BagABOCheck_LisItem] ADD CONSTRAINT [PK_Blood_BagABOCheck_LisItem] PRIMARY KEY CLUSTERED ( [ID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                #region blood_BagProcessType
                updateSql = "  if not exists (select 1 from sysobjects where id = object_id('blood_BagProcessType') and type = 'U') create table blood_BagProcessType ( LabID bigint null, PTNo nvarchar(20) not null, CName nvarchar(20) null, ShortCode nvarchar(20) null, HisOrderCode nvarchar(20) null, Visible nvarchar(10) null, Bcno nvarchar(20) null, ChargeItemNo nvarchar(20) null, DispOrder int null, DataAddTime datetime null, DataTimeStamp timestamp null, constraint PK_BLOOD_BAGPROCESSTYPE primary key (PTNo)); ";
                listSQL.Add(updateSql);
                #endregion

                #region blood_bagProcessTypeQry
                updateSql = " if not exists(select 1 from sysobjects where id = object_id('blood_bagProcessTypeQry') and type = 'U') create table blood_bagProcessTypeQry ( LabID bigint null,[ID] [bigint] IDENTITY(1,1) NOT NULL, PTNo nvarchar(20) null, bloodno nvarchar(20) null, CName nvarchar(20) null, DispOrder int null, DataAddTime datetime null, DataTimeStamp timestamp null, constraint PK_BLOOD_BAGPROCESSTYPEQRY primary key (ID)); ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_BagProcess
                updateSql = " if not exists(select 1 from sysobjects where id = object_id('Blood_BagProcess') and type = 'U')  create table Blood_BagProcess ( LabID bigint  null, BPID bigint not null, BPreFormID NVARCHAR (50) null, BPreItemID NVARCHAR (50) null, BBagCode NVARCHAR (20) null, PCode NVARCHAR (20) null, B3Code NVARCHAR (50) null, PTNo NVARCHAR (20) null, BPflag int null, DispOrder int null, DataAddTime datetime null, DataTimeStamp timestamp null, constraint PK_BLOOD_BAGPROCESS primary key (BPID)); ";
                listSQL.Add(updateSql);

                #endregion

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
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region 系统运行参数
                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5000278512090896900) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,5000278512090896900,N'实验室数据升级版本',N'系统',N'CONFIG',N'BL-ULAB-DATA-0007',N'1.0.0.1',N'实验室数据升级版本',20,1,0,N'2020-02-15 10:16:52'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5081884485807967905) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,5081884485807967905,N'登录后升级数据库',N'系统',N'CONFIG',N'BL-SYSE-UDAL-0001',N'1',N'1:是;0:否;',10,1,1,N'2020-02-15 10:13:44'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5085688375696300791) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,5085688375696300791,N'列表默认分页记录数',N'UI',N'CONFIG',N'BL-LRMP-UIPA-0003',N'10',N'列表默认分页记录数',10,1,1,N'2020-02-15 10:15:29'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5101233254823494116) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,5101233254823494116,N'血袋接收是否需要护工完成取血确认',N'护士站',N'CONFIG',N'BL-BBAG-ISNC-0010',N'0',N'1:是;0:否;',10,1,1,N'2020-02-15 10:17:25'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5214493501844260801) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,5214493501844260801,N'集成平台服务访问URL',N'集成平台',N'CONFIG',N'BL-SYSE-LURL-0002',N'http://localhost/ZhiFang.LabInformationIntegratePlatform',N'配置调用集成平台服务的URL',10,1,1,N'2020-02-15 10:14:38'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5524339378542032882) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,5524339378542032882,N'启用用户UI配置',N'UI',N'CONFIG',N'BL-EUSE-UICF-0008',N'0',N'1:是;0:否;',1,1,1,N'2020-02-15 10:15:59'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5691564273202278074) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[DataUpdateTime]) VALUES ( 0,5691564273202278074,N'输血申请单当前流水号',N'医生站',N'CONFIG',N'BL-BRQF-CURN-0009',N'14',N'申请单号生成规则:年月日+4位顺序流水号(按天重新生成)',11,1,0,N'2019-10-08 16:06:52',N'2020-02-06 16:10:04'); ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_TransRecordTypeItem
                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 4703071337826980944) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,4703071337826980944,4780816011182130210,N'10',N'立即停止输血,保持静脉通路',N'10',10,1,N'2020-02-14 17:19:27'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 5215863475154392192) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,5215863475154392192,4780816011182130210,N'20',N'对症处理',N'30',30,1,N'2020-02-14 17:20:27'); ";
                listSQL.Add(updateSql);
                #endregion

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
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_BOutItem
                updateSql = " if Exists(Select * from SysColumns where [Name]='BloodNo' and ID =(Select [ID] from SysObjects where Name = 'Blood_BOutItem')) alter table Blood_BOutItem ALTER COLUMN BloodNo int; ";
                listSQL.Add(updateSql);

                updateSql = " if Exists(Select * from SysColumns where [Name]='BloodABONo' and ID =(Select [ID] from SysObjects where Name = 'Blood_BOutItem')) alter table Blood_BOutItem ALTER COLUMN BloodABONo nvarchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " if Exists(Select * from SysColumns where [Name]='BloodUnitNo' and ID =(Select [ID] from SysObjects where Name = 'Blood_BOutItem')) alter table Blood_BOutItem ALTER COLUMN BloodUnitNo int; ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_BInItem
                updateSql = " if Exists(Select * from SysColumns where [Name]='BloodABONo' and ID =(Select [ID] from SysObjects where Name = 'Blood_BInItem')) alter table Blood_BInItem ALTER COLUMN BloodABONo nvarchar(20); ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_TransRecordType
                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 5485690321920592783) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,5485690321920592783,4,N'不良反应选择项',N'AdverseReactionOptions',30000,1,N'不良反应选择项',N'2020-02-18 10:41:43'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 5118439131802773757) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,5118439131802773757,5,N'临床处理结果',N'ClinicalResults',40000,1,N'临床处理结果',N'2020-02-18 10:54:59'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordType WHERE [TransRecordTypeID] = 5065045818603329234) INSERT [Blood_TransRecordType]([LabID],[TransRecordTypeID],[ContentTypeID],[CName],[TypeCode],[DispOrder],[IsVisible],[Memo],[DataAddTime]) VALUES ( 0,5065045818603329234,6,N'临床处理结果描述',N'ClinicalResultsDesc',50000,1,N'临床处理结果描述',N'2020-02-18 10:55:23'); ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_TransRecordType
                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 4803836600047891481) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,4803836600047891481,5485690321920592783,N'10',N'发热',N'10',10,1,N'2020-02-18 10:43:29'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 5531342248897198408) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,5531342248897198408,5485690321920592783,N'20',N'恶心呕吐',N'20',20,1,N'2020-02-18 10:43:50'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 5555439810679451060) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,5555439810679451060,5485690321920592783,N'30',N'血压升高',N'30',30,1,N'2020-02-18 10:45:08'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 5098846963842011802) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,5098846963842011802,5118439131802773757,N'10',N'对症处理',N'10',10,1,N'2020-02-18 10:57:50');";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 5321905214369573002) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,5321905214369573002,5065045818603329234,N'10',N'临床处理结果描述',N'10',10,1,N'2020-02-18 10:58:19'); ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.26");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.26) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.27
            if (IsUpdateDataBase(oldVersion, "1.0.0.27"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region 护士站外键关联
                updateSql = "  IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_BagOperation_Blood_BReqForm]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_BagOperation]')) ALTER TABLE [dbo].[Blood_BagOperation] DROP CONSTRAINT [FK_Blood_BagOperation_Blood_BReqForm]; ALTER TABLE [dbo].[Blood_BagOperation] WITH CHECK ADD CONSTRAINT [FK_Blood_BagOperation_Blood_BReqForm] FOREIGN KEY([BReqFormID]) REFERENCES [dbo].[Blood_BReqForm] ([BReqFormID]); ALTER TABLE [dbo].[Blood_BagOperation] CHECK CONSTRAINT [FK_Blood_BagOperation_Blood_BReqForm]; ";
                listSQL.Add(updateSql);

                updateSql = "  IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_BagOperationDtl_Blood_BagOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_BagOperationDtl]')) ALTER TABLE [dbo].[Blood_BagOperationDtl] DROP CONSTRAINT [FK_Blood_BagOperationDtl_Blood_BagOperation]; ALTER TABLE [dbo].[Blood_BagOperationDtl] WITH CHECK ADD CONSTRAINT [FK_Blood_BagOperationDtl_Blood_BagOperation] FOREIGN KEY([BagOperationID]) REFERENCES [dbo].[Blood_BagOperation] ([BagOperationID]); ALTER TABLE [dbo].[Blood_BagOperationDtl] CHECK CONSTRAINT [FK_Blood_BagOperationDtl_Blood_BagOperation]; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_TransItem_Blood_TransForm]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_TransItem]')) ALTER TABLE [dbo].[Blood_TransItem] DROP CONSTRAINT [FK_Blood_TransItem_Blood_TransForm]; ALTER TABLE [dbo].[Blood_TransItem] WITH CHECK ADD CONSTRAINT [FK_Blood_TransItem_Blood_TransForm] FOREIGN KEY([TransFormID]) REFERENCES [dbo].[Blood_TransForm] ([TransFormID]); ALTER TABLE [dbo].[Blood_TransItem] CHECK CONSTRAINT [FK_Blood_TransItem_Blood_TransForm]; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Blood_TransItem_Blood_TransRecordTypeItem]') AND parent_object_id = OBJECT_ID(N'[dbo].[Blood_TransItem]')) ALTER TABLE [dbo].[Blood_TransItem] DROP CONSTRAINT [FK_Blood_TransItem_Blood_TransRecordTypeItem]; ALTER TABLE [dbo].[Blood_TransItem] WITH CHECK ADD CONSTRAINT [FK_Blood_TransItem_Blood_TransRecordTypeItem] FOREIGN KEY([TransRecordTypeItemID]) REFERENCES [dbo].[Blood_TransRecordTypeItem] ([TransRecordTypeItemID]); ALTER TABLE [dbo].[Blood_TransItem] CHECK CONSTRAINT [FK_Blood_TransItem_Blood_TransRecordTypeItem]; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.27");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.27) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.28
            if (IsUpdateDataBase(oldVersion, "1.0.0.28"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_TransForm
                updateSql = " IF COL_LENGTH('Blood_TransForm', 'HasAdverseReactions') IS NULL ALTER TABLE Blood_TransForm ADD HasAdverseReactions bit; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_TransForm', 'AdverseReactionsTime') IS NULL ALTER TABLE Blood_TransForm ADD AdverseReactionsTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_TransForm', 'AdverseReactionsHP') IS NULL ALTER TABLE Blood_TransForm ADD AdverseReactionsHP float; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BOutItem
                updateSql = " if Exists(Select * from SysColumns where [Name]='BloodUnitNo' and ID =(Select [ID] from SysObjects where Name = 'Blood_BOutItem')) alter table Blood_BOutItem ALTER COLUMN BloodUnitNo int; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.28");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.28) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.29
            if (IsUpdateDataBase(oldVersion, "1.0.0.29"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_BOutItem
                updateSql = " if Exists(Select * from SysColumns where [Name]='BReqItemID' and ID =(Select [ID] from SysObjects where Name = 'Blood_BOutItem')) alter table Blood_BOutItem ALTER COLUMN BReqItemID bigint; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BPreForm
                updateSql = " if Exists(Select * from SysColumns where [Name]='BReqItemID' and ID =(Select [ID] from SysObjects where Name = 'Blood_BPreForm')) alter table Blood_BPreForm ALTER COLUMN BReqItemID bigint; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_AnesthesiaMsg
                updateSql = " if Exists(Select * from SysColumns where [Name]='BReqItemID' and ID =(Select [ID] from SysObjects where Name = 'Blood_AnesthesiaMsg')) alter table Blood_AnesthesiaMsg ALTER COLUMN BReqItemID bigint; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.29");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.29) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.30
            if (IsUpdateDataBase(oldVersion, "1.0.0.30"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_TransItem
                updateSql = " IF COL_LENGTH('Blood_TransItem', 'NumberItemResult') IS NULL ALTER TABLE Blood_TransItem ADD NumberItemResult float; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BOutForm
                updateSql = " update Blood_BOutForm set ConfirmCompletion=1 where ConfirmCompletion is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BOutForm set HandoverCompletion=1 where HandoverCompletion is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BOutForm set CourseCompletion=1 where CourseCompletion is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BOutForm set RecoverCompletion=1 where RecoverCompletion is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BOutItem
                updateSql = " update Blood_BOutItem set ConfirmCompletion=1 where ConfirmCompletion is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BOutItem set HandoverCompletion=1 where HandoverCompletion is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BOutItem set CourseCompletion=1 where CourseCompletion is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BOutItem set RecoverCompletion=1 where RecoverCompletion is null; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.30");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.30) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.31
            if (IsUpdateDataBase(oldVersion, "1.0.0.31"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_TransItem
                updateSql = " IF COL_LENGTH('Blood_TransItem', 'TransRecordTypeID') IS NULL ALTER TABLE Blood_TransItem ADD TransRecordTypeID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE Blood_TransItem ALTER COLUMN [LabID] bigint NULL; ";
                listSQL.Add(updateSql);
                #endregion

                #region blood_bagProcessTypeQry
                updateSql = " if not Exists(Select * from SysColumns where [Name]='ID' and ID =(Select [ID] from SysObjects where Name = 'blood_bagProcessTypeQry')) ALTER TABLE blood_bagProcessTypeQry ADD ID BIGINT IDENTITY NOT NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('blood_bagProcessTypeQry', 'LabID') IS NULL ALTER TABLE blood_bagProcessTypeQry ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE blood_bagProcessTypeQry ALTER COLUMN [LabID] bigint NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('blood_bagProcessTypeQry', 'DataAddTime') IS NULL ALTER TABLE blood_bagProcessTypeQry ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('blood_bagProcessTypeQry', 'DataTimeStamp') IS NULL ALTER TABLE blood_bagProcessTypeQry ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " Update blood_bagProcessTypeQry set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BagABOCheck_LisItem
                updateSql = " if not Exists(Select * from SysColumns where [Name]='ID' and ID =(Select [ID] from SysObjects where Name = 'Blood_BagABOCheck_LisItem')) ALTER TABLE Blood_BagABOCheck_LisItem ADD ID BIGINT IDENTITY NOT NULL; ";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE Blood_BagABOCheck_LisItem ALTER COLUMN [LabID] bigint NULL; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BagProcess
                updateSql = " IF COL_LENGTH('Blood_BagProcess', 'LabID') IS NULL ALTER TABLE Blood_BagProcess ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE Blood_BagProcess ALTER COLUMN [LabID] bigint NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BagProcess', 'DataAddTime') IS NULL ALTER TABLE Blood_BagProcess ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BagProcess', 'DataTimeStamp') IS NULL ALTER TABLE Blood_BagProcess ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " Update Blood_BagProcess set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region blood_BagProcessType

                updateSql = " IF COL_LENGTH('blood_BagProcessType', 'LabID') IS NULL ALTER TABLE blood_BagProcessType ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE blood_BagProcessType ALTER COLUMN [LabID] bigint NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('blood_BagProcessType', 'DataAddTime') IS NULL ALTER TABLE blood_BagProcessType ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('blood_BagProcessType', 'DataTimeStamp') IS NULL ALTER TABLE blood_BagProcessType ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " Update blood_BagProcessType set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                #endregion

                #region B_Parameter
                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5214493501844260811) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,5214493501844260811,N'CS服务访问URL',N'CS接口',N'CONFIG',N'BL-SYSE-CSRL-0011',N'http://localhost',N'配置调用CS服务的URL',11,1,1,N'2020-03-16 10:14:38'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5214493501844260900) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,5214493501844260900,N'是否输血过程记录登记后才能血袋回收登记',N'护士站',N'CONFIG',N'BL-BBBO-BBRR-0012',N'1',N'1:是;0:否;',12,1,1,N'2020-03-19 10:14:38'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5214493501844260901) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,5214493501844260901,N'更新输血过程登记时是否添加操作记录',N'护士站',N'CONFIG',N'BL-BBBO-BBTF-0013',N'1',N'1:是;0:否;',13,1,1,N'2020-03-21 12:01:38'); ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_TransOperation
                updateSql = " if exists(select 1 from sysobjects where id = object_id('Blood_TransOperation') and type = 'U') drop table Blood_TransOperation; create table Blood_TransOperation ( LabID bigint null, OperationID bigint not null, BOutItemID varchar(20) null, TransFormID bigint null, BloodNo int null, ContentTypeID int null, TransRecordTypeID bigint null, BusinessCode varchar(40) null, Memo varchar(5000) null, DispOrder int null, IsUse bit null, CreatorID bigint null, CreatorName varchar(50) null, DataAddTime datetime null, DataTimeStamp timestamp null, constraint PK_BLOOD_TRANSOPERATION primary key (OperationID)); ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.31");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.31) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.32
            if (IsUpdateDataBase(oldVersion, "1.0.0.32"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

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

                #region B_Parameter
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

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.32");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.32) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.33
            if (IsUpdateDataBase(oldVersion, "1.0.0.33"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region PUser
                updateSql = " IF COL_LENGTH('PUser', 'DoctorNo') IS NULL ALTER TABLE PUser ADD DoctorNo int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('PUser', 'DispOrder') IS NULL ALTER TABLE PUser ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " update PUser set DispOrder=UserNo; ";
                listSQL.Add(updateSql);
                #endregion

                #region Doctor
                updateSql = " IF COL_LENGTH('Doctor', 'DispOrder') IS NULL ALTER TABLE Doctor ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Doctor', 'DataAddTime') IS NULL ALTER TABLE Doctor ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " update Doctor set DispOrder=DoctorNo; ";
                listSQL.Add(updateSql);
                #endregion

                #region Department
                updateSql = " update Department set DispOrder=DeptNo; ";
                listSQL.Add(updateSql);
                #endregion

                #region DepartmentUser
                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DepartmentUser]') AND type in (N'U')) CREATE TABLE [dbo].[DepartmentUser]( [LabID] [bigint] NULL, [DeptEmpID] [bigint] NOT NULL, [DeptNo] [int] NULL, [UserNo] [int] NULL, [IsUse] [bit] NULL, [DispOrder] [int] NULL, [DataAddTime] [datetime] NULL, [DataUpdateTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_DepartmentUser] PRIMARY KEY CLUSTERED ( [DeptEmpID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY]; ";
                listSQL.Add(updateSql);

                #endregion

                #region 模板信息表                
                updateSql = "IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[B_Template]') AND type in(N'U')) CREATE TABLE [dbo].[B_Template]( [LabID] [bigint] NULL, [TemplateID] [bigint] NOT NULL, [CName] [varchar](100) NULL, [TypeID] [bigint] NULL, [TypeName] [varchar](50) NULL, [FilePath] [varchar](500) NULL, [FileExt] [varchar](50) NULL, [ContentType] [varchar](100) NULL, [FileSize] [float] NULL, [SName] [varchar](80) NULL, [Shortcode] [varchar](80) NULL, [PinYinZiTou] [varchar](50) NULL, [DispOrder] [int] NULL, [Comment] [ntext] NULL, [IsUse] [bit] NULL, [DataAddTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, [IsDefault] [bit] NULL, [FileName] [varchar](100) NULL, [ExcelRuleInfo] [ntext] NULL, [TemplateType] [varchar](40) NULL, CONSTRAINT [PK_B_TEMPLATE] PRIMARY KEY CLUSTERED( [TemplateID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.33");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.33) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.34
            if (IsUpdateDataBase(oldVersion, "1.0.0.34"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_BOutItem
                updateSql = " IF COL_LENGTH('Blood_BOutItem', 'toHisFlag') IS NULL ALTER TABLE Blood_BOutItem ADD toHisFlag int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutItem', 'OrderFlag') IS NULL ALTER TABLE Blood_BOutItem ADD OrderFlag nvarchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutItem', 'BloodAdverseCheckNo') IS NULL ALTER TABLE Blood_BOutItem ADD BloodAdverseCheckNo nvarchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutItem', 'BloodAdverseCheckName') IS NULL ALTER TABLE Blood_BOutItem ADD BloodAdverseCheckName nvarchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutItem', 'BloodAdverseCheckTime') IS NULL ALTER TABLE Blood_BOutItem ADD BloodAdverseCheckTime nvarchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutItem', 'Output_no') IS NULL ALTER TABLE Blood_BOutItem ADD Output_no nvarchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutItem', 'BloodAdverseDemo') IS NULL ALTER TABLE Blood_BOutItem ADD BloodAdverseDemo nvarchar(500); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutItem', 'BnrNo') IS NULL ALTER TABLE Blood_BOutItem ADD BnrNo nvarchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutItem', 'ScanID') IS NULL ALTER TABLE Blood_BOutItem ADD ScanID nvarchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutItem', 'ScanTime') IS NULL ALTER TABLE Blood_BOutItem ADD ScanTime nvarchar(20); ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_TransRecordTypeItem
                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 1100) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,1100,4712222272559814781,N'temperature',N'体温',N'体温',N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"°C\",\"ItemDataSet\":\"\"}',10,1,N'2020-02-14 14:54:56'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 1200) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,1200,4712222272559814781,N'bloodpressure',N'血压',N'血压',N'{\"ItemXType\":\"textfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"mmhg\",\"ItemDataSet\":\"\"}',20,1,N'2020-02-14 15:22:22'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 1300) INSERT [Blood_TransRecordTypeItem] ([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,1300,4712222272559814781,N'pulse',N'脉博',N'脉博',N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"/min\",\"ItemDataSet\":\"\"}',30,1,N'2020-02-14 15:24:43'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 1400) INSERT [Blood_TransRecordTypeItem] ([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,1400,4712222272559814781,N'other',N'患者一般情况',N'患者一般情况',N'{\"ItemXType\":\"uxSimpleComboBox\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''清醒'':''清醒''},{''昏迷'':''昏迷''},{''嗜睡'':''嗜睡''},{''麻醉'':''麻醉''}]\"}',40,1,N'2020-02-14 15:29:11'); ";
                listSQL.Add(updateSql);

                //输血15分钟
                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 2100) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,2100,5510188224824882839,N'temperature',N'体温',N'体温',N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"°C\",\"ItemDataSet\":\"\"}',10,1,N'2020-02-14 14:54:56'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 2200) INSERT[Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES( 0,2200,5510188224824882839, N'bloodpressure', N'血压', N'血压', N'{\"ItemXType\":\"textfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"mmhg\",\"ItemDataSet\":\"\"}',20,1, N'2020-02-14 15:22:22'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 2300) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,2300,5510188224824882839,N'pulse',N'脉博',N'脉博',N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"/min\",\"ItemDataSet\":\"\"}',30,1,N'2020-02-14 15:24:43'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 2400) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,2400,5510188224824882839,N'pulse',N'滴速',N'滴速',N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"滴/分\",\"ItemDataSet\":\"\"}',40,1,N'2020-02-14 15:24:43'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 2500) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,2500,5510188224824882839,N'other',N'患者一般情况',N'患者一般情况',N'{\"ItemXType\":\"uxSimpleComboBox\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''正常'':''正常''},{''异常'':''异常''}]\"}',50,1,N'2020-02-14 15:29:11'); ";
                listSQL.Add(updateSql);


                //输血60分钟
                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 3100) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,3100,5109775278160421817,N'temperature',N'体温',N'体温',N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"°C\",\"ItemDataSet\":\"\"}',10,1,N'2020-02-14 14:54:56'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 3200) INSERT [Blood_TransRecordTypeItem] ([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,3200,5109775278160421817,N'bloodpressure',N'血压',N'血压',N'{\"ItemXType\":\"textfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"mmhg\",\"ItemDataSet\":\"\"}',20,1,N'2020-02-14 15:22:22'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 3300) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,3300,5109775278160421817,N'pulse',N'脉博',N'脉博',N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"/min\",\"ItemDataSet\":\"\"}',30,1,N'2020-02-14 15:24:43'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 3400) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,3400,5109775278160421817,N'pulse',N'滴速',N'滴速',N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"滴/分\",\"ItemDataSet\":\"\"}',40,1,N'2020-02-14 15:24:43'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 3500) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,3500,5109775278160421817,N'other',N'患者一般情况',N'患者一般情况',N'{\"ItemXType\":\"uxSimpleComboBox\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''正常'':''正常''},{''异常'':''异常''}]\"}',50,1,N'2020-02-14 15:29:11'); ";
                listSQL.Add(updateSql);

                //输血2小时
                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 4100) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,4100,5660775821937029735,N'temperature',N'体温',N'体温',N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"°C\",\"ItemDataSet\":\"\"}',10,1,N'2020-02-14 14:54:56'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 4200) INSERT [Blood_TransRecordTypeItem] ([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,4200,5660775821937029735,N'bloodpressure',N'血压',N'血压',N'{\"ItemXType\":\"textfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"mmhg\",\"ItemDataSet\":\"\"}',20,1,N'2020-02-14 15:22:22'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 4300) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,4300,5660775821937029735,N'pulse',N'脉博',N'脉博',N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"/min\",\"ItemDataSet\":\"\"}',30,1,N'2020-02-14 15:24:43'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 4400) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,4400,5660775821937029735,N'pulse',N'滴速',N'滴速',N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"滴/分\",\"ItemDataSet\":\"\"}',40,1,N'2020-02-14 15:24:43'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 4500) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,4500,5660775821937029735,N'other',N'患者一般情况',N'患者一般情况',N'{\"ItemXType\":\"uxSimpleComboBox\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''正常'':''正常''},{''异常'':''异常''}]\"}',50,1,N'2020-02-14 15:29:11'); ";
                listSQL.Add(updateSql);

                //输血3小时
                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 5100) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,5100,4959979330910815717,N'temperature',N'体温',N'体温',N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"°C\",\"ItemDataSet\":\"\"}',10,1,N'2020-02-14 14:54:56'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 5200) INSERT [Blood_TransRecordTypeItem] ([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,5200,4959979330910815717,N'bloodpressure',N'血压',N'血压',N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"mmhg\",\"ItemDataSet\":\"\"}',20,1,N'2020-02-14 15:22:22'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 5300) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,5300,4959979330910815717,N'pulse',N'脉博',N'脉博',N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"/min\",\"ItemDataSet\":\"\"}',30,1,N'2020-02-14 15:24:43'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 5400) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,5400,4959979330910815717,N'pulse',N'滴速',N'滴速',N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"滴/分\",\"ItemDataSet\":\"\"}',40,1,N'2020-02-14 15:24:43'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 5500) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,5500,4959979330910815717,N'other',N'患者一般情况',N'患者一般情况',N'{\"ItemXType\":\"uxSimpleComboBox\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''正常'':''正常''},{''异常'':''异常''}]\"}',50,1,N'2020-02-14 15:29:11'); ";
                listSQL.Add(updateSql);

                //输血4小时
                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 6100) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,6100,4748574105933710009,N'temperature',N'体温',N'体温',N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"°C\",\"ItemDataSet\":\"\"}',10,1,N'2020-02-14 14:54:56'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 6200) INSERT [Blood_TransRecordTypeItem] ([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,6200,4748574105933710009,N'bloodpressure',N'血压',N'血压',N'{\"ItemXType\":\"textfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"mmhg\",\"ItemDataSet\":\"\"}',20,1,N'2020-02-14 15:22:22'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 6300) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,6300,4748574105933710009,N'pulse',N'脉博',N'脉博',N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"/min\",\"ItemDataSet\":\"\"}',30,1,N'2020-02-14 15:24:43'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 6400) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,6400,4748574105933710009,N'pulse',N'滴速',N'滴速',N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"滴/分\",\"ItemDataSet\":\"\"}',40,1,N'2020-02-14 15:24:43'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 6500) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[TransItemEditInfo],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,6500,4748574105933710009,N'other',N'患者一般情况',N'患者一般情况',N'{\"ItemXType\":\"uxSimpleComboBox\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''正常'':''正常''},{''异常'':''异常''}]\"}',50,1,N'2020-02-14 15:29:11'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 4703071337826980944) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,4703071337826980944,4780816011182130210,N'10',N'立即停止输血,保持静脉通路',N'10',10,1,N'2020-02-14 17:19:27'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 5215863475154392192) INSERT [Blood_TransRecordTypeItem]([LabID],[TransRecordTypeItemID],[TransRecordTypeID],[TransItemCode],[TransItemName],[SName],[DispOrder],[IsVisible],[DataAddTime]) VALUES ( 0,5215863475154392192,4780816011182130210,N'20',N'对症处理',N'30',30,1,N'2020-02-14 17:20:27'); ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.34");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.34) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.35
            if (IsUpdateDataBase(oldVersion, "1.0.0.35"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_Recei
                updateSql = " IF COL_LENGTH('Blood_Recei', 'BReqTypeID') IS NULL ALTER TABLE Blood_Recei ADD BReqTypeID nvarchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'AboReportDescImage') IS NULL ALTER TABLE Blood_Recei ADD AboReportDescImage image; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'RhReportDescImage') IS NULL ALTER TABLE Blood_Recei ADD RhReportDescImage image; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'AboReportDescDemoNoList') IS NULL ALTER TABLE Blood_Recei ADD AboReportDescDemoNoList nvarchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'AboReportDescDemoNameList') IS NULL ALTER TABLE Blood_Recei ADD AboReportDescDemoNameList nvarchar(200); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'RhReportDescDemoNoList') IS NULL ALTER TABLE Blood_Recei ADD RhReportDescDemoNoList nvarchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'RhReportDescDemoNameList') IS NULL ALTER TABLE Blood_Recei ADD RhReportDescDemoNameList nvarchar(200); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'srResult1Image') IS NULL ALTER TABLE Blood_Recei ADD srResult1Image image; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'srResult2Image') IS NULL ALTER TABLE Blood_Recei ADD srResult2Image image; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'srResult1DemoNoList') IS NULL ALTER TABLE Blood_Recei ADD srResult1DemoNoList nvarchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'SrResult1DemoNameList') IS NULL ALTER TABLE Blood_Recei ADD SrResult1DemoNameList nvarchar(200); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'SrResult2DemoNoList') IS NULL ALTER TABLE Blood_Recei ADD SrResult2DemoNoList nvarchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'SrResult2DemoNameList') IS NULL ALTER TABLE Blood_Recei ADD SrResult2DemoNameList nvarchar(200); ";
                listSQL.Add(updateSql);


                updateSql = " IF COL_LENGTH('Blood_Recei', 'NurseSender') IS NULL ALTER TABLE Blood_Recei ADD NurseSender nvarchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'NurseSendTime') IS NULL ALTER TABLE Blood_Recei ADD NurseSendTime nvarchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'InceptID') IS NULL ALTER TABLE Blood_Recei ADD InceptID nvarchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'incepter') IS NULL ALTER TABLE Blood_Recei ADD incepter nvarchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'incepter') IS NULL ALTER TABLE Blood_Recei ADD incepter nvarchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'inceptTime') IS NULL ALTER TABLE Blood_Recei ADD inceptTime nvarchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'ZDY1') IS NULL ALTER TABLE Blood_Recei ADD ZDY1 nvarchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'ZDY2') IS NULL ALTER TABLE Blood_Recei ADD ZDY2 nvarchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'ZDY3') IS NULL ALTER TABLE Blood_Recei ADD ZDY3 nvarchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'ZDY4') IS NULL ALTER TABLE Blood_Recei ADD ZDY4 nvarchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'ZDY5') IS NULL ALTER TABLE Blood_Recei ADD ZDY5 nvarchar(20); ";
                listSQL.Add(updateSql);


                updateSql = " IF COL_LENGTH('Blood_Recei', 'ReViewAboReportDescDemo') IS NULL ALTER TABLE Blood_Recei ADD ReViewAboReportDescDemo nvarchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'ReViewRhReportDescDemo') IS NULL ALTER TABLE Blood_Recei ADD ReViewRhReportDescDemo nvarchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'IFlag1') IS NULL ALTER TABLE Blood_Recei ADD IFlag1 nvarchar(5); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'IFlag2') IS NULL ALTER TABLE Blood_Recei ADD IFlag2 nvarchar(5); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'IFlag3') IS NULL ALTER TABLE Blood_Recei ADD IFlag3 nvarchar(5); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'IFlag4') IS NULL ALTER TABLE Blood_Recei ADD IFlag4 nvarchar(5); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Recei', 'IFlag5') IS NULL ALTER TABLE Blood_Recei ADD IFlag5 nvarchar(5); ";
                listSQL.Add(updateSql);
                #endregion

                #region PUser

                updateSql = " alter table PUser alter column ShortCode varchar(20); ";
                listSQL.Add(updateSql);

                #endregion

                #region Department
                updateSql = " IF COL_LENGTH('Department', 'ParentID') IS NULL ALTER TABLE Department ADD ParentID int; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.35");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.35) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.36
            if (IsUpdateDataBase(oldVersion, "1.0.0.36"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region RBAC_Module
                updateSql = "    IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RBAC_Module]') AND type in(N'U')) create table RBAC_Module ( LabID bigint null, ModuleID bigint not null, AppComID bigint null, ParentID bigint null, LevelNum int null, TreeCatalog int null, IsLeaf bit null, ModuleType int null, PicFile nvarchar(200) null, URL nvarchar(150) null, Para nvarchar(500) null, Owner varchar(50) null, UseCode varchar(50) null, StandCode varchar(50) null, DeveCode varchar(50) null, CName nvarchar(50) null, EName varchar(50) null, SName varchar(50) null, Shortcode varchar(20) null, PinYinZiTou varchar(50) null, Comment nvarchar(500) null, IsUse bit null, DispOrder int null, DataAddTime datetime null, DataUpdateTime datetime null, DataTimeStamp timestamp null, constraint PK_RBAC_MODULE primary key clustered (ModuleID)); ";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_Role
                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RBAC_Role]') AND type in(N'U')) create table RBAC_Role ( LabID bigint null, RoleID bigint not null, ParentID bigint null, LevelNum int null, TreeCatalog int null, UseCode varchar(50) null, StandCode varchar(50) null, DeveCode varchar(50) null, CName nvarchar(50) null, EName varchar(50) null, SName varchar(50) null, Shortcode varchar(20) null, PinYinZiTou varchar(50) null, Comment ntext null, IsUse bit null, DispOrder int null, DataAddTime datetime null, DataUpdateTime datetime null, DataTimeStamp timestamp null, constraint PK_RBAC_ROLE primary key clustered (RoleID));";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_EmpOptions
                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RBAC_EmpOptions]') AND type in(N'U')) create table RBAC_EmpOptions ( LabID bigint null, EmpOptionsID bigint not null, EmpID bigint null, DefaultModuleID bigint null, ModuleIconSize int null, NewModuleLookTime datetime null, AllModuleIconSize int null, IsLock bit null, ModuleInfoSize bigint null, NewInfoModuleLookTime datetime null, DataAddTime datetime null, DataUpdateTime datetime null, DataTimeStamp timestamp null, constraint PK_RBAC_EMPOPTIONS primary key clustered (EmpOptionsID));";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_EmpRoles
                updateSql = " IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RBAC_EmpRoles]') AND type in(N'U')) create table RBAC_EmpRoles ( LabID bigint null, EmpRoleID bigint not null, EmpID bigint null, RoleID bigint null, IsUse bit null, DispOrder int null, DataAddTime datetime null, DataUpdateTime datetime null, DataTimeStamp timestamp null, Validity varchar(500) null, constraint PK_RBAC_EMPROLES primary key clustered (EmpRoleID));";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_ModuleOper
                updateSql = "  IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[RBAC_ModuleOper]') AND type in(N'U')) create table RBAC_ModuleOper ( LabID bigint null, ModuleOperID bigint not null, AppComOperateID bigint null, ModuleID bigint null, RowFilterID bigint null, InvisibleOrDisable int not null, UseCode varchar(50) null, StandCode varchar(50) null, DeveCode varchar(50) null, CName nvarchar(50) null, EName varchar(50) null, SName varchar(50) null, Shortcode varchar(20) null, PinYinZiTou varchar(50) null, Comment ntext null, IsUse bit null, DispOrder int null, DefaultChecked bit null, OperateURL varchar(200) null, RowFilterURL varchar(200) null, RowFilterBase varchar(500) null, FilterCondition varchar(500) null, ColFilterURL varchar(200) null, ColFilterBase varchar(500) null, ColFilterDesc varchar(50) null, DataAddTime datetime null, DataUpdateTime datetime null, DataTimeStamp timestamp null, UseRowFilter bit null, PredefinedField ntext null, constraint PK_RBAC_MODULEOPER primary key clustered (ModuleOperID));";
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

                #region Department
                updateSql = " update Department set ParentID=0 where ParentID is null; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Department', 'LabID') IS NULL ALTER TABLE Department ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Department', 'DataAddTime') IS NULL ALTER TABLE Department ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Department', 'DataTimeStamp') IS NULL ALTER TABLE Department ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " Update Department set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);

                #endregion

                #region PUser
                updateSql = " IF COL_LENGTH('PUser', 'LabID') IS NULL ALTER TABLE PUser ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('PUser', 'DataAddTime') IS NULL ALTER TABLE PUser ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('PUser', 'DataTimeStamp') IS NULL ALTER TABLE PUser ADD DataTimeStamp timestamp; ";
                listSQL.Add(updateSql);

                updateSql = " Update PUser set LabID=0 where LabID is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region B_Parameter
                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5536111818360502494) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,5536111818360502494,N'HIS调用时按传入信息自动建立科室人员关系',N'HIS',N'CONFIG',N'BL-ISAT-ADDDU-0014',N'1',N'1:是;0:否;',10,1,0,N'2020-04-04 13:02:27',N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"1\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''是''},{''0'':''否''}]\"}'); ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.36");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.36) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.37
            if (IsUpdateDataBase(oldVersion, "1.0.0.37"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region B_Parameter
                //删除实验室数据升级版本运行参数
                updateSql = " delete from B_Parameter where ParaNo='BL-ULAB-DATA-0007'; ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5138720501622064189) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,5138720501622064189,N'用血申请传入的患者参数非空字段',N'HIS',N'CONFIG',N'BL-HISN-FIED-0017',N'admId',N'用血申请传入的患者参数非空字段',10,1,0,N'2020-04-08 14:57:52',N'{\"ItemXType\":\"textfield\",\"ItemDefaultValue\":\"admId\",\"ItemUnit\":\"\",\"ItemDataSet\":\"\"}'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5036215231652457320) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,5036215231652457320,N'用血申请审核完成后是否返回给HIS',N'HIS',N'CONFIG',N'BL-ISTO-HISR-0018',N'true',N'用血申请审核完成后是否返回给HIS',20,1,0,N'2020-04-08 14:59:34',N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"true\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''true'':''是''},{''false'':''否''}]\"}'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5309967330537261186) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,5309967330537261186,N'获取几天内的LIS检验结果',N'医生站',N'CONFIG',N'BL-LISG-DAYS-0015',N'7',N'获取几天内的LIS检验结果',10,1,0,N'2020-04-08 14:55:37',N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"7\",\"ItemUnit\":\"\",\"ItemDataSet\":\"\"}'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 4664812460164271159) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,4664812460164271159,N'LIS结果为空时默认值',N'医生站',N'CONFIG',N'BL-LISR-DEVL-0016',N'检查中',N'新增用血申请时,检验项目LIS结果为空时,设置的默认值',20,1,0,N'2020-04-08 14:56:55',N'{\"ItemXType\":\"textfield\",\"ItemDefaultValue\":\"检查中\",\"ItemUnit\":\"\",\"ItemDataSet\":\"\"}'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5000433943148799011) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,5000433943148799011,N'是否允许手工选择患者ABO及患者Rh',N'医生站',N'CONFIG',N'BL-NULL-ISBH-0021',N'true',N'是否允许手工选择患者ABO及患者Rh',50,1,0,N'2020-04-08 15:19:35',N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"true\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''true'':''是''},{''false'':''否''}]\"}'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5481303247194050358) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,5481303247194050358,N'紧急用血是否在用血申请确认提交时上传HIS',N'HIS',N'CONFIG',N'BL-ISTO-HISJ-0020',N'true',N'紧急用血是否在用血申请确认提交时上传HIS',40,1,0,N'2020-04-08 15:06:44',N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"true\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''true'':''是''},{''false'':''否''}]\"}'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5686468594605305205) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,5686468594605305205,N'申请作废时是否调用HIS作废接口',N'HIS',N'CONFIG',N'BL-ISTO-HISO-0019',N'true',N'申请作废时是否调用HIS作废接口',60,1,0,N'2020-04-08 15:09:34',N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"true\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''true'':''是''},{''false'':''否''}]\"}'); ";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_Role
                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 1000) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[CName],[SName],[Comment],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,1000,0,0,N'系统管理员',N'系统',N'系统管理员',1,10,N'2020-04-02 11:03:13'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 10000) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,10000,0,0,N'智方管理',N'智方',N'ZFGL',N'ZFGL',1,-1000,N'2020-04-03 14:28:19');";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20010) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,20010,0,0,N'医生',N'医生站',N'YS',N'YS',1,20,N'2020-04-03 14:24:23');";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20020) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,20020,0,0,N'护士',N'护士站',N'HS',N'HS',1,30,N'2020-04-03 14:24:38'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20030) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,20030,0,0,N'输血科',N'输血科',N'SXK',N'SXK',1,40,N'2020-04-03 14:25:49'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20040) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,20040,0,0,N'护工角色',N'护工',N'HGJS',N'HGJS',1,30,N'2020-04-03 14:25:03'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Role] WHERE [RoleID] = 20050) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[CName],[SName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,20050,0,0,N'统计和报表',N'统计',N'TJHBB',N'TJHBB',1,50,N'2020-04-03 14:27:42'); ";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_Module
                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100,0,0,0,0,3,N'package.PNG',N'血库系统',1,10,N'2020-04-02 10:36:15'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 1000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,1000,100,0,0,0,3,N'package.PNG',N'系统基础设置',N'XTJCSZ',N'XTJCSZ',1,10000,N'2016-03-22 17:54:17'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 2000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,2000,100,0,0,0,3,N'package.PNG',N'医生站',1,500,N'2020-04-03 10:21:18'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 2010) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,2010,2000,0,0,0,0,N'list.PNG',N'/layui/views/bloodtransfusion/doctorstation/director/index.html',N'科主任审核',N'KZRSH',N'KZRSH',1,400,N'2020-04-03 15:27:45'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 2020) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,2020,2000,0,0,0,0,N'list.PNG',N'/layui/views/bloodtransfusion/doctorstation/apply/index.html',N'用血申请',N'YXSQ',N'YXSQ',1,100,N'2020-04-03 15:27:04'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 2030) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,2030,2000,0,0,0,0,N'list.PNG',N'/layui/views/bloodtransfusion/doctorstation/senior/index.html',N'上级审核',N'SJSH',N'SJSH',1,300,N'2020-04-03 15:26:18'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 2040) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,2040,2000,0,0,0,0,N'list.PNG',N'/layui/views/bloodtransfusion/doctorstation/applyandreview/index.html',N'用库申请+',N'YKSQ',N'YKSQ',1,110,N'2020-04-03 15:25:04');  ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 2600) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,2600,2000,0,0,0,0,N'list.PNG',N'/layui/views/bloodtransfusion/doctorstation/medical/index.html',N'医务处审批',N'YWCSP',N'YWCSP',1,500,N'2020-04-03 15:25:39');  ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 3000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,3000,100,0,0,0,3,N'package.PNG',N'护士站',N'HSZ',N'HSZ',1,600,N'2020-04-03 10:21:18'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 3010) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,3010,3000,0,0,0,0,N'default.PNG',N'血袋领用确认',N'XDLYQR',N'XDLYQR',1,10,N'2020-04-03 14:10:33'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 3020) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,3020,3000,0,0,0,0,N'default.PNG',N'交叉配血条码打印',N'JCPXTMDY',N'JCPXTMDY',1,20,N'2020-04-03 14:10:45'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 3030) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,3030,3000,0,0,0,0,N'list.PNG',N'#Shell.class.blood.nursestation.bloodprohandover.App',N'血袋接收',N'XDJS',N'XDJS',1,40,N'2020-04-03 14:11:24'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 3040) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,3040,3000,0,0,0,0,N'list.PNG',N'#Shell.class.blood.nursestation.transrecord.App',N'输血过程记录',N'SXGCJL',N'SXGCJL',1,50,N'2020-04-03 14:11:49'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 3050) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,3050,3000,0,0,0,0,N'list.PNG',N'/layui/views/bloodtransfusion/nursestation/bloodbagretrieve/index.html',N'血袋回收',N'XDHS',N'XDHS',1,60,N'2020-04-03 14:12:54'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 3060) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,3060,3000,0,0,0,0,N'default.PNG',N'取血凭证',N'QXPZ',N'QXPZ',1,30,N'2020-04-03 14:11:06'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 4000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,4000,100,0,0,0,3,N'package.PNG',N'输血科',N'SXK',N'SXK',1,700,N'2020-04-03 10:21:18'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 5000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,5000,100,0,0,0,3,N'package.PNG',N'库存管理',N'KCGL',N'KCGL',1,800,N'2020-04-03 10:21:18'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 6000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,6000,100,0,0,0,3,N'package.PNG',N'统计和报表',N'TJHBB',N'TJHBB',1,900,N'2020-04-03 10:21:18'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 6100) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,6100,6000,0,0,0,0,N'list.PNG',N'#Shell.class.blood.statistics.reqntegrated.App',N'输血综合查询',N'SXZHCX',N'SXZHCX',1,10,N'2020-04-03 14:13:30'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 7000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,7000,100,0,0,0,3,N'package.PNG',N'系统参数',N'XTCS',N'XTCS',1,100,N'2020-04-03 10:21:18'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 7100) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,7100,7000,0,0,0,0,N'configuration.PNG',N'#Shell.class.blood.bparameters.runparams.App',N'运行参数',N'YXCS',N'YXCS',1,20,N'2020-04-03 14:04:34'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 7200) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,7200,7000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.bparameter.App?issys=1',N'全局参数',N'QJCS',N'QJCS',1,10,N'2020-04-03 14:04:02'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 7300) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,7300,7000,0,0,0,0,N'configuration.PNG',N'#Shell.class.blood.bparameters.setparams.Form',N'用户设置',N'YHSZ',N'YHSZ',1,30,N'2020-04-03 14:05:10'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8000,100,0,0,0,3,N'package.PNG',N'基础维护6.6',N'JCWH6.6',N'JCWH6.6',1,200,N'2020-04-03 10:21:18'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8010) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8010,8000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.doctor.App',N'医生维护',N'YSWH',N'YSWH',1,30,N'2020-04-03 14:07:51'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8020) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8020,8000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.puser.App',N'人员维护',N'RYWH',N'RYWH',1,20,N'2020-04-03 14:07:29'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8030) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8030,8000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.deptuser.App',N'科室人员维护',N'KSRYWH',N'KSRYWH',1,40,N'2020-04-03 14:08:18'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 8100) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,8100,8000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.department.App',N'科室维护',N'KSWH',N'KSWH',1,10,N'2020-04-03 14:06:58'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9000) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9000,100,0,0,0,3,N'package.PNG',N'基础维护',N'JCWH',N'JCWH',1,300,N'2020-04-03 10:21:18'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9010) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9010,9000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.dicttype.App',N'字典类型维护',N'ZDLXWH',N'ZDLXWH',1,10,N'2020-04-03 14:08:54'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9020) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9020,9000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.dict.App',N'字典维护',N'ZDWH',N'ZDWH',1,20,N'2020-04-03 14:09:17'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9100) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9100,100,0,0,0,3,N'package.PNG',N'医生站设置',N'YSZSZ',N'YSZSZ',1,400,N'2020-04-03 14:01:25'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9110) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9110,9100,0,0,0,0,N'configuration.PNG',N'/layui/views/bloodtransfusion/sysbase/bloodusedesc/index.html',N'用血说明维护',N'YXSMWH',N'YXSMWH',1,10,N'2020-04-03 15:22:38'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9120) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9120,9100,0,0,0,0,N'configuration.PNG',N'就诊类型',N'JZLX',N'JZLX',1,20,N'2020-04-03 15:22:55'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9130) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9130,9100,0,0,0,0,N'configuration.PNG',N'申请类型',N'SQLX',N'SQLX',1,30,N'2020-04-03 15:23:10'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9200) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9200,100,0,0,0,3,N'package.PNG',N'护士站设置',N'HSZSZ',N'HSZSZ',1,410,N'2020-04-03 14:01:42'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9210) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9210,9200,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.transrecordtype.App',N'输血过程记录分类',N'SXGCJLFL',N'SXGCJLFL',1,10,N'2020-04-03 14:09:46'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9220) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9220,9200,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.transrecordtypeitem.App',N'输血过程记录项字典',N'SXGCJLXZD',N'SXGCJLXZD',1,20,N'2020-04-03 14:10:13'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9300) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9300,100,0,0,0,3,N'package.PNG',N'输血科设置',N'SXKSZ',N'SXKSZ',1,420,N'2020-04-03 14:02:10'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9310) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9310,9300,0,0,0,0,N'configuration.PNG',N'血制品单位',N'XZPDW',N'XZPDW',1,20,N'2020-04-03 15:23:57'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9320) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9320,9300,0,0,0,0,N'configuration.PNG',N'血制品维护',N'XZPWH',N'XZPWH',1,30,N'2020-04-03 15:24:12'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9330) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9330,9300,0,0,0,0,N'configuration.PNG',N'血库项目维护',N'XKXMWH',N'XKXMWH',1,40,N'2020-04-03 15:24:26'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 9340) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,9340,9300,0,0,0,0,N'dictionary.PNG',N'血制品类型',N'XZPLX',N'XZPLX',1,10,N'2020-04-03 15:23:43'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100010) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100010,1000,0,0,0,3,N'package.PNG',N'#Shell.class.sysbase.module.App',N'功能模块',N'XTJCSZ',N'XTJCSZ',1,10,N'2016-03-22 17:54:17'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100020) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100020,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.jurisdiction.modulerole.App',N'模块角色',1,30,N'2020-04-02 16:31:36'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100030) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100030,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.role.App',N'角色管理',1,20,N'2020-04-02 16:31:18'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100040) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100040,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.jurisdiction.userrole.App',N'员工角色',1,40,N'2020-04-02 16:33:52'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100050) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100050,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.jurisdiction.roleuser.App',N'角色员工',1,50,N'2020-04-02 16:34:22'); ";
                listSQL.Add(updateSql);

                updateSql = " IF NOT EXISTS(SELECT * FROM [RBAC_Module] WHERE [ModuleID] = 100060) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,100060,1000,0,0,0,0,N'configuration.PNG',N'#Shell.class.sysbase.role.ModuleApp',N'角色权限',1,60,N'2020-04-02 16:35:11'); ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.37");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.37) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.38
            if (IsUpdateDataBase(oldVersion, "1.0.0.38"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_BUnit
                updateSql = " IF COL_LENGTH('Blood_BUnit', 'LabID') IS NULL ALTER TABLE Blood_BUnit ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BUnit', 'PinYinZiTou') IS NULL ALTER TABLE Blood_BUnit ADD PinYinZiTou varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BUnit', 'SName') IS NULL ALTER TABLE Blood_BUnit ADD SName varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BUnit', 'DispOrder') IS NULL ALTER TABLE Blood_BUnit ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BUnit', 'DataAddTime') IS NULL ALTER TABLE Blood_BUnit ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BUnit', 'Visible') IS NULL ALTER TABLE Blood_BUnit ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BUnit', 'Visible') IS NOT NULL ALTER TABLE Blood_BUnit ALTER COLUMN Visible bit;  ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_BUnit set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_Unit
                updateSql = " IF COL_LENGTH('Blood_Unit', 'LabID') IS NULL ALTER TABLE Blood_Unit ADD LabID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Unit', 'PinYinZiTou') IS NULL ALTER TABLE Blood_Unit ADD PinYinZiTou varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Unit', 'SName') IS NULL ALTER TABLE Blood_Unit ADD SName varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Unit', 'DispOrder') IS NULL ALTER TABLE Blood_Unit ADD DispOrder int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Unit', 'DataAddTime') IS NULL ALTER TABLE Blood_Unit ADD DataAddTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Unit', 'Visible') IS NULL ALTER TABLE Blood_Unit ADD Visible bit; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Unit', 'Visible') IS NOT NULL ALTER TABLE Blood_Unit ALTER COLUMN Visible bit;  ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_Unit set Visible=1 where Visible is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_Class
                updateSql = " IF COL_LENGTH('Blood_Class', 'PinYinZiTou') IS NULL ALTER TABLE Blood_Class ADD PinYinZiTou varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Class', 'SName') IS NULL ALTER TABLE Blood_Class ADD SName varchar(50); ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BReqType
                updateSql = " IF COL_LENGTH('Blood_BReqType', 'PinYinZiTou') IS NULL ALTER TABLE Blood_BReqType ADD PinYinZiTou varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BReqType', 'SName') IS NULL ALTER TABLE Blood_BReqType ADD SName varchar(50); ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_UseType
                updateSql = " IF COL_LENGTH('Blood_UseType', 'PinYinZiTou') IS NULL ALTER TABLE Blood_UseType ADD PinYinZiTou varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_UseType', 'SName') IS NULL ALTER TABLE Blood_UseType ADD SName varchar(50); ";
                listSQL.Add(updateSql);
                #endregion

                #region blood_style
                updateSql = " IF COL_LENGTH('blood_style', 'PinYinZiTou') IS NULL ALTER TABLE blood_style ADD PinYinZiTou varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('blood_style', 'SName') IS NULL ALTER TABLE blood_style ADD SName varchar(50); ";
                listSQL.Add(updateSql);
                #endregion

                #region Blood_BTestItem
                updateSql = " IF COL_LENGTH('Blood_BTestItem', 'PinYinZiTou') IS NULL ALTER TABLE Blood_BTestItem ADD PinYinZiTou varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BTestItem', 'SName') IS NULL ALTER TABLE Blood_BTestItem ADD SName varchar(50); ";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_Module
                updateSql = " update RBAC_Module set URL='#Shell.class.sysbase.bloodbreqtype.App' where URL is null and [ModuleID]=9120; ";
                listSQL.Add(updateSql);

                updateSql = " update RBAC_Module set URL='#Shell.class.sysbase.bloodusetype.App' where URL is null and [ModuleID]=9130; ";
                listSQL.Add(updateSql);

                updateSql = " update RBAC_Module set URL='#Shell.class.sysbase.bloodclass.App' where URL is null and [ModuleID]=9340; ";
                listSQL.Add(updateSql);

                updateSql = " update RBAC_Module set URL='#Shell.class.sysbase.bloodunit.App' where URL is null and [ModuleID]=9310; ";
                listSQL.Add(updateSql);

                updateSql = " update RBAC_Module set URL='#Shell.class.sysbase.bloodstyle.App' where URL is null and [ModuleID]=9320;";
                listSQL.Add(updateSql);

                updateSql = " update RBAC_Module set URL='#Shell.class.sysbase.bloodbtestitem.App' where URL is null and [ModuleID]=9330; ";
                listSQL.Add(updateSql);
                #endregion

                #region blood_style
                updateSql = " IF COL_LENGTH('blood_style', 'BloodUnitNo') IS NOT NULL ALTER TABLE blood_style ALTER COLUMN BloodUnitNo int;  ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.38");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.38) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.39 
            if (IsUpdateDataBase(oldVersion, "1.0.0.39"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region B_Parameter

                updateSql = "  if not exists(select * from B_Parameter where ParameterID=4741885760719818457) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[Shortcode],[DispOrder],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,4741885760719818457,N'血袋扫码识别字段',N'系统',N'CONFIG',N'BL-BBSC-IDFT-0022',N'BBagCode',N'血袋扫码识别字段',N'0',10,1,0,N'2020-04-30 11:40:27',N'{\"ItemXType\":\"uxSimpleComboBox\",\"ItemDefaultValue\":\"BBagCode\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''BBagCode'':''血袋号''},{''B3Code'':''惟一码''}]\"}'); ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.39");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.39) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.40
            if (IsUpdateDataBase(oldVersion, "1.0.0.40"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region SC_Operation

                updateSql = " IF COL_LENGTH('SC_Operation', 'TypeName') IS NOT NULL ALTER TABLE SC_Operation ALTER COLUMN TypeName varchar(50);  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('SC_Operation', 'BusinessModuleCode') IS NOT NULL ALTER TABLE SC_Operation ALTER COLUMN BusinessModuleCode varchar(50);  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('SC_Operation', 'Memo') IS NOT NULL ALTER TABLE SC_Operation ALTER COLUMN Memo text;  ";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_TransRecordTypeItem
                updateSql = " IF EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 1200) update Blood_TransRecordTypeItem set TransItemEditInfo = N'{\"ItemXType\":\"textfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"mmhg\",\"ItemDataSet\":\"\"}' WHERE [TransRecordTypeItemID] = 1200; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 2200) update Blood_TransRecordTypeItem set TransItemEditInfo = N'{\"ItemXType\":\"textfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"mmhg\",\"ItemDataSet\":\"\"}' WHERE [TransRecordTypeItemID] = 2200; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 3200) update Blood_TransRecordTypeItem set TransItemEditInfo = N'{\"ItemXType\":\"textfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"mmhg\",\"ItemDataSet\":\"\"}' WHERE [TransRecordTypeItemID] = 3200; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 4200) update Blood_TransRecordTypeItem set TransItemEditInfo = N'{\"ItemXType\":\"textfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"mmhg\",\"ItemDataSet\":\"\"}' WHERE [TransRecordTypeItemID] = 4200; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 5200) update Blood_TransRecordTypeItem set TransItemEditInfo = N'{\"ItemXType\":\"textfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"mmhg\",\"ItemDataSet\":\"\"}' WHERE [TransRecordTypeItemID] = 5200; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM Blood_TransRecordTypeItem WHERE [TransRecordTypeItemID] = 6200) update Blood_TransRecordTypeItem set TransItemEditInfo = N'{\"ItemXType\":\"textfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"mmhg\",\"ItemDataSet\":\"\"}' WHERE [TransRecordTypeItemID] = 6200; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.40");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.39) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.41
            if (IsUpdateDataBase(oldVersion, "1.0.0.41"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region 系统运行参数
                updateSql = " IF NOT EXISTS(SELECT * FROM B_Parameter WHERE [ParameterID] = 5101233254823494117) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[DispOrder],[IsUse],[IsUserSet],[DataAddTime]) VALUES ( 0,5101233254823494117,N'输血登记是否需要交接登记完成后',N'护士站',N'CONFIG',N'BL-BLTF-ISNB-0023',N'0',N'1:是;0:否;',10,1,1,N'2020-02-15 10:17:25'); ";
                listSQL.Add(updateSql);

                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"1\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''是''},{''0'':''否''}]\"}' where ParameterID=5101233254823494117 and ItemEditInfo is null; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.41");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.41) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.42
            if (IsUpdateDataBase(oldVersion, "1.0.0.42"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_BOutForm
                updateSql = " IF COL_LENGTH('Blood_BOutForm', 'EndBloodOperId') IS NULL ALTER TABLE Blood_BOutForm ADD EndBloodOperId bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutForm', 'EndBloodOperName') IS NULL ALTER TABLE Blood_BOutForm ADD EndBloodOperName varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutForm', 'EndBloodOperTime') IS NULL ALTER TABLE Blood_BOutForm ADD EndBloodOperTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_BOutForm', 'EndBloodReason') IS NULL ALTER TABLE Blood_BOutForm ADD EndBloodReason varchar(500); ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.42");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.42) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.43
            if (IsUpdateDataBase(oldVersion, "1.0.0.43"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_BOutForm
                updateSql = " IF COL_LENGTH('Blood_BOutForm', 'BDEndBReasonId') IS NULL ALTER TABLE Blood_BOutForm ADD BDEndBReasonId bigint; ";
                listSQL.Add(updateSql);
                #endregion

                #region B_DictType
                updateSql = " if not exists(select * from [B_DictType] where DCId=200) INSERT [B_DictType] ([LabID],[DCId],[CName],[DispOrder],[Memo],[IsUse],[DataAddTime],[DictTypeCode]) VALUES ( 0,200,N'终止输血原因',200,N'终止输血原因',1,N'2020-07-09 15:11:18',N'BDEndBReason');";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.43");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.43) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.44
            if (IsUpdateDataBase(oldVersion, "1.0.0.44"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_TransRecordType
                updateSql = " IF COL_LENGTH('Blood_TransRecordType', 'TransTypeId') IS NULL ALTER TABLE Blood_TransRecordType ADD TransTypeId bigint; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.44");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.44) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.45
            if (IsUpdateDataBase(oldVersion, "1.0.0.45"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Department 
                updateSql = " IF COL_LENGTH('Department', 'cname') IS NOT NULL ALTER TABLE Department ALTER COLUMN cname NVARCHAR(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Department', 'shortname') IS NOT NULL ALTER TABLE Department ALTER COLUMN shortname NVARCHAR(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Department', 'shortcode') IS NOT NULL ALTER TABLE Department ALTER COLUMN shortcode NVARCHAR(50); ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.45");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.45) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.46
            if (IsUpdateDataBase(oldVersion, "1.0.0.46"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_docGrade 
                updateSql = " UPDATE [dbo].[Blood_docGrade] SET [BCount] =800,[LowLimit] =0,[UpperLimit] =800 WHERE [GradeName]='申请医生' and UpperLimit is null;";
                listSQL.Add(updateSql);

                #endregion

                #region Blood_Patinfo
                updateSql = " IF COL_LENGTH('Blood_Patinfo', 'IsOrder') IS NULL ALTER TABLE Blood_Patinfo ADD IsOrder varchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Blood_Patinfo', 'IsLabNoC') IS NULL ALTER TABLE Blood_Patinfo ADD IsLabNoC varchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_Patinfo set IsOrder='有' where  IsOrder is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Blood_Patinfo set IsLabNoC='有' where  IsLabNoC is null; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.46");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.46) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.47
            if (IsUpdateDataBase(oldVersion, "1.0.0.47"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region B_Parameter 
                updateSql = "  if not exists(select * from B_Parameter where ParameterID=4955399619796462166) INSERT [B_Parameter] ([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[ParaDesc],[Shortcode],[DispOrder],[PinYinZiTou],[IsUse],[IsUserSet],[DataAddTime],[ItemEditInfo]) VALUES ( 0,4955399619796462166,N'用血申请确认后是否自动完成审批',N'医生站',N'CONFIG',N'BL-BLCF-ISAC-0024',N'false',N'true:是;false:否;',N'5',5,N'5',1,0,N'2020-09-09 14:46:42',N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"false\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''true'':''是''},{''false'':''否''}]\"}');";
                listSQL.Add(updateSql);

                #endregion

                #region blood_breqform 
                updateSql = "  update blood_breqform set tohisflag=0 where tohisflag is null;";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.47");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.47) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.48
            if (IsUpdateDataBase(oldVersion, "1.0.0.48"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Blood_BReqForm 
                updateSql = " IF COL_LENGTH('Blood_BReqForm', 'ConfirmType') IS NULL ALTER TABLE Blood_BReqForm ADD ConfirmType varchar(50); ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.48");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.48) Update Error, Please Check The Log!");
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

                //updateSql = "ALTER TABLE[dbo].[B_Parameter] WITH CHECK ADD CONSTRAINT[FK_B_Parameter_P_Dict] FOREIGN KEY([PDictId]) REFERENCES[dbo].[P_Dict] ([DictID])";
                //listSQL.Add(updateSql);

                //updateSql = "ALTER TABLE[dbo].[B_Parameter] CHECK CONSTRAINT[FK_B_Parameter_P_Dict]";
                //listSQL.Add(updateSql);

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

                //ExecuteUpdateSQL("insert into [B_Parameter]([LabID],[ParameterID],[PDictId],[Name],[SName],[ParaType],[ParaNo],[ParaValue]," +
                //    "[ParaDesc],[Shortcode],[DispOrder],[PinYinZiTou],[IsUse],[IsUserSet],[DataAddTime],[DataUpdateTime]) " +
                //    " Values(1," +//LabID
                //    ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "," +//ParameterID
                //    "null," +//PDictId
                //    "\'数据库版本号\'," +//Name
                //    "null," +//SName
                //    "\'SYS\'," +//ParaType
                //     "\'SYS_DBVersion\'," +//ParaNo
                //     "null," +//ParaValue
                //     "\'数据库版本号\'," +//ParaDesc
                //     "null," +//Shortcode
                //     "0," +//DispOrder
                //     "null," +//PinYinZiTou
                //     "1," +//IsUse
                //     "null," +//IsUserSet
                //     "\'" + DateTime.Now.ToString() + "\'," +//DataAddTime
                //     "null" + //DataUpdateTime
                //     ")");
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
