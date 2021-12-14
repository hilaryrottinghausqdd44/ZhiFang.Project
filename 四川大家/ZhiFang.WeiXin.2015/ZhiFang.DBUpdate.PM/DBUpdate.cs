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

        static string MainAssemblyFile = "ZhiFang.WeiXin.dll";//可以从配置文件获取

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
            dicVersion.Add("1.0.0.14", "1.0.0.14");
            //dicVersion.Add("1.0.0.15", "1.0.0.15");
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
                List<string> listSQL = new List<string>();

                updateSql = "CREATE TABLE [dbo].[B_WeiXin_Emp_Link](" + "	[LabID] [dbo].[D_系统主键] NULL," + "	[WeiXin_Emp_LinkID] [dbo].[D_系统主键] NOT NULL," + "	[WeiXinUserID] [dbo].[D_系统主键] NULL," + "	[EmpID] [dbo].[D_系统主键] NULL," + "	[EmpName] [varchar](50) NULL," + "	[DataAddTime] [datetime] NOT NULL," + "	[DataTimeStamp] [timestamp] NOT NULL," + " CONSTRAINT [PK_B_WEIXIN_EMP_LINK] PRIMARY KEY CLUSTERED " + "(" + "	[WeiXin_Emp_LinkID] ASC" + ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]" + ") ON [PRIMARY];CREATE TABLE [dbo].[B_WeiXin_PushMessageTemplate](" + "	[LabID] [dbo].[D_系统主键] NULL," + "	[PMTID] [dbo].[D_系统主键] NOT NULL," + "	[PMTKey] [varchar](500) NULL," + "	[Name] [varchar](40) NULL," + "	[SName] [varchar](40) NULL," + "	[Shortcode] [varchar](40) NULL," + "	[PinYinZiTou] [dbo].[D_汉语拼音字头] NULL," + "	[Comment] [ntext] NULL," + "	[IsUse] [bit] NULL," + "	[DataAddTime] [datetime] NULL," + "	[DataTimeStamp] [timestamp] NULL," + " CONSTRAINT [PK_B_WEIXIN_PUSHMESSAGETEMPLAT] PRIMARY KEY CLUSTERED " + "(" + "	[PMTID] ASC" + ")WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]" + ") ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];ALTER TABLE [dbo].[B_WeiXin_Emp_Link]  WITH CHECK ADD  CONSTRAINT [FK_B_WeiXin_Emp_Link_HR_Employee] FOREIGN KEY([EmpID])" + "REFERENCES [dbo].[HR_Employee] ([EmpID])" + "; " + "" + "ALTER TABLE [dbo].[B_WeiXin_Emp_Link]  WITH CHECK ADD  CONSTRAINT [FK_B_WEIXIN_REFERENCE_B_WEIXIN1] FOREIGN KEY([WeiXinUserID])" + "REFERENCES [dbo].[B_WeiXinAccount] ([WeiXinUserID])" + "; " + "" + "ALTER TABLE [dbo].[B_WeiXin_Emp_Link] CHECK CONSTRAINT [FK_B_WEIXIN_REFERENCE_B_WEIXIN1]" + "; " + "" + "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'平台客户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'B_WeiXin_Emp_Link', @level2type=N'COLUMN',@level2name=N'LabID'" + "; " + "" + "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'微信帐号和员工关系ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'B_WeiXin_Emp_Link', @level2type=N'COLUMN',@level2name=N'WeiXin_Emp_LinkID'" + "; " + "" + "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'微信关注用户ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'B_WeiXin_Emp_Link', @level2type=N'COLUMN',@level2name=N'WeiXinUserID'" + "; " + "" + "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'B_WeiXin_Emp_Link', @level2type=N'COLUMN',@level2name=N'EmpID'" + "; " + "" + "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'员工名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'B_WeiXin_Emp_Link', @level2type=N'COLUMN',@level2name=N'EmpName'" + "; " + "" + "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'微信账户绑定员工表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'B_WeiXin_Emp_Link'" + "; " + "" + "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'WeiXin服务器中的模板ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'B_WeiXin_PushMessageTemplate', @level2type=N'COLUMN',@level2name=N'PMTKey'" + "; " + "" + "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'简称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'B_WeiXin_PushMessageTemplate', @level2type=N'COLUMN',@level2name=N'SName'" + "; " + "" + "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'快捷码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'B_WeiXin_PushMessageTemplate', @level2type=N'COLUMN',@level2name=N'Shortcode'" + "; " + "" + "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'汉字拼音字头' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'B_WeiXin_PushMessageTemplate', @level2type=N'COLUMN',@level2name=N'PinYinZiTou'" + "; " + "" + "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'B_WeiXin_PushMessageTemplate', @level2type=N'COLUMN',@level2name=N'Comment'" + "; " + "" + "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否使用' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'B_WeiXin_PushMessageTemplate', @level2type=N'COLUMN',@level2name=N'IsUse'" + "; " + "" + "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'B_WeiXin_PushMessageTemplate', @level2type=N'COLUMN',@level2name=N'DataAddTime'" + "; " + "" + "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'时间戳' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'B_WeiXin_PushMessageTemplate', @level2type=N'COLUMN',@level2name=N'DataTimeStamp'" + "; " + "" + "EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'消息推送模版' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'B_WeiXin_PushMessageTemplate'" + "; ";
                listSQL.Add(updateSql);

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
                List<string> listSQL = new List<string>();
                #region 实验室项目表
                //实验室项目表加图片字段
                updateSql = " IF COL_LENGTH('B_Lab_TestItem', 'Pic') IS NULL ALTER TABLE B_Lab_TestItem ADD Pic varchar(500) ";
                listSQL.Add(updateSql);

                //实验室项目表加市场价格字段
                updateSql = " IF COL_LENGTH('B_Lab_TestItem', 'MarketPrice') IS NULL ALTER TABLE B_Lab_TestItem ADD MarketPrice float ";
                listSQL.Add(updateSql);

                //实验室项目表加大家价格字段
                updateSql = " IF COL_LENGTH('B_Lab_TestItem', 'GreatMasterPrice') IS NULL ALTER TABLE B_Lab_TestItem ADD GreatMasterPrice float ";
                listSQL.Add(updateSql);
                #endregion
                #region 中心项目表
                //中心项目表加图片字段
                updateSql = " IF COL_LENGTH('TestItem', 'Pic') IS NULL ALTER TABLE TestItem ADD Pic varchar(500) ";
                listSQL.Add(updateSql);

                //中心项目表加市场价格字段
                updateSql = " IF COL_LENGTH('TestItem', 'MarketPrice') IS NULL ALTER TABLE TestItem ADD MarketPrice float ";
                listSQL.Add(updateSql);

                //中心项目表加大家价格字段
                updateSql = " IF COL_LENGTH('TestItem', 'GreatMasterPrice') IS NULL ALTER TABLE TestItem ADD GreatMasterPrice float ";
                listSQL.Add(updateSql);
                #endregion
                #region 新增ItemNo
                //医嘱项目表加项目编码字段
                updateSql = " IF COL_LENGTH('OS_DoctorOrderItem', 'ItemNo') IS NULL ALTER TABLE OS_DoctorOrderItem ADD ItemNo varchar(50) ";
                listSQL.Add(updateSql);

                //订单项目表加项目编码字段
                updateSql = " IF COL_LENGTH('OS_UserOrderItem', 'ItemNo') IS NULL ALTER TABLE OS_UserOrderItem ADD ItemNo varchar(50) ";
                listSQL.Add(updateSql);

                //消费单项目表加项目编码字段
                updateSql = " IF COL_LENGTH('OS_UserConsumerItem', 'ItemNo') IS NULL ALTER TABLE OS_UserConsumerItem ADD ItemNo varchar(50) ";
                listSQL.Add(updateSql);

                //项目分类表加项目编码字段
                updateSql = " IF COL_LENGTH('OS_ItemProductClassTreeLink', 'ItemNo') IS NULL ALTER TABLE OS_ItemProductClassTreeLink ADD ItemNo varchar(50) ";
                listSQL.Add(updateSql);

                //特推项目表加项目编码字段
                updateSql = " IF COL_LENGTH('OS_RecommendationItemProduct', 'ItemNo') IS NULL ALTER TABLE OS_RecommendationItemProduct ADD ItemNo varchar(50) ";
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
                List<string> listSQL = new List<string>();
                #region 实验室项目表
                //实验室项目表加图片字段
                updateSql = " IF COL_LENGTH('OS_DoctorOrderItem', 'ItemCName') IS NULL ALTER TABLE OS_DoctorOrderItem ADD ItemCName varchar(50) ";
                listSQL.Add(updateSql);

                //实验室项目表加市场价格字段
                updateSql = "  IF COL_LENGTH('OS_UserOrderItem', 'ItemCName') IS NULL ALTER TABLE OS_UserOrderItem ADD ItemCName varchar(50) ";
                listSQL.Add(updateSql);

                //实验室项目表加大家价格字段
                updateSql = " IF COL_LENGTH('OS_UserConsumerItem', 'ItemCName') IS NULL ALTER TABLE OS_UserConsumerItem ADD ItemCName varchar(50) ";
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
                List<string> listSQL = new List<string>();
                #region 实验室项目表
                //实验室项目表加图片字段
                updateSql = " IF COL_LENGTH('B_DoctorAccount', 'AreaID') IS NULL ALTER TABLE B_DoctorAccount ADD AreaID bigint ";
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
                List<string> listSQL = new List<string>();
                #region B_LabTestItem和OS_RecommendationItemProduct分别添加咨询费比率
                //B_LabTestItem添加咨询费比率
                updateSql = " IF COL_LENGTH('B_Lab_TestItem', 'BonusPercent') IS NULL ALTER TABLE B_Lab_TestItem ADD BonusPercent float; ";
                listSQL.Add(updateSql);
                //OS_RecommendationItemProduct添加咨询费比率
                updateSql = " IF COL_LENGTH('OS_RecommendationItemProduct', 'BonusPercent') IS NULL ALTER TABLE OS_RecommendationItemProduct ADD BonusPercent float; ";
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
                List<string> listSQL = new List<string>();
                #region OS_UserOrderItem添加项目固定咨询费
                updateSql = " IF COL_LENGTH('OS_DoctorOrderItem', 'ItemBonusPrice') IS NULL ALTER TABLE OS_DoctorOrderItem ADD ItemBonusPrice float; ";
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
                List<string> listSQL = new List<string>();
                #region B_WeiXinAccount添加项阅读并同意协议时间字段
                updateSql = " IF COL_LENGTH('B_WeiXinAccount', 'ReadAgreement') IS NULL ALTER TABLE B_WeiXinAccount ADD ReadAgreement [datetime] NULL ; ";
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
                List<string> listSQL = new List<string>();
                #region OS_DoctorBonus 添加字段
                updateSql = " IF COL_LENGTH('OS_DoctorBonus', 'ReturnCode') IS NULL ALTER TABLE OS_DoctorBonus ADD ReturnCode varchar(50) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('OS_DoctorBonus', 'ResultCode') IS NULL ALTER TABLE OS_DoctorBonus ADD ResultCode varchar(50) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('OS_DoctorBonus', 'ReturnMsg') IS NULL ALTER TABLE OS_DoctorBonus ADD ReturnMsg varchar(500) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('OS_DoctorBonus', 'ErrCode') IS NULL ALTER TABLE OS_DoctorBonus ADD ErrCode varchar(50) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('OS_DoctorBonus', 'ErrCodeDes') IS NULL ALTER TABLE OS_DoctorBonus ADD ErrCodeDes varchar(500) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('OS_DoctorBonus', 'ErrorMemo') IS NULL ALTER TABLE OS_DoctorBonus ADD ErrorMemo varchar(500) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('OS_DoctorBonus', 'PaymentNo') IS NULL ALTER TABLE OS_DoctorBonus ADD PaymentNo varchar(50) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('OS_DoctorBonus', 'PaymentTime') IS NULL ALTER TABLE OS_DoctorBonus ADD PaymentTime [datetime] ; ";
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
                List<string> listSQL = new List<string>();
                #region B_Lab_TestItem添加字段
                updateSql = " IF COL_LENGTH('B_Lab_TestItem', 'BonusPercent') IS NULL ALTER TABLE B_Lab_TestItem ADD BonusPercent float ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_Lab_TestItem', 'CostPrice') IS NULL ALTER TABLE B_Lab_TestItem ADD CostPrice float ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_Lab_TestItem', 'InspectionPrice') IS NULL ALTER TABLE B_Lab_TestItem ADD InspectionPrice float ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_Lab_TestItem', 'ItemID') IS not NULL alter table B_Lab_TestItem drop constraint PK_B_Lab_TestItem_2;ALTER TABLE B_Lab_TestItem ALTER COLUMN ItemID bigint not NULL;alter table B_Lab_TestItem add constraint PK_B_Lab_TestItem_2 primary key(ItemID); ";
                listSQL.Add(updateSql);
                #endregion

                #region TestItem添加字段
                updateSql = " IF COL_LENGTH('TestItem', 'BonusPercent') IS NULL ALTER TABLE TestItem ADD BonusPercent float ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('TestItem', 'CostPrice') IS NULL ALTER TABLE TestItem ADD CostPrice float ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('TestItem', 'InspectionPrice') IS NULL ALTER TABLE TestItem ADD InspectionPrice float ; ";
                listSQL.Add(updateSql);

                #endregion

                #region B_DoctorAccount添加字段检验技师标志0医生1检验技师
                updateSql = " IF COL_LENGTH('B_DoctorAccount', 'DoctorAccountType') IS NULL ALTER TABLE B_DoctorAccount ADD DoctorAccountType bigint ; ";
                listSQL.Add(updateSql);
                #endregion

                #region OS_UserConsumerForm用户消费单
                updateSql = " IF COL_LENGTH('OS_UserConsumerForm', 'TypeID') IS NULL ALTER TABLE OS_UserConsumerForm ADD TypeID bigint ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('OS_UserConsumerForm', 'TypeName') IS NULL ALTER TABLE OS_UserConsumerForm ADD TypeName varchar(50) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region OS_UserOrderForm用户订单
                updateSql = " IF COL_LENGTH('OS_UserOrderForm', 'TypeID') IS NULL ALTER TABLE OS_UserOrderForm ADD TypeID bigint ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('OS_UserOrderForm', 'TypeName') IS NULL ALTER TABLE OS_UserOrderForm ADD TypeName varchar(50); ";
                listSQL.Add(updateSql);
                #endregion

                #region OS_DoctorOrderForm医生医嘱单
                updateSql = " IF COL_LENGTH('OS_DoctorOrderForm', 'TypeID') IS NULL ALTER TABLE OS_DoctorOrderForm ADD TypeID bigint ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('OS_DoctorOrderForm', 'TypeName') IS NULL ALTER TABLE OS_DoctorOrderForm ADD TypeName varchar(50) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region B_Lab_GroupItem添加字段组套内价格
                updateSql = "  IF COL_LENGTH('B_Lab_GroupItem', 'Id') IS not NULL ALTER TABLE B_Lab_GroupItem ALTER column Id bigint ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_Lab_GroupItem', 'Price') IS NULL ALTER TABLE B_Lab_GroupItem ADD Price float ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_Lab_GroupItem', 'DataAddTime') IS NULL ALTER TABLE B_Lab_GroupItem ADD DataAddTime datetime ; ";
                listSQL.Add(updateSql);

                #endregion

                #region ItemColorDict项目颜色字典表
                updateSql = " IF COL_LENGTH('ItemColorDict', 'ColorID') IS not NULL alter table ItemColorDict drop constraint PK_ColorDict;ALTER TABLE ItemColorDict ALTER COLUMN ColorID bigint not NULL;alter table ItemColorDict add constraint PK_ColorDict primary key(ColorID); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('ItemColorDict', 'DataAddTime') IS NULL ALTER TABLE ItemColorDict ADD DataAddTime datetime ; ";
                listSQL.Add(updateSql);
                #endregion

                #region ItemColorAndSampleTypeDetail项目颜色样本类型关系表
                updateSql = " drop index PK_ItemColorAndSampleTypeDetail  on  ItemColorAndSampleTypeDetail;ALTER TABLE ItemColorAndSampleTypeDetail ALTER column ColorId bigint; ALTER TABLE ItemColorAndSampleTypeDetail ALTER column SampleTypeNo bigint;               CREATE UNIQUE NONCLUSTERED INDEX PK_ItemColorAndSampleTypeDetail ON dbo.ItemColorAndSampleTypeDetail ( ColorId, SampleTypeNo ) WITH(STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY] ;";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('ItemColorAndSampleTypeDetail', 'DataAddTime') IS NULL ALTER TABLE ItemColorAndSampleTypeDetail ADD DataAddTime datetime ; ";
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
                List<string> listSQL = new List<string>();
                #region CLIENTELE主键更改为bigint
                updateSql = "  IF COL_LENGTH('CLIENTELE', 'ClIENTNO') IS not NULL alter table CLIENTELE drop constraint PK_CLIENTELE;ALTER TABLE CLIENTELE ALTER COLUMN ClIENTNO bigint not NULL;alter table CLIENTELE add constraint PK_CLIENTELE primary key(ClIENTNO); ";
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
                List<string> listSQL = new List<string>();
                #region OS_DoctorOrderForm添加字段
                updateSql = " IF COL_LENGTH('OS_DoctorOrderForm', 'CollectionFlag') IS NULL ALTER TABLE OS_DoctorOrderForm ADD CollectionFlag bit ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('OS_DoctorOrderForm', 'CollectionPrice') IS NULL ALTER TABLE OS_DoctorOrderForm ADD CollectionPrice float ; ";
                listSQL.Add(updateSql);
                #endregion

                #region OS_UserOrderForm添加字段
                updateSql = " IF COL_LENGTH('OS_UserOrderForm', 'CollectionFlag') IS NULL ALTER TABLE OS_UserOrderForm ADD CollectionFlag bit ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('OS_UserOrderForm', 'CollectionPrice') IS NULL ALTER TABLE OS_UserOrderForm ADD CollectionPrice float ; ";
                listSQL.Add(updateSql);
                #endregion

                #region OS_UserConsumerForm添加字段
                updateSql = " IF COL_LENGTH('OS_UserConsumerForm', 'CollectionFlag') IS NULL ALTER TABLE OS_UserConsumerForm ADD CollectionFlag bit ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('OS_UserConsumerForm', 'CollectionPrice') IS NULL ALTER TABLE OS_UserConsumerForm ADD CollectionPrice float ; ";
                listSQL.Add(updateSql);
                #endregion

                #region OS_ManagerRefundForm添加字段
                updateSql = " IF COL_LENGTH('OS_ManagerRefundForm', 'CollectionFlag') IS NULL ALTER TABLE OS_ManagerRefundForm ADD CollectionFlag bit ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('OS_ManagerRefundForm', 'CollectionPrice') IS NULL ALTER TABLE OS_ManagerRefundForm ADD CollectionPrice float ; ";
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
                List<string> listSQL = new List<string>();

                #region OS_UserOrderForm添加字段
                updateSql = " IF COL_LENGTH('OS_UserOrderForm', 'DoctMobileCode') IS NULL ALTER TABLE OS_UserOrderForm ADD DoctMobileCode varchar(20) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('OS_UserOrderForm', 'UserMobileCode') IS NULL ALTER TABLE OS_UserOrderForm ADD UserMobileCode varchar(20) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region OS_DoctorOrderForm添加字段
                updateSql = " IF COL_LENGTH('OS_DoctorOrderForm', 'DoctMobileCode') IS NULL ALTER TABLE OS_DoctorOrderForm ADD DoctMobileCode varchar(20) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('OS_DoctorOrderForm', 'UserMobileCode') IS NULL ALTER TABLE OS_DoctorOrderForm ADD UserMobileCode varchar(20) ; ";
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
                List<string> listSQL = new List<string>();

                #region BusinessLogicClientControl修改ID字段为非自增，修改类型为bigint
                updateSql = " set IDENTITY_INSERT BusinessLogicClientControl on; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('BusinessLogicClientControl', 'Id') IS not NULL ALTER TABLE BusinessLogicClientControl ALTER COLUMN Id bigint; ";
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
                List<string> listSQL = new List<string>();

                #region BarCodeForm表中CollecterID字段修改类型为bigint

                updateSql = " IF COL_LENGTH('BarCodeForm', 'CollecterID') IS not NULL ALTER TABLE BarCodeForm ALTER COLUMN CollecterID bigint; ";
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
                List<string> listSQL = new List<string>();

                #region B_Lab_SampleType表修改SampleTypeID字段为非自增，修改SampleTypeID字段类型为bigint，修改LabSampleTypeNo字段类型为varchar(100)
                
                updateSql = " IF COL_LENGTH('B_Lab_SampleType', 'SampleTypeID') IS not NULL ALTER TABLE B_Lab_SampleType ALTER COLUMN SampleTypeID bigint; ";
                listSQL.Add(updateSql);
                //修改字段类型后，需要进行大量的代码修改
                //updateSql = " IF COL_LENGTH('B_Lab_SampleType', 'LabSampleTypeNo') IS not NULL ALTER TABLE B_Lab_SampleType ALTER COLUMN LabSampleTypeNo varchar(100); ";
                //listSQL.Add(updateSql);
                #endregion
                #region B_Lab_SickType表修改SickTypeID字段为非自增，修改SickTypeID字段类型为bigint，修改LabSickTypeNo字段类型为varchar(100)

                updateSql = " IF COL_LENGTH('B_Lab_SickType', 'SickTypeID') IS not NULL ALTER TABLE B_Lab_SickType ALTER COLUMN SickTypeID bigint; ";
                listSQL.Add(updateSql);
                //修改字段类型后，需要进行大量的代码修改
                //updateSql = " IF COL_LENGTH('B_Lab_SickType', 'LabSickTypeNo') IS not NULL ALTER TABLE B_Lab_SickType ALTER COLUMN LabSickTypeNo varchar(100); ";
                //listSQL.Add(updateSql);
                #endregion    
                #region B_Lab_District表修改DistrictID字段为非自增，修改DistrictID字段类型为bigint，修改LabDistrictNo字段类型为varchar(100)

                updateSql = " IF COL_LENGTH('B_Lab_District', 'DistrictID') IS not NULL ALTER TABLE B_Lab_District ALTER COLUMN DistrictID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_Lab_District', 'LabDistrictNo') IS not NULL ALTER TABLE B_Lab_District ALTER COLUMN LabDistrictNo varchar(100); ";
                listSQL.Add(updateSql);
                #endregion   
                #region B_Lab_Doctor表修改DoctorID字段为非自增，修改DoctorID字段类型为bigint，修改LabDoctorNo字段类型为varchar(100)

                updateSql = " IF COL_LENGTH('B_Lab_Doctor', 'DoctorID') IS not NULL ALTER TABLE B_Lab_Doctor ALTER COLUMN DoctorID bigint; ";
                listSQL.Add(updateSql);
                //修改字段类型后，需要进行大量的代码修改
                //updateSql = " IF COL_LENGTH('B_Lab_Doctor', 'LabDoctorNo') IS not NULL ALTER TABLE B_Lab_Doctor ALTER COLUMN LabDoctorNo varchar(100); ";
                //listSQL.Add(updateSql);
                #endregion   
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.17");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.17) Update Error, Please Check The Log!");
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
            DataSet ds = SqlServerHelper.QuerySql("select object_id(\'"+objectname+"\', \'"+objectType+"\') as ObjectID", ADOConnectStr);
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
                     "\'"+DateTime.Now.ToString()+"\'," +//DataAddTime
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
 