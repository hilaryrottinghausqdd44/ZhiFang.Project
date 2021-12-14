using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System.Configuration;
using System.Collections.Specialized;
using ZhiFang.Digitlab.DAO.ADO;


namespace ZhiFang.DBUpdate.PM
{
    public class
        DBUpdate
    {
        public static string ADOConnectStr = GetADODataBaseSettings(ZhiFang.Common.Public.ConfigHelper.GetDataBaseSettings("databaseSettings", "db.connectionString"));

        static Dictionary<string, string> DicVersion = GetVersionComparison();//

        static string MainAssemblyFile = "ZhiFang.Digitlab.ReagentSys.dll";//可以从配置文件获取

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
            dicVersion.Add("1.0.0.3", "1.0.0.3");//供货单明细表增加PSaleDtlID
            dicVersion.Add("1.0.0.4", "1.0.0.4");//试剂表表增加测试数字段TestCount
            dicVersion.Add("1.0.0.5", "1.0.0.5");//订单明细表中CurrentQty数据类型由Int改为Varchar
            dicVersion.Add("1.0.0.6", "1.0.0.6");//增加表Rea_OrderGoods、Rea_Goods、Rea_CenOrg
            dicVersion.Add("1.0.0.7", "1.0.0.7");//Goods表增加DataAddTime字段
            dicVersion.Add("1.0.0.8", "1.0.0.8");
            dicVersion.Add("1.0.0.9", "1.0.0.9");
            dicVersion.Add("1.0.0.10", "1.0.0.10");
            dicVersion.Add("1.0.0.11", "1.0.0.11");//增加字典树
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
                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'DeleteFlag\' and ID = (Select[ID] from SysObjects where Name = \'BmsCenOrderDoc\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table BmsCenOrderDoc Add DeleteFlag int " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'删除标记\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'BmsCenOrderDoc\', @level2type = N\'COLUMN\',@level2name = N\'DeleteFlag\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.1");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.1) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.2
            if (IsUpdateDataBase(oldVersion, "1.0.0.2"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();
                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'Memo\' and ID = (Select[ID] from SysObjects where Name = \'BmsCenSaleDtl\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table BmsCenSaleDtl Add Memo varchar(500) " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'备注\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'BmsCenSaleDtl\', @level2type = N\'COLUMN\',@level2name = N\'Memo\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'Memo\' and ID = (Select[ID] from SysObjects where Name = \'Goods\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table Goods Add Memo varchar(500) " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'备注\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'Goods\', @level2type = N\'COLUMN\',@level2name = N\'Memo\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);
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
                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'PSaleDtlID\' and ID = (Select[ID] from SysObjects where Name = \'BmsCenSaleDtl\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table BmsCenSaleDtl Add PSaleDtlID bigint " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'上级ID\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'BmsCenSaleDtl\', @level2type = N\'COLUMN\',@level2name = N\'PSaleDtlID\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

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
                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'TestCount\' and ID = (Select[ID] from SysObjects where Name = \'Goods\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table Goods Add TestCount int " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'测试数\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'Goods\', @level2type = N\'COLUMN\',@level2name = N\'TestCount\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

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
                updateSql = "if Exists(Select * from SysColumns where [Name] = \'CurrentQty\' and ID = (Select[ID] from SysObjects where Name = \'BmsCenOrderDtl\')) " + "\r" +
                            "begin " + "\r" +
                                "alter table BmsCenOrderDtl alter column CurrentQty varchar(200) " + "\r" +
                            "end";
                listSQL.Add(updateSql);

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
                updateSql = " if not exists (select * from dbo.sysobjects where id = object_id(N\'[dbo].[Rea_CenOrg]\')" + "\r" +
                            " and OBJECTPROPERTY(id, N\'IsUserTable\') = 1) BEGIN " + "\r" +
                            " CREATE TABLE[dbo].[Rea_CenOrg]( " + "\r" +
                            "     [LabID] [dbo].[D_实验室ID] NULL, " + "\r" +
                            "     [OrgID] [bigint] NOT NULL, " + "\r" +
                            "     [OrgNo] [int] NOT NULL, " + "\r" +
                            "     [OrgType] [int] NOT NULL, " + "\r" +
                            "     [PlatformOrgID] [bigint] NULL, " + "\r" +
                            "     [PlatformOrgNo]  [int] NULL, " + "\r" +
                            "     [CName]  [varchar](100) NOT NULL, " + "\r" +
                            "     [EName] [varchar](100) NULL, " + "\r" +
                            "     [ServerIP] [varchar](100) NULL, " + "\r" +
                            "     [ServerPort] [varchar](10) NULL, " + "\r" +
                            "     [ShortCode] [varchar](50) NULL, " + "\r" +
                            "     [Address] [varchar](100) NULL, " + "\r" +
                            "     [Contact]  [varchar](100) NULL, " + "\r" +
                            "     [Tel] [varchar](500) NULL, " + "\r" +
                            "     [Tel1] [varchar](500) NULL, " + "\r" +
                            "     [HotTel] [varchar](500) NULL, " + "\r" +
                            "     [HotTel1] [varchar](500) NULL, " + "\r" +
                            "     [Fox] [varchar](500) NULL, " + "\r" +
                            "     [Email] [varchar](50) NULL, " + "\r" +
                            "     [WebAddress] [varchar](100) NULL, " + "\r" +
                            "     [Memo] [varchar](100) NULL, " + "\r" +
                            "     [DispOrder]  [int] NULL, " + "\r" +
                            "     [Visible] [int] NULL, " + "\r" +
                            "     [ZX1] [varchar](100) NULL, " + "\r" +
                            "     [ZX2] [varchar](100) NULL, " + "\r" +
                            "     [ZX3] [varchar](100) NULL, " + "\r" +
                            "     [DataUpdateTime] [datetime] NULL, " + "\r" +
                            "     [DataAddTime] [datetime] NULL, " + "\r" +
                            "  CONSTRAINT[PK_Rea_CenOrg] PRIMARY KEY CLUSTERED " + "\r" +
                            " ( " + "\r" +
                            "   [OrgID] ASC " + "\r" +
                            " )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY], " + "\r" +
                            "  CONSTRAINT[IX_Rea_CenOrg] UNIQUE NONCLUSTERED " + "\r" +
                            " ( " + "\r" +
                            "    [OrgNo] ASC " + "\r" +
                            " )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] " + "\r" +
                            " ) ON[PRIMARY] " + "\r" +
                            " end";
                listSQL.Add(updateSql);

                if (!CheckDataObjectIsExists("Rea_CenOrg", "U"))
                {
                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'实验室ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_CenOrg\', @level2type = N\'COLUMN\', @level2name = N\'LabID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name= N\'MS_Description\', @value= N\'机构ID\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'OrgID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'机构编号\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'OrgNo\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'机构类型\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'OrgType\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'平台机构ID\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'PlatformOrgID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'平台机构编码\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'PlatformOrgNo\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'中文名\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'CName\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'英文名\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'EName\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'服务器IP\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'ServerIP\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'服务器端口\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'ServerPort\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'代码\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'ShortCode\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'机构地址\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'Address\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'联系人\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'Contact\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'电话\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'Tel\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'电话1\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'Tel1\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'热线电话\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'HotTel\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'热线电话1\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'HotTel1\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'传真\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'Fox\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'邮箱\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'Email\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'网址\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'WebAddress\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'备注\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'Memo\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'显示次序\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'DispOrder\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'是否使用\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'Visible\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'专项1\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'ZX1\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'专项2\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'ZX2\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'专项3\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\', @level2type= N\'COLUMN\', @level2name= N\'ZX3\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value= N\'机构表\' , @level0type= N\'SCHEMA\', @level0name= N\'dbo\', @level1type= N\'TABLE\', @level1name= N\'Rea_CenOrg\'";
                    listSQL.Add(updateSql);
                }

                updateSql = " if not exists (select * from dbo.sysobjects where id = object_id(N\'[dbo].[Rea_Goods]\')" + "\r" +
                            " and OBJECTPROPERTY(id, N\'IsUserTable\') = 1) BEGIN " + "\r" +
                            " CREATE TABLE[dbo].[Rea_Goods]( " + "\r" +
                            "     [LabID][dbo].[D_实验室ID] NULL, " + "\r" +
                            "     [GoodsID] [bigint] NOT NULL, " + "\r" +
                            "     [OrgID] [bigint] NULL, " + "\r" +
                            "     [GoodsNo] [varchar](50) NOT NULL, " + "\r" +
                            "     [GoodsSN] [varchar](50) NULL, " + "\r" +
                            "     [CName] [varchar](200) NOT NULL, " + "\r" +
                            "     [SName] [varchar](200) NULL, " + "\r" +
                            "     [EName] [varchar](200) NULL, " + "\r" +
                            "     [ShortCode] [varchar](100) NULL, " + "\r" +
                            "     [PinYinZiTou] [varchar](50) NULL, " + "\r" +
                            "     [GoodsClass] [varchar](100) NULL, " + "\r" +
                            "     [GoodsClassType] [varchar](100) NULL, " + "\r" +
                            "     [ProdID] [bigint] NULL, " + "\r" +
                            "     [ProdGoodsNo] [varchar](50) NULL, " + "\r" +
                            "     [ProdOrgName] [varchar](100) NULL, " + "\r" +
                            "     [ProdEara] [varchar](50) NULL, " + "\r" +
                            "     [Price] [float] NULL, " + "\r" +
                            "     [UnitName] [varchar](10) NULL, " + "\r" +
                            "     [UnitMemo] [varchar](200) NULL, " + "\r" +
                            "     [StorageType] [varchar](200) NULL, " + "\r" +
                            "     [GoodsDesc] [varchar](500) NULL, " + "\r" +
                            "     [ApproveDocNo] [varchar](200) NULL, " + "\r" +
                            "     [Standard] [varchar](200) NULL, " + "\r" +
                            "     [RegistNo] [varchar](200) NULL, " + "\r" +
                            "     [RegistDate] [datetime] NULL, " + "\r" +
                            "     [RegistNoInvalidDate] [datetime] NULL, " + "\r" +
                            "     [Purpose] [varchar](500) NULL, " + "\r" +
                            "     [Constitute] [varchar](500) NULL, " + "\r" +
                            "     [License] [image] NULL, " + "\r" +
                            "     [GoodsPicture] [image] NULL, " + "\r" +
                            "     [ZX1] [varchar](100) NULL, " + "\r" +
                            "     [ZX2] [varchar](100) NULL, " + "\r" +
                            "     [ZX3] [varchar](100) NULL, " + "\r" +
                            "     [Equipment] [varchar](100) NULL, " + "\r" +
                            "     [CenOrgConfirm] [int] NULL, " + "\r" +
                            "     [BarCodeMgr] [int] NULL, " + "\r" +
                            "     [IsRegister] [int] NULL, " + "\r" +
                            "     [IsPrintBarCode] [int] NULL, " + "\r" +
                            "     [DispOrder] [int] NULL, " + "\r" +
                            "     [Visible] [int] NULL, " + "\r" +
                            "     [GoodsMemo] [ntext] NULL, " + "\r" +
                            "     [DataUpdateTime] [datetime] NULL, " + "\r" +
                            "     [DataAddTime] [datetime] NULL, " + "\r" +
                            "  CONSTRAINT[PK_Rea_Goods] PRIMARY KEY CLUSTERED " + "\r" +
                            " ( " + "\r" +
                            "    [GoodsID] ASC " + "\r" +
                            " )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] " + "\r" +
                            " ) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY] " + "\r" +
                            " end";
                listSQL.Add(updateSql);

                if (!CheckDataObjectIsExists("Rea_Goods", "U"))
                {
                    updateSql = "  ALTER TABLE[dbo].[Rea_Goods]  WITH CHECK ADD CONSTRAINT[FK_REA_GOOD_REFERENCE_OrgID] FOREIGN KEY([OrgID]) REFERENCES[dbo].[Rea_CenOrg] ([OrgID])";
                    listSQL.Add(updateSql);

                    updateSql = " ALTER TABLE[dbo].[Rea_Goods] CHECK CONSTRAINT[FK_REA_GOOD_REFERENCE_OrgID]";
                    listSQL.Add(updateSql);

                    updateSql = " ALTER TABLE[dbo].[Rea_Goods] WITH CHECK ADD CONSTRAINT[FK_REA_GOOD_REFERENCE_ProdID] FOREIGN KEY([ProdID]) REFERENCES[dbo].[Rea_CenOrg] ([OrgID])";
                    listSQL.Add(updateSql);

                    updateSql = " ALTER TABLE[dbo].[Rea_Goods]  CHECK CONSTRAINT[FK_REA_GOOD_REFERENCE_ProdID]";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'实验室ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'LabID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'货品ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'GoodsID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'本机构ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'OrgID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'货品编号\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'GoodsNo\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'货品顺序号\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'GoodsSN\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'中文名\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'CName\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'中文简称\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'SName\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'英文名\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'EName\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'代码\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'ShortCode\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'汉字拼音字头\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'PinYinZiTou\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'一级分类\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'GoodsClass\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'二级分类\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'GoodsClassType\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'厂商ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'ProdID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'厂商货品编号\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'ProdGoodsNo\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'生成厂家\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'ProdOrgName\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'产地\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'ProdEara\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'出厂单价\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'Price\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'单位\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'UnitName\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'单位描述（规格）\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'UnitMemo\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'储藏条件\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'StorageType\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'货品描述\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'GoodsDesc\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'批准文号\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'ApproveDocNo\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'国标\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'Standard\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'注册号\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'RegistNo\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'注册日期\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'RegistDate\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'注册证有效期\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'RegistNoInvalidDate\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'用途\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'Purpose\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'结构组成\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'Constitute\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'证书内容\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'License\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'外观照片\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'GoodsPicture\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'专项1\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'ZX1\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'专项2\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'ZX2\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'专项3\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'ZX3\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'支持的实验室仪器型号\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'Equipment\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'本机构确认\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'CenOrgConfirm\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'是否盒条码\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'BarCodeMgr\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'是否有注册证\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'IsRegister\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'是否打印条码\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'IsPrintBarCode\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'显示次序\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'DispOrder\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'是否使用\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'Visible\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'备注\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'GoodsMemo\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'数据更新时间\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'DataUpdateTime\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'数据新增时间\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\', @level2name = N\'DataAddTime\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'平台货品表\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_Goods\'";
                    listSQL.Add(updateSql);
                }

                updateSql = " if not exists (select * from dbo.sysobjects where id = object_id(N\'[dbo].[Rea_OrderGoods]\')" + "\r" +
                            " and OBJECTPROPERTY(id, N\'IsUserTable\') = 1) BEGIN " + "\r" +
                            " CREATE TABLE[dbo].[Rea_OrderGoods](" + "\r" +
                            "     [LabID][dbo].[D_实验室ID] NULL," + "\r" +
                            "     [OrderGoodsID] [bigint] NOT NULL," + "\r" +
                            "     [OrgID] [bigint] NULL," + "\r" +
                            "     [CenOrgID] [bigint] NOT NULL," + "\r" +
                            "     [GoodsID] [bigint] NOT NULL," + "\r" +
                            "     [CenOrgGoodsNo] [varchar](100) NULL," + "\r" +
                            "     [Price] [float] NULL," + "\r" +
                            "     [BiddingNo] [varchar](100) NULL," + "\r" +
                            "     [Memo] [ntext] NULL," + "\r" +
                            "     [DispOrder] [int] NULL," + "\r" +
                            "     [Visible] [int] NULL," + "\r" +
                            "     [ZX1] [varchar](100) NULL," + "\r" +
                            "     [ZX2] [varchar](100) NULL," + "\r" +
                            "     [ZX3] [varchar](100) NULL," + "\r" +
                            "     [DataUpdateTime] [datetime] NULL," + "\r" +
                            "     [DataAddTime] [datetime] NULL," + "\r" +
                            "  CONSTRAINT[PK_Rea_OrderGoods] PRIMARY KEY CLUSTERED" + "\r" +
                            " (" + "\r" +
                            "    [OrderGoodsID] ASC" + "\r" +
                            " )WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" + "\r" +
                            " ) ON[PRIMARY] TEXTIMAGE_ON[PRIMARY]" + "\r" +
                            " end";
                listSQL.Add(updateSql);

                if (!CheckDataObjectIsExists("Rea_OrderGoods", "U"))
                {
                    updateSql = " ALTER TABLE[dbo].[Rea_OrderGoods] WITH CHECK ADD CONSTRAINT[FK_REA_ORDE_REFERENCE_CenOrgID] FOREIGN KEY([CenOrgID]) REFERENCES[dbo].[Rea_CenOrg] ([OrgID])";
                    listSQL.Add(updateSql);

                    updateSql = " ALTER TABLE[dbo].[Rea_OrderGoods] CHECK CONSTRAINT[FK_REA_ORDE_REFERENCE_CenOrgID]";
                    listSQL.Add(updateSql);

                    updateSql = " ALTER TABLE[dbo].[Rea_OrderGoods] WITH CHECK ADD CONSTRAINT[FK_REA_ORDE_REFERENCE_OrgID] FOREIGN KEY([OrgID]) REFERENCES[dbo].[Rea_CenOrg] ([OrgID])";
                    listSQL.Add(updateSql);

                    updateSql = " ALTER TABLE[dbo].[Rea_OrderGoods]  CHECK CONSTRAINT[FK_REA_ORDE_REFERENCE_OrgID]";
                    listSQL.Add(updateSql);

                    updateSql = " ALTER TABLE[dbo].[Rea_OrderGoods] WITH CHECK ADD CONSTRAINT[FK_REA_ORDE_REFERENCE_REA_GOOD] FOREIGN KEY([GoodsID]) REFERENCES[dbo].[Rea_Goods] ([GoodsID])";
                    listSQL.Add(updateSql);

                    updateSql = " ALTER TABLE[dbo].[Rea_OrderGoods]  CHECK CONSTRAINT[FK_REA_ORDE_REFERENCE_REA_GOOD]";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'实验室ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_OrderGoods\', @level2type = N\'COLUMN\', @level2name = N\'LabID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'订货信息ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_OrderGoods\', @level2type = N\'COLUMN\', @level2name = N\'OrderGoodsID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'本机构ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_OrderGoods\', @level2type = N\'COLUMN\', @level2name = N\'OrgID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'供货或订货机构ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_OrderGoods\', @level2type = N\'COLUMN\', @level2name = N\'CenOrgID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'货品ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_OrderGoods\', @level2type = N\'COLUMN\', @level2name = N\'GoodsID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'供货或订货机构货品编码\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_OrderGoods\', @level2type = N\'COLUMN\', @level2name = N\'CenOrgGoodsNo\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'价格\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_OrderGoods\', @level2type = N\'COLUMN\', @level2name = N\'Price\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'招标号\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_OrderGoods\', @level2type = N\'COLUMN\', @level2name = N\'BiddingNo\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'备注\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_OrderGoods\', @level2type = N\'COLUMN\', @level2name = N\'Memo\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'显示次序\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_OrderGoods\', @level2type = N\'COLUMN\', @level2name = N\'DispOrder\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'是否使用\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_OrderGoods\', @level2type = N\'COLUMN\', @level2name = N\'Visible\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'专项1\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_OrderGoods\', @level2type = N\'COLUMN\', @level2name = N\'ZX1\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'专项2\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_OrderGoods\', @level2type = N\'COLUMN\', @level2name = N\'ZX2\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'专项3\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_OrderGoods\', @level2type = N\'COLUMN\', @level2name = N\'ZX3\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'订货信息表\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'Rea_OrderGoods\'";
                    listSQL.Add(updateSql);
                }

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
                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'DataAddTime\' and ID = (Select[ID] from SysObjects where Name = \'Goods\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table Goods Add DataAddTime datetime " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'数据加入时间\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'Goods\', @level2type = N\'COLUMN\',@level2name = N\'DataAddTime\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'DataUpdateTime\' and ID = (Select[ID] from SysObjects where Name = \'BmsAccountSaleDoc\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table BmsAccountSaleDoc Add DataUpdateTime datetime " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'数据更新时间\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'BmsAccountSaleDoc\', @level2type = N\'COLUMN\',@level2name = N\'DataUpdateTime\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'DataAddTime\' and ID = (Select[ID] from SysObjects where Name = \'CenMsg\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table CenMsg Add DataAddTime datetime " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'数据加入时间\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'CenMsg\', @level2type = N\'COLUMN\',@level2name = N\'DataAddTime\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'DataUpdateTime\' and ID = (Select[ID] from SysObjects where Name = \'CenMsg\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table CenMsg Add DataUpdateTime datetime " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'数据更新时间\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'CenMsg\', @level2type = N\'COLUMN\',@level2name = N\'DataUpdateTime\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'DataAddTime\' and ID = (Select[ID] from SysObjects where Name = \'CenOrgCondition\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table CenOrgCondition Add DataAddTime datetime " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'数据加入时间\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'CenOrgCondition\', @level2type = N\'COLUMN\',@level2name = N\'DataAddTime\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'DataUpdateTime\' and ID = (Select[ID] from SysObjects where Name = \'CenOrgCondition\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table CenOrgCondition Add DataUpdateTime datetime " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'数据更新时间\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'CenOrgCondition\', @level2type = N\'COLUMN\',@level2name = N\'DataUpdateTime\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'DataAddTime\' and ID = (Select[ID] from SysObjects where Name = \'CenOrgType\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table CenOrgType Add DataAddTime datetime " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'数据加入时间\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'CenOrgType\', @level2type = N\'COLUMN\',@level2name = N\'DataAddTime\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'DataUpdateTime\' and ID = (Select[ID] from SysObjects where Name = \'CenOrgType\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table CenOrgType Add DataUpdateTime datetime " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'数据更新时间\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'CenOrgType\', @level2type = N\'COLUMN\',@level2name = N\'DataUpdateTime\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'DataAddTime\' and ID = (Select[ID] from SysObjects where Name = \'CenQtyDtl\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table CenQtyDtl Add DataAddTime datetime " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'数据加入时间\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'CenQtyDtl\', @level2type = N\'COLUMN\',@level2name = N\'DataAddTime\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'DataUpdateTime\' and ID = (Select[ID] from SysObjects where Name = \'CenQtyDtl\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table CenQtyDtl Add DataUpdateTime datetime " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'数据更新时间\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'CenQtyDtl\', @level2type = N\'COLUMN\',@level2name = N\'DataUpdateTime\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'DataAddTime\' and ID = (Select[ID] from SysObjects where Name = \'CenQtyDtlTemp\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table CenQtyDtlTemp Add DataAddTime datetime " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'数据加入时间\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'CenQtyDtlTemp\', @level2type = N\'COLUMN\',@level2name = N\'DataAddTime\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'DataUpdateTime\' and ID = (Select[ID] from SysObjects where Name = \'CenQtyDtlTemp\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table CenQtyDtlTemp Add DataUpdateTime datetime " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'数据更新时间\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'CenQtyDtlTemp\', @level2type = N\'COLUMN\',@level2name = N\'DataUpdateTime\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'DataAddTime\' and ID = (Select[ID] from SysObjects where Name = \'CenQtyDtlTempHistory\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table CenQtyDtlTempHistory Add DataAddTime datetime " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'数据加入时间\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'CenQtyDtlTempHistory\', @level2type = N\'COLUMN\',@level2name = N\'DataAddTime\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'DataUpdateTime\' and ID = (Select[ID] from SysObjects where Name = \'CenQtyDtlTempHistory\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table CenQtyDtlTempHistory Add DataUpdateTime datetime " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'数据更新时间\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'CenQtyDtlTempHistory\', @level2type = N\'COLUMN\',@level2name = N\'DataUpdateTime\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'DataTimeStamp\' and ID = (Select[ID] from SysObjects where Name = \'Rea_OrderGoods\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table Rea_OrderGoods Add DataTimeStamp timestamp " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'时间戳\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'Rea_OrderGoods\', @level2type = N\'COLUMN\',@level2name = N\'DataTimeStamp\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'DataTimeStamp\' and ID = (Select[ID] from SysObjects where Name = \'Rea_Goods\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table Rea_Goods Add DataTimeStamp timestamp " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'时间戳\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'Rea_Goods\', @level2type = N\'COLUMN\',@level2name = N\'DataTimeStamp\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'DataTimeStamp\' and ID = (Select[ID] from SysObjects where Name = \'Rea_CenOrg\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table Rea_CenOrg Add DataTimeStamp timestamp " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'时间戳\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'Rea_CenOrg\', @level2type = N\'COLUMN\',@level2name = N\'DataTimeStamp\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'OperateObjectTypeCode\' and ID = (Select[ID] from SysObjects where Name = \'B_SampleOperate\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table B_SampleOperate Add OperateObjectTypeCode varchar(200) " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'实体代码\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'B_SampleOperate\', @level2type = N\'COLUMN\',@level2name = N\'OperateObjectTypeCode\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'OperateTypeCode\' and ID = (Select[ID] from SysObjects where Name = \'B_SampleOperate\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table B_SampleOperate Add OperateTypeCode varchar(200) " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'操作代码\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'B_SampleOperate\', @level2type = N\'COLUMN\',@level2name = N\'OperateTypeCode\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'OtherOrderDocNo\' and ID = (Select[ID] from SysObjects where Name = \'BmsCenOrderDtl\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table BmsCenOrderDtl Add OtherOrderDocNo varchar(200) " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'第三方订货单编码\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'BmsCenOrderDtl\', @level2type = N\'COLUMN\',@level2name = N\'OtherOrderDocNo\'" + "\r" +
                            "end";
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
                string updateSql = "";
                List<string> listSQL = new List<string>();
                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'OrderDocID\' and ID = (Select[ID] from SysObjects where Name = \'BmsCenSaleDoc\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table BmsCenSaleDoc Add OrderDocID bigint " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'订货单ID\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'BmsCenSaleDoc\', @level2type = N\'COLUMN\',@level2name = N\'OrderDocID\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'OrderDocNo\' and ID = (Select[ID] from SysObjects where Name = \'BmsCenSaleDoc\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table BmsCenSaleDoc Add OrderDocNo varchar(50) " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'订货单编码\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'BmsCenSaleDoc\', @level2type = N\'COLUMN\',@level2name = N\'OrderDocNo\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'OtherOrderDocNo\' and ID = (Select[ID] from SysObjects where Name = \'BmsCenSaleDoc\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table BmsCenSaleDoc Add OtherOrderDocNo varchar(50) " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'第三方订货单编码\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'BmsCenSaleDoc\', @level2type = N\'COLUMN\',@level2name = N\'OtherOrderDocNo\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "ALTER TABLE [dbo].[BmsCenSaleDoc]  WITH CHECK ADD  CONSTRAINT [FK_BmsCenSaleDoc_BmsCenOrderDoc_OrderDocID] FOREIGN KEY([OrderDocID]) REFERENCES[dbo].[BmsCenOrderDoc]([OrderDocID])";
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
                string updateSql = "";
                List<string> listSQL = new List<string>();
                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'AcceptCount\' and ID = (Select[ID] from SysObjects where Name = \'BmsCenSaleDtl\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table BmsCenSaleDtl Add AcceptCount int " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'验收数量\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'BmsCenSaleDtl\', @level2type = N\'COLUMN\',@level2name = N\'AcceptCount\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'RefuseCount\' and ID = (Select[ID] from SysObjects where Name = \'BmsCenSaleDtl\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table BmsCenSaleDtl Add RefuseCount int " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'拒收数量\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'BmsCenSaleDtl\', @level2type = N\'COLUMN\',@level2name = N\'RefuseCount\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'AcceptFlag\' and ID = (Select[ID] from SysObjects where Name = \'BmsCenSaleDtl\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table BmsCenSaleDtl Add AcceptFlag int " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'验收标志\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'BmsCenSaleDtl\', @level2type = N\'COLUMN\',@level2name = N\'AcceptFlag\'" + "\r" +
                            "end";
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
                string updateSql = "";
                List<string> listSQL = new List<string>();
                updateSql = "if Exists(Select * from SysColumns where [Name] = \'ParaValue\' and ID = (Select[ID] from SysObjects where Name = \'B_Parameter\')) " + "\r" +
                            "begin " + "\r" +
                                "alter table B_Parameter alter column ParaValue varchar(max) " + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Exists(Select * from SysColumns where [Name] = \'Name\' and ID = (Select[ID] from SysObjects where Name = \'B_Parameter\')) " + "\r" +
                            "begin " + "\r" +
                                "alter table B_Parameter alter column Name varchar(200) " + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Exists(Select * from SysColumns where [Name] = \'ParaType\' and ID = (Select[ID] from SysObjects where Name = \'B_Parameter\')) " + "\r" +
                            "begin " + "\r" +
                                "alter table B_Parameter alter column ParaType varchar(100) " + "\r" +
                            "end";
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
                string updateSql = "";
                List<string> listSQL = new List<string>();
                updateSql = "IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[B_DictTree]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) PRINT '存在' ELSE begin CREATE TABLE[dbo].[B_DictTree](	[LabID]   [dbo].[D_实验室ID]    NOT NULL,   [TypeTreeID] [dbo].[D_系统主键]  NOT NULL,   [ParentID] [dbo].[D_系统主键]    NULL,	[CName]  [varchar](100) NOT NULL,    [SName] [varchar](40) NULL,	[Shortcode] [dbo].[D_快捷码]  NULL,	[Memo] [varchar](500) NULL,	[DispOrder] [int] NULL,	[IsUse]  [bit] NULL,[CreatorID]        [dbo].[D_系统主键]   NULL,	[CreatorName]  [varchar](50) NULL,	[DataAddTime]   [datetime]   NULL,	[DataUpdateTime] [datetime] NULL,[DataTimeStamp]  [timestamp] NULL,	[StandCode] [varchar](50) NULL,	[DeveCode] [varchar](50) NULL, CONSTRAINT[PK_F_TYPETREE] PRIMARY KEY CLUSTERED([TypeTreeID] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]) ON[PRIMARY] SET ANSI_PADDING OFF  ALTER TABLE[dbo].[B_DictTree]   WITH CHECK ADD CONSTRAINT[FK_F_TypeTree_HR_Employee] FOREIGN KEY([CreatorID])REFERENCES[dbo].[HR_Employee] ([EmpID])   ALTER TABLE[dbo].[B_DictTree] CHECK CONSTRAINT[FK_F_TypeTree_HR_Employee] end";
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
                string updateSql = "";
                List<string> listSQL = new List<string>();
                updateSql = "if Exists(Select * from SysColumns where [Name] = \'Name\' and ID = (Select[ID] from SysObjects where Name = \'B_OperateObjectType\')) " + "\r" +
                            "begin " + "\r" +
                                "alter table B_OperateObjectType alter column Name varchar(100) " + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Exists(Select * from SysColumns where [Name] = \'SName\' and ID = (Select[ID] from SysObjects where Name = \'B_OperateObjectType\')) " + "\r" +
                            "begin " + "\r" +
                                "alter table B_OperateObjectType alter column SName varchar(100) " + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Exists(Select * from SysColumns where [Name] = \'Shortcode\' and ID = (Select[ID] from SysObjects where Name = \'B_OperateObjectType\')) " + "\r" +
                            "begin " + "\r" +
                                "alter table B_OperateObjectType alter column Shortcode varchar(100) " + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Exists(Select * from SysColumns where [Name] = \'Name\' and ID = (Select[ID] from SysObjects where Name = \'B_SampleOperateType\')) " + "\r" +
                            "begin " + "\r" +
                                "alter table B_SampleOperateType alter column Name varchar(100) " + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Exists(Select * from SysColumns where [Name] = \'SName\' and ID = (Select[ID] from SysObjects where Name = \'B_SampleOperateType\')) " + "\r" +
                            "begin " + "\r" +
                                "alter table B_SampleOperateType alter column SName varchar(100) " + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Exists(Select * from SysColumns where [Name] = \'Shortcode\' and ID = (Select[ID] from SysObjects where Name = \'B_SampleOperateType\')) " + "\r" +
                            "begin " + "\r" +
                                "alter table B_SampleOperateType alter column Shortcode varchar(100) " + "\r" +
                            "end";
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
                string updateSql = "";
                List<string> listSQL = new List<string>();
                updateSql = "IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[S_ServiceClient]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) PRINT '存在' ELSE begin CREATE TABLE [dbo].[S_ServiceClient](	[LabID] [dbo].[D_系统主键] NOT NULL,	[Name] [nvarchar](500) NULL,	[SName] [varchar](500) NULL,	[Shortcode] [dbo].[D_快捷码] NULL,	[PinYinZiTou] [dbo].[D_汉语拼音字头] NULL,	[DispOrder] [int] NULL,	[Comment] [varchar](max) NULL,	[CountryID] [dbo].[D_系统主键] NULL,	[ProvinceID] [dbo].[D_系统主键] NULL,	[CityID] [dbo].[D_系统主键] NULL,	[CountryName] [varchar](40) NULL,	[ProvinceName] [varchar](40) NULL,	[CityName] [varchar](40) NULL,	[SClevelID] [dbo].[D_系统主键] NULL,	[Principal] [varchar](40) NULL,	[LinkMan] [varchar](40) NULL,	[PhoneNum] [varchar](40) NULL,	[PhoneNum2] [varchar](40) NULL,	[Address] [varchar](150) NULL,	[MailNo] [varchar](40) NULL,	[Emall] [varchar](40) NULL,	[ClientType] [bigint] NULL,	[Bman] [varchar](40) NULL,	[UploadType] [varchar](40) NULL,	[InputDataType] [varchar](40) NULL,	[ClientArea] [varchar](40) NULL,	[ClientStyleID] [bigint] NULL,	[ClientStyleName] [varchar](40) NULL,	[WebLisSourceOrgID] [varchar](30) NULL,	[GroupName] [varchar](50) NULL,	[IsUse] [bit] NULL,	[DataAddTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_S_SERVICECLIENT] PRIMARY KEY CLUSTERED (	[LabID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] end";
                listSQL.Add(updateSql);

                updateSql = "IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[S_ServiceClientlevel]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) PRINT '存在' ELSE begin CREATE TABLE [dbo].[S_ServiceClientlevel](	[SClevelID] [dbo].[D_系统主键] NOT NULL,	[Name] [varchar](40) NULL,	[SName] [varchar](40) NULL,	[Code] [varchar](20) NULL,	[Shortcode] [dbo].[D_快捷码] NULL,	[PinYinZiTou] [dbo].[D_汉语拼音字头] NULL,	[DispOrder] [int] NULL,	[Comment] [varchar](max) NULL,	[IsUse] [bit] NULL,	[DataAddTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_S_SERVICECLIENTLEVEL] PRIMARY KEY CLUSTERED (	[SClevelID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] end";
                listSQL.Add(updateSql);

                updateSql = "IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Rea_BmsInDtl]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) PRINT '存在' ELSE begin CREATE TABLE [dbo].[Rea_BmsInDtl](	[LabID] [dbo].[D_系统主键] NULL,	[InDtlID] [dbo].[D_系统主键] NOT NULL,	[InDtlNo] [varchar](20) NULL,	[InDocID] [dbo].[D_系统主键] NOT NULL,	[InDocNo] [varchar](20) NULL,	[GoodsID] [dbo].[D_系统主键] NULL,	[GoodsCName] [varchar](200) NOT NULL,	[SerialNo] [varchar](50) NULL,	[GoodsUnitID] [dbo].[D_系统主键] NULL,	[GoodsUnit] [varchar](50) NULL,	[GoodsQty] [float] NULL,	[Price] [float] NULL,	[SumTotal] [float] NULL,	[TaxRate] [float] NULL,	[LotNo] [varchar](100) NULL,	[StorageID] [dbo].[D_系统主键] NULL,	[PlaceID] [dbo].[D_系统主键] NULL,	[GoodsSerial] [varchar](100) NULL,	[PackSerial] [varchar](100) NULL,	[LotSerial] [varchar](100) NULL,	[MixSerial] [varchar](100) NULL,	[ZX1] [varchar](50) NULL,	[ZX2] [varchar](50) NULL,	[ZX3] [varchar](50) NULL,	[DispOrder] [int] NULL,	[Memo] [varchar](max) NULL,	[Visible] [bit] NULL,	[CreaterID] [bigint] NULL,	[CreaterName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NOT NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_REA_BMSINDTL] PRIMARY KEY CLUSTERED (	[InDtlID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] end";
                listSQL.Add(updateSql);

                updateSql = "IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Rea_Place]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) PRINT '存在' ELSE begin CREATE TABLE [dbo].[Rea_Place](	[LabID] [dbo].[D_系统主键] NULL,	[PlaceID] [dbo].[D_系统主键] NOT NULL,	[StorageID] [dbo].[D_系统主键] NULL,	[CName] [varchar](20) NULL,	[ShortCode] [dbo].[D_系统主键] NULL,	[Memo] [varchar](max) NULL,	[ZX1] [varchar](50) NULL,	[ZX2] [varchar](50) NULL,	[ZX3] [varchar](50) NULL,	[DispOrder] [int] NULL,	[Visible] [bit] NULL,	[CreaterID] [bigint] NULL,	[CreaterName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NOT NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_REA_PLACE] PRIMARY KEY CLUSTERED (	[PlaceID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] end";
                listSQL.Add(updateSql);

                updateSql = "IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Rea_Storage]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) PRINT '存在' ELSE begin CREATE TABLE [dbo].[Rea_Storage](	[LabID] [dbo].[D_系统主键] NULL,	[StorageID] [dbo].[D_系统主键] NOT NULL,	[CName] [varchar](20) NULL,	[ShortCode] [dbo].[D_系统主键] NULL,	[Memo] [varchar](max) NULL,	[ZX1] [varchar](50) NULL,	[ZX2] [varchar](50) NULL,	[ZX3] [varchar](50) NULL,	[DispOrder] [int] NULL,	[Visible] [bit] NULL,	[CreaterID] [bigint] NULL,	[CreaterName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NOT NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_REA_STORAGE] PRIMARY KEY CLUSTERED (	[StorageID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] end";
                listSQL.Add(updateSql);

                updateSql = "IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Rea_BmsInDoc]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) PRINT '存在' ELSE begin CREATE TABLE [dbo].[Rea_BmsInDoc](	[LabID] [dbo].[D_系统主键] NULL,	[InDocID] [dbo].[D_系统主键] NOT NULL,	[OrgID] [bigint] NULL,	[InDocNo] [varchar](20) NULL,	[CompanyID] [dbo].[D_系统主键] NULL,	[CompanyName] [varchar](200) NULL,	[Carrier] [varchar](50) NULL,	[UserID] [dbo].[D_系统主键] NULL,	[InvoiceNo] [varchar](30) NULL,	[Status] [int] NULL,	[OperDate] [datetime] NULL,	[PrintTimes] [int] NULL,	[TotalPrice] [float] NULL,	[ZX1] [varchar](50) NULL,	[ZX2] [varchar](50) NULL,	[ZX3] [varchar](50) NULL,	[DispOrder] [int] NULL,	[Memo] [varchar](max) NULL,	[Visible] [bit] NULL,	[CreaterID] [bigint] NULL,	[CreaterName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NOT NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_REA_BMSINDOC] PRIMARY KEY CLUSTERED (	[InDocID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] end";
                listSQL.Add(updateSql);

                updateSql = "IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Rea_Req_Operation]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) PRINT '存在' ELSE begin CREATE TABLE [dbo].[Rea_Req_Operation](	[LabID] [dbo].[D_系统主键] NOT NULL,	[ReaROperationID] [bigint] NOT NULL,	[BobjectID] [dbo].[D_系统主键] NOT NULL,	[Type] [bigint] NULL,	[TypeName] [varchar](20) NULL,	[BusinessModuleCode] [varchar](20) NULL,	[Memo] [varchar](500) NULL,	[DispOrder] [int] NULL,	[IsUse] [bit] NULL,	[CreatorID] [dbo].[D_系统主键] NULL,	[CreatorName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_REA_REQ_OPERATION] PRIMARY KEY CLUSTERED (	[ReaROperationID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] end";
                listSQL.Add(updateSql);

                updateSql = "IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Rea_GoodsRegister]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) PRINT '存在' ELSE begin CREATE TABLE [dbo].[Rea_GoodsRegister](	[LabID] [bigint] NOT NULL,	[RegisterID] [bigint] NOT NULL,	[GoodsID] [bigint] NULL,	[FactoryName] [varchar](200) NULL,	[GoodsNo] [varchar](50) NULL,	[CName] [varchar](200) NULL,	[EName] [varchar](200) NULL,	[ShortCode] [varchar](100) NULL,	[GoodsLotNo] [varchar](200) NULL,	[RegisterNo] [varchar](200) NULL,	[RegisterDate] [datetime] NULL,	[RegisterInvalidDate] [datetime] NULL,	[RegisterFilePath] [varchar](500) NULL,	[DispOrder] [int] NULL,	[Visible] [int] NULL,	[ZX1] [varchar](100) NULL,	[ZX2] [varchar](100) NULL,	[ZX3] [varchar](100) NULL,	[EmpID] [bigint] NULL,	[EmpName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_Rea_GoodsRegister] PRIMARY KEY CLUSTERED (	[RegisterID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] end";
                listSQL.Add(updateSql);

                updateSql = "IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Rea_GoodsLot]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) PRINT '存在' ELSE begin CREATE TABLE [dbo].[Rea_GoodsLot](	[LabID] [dbo].[D_系统主键] NULL,	[GoodsLotID] [dbo].[D_系统主键] NOT NULL,	[GoodsID] [bigint] NULL,	[GoodsCName] [varchar](200) NOT NULL,	[LotNo] [varchar](20) NULL,	[ProdDate] [datetime] NULL,	[InvalidDate] [datetime] NULL,	[DispOrder] [int] NULL,	[Memo] [varchar](max) NULL,	[Visible] [bit] NULL,	[CreaterID] [bigint] NULL,	[CreaterName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NOT NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_REA_GOODSLOT] PRIMARY KEY CLUSTERED (	[GoodsLotID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] end";
                listSQL.Add(updateSql);

                updateSql = "IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Rea_DeptGoods]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) PRINT '存在' ELSE begin CREATE TABLE [dbo].[Rea_DeptGoods](	[LabID] [dbo].[D_系统主键] NULL,	[DeptGoodsID] [dbo].[D_系统主键] NOT NULL,	[GoodsCName] [varchar](200) NOT NULL,	[DeptID] [dbo].[D_系统主键] NULL,	[GoodsID] [bigint] NULL,	[DeptName] [varchar](50) NULL,	[DispOrder] [int] NULL,	[Memo] [varchar](max) NULL,	[Visible] [bit] NULL,	[CreaterID] [bigint] NULL,	[CreaterName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_REA_DEPTGOODS] PRIMARY KEY CLUSTERED (	[DeptGoodsID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] end";
                listSQL.Add(updateSql);

                updateSql = "IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Rea_BmsReqDtl]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) PRINT '存在' ELSE begin CREATE TABLE [dbo].[Rea_BmsReqDtl](	[LabID] [dbo].[D_系统主键] NULL,	[ReqDtlID] [dbo].[D_系统主键] NOT NULL,	[ReqDtlNo] [varchar](20) NULL,	[ReqDocID] [dbo].[D_系统主键] NULL,	[ReqDocNo] [varchar](20) NULL,	[GoodsCName] [varchar](200) NOT NULL,	[GoodsUnitID] [dbo].[D_系统主键] NULL,	[GoodsID] [bigint] NULL,	[GoodsUnit] [varchar](50) NULL,	[GoodsQty] [float] NULL,	[OrderFlag] [int] NULL,	[OrderDtlNo] [varchar](20) NULL,	[ZX1] [varchar](50) NULL,	[ZX2] [varchar](50) NULL,	[ZX3] [varchar](50) NULL,	[OrgID] [bigint] NOT NULL,	[OrgName] [varchar](100) NULL,	[DispOrder] [int] NULL,	[Memo] [varchar](max) NULL,	[Visible] [bit] NULL,	[CreaterID] [bigint] NULL,	[CreaterName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NOT NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_REA_BMSREQDTL] PRIMARY KEY CLUSTERED (	[ReqDtlID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] end";
                listSQL.Add(updateSql);

                updateSql = "IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Rea_GoodsUnit]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) PRINT '存在' ELSE begin CREATE TABLE [dbo].[Rea_GoodsUnit](	[LabID] [dbo].[D_系统主键] NULL,	[GoodsUnitID] [dbo].[D_系统主键] NOT NULL,	[GoodsUnit] [varchar](50) NULL,	[ChangeUnitID] [dbo].[D_系统主键] NULL,	[GoodsID] [bigint] NULL,	[ChangeUnit] [varchar](50) NULL,	[ChangeQty] [dbo].[D_系统主键] NULL,	[GoodsCName] [varchar](200) NOT NULL,	[DispOrder] [int] NULL,	[Memo] [varchar](max) NULL,	[Visible] [bit] NULL,	[CreaterID] [bigint] NULL,	[CreaterName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NOT NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_REA_GOODSUNIT] PRIMARY KEY CLUSTERED (	[GoodsUnitID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] end";
                listSQL.Add(updateSql);

                updateSql = "IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Rea_BmsReqDoc]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) PRINT '存在' ELSE begin CREATE TABLE [dbo].[Rea_BmsReqDoc](	[LabID] [dbo].[D_系统主键] NULL,	[ReqDocID] [dbo].[D_系统主键] NOT NULL,	[ReqDocNo] [varchar](20) NULL,	[DeptID] [dbo].[D_系统主键] NULL,	[UrgentFlag] [int] NULL,	[Status] [int] NULL,	[OperDate] [datetime] NULL,	[PrintTimes] [int] NULL,	[ZX1] [varchar](50) NULL,	[ZX2] [varchar](50) NULL,	[ZX3] [varchar](50) NULL,	[DispOrder] [int] NULL,	[Memo] [varchar](max) NULL,	[Visible] [bit] NULL,	[ApplyID] [bigint] NULL,	[ApplyName] [varchar](50) NULL,	[ApplyTime] [datetime] NULL,	[ReviewManID] [bigint] NULL,	[ReviewManName] [varchar](50) NULL,	[ReviewTime] [datetime] NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NOT NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_REA_BMSREQDOC] PRIMARY KEY CLUSTERED (	[ReqDocID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] end";
                listSQL.Add(updateSql);

                updateSql = "ALTER TABLE [dbo].[S_ServiceClient]  WITH CHECK ADD  CONSTRAINT [FK_S_SERVIC_REFERENCE_B_CITY] FOREIGN KEY([CityID]) REFERENCES [dbo].[B_City] ([CityID]);ALTER TABLE [dbo].[S_ServiceClient] CHECK CONSTRAINT [FK_S_SERVIC_REFERENCE_B_CITY];ALTER TABLE[dbo].[S_ServiceClient]        WITH CHECK ADD CONSTRAINT[FK_S_SERVIC_REFERENCE_B_COUNTR] FOREIGN KEY([CountryID]) REFERENCES[dbo].[B_Country] ([CountryID]);ALTER TABLE[dbo].[S_ServiceClient]        CHECK CONSTRAINT[FK_S_SERVIC_REFERENCE_B_COUNTR];        ALTER TABLE[dbo].[S_ServiceClient]        WITH CHECK ADD CONSTRAINT[FK_S_SERVIC_REFERENCE_B_PROVIN] FOREIGN KEY([ProvinceID]) REFERENCES[dbo].[B_Province] ([ProvinceID]);ALTER TABLE[dbo].[S_ServiceClient]        CHECK CONSTRAINT[FK_S_SERVIC_REFERENCE_B_PROVIN];        ALTER TABLE[dbo].[S_ServiceClient]        WITH CHECK ADD CONSTRAINT[FK_S_SERVIC_REFERENCE_S_SERVIC] FOREIGN KEY([SClevelID]) REFERENCES[dbo].[S_ServiceClientlevel] ([SClevelID]);ALTER TABLE[dbo].[S_ServiceClient]        CHECK CONSTRAINT[FK_S_SERVIC_REFERENCE_S_SERVIC];        ALTER TABLE[dbo].[Rea_BmsInDtl]        WITH CHECK ADD CONSTRAINT[FK_REA_BMSI_REFERENCE_REA_BMSI] FOREIGN KEY([InDocID]) REFERENCES[dbo].[Rea_BmsInDoc] ([InDocID]);ALTER TABLE[dbo].[Rea_BmsInDtl]        CHECK CONSTRAINT[FK_REA_BMSI_REFERENCE_REA_BMSI];        ALTER TABLE[dbo].[Rea_BmsInDtl]        WITH CHECK ADD CONSTRAINT[FK_REA_BMSI_REFERENCE_REA_GOOD] FOREIGN KEY([GoodsID]) REFERENCES[dbo].[Rea_Goods] ([GoodsID]);ALTER TABLE[dbo].[Rea_BmsInDtl]        CHECK CONSTRAINT[FK_REA_BMSI_REFERENCE_REA_GOOD];        ALTER TABLE[dbo].[Rea_BmsInDtl]        WITH CHECK ADD CONSTRAINT[FK_REA_BMSI_REFERENCE_REA_GOOD1] FOREIGN KEY([GoodsUnitID]) REFERENCES[dbo].[Rea_GoodsUnit] ([GoodsUnitID]);ALTER TABLE[dbo].[Rea_BmsInDtl]        CHECK CONSTRAINT[FK_REA_BMSI_REFERENCE_REA_GOOD1];        ALTER TABLE[dbo].[Rea_BmsInDtl]        WITH CHECK ADD CONSTRAINT[FK_REA_BMSI_REFERENCE_REA_PLAC] FOREIGN KEY([PlaceID]) REFERENCES[dbo].[Rea_Place] ([PlaceID]);ALTER TABLE[dbo].[Rea_BmsInDtl]        CHECK CONSTRAINT[FK_REA_BMSI_REFERENCE_REA_PLAC];        ALTER TABLE[dbo].[Rea_BmsInDtl]        WITH CHECK ADD CONSTRAINT[FK_REA_BMSI_REFERENCE_REA_STOR] FOREIGN KEY([StorageID]) REFERENCES[dbo].[Rea_Storage] ([StorageID]);ALTER TABLE[dbo].[Rea_BmsInDtl]        CHECK CONSTRAINT[FK_REA_BMSI_REFERENCE_REA_STOR];        ALTER TABLE[dbo].[Rea_Place]        WITH CHECK ADD CONSTRAINT[FK_REA_PLAC_REFERENCE_REA_STOR] FOREIGN KEY([StorageID]) REFERENCES[dbo].[Rea_Storage] ([StorageID]);ALTER TABLE[dbo].[Rea_Place]        CHECK CONSTRAINT[FK_REA_PLAC_REFERENCE_REA_STOR];        ALTER TABLE[dbo].[Rea_BmsInDoc]        WITH CHECK ADD CONSTRAINT[FK_REA_BMSI_REFERENCE_REA_CENO] FOREIGN KEY([OrgID]) REFERENCES[dbo].[Rea_CenOrg] ([OrgID]);ALTER TABLE[dbo].[Rea_BmsInDoc]        CHECK CONSTRAINT[FK_REA_BMSI_REFERENCE_REA_CENO];        ALTER TABLE[dbo].[Rea_GoodsRegister]        WITH CHECK ADD CONSTRAINT[FK_REA_GoodsRegister_REFERENCE_REA_GOOD] FOREIGN KEY([GoodsID]) REFERENCES[dbo].[Rea_Goods] ([GoodsID]);ALTER TABLE[dbo].[Rea_GoodsRegister]        CHECK CONSTRAINT[FK_REA_GoodsRegister_REFERENCE_REA_GOOD];        ALTER TABLE[dbo].[Rea_GoodsLot]        WITH CHECK ADD CONSTRAINT[FK_REA_GOOD_REFERENCE_REA_GOOD111] FOREIGN KEY([GoodsID]) REFERENCES[dbo].[Rea_Goods] ([GoodsID]);ALTER TABLE[dbo].[Rea_GoodsLot]        CHECK CONSTRAINT[FK_REA_GOOD_REFERENCE_REA_GOOD111];        ALTER TABLE[dbo].[Rea_DeptGoods]        WITH CHECK ADD CONSTRAINT[FK_REA_DEPT_REFERENCE_HR_DEPT] FOREIGN KEY([DeptID]) REFERENCES[dbo].[HR_Dept] ([DeptID]);ALTER TABLE[dbo].[Rea_DeptGoods]        CHECK CONSTRAINT[FK_REA_DEPT_REFERENCE_HR_DEPT];        ALTER TABLE[dbo].[Rea_DeptGoods]        WITH CHECK ADD CONSTRAINT[FK_REA_DEPT_REFERENCE_REA_GOOD] FOREIGN KEY([GoodsID]) REFERENCES[dbo].[Rea_Goods] ([GoodsID]);ALTER TABLE[dbo].[Rea_DeptGoods]        CHECK CONSTRAINT[FK_REA_DEPT_REFERENCE_REA_GOOD];        ALTER TABLE[dbo].[Rea_BmsReqDtl]        WITH CHECK ADD CONSTRAINT[FK_REA_BMSR_REFERENCE_REA_BMSR] FOREIGN KEY([ReqDocID]) REFERENCES[dbo].[Rea_BmsReqDoc] ([ReqDocID]);ALTER TABLE[dbo].[Rea_BmsReqDtl]        CHECK CONSTRAINT[FK_REA_BMSR_REFERENCE_REA_BMSR];        ALTER TABLE[dbo].[Rea_BmsReqDtl]        WITH CHECK ADD CONSTRAINT[FK_REA_BMSR_REFERENCE_REA_CENO] FOREIGN KEY([OrgID]) REFERENCES[dbo].[Rea_CenOrg] ([OrgID]);ALTER TABLE[dbo].[Rea_BmsReqDtl]        CHECK CONSTRAINT[FK_REA_BMSR_REFERENCE_REA_CENO];        ALTER TABLE[dbo].[Rea_BmsReqDtl]        WITH CHECK ADD CONSTRAINT[FK_REA_BMSR_REFERENCE_REA_GOOD] FOREIGN KEY([GoodsUnitID]) REFERENCES[dbo].[Rea_GoodsUnit] ([GoodsUnitID]);ALTER TABLE[dbo].[Rea_BmsReqDtl]        CHECK CONSTRAINT[FK_REA_BMSR_REFERENCE_REA_GOOD];        ALTER TABLE[dbo].[Rea_BmsReqDtl]        WITH CHECK ADD CONSTRAINT[FK_REA_BMSR_REFERENCE_REA_GOOD1] FOREIGN KEY([GoodsID]) REFERENCES[dbo].[Rea_Goods] ([GoodsID]);ALTER TABLE[dbo].[Rea_BmsReqDtl]        CHECK CONSTRAINT[FK_REA_BMSR_REFERENCE_REA_GOOD1];        ALTER TABLE[dbo].[Rea_GoodsUnit]        WITH CHECK ADD CONSTRAINT[FK_REA_GOOD_REFERENCE_REA_GOOD] FOREIGN KEY([ChangeUnitID]) REFERENCES[dbo].[Rea_GoodsUnit] ([GoodsUnitID]);ALTER TABLE[dbo].[Rea_GoodsUnit]        CHECK CONSTRAINT[FK_REA_GOOD_REFERENCE_REA_GOOD];        ALTER TABLE[dbo].[Rea_GoodsUnit]        WITH CHECK ADD CONSTRAINT[FK_REA_GOOD_REFERENCE_REA_GOOD1] FOREIGN KEY([GoodsID]) REFERENCES[dbo].[Rea_Goods] ([GoodsID]);ALTER TABLE[dbo].[Rea_GoodsUnit]        CHECK CONSTRAINT[FK_REA_GOOD_REFERENCE_REA_GOOD1];        ALTER TABLE[dbo].[Rea_BmsReqDoc]        WITH CHECK ADD CONSTRAINT[FK_REA_BMSR_REFERENCE_HR_DEPT] FOREIGN KEY([DeptID]) REFERENCES[dbo].[HR_Dept] ([DeptID]);ALTER TABLE[dbo].[Rea_BmsReqDoc]        CHECK CONSTRAINT[FK_REA_BMSR_REFERENCE_HR_DEPT];";
                listSQL.Add(updateSql);

                updateSql = "IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[SC_Attachment]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) PRINT '存在' ELSE begin CREATE TABLE [dbo].[SC_Attachment](	[LabID] [dbo].[D_实验室ID] NOT NULL,	[SCAttachmentID] [bigint] NOT NULL,	[BobjectID] [dbo].[D_系统主键] NULL,	[FileName] [varchar](100) NULL,	[FileExt] [varchar](100) NULL,	[FileSize] [bigint] NULL,	[FilePath] [varchar](200) NULL,	[Memo] [varchar](500) NULL,	[DispOrder] [int] NULL,	[IsUse] [bit] NULL,	[CreatorID] [dbo].[D_系统主键] NULL,	[CreatorName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL,	[NewFileName] [varchar](100) NULL,	[FileType] [varchar](100) NULL) ON [PRIMARY] SET ANSI_PADDING ON ALTER TABLE [dbo].[SC_Attachment] ADD [BusinessModuleCode] [varchar](50) NULL CONSTRAINT [PK_SC_ATTACHMENT] PRIMARY KEY CLUSTERED (	[SCAttachmentID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] end";
                listSQL.Add(updateSql);

                updateSql = "IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[SC_Operation]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) PRINT '存在' ELSE begin CREATE TABLE [dbo].[SC_Operation](	[LabID] [dbo].[D_实验室ID] NOT NULL,	[SCOperationID] [bigint] NOT NULL,	[BobjectID] [dbo].[D_系统主键] NOT NULL,	[Type] [bigint] NULL,	[Memo] [varchar](500) NULL,	[DispOrder] [int] NULL,	[IsUse] [bit] NULL,	[CreatorID] [dbo].[D_系统主键] NULL,	[CreatorName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL) ON [PRIMARY] SET ANSI_PADDING ON ALTER TABLE [dbo].[SC_Operation] ADD [TypeName] [varchar](50) NULL ALTER TABLE[dbo].[SC_Operation] ADD[BusinessModuleCode][varchar](50) NULL CONSTRAINT[PK_SC_OPERATION] PRIMARY KEY CLUSTERED([SCOperationID] ASC)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] end";
                listSQL.Add(updateSql);

                updateSql = "IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Rea_CheckIn_Operation]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) PRINT '存在' ELSE begin CREATE TABLE [dbo].[Rea_CheckIn_Operation](	[LabID] [dbo].[D_系统主键] NOT NULL,	[ReaROperationID] [bigint] NOT NULL,	[BobjectID] [dbo].[D_系统主键] NOT NULL,	[Type] [bigint] NULL,	[TypeName] [varchar](20) NULL,	[BusinessModuleCode] [varchar](20) NULL,	[Memo] [varchar](500) NULL,	[DispOrder] [int] NULL,	[IsUse] [bit] NULL,	[CreatorID] [dbo].[D_系统主键] NULL,	[CreatorName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_REA_CheckIn_OPERATION] PRIMARY KEY CLUSTERED (	[ReaROperationID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] end";
                listSQL.Add(updateSql);

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
                updateSql = " if not exists (select * from dbo.sysobjects where id = object_id(N\'[dbo].[BmsCenSaleDocConfirm]\')" + "\r" +
                             " and OBJECTPROPERTY(id, N\'IsUserTable\') = 1) BEGIN " + "\r" +
                             " CREATE TABLE[dbo].[BmsCenSaleDocConfirm]( " + "\r" +
                             "    [SaleDocConfirmID][bigint] NOT NULL, " + "\r" +
                             "    [SaleDocConfirmNo] [varchar](50) NULL, " + "\r" +
                             "    [SaleDocID] [bigint] NOT NULL, " + "\r" +
                             "    [SaleDocNo] [varchar](50) NULL, " + "\r" +
                             "    [LabID] [bigint] NULL, " + "\r" +
                             "    [LabName] [varchar](100) NULL, " + "\r" +
                             "    [CompID] [bigint] NULL, " + "\r" +
                             "    [CompanyName] [varchar](200) NULL, " + "\r" +
                             "    [Status] [int] NULL, " + "\r" +
                             "   [StatusName] [varchar](100) NULL, " + "\r" +
                             "    [AccepterID] [bigint] NULL, " + "\r" +
                             "   [AccepterName] [varchar](100) NULL, " + "\r" +
                             "   [AcceptTime] [datetime]  NULL, " + "\r" +
                             "   [SecAccepterID] [bigint] NULL, " + "\r" +
                             "    [SecAccepterName]  [varchar](100) NULL, " + "\r" +
                             "   [SecAcceptTime] [datetime] NULL, " + "\r" +
                             "   [AcceptMemo] [varchar](1000) NULL, " + "\r" +
                             "   [IsAcceptError] [bit] NULL, " + "\r" +
                             "   [PrintTimes] [int] NULL, " + "\r" +
                             "   [Memo] [varchar](1000) NULL, " + "\r" +
                             "   [DispOrder] [int] NULL, " + "\r" +
                             "   [DeleteFlag] [int] NULL, " + "\r" +
                             "   [DataUpdateTime]  [datetime] NULL, " + "\r" +
                             "   [DataAddTime] [datetime]  NULL, " + "\r" +
                             "CONSTRAINT[PK_BMSCENSALEDOCCONFIRM] PRIMARY KEY CLUSTERED " + "\r" +
                             "( " + "\r" +
                             "   [SaleDocConfirmID] ASC " + "\r" +
                             ")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] " + "\r" +
                             ") ON[PRIMARY] " + "\r" +
                            " end";
                listSQL.Add(updateSql);
                if (!CheckDataObjectIsExists("BmsCenSaleDocConfirm", "U"))
                {

                    updateSql = " ALTER TABLE [dbo].[BmsCenSaleDocConfirm]  WITH CHECK ADD CONSTRAINT[FK_BMSCENSA_REFERENCE_BMSCENSA] FOREIGN KEY([SaleDocID]) REFERENCES[dbo].[BmsCenSaleDoc] ([SaleDocID])";
                    listSQL.Add(updateSql);

                    updateSql = " ALTER TABLE [dbo].[BmsCenSaleDocConfirm] CHECK CONSTRAINT[FK_BMSCENSA_REFERENCE_BMSCENSA]";
                    listSQL.Add(updateSql);

                    updateSql = " ALTER TABLE [dbo].[BmsCenSaleDocConfirm] WITH CHECK ADD CONSTRAINT[FK_BMSCENSA_REFERENCE_CENORG_CompID] FOREIGN KEY([CompID]) REFERENCES[dbo].[CenOrg] ([OrgID])";
                    listSQL.Add(updateSql);

                    updateSql = " ALTER TABLE [dbo].[BmsCenSaleDocConfirm]  CHECK CONSTRAINT[FK_BMSCENSA_REFERENCE_CENORG_CompID]";
                    listSQL.Add(updateSql);

                    updateSql = " ALTER TABLE [dbo].[BmsCenSaleDocConfirm]  WITH CHECK ADD CONSTRAINT[FK_BMSCENSA_REFERENCE_CENORG_LabID] FOREIGN KEY([LabID]) REFERENCES[dbo].[CenOrg] ([OrgID])";
                    listSQL.Add(updateSql);

                    updateSql = " ALTER TABLE [dbo].[BmsCenSaleDocConfirm]  CHECK CONSTRAINT[FK_BMSCENSA_REFERENCE_CENORG_LabID]";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'供货验收单ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\', @level2type = N\'COLUMN\', @level2name = N\'SaleDocConfirmID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'供货验收单号\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\', @level2type = N\'COLUMN\', @level2name = N\'SaleDocConfirmNo\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'供货总单ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\', @level2type = N\'COLUMN\', @level2name = N\'SaleDocID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'供货单号\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\', @level2type = N\'COLUMN\', @level2name = N\'SaleDocNo\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'实验室ID(关联机构信息表)\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\', @level2type = N\'COLUMN\', @level2name = N\'LabID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'实验室名称\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\', @level2type = N\'COLUMN\', @level2name = N\'LabName\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'供应商ID(关联机构信息表)\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\', @level2type = N\'COLUMN\', @level2name = N\'CompID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'供应商名称\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\', @level2type = N\'COLUMN\', @level2name = N\'CompanyName\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'单据状态\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\', @level2type = N\'COLUMN\', @level2name = N\'Status\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'单据状态描述\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\', @level2type = N\'COLUMN\', @level2name = N\'StatusName\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'主验收人ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\', @level2type = N\'COLUMN\', @level2name = N\'AccepterID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'主验收人\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\', @level2type = N\'COLUMN\', @level2name = N\'AccepterName\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'验收时间\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\', @level2type = N\'COLUMN\', @level2name = N\'AcceptTime\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'次验收人ID\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\', @level2type = N\'COLUMN\', @level2name = N\'SecAccepterID\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'次验收人\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\', @level2type = N\'COLUMN\', @level2name = N\'SecAccepterName\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'次验收时间\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\', @level2type = N\'COLUMN\', @level2name = N\'SecAcceptTime\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'验收备注\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\', @level2type = N\'COLUMN\', @level2name = N\'AcceptMemo\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'是否验收异常\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\', @level2type = N\'COLUMN\', @level2name = N\'IsAcceptError\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'打印次数\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\', @level2type = N\'COLUMN\', @level2name = N\'PrintTimes\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'备注\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\', @level2type = N\'COLUMN\', @level2name = N\'Memo\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'显示次序\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\', @level2type = N\'COLUMN\', @level2name = N\'DispOrder\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'删除标志\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\', @level2type = N\'COLUMN\', @level2name = N\'DeleteFlag\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'数据更新时间\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\', @level2type = N\'COLUMN\', @level2name = N\'DataUpdateTime\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'创建时间\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\', @level2type = N\'COLUMN\', @level2name = N\'DataAddTime\'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC  sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'供货验收单表\' , @level0type = N\'SCHEMA\', @level0name = N\'dbo\', @level1type = N\'TABLE\', @level1name = N\'BmsCenSaleDocConfirm\'";
                    listSQL.Add(updateSql);
                }

                updateSql = " if not exists (select * from dbo.sysobjects where id = object_id(N\'[dbo].[BmsCenSaleDtlConfirm]\')" + "\r" +
                            " and OBJECTPROPERTY(id, N\'IsUserTable\') = 1) BEGIN " + "\r" +
                            " CREATE TABLE[dbo].[BmsCenSaleDtlConfirm]( " + "\r" +
                            "    [SaleDtlConfirmID][bigint] NOT NULL, " + "\r" +
                            "    [SaleDtlConfirmNo] [varchar](50) NULL, " + "\r" +
                            "    [SaleDocConfirmID] [bigint] NULL, " + "\r" +
                            "    [SaleDocConfirmNo] [varchar](50) NULL, " + "\r" +
                            "    [SaleDtlID] [bigint] NOT NULL, " + "\r" +
                            "    [GoodsID] [bigint]  NULL, " + "\r" +
                            "    [ProdGoodsNo] [varchar](50) NULL, " + "\r" +
                            "    [ProdID] [bigint] NULL, " + "\r" +
                            "    [ProdOrgName] [varchar](100) NULL, " + "\r" +
                            "    [GoodsName] [varchar](100) NULL, " + "\r" +
                            "    [GoodsUnit] [varchar](10) NULL, " + "\r" +
                            "    [UnitMemo] [varchar](100) NULL, " + "\r" +
                            "    [StorageType] [varchar](200) NULL, " + "\r" +
                            "    [TempRange] [varchar](100) NULL, " + "\r" +
                            "    [GoodsQty] [int] NOT NULL, " + "\r" +
                            "    [Price] [float] NOT NULL, " + "\r" +
                            "    [SumTotal] [float] NULL, " + "\r" +
                            "    [TaxRate] [float] NULL, " + "\r" +
                            "    [LotNo] [varchar](100) NULL, " + "\r" +
                            "    [ProdDate] [datetime] NULL, " + "\r" +
                            "    [InvalidDate] [datetime] NULL, " + "\r" +
                            "    [BiddingNo] [varchar](100) NULL, " + "\r" +
                            "    [ApproveDocNo] [varchar](200) NULL, " + "\r" +
                            "    [GoodsSerial] [varchar](100) NULL, " + "\r" +
                            "    [PackSerial] [varchar](100) NULL, " + "\r" +
                            "    [RegisterInvalidDate] [datetime] NULL, " + "\r" +
                            "    [LotSerial] [varchar](100) NULL, " + "\r" +
                            "    [MixSerial] [varchar](100) NULL, " + "\r" +
                            "    [RegisterNo] [varchar](200) NULL, " + "\r" +
                            "    [AcceptCount] [int] NULL, " + "\r" +
                            "    [RefuseCount] [int] NULL, " + "\r" +
                            "    [AcceptMemo] [varchar](1000) NULL, " + "\r" +
                            "    [DispOrder] [int] NULL, " + "\r" +
                            "    [DataUpdateTime] [datetime] NULL, " + "\r" +
                            "    [DataAddTime] [datetime]  NULL, " + "\r" +
                            " CONSTRAINT[PK_BMSCENSALEDTLCONFIRM] PRIMARY KEY CLUSTERED " + "\r" +
                            "( " + "\r" +
                            "   [SaleDtlConfirmID] ASC " + "\r" +
                            ")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] " + "\r" +
                            ") ON[PRIMARY] " + "\r" +
                            " end";
                listSQL.Add(updateSql);

                if (!CheckDataObjectIsExists("BmsCenSaleDtlConfirm", "U"))
                {
                    updateSql = " ALTER TABLE [dbo].[BmsCenSaleDtlConfirm] WITH CHECK ADD CONSTRAINT[FK_BMSCENSA_REFERENCE_BMSCENSA_SaleDocConfirmID] FOREIGN KEY([SaleDocConfirmID]) REFERENCES[dbo].[BmsCenSaleDocConfirm] ([SaleDocConfirmID])";
                    listSQL.Add(updateSql);

                    updateSql = " ALTER TABLE [dbo].[BmsCenSaleDtlConfirm]  CHECK CONSTRAINT[FK_BMSCENSA_REFERENCE_BMSCENSA_SaleDocConfirmID]";
                    listSQL.Add(updateSql);

                    updateSql = " ALTER TABLE [dbo].[BmsCenSaleDtlConfirm] WITH CHECK ADD CONSTRAINT[FK_BMSCENSA_REFERENCE_BMSCENSA_SaleDtlID] FOREIGN KEY([SaleDtlID]) REFERENCES[dbo].[BmsCenSaleDtl] ([SaleDtlID])";
                    listSQL.Add(updateSql);

                    updateSql = " ALTER TABLE [dbo].[BmsCenSaleDtlConfirm] CHECK CONSTRAINT[FK_BMSCENSA_REFERENCE_BMSCENSA_SaleDtlID]";
                    listSQL.Add(updateSql);

                    updateSql = " ALTER TABLE [dbo].[BmsCenSaleDtlConfirm] WITH CHECK ADD CONSTRAINT[FK_BMSCENSA_REFERENCE_CENORG_ProdID] FOREIGN KEY([ProdID]) REFERENCES[dbo].[CenOrg] ([OrgID])";
                    listSQL.Add(updateSql);

                    updateSql = " ALTER TABLE [dbo].[BmsCenSaleDtlConfirm]  CHECK CONSTRAINT[FK_BMSCENSA_REFERENCE_CENORG_ProdID]";
                    listSQL.Add(updateSql);

                    updateSql = " ALTER TABLE [dbo].[BmsCenSaleDtlConfirm] WITH CHECK ADD CONSTRAINT[FK_BMSCENSA_REFERENCE_GOODS_GoodsID] FOREIGN KEY([GoodsID]) REFERENCES[dbo].[Goods] ([GoodsID])";
                    listSQL.Add(updateSql);

                    updateSql = " ALTER TABLE [dbo].[BmsCenSaleDtlConfirm] CHECK CONSTRAINT[FK_BMSCENSA_REFERENCE_GOODS_GoodsID]";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'供货验收明细单ID' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'SaleDtlConfirmID'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'供货验收明细单号' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'SaleDtlConfirmNo'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'供货验收单ID' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'SaleDocConfirmID'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'供货验收单号' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'SaleDocConfirmNo'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'供货明细单ID' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'SaleDtlID'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'产品ID' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'GoodsID'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'厂商产品编号' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'ProdGoodsNo'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'厂商ID' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'ProdID'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'厂商名称' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'ProdOrgName'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'货品名称' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'GoodsName'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'包装单位' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'GoodsUnit'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'包装单位描述（规格）' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'UnitMemo'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'储藏条件' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'StorageType'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'温度范围' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'TempRange'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'购进数量' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'GoodsQty'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'单价' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'Price'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'总计金额' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'SumTotal'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'税率' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'TaxRate'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'货品批号' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'LotNo'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'生产日期' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'ProdDate'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'有效期' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'InvalidDate'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'招标号' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'BiddingNo'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'批准文号' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'ApproveDocNo'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'产品条码' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'GoodsSerial'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'包装单位条码' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'PackSerial'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'注册证有效期' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'RegisterInvalidDate'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'批号条码' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'LotSerial'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'混合条码' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'MixSerial'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'注册证编号' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'RegisterNo'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'验收数量' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'AcceptCount'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'拒收数量' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'RefuseCount'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'验收备注' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'AcceptMemo'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'显示次序' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'DispOrder'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'数据更新时间' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'DataUpdateTime'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'创建时间' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm', @level2type = N'COLUMN', @level2name = N'DataAddTime'";
                    listSQL.Add(updateSql);

                    updateSql = " EXEC sys.sp_addextendedproperty @name = N'MS_Description', @value = N'供货验收明细单表' , @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'TABLE', @level1name = N'BmsCenSaleDtlConfirm'";
                    listSQL.Add(updateSql);
                }

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
                updateSql = "IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Rea_GoodsLink]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) PRINT '存在' ELSE begin CREATE TABLE [dbo].[Rea_GoodsLink](	[LabID] [dbo].[D_实验室ID] NULL,	[GoodsLinkID] [dbo].[D_系统主键] NOT NULL,	[PGoodsID] [bigint] NOT NULL,	[GoodsID] [bigint] NOT NULL,	[ChangeQty] [float] NULL,	[Memo] [ntext] NULL,	[DispOrder] [int] NULL,	[Visible] [int] NULL,	[ZX1] [varchar](100) NULL,	[ZX2] [varchar](100) NULL,	[ZX3] [varchar](100) NULL,	[DataUpdateTime] [datetime] NOT NULL,	[DataAddTime] [datetime] NOT NULL,	[BeginTime] [datetime] NULL,	[EndTime] [datetime] NULL, CONSTRAINT [PK_REA_GOODSLINK] PRIMARY KEY CLUSTERED (	[GoodsLinkID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] end";
                listSQL.Add(updateSql);

                updateSql = "IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Rea_GoodsBarcode]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) PRINT '存在' ELSE begin CREATE TABLE [dbo].[Rea_GoodsBarcode](	[LabID] [dbo].[D_系统主键] NULL,	[GoodsBarcodeID] [dbo].[D_系统主键] NOT NULL,	[PGoodsBarcodeID] [dbo].[D_系统主键] NULL,	[SerialNo] [varchar](100) NULL,	[GoodsSerial] [varchar](100) NULL,	[PackSerial] [varchar](100) NULL,	[LotSerial] [varchar](100) NULL,	[MixSerial] [varchar](100) NULL,	[SaleDtlID] [dbo].[D_系统主键] NULL,	[ConfirmDtlID] [dbo].[D_系统主键] NULL,	[InDtlID] [dbo].[D_系统主键] NULL,	[OutDtlID] [dbo].[D_系统主键] NULL,	[QtyDtlID] [dbo].[D_系统主键] NULL,	[Memo] [varchar](max) NULL,	[DispOrder] [int] NULL,	[Visible] [bit] NULL,	[CreaterID] [bigint] NULL,	[CreaterName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NOT NULL,	[DataTimeStamp] [timestamp] NULL,	[GoodsID] [dbo].[D_系统主键] NULL,	[GoodsCName] [varchar](200) NOT NULL,	[GoodsUnitID] [dbo].[D_系统主键] NULL,	[GoodsUnit] [varchar](50) NULL, CONSTRAINT [PK_REA_GOODSBARCODE] PRIMARY KEY CLUSTERED (	[GoodsBarcodeID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] end";
                listSQL.Add(updateSql);

                updateSql = "IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Rea_BmsOutDtl]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) PRINT '存在' ELSE begin CREATE TABLE [dbo].[Rea_BmsOutDtl](	[LabID] [dbo].[D_系统主键] NULL,	[OutDtlID] [dbo].[D_系统主键] NOT NULL,	[OutDocID] [dbo].[D_系统主键] NULL,	[QtyDtlID] [dbo].[D_系统主键] NOT NULL,	[GoodsID] [dbo].[D_系统主键] NULL,	[GoodsCName] [varchar](200) NOT NULL,	[SerialNo] [varchar](50) NULL,	[GoodsUnitID] [dbo].[D_系统主键] NULL,	[GoodsUnit] [varchar](50) NULL,	[GoodsQty] [float] NULL,	[Price] [float] NULL,	[SumTotal] [float] NULL,	[TaxRate] [float] NULL,	[LotNo] [varchar](100) NULL,	[StorageID] [dbo].[D_系统主键] NULL,	[PlaceID] [dbo].[D_系统主键] NULL,	[GoodsSerial] [varchar](100) NULL,	[PackSerial] [varchar](100) NULL,	[LotSerial] [varchar](100) NULL,	[MixSerial] [varchar](100) NULL,	[ZX1] [varchar](50) NULL,	[ZX2] [varchar](50) NULL,	[ZX3] [varchar](50) NULL,	[DispOrder] [int] NULL,	[Memo] [varchar](max) NULL,	[Visible] [bit] NULL,	[CreaterID] [bigint] NULL,	[CreaterName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NOT NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_REA_BMSOUTDTL] PRIMARY KEY CLUSTERED (	[OutDtlID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] end";
                listSQL.Add(updateSql);

                updateSql = "IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Rea_BmsQtyDtl]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) PRINT '存在' ELSE begin CREATE TABLE [dbo].[Rea_BmsQtyDtl](	[LabID] [dbo].[D_系统主键] NULL,	[QtyDtlID] [dbo].[D_系统主键] NOT NULL,	[PQtyDtlID] [dbo].[D_系统主键] NOT NULL,	[SerialNo] [varchar](100) NULL,	[CompanyID] [dbo].[D_系统主键] NULL,	[CompanyName] [varchar](100) NULL,	[GoodsID] [dbo].[D_系统主键] NULL,	[GoodsName] [varchar](100) NULL,	[LotNo] [varchar](100) NULL,	[StorageID] [dbo].[D_系统主键] NULL,	[PlaceID] [dbo].[D_系统主键] NULL,	[GoodsUnitID] [dbo].[D_系统主键] NULL,	[GoodsUnit] [varchar](100) NULL,	[GoodsQty] [float] NULL,	[Price] [float] NULL,	[SumTotal] [float] NULL,	[TaxRate] [float] NULL,	[OutFlag] [int] NULL,	[SumFlag] [int] NULL,	[IOFlag] [int] NULL,	[ZX1] [varchar](50) NULL,	[ZX2] [varchar](50) NULL,	[ZX3] [varchar](50) NULL,	[Memo] [varchar](max) NULL,	[DispOrder] [int] NULL,	[Visible] [bit] NULL,	[CreaterID] [bigint] NULL,	[CreaterName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NOT NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_REA_BMSQTYDTL] PRIMARY KEY CLUSTERED (	[QtyDtlID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] end";
                listSQL.Add(updateSql);

                updateSql = "IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Rea_BmsOutDoc]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) PRINT '存在' ELSE begin CREATE TABLE [dbo].[Rea_BmsOutDoc](	[LabID] [dbo].[D_系统主键] NULL,	[OutDocID] [dbo].[D_系统主键] NOT NULL,	[DeptID] [bigint] NULL,	[OutType] [varchar](20) NULL,	[Status] [int] NULL,	[OperDate] [datetime] NULL,	[PrintTimes] [int] NULL,	[ZX1] [varchar](50) NULL,	[ZX2] [varchar](50) NULL,	[ZX3] [varchar](50) NULL,	[DispOrder] [int] NULL,	[Memo] [varchar](max) NULL,	[Visible] [bit] NULL,	[CreaterID] [bigint] NULL,	[CreaterName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NOT NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_REA_BMSOUTDOC] PRIMARY KEY CLUSTERED (	[OutDocID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY] end";
                listSQL.Add(updateSql);

                updateSql = "ALTER TABLE [dbo].[Rea_GoodsLink]  WITH CHECK ADD  CONSTRAINT [FK_REA_GOOD_REFERENCE_REA_GoodsID] FOREIGN KEY([GoodsID])REFERENCES [dbo].[Rea_Goods] ([GoodsID]) ALTER TABLE [dbo].[Rea_GoodsLink] CHECK CONSTRAINT [FK_REA_GOOD_REFERENCE_REA_GoodsID] ALTER TABLE [dbo].[Rea_GoodsLink]  WITH CHECK ADD  CONSTRAINT [FK_REA_GOOD_REFERENCE_REA_PGoodsID] FOREIGN KEY([PGoodsID])REFERENCES [dbo].[Rea_Goods] ([GoodsID]) ALTER TABLE [dbo].[Rea_GoodsLink] CHECK CONSTRAINT [FK_REA_GOOD_REFERENCE_REA_PGoodsID] ALTER TABLE [dbo].[Rea_GoodsBarcode]  WITH CHECK ADD  CONSTRAINT [FK_REA_GOOD_REFERENCE_BMSCENSA] FOREIGN KEY([ConfirmDtlID])REFERENCES [dbo].[BmsCenSaleDtlConfirm] ([SaleDtlConfirmID]) ALTER  TABLE [dbo].[Rea_GoodsBarcode] CHECK CONSTRAINT [FK_REA_GOOD_REFERENCE_BMSCENSA] ALTER TABLE [dbo].[Rea_GoodsBarcode]  WITH CHECK ADD  CONSTRAINT [FK_REA_GOOD_REFERENCE_BMSCENSA1] FOREIGN KEY([SaleDtlID])REFERENCES [dbo].[BmsCenSaleDtl] ([SaleDtlID]) ALTER  TABLE [dbo].[Rea_GoodsBarcode] CHECK CONSTRAINT [FK_REA_GOOD_REFERENCE_BMSCENSA1] ALTER  TABLE [dbo].[Rea_GoodsBarcode]  WITH CHECK ADD  CONSTRAINT [FK_REA_GOOD_REFERENCE_REA_BMSI] FOREIGN KEY([InDtlID])REFERENCES [dbo].[Rea_BmsInDtl] ([InDtlID]) ALTER  TABLE [dbo].[Rea_GoodsBarcode] CHECK CONSTRAINT [FK_REA_GOOD_REFERENCE_REA_BMSI] ALTER  TABLE [dbo].[Rea_GoodsBarcode]  WITH CHECK ADD  CONSTRAINT [FK_REA_GOOD_REFERENCE_REA_BMSO] FOREIGN KEY([OutDtlID])REFERENCES [dbo].[Rea_BmsOutDtl] ([OutDtlID]) ALTER  TABLE [dbo].[Rea_GoodsBarcode] CHECK CONSTRAINT [FK_REA_GOOD_REFERENCE_REA_BMSO] ALTER  TABLE [dbo].[Rea_GoodsBarcode]  WITH CHECK ADD  CONSTRAINT [FK_REA_GOOD_REFERENCE_REA_BMSQ] FOREIGN KEY([QtyDtlID])REFERENCES [dbo].[Rea_BmsQtyDtl] ([QtyDtlID]) ALTER  TABLE [dbo].[Rea_GoodsBarcode] CHECK CONSTRAINT [FK_REA_GOOD_REFERENCE_REA_BMSQ] ALTER  TABLE [dbo].[Rea_GoodsBarcode]  WITH CHECK ADD  CONSTRAINT [FK_REA_GOOD_REFERENCE_REA_GOOD11] FOREIGN KEY([GoodsID])REFERENCES [dbo].[Rea_Goods] ([GoodsID]) ALTER  TABLE [dbo].[Rea_GoodsBarcode] CHECK CONSTRAINT [FK_REA_GOOD_REFERENCE_REA_GOOD11] ALTER  TABLE [dbo].[Rea_GoodsBarcode]  WITH CHECK ADD  CONSTRAINT [FK_REA_GOOD_REFERENCE_REA_GOODaaa] FOREIGN KEY([GoodsUnitID])REFERENCES [dbo].[Rea_GoodsUnit] ([GoodsUnitID]) ALTER  TABLE [dbo].[Rea_GoodsBarcode] CHECK CONSTRAINT [FK_REA_GOOD_REFERENCE_REA_GOODaaa] ALTER  TABLE [dbo].[Rea_BmsOutDtl]  WITH CHECK ADD  CONSTRAINT [FK_REA_BMSO_REFERENCE_REA_BMSO] FOREIGN KEY([OutDocID])REFERENCES [dbo].[Rea_BmsOutDoc] ([OutDocID]) ALTER  TABLE [dbo].[Rea_BmsOutDtl] CHECK CONSTRAINT [FK_REA_BMSO_REFERENCE_REA_BMSO] ALTER  TABLE [dbo].[Rea_BmsOutDtl]  WITH CHECK ADD  CONSTRAINT [FK_REA_BMSO_REFERENCE_REA_BMSQ] FOREIGN KEY([QtyDtlID])REFERENCES [dbo].[Rea_BmsQtyDtl] ([QtyDtlID]) ALTER  TABLE [dbo].[Rea_BmsOutDtl] CHECK CONSTRAINT [FK_REA_BMSO_REFERENCE_REA_BMSQ] ALTER  TABLE [dbo].[Rea_BmsOutDtl]  WITH CHECK ADD  CONSTRAINT [FK_REA_BMSO_REFERENCE_REA_GOOD] FOREIGN KEY([GoodsID])REFERENCES [dbo].[Rea_Goods] ([GoodsID]) ALTER  TABLE [dbo].[Rea_BmsOutDtl] CHECK CONSTRAINT [FK_REA_BMSO_REFERENCE_REA_GOOD] ALTER  TABLE [dbo].[Rea_BmsOutDtl]  WITH CHECK ADD  CONSTRAINT [FK_REA_BMSO_REFERENCE_REA_GOOD1] FOREIGN KEY([GoodsUnitID])REFERENCES [dbo].[Rea_GoodsUnit] ([GoodsUnitID]) ALTER  TABLE [dbo].[Rea_BmsOutDtl] CHECK CONSTRAINT [FK_REA_BMSO_REFERENCE_REA_GOOD1] ALTER  TABLE [dbo].[Rea_BmsQtyDtl]  WITH CHECK ADD  CONSTRAINT [FK_REA_BMSQ_REFERENCE_REA_GOOD] FOREIGN KEY([GoodsID])REFERENCES [dbo].[Rea_Goods] ([GoodsID]) ALTER  TABLE [dbo].[Rea_BmsQtyDtl] CHECK CONSTRAINT [FK_REA_BMSQ_REFERENCE_REA_GOOD]";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_GoodsUnit', 'ChangeUnitID') IS NOT NULL ALTER TABLE Rea_GoodsUnit DROP constraint FK_REA_GOOD_REFERENCE_REA_GOOD ;";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_GoodsUnit', 'ChangeUnitID') IS NOT NULL ALTER TABLE Rea_GoodsUnit   DROP COLUMN ChangeUnitID ;";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_GoodsUnit', 'GoodsID') IS NOT NULL ALTER TABLE Rea_GoodsUnit DROP constraint FK_REA_GOOD_REFERENCE_REA_GOOD1 ;";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_GoodsUnit', 'GoodsID') IS NOT NULL ALTER TABLE Rea_GoodsUnit   DROP COLUMN GoodsID ;";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_GoodsUnit', 'ChangeUnit') IS NOT NULL ALTER TABLE Rea_GoodsUnit   DROP COLUMN ChangeUnit ;";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_GoodsUnit', 'ChangeQty') IS NOT NULL ALTER TABLE Rea_GoodsUnit   DROP COLUMN ChangeQty ;";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_GoodsUnit', 'GoodsCName') IS NOT NULL ALTER TABLE Rea_GoodsUnit   DROP COLUMN GoodsCName ;";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_BmsInDtl', 'GoodsSerial') IS NOT NULL ALTER TABLE Rea_BmsInDtl   DROP COLUMN GoodsSerial ;";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_BmsInDtl', 'PackSerial') IS NOT NULL ALTER TABLE Rea_BmsInDtl   DROP COLUMN PackSerial ;";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_BmsInDtl', 'LotSerial') IS NOT NULL ALTER TABLE Rea_BmsInDtl   DROP COLUMN LotSerial ;";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_BmsInDtl', 'MixSerial') IS NOT NULL ALTER TABLE Rea_BmsInDtl   DROP COLUMN MixSerial ;";
                listSQL.Add(updateSql);

                updateSql = "IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Rea_OrderGoods]') AND OBJECTPROPERTY(ID, 'IsTable') = 1) EXEC sp_rename 'Rea_OrderGoods', 'Rea_GoodsOrgLink' ELSE PRINT '不存在';";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_GoodsOrgLink', 'BeginTime') IS NULL ALTER TABLE Rea_GoodsOrgLink   ADD BeginTime datetime";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_GoodsOrgLink', 'EndTime') IS NULL ALTER TABLE Rea_GoodsOrgLink   ADD EndTime datetime";
                listSQL.Add(updateSql);



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

                updateSql = "IF COL_LENGTH('BmsCenOrderDoc', 'ReaCompID') IS NULL ALTER TABLE BmsCenOrderDoc   ADD ReaCompID bigint ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('BmsCenOrderDoc', 'ReaCompName') IS NULL ALTER TABLE BmsCenOrderDoc   ADD ReaCompName varchar(200) ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('BmsCenOrderDoc', 'ReaServerCompCode') IS NULL ALTER TABLE BmsCenOrderDoc   ADD ReaServerCompCode varchar(200) ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('BmsCenOrderDoc', 'CenOrderDocID') IS NULL ALTER TABLE BmsCenOrderDoc   ADD CenOrderDocID bigint ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('BmsCenOrderDtl', 'ReaGoodsID') IS NULL ALTER TABLE BmsCenOrderDtl   ADD ReaGoodsID bigint";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('BmsCenOrderDtl', 'ReaGoodsName') IS NULL ALTER TABLE BmsCenOrderDtl   ADD ReaGoodsName varchar(100)";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('BmsCenOrderDtl', 'GoodsNo') IS NULL ALTER TABLE BmsCenOrderDtl   ADD GoodsNo varchar(100)";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('BmsCenOrderDtl', 'OrderGoodsID') IS NULL ALTER TABLE BmsCenOrderDtl   ADD OrderGoodsID bigint";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_BmsReqDtl', 'OrderDtlID') IS NULL ALTER TABLE Rea_BmsReqDtl   ADD OrderDtlID bigint";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_BmsReqDtl', 'OrderGoodsID') IS NULL ALTER TABLE Rea_BmsReqDtl   ADD OrderGoodsID bigint";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_BmsReqDoc', 'DeptName') IS NULL ALTER TABLE Rea_BmsReqDoc   ADD DeptName varchar(20) ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_BmsOutDtl', 'StorageName') IS NULL ALTER TABLE Rea_BmsOutDtl   ADD StorageName varchar(100) ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_BmsOutDtl', 'PlaceName') IS NULL ALTER TABLE Rea_BmsOutDtl   ADD PlaceName varchar(100) ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_BmsInDtl', 'StorageName') IS NULL ALTER TABLE Rea_BmsInDtl   ADD StorageName varchar(100) ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_BmsInDtl', 'PlaceName') IS NULL ALTER TABLE Rea_BmsInDtl   ADD PlaceName varchar(100) ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_BmsQtyDtl', 'StorageName') IS NULL ALTER TABLE Rea_BmsQtyDtl   ADD StorageName varchar(100) ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_BmsQtyDtl', 'PlaceName') IS NULL ALTER TABLE Rea_BmsQtyDtl   ADD PlaceName varchar(100) ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_BmsOutDoc', 'DeptName') IS NULL ALTER TABLE Rea_BmsOutDoc   ADD DeptName varchar(100) ";
                listSQL.Add(updateSql);











                updateSql = " ALTER TABLE [dbo].[Rea_BmsReqDtl]  WITH CHECK ADD  CONSTRAINT [FK_Rea_BmsReqDtl_REFERENCE_OrderDtlID] FOREIGN KEY([OrderDtlID])REFERENCES [dbo].[BmsCenOrderDtl] ([OrderDtlID]) ";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE [dbo].[Rea_BmsReqDtl]  WITH CHECK ADD  CONSTRAINT [FK_Rea_BmsReqDtl_REFERENCE_OrderGoodsID] FOREIGN KEY([OrderGoodsID])REFERENCES [dbo].[Rea_GoodsOrgLink] ([OrderGoodsID]) ";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE [dbo].[BmsCenOrderDoc]  WITH CHECK ADD  CONSTRAINT [FK_BmsCenOrderDoc_REFERENCE_ReaCompID] FOREIGN KEY([ReaCompID])REFERENCES [dbo].[Rea_CenOrg] ([OrgID]) ";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE [dbo].[BmsCenOrderDtl]  WITH CHECK ADD  CONSTRAINT  [FK_BmsCenOrderDtl_REFERENCE_ReaGoodsID] FOREIGN KEY([ReaGoodsID])REFERENCES [dbo].[Rea_Goods] ([GoodsID]) ";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE [dbo].[BmsCenOrderDtl]  WITH CHECK ADD  CONSTRAINT [FK_BmsCenOrderDtl_REFERENCE_OrderGoodsID] FOREIGN KEY([OrderGoodsID])REFERENCES [dbo].[Rea_GoodsOrgLink] ([OrderGoodsID]) ";
                listSQL.Add(updateSql);

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
                #region BmsCenOrderDoc
                updateSql = "IF COL_LENGTH('BmsCenOrderDoc', 'SSCID') IS NULL ALTER TABLE BmsCenOrderDoc   ADD SSCID bigint ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('BmsCenOrderDtl', 'SSCID') IS NULL ALTER TABLE BmsCenOrderDtl   ADD SSCID bigint ";
                listSQL.Add(updateSql);
                #endregion

                #region BmsCenSaleDoc
                updateSql = "IF COL_LENGTH('BmsCenSaleDoc', 'ReaCompID') IS NULL ALTER TABLE BmsCenSaleDoc   ADD ReaCompID bigint ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('BmsCenSaleDoc', 'ReaCompName') IS NULL ALTER TABLE BmsCenSaleDoc   ADD ReaCompName varchar(200) ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('BmsCenSaleDoc', 'ReaServerCompCode') IS NULL ALTER TABLE BmsCenSaleDoc   ADD ReaServerCompCode varchar(200) ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('BmsCenSaleDoc', 'CenSaleDocID') IS NULL ALTER TABLE BmsCenSaleDoc   ADD CenSaleDocID bigint ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('BmsCenSaleDoc', 'SSCID') IS NULL ALTER TABLE BmsCenSaleDoc   ADD SSCID bigint ";
                listSQL.Add(updateSql);
                #endregion

                #region BmsCenSaleDtl
                updateSql = "IF COL_LENGTH('BmsCenSaleDtl', 'SSCID') IS NULL ALTER TABLE BmsCenSaleDtl   ADD SSCID bigint ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('BmsCenSaleDtl', 'ReaGoodsID') IS NULL ALTER TABLE BmsCenSaleDtl   ADD ReaGoodsID bigint";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('BmsCenSaleDtl', 'ReaGoodsName') IS NULL ALTER TABLE BmsCenSaleDtl   ADD ReaGoodsName varchar(100)";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('BmsCenSaleDtl', 'GoodsNo') IS NULL ALTER TABLE BmsCenSaleDtl   ADD GoodsNo varchar(100)";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('BmsCenSaleDtl', 'OrderGoodsID') IS NULL ALTER TABLE BmsCenSaleDtl   ADD OrderGoodsID bigint";
                listSQL.Add(updateSql);
                #endregion

                #region BmsCenSaleDocConfirm
                updateSql = "IF COL_LENGTH('BmsCenSaleDocConfirm', 'SSCID') IS NULL ALTER TABLE BmsCenSaleDocConfirm   ADD SSCID bigint ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('BmsCenSaleDocConfirm', 'ReaCompID') IS NULL ALTER TABLE BmsCenSaleDocConfirm   ADD ReaCompID bigint ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('BmsCenSaleDocConfirm', 'ReaCompName') IS NULL ALTER TABLE BmsCenSaleDocConfirm   ADD ReaCompName varchar(200) ";
                listSQL.Add(updateSql);
                #endregion

                #region BmsCenSaleDtlConfirm
                updateSql = "IF COL_LENGTH('BmsCenSaleDtlConfirm', 'SSCID') IS NULL ALTER TABLE BmsCenSaleDtlConfirm   ADD SSCID bigint ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('BmsCenSaleDtlConfirm', 'ReaGoodsID') IS NULL ALTER TABLE BmsCenSaleDtlConfirm   ADD ReaGoodsID bigint";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('BmsCenSaleDtlConfirm', 'ReaGoodsName') IS NULL ALTER TABLE BmsCenSaleDtlConfirm   ADD ReaGoodsName varchar(100)";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('BmsCenSaleDtlConfirm', 'OrderGoodsID') IS NULL ALTER TABLE BmsCenSaleDtlConfirm   ADD OrderGoodsID bigint";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('BmsCenSaleDtlConfirm', 'GoodsNo') IS NULL ALTER TABLE BmsCenSaleDtlConfirm   ADD GoodsNo varchar(100)";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsInDtl
                updateSql = "IF COL_LENGTH('Rea_BmsInDtl', 'ReaCompanyID') IS NULL ALTER TABLE Rea_BmsInDtl   ADD ReaCompanyID bigint";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_BmsInDtl', 'CompanyName') IS NULL ALTER TABLE Rea_BmsInDtl   ADD CompanyName varchar(100)";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtl
                updateSql = "IF COL_LENGTH('Rea_BmsQtyDtl', 'CompanyID') IS not NULL exec sp_rename 'Rea_BmsQtyDtl.CompanyID','ReaCompanyID','column';";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsOutDtl
                updateSql = "IF COL_LENGTH('Rea_BmsOutDtl', 'ReaCompanyID') IS  NULL ALTER TABLE Rea_BmsOutDtl   ADD ReaCompanyID bigint";
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

                #region BmsCenSaleDoc
                updateSql = " ALTER TABLE [dbo].[BmsCenSaleDoc]  WITH CHECK ADD  CONSTRAINT [FK_BmsCenSaleDoc_REFERENCE_ReaCompID] FOREIGN KEY([ReaCompID])REFERENCES [dbo].[Rea_CenOrg] ([OrgID]) ";
                listSQL.Add(updateSql);

                #endregion

                #region BmsCenSaleDtl
                updateSql = " ALTER TABLE [dbo].[BmsCenSaleDtl]  WITH CHECK ADD  CONSTRAINT [FK_BmsCenSaleDoc_REFERENCE_ReaGoodsID] FOREIGN KEY([ReaGoodsID])REFERENCES [dbo].[Rea_Goods] ([GoodsID]) ";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE [dbo].[BmsCenSaleDtl]  WITH CHECK ADD  CONSTRAINT [FK_BmsCenSaleDoc_REFERENCE_OrderGoodsID] FOREIGN KEY([OrderGoodsID])REFERENCES [dbo].[Rea_GoodsOrgLink] ([OrderGoodsID])  ";
                listSQL.Add(updateSql);
                #endregion

                #region BmsCenSaleDocConfirm   
                updateSql = " ALTER TABLE [dbo].[BmsCenSaleDocConfirm]  WITH CHECK ADD  CONSTRAINT [FK_BmsCenSaleDocConfirm_REFERENCE_ReaCompID] FOREIGN KEY([ReaCompID])REFERENCES [dbo].[Rea_CenOrg] ([OrgID]) ";
                listSQL.Add(updateSql);
                #endregion

                #region BmsCenSaleDtlConfirm    
                updateSql = " ALTER TABLE [dbo].[BmsCenSaleDtlConfirm]  WITH CHECK ADD  CONSTRAINT [FK_BmsCenSaleDtlConfirm_REFERENCE_ReaGoodsID] FOREIGN KEY([ReaGoodsID])REFERENCES [dbo].[Rea_Goods] ([GoodsID]) ";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE [dbo].[BmsCenSaleDtlConfirm]  WITH CHECK ADD  CONSTRAINT [FK_BmsCenSaleDtlConfirm_REFERENCE_OrderGoodsID] FOREIGN KEY([OrderGoodsID])REFERENCES [dbo].[Rea_GoodsOrgLink] ([OrderGoodsID])  ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsInDtl
                updateSql = " ALTER TABLE [dbo].[Rea_BmsInDtl]  WITH CHECK ADD  CONSTRAINT [FK_Rea_BmsInDtl_REFERENCE_ReaCompanyID] FOREIGN KEY([ReaCompanyID])REFERENCES [dbo].[Rea_CenOrg] ([OrgID]) ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsQtyDtl
                updateSql = " ALTER TABLE [dbo].[Rea_BmsQtyDtl]  WITH CHECK ADD  CONSTRAINT [FK_Rea_BmsQtyDtl_REFERENCE_ReaCompanyID] FOREIGN KEY([ReaCompanyID])REFERENCES [dbo].[Rea_CenOrg] ([OrgID]) ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsOutDtl
                updateSql = " ALTER TABLE [dbo].[Rea_BmsOutDtl]  WITH CHECK ADD  CONSTRAINT [FK_Rea_BmsOutDtl_REFERENCE_ReaCompanyID] FOREIGN KEY([ReaCompanyID])REFERENCES [dbo].[Rea_CenOrg] ([OrgID]) ";
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

                #region Rea_GoodsBarcode
                updateSql = "IF COL_LENGTH('Rea_GoodsBarcode', 'IsOutFlag') IS NULL ALTER TABLE Rea_GoodsBarcode   ADD IsOutFlag bit ";
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

                #region BmsCenOrderDoc
                updateSql = "IF COL_LENGTH('BmsCenOrderDoc', 'CheckMemo') IS NULL ALTER TABLE BmsCenOrderDoc   ADD CheckMemo varchar(50) ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsReqDtl
                updateSql = "IF COL_LENGTH('Rea_BmsReqDtl', 'OrderStatus') IS NULL ALTER TABLE Rea_BmsReqDtl   ADD OrderStatus bigint ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_BmsReqDtl', 'OrderCheckMemo') IS NULL ALTER TABLE Rea_BmsReqDtl   ADD OrderCheckMemo varchar(500) ";
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
                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'PinYinZiTou\' and ID = (Select[ID] from SysObjects where Name = \'Goods\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table Goods Add PinYinZiTou varchar(100) " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'拼音字头\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'Goods\', @level2type = N\'COLUMN\',@level2name = N\'PinYinZiTou\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);
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
                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'DeptName\' and ID = (Select[ID] from SysObjects where Name = \'BmsCenOrderDoc\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table BmsCenOrderDoc Add DeptName varchar(200) " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'科室名称\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'BmsCenOrderDoc\', @level2type = N\'COLUMN\',@level2name = N\'DeptName\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'DeptName\' and ID = (Select[ID] from SysObjects where Name = \'BmsCenSaleDoc\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table BmsCenSaleDoc Add DeptName varchar(200) " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'科室名称\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'BmsCenSaleDoc\', @level2type = N\'COLUMN\',@level2name = N\'DeptName\'" + "\r" +
                            "end";
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
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region 库房、货架 ShortCode改varchar(20)
                updateSql = "IF COL_LENGTH('Rea_Storage', 'ShortCode') IS not NULL alter table Rea_Storage alter column ShortCode varchar(20) not null  ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_Place', 'ShortCode') IS not NULL alter table Rea_Place alter column ShortCode varchar(20) not null  ";
                listSQL.Add(updateSql);
                #endregion

                #region 条码表调整
                updateSql = "alter table[Rea_GoodsBarcode] drop constraint[FK_REA_GOOD_REFERENCE_BMSCENSA] ";
                listSQL.Add(updateSql);

                updateSql = "alter table[Rea_GoodsBarcode] drop constraint FK_REA_GOOD_REFERENCE_BMSCENSA1 ";
                listSQL.Add(updateSql);

                updateSql = "alter table[Rea_GoodsBarcode] drop constraint FK_REA_GOOD_REFERENCE_REA_BMSI ";
                listSQL.Add(updateSql);

                updateSql = "alter table[Rea_GoodsBarcode] drop constraint FK_REA_GOOD_REFERENCE_REA_BMSO ";
                listSQL.Add(updateSql);

                updateSql = "alter table[Rea_GoodsBarcode] drop constraint FK_REA_GOOD_REFERENCE_REA_BMSQ ";
                listSQL.Add(updateSql);


                updateSql = "ALTER TABLE[Rea_GoodsBarcode] DROP COLUMN SaleDtlID ";
                listSQL.Add(updateSql);

                updateSql = "ALTER TABLE[Rea_GoodsBarcode] DROP COLUMN ConfirmDtlID ";
                listSQL.Add(updateSql);

                updateSql = "ALTER TABLE[Rea_GoodsBarcode] DROP COLUMN InDtlID ";
                listSQL.Add(updateSql);

                updateSql = "ALTER TABLE[Rea_GoodsBarcode] DROP COLUMN OutDtlID ";
                listSQL.Add(updateSql);

                updateSql = "ALTER TABLE[Rea_GoodsBarcode] DROP COLUMN QtyDtlID ";
                listSQL.Add(updateSql);

                #endregion

                #region 明细条码关系表
                updateSql = "create table Rea_BmsOutDtlLink (   LabID D_系统主键               null,   ReaBODLID D_系统主键               not null,   OutDtlID D_系统主键               null,   GoodsBarcodeID D_系统主键               null,   SerialNo varchar(100)         null,   Memo                 varchar(Max)         null,   DispOrder            int                  null,   Visible              bit                  null,   CreaterID            bigint               null,   CreaterName          varchar(50)          null,   DataAddTime          datetime             null,   DataUpdateTime       datetime             not null,   DataTimeStamp        timestamp            null,   constraint PK_REA_BMSOUTDTLLINK primary key(ReaBODLID),   constraint FK_REA_BMSODL_REFERENCE_REA_BMSO foreign key(OutDtlID)      references Rea_BmsOutDtl(OutDtlID),   constraint FK_REA_BMSODL_REFERENCE_REA_GOOD foreign key(GoodsBarcodeID)      references Rea_GoodsBarcode(GoodsBarcodeID))   ";
                listSQL.Add(updateSql);

                updateSql = "create table Rea_BmsQtyDtlLink (   LabID D_系统主键               null,   ReaBQDLID D_系统主键               not null,   QtyDtlID D_系统主键               null,   GoodsBarcodeID D_系统主键               null,   SerialNo varchar(100)         null,   Memo                 varchar(Max)         null,   DispOrder            int                  null,   Visible              bit                  null,   CreaterID            bigint               null,   CreaterName          varchar(50)          null,   DataAddTime          datetime             null,   DataUpdateTime       datetime             not null,   DataTimeStamp        timestamp            null,   constraint PK_REA_BMSQTYDTLLINK primary key(ReaBQDLID),   constraint FK_REA_BMSQDL_REFERENCE_REA_BMSQ foreign key(QtyDtlID)      references Rea_BmsQtyDtl(QtyDtlID),   constraint FK_REA_BMSQDL_REFERENCE_REA_GOOD foreign key(GoodsBarcodeID)      references Rea_GoodsBarcode(GoodsBarcodeID))   ";
                listSQL.Add(updateSql);

                updateSql = "create table Rea_BmsInDtlLink (   LabID D_系统主键               null,   ReaBIDLID D_系统主键               not null,   InDtlID D_系统主键               null,   GoodsBarcodeID D_系统主键               null,   SerialNo varchar(100)         null,   Memo                 varchar(Max)         null,   DispOrder            int                  null,   Visible              bit                  null,   CreaterID            bigint               null,   CreaterName          varchar(50)          null,   DataAddTime          datetime             null,   DataUpdateTime       datetime             not null,   DataTimeStamp        timestamp            null,   constraint PK_REA_BMSINDTLLINK primary key(ReaBIDLID),   constraint FK_REA_BMSIDL_REFERENCE_REA_BMSI foreign key(InDtlID)      references Rea_BmsInDtl(InDtlID),   constraint FK_REA_BMSIDL_REFERENCE_REA_GOOD foreign key(GoodsBarcodeID)      references Rea_GoodsBarcode(GoodsBarcodeID))   ";
                listSQL.Add(updateSql);

                updateSql = "create table Rea_BmsCenSaleDtlConfirmLink (   LabID D_系统主键               null,   ReaBCSDLCID D_系统主键               not null,   SaleDtlConfirmID bigint               null,   GoodsBarcodeID D_系统主键               null,   SerialNo varchar(100)         null,   Memo                 varchar(Max)         null,   DispOrder            int                  null,   Visible              bit                  null,   CreaterID            bigint               null,   CreaterName          varchar(50)          null,   DataAddTime          datetime             null,   DataUpdateTime       datetime             not null,   DataTimeStamp        timestamp            null,   constraint PK_REA_BMSCENSALEDTLCONFIRMLIN primary key(ReaBCSDLCID),   constraint FK_REA_BMSCDCL_REFERENCE_BMSCENSA foreign key(SaleDtlConfirmID)      references BmsCenSaleDtlConfirm(SaleDtlConfirmID),   constraint FK_REA_BMSCDCL_REFERENCE_REA_GOOD foreign key(GoodsBarcodeID)      references Rea_GoodsBarcode(GoodsBarcodeID))   ";
                listSQL.Add(updateSql);

                updateSql = "create table Rea_BmsCenSaleDtlLink (   LabID D_系统主键               null,   ReaBCSDLID D_系统主键               not null,   SaleDtlID bigint               null,   GoodsBarcodeID D_系统主键               null,   SerialNo varchar(100)         null,   Memo                 varchar(Max)         null,   DispOrder            int                  null,   Visible              bit                  null,   CreaterID            bigint               null,   CreaterName          varchar(50)          null,   DataAddTime          datetime             null,   DataUpdateTime       datetime             not null,   DataTimeStamp        timestamp            null,   constraint PK_REA_BMSCENSALEDTLLINK primary key(ReaBCSDLID),   constraint FK_REA_BMSCDL_REFERENCE_BMSCENSA foreign key(SaleDtlID)      references dbo.BmsCenSaleDtl(SaleDtlID),   constraint FK_REA_BMSCDL_REFERENCE_REA_GOOD foreign key(GoodsBarcodeID)      references Rea_GoodsBarcode(GoodsBarcodeID))";
                listSQL.Add(updateSql);

                #endregion

                #region 货品表

                //删除货品关系表
                updateSql = " IF EXISTS  (SELECT  * FROM dbo.SysObjects WHERE ID = object_id(N'[Rea_GoodsLink]') AND OBJECTPROPERTY(ID, 'IsTable') = 1)  DROP TABLE Rea_GoodsLink ";
                listSQL.Add(updateSql);

                //货品表添加换算组代码-GonvertGroupCode
                updateSql = " IF COL_LENGTH('Rea_Goods', 'GonvertGroupCode') IS NULL ALTER TABLE Rea_Goods   ADD GonvertGroupCode varchar(50)  ";//
                listSQL.Add(updateSql);

                //货品表添加货品相似码-LinkGroupCode
                updateSql = " IF COL_LENGTH('Rea_Goods', 'LinkGroupCode') IS NULL ALTER TABLE Rea_Goods   ADD LinkGroupCode varchar(50)    ";
                listSQL.Add(updateSql);

                //货品表添加换算比率-GonvertQty
                updateSql = " IF COL_LENGTH('Rea_Goods', 'GonvertQty') IS NULL ALTER TABLE Rea_Goods   ADD GonvertQty float  ";
                listSQL.Add(updateSql);

                //货品表添加换算权重-GonvertSort
                updateSql = " IF COL_LENGTH('Rea_Goods', 'GonvertSort') IS NULL ALTER TABLE Rea_Goods   ADD GonvertSort int  ";
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

                #region 入库单加入库类型

                updateSql = " IF COL_LENGTH('Rea_BmsInDoc', 'InType') IS NULL ALTER TABLE Rea_BmsInDoc   ADD InType bigint  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDoc', 'InTypeName') IS NULL ALTER TABLE Rea_BmsInDoc   ADD InTypeName varchar(50)  ";
                listSQL.Add(updateSql);

                #endregion

                #region 出库单加出库类型

                updateSql = "IF COL_LENGTH('Rea_BmsOutDoc', 'OutType') IS not NULL alter table Rea_BmsOutDoc alter column OutType bigint not null  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'OutTypeName') IS NULL ALTER TABLE Rea_BmsOutDoc   ADD OutTypeName varchar(50)  ";//
                listSQL.Add(updateSql);

                #endregion

                #region 验货单明细加状态ID和状态名称

                updateSql = " IF COL_LENGTH('BmsCenSaleDtlConfirm', 'Status') IS NULL ALTER TABLE BmsCenSaleDtlConfirm   ADD Status bigint  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('BmsCenSaleDtlConfirm', 'StatusName') IS NULL ALTER TABLE BmsCenSaleDtlConfirm   ADD StatusName  varchar(50)";
                listSQL.Add(updateSql);

                #endregion

                #region 供货单明细加状态ID和状态名称

                updateSql = " IF COL_LENGTH('BmsCenSaleDtl', 'GonvertGroupCode') IS NULL ALTER TABLE BmsCenSaleDtl ADD Status bigint ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('BmsCenSaleDtl', 'GonvertGroupCode') IS NULL ALTER TABLE BmsCenSaleDtl ADD StatusName varchar(50) ";
                listSQL.Add(updateSql);

                #endregion

                #region 货品批号表加厂商ID、厂商名称

                updateSql = " IF COL_LENGTH('Rea_GoodsLot', 'FactoryID') IS NULL ALTER TABLE Rea_GoodsLot ADD FactoryID bigint ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsLot', 'FactoryName') IS NULL ALTER TABLE Rea_GoodsLot ADD FactoryName varchar(50) ";
                listSQL.Add(updateSql);

                updateSql = "  ALTER TABLE [dbo].[Rea_GoodsLot]  WITH CHECK ADD  CONSTRAINT [FK_Rea_GoodsLot_REFERENCE_Rea_CenOrg] FOREIGN KEY([FactoryID])REFERENCES [dbo].[Rea_CenOrg] ([OrgID])  ";
                listSQL.Add(updateSql);

                #endregion

                #region 货品注册证表加厂商ID、供应商ID、供应商名称、供应商编号

                updateSql = " IF COL_LENGTH('Rea_GoodsRegister', 'FactoryID') IS NULL ALTER TABLE Rea_GoodsRegister ADD FactoryID bigint ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsRegister', 'CompID') IS NULL ALTER TABLE Rea_GoodsRegister ADD CompID bigint ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsRegister', 'CompanyName') IS NULL ALTER TABLE Rea_GoodsRegister ADD CompanyName varchar(50) ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsRegister', 'CompNo') IS NULL ALTER TABLE Rea_GoodsRegister ADD CompNo varchar(50) ";
                listSQL.Add(updateSql);

                updateSql = "  ALTER TABLE [dbo].[Rea_GoodsRegister]  WITH CHECK ADD  CONSTRAINT [FK_Rea_GoodsRegister_REFERENCE_FactoryID] FOREIGN KEY([FactoryID])REFERENCES [dbo].[Rea_CenOrg] ([OrgID])  ";
                listSQL.Add(updateSql);

                updateSql = "  ALTER TABLE [dbo].[Rea_GoodsRegister]  WITH CHECK ADD  CONSTRAINT [FK_Rea_GoodsRegister_REFERENCE_CompID] FOREIGN KEY([CompID])REFERENCES [dbo].[Rea_CenOrg] ([OrgID])  ";
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

                #region 入库单加操作对象引用（撤销入库、更正入库）、单据状态名称

                updateSql = " IF COL_LENGTH('Rea_BmsInDoc', 'StatusName') IS NULL ALTER TABLE Rea_BmsInDoc   ADD StatusName  varchar(50)";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDoc', 'OperateInDocID') IS NULL ALTER TABLE Rea_BmsInDoc ADD OperateInDocID  bigint";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDoc', 'OperateInDocNo') IS NULL ALTER TABLE Rea_BmsInDoc ADD OperateInDocNo  varchar(50)";
                listSQL.Add(updateSql);

                #endregion

                #region 验货单加订单ID、订单编号

                updateSql = " IF COL_LENGTH('BmsCenSaleDocConfirm', 'OrderDocID') IS NULL ALTER TABLE BmsCenSaleDocConfirm   ADD OrderDocID  bigint";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('BmsCenSaleDocConfirm', 'OrderDocNo') IS NULL ALTER TABLE BmsCenSaleDocConfirm   ADD OrderDocNo  varchar(50)";
                listSQL.Add(updateSql);

                updateSql = "  ALTER TABLE [dbo].[BmsCenSaleDocConfirm]  WITH CHECK ADD  CONSTRAINT [FK_BmsCenSaleDocConfirm_REFERENCE_OrderDocID] FOREIGN KEY([OrderDocID])REFERENCES [dbo].[BmsCenOrderDoc] ([OrderDocID])  ";
                listSQL.Add(updateSql);

                #endregion

                #region 验收单明细加已入库数量

                updateSql = " IF COL_LENGTH('BmsCenSaleDtlConfirm', 'InCount') IS NULL ALTER TABLE BmsCenSaleDtlConfirm   ADD InCount  int";
                listSQL.Add(updateSql);

                #endregion

                #region 入库单明细加批条码信息
                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'SerialNo') IS not NULL ALTER TABLE[Rea_BmsInDtl] DROP COLUMN SerialNo  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'ProdDate') IS NULL ALTER TABLE Rea_BmsInDtl   ADD ProdDate  datetime";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'InvalidDate') IS NULL ALTER TABLE Rea_BmsInDtl   ADD InvalidDate  datetime";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'RegisterInvalidDate') IS NULL ALTER TABLE Rea_BmsInDtl   ADD RegisterInvalidDate  datetime";
                listSQL.Add(updateSql);


                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'BiddingNo') IS NULL ALTER TABLE Rea_BmsInDtl ADD BiddingNo varchar(100) ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'ApproveDocNo') IS NULL ALTER TABLE Rea_BmsInDtl   ADD ApproveDocNo varchar(200) ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'GoodsSerial') IS NULL ALTER TABLE Rea_BmsInDtl   ADD GoodsSerial varchar(100) ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'PackSerial') IS NULL ALTER TABLE Rea_BmsInDtl   ADD PackSerial varchar(100) ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'LotSerial') IS NULL ALTER TABLE Rea_BmsInDtl   ADD LotSerial varchar(100) ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'MixSerial') IS NULL ALTER TABLE Rea_BmsInDtl   ADD MixSerial varchar(100) ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'RegisterNo') IS NULL ALTER TABLE Rea_BmsInDtl   ADD RegisterNo varchar(200) ";
                listSQL.Add(updateSql);

                #endregion

                #region 出库单加操作对象引用（撤销出库、更正出库）、单据状态名称

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'StatusName') IS NULL ALTER TABLE Rea_BmsOutDoc   ADD StatusName  varchar(50)";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'OperateOutDocID') IS NULL ALTER TABLE Rea_BmsOutDoc ADD OperateOutDocID  bigint";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'OperateOutDocNo') IS NULL ALTER TABLE Rea_BmsOutDoc ADD OperateOutDocNo  varchar(50)";
                listSQL.Add(updateSql);

                #endregion

                #region 移库单

                updateSql = " create table Rea_BmsTransferDoc (   LabID D_系统主键               null,   TransferDocID D_系统主键               not null,   DeptID bigint               null,   DeptName varchar(20)          null,   TransferType         bigint               null,   TransferTypeName     varchar(50)          null,   Status               int                  null,   StatusName           varchar(50)          null,   SStorageID           D_系统主键               null,   SStorageName         varchar(50)          null,   DStorageID           D_系统主键               null,   DStorageName         varchar(50)          null,   操作者ID                D_系统主键               null,   操作者名称                varchar(50)          null,   OperDate             datetime             null,   PrintTimes           int                  null,   ZX1                  varchar(50)          null,   ZX2                  varchar(50)          null,   ZX3                  varchar(50)          null,   DispOrder            int                  null,   Memo                 varchar(Max)         null,   Visible              bit                  null,   CreaterID            bigint               null,   CreaterName          varchar(50)          null,   DataAddTime          datetime             null,   DataUpdateTime       datetime             not null,   DataTimeStamp        timestamp            null,   constraint PK_REA_BMSTRANSFERDOC primary key(TransferDocID),   constraint FK_Rea_BmsTransferDoc_REFERENCE_REA_SStorageID foreign key(SStorageID)      references Rea_Storage(StorageID),   constraint FK_Rea_BmsTransferDoc_REFERENCE_REA_DStorageID foreign key(DStorageID)      references Rea_Storage(StorageID))";

                listSQL.Add(updateSql);
                updateSql = " create table Rea_BmsTransferDtl (   LabID D_系统主键               null,   TransferDtlID D_系统主键               not null,   TransferDocID D_系统主键               null,   QtyDtlID D_系统主键               not null,   GoodsID D_系统主键               null,   GoodsCName varchar(200)         collate Chinese_PRC_CI_AS not null,   SerialNo             varchar(50)          null,   GoodsUnitID          D_系统主键               null,   GoodsUnit            varchar(50)          null,   ReaCompanyID         bigint               null,   ReaCompanyName       varchar(50)          null,   GoodsQty             float                null,   Price                float                null,   SumTotal             float                null,   TaxRate              float                null,   LotNo                varchar(100)         null,   SStorageID           D_系统主键               null,   SPlaceID             D_系统主键               null,   SStorageName         varchar(100)         null,   SPlaceName           varchar(100)         null,   DStorageID           D_系统主键               null,   DPlaceID             D_系统主键               null,   DStorageName         varchar(100)         null,   DPlaceName           varchar(100)         null,   GoodsSerial          varchar(100)         null,   PackSerial           varchar(100)         null,   LotSerial            varchar(100)         null,   MixSerial            varchar(100)         null,   ZX1                  varchar(50)          null,   ZX2                  varchar(50)          null,   ZX3                  varchar(50)          null,   DispOrder            int                  null,   Memo                 varchar(Max)         null,   Visible              bit                  null,   CreaterID            bigint               null,   CreaterName          varchar(50)          null,   DataAddTime          datetime             null,   DataUpdateTime       datetime             not null,   DataTimeStamp        timestamp            null,   constraint PK_REA_BMSTRANSFERDTL primary key(TransferDtlID),   constraint FK_Rea_BmsTransferDtl_REFERENCE_REA_TransferDocID foreign key(TransferDocID)      references Rea_BmsTransferDoc(TransferDocID),   constraint FK_Rea_BmsTransferDtl_REFERENCE_REA_DStorageID foreign key(DStorageID)      references Rea_Storage(StorageID),   constraint FK_Rea_BmsTransferDtl_REFERENCE_REA_SPlaceID foreign key(SPlaceID)      references Rea_Place(PlaceID),   constraint FK_Rea_BmsTransferDtl_REFERENCE_REA_SStorageID foreign key(SStorageID)      references Rea_Storage(StorageID),   constraint FK_Rea_BmsTransferDtl_REFERENCE_REA_DPlaceID foreign key(DPlaceID)      references Rea_Place(PlaceID),   constraint FK_Rea_BmsTransferDtl_REFERENCE_REA_GoodsUnitID foreign key(GoodsUnitID)      references Rea_GoodsUnit(GoodsUnitID),   constraint FK_Rea_BmsTransferDtl_REFERENCE_REA_GoodsID foreign key(GoodsID)      references dbo.Rea_Goods(GoodsID),   constraint FK_Rea_BmsTransferDtl_REFERENCE_REA_QtyDtlID foreign key(QtyDtlID)      references Rea_BmsQtyDtl(QtyDtlID))";

                listSQL.Add(updateSql);
                updateSql = " create table Rea_BmsTransferDtlLink (   LabID D_系统主键               null,   ReaBTDLID D_系统主键               not null,   TransferDtlID D_系统主键               null,   GoodsBarcodeID D_系统主键               null,   SerialNo varchar(100)         null,   Memo                 varchar(Max)         null,   DispOrder            int                  null,   Visible              bit                  null,   CreaterID            bigint               null,   CreaterName          varchar(50)          null,   DataAddTime          datetime             null,   DataUpdateTime       datetime             not null,   DataTimeStamp        timestamp            null,   constraint PK_REA_BMSTRANSFERDTLLINK primary key(ReaBTDLID),   constraint FK_Rea_BmsTransferDtlLink_REFERENCE_REA_TransferDtlID foreign key(TransferDtlID)      references Rea_BmsTransferDtl(TransferDtlID),   constraint FK_Rea_BmsTransferDtlLink_REFERENCE_REA_GoodsBarcodeID foreign key(GoodsBarcodeID)      references Rea_GoodsBarcode(GoodsBarcodeID))";
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

                #region 验货单、验货单明细的供货单ID和供货单明细ID可以为空

                updateSql = " IF COL_LENGTH('BmsCenSaleDocConfirm', 'SaleDocID') IS not NULL alter table BmsCenSaleDocConfirm alter column SaleDocID bigint not null ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('BmsCenSaleDtlConfirm', 'SaleDtlID') IS not NULL alter table BmsCenSaleDtlConfirm alter column SaleDtlID bigint not null ";
                listSQL.Add(updateSql);
                #endregion

                #region 移库单字段修改

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDoc', '操作者ID') IS not NULL exec sp_rename 'Rea_BmsTransferDoc.操作者ID','OperID','column' ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDoc', '操作者名称') IS not NULL exec sp_rename 'Rea_BmsTransferDoc.操作者名称','OperName','column' ";
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

                #region 验货单明细新增订货总单ID、订货总单号、订货明细ID

                updateSql = " IF COL_LENGTH('BmsCenSaleDtlConfirm', 'OrderDocID') IS not NULL  ALTER TABLE BmsCenSaleDtlConfirm   ADD OrderDocID  bigint ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('BmsCenSaleDtlConfirm', 'OrderDocNo') IS not NULL  ALTER TABLE BmsCenSaleDtlConfirm   ADD OrderDocNo  varchar(50)  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('BmsCenSaleDtlConfirm', 'OrderDtlID') IS not NULL  ALTER TABLE BmsCenSaleDtlConfirm   ADD OrderDtlID  bigint ";
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

                #region 验货单、验货单明细的供货单ID和供货单明细ID可以为空

                updateSql = " IF COL_LENGTH('BmsCenSaleDocConfirm', 'SaleDocID') IS not NULL alter table BmsCenSaleDocConfirm alter column SaleDocID bigint ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('BmsCenSaleDtlConfirm', 'SaleDtlID') IS not NULL alter table BmsCenSaleDtlConfirm alter column SaleDtlID bigint  ";
                listSQL.Add(updateSql);
                #endregion

                #region 验货单明细新增订货总单ID、订货总单号、订货明细ID

                updateSql = " IF COL_LENGTH('BmsCenSaleDtlConfirm', 'OrderDocID') IS NULL  ALTER TABLE BmsCenSaleDtlConfirm   ADD OrderDocID  bigint ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('BmsCenSaleDtlConfirm', 'OrderDocNo') IS NULL  ALTER TABLE BmsCenSaleDtlConfirm   ADD OrderDocNo  varchar(50)  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('BmsCenSaleDtlConfirm', 'OrderDtlID') IS NULL  ALTER TABLE BmsCenSaleDtlConfirm   ADD OrderDtlID  bigint ";
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

                #region 验货单加订单编号   
                          
                updateSql = " IF COL_LENGTH('BmsCenSaleDocConfirm', 'OrderDocNo') IS NULL ALTER TABLE BmsCenSaleDocConfirm   ADD OrderDocNo  varchar(50)";
                listSQL.Add(updateSql);

                #endregion

                #region 验货单明细加订单ID外键引用   

                updateSql = "  ALTER TABLE [dbo].[BmsCenSaleDtlConfirm]  WITH CHECK ADD  CONSTRAINT [FK_BmsCenSaleDtlConfirm_REFERENCE_OrderDocID] FOREIGN KEY([OrderDocID])REFERENCES [dbo].[BmsCenOrderDoc] ([OrderDocID])  ";
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
                updateSql = "if Not Exists(Select * from SysColumns where [Name] = \'ThirdOrderDocNo\' and ID = (Select[ID] from SysObjects where Name = \'BmsCenSaleDoc\')) " + "\r" +
                            "begin " + "\r" +
                                "Alter Table BmsCenSaleDoc Add ThirdOrderDocNo varchar(50) " + "\r" +
                                "EXEC sys.sp_addextendedproperty @name = N\'MS_Description\', @value = N\'第三方订货单号\' , @level0type = N\'SCHEMA\',@level0name = N\'dbo\', " +
                                "@level1type = N\'TABLE\',@level1name = N\'BmsCenSaleDoc\', @level2type = N\'COLUMN\',@level2name = N\'ThirdOrderDocNo\'" + "\r" +
                            "end";
                listSQL.Add(updateSql);

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

                #region BmsCenSaleDtlConfirm_供货验收明细单表
                updateSql = " IF COL_LENGTH('BmsCenSaleDtlConfirm', 'BarCodeMgr') IS NULL ALTER TABLE BmsCenSaleDtlConfirm   ADD BarCodeMgr  int ";
                listSQL.Add(updateSql);
                #endregion

                #region Goods_平台产品表
                updateSql = " IF COL_LENGTH('Goods', 'GoodsSort') IS NULL ALTER TABLE Goods   ADD GoodsSort int ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_Goods_本地货品表
                updateSql = " IF COL_LENGTH('Rea_Goods', 'SuitableType') IS NULL ALTER TABLE Rea_Goods   ADD SuitableType  varchar(1000)";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_Goods', 'TestCount') IS NULL ALTER TABLE Rea_Goods   ADD TestCount  int";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_Goods', 'GoodsSort') IS NULL ALTER TABLE Rea_Goods   ADD GoodsSort  int";
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

                #region 供应商条码格式表
                updateSql = "create table Rea_CenBarCodeFormat (   LabID bigint               null,   CBCFID bigint               not null,   PlatformOrgNo        int                  null,   CName varchar(100)         collate Chinese_PRC_CI_AS not null,   BarCodeFormatExample varchar(100)         null,   RegularExpression    varchar(100)         null,   SplitCount           int                  null,   SName                varchar(40)          collate Chinese_PRC_CI_AS null,   ShortCode            varchar(20)            collate Chinese_PRC_CI_AS null,   pinyinzitou          varchar(50)         collate Chinese_PRC_CI_AS null,   DispOrder            int                  null,   IsUse                bit                  null,   Memo                 varchar(500)         collate Chinese_PRC_CI_AS null,   DataAddTime          datetime             null,   DataTimeStamp        timestamp            null,   constraint PK_REA_CENBARCODEFORMAT primary key(CBCFID))";
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

                #region Rea_BmsCenSaleDtlConfirmLink_平台验货明细条码关系表
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirmLink', 'ReceiveFlag') IS NULL ALTER TABLE Rea_BmsCenSaleDtlConfirmLink   ADD ReceiveFlag  bigint ";
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

                #region B_Parameter_参数表
                updateSql = " IF COL_LENGTH('B_Parameter', 'PDictId') IS NULL ALTER TABLE B_Parameter ADD PDictId  bigint ";
                listSQL.Add(updateSql);

                updateSql = "  ALTER TABLE [dbo].[B_Parameter]  WITH CHECK ADD  CONSTRAINT [FK_B_Parameter_REFERENCE_B_Dic] FOREIGN KEY([PDictId])REFERENCES [dbo].[B_Dic] ([DID])  ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsInDtl_入库明细表
                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'SaleDtlConfirmID') IS NULL ALTER TABLE Rea_BmsInDtl ADD SaleDtlConfirmID  bigint ";
                listSQL.Add(updateSql);

                updateSql = "  ALTER TABLE [dbo].[Rea_BmsInDtl]  WITH CHECK ADD  CONSTRAINT [FK_Rea_BmsInDtl_REFERENCE_BmsCenSaleDtlConfirm] FOREIGN KEY([SaleDtlConfirmID])REFERENCES [dbo].[BmsCenSaleDtlConfirm] ([SaleDtlConfirmID])  ";
                listSQL.Add(updateSql);
                #endregion

                #region BmsCenSaleDocConfirm_供货验收单表
                updateSql = " IF COL_LENGTH('BmsCenSaleDocConfirm', 'SourceType') IS NULL ALTER TABLE BmsCenSaleDocConfirm ADD SourceType  bigint ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsReqDoc_申请总单表
                updateSql = " IF COL_LENGTH('Rea_BmsReqDoc', 'ReviewMemo') IS NULL ALTER TABLE Rea_BmsReqDoc ADD ReviewMemo  varchar(max) ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_ChooseGoodsTemplate_产品录入模版字典
                updateSql = " create table Rea_ChooseGoodsTemplate (   LabID bigint               null,   ReaCGTID bigint               not null,   OrgID bigint               null,   OrgName varchar(100)         null,   DeptID               bigint           null,   DeptName             varchar(100)         null,   CName                varchar(100)         null,   Site                 varchar(100)         null,   ContextJson          varchar(max)         null,   SName                varchar(40)          collate Chinese_PRC_CI_AS null,   ShortCode            varchar(20)            collate Chinese_PRC_CI_AS null,   pinyinzitou          varchar(50)         collate Chinese_PRC_CI_AS null,   DispOrder            int                  null,   IsUse                bit                  null,   Memo                 varchar(500)         collate Chinese_PRC_CI_AS null,   CreaterID            bigint               null,   CreatName            varchar(50)          null,   DataAddTime          datetime             null,   DataTimeStamp        timestamp            null,   constraint PK_REA_CHOOSEGOODSTEMPLATE primary key(ReaCGTID),   constraint FK_REA_CHOO_REFERENCE_REA_CENO foreign key(OrgID)      references dbo.Rea_CenOrg(OrgID),   constraint FK_REA_CHOO_REFERENCE_HR_DEPT foreign key(DeptID)      references dbo.HR_Dept(DeptID)) ";
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

                #region B_Parameter_参数表
                //字典类型表系统参数
                updateSql = " if not exists (select 1 from B_DicClass where  DCId =4658943850913198560) INSERT[B_DicClass]([LabID],[DCId],[Code],[Name],[DispOrder],[DataAddTime],[IsUse],[Comment]) VALUES( 1,4658943850913198560, N'BParameter', N'系统参数',37, N'2016/10/12 11:36:26',1, N'系统参数分类');";
                listSQL.Add(updateSql);

                //字典表的帮助文档保存路径
                updateSql = " if not exists (select 1 from B_Dic where  DID =4784659538853660916) INSERT[B_Dic]([DID],[LabID],[DCId],[Shortcode],[Name],[DispOrder],[IsUse],[DataAddTime]) VALUES( 4784659538853660916,1,4658943850913198560, N'SaveHelpHtmlAndJson', N'生成帮助文档保存路径',9,1, N'2016/12/12 12:55:33');";
                listSQL.Add(updateSql);

                //字典表的附件上传保存路径
                updateSql = " if not exists (select 1 from B_Dic where  DID =5535013548029332007) INSERT[B_Dic]([DID],[LabID],[DCId],[Shortcode],[Name],[DispOrder],[IsUse],[Comment],[DataAddTime]) VALUES( 5535013548029332007,1,4658943850913198560, N'UploadFilesPath', N'附件上传保存路径',1,1, N'请配置为物理路径', N'2016/10/12 11:37:19');";
                listSQL.Add(updateSql);

                //字典表的Excel文件导出后保存路径
                updateSql = " if not exists (select 1 from B_Dic where  DID =4707013139216671258) INSERT[B_Dic]([DID],[LabID],[DCId],[Shortcode],[Name],[DispOrder],[IsUse],[Comment],[DataAddTime]) VALUES( 4707013139216671258,1,4658943850913198560, N'ExcelExportSavePath', N'文件导出后保存路径',1,1, N'Excel文件导出后保存路径', N'2016/10/12 11:37:19');";
                listSQL.Add(updateSql);

                //字典表的在线编辑器配置文件
                updateSql = " if not exists (select 1 from B_Dic where  DID =5614935430447999062) INSERT[B_Dic]([DID],[LabID],[DCId],[Shortcode],[Name],[DispOrder],[IsUse],[Comment],[DataAddTime]) VALUES( 5614935430447999062,1,4658943850913198560, N'uploadConfigIson', N'在线编辑器配置文件',1,1, N'ueditor在线编辑器配置文件,默认为根目录下的upload.json,请勿移动和修改', N'2016/10/12 11:37:19');";
                listSQL.Add(updateSql);

                //字典表的上传电子签名保存路径
                updateSql = " if not exists (select 1 from B_Dic where  DID =5606886495082057346) INSERT[B_Dic]([DID],[LabID],[DCId],[Shortcode],[Name],[DispOrder],[IsUse],[Comment],[DataAddTime]) VALUES( 5606886495082057346,1,4658943850913198560, N'UploadEmpSignPath', N'上传电子签名保存路径',1,1, N'上传电子签名保存路径', N'2016/10/12 11:37:19');";
                listSQL.Add(updateSql);

                //字典表的上传图片地址
                updateSql = " if not exists (select 1 from B_Dic where  DID =4683016969821132438) INSERT[B_Dic]([DID],[LabID],[DCId],[Shortcode],[Name],[DispOrder],[IsUse],[Comment],[DataAddTime]) VALUES( 4683016969821132438,1,4658943850913198560, N'UpLoadPicturePath', N'上传图片地址',1,1, N'上传图片地址', N'2016/10/12 11:37:19');";
                listSQL.Add(updateSql);

                //字典表的来货验收确认方式
                updateSql = " if not exists (select 1 from B_Dic where  DID =4683016969821132455) INSERT[B_Dic]([DID],[LabID],[DCId],[Shortcode],[Name],[DispOrder],[IsUse],[Comment],[DataAddTime]) VALUES( 4683016969821132455,1,4658943850913198560, N'SecAccepterAccount', N'来货验收确认方式',10,1, N'验收双确认方式:默认本实验室(1),供应商(2),供应商或实验室(3)', N'2016/10/12 11:37:19');";
                listSQL.Add(updateSql);

                //系统参数表的来货验收确认方式
                updateSql = " if not exists (select 1 from B_Parameter where  ParameterID =4670215574813740766) INSERT[B_Parameter]([LabID],[ParameterID],[PDictId],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[Shortcode],[DispOrder],[IsUse],[DataAddTime],[ParaDesc]) VALUES( 1,4670215574813740766,4683016969821132455, N'来货验收确认方式', N'4', N'SYS', N'SecAccepterAccount', N'1', N'',4,1, N'2016/10/14 16:17:57','验收双确认方式:默认本实验室(1),供应商(2),供应商或实验室(3)');";
                listSQL.Add(updateSql);

                //系统参数表的在线编辑器配置文件
                updateSql = "if not exists (select 1 from B_Parameter where  ParameterID =4670215574813740793) INSERT[B_Parameter]([LabID],[ParameterID],[PDictId],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[Shortcode],[DispOrder],[IsUse],[DataAddTime]) VALUES( 1,4670215574813740793,5614935430447999062, N'在线编辑器配置文件', N'4', N'SYS', N'uploadConfigIson', N'upload.json', N'4',4,1, N'2016/10/14 16:17:57');";
                listSQL.Add(updateSql);

                //系统参数表的附件上传保存路径
                updateSql = " if not exists (select 1 from B_Parameter where  ParameterID =5319869423748978044) INSERT[B_Parameter]([LabID],[ParameterID],[PDictId],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[DispOrder],[IsUse],[DataAddTime])    VALUES( 1,5319869423748978044,5535013548029332007, N'附件上传保存路径', N'2', N'SYS', N'UploadFilesPath', NULL,1,1, N'2016/10/14 16:18:31');";
                listSQL.Add(updateSql);

                //系统参数表的Excel文件导出后保存路径
                updateSql = " if not exists (select 1 from B_Parameter where  ParameterID =5355293405750102840) INSERT[B_Parameter]([LabID],[ParameterID],[PDictId],[Name],[ParaType],[ParaNo],[ParaValue],[DispOrder],[IsUse],[DataAddTime]) VALUES( 1,5355293405750102840,4707013139216671258, N'Excel文件导出后保存路径', N'SYS', N'ExcelExportSavePath', NULL,0,1, N'2016/10/25 14:35:09');";
                listSQL.Add(updateSql);

                //系统参数表的上传图片地址
                updateSql = " if not exists (select 1 from B_Parameter where  ParameterID =5401113517232823907) INSERT[B_Parameter]([LabID],[ParameterID],[PDictId],[Name],[SName],[ParaType],[ParaNo],[ParaValue],[Shortcode],[DispOrder],[IsUse],[DataAddTime]) VALUES( 1,5401113517232823907,4683016969821132438, N'上传图片地址', N'3', N'WebConfig', N'UpLoadPicturePath', N'Images', N'3',3,1, N'2016/10/14 16:18:55');";
                listSQL.Add(updateSql);

                //系统参数表的生成帮助文档保存路径
                updateSql = " if not exists (select 1 from B_Parameter where  ParameterID =5534894644551449999) INSERT[B_Parameter]([LabID],[ParameterID],[PDictId],[Name],[ParaType],[ParaNo],[ParaValue],[DispOrder],[IsUse],[DataAddTime]) VALUES( 1,5534894644551449999,4784659538853660916, N'生成帮助文档保存路径', N'SYS', N'SaveHelpHtmlAndJson', NULL,0,1, N'2016/12/12 12:56:27');";
                listSQL.Add(updateSql);

                //系统参数表的上传电子签名保存路径
                updateSql = " if not exists (select 1 from B_Parameter where  ParameterID =5625350926522958857) INSERT[B_Parameter]([LabID],[ParameterID],[PDictId],[Name],[ParaType],[ParaNo],[ParaValue],[DispOrder],[IsUse],[DataAddTime]) VALUES( 1,5625350926522958857,5606886495082057346, N'上传电子签名保存路径', N'SYS', N'UploadEmpSignPath', NULL,1,1, N'2016/11/1 14:03:33');";
                listSQL.Add(updateSql);

                #endregion

                #region BmsCenSaleDocConfirm_供货验收单表新增发票号
                updateSql = " IF COL_LENGTH('BmsCenSaleDocConfirm', 'InvoiceNo') IS NULL ALTER TABLE BmsCenSaleDocConfirm ADD InvoiceNo  varchar(50) ";
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

                #region Rea_Goods_本地货品表
                updateSql = " IF COL_LENGTH('Rea_Goods', 'IsMinUnit') IS NULL ALTER TABLE Rea_Goods ADD IsMinUnit  bit ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_Goods', 'GoodsSN') IS NOT NULL ALTER TABLE Rea_Goods DROP COLUMN GoodsSN ;";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_Goods', 'GonvertSort') IS NOT NULL ALTER TABLE Rea_Goods DROP COLUMN GonvertSort ;";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_Goods', 'GonvertGroupCode') IS NOT NULL ALTER TABLE Rea_Goods DROP COLUMN GonvertGroupCode ;";
                listSQL.Add(updateSql);
                #endregion

                #region 删除 Rea_BmsInDtlLink_入库明细条码关系表
                updateSql = " drop table Rea_BmsInDtlLink; ";
                listSQL.Add(updateSql);
                #endregion

                #region 删除 Rea_BmsQtyDtlLink_库存条码关系表
                updateSql = " drop table Rea_BmsQtyDtlLink; ";
                listSQL.Add(updateSql);
                #endregion

                #region 删除 Rea_BmsOutDtlLink_出库明细条码关系表
                updateSql = " drop table Rea_BmsOutDtlLink; ";
                listSQL.Add(updateSql);
                #endregion

                #region 删除 Rea_BmsTransferDtlLink_移库明细条码关系表
                updateSql = " drop table Rea_BmsTransferDtlLink; ";
                listSQL.Add(updateSql);
                #endregion

                #region 删除 Rea_BmsCenSaleDtlConfirmLink_平台验货明细条码关系表
                updateSql = " drop table Rea_BmsCenSaleDtlConfirmLink; ";
                listSQL.Add(updateSql);
                #endregion

                #region 删除 Rea_BmsCenSaleDtlLink_平台供货明细条码关系表
                updateSql = " drop table Rea_BmsCenSaleDtlLink; ";
                listSQL.Add(updateSql);
                #endregion

                #region 删除 Rea_GoodsBarcode_货品明细条码表
                updateSql = " drop table Rea_GoodsBarcode; ";
                listSQL.Add(updateSql);
                #endregion

                #region BmsCenSaleDtl_平台供货明细表
                updateSql = " IF COL_LENGTH('BmsCenSaleDtl', 'SysLotSerial') IS NULL ALTER TABLE BmsCenSaleDtl ADD SysLotSerial  varchar(100) ; ";
                listSQL.Add(updateSql);

                //updateSql = " IF COL_LENGTH('BmsCenSaleDtl', 'MixSerial') IS NOT NULL ALTER TABLE BmsCenSaleDtl  DROP COLUMN MixSerial ;";
                //listSQL.Add(updateSql);

                //updateSql = "  IF COL_LENGTH('BmsCenSaleDtl', 'PackSerial') IS NOT NULL ALTER TABLE BmsCenSaleDtl DROP COLUMN PackSerial ;";
                //listSQL.Add(updateSql);
                #endregion

                #region BmsCenSaleDtlConfirm_供货验收明细单表
                updateSql = " IF COL_LENGTH('BmsCenSaleDtlConfirm', 'SysLotSerial') IS NULL ALTER TABLE BmsCenSaleDtlConfirm ADD SysLotSerial  varchar(100) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('BmsCenSaleDtlConfirm', 'MixSerial') IS NOT NULL ALTER TABLE BmsCenSaleDtlConfirm  DROP COLUMN MixSerial ;";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('BmsCenSaleDtlConfirm', 'PackSerial') IS NOT NULL ALTER TABLE BmsCenSaleDtlConfirm DROP COLUMN PackSerial ;";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsInDtl_入库明细表
                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'GoodsSerial') IS NULL ALTER TABLE Rea_BmsInDtl ADD GoodsSerial  varchar(100) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'LotSerial') IS NULL ALTER TABLE Rea_BmsInDtl ADD LotSerial  varchar(100) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'SysLotSerial') IS NULL ALTER TABLE Rea_BmsInDtl ADD SysLotSerial  varchar(100) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'SerialNo') IS NOT NULL ALTER TABLE Rea_BmsInDtl  DROP COLUMN SerialNo ;";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsOutDtl_出库明细表               

                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'SysLotSerial') IS NULL ALTER TABLE Rea_BmsOutDtl ADD SysLotSerial  varchar(100) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'PackSerial') IS NOT NULL ALTER TABLE Rea_BmsOutDtl  DROP COLUMN PackSerial ;";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'MixSerial') IS NOT NULL ALTER TABLE Rea_BmsOutDtl  DROP COLUMN MixSerial ;";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtl_库存表
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'GoodsSerial') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD GoodsSerial  varchar(100) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'LotSerial') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD LotSerial  varchar(100) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'SysLotSerial') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD SysLotSerial  varchar(100) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'SerialNo') IS NOT NULL ALTER TABLE Rea_BmsQtyDtl  DROP COLUMN SerialNo ;";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtlOperation_库存表操作记录
                updateSql = " create table Rea_BmsQtyDtlOperation (   LabID bigint               null,   QtyDtlOID bigint               not null,   ReaCompanyID bigint               null,   CompanyName varchar(100)         null,   GoodsID              bigint               null,   GoodsName            varchar(100)         null,   LotNo                varchar(100)         null,   StorageID            bigint               null,   PlaceID              bigint               null,   StorageName          varchar(100)         null,   PlaceName            varchar(100)         null,   GoodsUnitID          bigint               null,   OrgID                bigint               null,   GoodsUnit            varchar(100)         null,   GoodsQty             float                null,   Price                float                null,   SumTotal             float                null,   TaxRate              float                null,   OutFlag              int                  null,   SumFlag              int                  null,   IOFlag               int                  null,   GoodsSerial          varchar(100)         collate Chinese_PRC_CI_AS null,   LotSerial            varchar(100)         collate Chinese_PRC_CI_AS null,   SysLotSerial         varchar(100)         null,   ZX1                  varchar(50)          null,   ZX2                  varchar(50)          null,   ZX3                  varchar(50)          null,   Memo                 varchar(Max)         null,   DispOrder            int                  null,   Visible              bit                  null,   CreaterID            bigint               null,   CreaterName          varchar(50)          null,   DataAddTime          datetime             null,   DataUpdateTime       datetime             not null,   DataTimeStamp        timestamp            null,   BDocNo               varchar(100)         null,   BDocID               bigint               null,   BDtlID               bigint               null,   OperTypeID           bigint               null,   OperTypeName         varchar(50)          null,   ChangeCount          float                null,   constraint PK_REA_BMSQTYDTLOPERATION primary key(QtyDtlOID),   constraint FK_REA_BMSQ_REFERENCE_REA_PlaceID foreign key(PlaceID)      references Rea_Place(PlaceID),   constraint FK_REA_BMSQ_REFERENCE_REA_GOODUnitID foreign key(GoodsUnitID)      references Rea_GoodsUnit(GoodsUnitID),   constraint FK_REA_BMSQ_REFERENCE_REA_CENOrgID foreign key(OrgID)      references dbo.Rea_CenOrg(OrgID),   constraint FK_REA_BMSQ_REFERENCE_REA_GOODID foreign key(GoodsID)      references dbo.Rea_Goods(GoodsID),   constraint FK_REA_BMSQ_REFERENCE_REA_StorageID foreign key(StorageID)      references Rea_Storage(StorageID)); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_GoodsBarcodeOperation_货品条码操作列表
                updateSql = " create table Rea_GoodsBarcodeOperation (   LabID bigint               null,   ReaGBOID bigint               not null,   BDocNo varchar(100)         null,   BDocID               bigint               null,   BDtlID               bigint               null,   OperTypeID           bigint               null,   OperTypeName         varchar(50)          null,   SysPackSerial        varchar(100)         null,   OtherPackSerial      varchar(100)         null,   UsePackSerial        varchar(100)         null,   LotNo                varchar(100)         null,   Memo                 varchar(Max)         null,   DispOrder            int                  null,   Visible              bit                  null,   CreaterID            bigint               null,   CreaterName          varchar(50)          null,   DataAddTime          datetime             null,   DataUpdateTime       datetime             not null,   DataTimeStamp        timestamp            null,   ReaCompanyID         bigint               null,   CompanyName          varchar(200)         null,   GoodsID              bigint               null,   GoodsCName           varchar(200)         collate Chinese_PRC_CI_AS not null,   GoodsUnitID          bigint               null,   GoodsUnit            varchar(50)          null,   IsOutFlag            bit                  null,   constraint PK_REA_GOODSBARCODEOPERATION primary key(ReaGBOID),   constraint FK_REA_GOOD_REFERENCE_REA_GoodsID foreign key(GoodsID)      references dbo.Rea_Goods(GoodsID),   constraint FK_REA_GOOD_REFERENCE_REA_GoodsUnitID foreign key(GoodsUnitID)      references Rea_GoodsUnit(GoodsUnitID)); ";
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

                #region BmsCenSaleDtl_供货单明细
                updateSql = " IF COL_LENGTH('BmsCenSaleDtl', 'GoodsQty') IS not NULL ALTER TABLE BmsCenSaleDtl alter column GoodsQty float ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('BmsCenSaleDtl', 'IOGoodsQty') IS not NULL ALTER TABLE BmsCenSaleDtl alter column IOGoodsQty float ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('BmsCenSaleDtl', 'DtlCount') IS not NULL ALTER TABLE BmsCenSaleDtl alter column DtlCount float ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('BmsCenSaleDtl', 'AcceptCount') IS not NULL ALTER TABLE BmsCenSaleDtl alter column AcceptCount float ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('BmsCenSaleDtl', 'RefuseCount') IS not NULL ALTER TABLE BmsCenSaleDtl alter column RefuseCount float ; ";
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

                #region 平台供单明细BmsCenSaleDoc
                updateSql = " IF COL_LENGTH('BmsCenSaleDoc', 'ThirdOrderDocNo') IS NOT NULL ALTER TABLE BmsCenSaleDoc DROP COLUMN ThirdOrderDocNo ";
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

                #region 平台供单明细BmsCenSaleDoc
                updateSql = " IF COL_LENGTH('Goods', 'DownloadFlag') IS NULL ALTER TABLE Goods add DownloadFlag int ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.39");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.39) Update Error, Please Check The Log!");
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
            else
            {
                //ExecuteUpdateSQL("if not Exists(Select * from B_Parameter where ParaType =\'SYS\' and ParaNo=\'SYS_DBVersion\') " + "\r" +
                //    "begin " + "\r" +
                //    "insert into [B_Parameter]([LabID],[ParameterID],[Name],[SName],[ParaType],[ParaNo],[ParaValue]," +
                //    "[ParaDesc],[Shortcode],[DispOrder],[PinYinZiTou],[IsUse],[DataAddTime],[DataUpdateTime]) " +
                //    " Values(1," +//LabID
                //    ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "," +//ParameterID
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
                //     "\'" + DateTime.Now.ToString() + "\'," +//DataAddTime
                //     "null" + //DataUpdateTime
                //     ")" + "\r" +
                //     " end ");
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
