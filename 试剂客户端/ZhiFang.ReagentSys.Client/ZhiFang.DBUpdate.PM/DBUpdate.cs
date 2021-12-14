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

        static string MainAssemblyFile = "ZhiFang.ReagentSys.Client.dll";//可以从配置文件获取

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
            dicVersion.Add("1.0.0.49", "1.0.0.49");
            dicVersion.Add("1.0.0.50", "1.0.0.50");
            dicVersion.Add("1.0.0.51", "1.0.0.51");
            dicVersion.Add("1.0.0.52", "1.0.0.52");
            dicVersion.Add("1.0.0.53", "1.0.0.53");
            dicVersion.Add("1.0.0.54", "1.0.0.54");
            dicVersion.Add("1.0.0.55", "1.0.0.55");
            dicVersion.Add("1.0.0.56", "1.0.0.56");
            dicVersion.Add("1.0.0.57", "1.0.0.57");
            dicVersion.Add("1.0.0.58", "1.0.0.58");
            dicVersion.Add("1.0.0.59", "1.0.0.59");
            dicVersion.Add("1.0.0.60", "1.0.0.60");
            dicVersion.Add("1.0.0.61", "1.0.0.61");
            dicVersion.Add("1.0.0.62", "1.0.0.62");
            dicVersion.Add("1.0.0.63", "1.0.0.63");
            dicVersion.Add("1.0.0.64", "1.0.0.64");
            dicVersion.Add("1.0.0.65", "1.0.0.65");
            dicVersion.Add("1.0.0.66", "1.0.0.66");
            dicVersion.Add("1.0.0.67", "1.0.0.67");
            dicVersion.Add("1.0.0.68", "1.0.0.68");
            dicVersion.Add("1.0.0.69", "1.0.0.69");
            dicVersion.Add("1.0.0.70", "1.0.0.70");
            dicVersion.Add("1.0.0.71", "1.0.0.71");
            dicVersion.Add("1.0.0.72", "1.0.0.72");
            dicVersion.Add("1.0.0.73", "1.0.0.73");
            dicVersion.Add("1.0.0.74", "1.0.0.74");
            dicVersion.Add("1.0.0.75", "1.0.0.75");
            dicVersion.Add("1.0.0.76", "1.0.0.76");
            dicVersion.Add("1.0.0.77", "1.0.0.77");
            dicVersion.Add("1.0.0.78", "1.0.0.78");
            dicVersion.Add("1.0.0.79", "1.0.0.79");
            dicVersion.Add("1.0.0.80", "1.0.0.80");
            dicVersion.Add("1.0.0.81", "1.0.0.81");
            dicVersion.Add("1.0.0.82", "1.0.0.82");
            dicVersion.Add("1.0.0.83", "1.0.0.83");
            dicVersion.Add("1.0.0.84", "1.0.0.84");
            dicVersion.Add("1.0.0.85", "1.0.0.85");
            dicVersion.Add("1.0.0.86", "1.0.0.86");
            dicVersion.Add("1.0.0.87", "1.0.0.87");
            dicVersion.Add("1.0.0.88", "1.0.0.88");
            dicVersion.Add("1.0.0.89", "1.0.0.89");
            dicVersion.Add("1.0.0.90", "1.0.0.90");
            dicVersion.Add("1.0.0.91", "1.0.0.91");
            dicVersion.Add("1.0.0.92", "1.0.0.92");
            dicVersion.Add("1.0.0.93", "1.0.0.93");
            dicVersion.Add("1.0.0.94", "1.0.0.94");
            dicVersion.Add("1.0.0.95", "1.0.0.95");
            dicVersion.Add("1.0.0.96", "1.0.0.96");
            dicVersion.Add("1.0.0.97", "1.0.0.97");
            dicVersion.Add("1.0.0.98", "1.0.0.98");
            dicVersion.Add("1.0.0.99", "1.0.0.99");
            dicVersion.Add("1.0.0.100", "1.0.0.100");

            dicVersion.Add("1.0.0.101", "1.0.0.101");
            dicVersion.Add("1.0.0.102", "1.0.0.102");
            dicVersion.Add("1.0.0.103", "1.0.0.103");
            dicVersion.Add("1.0.0.104", "1.0.0.104");
            dicVersion.Add("1.0.0.105", "1.0.0.105");
            dicVersion.Add("1.0.0.106", "1.0.0.106");
            dicVersion.Add("1.0.0.107", "1.0.0.107");
            dicVersion.Add("1.0.0.108", "1.0.0.108");
            dicVersion.Add("1.0.0.109", "1.0.0.109");
            dicVersion.Add("1.0.0.110", "1.0.0.110");
            dicVersion.Add("1.0.0.111", "1.0.0.111");
            dicVersion.Add("1.0.0.112", "1.0.0.112");
            dicVersion.Add("1.0.0.113", "1.0.0.113");
            dicVersion.Add("1.0.0.114", "1.0.0.114");
            dicVersion.Add("1.0.0.115", "1.0.0.115");
            dicVersion.Add("1.0.0.116", "1.0.0.116");
            dicVersion.Add("1.0.0.117", "1.0.0.117");
            dicVersion.Add("1.0.0.118", "1.0.0.118");
            dicVersion.Add("1.0.0.119", "1.0.0.119");
            dicVersion.Add("1.0.0.120", "1.0.0.120");
            dicVersion.Add("1.0.0.121", "1.0.0.121");
            dicVersion.Add("1.0.0.122", "1.0.0.122");
            dicVersion.Add("1.0.0.123", "1.0.0.123");
            dicVersion.Add("1.0.0.124", "1.0.0.124");
            dicVersion.Add("1.0.0.125", "1.0.0.125");
            dicVersion.Add("1.0.0.126", "1.0.0.126");
            dicVersion.Add("1.0.0.127", "1.0.0.127");
            dicVersion.Add("1.0.0.128", "1.0.0.128");
            dicVersion.Add("1.0.0.129", "1.0.0.129");
            dicVersion.Add("1.0.0.130", "1.0.0.130");
            dicVersion.Add("1.0.0.131", "1.0.0.131");
            dicVersion.Add("1.0.0.132", "1.0.0.132");
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

                #region 加时间戳
                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDoc', 'DataTimeStamp') IS NULL ALTER TABLE Rea_BmsCenOrderDoc ADD DataTimeStamp  timestamp ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDtl', 'DataTimeStamp') IS NULL ALTER TABLE Rea_BmsCenOrderDtl ADD DataTimeStamp  timestamp ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDoc', 'DataTimeStamp') IS NULL ALTER TABLE Rea_BmsCenSaleDoc ADD DataTimeStamp  timestamp ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'DataTimeStamp') IS NULL ALTER TABLE Rea_BmsCenSaleDtl ADD DataTimeStamp  timestamp ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDocConfirm', 'DataTimeStamp') IS NULL ALTER TABLE Rea_BmsCenSaleDocConfirm ADD DataTimeStamp  timestamp ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'DataTimeStamp') IS NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ADD DataTimeStamp  timestamp ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('CenOrg', 'DataTimeStamp') IS NULL ALTER TABLE CenOrg ADD DataTimeStamp  timestamp ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('CenOrgCondition', 'DataTimeStamp') IS NULL ALTER TABLE CenOrgCondition ADD DataTimeStamp  timestamp ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('CenOrgType', 'DataTimeStamp') IS NULL ALTER TABLE CenOrgType ADD DataTimeStamp  timestamp ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Goods', 'DataTimeStamp') IS NULL ALTER TABLE Goods ADD DataTimeStamp  timestamp ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('BmsCenOrderDoc', 'DataTimeStamp') IS NULL ALTER TABLE BmsCenOrderDoc ADD DataTimeStamp  timestamp ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('BmsCenOrderDtl', 'DataTimeStamp') IS NULL ALTER TABLE BmsCenOrderDtl ADD DataTimeStamp  timestamp ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('BmsCenSaleDoc', 'DataTimeStamp') IS NULL ALTER TABLE BmsCenSaleDoc ADD DataTimeStamp  timestamp ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('BmsCenSaleDtl', 'DataTimeStamp') IS NULL ALTER TABLE BmsCenSaleDtl ADD DataTimeStamp  timestamp ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('BmsCenSaleDocConfirm', 'DataTimeStamp') IS NULL ALTER TABLE BmsCenSaleDocConfirm ADD DataTimeStamp  timestamp ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('BmsCenSaleDtlConfirm', 'DataTimeStamp') IS NULL ALTER TABLE BmsCenSaleDtlConfirm ADD DataTimeStamp  timestamp ; ";
                listSQL.Add(updateSql);
                #endregion

                #region 客户端订单Rea_BmsCenOrderDoc
                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDoc', 'ReaCompID1') IS NOT NULL ALTER TABLE Rea_BmsCenOrderDoc DROP COLUMN ReaCompID1 ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDoc', 'ReaCompName') IS NOT NULL ALTER TABLE Rea_BmsCenOrderDoc DROP COLUMN ReaCompName ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDoc', 'ReaServerCompCode1') IS NOT NULL ALTER TABLE Rea_BmsCenOrderDoc DROP COLUMN ReaServerCompCode1 ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDoc', 'CenOrderDocID1') IS NOT NULL ALTER TABLE Rea_BmsCenOrderDoc DROP COLUMN CenOrderDocID1 ; ";
                listSQL.Add(updateSql);
                #endregion

                #region 客户端订单明细Rea_BmsCenOrderDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDtl', 'SumTotal') IS NULL ALTER TABLE Rea_BmsCenOrderDtl add SumTotal float ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDtl', 'DeleteFlag')  IS NULL ALTER TABLE Rea_BmsCenOrderDtl add DeleteFlag int ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDtl', 'Memo')  IS NULL ALTER TABLE Rea_BmsCenOrderDtl add Memo varchar(500) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDtl', 'LabMemo')  IS NULL ALTER TABLE Rea_BmsCenOrderDtl add LabMemo varchar(500) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDtl', 'CompMemo')  IS NULL ALTER TABLE Rea_BmsCenOrderDtl add CompMemo varchar(500) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDtl', 'OtherOrderDocNo')  IS NULL ALTER TABLE Rea_BmsCenOrderDtl add OtherOrderDocNo varchar(500) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region 客户端供单Rea_BmsCenSaleDoc
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDoc', 'SecAccepterTime') IS NULL ALTER TABLE Rea_BmsCenSaleDoc add SecAccepterTime datetime ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDoc', 'IsSplit')  IS NULL ALTER TABLE Rea_BmsCenSaleDoc add IsSplit bit ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDoc', 'TotalPrice')  IS NULL ALTER TABLE Rea_BmsCenSaleDoc add TotalPrice float ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDoc', 'CheckerID')  IS NULL ALTER TABLE Rea_BmsCenSaleDoc add CheckerID bigint ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDoc', 'DeleteFlag')  IS NULL ALTER TABLE Rea_BmsCenSaleDoc add DeleteFlag int ; ";
                listSQL.Add(updateSql);
                #endregion

                #region 客户端供单明细Rea_BmsCenSaleDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'DeleteFlag') IS NULL ALTER TABLE Rea_BmsCenSaleDtl add DeleteFlag int ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'Memo')  IS NULL ALTER TABLE Rea_BmsCenSaleDtl add Memo varchar(500) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'PSaleDtlID')  IS NULL ALTER TABLE Rea_BmsCenSaleDtl add PSaleDtlID bigint ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'RefuseCount')  IS NULL ALTER TABLE Rea_BmsCenSaleDtl add RefuseCount float ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'AcceptFlag')  IS NULL ALTER TABLE Rea_BmsCenSaleDtl add AcceptFlag int ; ";
                listSQL.Add(updateSql);
                #endregion

                #region 客户端验收单Rea_BmsCenSaleDocConfirm
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDocConfirm', 'SaleDocID') IS not NULL ALTER TABLE Rea_BmsCenSaleDocConfirm alter column SaleDocID bigint ; ";
                listSQL.Add(updateSql);
                #endregion

                #region 客户端验收单明细Rea_BmsCenSaleDtlConfirm
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'SaleDtlID') IS not NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm alter column SaleDtlID bigint ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'GoodsQty') IS not NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm alter column GoodsQty float ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'InCount') IS not NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm alter column InCount float ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'AcceptCount') IS not NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm alter column AcceptCount float ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'RefuseCount') IS not NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm alter column RefuseCount float ; ";
                listSQL.Add(updateSql);

                #endregion                

                #region 验收单、入库单操作记录表Rea_CheckIn_Operation
                updateSql = " IF COL_LENGTH('Rea_CheckIn_Operation', 'BusinessModuleCode') IS not NULL ALTER TABLE Rea_CheckIn_Operation alter column BusinessModuleCode varchar(50) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region 申请单明细Rea_BmsReqDtl

                updateSql = " IF EXISTS (SELECT * FROM information_schema.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'FK_Rea_BmsReqDtl_REFERENCE_OrderDtlID') DROP constraint FK_Rea_BmsReqDtl_REFERENCE_OrderDtlID ";
                listSQL.Add(updateSql);
                #endregion

                #region 平台供单明细BmsCenSaleDoc
                updateSql = " IF COL_LENGTH('BmsCenSaleDoc', 'ThirdOrderDocNo') IS NOT NULL ALTER TABLE BmsCenSaleDoc DROP COLUMN ThirdOrderDocNo ";
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
            if (IsUpdateDataBase(oldVersion, "1.0.0.2"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();
                #region Rea_BmsInDtl_入库明细表
                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'MixSerial') IS NOT NULL ALTER TABLE Rea_BmsInDtl  DROP COLUMN MixSerial ;";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_BmsInDtl', 'PackSerial') IS NOT NULL ALTER TABLE Rea_BmsInDtl DROP COLUMN PackSerial ;";
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
            if (IsUpdateDataBase(oldVersion, "1.0.0.3"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();
                #region Rea_BmsCenSaleDtl_供货明细表
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'ReaCompID') IS NULL ALTER TABLE Rea_BmsCenSaleDtl ADD ReaCompID  bigint ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'ReaCompanyName') IS NULL ALTER TABLE Rea_BmsCenSaleDtl ADD ReaCompanyName  varchar(200) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'ReaServerCompCode') IS NULL ALTER TABLE Rea_BmsCenSaleDtl ADD ReaServerCompCode  varchar(200) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDtlConfirm_供货验收明细单表
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'ReaCompID') IS NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ADD ReaCompID  bigint ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'ReaCompanyName') IS NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ADD ReaCompanyName  varchar(200) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'ReaServerCompCode') IS NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ADD ReaServerCompCode  varchar(200) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsInDtl_入库明细表
                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'GoodsNo') IS NULL ALTER TABLE Rea_BmsInDtl ADD GoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'OrderGoodsID') IS NULL ALTER TABLE Rea_BmsInDtl ADD OrderGoodsID  bigint ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'ReaServerCompCode') IS NULL ALTER TABLE Rea_BmsInDtl ADD ReaServerCompCode  varchar(200) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtl_库存表
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'GoodsNo') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD GoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'OrderGoodsID') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD OrderGoodsID  bigint ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'ReaServerCompCode') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD ReaServerCompCode  varchar(200) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtlOperation_库存表操作记录
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtlOperation', 'GoodsNo') IS NULL ALTER TABLE Rea_BmsQtyDtlOperation ADD GoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtlOperation', 'OrderGoodsID') IS NULL ALTER TABLE Rea_BmsQtyDtlOperation ADD OrderGoodsID  bigint ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtlOperation', 'ReaServerCompCode') IS NULL ALTER TABLE Rea_BmsQtyDtlOperation ADD ReaServerCompCode  varchar(200) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsOutDtl_出库明细表
                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'GoodsNo') IS NULL ALTER TABLE Rea_BmsOutDtl ADD GoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'OrderGoodsID') IS NULL ALTER TABLE Rea_BmsOutDtl ADD OrderGoodsID  bigint ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'ReaServerCompCode') IS NULL ALTER TABLE Rea_BmsOutDtl ADD ReaServerCompCode  varchar(200) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'CompanyName') IS NULL ALTER TABLE Rea_BmsOutDtl ADD CompanyName  varchar(200) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsTransferDtl_移库明细表
                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'GoodsNo') IS NULL ALTER TABLE Rea_BmsTransferDtl ADD GoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'OrderGoodsID') IS NULL ALTER TABLE Rea_BmsTransferDtl ADD OrderGoodsID  bigint ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'ReaServerCompCode') IS NULL ALTER TABLE Rea_BmsTransferDtl ADD ReaServerCompCode  varchar(200) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'SysLotSerial') IS NULL ALTER TABLE Rea_BmsTransferDtl ADD SysLotSerial  varchar(100) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'MixSerial') IS NOT NULL ALTER TABLE Rea_BmsTransferDtl  DROP COLUMN MixSerial ;";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_BmsTransferDtl', 'PackSerial') IS NOT NULL ALTER TABLE Rea_BmsTransferDtl DROP COLUMN PackSerial ;";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsReqDtl_申请明细表
                updateSql = " IF COL_LENGTH('Rea_BmsReqDtl', 'OrgID') IS not NULL alter table Rea_BmsReqDtl alter column OrgID bigint  null ; ";
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
                #region F_File_文档表
                updateSql = " CREATE TABLE [dbo].[F_File](	[LabID] [bigint] NOT NULL,	[FileID] [bigint] NOT NULL,	[Title] [varchar](100) NULL,	[No] [varchar](100) NULL,	[Content] [varchar](max) NULL,	[Type] [int] NULL,	[ContentType] [bigint] NULL,	[Status] [int] NULL,	[Keyword] [varchar](100) NULL,	[Summary] [varchar](100) NULL,	[Source] [varchar](100) NULL,	[VersionNo] [varchar](100) NULL,	[Pagination] [int] NULL,	[ReviseNo] [varchar](100) NULL,	[RevisorID] [bigint] NULL,	[ReviseReason] [varchar](500) NULL,	[ReviseContent] [varchar](max) NULL,	[ReviseTime] [datetime] NULL,	[BeginTime] [datetime] NULL,	[EndTime] [datetime] NULL,	[NextExecutorID] [bigint] NULL,	[Memo] [varchar](max) NULL,	[DispOrder] [int] NULL,	[IsUse] [int] NULL,	[CreatorID] [bigint] NULL,	[CreatorName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL,	[FTypeTreeId] [bigint] NULL,	[OriginalFileID] [dbo].[D_系统主键] NULL,	[CheckerId] [bigint] NULL,	[CheckerName] [varchar](50) NULL,	[ApprovalId] [bigint] NULL,	[ApprovalName] [varchar](50) NULL,	[PublisherId] [bigint] NULL,	[PublisherName] [varchar](50) NULL,	[DrafterId] [bigint] NULL,	[DrafterCName] [varchar](50) NULL,	[IsTop] [bit] NULL,	[IsDiscuss] [bit] NULL,	[Counts] [int] NULL,	[DrafterDateTime] [datetime] NULL,	[CheckerDateTime] [datetime] NULL,	[ApprovalDateTime] [datetime] NULL,	[PublisherDateTime] [datetime] NULL,	[IsSyncWeiXin] [bit] NULL,	[WeiXinContent] [varchar](max) NULL,	[ThumbnailsPath] [varchar](100) NULL,	[ThumbnailsMemo] [varchar](500) NULL,	[Media_id] [nvarchar](100) NULL,	[Thumb_media_id] [nvarchar](100) NULL,	[WeiXinTitle] [nvarchar](100) NULL,	[WeiXinAuthor] [nvarchar](100) NULL,	[WeiXinDigest] [nvarchar](100) NULL,	[WeiXinUrl] [nvarchar](200) NULL,	[WeiXinContent_source_url] [nvarchar](2100) NULL, CONSTRAINT [PK_F_FILE] PRIMARY KEY CLUSTERED (	[FileID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                #region F_File_Attachment_文档附件表
                updateSql = " CREATE TABLE [dbo].[F_File_Attachment](	[LabID] [dbo].[D_实验室ID] NOT NULL,	[FileAttachmentID] [bigint] NOT NULL,	[FileID] [dbo].[D_系统主键] NULL,	[FileName] [varchar](500) NULL,	[FileExt] [varchar](100) NULL,	[FileSize] [bigint] NULL,	[FilePath] [varchar](500) NULL,	[Memo] [varchar](500) NULL,	[DispOrder] [varchar](500) NULL,	[IsUse] [bit] NULL,	[CreatorID] [bigint] NULL,	[CreatorName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_F_FILE_ATTACHMENT] PRIMARY KEY CLUSTERED (	[FileAttachmentID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]); ";
                listSQL.Add(updateSql);
                #endregion

                #region F_File_CopyUser_文档抄送对象表
                updateSql = " CREATE TABLE [dbo].[F_File_CopyUser](	[LabID] [dbo].[D_实验室ID] NOT NULL,	[FileCopyUserID] [bigint] NOT NULL,	[FileID] [dbo].[D_系统主键] NULL,	[Type] [bigint] NULL,	[DeptID] [bigint] NULL,	[RoleID] [bigint] NULL,	[UserID] [bigint] NULL,	[Memo] [varchar](500) NULL,	[DispOrder] [int] NULL,	[IsUse] [bit] NULL,	[CreatorID] [dbo].[D_系统主键] NULL,	[CreatorName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_F_FILE_COPYUSER] PRIMARY KEY CLUSTERED (	[FileCopyUserID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                #region F_File_Interaction_文档交流表
                updateSql = " CREATE TABLE [dbo].[F_File_Interaction](	[LabID] [dbo].[D_系统主键] NULL,	[InteractionID] [dbo].[D_系统主键] NOT NULL,	[FileID] [dbo].[D_系统主键] NULL,	[Contents] [varchar](max) NULL,	[SenderID] [dbo].[D_系统主键] NOT NULL,	[SenderName] [varchar](50) NULL,	[ReceiverID] [dbo].[D_系统主键] NULL,	[ReceiverName] [varchar](50) NULL,	[HasAttachment] [bit] NULL,	[Memo] [varchar](500) NULL,	[IsUse] [bit] NULL,	[DataAddTime] [datetime] NOT NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_F_File_Interaction] PRIMARY KEY CLUSTERED (	[InteractionID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                #region F_File_Operation_文档操作记录表
                updateSql = " CREATE TABLE [dbo].[F_File_Operation](	[LabID] [dbo].[D_实验室ID] NOT NULL,	[FileOperationID] [bigint] NOT NULL,	[FileID] [dbo].[D_系统主键] NOT NULL,	[Type] [bigint] NULL,	[Memo] [varchar](500) NULL,	[DispOrder] [int] NULL,	[IsUse] [bit] NULL,	[CreatorID] [dbo].[D_系统主键] NULL,	[CreatorName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_F_FILE_OPERATION] PRIMARY KEY CLUSTERED (	[FileOperationID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                #region F_File_ReadingLog_文档阅读记录表
                updateSql = " CREATE TABLE [dbo].[F_File_ReadingLog](	[LabID] [dbo].[D_实验室ID] NOT NULL,	[FileReadingLogID] [bigint] NOT NULL,	[FileID] [dbo].[D_系统主键] NOT NULL,	[ReaderID] [bigint] NULL,	[ReaderName] [varchar](50) NULL,	[ReadTimes] [int] NULL,	[Memo] [varchar](500) NULL,	[DispOrder] [int] NULL,	[IsUse] [bit] NULL,	[CreatorID] [dbo].[D_系统主键] NULL,	[CreatorName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_F_FILE_READINGLOG] PRIMARY KEY CLUSTERED (	[FileReadingLogID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                #region F_File_ReadingUser_文档阅读对象表
                updateSql = " CREATE TABLE [dbo].[F_File_ReadingUser](	[LabID] [dbo].[D_实验室ID] NOT NULL,	[FileReadingUserID] [bigint] NOT NULL,	[FileID] [dbo].[D_系统主键] NULL,	[Type] [bigint] NULL,	[DeptID] [bigint] NULL,	[RoleID] [bigint] NULL,	[UserID] [bigint] NULL,	[Memo] [varchar](500) NULL,	[DispOrder] [int] NULL,	[IsUse] [bit] NULL,	[CreatorID] [dbo].[D_系统主键] NULL,	[CreatorName] [varchar](50) NULL,	[DataAddTime] [datetime] NULL,	[DataUpdateTime] [datetime] NULL,	[DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_F_FILE_READINGUSER] PRIMARY KEY CLUSTERED (	[FileReadingUserID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                #region 索引外键
                updateSql = " ALTER TABLE [dbo].[F_File] ADD  DEFAULT ((0)) FOR [IsSyncWeiXin];ALTER TABLE [dbo].[F_File]  WITH CHECK ADD  CONSTRAINT [FK_F_File_F_TypeTree] FOREIGN KEY([FTypeTreeId])REFERENCES [dbo].[B_DictTree] ([TypeTreeID]);ALTER TABLE [dbo].[F_File] CHECK CONSTRAINT [FK_F_File_F_TypeTree];ALTER TABLE [dbo].[F_File]  WITH CHECK ADD  CONSTRAINT [FK_F_File_HR_Creator] FOREIGN KEY([CreatorID])REFERENCES [dbo].[HR_Employee] ([EmpID]);ALTER TABLE [dbo].[F_File] CHECK CONSTRAINT [FK_F_File_HR_Creator];ALTER TABLE [dbo].[F_File]  WITH CHECK ADD  CONSTRAINT [FK_F_File_HR_NextExecutor] FOREIGN KEY([NextExecutorID])REFERENCES [dbo].[HR_Employee] ([EmpID]);ALTER TABLE [dbo].[F_File] CHECK CONSTRAINT [FK_F_File_HR_NextExecutor];ALTER TABLE [dbo].[F_File]  WITH CHECK ADD  CONSTRAINT [FK_F_File_HR_Revisor] FOREIGN KEY([RevisorID])REFERENCES [dbo].[HR_Employee] ([EmpID]);ALTER TABLE [dbo].[F_File] CHECK CONSTRAINT [FK_F_File_HR_Revisor];ALTER TABLE [dbo].[F_File]  WITH CHECK ADD  CONSTRAINT [FK_F_File_B_Dict] FOREIGN KEY([ContentType])REFERENCES [dbo].[B_Dict] ([DID]);ALTER TABLE [dbo].[F_File] CHECK CONSTRAINT [FK_F_File_B_Dict];ALTER TABLE [dbo].[F_File_Attachment]  WITH CHECK ADD  CONSTRAINT [FK_F_File_Attachment_F_File] FOREIGN KEY([FileID])REFERENCES [dbo].[F_File] ([FileID]);ALTER TABLE [dbo].[F_File_Attachment] CHECK CONSTRAINT [FK_F_File_Attachment_F_File];ALTER TABLE [dbo].[F_File_Attachment]  WITH CHECK ADD  CONSTRAINT [FK_F_File_Attachment_HR_Employee] FOREIGN KEY([CreatorID])REFERENCES [dbo].[HR_Employee] ([EmpID]);ALTER TABLE [dbo].[F_File_Attachment] CHECK CONSTRAINT [FK_F_File_Attachment_HR_Employee];ALTER TABLE [dbo].[F_File_CopyUser]  WITH CHECK ADD  CONSTRAINT [FK_F_File_CopyUser_F_File] FOREIGN KEY([FileID])REFERENCES [dbo].[F_File] ([FileID]);ALTER TABLE [dbo].[F_File_CopyUser] CHECK CONSTRAINT [FK_F_File_CopyUser_F_File];ALTER TABLE [dbo].[F_File_CopyUser]  WITH CHECK ADD  CONSTRAINT [FK_F_File_CopyUser_HR_Creator] FOREIGN KEY([CreatorID])REFERENCES [dbo].[HR_Employee] ([EmpID]);ALTER TABLE [dbo].[F_File_CopyUser] CHECK CONSTRAINT [FK_F_File_CopyUser_HR_Creator];ALTER TABLE [dbo].[F_File_CopyUser]  WITH CHECK ADD  CONSTRAINT [FK_F_File_CopyUser_HR_Dept] FOREIGN KEY([DeptID])REFERENCES [dbo].[HR_Dept] ([DeptID]);ALTER TABLE [dbo].[F_File_CopyUser] CHECK CONSTRAINT [FK_F_File_CopyUser_HR_Dept];ALTER TABLE [dbo].[F_File_CopyUser]  WITH CHECK ADD  CONSTRAINT [FK_F_File_CopyUser_HR_User] FOREIGN KEY([UserID])REFERENCES [dbo].[HR_Employee] ([EmpID]);ALTER TABLE [dbo].[F_File_CopyUser] CHECK CONSTRAINT [FK_F_File_CopyUser_HR_User];ALTER TABLE [dbo].[F_File_CopyUser]  WITH CHECK ADD  CONSTRAINT [FK_F_File_CopyUser_RBAC_Role] FOREIGN KEY([RoleID])REFERENCES [dbo].[RBAC_Role] ([RoleID]);ALTER TABLE [dbo].[F_File_CopyUser] CHECK CONSTRAINT [FK_F_File_CopyUser_RBAC_Role];ALTER TABLE [dbo].[F_File_Interaction]  WITH CHECK ADD  CONSTRAINT [FK_F_File_Interaction_F_File] FOREIGN KEY([FileID])REFERENCES [dbo].[F_File] ([FileID]);ALTER TABLE [dbo].[F_File_Interaction] CHECK CONSTRAINT [FK_F_File_Interaction_F_File];ALTER TABLE [dbo].[F_File_Interaction]  WITH CHECK ADD  CONSTRAINT [FK_F_File_Interaction_HR_Receiver] FOREIGN KEY([ReceiverID])REFERENCES [dbo].[HR_Employee] ([EmpID]);ALTER TABLE [dbo].[F_File_Interaction] CHECK CONSTRAINT [FK_F_File_Interaction_HR_Receiver];ALTER TABLE [dbo].[F_File_Interaction]  WITH CHECK ADD  CONSTRAINT [FK_F_File_Interaction_HR_Sender] FOREIGN KEY([SenderID])REFERENCES [dbo].[HR_Employee] ([EmpID]);ALTER TABLE [dbo].[F_File_Interaction] CHECK CONSTRAINT [FK_F_File_Interaction_HR_Sender];ALTER TABLE [dbo].[F_File_Operation]  WITH CHECK ADD  CONSTRAINT [FK_F_File_Operation_F_File] FOREIGN KEY([FileID])REFERENCES [dbo].[F_File] ([FileID]);ALTER TABLE [dbo].[F_File_Operation] CHECK CONSTRAINT [FK_F_File_Operation_F_File];ALTER TABLE [dbo].[F_File_Operation]  WITH CHECK ADD  CONSTRAINT [FK_F_File_Operation_HR_Employee] FOREIGN KEY([CreatorID])REFERENCES [dbo].[HR_Employee] ([EmpID]);ALTER TABLE [dbo].[F_File_Operation] CHECK CONSTRAINT [FK_F_File_Operation_HR_Employee];ALTER TABLE [dbo].[F_File_ReadingLog]  WITH CHECK ADD  CONSTRAINT [FK_F_File_ReadingLog_F_File] FOREIGN KEY([FileID])REFERENCES [dbo].[F_File] ([FileID]);ALTER TABLE [dbo].[F_File_ReadingLog] CHECK CONSTRAINT [FK_F_File_ReadingLog_F_File];ALTER TABLE [dbo].[F_File_ReadingLog]  WITH CHECK ADD  CONSTRAINT [FK_F_File_ReadingLog_HR_Creator] FOREIGN KEY([CreatorID])REFERENCES [dbo].[HR_Employee] ([EmpID]);ALTER TABLE [dbo].[F_File_ReadingLog] CHECK CONSTRAINT [FK_F_File_ReadingLog_HR_Creator];ALTER TABLE [dbo].[F_File_ReadingLog]  WITH CHECK ADD  CONSTRAINT [FK_F_File_ReadingLog_HR_Employee] FOREIGN KEY([ReaderID])REFERENCES [dbo].[HR_Employee] ([EmpID]);ALTER TABLE [dbo].[F_File_ReadingLog] CHECK CONSTRAINT [FK_F_File_ReadingLog_HR_Employee];ALTER TABLE [dbo].[F_File_ReadingUser]  WITH CHECK ADD  CONSTRAINT [FK_F_File_ReadingUser_F_File] FOREIGN KEY([FileID])REFERENCES [dbo].[F_File] ([FileID]);ALTER TABLE [dbo].[F_File_ReadingUser] CHECK CONSTRAINT [FK_F_File_ReadingUser_F_File];ALTER TABLE [dbo].[F_File_ReadingUser]  WITH CHECK ADD  CONSTRAINT [FK_F_File_ReadingUser_HR_Creator] FOREIGN KEY([CreatorID])REFERENCES [dbo].[HR_Employee] ([EmpID]);ALTER TABLE [dbo].[F_File_ReadingUser] CHECK CONSTRAINT [FK_F_File_ReadingUser_HR_Creator];ALTER TABLE [dbo].[F_File_ReadingUser]  WITH CHECK ADD  CONSTRAINT [FK_F_File_ReadingUser_HR_Dept] FOREIGN KEY([DeptID])REFERENCES [dbo].[HR_Dept] ([DeptID]);ALTER TABLE [dbo].[F_File_ReadingUser] CHECK CONSTRAINT [FK_F_File_ReadingUser_HR_Dept];ALTER TABLE [dbo].[F_File_ReadingUser]  WITH CHECK ADD  CONSTRAINT [FK_F_File_ReadingUser_HR_User] FOREIGN KEY([UserID])REFERENCES [dbo].[HR_Employee] ([EmpID]);ALTER TABLE [dbo].[F_File_ReadingUser] CHECK CONSTRAINT [FK_F_File_ReadingUser_HR_User];ALTER TABLE [dbo].[F_File_ReadingUser]  WITH CHECK ADD  CONSTRAINT [FK_F_File_ReadingUser_RBAC_Role] FOREIGN KEY([RoleID])REFERENCES [dbo].[RBAC_Role] ([RoleID]);ALTER TABLE [dbo].[F_File_ReadingUser] CHECK CONSTRAINT [FK_F_File_ReadingUser_RBAC_Role]; ";
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
                #region 仪器表加时间戳
                updateSql = " IF COL_LENGTH('TestEquipLab', 'DataTimeStamp') IS NULL ALTER TABLE TestEquipLab add DataTimeStamp timestamp ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('TestEquipProd', 'DataTimeStamp') IS NULL ALTER TABLE TestEquipProd add DataTimeStamp timestamp ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('TestEquipType', 'DataTimeStamp') IS NULL ALTER TABLE TestEquipType add DataTimeStamp timestamp ; ";
                listSQL.Add(updateSql);
                #endregion

                #region 货品表加字段
                updateSql = " IF COL_LENGTH('Rea_Goods', 'StoreUpper') IS NULL ALTER TABLE Rea_Goods add StoreUpper float ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_Goods', 'StoreLower') IS NULL ALTER TABLE Rea_Goods add StoreLower float ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_Goods', 'BeforeWarningDay') IS NULL ALTER TABLE Rea_Goods add BeforeWarningDay float ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDocConfirm表
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDocConfirm', 'InvoiceNo') IS not NULL ALTER TABLE Rea_BmsCenSaleDocConfirm  alter column InvoiceNo varchar(100) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region 条码规则表
                updateSql = " IF COL_LENGTH('Rea_CenBarCodeFormat', 'Type') IS NULL ALTER TABLE Rea_CenBarCodeFormat   add Type bigint ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_CenBarCodeFormat', 'ComNoIndex') IS  NULL ALTER TABLE Rea_CenBarCodeFormat   add ComNoIndex bigint ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_CenBarCodeFormat', 'BatchIndex') IS  NULL ALTER TABLE Rea_CenBarCodeFormat   add BatchIndex bigint ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_CenBarCodeFormat', 'GoodNoIndex') IS  NULL ALTER TABLE Rea_CenBarCodeFormat   add GoodNoIndex bigint ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_CenBarCodeFormat', 'GoodInvalidDateIndex') IS  NULL ALTER TABLE Rea_CenBarCodeFormat   add GoodInvalidDateIndex bigint ; ";
                listSQL.Add(updateSql);
                #endregion

                #region 仪器试剂信息表
                updateSql = " create table Rea_EquipReagentLink (   LabID                bigint               not null,   EquipReagentLinkID   bigint               not null,   TestEquipID          bigint               null,   GoodsID              bigint               null,   Memo                 varchar(500)         collate Chinese_PRC_CI_AS null,   Visible              int                  null,   DispOrder            int                  null,   DataUpdateTime       datetime             not null,   DataAddTime          datetime             not null,   DataTimeStamp        timestamp            null,   constraint PK_REA_EQUIPREAGENTLINK primary key (EquipReagentLinkID),   constraint FK_REA_EQUI_REFERENCE_TESTEQUI foreign key (TestEquipID)      references dbo.TestEquipLab (TestEquipID),   constraint FK_REA_EQUI_REFERENCE_REA_GOOD foreign key (GoodsID)      references dbo.Rea_Goods (GoodsID)) ; ";
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
                #region TestEquipLab仪器表删除外键引用
                updateSql = " delete from TestEquipLab ; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select 1 from sysobjects where name= 'FK_TestEquipLab_LabID ' and xtype= 'F ') ALTER TABLE dbo.TestEquipLab DROP CONSTRAINT FK_TestEquipLab_LabID ; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select 1 from sysobjects where name= 'FK_TestEquipLab_CompOrgID ' and xtype= 'F ') ALTER TABLE dbo.TestEquipLab DROP CONSTRAINT FK_TestEquipLab_CompOrgID ; ";
                listSQL.Add(updateSql);

                updateSql = " alter table TestEquipLab add constraint FK_TestEquipLab_Rea_CenOrg_CompOrgID foreign key (CompOrgID) references Rea_CenOrg(OrgID) ; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select 1 from sysobjects where name= 'FK_TestEquipLab_ProdOrgID ' and xtype= 'F ') ALTER TABLE dbo.TestEquipLab DROP CONSTRAINT FK_TestEquipLab_ProdOrgID ; ";
                listSQL.Add(updateSql);

                updateSql = " alter table TestEquipLab add constraint FK_TestEquipLab_Rea_CenOrg_ProdOrgID foreign key (ProdOrgID) references Rea_CenOrg(OrgID) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('TestEquipLab', 'DataUpdateTime') IS not NULL ALTER TABLE TestEquipLab alter column DataUpdateTime datetime ;   ";
                listSQL.Add(updateSql);
                #endregion

                #region TestEquipProd               

                updateSql = " IF COL_LENGTH('TestEquipProd', 'LabID') IS NULL ALTER TABLE TestEquipProd add LabID bigint ;   ";
                listSQL.Add(updateSql);

                #endregion

                #region TestEquipType               

                updateSql = " IF COL_LENGTH('TestEquipType', 'LabID') IS NULL ALTER TABLE TestEquipType add LabID bigint ;   ";
                listSQL.Add(updateSql);

                #endregion

                #region CenOrg               

                updateSql = " IF COL_LENGTH('CenOrg', 'LabID') IS NULL ALTER TABLE CenOrg add LabID bigint ;   ";
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
                #region Rea_EquipReagentLink
                updateSql = " IF COL_LENGTH('Rea_EquipReagentLink', 'DataUpdateTime') IS not NULL ALTER TABLE Rea_EquipReagentLink alter column DataUpdateTime datetime ;   ";
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
                #region 供货明细、验货明细、入库明细、库存明细、出库明细、移库明细加条码类型字段0无条码1盒条码2批条码
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'BarCodeType') IS NULL ALTER TABLE Rea_BmsCenSaleDtl ADD BarCodeType  bigint ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'BarCodeType') IS NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ADD BarCodeType  bigint ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'BarCodeType') IS NULL ALTER TABLE Rea_BmsInDtl ADD BarCodeType  bigint ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'BarCodeType') IS NULL ALTER TABLE Rea_BmsOutDtl ADD BarCodeType  bigint ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'BarCodeType') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD BarCodeType  bigint ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtlOperation', 'BarCodeType') IS NULL ALTER TABLE Rea_BmsQtyDtlOperation ADD BarCodeType  bigint ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'BarCodeType') IS NULL ALTER TABLE Rea_BmsTransferDtl ADD BarCodeType  bigint ;  ";
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

                #region 验货明细、入库明细、库存明细、库存表操作记录、出库明细、移库明细加批号、效期、生产日期
                //验货明细
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'LotNo') IS NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ADD LotNo  varchar(100) ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'ProdDate') IS NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ADD ProdDate  datetime ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'InvalidDate') IS NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ADD InvalidDate  datetime ;  ";
                listSQL.Add(updateSql);

                //入库明细
                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'LotNo') IS NULL ALTER TABLE Rea_BmsInDtl ADD LotNo  varchar(100) ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'ProdDate') IS NULL ALTER TABLE Rea_BmsInDtl ADD ProdDate  datetime ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'InvalidDate') IS NULL ALTER TABLE Rea_BmsInDtl ADD InvalidDate  datetime ;  ";
                listSQL.Add(updateSql);

                //库存明细
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'LotNo') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD LotNo  varchar(100) ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'ProdDate') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD ProdDate  datetime ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'InvalidDate') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD InvalidDate  datetime ;  ";
                listSQL.Add(updateSql);

                //库存表操作记录
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtlOperation', 'LotNo') IS NULL ALTER TABLE Rea_BmsQtyDtlOperation ADD LotNo  varchar(100) ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtlOperation', 'ProdDate') IS NULL ALTER TABLE Rea_BmsQtyDtlOperation ADD ProdDate  datetime ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtlOperation', 'InvalidDate') IS NULL ALTER TABLE Rea_BmsQtyDtlOperation ADD InvalidDate  datetime ;  ";
                listSQL.Add(updateSql);

                //出库明细
                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'LotNo') IS NULL ALTER TABLE Rea_BmsOutDtl ADD LotNo  varchar(100) ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'ProdDate') IS NULL ALTER TABLE Rea_BmsOutDtl ADD ProdDate  datetime ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'InvalidDate') IS NULL ALTER TABLE Rea_BmsOutDtl ADD InvalidDate  datetime ;  ";
                listSQL.Add(updateSql);

                //移库明细
                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'LotNo') IS NULL ALTER TABLE Rea_BmsTransferDtl ADD LotNo  varchar(100) ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'ProdDate') IS NULL ALTER TABLE Rea_BmsTransferDtl ADD ProdDate  datetime ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'InvalidDate') IS NULL ALTER TABLE Rea_BmsTransferDtl ADD InvalidDate  datetime ;  ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_CenBarCodeFormat_供应商条码格式表
                updateSql = " IF COL_LENGTH('Rea_CenBarCodeFormat', 'ComNoIndex') IS not NULL ALTER TABLE Rea_CenBarCodeFormat   DROP COLUMN ComNoIndex ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_CenBarCodeFormat', 'BatchIndex') IS not NULL ALTER TABLE Rea_CenBarCodeFormat   DROP COLUMN BatchIndex ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_CenBarCodeFormat', 'GoodNoIndex') IS not NULL ALTER TABLE Rea_CenBarCodeFormat   DROP COLUMN GoodNoIndex ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_CenBarCodeFormat', 'GoodInvalidDateIndex') IS not NULL ALTER TABLE Rea_CenBarCodeFormat   DROP COLUMN GoodInvalidDateIndex ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_CenBarCodeFormat', 'RegularExpression') IS not NULL ALTER TABLE Rea_CenBarCodeFormat   alter column RegularExpression varchar(1000) ; ";
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

                #region 出库单
                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'OutDocNo') IS NULL ALTER TABLE Rea_BmsOutDoc ADD OutDocNo  varchar(20) ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'TotalPrice') IS NULL ALTER TABLE Rea_BmsOutDoc ADD TotalPrice  float ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'TakerID') IS NULL ALTER TABLE Rea_BmsOutDoc ADD TakerID  bigint ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'TakerName') IS NULL ALTER TABLE Rea_BmsOutDoc ADD TakerName  varchar(20) ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'CheckID') IS NULL ALTER TABLE Rea_BmsOutDoc ADD CheckID  bigint ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'CheckName') IS NULL ALTER TABLE Rea_BmsOutDoc ADD CheckName  varchar(20) ;  ";
                listSQL.Add(updateSql);

                #endregion

                #region 出库明细
                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'TestEquipID') IS NULL ALTER TABLE Rea_BmsOutDtl ADD TestEquipID  bigint;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'TestEquipName') IS NULL ALTER TABLE Rea_BmsOutDtl ADD TestEquipName  varchar(200);  ";
                listSQL.Add(updateSql);
                #endregion

                #region 移库单
                updateSql = " IF COL_LENGTH('Rea_BmsTransferDoc', 'TransferDocNo') IS NULL ALTER TABLE Rea_BmsTransferDoc ADD TransferDocNo  varchar(20) ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDoc', 'TotalPrice') IS NULL ALTER TABLE Rea_BmsTransferDoc ADD TotalPrice  float ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDoc', 'TakerID') IS NULL ALTER TABLE Rea_BmsTransferDoc ADD TakerID  bigint ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDoc', 'TakerName') IS NULL ALTER TABLE Rea_BmsTransferDoc ADD TakerName  varchar(20) ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDoc', 'CheckID') IS NULL ALTER TABLE Rea_BmsTransferDoc ADD CheckID  bigint ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDoc', 'CheckName') IS NULL ALTER TABLE Rea_BmsTransferDoc ADD CheckName  varchar(20) ;  ";
                listSQL.Add(updateSql);
                #endregion

                #region 库存表
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'UnitMemo') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD UnitMemo varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'RegisterNo') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD RegisterNo  varchar(200); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'InDtlID') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD InDtlID  bigint ; ";
                listSQL.Add(updateSql);

                updateSql = " alter table Rea_BmsQtyDtl add constraint FK_Rea_BmsQtyDtl_Rea_BmsInDtl_InDtlID foreign key (InDtlID) references Rea_BmsInDtl(InDtlID) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'InvalidWarningDate') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD InvalidWarningDate  datetime ;  ";
                listSQL.Add(updateSql);
                #endregion

                #region 盘库总单
                updateSql = " create table Rea_BmsCheckDoc (   LabID D_系统主键               null,   CheckDocID D_系统主键               not null,   CheckDocNo varchar(20)          null,   CheckType            bigint               null,   CheckTypeName        varchar(50)          null,   ReaCompanyID         D_系统主键               null,   CompanyName          varchar(100)         null,   StorageID            D_系统主键               null,   PlaceID              D_系统主键               null,      StorageName          varchar(100)         null,   PlaceName            varchar(100)         null,   ReaServerCompCode    varchar(200)         null,   Status               int                  null,   StatusName           varchar(50)          null,   IsLock               int                  null,   IsException          int                  null,   IsHandleException    int                  null,   CheckerID            bigint               null,   CheckerName          varchar(50)          null,   CheckDateTime        datetime             null,   ExaminerID           bigint               null,   ExaminerName         varchar(50)          null,   ExaminerDateTime     datetime             null,   ExaminerMemo         varchar(Max)         null,   OperDate             datetime             null,   PrintTimes           int                  null,   ZX1                  varchar(50)          null,   ZX2                  varchar(50)          null,   ZX3                  varchar(50)          null,   DispOrder            int                  null,   Memo                 varchar(Max)         null,   Visible              bit                  null,   CreaterID            bigint               null,   CreaterName          varchar(50)          null,   DataAddTime          datetime             null,   DataUpdateTime       datetime             not null,   DataTimeStamp        timestamp            null,   constraint PK_REA_BMSCHECKDOC primary key(CheckDocID),   constraint FK_REA_BMSC_REFERENCE_Rea_CenOrg_ReaCompanyID foreign key(ReaCompanyID)      references dbo.Rea_CenOrg(OrgID),   constraint FK_REA_BMSC_REFERENCE_Rea_Place_PlaceID foreign key(PlaceID)      references Rea_Place(PlaceID),   constraint FK_REA_BMSC_REFERENCE_Rea_Storage_StorageID foreign key(StorageID)      references Rea_Storage(StorageID)) ;  ";
                listSQL.Add(updateSql);
                #endregion

                #region 盘库明细
                updateSql = " create table Rea_BmsCheckDtl (   LabID D_系统主键               null,   CheckDtlID D_系统主键               not null,   CheckDocID D_系统主键               null,   ReaCompanyID D_系统主键               null,   CompanyName varchar(100)         null,   StorageID            D_系统主键               null,   PlaceID              D_系统主键               null,   StorageName          varchar(100)         null,   PlaceName            varchar(100)         null,   ReaServerCompCode    varchar(200)         null,   GoodsID              D_系统主键              not null,   GoodsName            varchar(100)         null,   LotNo                varchar(100)         null,   GoodsUnitID          D_系统主键               null,   GoodsUnit            varchar(100)         null,   UnitMemo             varchar(200)         collate Chinese_PRC_CI_AS null,   GoodsQty             float                null,   CheckQty             float                null,   Price                float                null,   SumTotal             float                null,   IsException          int                  null,   IsHandleException    int                  null,   OperDate             datetime             null,   ZX1                  varchar(50)          null,   ZX2                  varchar(50)          null,   ZX3                  varchar(50)          null,   DispOrder            int                  null,   Memo                 varchar(Max)         null,   Visible              bit                  null,   CreaterID            bigint               null,   CreaterName          varchar(50)          null,   DataAddTime          datetime             null,   DataUpdateTime       datetime             not null,   DataTimeStamp        timestamp            null,   constraint PK_REA_BMSCHECKDTL primary key(CheckDtlID),   constraint FK_REA_BMSC_REFERENCE_REA_BMSC_CheckDocID foreign key(CheckDocID)      references Rea_BmsCheckDoc(CheckDocID),   constraint FK_REA_BMSC_REFERENCE_REA_CENO_ReaCompanyID foreign key(ReaCompanyID)      references dbo.Rea_CenOrg(OrgID),   constraint FK_REA_BMSC_REFERENCE_REA_STOR_StorageID foreign key(StorageID)      references Rea_Storage(StorageID),   constraint FK_REA_BMSC_REFERENCE_REA_PLAC_PlaceID foreign key(PlaceID)      references Rea_Place(PlaceID),   constraint FK_REA_BMSC_REFERENCE_REA_GOOD_GoodsID foreign key(GoodsID)      references dbo.Rea_Goods(GoodsID),   constraint FK_REA_BMSC_REFERENCE_REA_GOOD_GoodsUnitID foreign key(GoodsUnitID)      references Rea_GoodsUnit(GoodsUnitID)) ;  ";
                listSQL.Add(updateSql);
                #endregion

                #region 修改仪器表表名
                updateSql = "   EXEC   sp_rename   'TestEquipLab',   'Rea_TestEquipLab';  ";
                listSQL.Add(updateSql);
                updateSql = "   EXEC   sp_rename   'TestEquipProd',   'Rea_TestEquipProd' ;  ";
                listSQL.Add(updateSql);
                updateSql = "   EXEC   sp_rename   'TestEquipType',   'Rea_TestEquipType' ;  ";
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

                #region 盘库总单
                updateSql = " IF COL_LENGTH('Rea_BmsCheckDoc', 'CheckType') IS NOT NULL ALTER TABLE Rea_BmsCheckDoc DROP COLUMN CheckType ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCheckDoc', 'CheckTypeName') IS NOT NULL ALTER TABLE Rea_BmsCheckDoc DROP COLUMN CheckTypeName ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCheckDoc', 'IsCompFlag') IS NULL ALTER TABLE Rea_BmsCheckDoc ADD IsCompFlag bit ;  ";
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

                #region 盘库明细单
                updateSql = "  IF COL_LENGTH('Rea_BmsCheckDtl', 'SysLotSerial') IS NULL ALTER TABLE Rea_BmsCheckDtl ADD SysLotSerial  varchar(100) ; ";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_BmsCheckDtl', 'LotSerial') IS NULL ALTER TABLE Rea_BmsCheckDtl ADD LotSerial  varchar(100) ; ";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_BmsCheckDtl', 'GoodsNo') IS NULL ALTER TABLE Rea_BmsCheckDtl ADD GoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_BmsCheckDtl', 'OrderGoodsID') IS NULL ALTER TABLE Rea_BmsCheckDtl ADD OrderGoodsID  bigint ; ";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_BmsCheckDtl', 'ProdDate') IS NULL ALTER TABLE Rea_BmsCheckDtl ADD ProdDate  datetime ; ";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_BmsCheckDtl', 'InvalidDate') IS NULL ALTER TABLE Rea_BmsCheckDtl ADD InvalidDate  datetime ; ";
                listSQL.Add(updateSql);

                updateSql = " alter table Rea_BmsCheckDtl add constraint FK_Rea_BmsCheckDtl_Rea_GoodsOrgLink_OrderGoodsID foreign key (OrderGoodsID) references Rea_GoodsOrgLink(OrderGoodsID) ; ";
                listSQL.Add(updateSql);

                #endregion

                #region 盘库总单
                updateSql = "  IF COL_LENGTH('Rea_BmsCheckDoc', 'BmsCheckResult') IS NULL ALTER TABLE Rea_BmsCheckDoc ADD BmsCheckResult  bigint ; ";
                listSQL.Add(updateSql);
                #endregion

                #region 入库总单
                updateSql = "  IF COL_LENGTH('Rea_BmsInDoc', 'CheckDocID') IS NULL ALTER TABLE Rea_BmsInDoc ADD CheckDocID  bigint ; ";
                listSQL.Add(updateSql);

                updateSql = " alter table Rea_BmsInDoc add constraint FK_Rea_BmsInDoc_Rea_BmsCheckDoc_CheckDocID foreign key (CheckDocID) references Rea_BmsCheckDoc(CheckDocID) ; ";
                listSQL.Add(updateSql);

                #endregion

                #region 盘库总单
                updateSql = "  IF COL_LENGTH('Rea_BmsOutDoc', 'CheckDocID') IS NULL ALTER TABLE Rea_BmsOutDoc ADD CheckDocID  bigint ; ";
                listSQL.Add(updateSql);

                updateSql = " alter table Rea_BmsOutDoc add constraint FK_Rea_BmsOutDoc_Rea_BmsCheckDoc_CheckDocID foreign key (CheckDocID) references Rea_BmsCheckDoc(CheckDocID) ; ";
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

                #region 字典表
                updateSql = "  IF COL_LENGTH('B_Dict', 'Code') IS  NOT NULL ALTER TABLE B_Dict DROP COLUMN Code ; ";
                listSQL.Add(updateSql);
                updateSql = "  IF COL_LENGTH('B_DictType', 'Code') IS  NOT NULL ALTER TABLE B_DictType DROP COLUMN Code ; ";
                listSQL.Add(updateSql);
                updateSql = "  IF COL_LENGTH('B_DictType', 'SName') IS  NOT NULL ALTER TABLE B_DictType DROP COLUMN SName ; ";
                listSQL.Add(updateSql);
                updateSql = "  IF COL_LENGTH('B_DictType', 'Shortcode') IS  NOT NULL ALTER TABLE B_DictType DROP COLUMN Shortcode ; ";
                listSQL.Add(updateSql);
                updateSql = "  IF COL_LENGTH('B_DictType', 'PinYinZiTou') IS  NOT NULL ALTER TABLE B_DictType DROP COLUMN PinYinZiTou ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_Dict', 'Name') IS  NOT NULL  EXEC sp_rename   'B_Dict.Name' , 'CName' ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_Dict', 'Comment') IS  NOT NULL  EXEC sp_rename   'B_Dict.Comment' , 'Memo' ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_DictType', 'Name') IS  NOT NULL  EXEC sp_rename   'B_DictType.Name' , 'CName' ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_DictType', 'Comment') IS  NOT NULL  EXEC sp_rename   'B_DictType.Comment' , 'Memo' ";
                listSQL.Add(updateSql);
                #endregion

                #region 库存表
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtlOperation', 'UnitMemo') IS NULL ALTER TABLE Rea_BmsQtyDtlOperation ADD UnitMemo varchar(100) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region 订货明细表
                updateSql = "IF COL_LENGTH('Rea_BmsCenOrderDtl', 'GoodsQty') IS not NULL ALTER TABLE Rea_BmsCenOrderDtl alter column GoodsQty float ;";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_BmsCenOrderDtl', 'CurrentQty') IS not NULL ALTER TABLE Rea_BmsCenOrderDtl alter column CurrentQty float ;";
                listSQL.Add(updateSql);
                #endregion

                #region 本地货品表
                updateSql = "IF COL_LENGTH('Rea_Goods', 'OrgID') IS NOT NULL ALTER TABLE Rea_Goods DROP constraint FK_REA_GOOD_REFERENCE_OrgID ";
                listSQL.Add(updateSql);

                updateSql = "IF COL_LENGTH('Rea_Goods', 'OrgID') IS not NULL ALTER TABLE Rea_Goods DROP COLUMN OrgID;";
                listSQL.Add(updateSql);

                #endregion

                #region 库存结转主表
                updateSql = " create table Rea_BmsQtyBalanceDoc (   LabID D_系统主键               null,   QtyBalanceDocID D_系统主键               not null,   QtyBalanceDocNo varchar(20)          null,   OperID               D_系统主键               null,   OperName             varchar(20)          null,   OperDate             datetime             null,   PrintTimes           int                  null,   ZX1                  varchar(50)          null,   ZX2                  varchar(50)          null,   ZX3                  varchar(50)          null,   DispOrder            int                  null,   Memo                 varchar(Max)         null,   Visible              bit                  null,   CreaterID            bigint               null,   CreaterName          varchar(50)          null,   DataAddTime          datetime             null,   DataUpdateTime       datetime             not null,   DataTimeStamp        timestamp            null,   constraint PK_REA_BMSQTYBALANCEDOC primary key(QtyBalanceDocID)) ";
                listSQL.Add(updateSql);
                #endregion

                #region 库存结转明细表
                updateSql = " create table Rea_BmsQtyBalanceDtl (   LabID D_系统主键               null,   BmsQtyBalanceDtlID D_系统主键               not null,   QtyBalanceDocID D_系统主键               null,   QtyDtlID D_系统主键               not null,   PQtyDtlID D_系统主键               not null,   ReaCompanyID D_系统主键               null,   CompanyName varchar(100)         null,   GoodsID              D_系统主键               null,   OrgID                bigint               null,   GoodsName            varchar(100)         null,   LotNo                varchar(100)         null,   StorageID            D_系统主键               null,   PlaceID              D_系统主键               null,   InDtlID              D_系统主键               null,   StorageName          varchar(100)         null,   PlaceName            varchar(100)         null,   GoodsUnitID          D_系统主键               null,   GoodsUnit            varchar(100)         null,   UnitMemo             varchar(200)         null,   GoodsQty             float                null,   Price                float                null,   SumTotal             float                null,   TaxRate              float                null,   OutFlag              int                  null,   SumFlag              int                  null,   IOFlag               int                  null,   GoodsSerial          varchar(100)         null,   LotSerial            varchar(100)         null,   SysLotSerial         varchar(100)         null,   ZX1                  varchar(50)          null,   ZX2                  varchar(50)          null,   ZX3                  varchar(50)          null,   Memo                 varchar(Max)         null,   DispOrder            int                  null,   Visible              bit                  null,   CreaterID            bigint               null,   CreaterName          varchar(50)          null,   DataAddTime          datetime             null,   DataUpdateTime       datetime             not null,   DataTimeStamp        timestamp            null,   GoodsNo              varchar(100)         null,   OrderGoodsID         bigint               null,   ProdDate             datetime             null,   InvalidDate          datetime             null,   InvalidWarningDate   datetime             null,   ReaServerCompCode    varchar(200)         null,   RegisterNo           varchar(200)         null,   constraint PK_REA_BMSQTYBALANCEDTL primary key(BmsQtyBalanceDtlID),   constraint FK_Rea_BmsQtyBalanceDtl_REFERENCE_Rea_BmsQtyDtl_QtyBalanceDocID foreign key(QtyBalanceDocID)      references Rea_BmsQtyBalanceDoc(QtyBalanceDocID),   constraint FK_Rea_BmsQtyBalanceDtl_REFERENCE_Rea_Goods_GoodsID foreign key(GoodsID)      references dbo.Rea_Goods(GoodsID),   constraint FK_Rea_BmsQtyBalanceDtl_REFERENCE_REA_CENO foreign key(OrgID)      references dbo.Rea_CenOrg(OrgID),   constraint FK_Rea_BmsQtyBalanceDtl_REFERENCE_Rea_BmsQtyDtl_QtyDtlID foreign key(QtyDtlID)      references Rea_BmsQtyDtl(QtyDtlID),   constraint FK_Rea_BmsQtyBalanceDtl_REFERENCE_REA_PLAC foreign key(PlaceID)      references Rea_Place(PlaceID),   constraint FK_Rea_BmsQtyBalanceDtl_REFERENCE_Rea_Storage_StorageID foreign key(StorageID)      references Rea_Storage(StorageID),   constraint FK_Rea_BmsQtyBalanceDtl_REFERENCE_Rea_Goods_GoodsUnitID foreign key(GoodsUnitID)      references Rea_GoodsUnit(GoodsUnitID)) ";
                listSQL.Add(updateSql);
                #endregion

                #region 库存月结主表
                updateSql = " create table Rea_BmsQtyMonthBalanceDoc (   LabID D_系统主键               null,   QtyMonthBalanceDocID D_系统主键               not null,   StorageID D_系统主键               null,   PlaceID D_系统主键               null,   StorageName varchar(100)         null,   PlaceName            varchar(100)         null,   QtyMonthBalanceDocNo varchar(20)          null,   Round                varchar(50)          null,   StartDate            datetime             null,   EndDate              datetime             null,   TypeID               bigint               null,   TypeName             varchar(50)          null,   StatisticalTypeID    bigint               null,   StatisticalTypeName  varchar(50)          null,   OperID               D_系统主键               null,   OperName             varchar(20)          null,   OperDate             datetime             null,   PrintTimes           int                  null,   ZX1                  varchar(50)          null,   ZX2                  varchar(50)          null,   ZX3                  varchar(50)          null,   DispOrder            int                  null,   Memo                 varchar(Max)         null,   Visible              bit                  null,   CreaterID            bigint               null,   CreaterName          varchar(50)          null,   DataAddTime          datetime             null,   DataUpdateTime       datetime             not null,   DataTimeStamp        timestamp            null,   constraint PK_REA_BMSQTYMONTHBALANCEDOC primary key(QtyMonthBalanceDocID),   constraint FK_Rea_BmsQtyMonthBalanceDoc_REFERENCE_Rea_Storage_StorageID foreign key(StorageID)      references Rea_Storage(StorageID),   constraint FK_Rea_BmsQtyMonthBalanceDoc_REFERENCE_REA_PLAC foreign key(PlaceID)      references Rea_Place(PlaceID)) ";
                listSQL.Add(updateSql);
                #endregion

                #region 库存月结明细表
                updateSql = " create table Rea_BmsQtyMonthBalanceDtl (   LabID D_系统主键               null,   QtyMonthBalanceDtlID D_系统主键               not null,   QtyMonthBalanceDocID D_系统主键               null,   ReaCompanyID D_系统主键               null,   CompanyName varchar(100)         null,   OrgID                bigint               null,   ReaServerCompCode    varchar(200)         null,   GoodsNo              varchar(100)         null,   OrderGoodsID         bigint               null,   GoodsID              D_系统主键               null,   GoodsName            varchar(100)         null,   LotNo                varchar(100)         null,   ProdDate             datetime             null,   RegisterNo           varchar(200)         collate Chinese_PRC_CI_AS null,   InvalidDate          datetime             null,   InvalidWarningDate   datetime             null,   GoodsUnitID          D_系统主键               null,   GoodsUnit            varchar(100)         null,   UnitMemo             varchar(200)         collate Chinese_PRC_CI_AS null,   Price                float                null,   PreMonthQty          float                null,   PreMonthQtyPrice     float                null,   InQty                float                null,   InQtyPrice           float                null,   EquipQty             float                null,   EquipPrice           float                null,   ReturnQty            float                null,   ReturnPrice          float                null,   MonthQty             float                null,   MonthQtyPrice        float                null,   LossQty              float                null,   LossQtyPrice         float                null,   AdjustmentOutQty     float                null,   AdjustmentOutQtyPrice float                null,   ZX1                  varchar(50)          null,   ZX2                  varchar(50)          null,   ZX3                  varchar(50)          null,   Memo                 varchar(Max)         null,   DispOrder            int                  null,   Visible              bit                  null,   CreaterID            bigint               null,   CreaterName          varchar(50)          null,   DataAddTime          datetime             null,   DataUpdateTime       datetime             not null,   DataTimeStamp        timestamp            null,   constraint PK_REA_BMSQTYMONTHBALANCEDTL primary key(QtyMonthBalanceDtlID),   constraint FK_REA_BMSQ_REFERENCE_REA_BMSQ foreign key(QtyMonthBalanceDocID)      references Rea_BmsQtyMonthBalanceDoc(QtyMonthBalanceDocID),   constraint FK_REA_BMSQ_REFERENCE_Rea_Goods_GoodsID foreign key(GoodsID)      references dbo.Rea_Goods(GoodsID),   constraint FK_REA_BMSQ_REFERENCE_REA_CENO foreign key(OrgID)      references dbo.Rea_CenOrg(OrgID),   constraint FK_REA_BMSQ_REFERENCE_Rea_GoodsUnit_GoodsUnitID foreign key(GoodsUnitID)      references Rea_GoodsUnit(GoodsUnitID))";
                listSQL.Add(updateSql);
                #endregion

                #region 出库库房货架权限
                updateSql = "create table Rea_User_Storage_Link (   LabID D_系统主键               null,   UserStorageLinkID D_系统主键               not null,   出库人ID bigint               null,   出库人姓名 varchar(20)          null,   PlaceID              D_系统主键               null,   StorageID            D_系统主键               null,   StorageName          varchar(100)         null,   PlaceName            varchar(100)         null,   DispOrder            int                  null,   Memo                 varchar(Max)         null,   Visible              bit                  null,   CreaterID            bigint               null,   CreaterName          varchar(50)          null,   DataAddTime          datetime             null,   DataUpdateTime       datetime             not null,   DataTimeStamp        timestamp            null,   constraint PK_REA_USER_STORAGE_LINK primary key(UserStorageLinkID),   constraint FK_REA_USER_REFERENCE_REA_PLAC foreign key(PlaceID)      references Rea_Place(PlaceID),   constraint FK_REA_USER_REFERENCE_Rea_Storage_StorageID foreign key(StorageID)      references Rea_Storage(StorageID)) ";
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

                #region 出库库房货架权限                
                updateSql = " IF COL_LENGTH('Rea_User_Storage_Link', '出库人ID') IS  NOT NULL  EXEC sp_rename   'Rea_User_Storage_Link.出库人ID' , 'OperID' ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_User_Storage_Link', '出库人姓名') IS  NOT NULL  EXEC sp_rename   'Rea_User_Storage_Link.出库人姓名' , 'OperName' ";
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

                #region 字典类型表                
                updateSql = " IF COL_LENGTH('B_DictType', 'DictTypeCode') IS  NULL  ALTER TABLE B_DictType ADD DictTypeCode  varchar(100) ;  ";
                listSQL.Add(updateSql);
                #endregion

                #region 报表管理信息表                
                updateSql = " create table B_Report (   LabID bigint          not null,   ReportID bigint           not null,   CName varchar(40)           null,   TypeID               bigint               null,   TypeName             varchar(100)         null,   StatisticsBeginDateTime datetime             null,   StatisticsEndDateTime datetime             null,   BobjectID            bigint               null,   BusinessModuleCode   varchar(100)         null,   Status               bigint               null,   FilePath             varchar(200)         null,   FileExt              varchar(50)          null,   ContentType          varchar(100)         null,   FileSize             float                null,   CreatorID            bigint               null,   CreatorName          varchar(50)          null,   DataAddTime          datetime             null,   CheckerID            bigint               null,   CheckerName          varchar(50)          null,   PublisherID          bigint               null,   PublisherName        varchar(50)          null,   CheckerDateTime      DateTime             null,   PublisherDateTime    DateTime             null,   SName                varchar(40)           null,   Shortcode            varchar(20)             null,   PinYinZiTou          varchar(50)          null,   DispOrder            int                  null,   Comment              ntext                 null,   IsUse                bit                  null,   DataTimeStamp        timestamp            null,   constraint PK_B_REPORT primary key(ReportID))  ";
                listSQL.Add(updateSql);
                #endregion

                #region 模板信息表                
                updateSql = " create table B_Template (   LabID bigint          not null,   TemplateID bigint          not null,   CName varchar(40)          collate Chinese_PRC_CI_AS null,   TypeID               bigint               null,   TypeName             varchar(50)          null,   FilePath             varchar(200)         null,   FileExt              varchar(50)          null,   ContentType          varchar(100)         null,   FileSize             float                null,   SName                varchar(40)          collate Chinese_PRC_CI_AS null,   Shortcode            varchar(20)            collate Chinese_PRC_CI_AS null,   PinYinZiTou          varchar(50)         collate Chinese_PRC_CI_AS null,   DispOrder            int                  null,   Comment              ntext                collate Chinese_PRC_CI_AS null,   IsUse                bit                  null,   DataAddTime          datetime             null,   DataTimeStamp        timestamp            null,   constraint PK_B_TEMPLATE primary key(TemplateID))  ";
                listSQL.Add(updateSql);
                #endregion

                #region 货品条码操作列表              
                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'PUsePackSerial') IS  NULL  ALTER TABLE Rea_GoodsBarcodeOperation ADD PUsePackSerial  varchar(100) ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'BarcodeCreatType') IS  NULL  ALTER TABLE Rea_GoodsBarcodeOperation ADD BarcodeCreatType  bigint ;  ";
                listSQL.Add(updateSql);
                #endregion

                #region 库存结转明细表                
                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDtl', 'BarcodeType') IS  NULL  ALTER TABLE Rea_BmsQtyBalanceDtl ADD BarcodeType  bigint ;  ";
                listSQL.Add(updateSql);
                #endregion

                #region 库存月结主表                
                updateSql = " IF COL_LENGTH('Rea_BmsQtyMonthBalanceDoc', 'QtyBalanceDocID') IS  NULL  ALTER TABLE Rea_BmsQtyMonthBalanceDoc ADD QtyBalanceDocID  bigint ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyMonthBalanceDoc', 'QtyBalanceDocNo') IS  NULL  ALTER TABLE Rea_BmsQtyMonthBalanceDoc ADD QtyBalanceDocNo  varchar(100) ;  ";
                listSQL.Add(updateSql);

                updateSql = " alter table Rea_BmsQtyMonthBalanceDoc add constraint FK_Rea_BmsQtyMonthBalanceDoc_Rea_BmsQtyBalanceDoc_QtyBalanceDocID foreign key (QtyBalanceDocID) references Rea_BmsQtyBalanceDoc(QtyBalanceDocID) ; ";
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

                #region 货品条码操作列表                
                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'PrintCount') IS  NULL  ALTER TABLE Rea_GoodsBarcodeOperation ADD PrintCount  int ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'UnitMemo') IS  NULL  ALTER TABLE Rea_GoodsBarcodeOperation ADD UnitMemo  varchar(200) ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'GoodsQty') IS  NULL  ALTER TABLE Rea_GoodsBarcodeOperation ADD GoodsQty  float ;  ";
                listSQL.Add(updateSql);
                #endregion

                #region 模板信息表                
                updateSql = " IF COL_LENGTH('B_Template', 'IsDefault') IS  NULL  ALTER TABLE B_Template ADD IsDefault  bit ;  ";
                listSQL.Add(updateSql);
                #endregion

                #region 货品表                
                updateSql = " IF COL_LENGTH('Rea_Goods', 'ProdID') IS NOT NULL ALTER TABLE Rea_Goods DROP constraint FK_REA_GOOD_REFERENCE_ProdID ;  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_TestEquipLab', 'ProdOrgID') IS NOT NULL ALTER TABLE Rea_TestEquipLab DROP constraint FK_TestEquipLab_Rea_CenOrg_ProdOrgID ;  ";
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

                #region Rea_BmsQtyDtl添加字段
                updateSql = "  IF COL_LENGTH('Rea_BmsQtyDtl', 'PrintCount') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD PrintCount int ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDtl添加字段
                updateSql = "  IF COL_LENGTH('Rea_BmsCenSaleDtl', 'PrintCount') IS NULL ALTER TABLE Rea_BmsCenSaleDtl ADD PrintCount int ; ";
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

                #region Rea_BmsCenSaleDoc
                updateSql = "  IF COL_LENGTH('Rea_BmsCenSaleDoc', 'ReaServerLabcCode') IS NULL ALTER TABLE Rea_BmsCenSaleDoc ADD ReaServerLabcCode varchar(200) ; ";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_BmsCenSaleDoc', 'AccepterID') IS not NULL ALTER TABLE Rea_BmsCenSaleDoc DROP COLUMN AccepterID ;";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_BmsCenSaleDoc', 'AccepterName') IS not NULL ALTER TABLE Rea_BmsCenSaleDoc DROP COLUMN AccepterName ;";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_BmsCenSaleDoc', 'SecAccepterID') IS not NULL ALTER TABLE Rea_BmsCenSaleDoc DROP COLUMN SecAccepterID ;";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_BmsCenSaleDoc', 'SecAccepterName') IS not NULL ALTER TABLE Rea_BmsCenSaleDoc DROP COLUMN SecAccepterName ;";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_BmsCenSaleDoc', 'AccepterTime') IS not NULL ALTER TABLE Rea_BmsCenSaleDoc DROP COLUMN AccepterTime ;";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_BmsCenSaleDoc', 'AccepterMemo') IS not NULL ALTER TABLE Rea_BmsCenSaleDoc DROP COLUMN AccepterMemo ;";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_BmsCenSaleDoc', 'IsAccepterError') IS not NULL ALTER TABLE Rea_BmsCenSaleDoc DROP COLUMN IsAccepterError ;";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_BmsCenSaleDoc', 'QtyBalanceDocID') IS not NULL ALTER TABLE Rea_BmsCenSaleDoc DROP COLUMN QtyBalanceDocID ;";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDtl
                updateSql = "  IF COL_LENGTH('Rea_BmsCenSaleDtl', 'DtlCount') IS not NULL ALTER TABLE Rea_BmsCenSaleDtl DROP COLUMN DtlCount ;";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_BmsCenSaleDtl', 'AcceptCount') IS not NULL ALTER TABLE Rea_BmsCenSaleDtl DROP COLUMN AcceptCount ;";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_BmsCenSaleDtl', 'AccepterErrorMsg') IS not NULL ALTER TABLE Rea_BmsCenSaleDtl DROP COLUMN AccepterErrorMsg ;";
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

                #region Rea_BmsCenSaleDtl供货明细
                updateSql = "  IF COL_LENGTH('Rea_BmsCenSaleDtl', 'LabcGoodsLinkID') IS NULL ALTER TABLE Rea_BmsCenSaleDtl ADD LabcGoodsLinkID bigint ; ";
                listSQL.Add(updateSql);

                updateSql = "  ALTER TABLE [dbo].[Rea_BmsCenSaleDtl]  WITH CHECK ADD  CONSTRAINT [FK_Rea_BmsCenSaleDtl_Rea_GoodsOrgLink_LabcGoodsLinkID] FOREIGN KEY([LabcGoodsLinkID]) REFERENCES [dbo].[Rea_GoodsOrgLink] ([OrderGoodsID]); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'OrderGoodsID') IS  NOT NULL  EXEC sp_rename   'Rea_BmsCenSaleDtl.OrderGoodsID' , 'CompGoodsLinkID' ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDtlConfirm供货明细
                updateSql = "  IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'LabcGoodsLinkID') IS NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ADD LabcGoodsLinkID bigint ; ";
                listSQL.Add(updateSql);

                updateSql = "  ALTER TABLE [dbo].[Rea_BmsCenSaleDtlConfirm]  WITH CHECK ADD  CONSTRAINT [FK_Rea_BmsCenSaleDtlConfirm_Rea_GoodsOrgLink_LabcGoodsLinkID] FOREIGN KEY([LabcGoodsLinkID]) REFERENCES [dbo].[Rea_GoodsOrgLink] ([OrderGoodsID]); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'OrderGoodsID') IS  NOT NULL  EXEC sp_rename   'Rea_BmsCenSaleDtlConfirm.OrderGoodsID' , 'CompGoodsLinkID' ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenOrderDoc订货单表
                updateSql = "  IF COL_LENGTH('Rea_BmsCenOrderDoc', 'ReaServerLabcCode') IS NULL ALTER TABLE Rea_BmsCenOrderDoc ADD ReaServerLabcCode varchar(200) ; ";
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

                #region Rea_GoodsOrgLink货品机构关系表
                updateSql = "  IF COL_LENGTH('Rea_GoodsOrgLink', 'BarCodeType') IS NULL ALTER TABLE Rea_GoodsOrgLink ADD BarCodeType bigint ; ";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_GoodsOrgLink', 'IsPrintBarCode') IS NULL ALTER TABLE Rea_GoodsOrgLink ADD IsPrintBarCode bigint ; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsCenOrderDtl订货明细表
                updateSql = "  IF COL_LENGTH('Rea_BmsCenOrderDtl', 'LabcGoodsLinkID') IS NULL ALTER TABLE Rea_BmsCenOrderDtl ADD LabcGoodsLinkID bigint ; ";
                listSQL.Add(updateSql);

                updateSql = "  ALTER TABLE [dbo].[Rea_BmsCenOrderDtl]  WITH CHECK ADD  CONSTRAINT [FK_Rea_BmsCenOrderDtl_Rea_GoodsOrgLink_LabcGoodsLinkID] FOREIGN KEY([LabcGoodsLinkID]) REFERENCES [dbo].[Rea_GoodsOrgLink] ([OrderGoodsID]); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDtl', 'OrderGoodsID') IS  NOT NULL  EXEC sp_rename   'Rea_BmsCenOrderDtl.OrderGoodsID' , 'CompGoodsLinkID' ";
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
                #region Rea_Goods_本地货品表
                updateSql = "  IF COL_LENGTH('Rea_Goods', 'ReaGoodsNo') IS NULL ALTER TABLE Rea_Goods ADD ReaGoodsNo varchar(100) ; ";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_Goods', 'GoodsNo') IS NOT NULL ALTER TABLE Rea_Goods ALTER column GoodsNo varchar(100) ; ";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_Goods', 'ProdGoodsNo') IS NOT NULL ALTER TABLE Rea_Goods ALTER column ProdGoodsNo varchar(100) ; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_GoodsOrgLink货品机构关系表
                updateSql = "  IF COL_LENGTH('Rea_GoodsOrgLink', 'ProdGoodsNo') IS NULL ALTER TABLE Rea_GoodsOrgLink ADD ProdGoodsNo varchar(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenOrderDtl订货明细表
                updateSql = "  IF COL_LENGTH('Rea_BmsCenOrderDtl', 'BarCodeType') IS NULL ALTER TABLE Rea_BmsCenOrderDtl ADD BarCodeType bigint ; ";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_BmsCenOrderDtl', 'IsPrintBarCode') IS NULL ALTER TABLE Rea_BmsCenOrderDtl ADD IsPrintBarCode bigint ; ";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_BmsCenOrderDtl', 'ProdGoodsNo') IS NOT NULL ALTER TABLE Rea_BmsCenOrderDtl ALTER column ProdGoodsNo varchar(100) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsInDtl_入库明细表
                updateSql = "  IF COL_LENGTH('Rea_BmsInDtl', 'ProdGoodsNo') IS NULL ALTER TABLE Rea_BmsInDtl ADD ProdGoodsNo varchar(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtl_库存表
                updateSql = "  IF COL_LENGTH('Rea_BmsQtyDtl', 'ProdGoodsNo') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD ProdGoodsNo varchar(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtlOperation_库存表操作记录
                updateSql = "  IF COL_LENGTH('Rea_BmsQtyDtlOperation', 'ProdGoodsNo') IS NULL ALTER TABLE Rea_BmsQtyDtlOperation ADD ProdGoodsNo varchar(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyBalanceDtl_库存结转明细表
                updateSql = "  IF COL_LENGTH('Rea_BmsQtyBalanceDtl', 'ProdGoodsNo') IS NULL ALTER TABLE Rea_BmsQtyBalanceDtl ADD ProdGoodsNo varchar(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_GoodsBarcodeOperation_货品条码操作列表
                updateSql = "  IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'ProdGoodsNo') IS NULL ALTER TABLE Rea_GoodsBarcodeOperation ADD ProdGoodsNo varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'GoodsNo') IS NULL ALTER TABLE Rea_GoodsBarcodeOperation ADD GoodsNo varchar(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsOutDtl_出库明细表
                updateSql = "  IF COL_LENGTH('Rea_BmsOutDtl', 'ProdGoodsNo') IS NULL ALTER TABLE Rea_BmsOutDtl ADD ProdGoodsNo varchar(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsTransferDtl_移库明细表
                updateSql = "  IF COL_LENGTH('Rea_BmsTransferDtl', 'ProdGoodsNo') IS NULL ALTER TABLE Rea_BmsTransferDtl ADD ProdGoodsNo varchar(100); ";
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

                #region Rea_BmsCenOrderDoc_订货单表
                updateSql = "  IF COL_LENGTH('Rea_BmsCenOrderDoc', 'IsVerifyProdGoodsNo') IS NULL ALTER TABLE Rea_BmsCenOrderDoc ADD IsVerifyProdGoodsNo bit ; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsCenOrderDtl订货明细表
                updateSql = "  IF COL_LENGTH('Rea_BmsCenOrderDtl', 'ProdOrgNo') IS NULL ALTER TABLE Rea_BmsCenOrderDtl ADD ProdOrgNo varchar(100) ; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsCenSaleDtl_供货明细表
                updateSql = "  IF COL_LENGTH('Rea_BmsCenSaleDtl', 'IsPrintBarCode') IS NULL ALTER TABLE Rea_BmsCenSaleDtl ADD IsPrintBarCode bigint ; ";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_BmsCenSaleDtl', 'ProdOrgNo') IS NULL ALTER TABLE Rea_BmsCenSaleDtl ADD ProdOrgNo varchar(100) ; ";
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

                #region Rea_BmsReqDtl
                updateSql = " IF COL_LENGTH('Rea_BmsReqDtl', 'ReaGoodsNo') IS NULL ALTER TABLE Rea_BmsReqDtl ADD ReaGoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);
                updateSql = " IF COL_LENGTH('Rea_BmsReqDtl', 'CenOrgGoodsNo') IS NULL ALTER TABLE Rea_BmsReqDtl ADD CenOrgGoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenOrderDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDtl', 'ReaGoodsNo') IS NULL ALTER TABLE Rea_BmsCenOrderDtl ADD ReaGoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);
                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDtl', 'CenOrgGoodsNo') IS NULL ALTER TABLE Rea_BmsCenOrderDtl ADD CenOrgGoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'ReaGoodsNo') IS NULL ALTER TABLE Rea_BmsCenSaleDtl ADD ReaGoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'CenOrgGoodsNo') IS NULL ALTER TABLE Rea_BmsCenSaleDtl ADD CenOrgGoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDtlConfirm
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'ReaGoodsNo') IS NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ADD ReaGoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'CenOrgGoodsNo') IS NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ADD CenOrgGoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsInDtl
                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'ReaGoodsNo') IS NULL ALTER TABLE Rea_BmsInDtl ADD ReaGoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);
                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'CenOrgGoodsNo') IS NULL ALTER TABLE Rea_BmsInDtl ADD CenOrgGoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtl
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'ReaGoodsNo') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD ReaGoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'CenOrgGoodsNo') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD CenOrgGoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyBalanceDtl
                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDtl', 'ReaGoodsNo') IS NULL ALTER TABLE Rea_BmsQtyBalanceDtl ADD ReaGoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);
                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDtl', 'CenOrgGoodsNo') IS NULL ALTER TABLE Rea_BmsQtyBalanceDtl ADD CenOrgGoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtlOperation
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtlOperation', 'ReaGoodsNo') IS NULL ALTER TABLE Rea_BmsQtyDtlOperation ADD ReaGoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtlOperation', 'CenOrgGoodsNo') IS NULL ALTER TABLE Rea_BmsQtyDtlOperation ADD CenOrgGoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_GoodsBarcodeOperation
                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'ReaGoodsNo') IS NULL ALTER TABLE Rea_GoodsBarcodeOperation ADD ReaGoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);
                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'CenOrgGoodsNo') IS NULL ALTER TABLE Rea_GoodsBarcodeOperation ADD CenOrgGoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsOutDtl
                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'ReaGoodsNo') IS NULL ALTER TABLE Rea_BmsOutDtl ADD ReaGoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);
                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'CenOrgGoodsNo') IS NULL ALTER TABLE Rea_BmsOutDtl ADD CenOrgGoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsTransferDtl
                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'ReaGoodsNo') IS NULL ALTER TABLE Rea_BmsTransferDtl ADD ReaGoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);
                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'CenOrgGoodsNo') IS NULL ALTER TABLE Rea_BmsTransferDtl ADD CenOrgGoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCheckDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCheckDtl', 'ReaGoodsNo') IS NULL ALTER TABLE Rea_BmsCheckDtl ADD ReaGoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);
                updateSql = " IF COL_LENGTH('Rea_BmsCheckDtl', 'CenOrgGoodsNo') IS NULL ALTER TABLE Rea_BmsCheckDtl ADD CenOrgGoodsNo  varchar(100) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region SC_Attachment
                updateSql = " IF COL_LENGTH('SC_Attachment', 'DownloadCounts') IS NULL ALTER TABLE SC_Attachment ADD DownloadCounts  int ; ";
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
                #region Rea_BmsCenSaleDtl_供货明细表
                updateSql = "  IF COL_LENGTH('Rea_BmsCenSaleDtl', 'LotQRCode') IS NULL ALTER TABLE Rea_BmsCenSaleDtl ADD LotQRCode varchar(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDtlConfirm_供货验收明细单表
                updateSql = "  IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'LotQRCode') IS NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ADD LotQRCode varchar(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsInDtl_入库明细表
                updateSql = "  IF COL_LENGTH('Rea_BmsInDtl', 'LotQRCode') IS NULL ALTER TABLE Rea_BmsInDtl ADD LotQRCode varchar(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtl_库存表
                updateSql = "  IF COL_LENGTH('Rea_BmsQtyDtl', 'LotQRCode') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD LotQRCode varchar(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtlOperation_库存表操作记录
                updateSql = "  IF COL_LENGTH('Rea_BmsQtyDtlOperation', 'LotQRCode') IS NULL ALTER TABLE Rea_BmsQtyDtlOperation ADD LotQRCode varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_BmsQtyDtlOperation', 'UnitMemo') IS NULL ALTER TABLE Rea_BmsQtyDtlOperation ADD UnitMemo varchar(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyBalanceDtl_库存结转明细表
                updateSql = "  IF COL_LENGTH('Rea_BmsQtyBalanceDtl', 'LotQRCode') IS NULL ALTER TABLE Rea_BmsQtyBalanceDtl ADD LotQRCode varchar(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_GoodsBarcodeOperation_货品条码操作列表
                updateSql = "  IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'UsePackQRCode') IS NULL ALTER TABLE Rea_GoodsBarcodeOperation ADD UsePackQRCode varchar(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsOutDtl_出库明细表
                updateSql = "  IF COL_LENGTH('Rea_BmsOutDtl', 'LotQRCode') IS NULL ALTER TABLE Rea_BmsOutDtl ADD LotQRCode varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_BmsOutDtl', 'UnitMemo') IS NULL ALTER TABLE Rea_BmsOutDtl ADD UnitMemo varchar(100); ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsTransferDtl_移库明细表
                updateSql = "  IF COL_LENGTH('Rea_BmsTransferDtl', 'LotQRCode') IS NULL ALTER TABLE Rea_BmsTransferDtl ADD LotQRCode varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_BmsTransferDtl', 'UnitMemo') IS NULL ALTER TABLE Rea_BmsTransferDtl ADD UnitMemo varchar(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCheckDtl_盘库明细表
                updateSql = "  IF COL_LENGTH('Rea_BmsCheckDtl', 'LotQRCode') IS NULL ALTER TABLE Rea_BmsCheckDtl ADD LotQRCode varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = "  IF COL_LENGTH('Rea_BmsCheckDtl', 'ProdGoodsNo') IS NULL ALTER TABLE Rea_BmsCheckDtl ADD ProdGoodsNo varchar(100); ";
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

                #region Rea_BmsReqDtl
                updateSql = " IF COL_LENGTH('Rea_Goods', 'ReaCompanyName') IS NULL ALTER TABLE Rea_Goods ADD ReaCompanyName  varchar(200) ; ";
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

                #region S_ServiceClient
                updateSql = " if not exists (select * from S_ServiceClient where LabID=1) INSERT [S_ServiceClient] ([LabID],[Name],[DispOrder],[ClientType],[ClientStyleID],[IsUse],[DataAddTime]) VALUES ( 1,N'智方管理',0,0,0,1,N'2018-05-29 11:48:10'); ";
                listSQL.Add(updateSql);
                #endregion
                #region CenOrg
                updateSql = " if not exists (select * from CenOrg where OrgID=10000)INSERT [CenOrg] ([OrgID],[OrgNo],[OrgTypeID],[CName],[DispOrder],[Visible],[DataUpdateTime],[LabID]) VALUES ( 10000,10000,5095132324043854891,N'智方管理',0,1,N'2016-06-30 12:54:34',1); ";
                listSQL.Add(updateSql);
                #endregion
                #region CenOrg
                updateSql = " update CenOrg set Visible=0 where Visible=1 and OrgNo<=101815 and OrgNo!=10000; ";
                listSQL.Add(updateSql);
                #endregion
                #region B_DictType
                updateSql = " update B_DictType set LabID=1 where LabID=0; ";
                listSQL.Add(updateSql);
                #endregion
                #region B_Dict
                updateSql = " update B_Dict set LabID=1 where LabID=0; ";
                listSQL.Add(updateSql);
                #endregion
                #region B_Parameter
                updateSql = " update B_Parameter set LabID=1 where LabID=0; ";
                listSQL.Add(updateSql);
                #endregion
                #region HR_Dept
                updateSql = " update HR_Dept set LabID=1 where LabID=0; ";
                listSQL.Add(updateSql);
                #endregion
                #region HR_Employee
                updateSql = " update HR_Employee set LabID=1 where LabID=0; ";
                listSQL.Add(updateSql);
                #endregion
                #region RBAC_EmpRoles
                updateSql = " update RBAC_EmpRoles set LabID=1 where LabID=0; ";
                listSQL.Add(updateSql);
                #endregion
                #region RBAC_Module
                updateSql = " update RBAC_Module set LabID=1 where LabID=0; ";
                listSQL.Add(updateSql);
                #endregion
                #region RBAC_Role
                updateSql = " update RBAC_Role set LabID=1 where LabID=0; ";
                listSQL.Add(updateSql);
                #endregion
                #region RBAC_RoleModule
                updateSql = " update RBAC_RoleModule set LabID=1 where LabID=0; ";
                listSQL.Add(updateSql);
                #endregion
                #region RBAC_User
                updateSql = " update RBAC_User set LabID=1 where LabID=0; ";
                listSQL.Add(updateSql);
                #endregion
                #region Rea_CenBarCodeFormat
                updateSql = " update Rea_CenBarCodeFormat set LabID=1 where LabID=0; ";
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

                #region RBAC_Module
                updateSql = " if not exists (select * from RBAC_Module where ModuleID=4816322121827404629) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[Comment],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,4816322121827404629,5019301137460545284,0,0,0,3,N'configuration.PNG',N'#Shell.class.rea.client.initialization.license.License',N'机构初始化',N'JGCSH',N'JGCSH',N'独立部署的客户端的机构初始化功能',1,-1000,N'2018-06-27 09:45:33'); ";
                listSQL.Add(updateSql);
                #endregion

                #region HR_Employee
                updateSql = " if not exists (select * from HR_Employee where EmpID=5701874243082459994) INSERT [HR_Employee] ([LabID],[EmpID],[DeptID],[NameL],[NameF],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[IsEnabled],[DataAddTime]) VALUES ( 1,5701874243082459994,5345088590572965995,N'授权机构',N'初始化专员',N'授权机构初始化专员',N'SQJGCSHZY',N'SQJGCSHZY',1,0,1,N'2018-06-27 09:38:17'); ";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_User
                updateSql = " if not exists (select * from [RBAC_User] where UserID=4878104343920690772) INSERT [RBAC_User] ([LabID],[UserID],[EmpID],[Account],[PWD],[EnMPwd],[PwdExprd],[AccExprd],[AccLock],[AuUnlock],[AccBeginTime],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,4878104343920690772,5701874243082459994,N'jgcsh001',N'DDD7345E9A5AA7434AD646E6E70DD6CE',1,1,0,0,0,N'2018-06-27 00:00:00',1,0,N'2018-06-27 09:39:04'); ";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_Role
                updateSql = " if not exists (select * from RBAC_Role where RoleID=5402498837070767766) INSERT [RBAC_Role] ([LabID],[RoleID],[LevelNum],[TreeCatalog],[CName],[SName],[Shortcode],[PinYinZiTou],[Comment],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,5402498837070767766,0,0,N'授权机构初始化',N'授权',N'SQJGCSH',N'SQJGCSH',N'独立部署的客户端的授权机构初始化',1,-11110,N'2018-06-27 09:42:12'); ";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_EmpRoles
                updateSql = " if not exists (select * from [RBAC_EmpRoles] where EmpRoleID=5161864841358501653) INSERT [RBAC_EmpRoles] ([LabID],[EmpRoleID],[EmpID],[RoleID],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,5161864841358501653,5701874243082459994,5402498837070767766,1,0,N'2018-06-27 09:49:14'); ";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_RoleModule
                updateSql = " if not exists (select * from [RBAC_RoleModule] where [ModuleVisiteID]=4746285506580729719) INSERT [RBAC_RoleModule] ([LabID],[ModuleVisiteID],[ModuleID],[RoleID],[IsUse],[DispOrder],[IsOftenUse],[IsDefaultOpen],[DataAddTime]) VALUES ( 1,4746285506580729719,5019301137460545284,5402498837070767766,1,0,0,0,N'2018-06-27 09:46:12'); ";
                listSQL.Add(updateSql);

                updateSql = " if not exists (select * from [RBAC_RoleModule] where [ModuleVisiteID]=4981375382035287098) INSERT [RBAC_RoleModule] ([LabID],[ModuleVisiteID],[ModuleID],[RoleID],[IsUse],[DispOrder],[IsOftenUse],[IsDefaultOpen],[DataAddTime]) VALUES ( 1,4981375382035287098,4816322121827404629,5402498837070767766,1,0,0,0,N'2018-06-27 09:46:12'); ";
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

                #region SC_Attachment
                updateSql = " IF COL_LENGTH('SC_Attachment', 'Memo') IS NOT NULL ALTER TABLE SC_Attachment ALTER COLUMN Memo  varchar(MAX) ; ";
                listSQL.Add(updateSql);
                #endregion
                #region SC_Operation
                updateSql = " IF COL_LENGTH('SC_Operation', 'Memo') IS NOT NULL ALTER TABLE SC_Operation ALTER COLUMN Memo  varchar(MAX) ; ";
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

                #region Rea_CenBarCodeFormat
                updateSql = " IF COL_LENGTH('Rea_CenBarCodeFormat', 'BarCodeType') IS NULL ALTER TABLE Rea_CenBarCodeFormat ADD BarCodeType  int ; ";
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

                #region Rea_BarCodeRules
                updateSql = " if exists(select 1 from sysobjects where id = object_id('Rea_BarCodeRules') and type = 'U') drop table Rea_BarCodeRules; create table Rea_BarCodeRules ( LabID bigint null, BmsType varchar(60) null, RulesId bigint not null, DataUpdateTime datetime null, DataAddTime datetime null, DataTimeStamp timestamp null, CurBarCode int null, constraint PK_REA_BARCODERULES primary key (RulesId)); ";
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

                #region Rea_BmsSerial
                updateSql = "if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Rea_BarCodeRules]') and OBJECTPROPERTY(id, N'IsUserTable') = 1) EXEC sp_rename 'Rea_BarCodeRules', 'Rea_BmsSerial';";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsSerial', 'CurBarCode') IS NOT NULL ALTER TABLE Rea_BmsSerial ALTER COLUMN CurBarCode  bigint; ";
                listSQL.Add(updateSql);

                updateSql = " if  exists (select * from dbo.sysobjects where id = object_id(N'P_GetReaBmsSerial') and OBJECTPROPERTY(id, N'IsProcedure') = 1) drop procedure P_GetReaBmsSerial;  ";
                listSQL.Add(updateSql);

                updateSql = "  CREATE PROCEDURE [dbo].[P_GetReaBmsSerial] @rulesId varchar(20), @labId varchar(20), @bmsType varchar(50) as begin declare @rulesId2 bigint; declare @labId2 bigint; set @rulesId2=CONVERT(bigint,@rulesId); set @labId2=CONVERT(bigint,@labId); if exists( select 1 from Rea_BmsSerial where BmsType= @bmsType and LabID=@labId2 and datediff(day,getdate(),DataUpdateTime) = 0) begin SET NOCOUNT ON; update Rea_BmsSerial set CurBarCode=ISNULL(CurBarCode, 0) + 1 where BmsType= @bmsType and LabID=@labId2 and datediff(day,getdate(),DataUpdateTime) = 0; SET NOCOUNT OFF; select * from Rea_BmsSerial where BmsType= @bmsType and LabID=@labId2 and datediff(day,getdate(),DataUpdateTime) = 0 ; end else if exists(select 1 from Rea_BmsSerial where BmsType= @bmsType and LabID=@labId2) begin SET NOCOUNT ON; update Rea_BmsSerial set CurBarCode=1, DataUpdateTime=GETDATE() where BmsType= @bmsType and LabID=@labId2; SET NOCOUNT OFF; select * from Rea_BmsSerial where BmsType= @bmsType and LabID=@labId2 end else begin if(@rulesId2<=0) set @rulesId2=(select case when max(RulesId)+1 is null then 10000 else max(RulesId)+1 end RulesId from Rea_BmsSerial); SET NOCOUNT ON; insert into Rea_BmsSerial(LabID, BmsType,RulesId,DataAddTime, DataUpdateTime, CurBarCode) values (@labId2, @bmsType,@rulesId2,GETDATE(), GETDATE(), 1); SET NOCOUNT OFF; select * from Rea_BmsSerial where BmsType= @bmsType and LabID=@labId2 end end ; ";
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

                #region Rea_BmsSerial
                //给智方管理机构添加一维条码序号信息
                updateSql = " if not exists(select 1 from Rea_BmsSerial where RulesId = 4873796527255564753 and LabID=1 and BmsType='ReaBmsQtyDtl') INSERT [Rea_BmsSerial] ([LabID],[BmsType],[RulesId],[DataUpdateTime],[DataAddTime],[CurBarCode]) VALUES ( 1,N'ReaBmsQtyDtl',4873796527255564753,N'2018-07-03 09:33:15',N'2018-07-03 09:33:15',0);";
                listSQL.Add(updateSql);

                updateSql = " if not exists(select 1 from Rea_BmsSerial where RulesId = 5560801650881654272 and LabID=1 and BmsType='ReaBmsCenSaleDtl') INSERT [Rea_BmsSerial] ([LabID],[BmsType],[RulesId],[DataUpdateTime],[DataAddTime],[CurBarCode]) VALUES ( 1,N'ReaBmsCenSaleDtl',5560801650881654272,N'2018-07-03 09:33:15',N'2018-07-03 09:33:15',0);";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenOrderDoc
                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDoc', 'ReaCompCode') IS NULL ALTER TABLE Rea_BmsCenOrderDoc ADD ReaCompCode  varchar(200) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDoc', 'ReaLabcCode') IS NULL ALTER TABLE Rea_BmsCenOrderDoc ADD ReaLabcCode  varchar(200) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDoc
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDoc', 'ReaCompCode') IS NULL ALTER TABLE Rea_BmsCenSaleDoc ADD ReaCompCode  varchar(200) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDoc', 'ReaLabcCode') IS NULL ALTER TABLE Rea_BmsCenSaleDoc ADD ReaLabcCode  varchar(200) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDocConfirm
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDocConfirm', 'ReaServerLabcCode') IS NULL ALTER TABLE Rea_BmsCenSaleDocConfirm ADD ReaServerLabcCode  varchar(200) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDocConfirm', 'ReaCompCode') IS NULL ALTER TABLE Rea_BmsCenSaleDocConfirm ADD ReaCompCode  varchar(200) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDocConfirm', 'ReaLabcCode') IS NULL ALTER TABLE Rea_BmsCenSaleDocConfirm ADD ReaLabcCode  varchar(200) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDtlConfirm
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'ReaCompCode') IS NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ADD ReaCompCode  varchar(200) ; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsInDtl
                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'ReaCompCode') IS NULL ALTER TABLE Rea_BmsInDtl ADD ReaCompCode  varchar(200) ; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsQtyDtl
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'ReaCompCode') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD ReaCompCode  varchar(200) ; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsQtyBalanceDtl
                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDtl', 'ReaCompCode') IS NULL ALTER TABLE Rea_BmsQtyBalanceDtl ADD ReaCompCode  varchar(200) ; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsQtyDtlOperation
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtlOperation', 'ReaCompCode') IS NULL ALTER TABLE Rea_BmsQtyDtlOperation ADD ReaCompCode  varchar(200) ; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_GoodsBarcodeOperation
                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'ReaCompCode') IS NULL ALTER TABLE Rea_GoodsBarcodeOperation ADD ReaCompCode  varchar(200) ; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsOutDtl
                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'ReaCompCode') IS NULL ALTER TABLE Rea_BmsOutDtl ADD ReaCompCode  varchar(200) ; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsTransferDtl
                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'ReaCompCode') IS NULL ALTER TABLE Rea_BmsTransferDtl ADD ReaCompCode  varchar(200) ; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsCheckDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCheckDtl', 'ReaCompCode') IS NULL ALTER TABLE Rea_BmsCheckDtl ADD ReaCompCode  varchar(200) ; ";
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

                #region Rea_BmsCenOrderDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDtl', 'GoodsSort') IS NULL ALTER TABLE Rea_BmsCenOrderDtl ADD GoodsSort int; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'GoodsSort') IS NULL ALTER TABLE Rea_BmsCenSaleDtl ADD GoodsSort  int; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDtlConfirm
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'GoodsSort') IS NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ADD GoodsSort int;";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsInDtl
                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'GoodsSort') IS NULL ALTER TABLE Rea_BmsInDtl ADD GoodsSort int; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsQtyDtl
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'GoodsSort') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD GoodsSort int ; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsQtyBalanceDtl
                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDtl', 'GoodsSort') IS NULL ALTER TABLE Rea_BmsQtyBalanceDtl ADD GoodsSort int ; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsQtyDtlOperation
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtlOperation', 'GoodsSort') IS NULL ALTER TABLE Rea_BmsQtyDtlOperation ADD GoodsSort  int ; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_GoodsBarcodeOperation
                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'GoodsSort') IS NULL ALTER TABLE Rea_GoodsBarcodeOperation ADD GoodsSort  int ; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsOutDtl
                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'GoodsSort') IS NULL ALTER TABLE Rea_BmsOutDtl ADD GoodsSort  int; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsTransferDtl
                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'GoodsSort') IS NULL ALTER TABLE Rea_BmsTransferDtl ADD GoodsSort int; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsCheckDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCheckDtl', 'GoodsSort') IS NULL ALTER TABLE Rea_BmsCheckDtl ADD GoodsSort int ; ";
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

                #region Rea_BmsCenSaleDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'ReaCompCode') IS NULL ALTER TABLE Rea_BmsCenSaleDtl ADD ReaCompCode  varchar(200) ; ";
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

                #region Rea_BmsCenOrderDoc
                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDoc', 'ReaLabcCode') IS NOT NULL ALTER TABLE Rea_BmsCenOrderDoc DROP COLUMN ReaLabcCode ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDocConfirm
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDocConfirm', 'ReaLabcCode') IS NOT NULL ALTER TABLE Rea_BmsCenSaleDocConfirm DROP COLUMN ReaLabcCode ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'ReaCompCode') IS NOT NULL ALTER TABLE Rea_BmsCenSaleDtl DROP COLUMN ReaCompCode ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDtlConfirm
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'ReaCompCode') IS NOT NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm DROP COLUMN ReaCompCode ; ";
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

                #region B_Dict
                updateSql = " delete  from B_Dict where  CName='' or LabID!=1  ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_Goods
                updateSql = " IF COL_LENGTH('Rea_Goods', 'MatchCode') IS NULL ALTER TABLE Rea_Goods ADD MatchCode  varchar(100) ; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BusinessInterface

                updateSql = "if exists(select 1 from sysobjects where id = object_id('Rea_BusinessInterface') and type = 'U') drop table Rea_BusinessInterface;";
                listSQL.Add(updateSql);

                updateSql = "  create table Rea_BusinessInterface( LabID D_实验室ID null, Id bigint not null, InterfaceType varchar(60) null, DispOrder int null, DataUpdateTime datetime null, DataAddTime datetime null, DataTimeStamp timestamp null, ZX1 varchar(100) collate Chinese_PRC_CI_AS null, ZX2 varchar(100) collate Chinese_PRC_CI_AS null, ZX3 varchar(100) collate Chinese_PRC_CI_AS null, Visible int null, BusinessType varchar(60) null, CName varchar(40) collate Chinese_PRC_CI_AS null, URL varchar(500) null, AppKey varchar(100) null, CompID bigint null, CompanyName varchar(200) collate Chinese_PRC_CI_AS null, AppPassword varchar(100) null, ReaServerCompCode varchar(200) null, ReaServerLabcCode varchar(200) null, ReaCompCode varchar(200) null, constraint PK_REA_BUSINESSINTERFACE primary key (Id)) if exists (select 1 from sys.extended_properties where major_id = object_id('Rea_BusinessInterface') and minor_id = 0) begin declare @CurrentUser sysname select @CurrentUser = user_name() execute sp_dropextendedproperty 'MS_Description', 'user', @CurrentUser, 'table', 'Rea_BusinessInterface' end ; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_MaterialReaGoodsMatch

                updateSql = "if exists(select 1 from sysobjects where id = object_id('Rea_MaterialReaGoodsMatch') and type = 'U') drop table Rea_MaterialReaGoodsMatch;";
                listSQL.Add(updateSql);

                updateSql = "  create table Rea_MaterialReaGoodsMatch( LabID D_实验室ID null, ID bigint not null, GoodsName varchar(200) collate Chinese_PRC_CI_AS not null, GoodsID bigint not null, DispOrder int null, Visible int null, DataUpdateTime datetime null, DataAddTime datetime null, MatchCode varchar(100) null, BusinessId bigint null, BusinessCName varchar(200) null, constraint PK_REA_MATERIALREAGOODSMATCH primary key (ID)) ; ";
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

                #region Rea_Storage
                updateSql = " IF COL_LENGTH('Rea_Storage', 'IsMainStorage') IS NULL ALTER TABLE Rea_Goods ADD IsMainStorage  bit; ";
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

                #region Rea_MaterialReaCenOrgMatch
                updateSql = "if exists(select 1 from sysobjects where id = object_id('Rea_MaterialReaCenOrgMatch') and type = 'U') drop table Rea_MaterialReaCenOrgMatch;";
                listSQL.Add(updateSql);

                updateSql = " create table Rea_MaterialReaCenOrgMatch( LabID D_实验室ID null, ID bigint not null, CompanyName varchar(200) collate Chinese_PRC_CI_AS null, CompID bigint not null, DispOrder int null, Visible int null, DataUpdateTime datetime null, DataAddTime datetime null, MatchCode varchar(100) null, BusinessId bigint not null, BusinessCName varchar(200) null, constraint PK_REA_MATERIALREACENORGMATCH primary key (ID)); ";
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

                #region Rea_CenBarCodeFormat
                updateSql = " update Rea_CenBarCodeFormat set BarCodeType=2 where BarCodeType is null; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_GoodsBarcodeOperation
                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'CompGoodsLinkID') IS NULL ALTER TABLE Rea_GoodsBarcodeOperation ADD CompGoodsLinkID  bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'BarCodeType') IS NULL ALTER TABLE Rea_GoodsBarcodeOperation ADD BarCodeType int; ";
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

                #region Rea_Storage
                updateSql = " IF COL_LENGTH('Rea_Storage', 'IsMainStorage') IS NULL ALTER TABLE Rea_Storage ADD IsMainStorage  bit; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_Goods
                updateSql = " IF COL_LENGTH('Rea_Goods', 'IsMainStorage') IS NOT NULL ALTER TABLE Rea_Goods drop column IsMainStorage; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.40");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.40) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.41
            if (IsUpdateDataBase(oldVersion, "1.0.0.41"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsCenOrderDoc
                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDoc', 'LabOrderDocID') IS NULL ALTER TABLE Rea_BmsCenOrderDoc ADD LabOrderDocID  bigint; ";
                listSQL.Add(updateSql);

                updateSql = " update dbo.Rea_BmsCenOrderDoc set LabOrderDocID=OrderDocID; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenOrderDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDtl', 'LabOrderDtlID') IS NULL ALTER TABLE Rea_BmsCenOrderDtl ADD LabOrderDtlID  bigint; ";
                listSQL.Add(updateSql);

                updateSql = " update dbo.Rea_BmsCenOrderDtl set LabOrderDtlID=OrderDtlID; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDoc
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDoc', 'LabOrderDocID') IS NULL ALTER TABLE Rea_BmsCenSaleDoc ADD LabOrderDocID  bigint; ";
                listSQL.Add(updateSql);

                updateSql = " update dbo.Rea_BmsCenSaleDoc set LabOrderDocID=OrderDocID; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_GoodsOrgLink
                updateSql = " IF COL_LENGTH('Rea_GoodsOrgLink', 'CompGoodsLinkID') IS NULL ALTER TABLE Rea_GoodsOrgLink ADD CompGoodsLinkID  bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsOrgLink', 'LabcGoodsLinkID') IS NULL ALTER TABLE Rea_GoodsOrgLink ADD LabcGoodsLinkID  bigint; ";
                listSQL.Add(updateSql);
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'PSaleDtlID') IS NOT NULL ALTER TABLE Rea_BmsCenSaleDtl drop column PSaleDtlID; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'RefuseCount') IS NOT NULL ALTER TABLE Rea_BmsCenSaleDtl drop column RefuseCount; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'AcceptFlag') IS NOT NULL ALTER TABLE Rea_BmsCenSaleDtl drop column AcceptFlag; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'LabOrderDtlID') IS NULL ALTER TABLE Rea_BmsCenSaleDtl ADD LabOrderDtlID  bigint; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_MaterialReaGoodsMatch
                updateSql = " IF COL_LENGTH('Rea_MaterialReaGoodsMatch', 'DataTimeStamp') IS NULL ALTER TABLE Rea_MaterialReaGoodsMatch ADD DataTimeStamp  timestamp NULL; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_MaterialReaCenOrgMatch
                updateSql = " IF COL_LENGTH('Rea_MaterialReaCenOrgMatch', 'DataTimeStamp') IS NULL ALTER TABLE Rea_MaterialReaCenOrgMatch ADD DataTimeStamp  timestamp NULL; ";
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

                #region Rea_BmsReqDtl
                updateSql = " IF COL_LENGTH('Rea_BmsReqDtl', 'OrderGoodsID') IS  NOT NULL  EXEC sp_rename   'Rea_BmsReqDtl.OrderGoodsID' , 'CompGoodsLinkID'; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsInDtl
                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'OrderGoodsID') IS  NOT NULL  EXEC sp_rename   'Rea_BmsInDtl.OrderGoodsID' , 'CompGoodsLinkID'; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsQtyDtl
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'OrderGoodsID') IS  NOT NULL  EXEC sp_rename   'Rea_BmsQtyDtl.OrderGoodsID' , 'CompGoodsLinkID'; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsCheckDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCheckDtl', 'OrderGoodsID') IS  NOT NULL  EXEC sp_rename   'Rea_BmsCheckDtl.OrderGoodsID' , 'CompGoodsLinkID'; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsQtyBalanceDtl
                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDtl', 'OrderGoodsID') IS  NOT NULL  EXEC sp_rename   'Rea_BmsQtyBalanceDtl.OrderGoodsID' , 'CompGoodsLinkID'; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsTransferDtl
                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'OrderGoodsID') IS  NOT NULL  EXEC sp_rename   'Rea_BmsTransferDtl.OrderGoodsID' , 'CompGoodsLinkID'; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsOutDtl
                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'OrderGoodsID') IS  NOT NULL  EXEC sp_rename   'Rea_BmsOutDtl.OrderGoodsID' , 'CompGoodsLinkID'; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsQtyDtlOperation
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtlOperation', 'OrderGoodsID') IS  NOT NULL  EXEC sp_rename   'Rea_BmsQtyDtlOperation.OrderGoodsID' , 'CompGoodsLinkID'; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_MaterialReaGoodsMatch
                updateSql = " IF COL_LENGTH('Rea_MaterialReaGoodsMatch', 'DataTimeStamp') IS NULL ALTER TABLE Rea_MaterialReaGoodsMatch ADD DataTimeStamp  timestamp NULL; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_MaterialReaCenOrgMatch
                updateSql = " IF COL_LENGTH('Rea_MaterialReaCenOrgMatch', 'DataTimeStamp') IS NULL ALTER TABLE Rea_MaterialReaCenOrgMatch ADD DataTimeStamp  timestamp NULL; ";
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

                #region B_Parameter
                //删除多出的"盘库货品扫码"运行参数记录
                updateSql = " DELETE FROM [dbo].[B_Parameter] WHERE ParameterID=5500686054195442631; ";
                listSQL.Add(updateSql);

                //删除原"业务接口URL配置"运行参数记录
                updateSql = " DELETE FROM [dbo].[B_Parameter] WHERE ParaType='CONFIG' and ParaNo='C-IURL-CONF-0020'; ";
                listSQL.Add(updateSql);

                //删除原"实验室升级版本"运行参数记录
                updateSql = " DELETE FROM [dbo].[B_Parameter] WHERE ParaType='CONFIG' and ParaNo='LAB_DBVersion'; ";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_Role
                //更新智方机构(labID=1)的"机构管理员"角色信息存在多个的bug(只保留一个)
                updateSql = " update RBAC_Role set DeveCode=null where LabID=1 and DeveCode='sys_admin' and (RoleID=4688251195251595898 or RoleID=5209462160979755357); ";
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

                #region Rea_MaterialReaGoodsMatch
                updateSql = " IF COL_LENGTH('Rea_MaterialReaGoodsMatch', 'DataTimeStamp') IS NULL ALTER TABLE Rea_MaterialReaGoodsMatch ADD DataTimeStamp  timestamp NULL; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_MaterialReaCenOrgMatch
                updateSql = " IF COL_LENGTH('Rea_MaterialReaCenOrgMatch', 'DataTimeStamp') IS NULL ALTER TABLE Rea_MaterialReaCenOrgMatch ADD DataTimeStamp  timestamp NULL; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsReqDtl
                updateSql = " IF COL_LENGTH('Rea_BmsReqDtl', 'ReqGoodsQty') IS NULL ALTER TABLE Rea_BmsReqDtl ADD ReqGoodsQty  float NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsReqDtl', 'ReqGoodsQty') IS NOT NULL update Rea_BmsReqDtl set ReqGoodsQty=GoodsQty; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsCenOrderDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDtl', 'ReqGoodsQty') IS NULL ALTER TABLE Rea_BmsCenOrderDtl ADD ReqGoodsQty  float NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDtl', 'ReqGoodsQty') IS NOT NULL update Rea_BmsCenOrderDtl set ReqGoodsQty=GoodsQty; ";
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

                #region Rea_BusinessInterface
                updateSql = " IF COL_LENGTH('Rea_BusinessInterface', 'BusinessType') IS NOT NULL ALTER TABLE Rea_BusinessInterface drop column BusinessType;";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BusinessInterface', 'CompID') IS NOT NULL ALTER TABLE Rea_BusinessInterface drop column CompID;";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BusinessInterface', 'CompanyName') IS NOT NULL ALTER TABLE Rea_BusinessInterface drop column CompanyName;";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BusinessInterface', 'ReaServerCompCode') IS NOT NULL ALTER TABLE Rea_BusinessInterface drop column ReaServerCompCode;";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BusinessInterface', 'ReaCompCode') IS NOT NULL ALTER TABLE Rea_BusinessInterface drop column ReaCompCode;";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BusinessInterfaceLink
                updateSql = "if exists(select 1 from sysobjects where id = object_id('Rea_BusinessInterfaceLink') and type = 'U') drop table Rea_BusinessInterfaceLink;";
                listSQL.Add(updateSql);

                updateSql = " create table Rea_BusinessInterfaceLink( LabID D_实验室ID null, BusinessId bigint not null, BusinessCName varchar(200) null, Id bigint not null, DispOrder int null, DataUpdateTime datetime null, DataAddTime datetime null, DataTimeStamp timestamp null, ZX1 varchar(100) collate Chinese_PRC_CI_AS null, ZX2 varchar(100) collate Chinese_PRC_CI_AS null, ZX3 varchar(100) collate Chinese_PRC_CI_AS null, Visible int null, BusinessTypeId bigint not null, BusinessType varchar(60) null, CompID bigint null, CompanyName varchar(200) collate Chinese_PRC_CI_AS null, ReaServerCompCode varchar(200) null, ReaCompCode varchar(200) null); ";
                listSQL.Add(updateSql);

                updateSql = "IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BusinessInterfaceLink_Rea_BusinessInterface]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BusinessInterfaceLink]')) ALTER TABLE [dbo].[Rea_BusinessInterfaceLink] DROP CONSTRAINT [FK_Rea_BusinessInterfaceLink_Rea_BusinessInterface]; ALTER TABLE [dbo].[Rea_BusinessInterfaceLink] WITH CHECK ADD CONSTRAINT [FK_Rea_BusinessInterfaceLink_Rea_BusinessInterface] FOREIGN KEY([BusinessId]) REFERENCES [dbo].[Rea_BusinessInterface] ([Id]); ALTER TABLE [dbo].[Rea_BusinessInterfaceLink] CHECK CONSTRAINT [FK_Rea_BusinessInterfaceLink_Rea_BusinessInterface];";
                listSQL.Add(updateSql);

                updateSql = "IF EXISTS(SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Rea_BusinessInterfaceLink]') AND name = N'PK_Rea_BusinessInterfaceLink') ALTER TABLE [dbo].[Rea_BusinessInterfaceLink] DROP CONSTRAINT [PK_Rea_BusinessInterfaceLink]; ALTER TABLE [dbo].[Rea_BusinessInterfaceLink] ADD CONSTRAINT [PK_Rea_BusinessInterfaceLink] PRIMARY KEY CLUSTERED ( [Id] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_MaterialReaCenOrgMatch
                updateSql = "IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_MaterialReaCenOrgMatch_Rea_BusinessInterface]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_MaterialReaCenOrgMatch]')) ALTER TABLE [dbo].[Rea_MaterialReaCenOrgMatch] DROP CONSTRAINT [FK_Rea_MaterialReaCenOrgMatch_Rea_BusinessInterface]; ALTER TABLE [dbo].[Rea_MaterialReaCenOrgMatch] WITH CHECK ADD CONSTRAINT [FK_Rea_MaterialReaCenOrgMatch_Rea_BusinessInterface] FOREIGN KEY([BusinessId]) REFERENCES [dbo].[Rea_BusinessInterface] ([Id]); ALTER TABLE [dbo].[Rea_MaterialReaCenOrgMatch] CHECK CONSTRAINT [FK_Rea_MaterialReaCenOrgMatch_Rea_BusinessInterface];";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_MaterialReaGoodsMatch
                updateSql = "IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_MaterialReaGoodsMatch_Rea_BusinessInterface]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_MaterialReaGoodsMatch]')) ALTER TABLE [dbo].[Rea_MaterialReaGoodsMatch] DROP CONSTRAINT [FK_Rea_MaterialReaGoodsMatch_Rea_BusinessInterface]; ALTER TABLE [dbo].[Rea_MaterialReaGoodsMatch] WITH CHECK ADD CONSTRAINT [FK_Rea_MaterialReaGoodsMatch_Rea_BusinessInterface] FOREIGN KEY([BusinessId]) REFERENCES [dbo].[Rea_BusinessInterface] ([Id]); ALTER TABLE [dbo].[Rea_MaterialReaGoodsMatch] CHECK CONSTRAINT [FK_Rea_MaterialReaGoodsMatch_Rea_BusinessInterface];";
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

                #region HR_Dept
                updateSql = " update HR_Dept set CName='智方管理',UseCode=10000  where LabID=1 and DeveCode='sys_admin' and DeptID=5394591405357219400;";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_RoleModule
                updateSql = " DELETE FROM RBAC_RoleModule where ModuleID is null or RoleID is null and LabID=1;";
                listSQL.Add(updateSql);

                //清空角色为"客户端功能-测试"的角色模块信息
                updateSql = "DELETE FROM RBAC_RoleModule where RoleID=4827452188294060315;";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_Role
                updateSql = " update RBAC_Role set IsUse=1,Comment='【智方管理】的机构管理员' where RoleID=5192782536902634177 and LabID=1;";
                listSQL.Add(updateSql);
                #endregion

                #region HR_Employee
                updateSql = " update HR_Employee set DeveCode='sys_admin' where LabID=1 and EmpID=5010877838461864487;";
                listSQL.Add(updateSql);

                updateSql = " update HR_Employee set DeveCode=NULL where LabID=1 and EmpID=5604788291655685563 and DeveCode='sys_admin';";
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

                #region Rea_BmsCenOrderDoc

                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDoc', 'DeptID') IS NULL ALTER TABLE Rea_BmsCenOrderDoc ADD DeptID  bigint NULL; ";
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

                #region Rea_BmsCenSaleDtl

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'LotSerial') IS NOT NULL ALTER TABLE Rea_BmsCenSaleDtl ALTER column LotSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'LotQRCode') IS NOT NULL ALTER TABLE Rea_BmsCenSaleDtl ALTER column LotQRCode  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'SysLotSerial') IS NOT NULL ALTER TABLE Rea_BmsCenSaleDtl ALTER column SysLotSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDtlConfirm

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'LotSerial') IS NOT NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ALTER column LotSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'LotQRCode') IS NOT NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ALTER column LotQRCode  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'SysLotSerial') IS NOT NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ALTER column SysLotSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDtlConfirm
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'LotSerial') IS NOT NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ALTER column LotSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'LotQRCode') IS NOT NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ALTER column LotQRCode  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'SysLotSerial') IS NOT NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ALTER column SysLotSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsInDtl
                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'LotSerial') IS NOT NULL ALTER TABLE Rea_BmsInDtl ALTER column LotSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'LotQRCode') IS NOT NULL ALTER TABLE Rea_BmsInDtl ALTER column LotQRCode  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'SysLotSerial') IS NOT NULL ALTER TABLE Rea_BmsInDtl ALTER column SysLotSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsInDtl
                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'LotSerial') IS NOT NULL ALTER TABLE Rea_BmsInDtl ALTER column LotSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'LotQRCode') IS NOT NULL ALTER TABLE Rea_BmsInDtl ALTER column LotQRCode  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'SysLotSerial') IS NOT NULL ALTER TABLE Rea_BmsInDtl ALTER column SysLotSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtl
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'LotSerial') IS NOT NULL ALTER TABLE Rea_BmsQtyDtl ALTER column LotSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'LotQRCode') IS NOT NULL ALTER TABLE Rea_BmsQtyDtl ALTER column LotQRCode  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'SysLotSerial') IS NOT NULL ALTER TABLE Rea_BmsQtyDtl ALTER column SysLotSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsOutDtl
                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'LotSerial') IS NOT NULL ALTER TABLE Rea_BmsOutDtl ALTER column LotSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'LotQRCode') IS NOT NULL ALTER TABLE Rea_BmsOutDtl ALTER column LotQRCode  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'SysLotSerial') IS NOT NULL ALTER TABLE Rea_BmsOutDtl ALTER column SysLotSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsOutDtl
                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'LotSerial') IS NOT NULL ALTER TABLE Rea_BmsOutDtl ALTER column LotSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'LotQRCode') IS NOT NULL ALTER TABLE Rea_BmsOutDtl ALTER column LotQRCode  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'SysLotSerial') IS NOT NULL ALTER TABLE Rea_BmsOutDtl ALTER column SysLotSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsTransferDtl
                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'LotSerial') IS NOT NULL ALTER TABLE Rea_BmsTransferDtl ALTER column LotSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'LotQRCode') IS NOT NULL ALTER TABLE Rea_BmsTransferDtl ALTER column LotQRCode  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'SysLotSerial') IS NOT NULL ALTER TABLE Rea_BmsTransferDtl ALTER column SysLotSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCheckDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCheckDtl', 'LotSerial') IS NOT NULL ALTER TABLE Rea_BmsCheckDtl ALTER column LotSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCheckDtl', 'LotQRCode') IS NOT NULL ALTER TABLE Rea_BmsCheckDtl ALTER column LotQRCode  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCheckDtl', 'SysLotSerial') IS NOT NULL ALTER TABLE Rea_BmsCheckDtl ALTER column SysLotSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtlOperation
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtlOperation', 'LotSerial') IS NOT NULL ALTER TABLE Rea_BmsQtyDtlOperation ALTER column LotSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtlOperation', 'LotQRCode') IS NOT NULL ALTER TABLE Rea_BmsQtyDtlOperation ALTER column LotQRCode  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtlOperation', 'SysLotSerial') IS NOT NULL ALTER TABLE Rea_BmsQtyDtlOperation ALTER column SysLotSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyBalanceDtl
                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDtl', 'LotSerial') IS NOT NULL ALTER TABLE Rea_BmsQtyBalanceDtl ALTER column LotSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDtl', 'LotQRCode') IS NOT NULL ALTER TABLE Rea_BmsQtyBalanceDtl ALTER column LotQRCode  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDtl', 'SysLotSerial') IS NOT NULL ALTER TABLE Rea_BmsQtyBalanceDtl ALTER column SysLotSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_GoodsBarcodeOperation
                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'SysPackSerial') IS NOT NULL ALTER TABLE Rea_GoodsBarcodeOperation ALTER column SysPackSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'OtherPackSerial') IS NOT NULL ALTER TABLE Rea_GoodsBarcodeOperation ALTER column OtherPackSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'UsePackQRCode') IS NOT NULL ALTER TABLE Rea_GoodsBarcodeOperation ALTER column UsePackQRCode  varchar(150) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'UsePackSerial') IS NOT NULL ALTER TABLE Rea_GoodsBarcodeOperation ALTER column UsePackSerial  varchar(150) NULL; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.48");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.48) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.49
            if (IsUpdateDataBase(oldVersion, "1.0.0.49"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region B_Dict

                updateSql = " IF COL_LENGTH('B_Dict', 'CName') IS NOT NULL ALTER TABLE B_Dict ALTER column CName  varchar(80) NULL; ";
                listSQL.Add(updateSql);
                updateSql = " IF COL_LENGTH('B_Dict', 'SName') IS NOT NULL ALTER TABLE B_Dict ALTER column SName  varchar(80) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_Dict', 'Shortcode') IS NOT NULL ALTER TABLE B_Dict ALTER column Shortcode  varchar(40) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_Dict', 'PinYinZiTou') IS NOT NULL ALTER TABLE B_Dict ALTER column PinYinZiTou  varchar(50) NULL; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.49");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.49) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.50
            if (IsUpdateDataBase(oldVersion, "1.0.0.50"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region HR_Employee
                //调整测试实验室(市一人民医院测试)的管理员信息
                updateSql = " update HR_Employee set DeveCode='sys_admin' where LabID=5705145215734838227 and EmpID=5570379842916118101; ";
                listSQL.Add(updateSql);
                #endregion

                #region HR_Employee
                ////调整测试实验室(市一人民医院测试)的员工角色信息
                //updateSql = " delete FROM RBAC_EmpRoles where LabID=5705145215734838227 and EmpID!=5570379842916118101 and DataAddTime>'2018-05-30'; ";
                //listSQL.Add(updateSql);
                ////调整测试实验室(市一人民医院测试)的员工角色信息
                //updateSql = " delete FROM HR_Employee where LabID=5705145215734838227 and EmpID!=5570379842916118101 and DataAddTime>'2018-05-30'; ";
                //listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsCenSaleDocConfirm
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDocConfirm', 'TotalPrice') IS NULL ALTER TABLE Rea_BmsCenSaleDocConfirm ADD TotalPrice  float; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.50");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.50) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.51
            if (IsUpdateDataBase(oldVersion, "1.0.0.51"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region B_Template
                //清空原报表模板表的测试数据
                updateSql = " DELETE FROM B_Template where LabID=5705145215734838227 or LabID=1; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_Template', 'FileName') IS NULL ALTER TABLE B_Template ADD FileName  varchar(60); ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.51");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.51) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.52
            if (IsUpdateDataBase(oldVersion, "1.0.0.52"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_TestItem
                updateSql = " if EXISTS(SELECT 1 FROM sysobjects WHERE id = object_id('Rea_TestItem') AND type = 'U') drop table Rea_TestItem create table Rea_TestItem ( LabID D_实验室ID null, TestItemID bigint NOT null, CName varchar(500) collate Chinese_PRC_CI_AS NOT null, EName varchar(100) collate Chinese_PRC_CI_AS null, Price float null, ShortCode varchar(50) collate Chinese_PRC_CI_AS null, DispOrder int null, Visible int null, LisCode varchar(100) collate Chinese_PRC_CI_AS null, ZX1 varchar(100) collate Chinese_PRC_CI_AS null, ZX2 varchar(100) collate Chinese_PRC_CI_AS null, ZX3 varchar(100) collate Chinese_PRC_CI_AS null, DataUpdateTime datetime null, Memo varchar(100) collate Chinese_PRC_CI_AS null, DataAddTime datetime null, DataTimeStamp timestamp null, constraint PK_REA_TESTITEM primary key (TestItemID)) ; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_TestEquipItem
                updateSql = " if EXISTS(SELECT 1 FROM sysobjects WHERE id = object_id('Rea_TestEquipItem') AND type = 'U') drop table Rea_TestEquipItem create table Rea_TestEquipItem ( LabID D_实验室ID null, TestEquipItemID bigint NOT null, TestEquipID bigint NOT null, TestItemID bigint NOT null, DispOrder int null, Visible int null, ZX1 varchar(100) collate Chinese_PRC_CI_AS null, ZX2 varchar(100) collate Chinese_PRC_CI_AS null, DataUpdateTime datetime null, DataAddTime datetime null, DataTimeStamp timestamp null, constraint PK_REA_TESTEQUIPITEM primary key (TestEquipItemID)); ";
                listSQL.Add(updateSql);

                //FK_Rea_TestEquipItem_Rea_TestEquipLab
                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_TestEquipItem_Rea_TestEquipLab]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_TestEquipItem]')) ALTER TABLE [dbo].[Rea_TestEquipItem] DROP CONSTRAINT [FK_Rea_TestEquipItem_Rea_TestEquipLab]; ALTER TABLE [dbo].[Rea_TestEquipItem] WITH CHECK ADD CONSTRAINT [FK_Rea_TestEquipItem_Rea_TestEquipLab] FOREIGN KEY([TestEquipID]) REFERENCES [dbo].[Rea_TestEquipLab] ([TestEquipID]); ALTER TABLE [dbo].[Rea_TestEquipItem] CHECK CONSTRAINT [FK_Rea_TestEquipItem_Rea_TestEquipLab]; ";
                listSQL.Add(updateSql);

                //FK_Rea_TestEquipItem_Rea_TestItem
                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_TestEquipItem_Rea_TestItem]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_TestEquipItem]')) ALTER TABLE [dbo].[Rea_TestEquipItem] DROP CONSTRAINT [FK_Rea_TestEquipItem_Rea_TestItem]; ALTER TABLE [dbo].[Rea_TestEquipItem] WITH CHECK ADD CONSTRAINT [FK_Rea_TestEquipItem_Rea_TestItem] FOREIGN KEY([TestItemID]) REFERENCES [dbo].[Rea_TestItem] ([TestItemID]); ALTER TABLE [dbo].[Rea_TestEquipItem] CHECK CONSTRAINT [FK_Rea_TestEquipItem_Rea_TestItem]; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_EquipTestItemReaGoodLink
                updateSql = " if EXISTS(SELECT 1 FROM sysobjects WHERE id = object_id('Rea_EquipTestItemReaGoodLink') AND type = 'U') drop table Rea_EquipTestItemReaGoodLink create table Rea_EquipTestItemReaGoodLink ( LabID D_实验室ID null, Id bigint NOT null, TestEquipItemID bigint NOT null, TestItemID bigint null, GoodsID bigint null, UnitName varchar(10) collate Chinese_PRC_CI_AS null, UnitMemo varchar(200) collate Chinese_PRC_CI_AS null, TestCount int null, DispOrder int null, Visible int null, ZX1 varchar(100) collate Chinese_PRC_CI_AS null, ZX2 varchar(100) collate Chinese_PRC_CI_AS null, ZX3 varchar(100) collate Chinese_PRC_CI_AS null, DataUpdateTime datetime null, DataAddTime datetime null, DataTimeStamp timestamp null, constraint PK_REA_EQUIPTESTITEMREAGOODLIN primary key (Id)); ";
                listSQL.Add(updateSql);

                //FK_Rea_EquipTestItemReaGoodLink_Rea_TestEquipItem
                updateSql = " IF EXISTS(SELECT  * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_EquipTestItemReaGoodLink_Rea_TestEquipItem]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_EquipTestItemReaGoodLink]')) ALTER TABLE [dbo].[Rea_EquipTestItemReaGoodLink] DROP CONSTRAINT [FK_Rea_EquipTestItemReaGoodLink_Rea_TestEquipItem]; ALTER TABLE [dbo].[Rea_EquipTestItemReaGoodLink] WITH CHECK ADD CONSTRAINT [FK_Rea_EquipTestItemReaGoodLink_Rea_TestEquipItem] FOREIGN KEY([TestEquipItemID]) REFERENCES [dbo].[Rea_TestEquipItem] ([TestEquipItemID]); ALTER TABLE [dbo].[Rea_EquipTestItemReaGoodLink] CHECK CONSTRAINT [FK_Rea_EquipTestItemReaGoodLink_Rea_TestEquipItem]; ";
                listSQL.Add(updateSql);

                //FK_Rea_EquipTestItemReaGoodLink_Rea_Goods
                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_EquipTestItemReaGoodLink_Rea_Goods]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_EquipTestItemReaGoodLink]')) ALTER TABLE [dbo].[Rea_EquipTestItemReaGoodLink] DROP CONSTRAINT [FK_Rea_EquipTestItemReaGoodLink_Rea_Goods]; ALTER TABLE [dbo].[Rea_EquipTestItemReaGoodLink] WITH CHECK ADD CONSTRAINT [FK_Rea_EquipTestItemReaGoodLink_Rea_Goods] FOREIGN KEY([GoodsID]) REFERENCES [dbo].[Rea_Goods] ([GoodsID]); ALTER TABLE [dbo].[Rea_EquipTestItemReaGoodLink] CHECK CONSTRAINT [FK_Rea_EquipTestItemReaGoodLink_Rea_Goods]; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.52");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.52) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.53
            if (IsUpdateDataBase(oldVersion, "1.0.0.53"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region B_Template

                updateSql = " IF COL_LENGTH('B_Template', 'CName') IS NOT NULL ALTER TABLE B_Template ALTER column CName  varchar(100) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_Template', 'FileName') IS NOT NULL ALTER TABLE B_Template ALTER column FileName  varchar(100) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_Template', 'SName') IS NOT NULL ALTER TABLE B_Template ALTER column SName  varchar(80) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_Template', 'Shortcode') IS NOT NULL ALTER TABLE B_Template ALTER column Shortcode  varchar(80) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_Template', 'FilePath') IS NOT NULL ALTER TABLE B_Template ALTER column FilePath  varchar(500) NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_Template', 'ExcelRuleInfo') IS NULL ALTER TABLE B_Template ADD ExcelRuleInfo  ntext; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.53");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.53) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.54
            if (IsUpdateDataBase(oldVersion, "1.0.0.54"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_Goods

                updateSql = " IF COL_LENGTH('Rea_Goods', 'DeptName') IS NULL ALTER TABLE Rea_Goods ADD DeptName  varchar(100); ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.54");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.54) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.55
            if (IsUpdateDataBase(oldVersion, "1.0.0.55"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsReqDoc
                updateSql = " IF COL_LENGTH('Rea_BmsReqDoc', 'LabcID') IS NULL ALTER TABLE Rea_BmsReqDoc ADD LabcID  bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsReqDoc', 'LabcName') IS NULL ALTER TABLE Rea_BmsReqDoc ADD LabcName  varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsReqDoc', 'ReaServerLabcCode') IS NULL ALTER TABLE Rea_BmsReqDoc ADD ReaServerLabcCode  varchar(100); ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsReqDtl
                updateSql = " IF COL_LENGTH('Rea_BmsReqDtl', 'ProdID') IS NULL ALTER TABLE Rea_BmsReqDtl ADD ProdID  bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsReqDtl', 'ProdOrgName') IS NULL ALTER TABLE Rea_BmsReqDtl ADD ProdOrgName  varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsReqDtl', 'UnitMemo') IS NULL ALTER TABLE Rea_BmsReqDtl ADD UnitMemo  varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsReqDtl', 'ArrivalTime') IS NULL ALTER TABLE Rea_BmsReqDtl ADD ArrivalTime  datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsReqDtl', 'ExpectedStock') IS NULL ALTER TABLE Rea_BmsReqDtl ADD ExpectedStock  float; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsReqDtl', 'CurrentQty') IS NULL ALTER TABLE Rea_BmsReqDtl ADD CurrentQty  float; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsReqDtl', 'MonthlyUsage') IS NULL ALTER TABLE Rea_BmsReqDtl ADD MonthlyUsage  float; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsCenOrderDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDtl', 'ArrivalTime') IS NULL ALTER TABLE Rea_BmsCenOrderDtl ADD ArrivalTime  datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDtl', 'ExpectedStock') IS NULL ALTER TABLE Rea_BmsCenOrderDtl ADD ExpectedStock  float; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDtl', 'MonthlyUsage') IS NULL ALTER TABLE Rea_BmsCenOrderDtl ADD MonthlyUsage  float; ";
                listSQL.Add(updateSql);

                #endregion

                #region RBAC_EmpRoles
                //"智方管理"机构的角色管理员
                updateSql = " if((select COUNT(*) from [RBAC_Role] where RoleID=5192782536902634177)=0) INSERT[RBAC_Role]([LabID],[RoleID],[LevelNum],[TreeCatalog],[DeveCode],[CName],[Comment],[IsUse],[DispOrder],[DataAddTime]) VALUES( 1,5192782536902634177,0,0, N'sys_admin', N'机构管理员', N'【智方管理】的机构管理员',1,1, N'2018-06-27 10:26:45'); ";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_EmpRoles
                //手工调整"智方管理"机构的角色管理员与管理员帐号绑定
                updateSql = " if((select COUNT(*) from RBAC_EmpRoles where EmpRoleID=5713427501648134534)=0) INSERT[RBAC_EmpRoles]([LabID],[EmpRoleID],[EmpID],[RoleID],[IsUse],[DispOrder],[DataAddTime]) VALUES( 1,5713427501648134534,5010877838461864487,5192782536902634177,1,0, N'2018-09-04 15:12:58'); ";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_Module
                updateSql = " update RBAC_Module set LabId=1 where LabId=0; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.55");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.55) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.56
            if (IsUpdateDataBase(oldVersion, "1.0.0.56"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region B_Template
                updateSql = " IF COL_LENGTH('B_Template', 'TemplateType') IS NULL ALTER TABLE B_Template ADD TemplateType  varchar(40); ";
                listSQL.Add(updateSql);

                updateSql = " UPDATE B_Template SET TemplateType='Excel模板' WHERE FileExt='.xlsx';";
                listSQL.Add(updateSql);
                updateSql = " UPDATE B_Template SET TemplateType='Frx模板' WHERE FileExt='.frx';";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_AlertInfoSettings
                updateSql = " if EXISTS(SELECT 1 FROM sysobjects WHERE id = object_id('Rea_AlertInfoSettings') AND type = 'U') drop table Rea_AlertInfoSettings; create table Rea_AlertInfoSettings ( LabID D_实验室ID null, AlertId bigint NOT null, AlertTypeId int null, AlertTypeCName varchar(60) null, AlertColor varchar(60) null, DispOrder int null, StoreUpper float null, StoreLower float null, Visible int null, DataUpdateTime datetime null, DataAddTime datetime null, Memo ntext collate Chinese_PRC_CI_AS null, DataTimeStamp timestamp null, constraint PK_REA_ALERTINFOSETTINGS primary key (AlertId)); ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_MonthUsageStatisticsDoc
                updateSql = " if EXISTS(SELECT 1 FROM sysobjects WHERE id = object_id('Rea_MonthUsageStatisticsDoc') AND type = 'U') drop table Rea_MonthUsageStatisticsDoc; create table Rea_MonthUsageStatisticsDoc ( LabID D_系统主键 null, DocID D_系统主键 NOT null, DocNo varchar(20) null, Round varchar(50) null, StartDate datetime null, EndDate datetime null, DeptID bigint null, DeptName varchar(20) null, TypeID bigint null, TypeName varchar(50) null, ZX1 varchar(50) null, ZX2 varchar(50) null, ZX3 varchar(50) null, DispOrder int null, Memo varchar(Max) null, Visible bit null, CreaterID bigint null, CreaterName varchar(50) null, DataAddTime datetime null, DataUpdateTime datetime NOT null, DataTimeStamp timestamp null, constraint PK_REA_MONTHUSAGESTATISTICSDOC primary key (DocID)); ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_MonthUsageStatisticsDtl
                updateSql = " if EXISTS(SELECT 1 FROM sysobjects WHERE id = object_id('Rea_MonthUsageStatisticsDtl') AND type = 'U') drop table Rea_MonthUsageStatisticsDtl; create table Rea_MonthUsageStatisticsDtl ( LabID D_系统主键 null, DocID D_系统主键 NOT null, DtlID bigint NOT null, GoodsID D_系统主键 null, GoodsName varchar(100) null, GoodsUnit varchar(100) null, ProdGoodsNo varchar(100) collate Chinese_PRC_CI_AS null, ReaGoodsNo varchar(100) null, CenOrgGoodsNo varchar(100) null, ReaCompCode varchar(200) null, UnitMemo varchar(200) collate Chinese_PRC_CI_AS null, OutQty float null, DispOrder int null, Visible bit null, DataAddTime datetime null, DataUpdateTime datetime NOT null, DataTimeStamp timestamp null, constraint PK_REA_MONTHUSAGESTATISTICSDTL primary key (DtlID)); ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_MonthUsageStatisticsDtl_Rea_MonthUsageStatisticsDoc]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_MonthUsageStatisticsDtl]')) ALTER TABLE [dbo].[Rea_MonthUsageStatisticsDtl] DROP CONSTRAINT [FK_Rea_MonthUsageStatisticsDtl_Rea_MonthUsageStatisticsDoc]; ALTER TABLE [dbo].[Rea_MonthUsageStatisticsDtl] WITH CHECK ADD CONSTRAINT [FK_Rea_MonthUsageStatisticsDtl_Rea_MonthUsageStatisticsDoc] FOREIGN KEY([DocID]) REFERENCES [dbo].[Rea_MonthUsageStatisticsDoc] ([DocID]); ALTER TABLE [dbo].[Rea_MonthUsageStatisticsDtl] CHECK CONSTRAINT [FK_Rea_MonthUsageStatisticsDtl_Rea_MonthUsageStatisticsDoc]; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.56");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.56) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.57
            if (IsUpdateDataBase(oldVersion, "1.0.0.57"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_Goods
                updateSql = " IF COL_LENGTH('Rea_Goods', 'MonthlyUsage') IS NULL ALTER TABLE Rea_Goods ADD MonthlyUsage  float; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenOrderDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDtl', 'LastMonthlyUsage') IS NULL ALTER TABLE Rea_BmsCenOrderDtl ADD LastMonthlyUsage  float; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsReqDtl
                updateSql = " IF COL_LENGTH('Rea_BmsReqDtl', 'LastMonthlyUsage') IS NULL ALTER TABLE Rea_BmsReqDtl ADD LastMonthlyUsage  float; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.57");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.57) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.58
            if (IsUpdateDataBase(oldVersion, "1.0.0.58"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_MonthUsageStatisticsDtl
                updateSql = " IF COL_LENGTH('Rea_MonthUsageStatisticsDtl', 'DeptID') IS NULL ALTER TABLE Rea_MonthUsageStatisticsDtl ADD DeptID  bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_MonthUsageStatisticsDtl', 'DeptName') IS NULL ALTER TABLE Rea_MonthUsageStatisticsDtl ADD DeptName  varchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_MonthUsageStatisticsDtl', 'GoodsID') IS NOT NULL ALTER TABLE Rea_MonthUsageStatisticsDtl DROP COLUMN GoodsID; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_MonthUsageStatisticsDtl', 'ReaCompCode') IS NOT NULL ALTER TABLE Rea_MonthUsageStatisticsDtl DROP COLUMN ReaCompCode; ";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_Role
                //更新测试实验室机构的所属LabId由1更改为:5705145215734838227
                updateSql = " update RBAC_Role set LabID=5705145215734838227,IsUse=1 where LabID=1 and RoleID=4845150561732500596; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.58");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.58) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.59
            if (IsUpdateDataBase(oldVersion, "1.0.0.59"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_TestItem
                updateSql = " IF COL_LENGTH('Rea_TestItem', 'SName') IS NULL ALTER TABLE Rea_TestItem ADD SName  varchar(60); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_EquipTestItemReaGoodLink
                updateSql = " IF COL_LENGTH('Rea_EquipTestItemReaGoodLink', 'UnitName') IS NOT NULL ALTER TABLE Rea_EquipTestItemReaGoodLink DROP COLUMN UnitName; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_EquipTestItemReaGoodLink', 'UnitMemo') IS NOT NULL ALTER TABLE Rea_EquipTestItemReaGoodLink DROP COLUMN UnitMemo; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_Goods
                updateSql = " UPDATE Rea_Goods set MonthlyUsage=0 where MonthlyUsage is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsReqDtl
                updateSql = " UPDATE Rea_BmsReqDtl set ExpectedStock=0 where ExpectedStock is null; ";
                listSQL.Add(updateSql);

                updateSql = " UPDATE Rea_BmsReqDtl set CurrentQty=0 where CurrentQty is null; ";
                listSQL.Add(updateSql);

                updateSql = " UPDATE Rea_BmsReqDtl set MonthlyUsage=0 where MonthlyUsage is null; ";
                listSQL.Add(updateSql);

                updateSql = " UPDATE Rea_BmsReqDtl set LastMonthlyUsage=0 where LastMonthlyUsage is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenOrderDtl
                updateSql = " UPDATE Rea_BmsCenOrderDtl set ExpectedStock=0 where ExpectedStock is null; ";
                listSQL.Add(updateSql);

                updateSql = " UPDATE Rea_BmsCenOrderDtl set CurrentQty=0 where CurrentQty is null; ";
                listSQL.Add(updateSql);

                updateSql = " UPDATE Rea_BmsCenOrderDtl set MonthlyUsage=0 where MonthlyUsage is null; ";
                listSQL.Add(updateSql);

                updateSql = " UPDATE Rea_BmsCenOrderDtl set LastMonthlyUsage=0 where LastMonthlyUsage is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_LisTestStatisticalResults
                updateSql = " if exists(select 1 from sysobjects where id = object_id('Rea_LisTestStatisticalResults') and type = 'U') drop table Rea_LisTestStatisticalResults; create table Rea_LisTestStatisticalResults ( LabID D_系统主键 null, Id bigint not null, TestDate datetime null, TestEquipID bigint null, TestEquipCode varchar(60) not null, TestEquipName varchar(200) not null, TestEquipTypeCode varchar(60) null, TestEquipTypeName varchar(100) null, TestType varchar(60) not null, TestItemID bigint null, TestItemCode varchar(60) not null, TestItemCName varchar(150) not null, TestItemSName varchar(60) null, TestItemEName varchar(60) null, TestCount int not null, Price float null, SumTotal float null, DataAddTime datetime null, DataTimeStamp timestamp null, constraint PK_REA_LISTESTSTATISTICALRESUL primary key (Id)); ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_LisTestStatisticalResults_Rea_TestEquipLab]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_LisTestStatisticalResults]')) ALTER TABLE [dbo].[Rea_LisTestStatisticalResults] DROP CONSTRAINT [FK_Rea_LisTestStatisticalResults_Rea_TestEquipLab]; ALTER TABLE [dbo].[Rea_LisTestStatisticalResults] WITH CHECK ADD CONSTRAINT [FK_Rea_LisTestStatisticalResults_Rea_TestEquipLab] FOREIGN KEY([TestEquipID]) REFERENCES [dbo].[Rea_TestEquipLab] ([TestEquipID]); ALTER TABLE [dbo].[Rea_LisTestStatisticalResults] CHECK CONSTRAINT [FK_Rea_LisTestStatisticalResults_Rea_TestEquipLab]; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_LisTestStatisticalResults_Rea_TestItem]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_LisTestStatisticalResults]')) ALTER TABLE [dbo].[Rea_LisTestStatisticalResults] DROP CONSTRAINT [FK_Rea_LisTestStatisticalResults_Rea_TestItem]; ALTER TABLE [dbo].[Rea_LisTestStatisticalResults] WITH CHECK ADD CONSTRAINT [FK_Rea_LisTestStatisticalResults_Rea_TestItem] FOREIGN KEY([TestItemID]) REFERENCES [dbo].[Rea_TestItem] ([TestItemID]); ALTER TABLE [dbo].[Rea_LisTestStatisticalResults] CHECK CONSTRAINT [FK_Rea_LisTestStatisticalResults_Rea_TestItem]; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_EquipTestItemReaGoodLink
                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_EquipTestItemReaGoodLink_Rea_TestEquipItem]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_EquipTestItemReaGoodLink]')) ALTER TABLE [dbo].[Rea_EquipTestItemReaGoodLink] DROP CONSTRAINT [FK_Rea_EquipTestItemReaGoodLink_Rea_TestEquipItem];";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_EquipTestItemReaGoodLink', 'TestEquipItemID') IS NOT NULL ALTER TABLE Rea_EquipTestItemReaGoodLink DROP COLUMN TestEquipItemID; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_EquipTestItemReaGoodLink', 'TestEquipID') IS NULL ALTER TABLE Rea_EquipTestItemReaGoodLink ADD TestEquipID  bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_EquipTestItemReaGoodLink_Rea_TestEquipLab]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_EquipTestItemReaGoodLink]')) ALTER TABLE [dbo].[Rea_EquipTestItemReaGoodLink] DROP CONSTRAINT [FK_Rea_EquipTestItemReaGoodLink_Rea_TestEquipLab]; ALTER TABLE [dbo].[Rea_EquipTestItemReaGoodLink] WITH CHECK ADD CONSTRAINT [FK_Rea_EquipTestItemReaGoodLink_Rea_TestEquipLab] FOREIGN KEY([TestEquipID]) REFERENCES [dbo].[Rea_TestEquipLab] ([TestEquipID]); ALTER TABLE [dbo].[Rea_EquipTestItemReaGoodLink] CHECK CONSTRAINT [FK_Rea_EquipTestItemReaGoodLink_Rea_TestEquipLab]; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_EquipTestItemReaGoodLink_Rea_TestItem]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_EquipTestItemReaGoodLink]')) ALTER TABLE [dbo].[Rea_EquipTestItemReaGoodLink] DROP CONSTRAINT [FK_Rea_EquipTestItemReaGoodLink_Rea_TestItem]; ALTER TABLE [dbo].[Rea_EquipTestItemReaGoodLink] WITH CHECK ADD CONSTRAINT [FK_Rea_EquipTestItemReaGoodLink_Rea_TestItem] FOREIGN KEY([TestItemID]) REFERENCES [dbo].[Rea_TestItem] ([TestItemID]); ALTER TABLE [dbo].[Rea_EquipTestItemReaGoodLink] CHECK CONSTRAINT [FK_Rea_EquipTestItemReaGoodLink_Rea_TestItem];";
                listSQL.Add(updateSql);
                #endregion

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
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsTransferDtl

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'SQtyDtlID') IS NULL ALTER TABLE Rea_BmsTransferDtl ADD SQtyDtlID  bigint; ";
                listSQL.Add(updateSql);

                updateSql = " update Rea_BmsTransferDtl set SQtyDtlID=QtyDtlID where SQtyDtlID is null; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_TestEquipLab

                updateSql = "update dbo.Rea_TestEquipLab set LisCode=TestEquipID where LisCode is null or LisCode=''; ";
                listSQL.Add(updateSql);

                #endregion

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
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsQtyDtl
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'InDocNo') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD InDocNo  varchar(20); ";
                listSQL.Add(updateSql);

                updateSql = "update Rea_BmsQtyDtl set InDocNo=(select indtl.InDocNo from Rea_BmsInDtl as indtl where InDtlID=Rea_BmsQtyDtl.InDtlID) where Rea_BmsQtyDtl.InDtlID is not null and Rea_BmsQtyDtl.InDocNo is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyBalanceDtl
                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDtl', 'InDocNo') IS NULL ALTER TABLE Rea_BmsQtyBalanceDtl ADD InDocNo  varchar(20); ";
                listSQL.Add(updateSql);

                updateSql = "update Rea_BmsQtyBalanceDtl set InDocNo=(select indtl.InDocNo from Rea_BmsInDtl as indtl where InDtlID=Rea_BmsQtyBalanceDtl.InDtlID) where Rea_BmsQtyBalanceDtl.InDtlID is not null and Rea_BmsQtyBalanceDtl.InDocNo is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsTransferDtl
                updateSql = " if exists(select 1 from sysobjects where name= 'FK_Rea_BmsTransferDtl_REFERENCE_REA_QtyDtlID ' and xtype= 'F ') ALTER TABLE dbo.Rea_BmsTransferDtl DROP CONSTRAINT FK_Rea_BmsTransferDtl_REFERENCE_REA_QtyDtlID ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'QtyDtlID') IS NOT NULL ALTER TABLE Rea_BmsTransferDtl ALTER column QtyDtlID varchar(400); ";
                listSQL.Add(updateSql);
                #endregion

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
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsOutDtl
                updateSql = " if exists(select 1 from sysobjects where name= 'FK_REA_BMSO_REFERENCE_REA_BMSQ ' and xtype= 'F ') ALTER TABLE dbo.Rea_BmsOutDtl DROP CONSTRAINT FK_REA_BMSO_REFERENCE_REA_BMSQ ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'QtyDtlID') IS NOT NULL ALTER TABLE Rea_BmsOutDtl ALTER column QtyDtlID varchar(400); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsTransferDtl
                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'SQtyDtlID') IS NOT NULL ALTER TABLE Rea_BmsTransferDtl ALTER column SQtyDtlID varchar(400); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsInDoc
                updateSql = " IF COL_LENGTH('Rea_BmsInDoc', 'ReconciliationMark') IS NULL ALTER TABLE Rea_BmsInDoc ADD ReconciliationMark  int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDoc', 'LockMark') IS NULL ALTER TABLE Rea_BmsInDoc ADD LockMark  int; ";
                listSQL.Add(updateSql);

                updateSql = "update Rea_BmsInDoc set ReconciliationMark=1,LockMark=1 where ReconciliationMark is null or ReconciliationMark=0; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsTransferDtlUseReturn
                updateSql = "  IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsTransferDtlUseReturn_Rea_BmsTransferDoc]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsTransferDtlUseReturn]')) ALTER TABLE [dbo].[Rea_BmsTransferDtlUseReturn] DROP CONSTRAINT [FK_Rea_BmsTransferDtlUseReturn_Rea_BmsTransferDoc]; IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsTransferDtlUseReturn_Rea_BmsTransferDtl]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsTransferDtlUseReturn]')) ALTER TABLE [dbo].[Rea_BmsTransferDtlUseReturn] DROP CONSTRAINT [FK_Rea_BmsTransferDtlUseReturn_Rea_BmsTransferDtl]; IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsTransferDtlUseReturn_Rea_Goods]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsTransferDtlUseReturn]')) ALTER TABLE [dbo].[Rea_BmsTransferDtlUseReturn] DROP CONSTRAINT [FK_Rea_BmsTransferDtlUseReturn_Rea_Goods]; IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rea_BmsTransferDtlUseReturn]') AND type IN (N'U')) DROP TABLE [dbo].[Rea_BmsTransferDtlUseReturn]; CREATE TABLE [dbo].[Rea_BmsTransferDtlUseReturn]( [LabID] [dbo].[D_系统主键] NULL, [UseReturnId] [bigint] NOT NULL, [TransferDtlID] [dbo].[D_系统主键] NOT NULL, [TransferDocID] [dbo].[D_系统主键] NOT NULL, [GoodsID] [dbo].[D_系统主键] NOT NULL, [GoodsCName] [varchar](200) NOT NULL, [GoodsUnitID] [dbo].[D_系统主键] NULL, [GoodsUnit] [varchar](50) NULL, [UnitMemo] [varchar](100) NULL, [ReaCompanyID] [bigint] NULL, [ReaCompanyName] [varchar](50) NULL, [GoodsQty] [float] NULL, [Price] [float] NULL, [SumTotal] [float] NULL, [GoodsNo] [varchar](100) NULL, [CompGoodsLinkID] [bigint] NULL, [ReaServerCompCode] [varchar](200) NULL, [ProdDate] [datetime] NULL, [ProdGoodsNo] [varchar](100) NULL, [InvalidDate] [datetime] NULL, [ReaGoodsNo] [varchar](100) NULL, [CenOrgGoodsNo] [varchar](100) NULL, [ReaCompCode] [varchar](200) NULL, [GoodsSort] [int] NULL, [LotNo] [varchar](100) NULL, [ZX1] [varchar](50) NULL, [ZX2] [varchar](50) NULL, [ZX3] [varchar](50) NULL, [DispOrder] [int] NULL, [Memo] [varchar](max) NULL, [Visible] [bit] NULL, [CreaterID] [bigint] NULL, [CreaterName] [varchar](50) NULL, [DataAddTime] [datetime] NULL, [DataUpdateTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_REA_BMSTRANSFERDTLUSERETURN] PRIMARY KEY CLUSTERED ( [UseReturnId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] ALTER TABLE [dbo].[Rea_BmsTransferDtlUseReturn] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsTransferDtlUseReturn_Rea_BmsTransferDoc] FOREIGN KEY([TransferDocID]) REFERENCES [dbo].[Rea_BmsTransferDoc] ([TransferDocID]); ALTER TABLE [dbo].[Rea_BmsTransferDtlUseReturn] CHECK CONSTRAINT [FK_Rea_BmsTransferDtlUseReturn_Rea_BmsTransferDoc]; ALTER TABLE [dbo].[Rea_BmsTransferDtlUseReturn] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsTransferDtlUseReturn_Rea_BmsTransferDtl] FOREIGN KEY([TransferDtlID]) REFERENCES [dbo].[Rea_BmsTransferDtl] ([TransferDtlID]); ALTER TABLE [dbo].[Rea_BmsTransferDtlUseReturn] CHECK CONSTRAINT [FK_Rea_BmsTransferDtlUseReturn_Rea_BmsTransferDtl]; ALTER TABLE [dbo].[Rea_BmsTransferDtlUseReturn] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsTransferDtlUseReturn_Rea_Goods] FOREIGN KEY([GoodsID]) REFERENCES [dbo].[Rea_Goods] ([GoodsID]); ALTER TABLE [dbo].[Rea_BmsTransferDtlUseReturn] CHECK CONSTRAINT [FK_Rea_BmsTransferDtlUseReturn_Rea_Goods]; ; ";
                listSQL.Add(updateSql);

                #endregion

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
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsQtyDtl
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'CSQtyDtlNo') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD CSQtyDtlNo  varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'CSInDtlNo') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD CSInDtlNo  varchar(50); ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_TestEquipLab
                updateSql = " IF COL_LENGTH('Rea_TestEquipLab', 'DeptID') IS NULL ALTER TABLE Rea_TestEquipLab ADD DeptID  bigint;";
                listSQL.Add(updateSql);

                updateSql = "  IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_TestEquipLab_HR_Dept]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_TestEquipLab]')) ALTER TABLE [dbo].[Rea_TestEquipLab] DROP CONSTRAINT [FK_Rea_TestEquipLab_HR_Dept]; ALTER TABLE [dbo].[Rea_TestEquipLab] WITH CHECK ADD CONSTRAINT [FK_Rea_TestEquipLab_HR_Dept] FOREIGN KEY([DeptID]) REFERENCES [dbo].[HR_Dept] ([DeptID]); ALTER TABLE [dbo].[Rea_TestEquipLab] CHECK CONSTRAINT [FK_Rea_TestEquipLab_HR_Dept];";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.63");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.63) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.64
            if (IsUpdateDataBase(oldVersion, "1.0.0.64"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsInDtl
                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'UnitMemo') IS NULL ALTER TABLE Rea_BmsInDtl ADD UnitMemo  varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " update Rea_BmsInDtl set Rea_BmsInDtl.UnitMemo=(select UnitMemo from Rea_Goods where Rea_BmsInDtl.GoodsID=Rea_Goods.GoodsID); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsTransferDoc
                updateSql = " IF COL_LENGTH('Rea_BmsTransferDoc', 'CheckMemo') IS NULL ALTER TABLE Rea_BmsTransferDoc ADD CheckMemo  varchar(500); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDoc', 'CheckTime') IS NULL ALTER TABLE Rea_BmsTransferDoc ADD CheckTime  datetime; ";
                listSQL.Add(updateSql);

                //移库完成
                updateSql = " update Rea_BmsTransferDoc set Status=6,StatusName='移库完成';";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsTransferDtl
                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'ReqCurrentQty') IS NULL ALTER TABLE Rea_BmsTransferDtl ADD ReqCurrentQty float NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'ReqGoodsQty') IS NULL ALTER TABLE Rea_BmsTransferDtl ADD ReqGoodsQty float NULL; ";
                listSQL.Add(updateSql);

                updateSql = " update Rea_BmsTransferDtl set ReqGoodsQty=GoodsQty where ReqGoodsQty is null or ReqGoodsQty='' or ReqGoodsQty=0;";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsOutDoc
                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'CheckMemo') IS NULL ALTER TABLE Rea_BmsOutDoc ADD CheckMemo  varchar(500); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'CheckTime') IS NULL ALTER TABLE Rea_BmsOutDoc ADD CheckTime  datetime; ";
                listSQL.Add(updateSql);

                //出库完成
                updateSql = " update Rea_BmsOutDoc set Status=6,StatusName='出库完成';";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsOutDtl
                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'ReqCurrentQty') IS NULL ALTER TABLE Rea_BmsOutDtl ADD ReqCurrentQty float NULL; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'ReqGoodsQty') IS NULL ALTER TABLE Rea_BmsOutDtl ADD ReqGoodsQty float NULL; ";
                listSQL.Add(updateSql);

                updateSql = " update Rea_BmsOutDtl set ReqGoodsQty=GoodsQty where ReqGoodsQty is null or ReqGoodsQty='' or ReqGoodsQty=0; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_MonthUsageStatisticsDoc
                updateSql = " IF COL_LENGTH('Rea_MonthUsageStatisticsDoc', 'RoundTypeId') IS NULL ALTER TABLE Rea_MonthUsageStatisticsDoc ADD RoundTypeId bigint NULL;";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_MonthUsageStatisticsDoc', 'RoundTypeName') IS NULL ALTER TABLE Rea_MonthUsageStatisticsDoc ADD RoundTypeName varchar(50);";
                listSQL.Add(updateSql);

                updateSql = " update Rea_MonthUsageStatisticsDoc set TypeID=2 where TypeID is null or TypeID='' or TypeID=1;";
                listSQL.Add(updateSql);

                updateSql = " update Rea_MonthUsageStatisticsDoc set RoundTypeId=1 where RoundTypeId is null or RoundTypeId='' or RoundTypeId=0;";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.64");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.64) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.65
            if (IsUpdateDataBase(oldVersion, "1.0.0.65"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_CenBarCodeFormat

                updateSql = "UPDATE [Rea_CenBarCodeFormat] SET [BarCodeType] = 2 WHERE BarCodeType is null or BarCodeType=''; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_TestEquipLab
                updateSql = " IF COL_LENGTH('Rea_TestEquipLab', 'DeptName') IS NULL ALTER TABLE Rea_TestEquipLab ADD DeptName varchar(200); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_CenOrg
                updateSql = " IF COL_LENGTH('Rea_CenOrg', 'OpeningBank') IS NULL ALTER TABLE Rea_CenOrg ADD OpeningBank varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_CenOrg', 'BankName') IS NULL ALTER TABLE Rea_CenOrg ADD BankName varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_CenOrg', 'TaxNumber') IS NULL ALTER TABLE Rea_CenOrg ADD TaxNumber varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_CenOrg', 'BankAccount') IS NULL ALTER TABLE Rea_CenOrg ADD BankAccount varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_CenOrg', 'IdentificationCode') IS NULL ALTER TABLE Rea_CenOrg ADD IdentificationCode varchar(200); ";
                listSQL.Add(updateSql);

                #endregion

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
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsTransferDoc
                updateSql = " IF COL_LENGTH('Rea_BmsTransferDoc', 'SStorageID') IS NOT NULL ALTER TABLE Rea_BmsTransferDoc DROP constraint FK_Rea_BmsTransferDoc_REFERENCE_REA_SStorageID; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDoc', 'DStorageID') IS NOT NULL ALTER TABLE Rea_BmsTransferDoc DROP constraint FK_Rea_BmsTransferDoc_REFERENCE_REA_DStorageID; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDoc', 'SStorageID') IS NOT NULL ALTER TABLE Rea_BmsTransferDoc DROP COLUMN SStorageID; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDoc', 'SStorageName') IS NOT NULL ALTER TABLE Rea_BmsTransferDoc DROP COLUMN SStorageName; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDoc', 'DStorageID') IS NOT NULL ALTER TABLE Rea_BmsTransferDoc DROP COLUMN DStorageID; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDoc', 'DStorageName') IS NOT NULL ALTER TABLE Rea_BmsTransferDoc DROP COLUMN DStorageName; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsTransferDtlUseReturn
                updateSql = " if exists(select 1 from sysobjects where id = object_id('Rea_BmsTransferDtlUseReturn') and type = 'U') drop table Rea_BmsTransferDtlUseReturn; ";
                listSQL.Add(updateSql);
                #endregion

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
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsTransferDoc
                updateSql = " IF COL_LENGTH('Rea_BmsTransferDoc', 'TakerName') IS NOT NULL ALTER TABLE Rea_BmsTransferDoc ALTER column  TakerName varchar(50);";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDoc', 'CheckName') IS NOT NULL ALTER TABLE Rea_BmsTransferDoc ALTER column  CheckName varchar(50);";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsOutDoc
                updateSql = " update Rea_BmsOutDoc set OperateOutDocID=null where OperateOutDocID=0;";
                listSQL.Add(updateSql);

                updateSql = " update Rea_BmsOutDoc set CheckID=null where CheckID=0;";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'TakerName') IS NOT NULL ALTER TABLE Rea_BmsOutDoc ALTER column  TakerName varchar(50);";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'CheckName') IS NOT NULL ALTER TABLE Rea_BmsOutDoc ALTER column  CheckName varchar(50);";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsOutDtl
                updateSql = " update Rea_BmsOutDtl set TestEquipID=null where TestEquipID=0;";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsTransferDtlUseReturn
                updateSql = " if exists(select 1 from sysobjects where id = object_id('Rea_BmsTransferDtlUseReturn') and type = 'U') drop table Rea_BmsTransferDtlUseReturn; ";
                listSQL.Add(updateSql);
                #endregion

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
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region RBAC_Module
                //仪器信息维护的模块入口更新
                updateSql = " update RBAC_Module set URL='#Shell.class.rea.client.equip.lab.App' where ModuleID=5719304057558610082; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsInDtl
                //入库明细的规格信息更新
                updateSql = " update Rea_BmsInDtl set Rea_BmsInDtl.UnitMemo=(select distinct g.UnitMemo from Rea_Goods as g where Rea_BmsInDtl.GoodsID=g.GoodsID) where Rea_BmsInDtl.UnitMemo is null or Rea_BmsInDtl.UnitMemo=''; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsInDtl
                //库存变化操作记录的出库金额(借出出库,使用出库,退供应商,盘亏出库,报损出库,移库领用)更新为负
                updateSql = " update Rea_BmsQtyDtlOperation set SumTotal=0-SumTotal where OperTypeID in (5,6,9,11,12,13) and SumTotal>0; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtlOperation
                //库存变化操作记录的ReaCompCode更新为供应商对应的OrgNo
                updateSql = " update Rea_BmsQtyDtlOperation set ReaCompCode=(select distinct c.OrgNo from Rea_CenOrg as c where Rea_BmsQtyDtlOperation.ReaCompanyID=c.OrgID) where ReaCompCode is null; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtlOperation', 'QtyDtlID') IS NULL ALTER TABLE Rea_BmsQtyDtlOperation ADD QtyDtlID bigint; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsTransferDoc
                updateSql = " IF COL_LENGTH('Rea_BmsTransferDoc', 'DeptName') IS NOT NULL ALTER TABLE Rea_BmsTransferDoc ALTER column  DeptName varchar(50);";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsOutDoc
                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'DeptName') IS NOT NULL ALTER TABLE Rea_BmsOutDoc ALTER column  DeptName varchar(50);";
                listSQL.Add(updateSql);

                #endregion

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
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsTransferDoc
                updateSql = " IF COL_LENGTH('Rea_BmsTransferDoc', 'SStorageID') IS NULL ALTER TABLE Rea_BmsTransferDoc ADD SStorageID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDoc', 'SStorageName') IS NULL ALTER TABLE Rea_BmsTransferDoc ADD SStorageName varchar(50);";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDoc', 'DStorageID') IS NULL ALTER TABLE Rea_BmsTransferDoc ADD DStorageID bigint;";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDoc', 'DStorageName') IS  NULL ALTER TABLE Rea_BmsTransferDoc ADD DStorageName varchar(50);";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsOutDoc
                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'StorageID') IS NULL ALTER TABLE Rea_BmsOutDoc ADD StorageID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'StorageName') IS NULL ALTER TABLE Rea_BmsOutDoc ADD StorageName varchar(50);";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.69");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.69) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.70
            if (IsUpdateDataBase(oldVersion, "1.0.0.70"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsOutDoc
                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'OutBoundID') IS NULL ALTER TABLE Rea_BmsOutDoc ADD OutBoundID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'OutBoundName') IS NULL ALTER TABLE Rea_BmsOutDoc ADD OutBoundName varchar(50);";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsOutDtl
                //通过出库明细的仪器名称获取仪器信息表的仪器ID,更新到出库明细列表的仪器ID值,原仪器ID值的值是错误的仪器ID值
                updateSql = " update Rea_BmsOutDtl set Rea_BmsOutDtl.TestEquipID=(select DISTINCT Rea_TestEquipLab.TestEquipID from dbo.Rea_TestEquipLab where  Rea_BmsOutDtl.TestEquipName=Rea_TestEquipLab.CName) where Rea_BmsOutDtl.TestEquipName!='' and Rea_BmsOutDtl.TestEquipName is not null; ";
                listSQL.Add(updateSql);

                updateSql = " IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsOutDtl_Rea_TestEquipLab]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsOutDtl]')) ALTER TABLE [dbo].[Rea_BmsOutDtl] DROP CONSTRAINT [FK_Rea_BmsOutDtl_Rea_TestEquipLab]; ";
                listSQL.Add(updateSql);

                updateSql = " ALTER TABLE [dbo].[Rea_BmsOutDtl]  WITH CHECK ADD  CONSTRAINT [FK_Rea_BmsOutDtl_Rea_TestEquipLab] FOREIGN KEY([TestEquipID]) REFERENCES[dbo].[Rea_TestEquipLab]([TestEquipID]); ALTER TABLE[dbo].[Rea_BmsOutDtl] CHECK CONSTRAINT[FK_Rea_BmsOutDtl_Rea_TestEquipLab]; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.70");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.70) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.71
            if (IsUpdateDataBase(oldVersion, "1.0.0.71"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsReqDtl
                updateSql = " IF COL_LENGTH('Rea_BmsReqDtl', 'Price') IS NULL ALTER TABLE Rea_BmsReqDtl ADD Price float; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsReqDtl', 'SumTotal') IS NULL ALTER TABLE Rea_BmsReqDtl ADD SumTotal float;";
                listSQL.Add(updateSql);

                updateSql = " update Rea_BmsReqDtl set Price=(select Price from Rea_GoodsOrgLink where Rea_GoodsOrgLink.Visible=1 and  Rea_BmsReqDtl.OrgID=Rea_GoodsOrgLink.CenOrgID and Rea_BmsReqDtl.GoodsID=Rea_GoodsOrgLink.GoodsID) where Rea_BmsReqDtl.OrgID is not null and Rea_BmsReqDtl.OrgID!='' and (Rea_BmsReqDtl.Price=0 or Rea_BmsReqDtl.Price is null);";
                listSQL.Add(updateSql);

                updateSql = " update Rea_BmsReqDtl set SumTotal=Price*GoodsQty where (SumTotal=0 or SumTotal is null);";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_Module
                //业务接口配置维护的模块入口更新
                updateSql = " update RBAC_Module set URL='#Shell.class.rea.client.businessinterface.App' where ModuleID=4847023968826006171; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_User_Storage_Link
                //删除库房货架人员权限关系的货架人员权限的关系,系统只对库房进行人员权限绑定
                updateSql = " delete FROM [Rea_User_Storage_Link] where PlaceID is not null; ";
                listSQL.Add(updateSql);

                #endregion
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
                string updateSql = "";
                List<string> listSQL = new List<string>();
                #region Rea_CenOrg
                updateSql = " IF COL_LENGTH('Rea_CenOrg', 'MatchCode') IS NULL ALTER TABLE Rea_CenOrg ADD MatchCode  varchar(100) ; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_Storage
                updateSql = " IF COL_LENGTH('Rea_Storage', 'MatchCode') IS NULL ALTER TABLE Rea_Storage ADD MatchCode  varchar(100) ; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_Place
                updateSql = " IF COL_LENGTH('Rea_Place', 'MatchCode') IS NULL ALTER TABLE Rea_Place ADD MatchCode  varchar(100) ; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.72");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.72) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.73
            if (IsUpdateDataBase(oldVersion, "1.0.0.73"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_TestItem
                updateSql = " IF COL_LENGTH('Rea_TestItem', 'Memo') IS NOT NULL ALTER TABLE Rea_TestItem ALTER column  Memo varchar(1024);";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.73");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.73) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.74
            if (IsUpdateDataBase(oldVersion, "1.0.0.74"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region RBAC_Module
                //仪器信息维护的模块入口更新
                updateSql = " update RBAC_Module set URL='#Shell.class.rea.client.equip.lab.App' where ModuleID=5719304057558610082; ";
                listSQL.Add(updateSql);

                //检验项目维护的模块入口更新
                updateSql = " update RBAC_Module set URL='#Shell.class.rea.client.testitem.item.App',CName='检验项目维护' where ModuleID=5284298220506850838; ";
                listSQL.Add(updateSql);

                //月结报表的模块名称调整为"结转报表"更新
                updateSql = " update RBAC_Module set CName='结转报表' where ModuleID=4893407254483225394; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsCenOrderDoc
                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDoc', 'IsHasApproval') IS NULL ALTER TABLE Rea_BmsCenOrderDoc ADD IsHasApproval   bit; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDoc', 'ApprovalID') IS NULL ALTER TABLE Rea_BmsCenOrderDoc ADD ApprovalID  bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDoc', 'ApprovalCName') IS NULL ALTER TABLE Rea_BmsCenOrderDoc ADD ApprovalCName  varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDoc', 'ApprovalTime') IS NULL ALTER TABLE Rea_BmsCenOrderDoc ADD ApprovalTime  datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDoc', 'ApprovalMemo') IS NULL ALTER TABLE Rea_BmsCenOrderDoc ADD ApprovalMemo  varchar(500); ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.74");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.74) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.75
            if (IsUpdateDataBase(oldVersion, "1.0.0.75"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsCenSaleDoc
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDoc', 'OtherDocNo') IS NULL ALTER TABLE Rea_BmsCenSaleDoc ADD OtherDocNo  varchar(50); ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsCenSaleDocConfirm
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDocConfirm', 'OtherDocNo') IS NULL ALTER TABLE Rea_BmsCenSaleDocConfirm ADD OtherDocNo  varchar(50); ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsInDoc
                updateSql = " IF COL_LENGTH('Rea_BmsInDoc', 'OtherDocNo') IS NULL ALTER TABLE Rea_BmsInDoc ADD OtherDocNo  varchar(50); ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsInDtl
                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'BatchSn') IS NULL ALTER TABLE Rea_BmsInDtl ADD BatchSn  varchar(200); ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.75");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.75) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.76
            if (IsUpdateDataBase(oldVersion, "1.0.0.76"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsCenSaleDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'OtherDtlNo') IS NULL ALTER TABLE Rea_BmsCenSaleDtl ADD OtherDtlNo  varchar(50); ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsCenSaleDtlConfirm
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'OtherDtlNo') IS NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ADD OtherDtlNo  varchar(50); ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsInDtl
                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'OtherDtlNo') IS NULL ALTER TABLE Rea_BmsInDtl ADD OtherDtlNo  varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'BatchSn') IS NOT NULL ALTER TABLE Rea_BmsInDtl DROP COLUMN BatchSn ; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.76");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.76) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.77
            if (IsUpdateDataBase(oldVersion, "1.0.0.77"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();
                #region HR_Employee
                //统一将各机构(智方管理机构除外)的系统管理员帐号的姓,名及全名称更新为"系统","管理员","系统管理员"
                updateSql = " update HR_Employee set NameL='系统',NameF='管理员',CName='系统管理员' where HR_Employee.DeveCode='sys_admin' and LabID!=1; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsReqDoc
                updateSql = " IF COL_LENGTH('Rea_BmsReqDoc', 'DeptName') IS NOT NULL ALTER TABLE Rea_BmsReqDoc ALTER COLUMN DeptName varchar(100); ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsCenOrderDoc
                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDoc', 'IsHasApproval') IS NOT NULL Update Rea_BmsCenOrderDoc set IsHasApproval=0 where IsHasApproval is null or IsHasApproval=''; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.77");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.77) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.78
            if (IsUpdateDataBase(oldVersion, "1.0.0.78"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();
                #region Rea_CenOrg
                updateSql = " IF COL_LENGTH('HR_Dept', 'MatchCode') IS NULL ALTER TABLE HR_Dept ADD MatchCode  varchar(100) ; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_Storage
                updateSql = " IF COL_LENGTH('HR_Employee', 'MatchCode') IS NULL ALTER TABLE HR_Employee ADD MatchCode  varchar(100) ; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.78");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.78) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.79
            if (IsUpdateDataBase(oldVersion, "1.0.0.79"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();
                #region Rea_BmsInDoc
                updateSql = " IF COL_LENGTH('Rea_BmsInDoc', 'SaleDocNo') IS NULL ALTER TABLE Rea_BmsInDoc ADD SaleDocNo  varchar(50) ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDoc', 'SaleDocConfirmID') IS NULL ALTER TABLE Rea_BmsInDoc ADD SaleDocConfirmID  bigint; ";
                listSQL.Add(updateSql);

                #endregion

                #region B_Parameter
                updateSql = " delete FROM B_Parameter where labId=5000080113240723860 and ParameterID in(5418911780137530835,5579887201355653315); ";
                listSQL.Add(updateSql);
                #endregion
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.79");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.79) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.80
            if (IsUpdateDataBase(oldVersion, "1.0.0.80"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_Storage
                updateSql = " IF COL_LENGTH('Rea_Storage', 'CName') IS NOT NULL ALTER TABLE Rea_Storage alter column CName varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_Storage', 'ShortCode') IS NOT NULL ALTER TABLE Rea_Storage alter column ShortCode varchar(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_Place
                updateSql = " IF COL_LENGTH('Rea_Place', 'CName') IS NOT NULL ALTER TABLE Rea_Place alter column CName varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_Place', 'ShortCode') IS NOT NULL ALTER TABLE Rea_Place alter column ShortCode varchar(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsInDtl
                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'StorageName') IS NOT NULL ALTER TABLE Rea_BmsInDtl alter column StorageName varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'PlaceName') IS NOT NULL ALTER TABLE Rea_BmsInDtl alter column PlaceName varchar(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtl
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'StorageName') IS NOT NULL ALTER TABLE Rea_BmsQtyDtl alter column StorageName varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'PlaceName') IS NOT NULL ALTER TABLE Rea_BmsQtyDtl alter column PlaceName varchar(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtlOperation
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtlOperation', 'StorageName') IS NOT NULL ALTER TABLE Rea_BmsQtyDtlOperation alter column StorageName varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtlOperation', 'PlaceName') IS NOT NULL ALTER TABLE Rea_BmsQtyDtlOperation alter column PlaceName varchar(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyBalanceDtl
                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDtl', 'StorageName') IS NOT NULL ALTER TABLE Rea_BmsQtyBalanceDtl alter column StorageName varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDtl', 'PlaceName') IS NOT NULL ALTER TABLE Rea_BmsQtyBalanceDtl alter column PlaceName varchar(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsTransferDtl
                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'SStorageName') IS NOT NULL ALTER TABLE Rea_BmsTransferDtl alter column SStorageName varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'SPlaceName') IS NOT NULL ALTER TABLE Rea_BmsTransferDtl alter column SPlaceName varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'DStorageName') IS NOT NULL ALTER TABLE Rea_BmsTransferDtl alter column DStorageName varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'DPlaceName') IS NOT NULL ALTER TABLE Rea_BmsTransferDtl alter column DPlaceName varchar(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCheckDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCheckDtl', 'StorageName') IS NOT NULL ALTER TABLE Rea_BmsCheckDtl alter column StorageName varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCheckDtl', 'PlaceName') IS NOT NULL ALTER TABLE Rea_BmsCheckDtl alter column PlaceName varchar(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_GoodsBarcodeOperation
                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'QtyDtlID') IS NULL ALTER TABLE Rea_GoodsBarcodeOperation ADD QtyDtlID  bigint;";
                listSQL.Add(updateSql);

                updateSql = " update Rea_GoodsBarcodeOperation set QtyDtlID=BDtlID where QtyDtlID is null or QtyDtlID='';";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsCenSaleDoc
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDoc', 'ZX1') IS NULL ALTER TABLE Rea_BmsCenSaleDoc ADD ZX1 varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDoc', 'ZX2') IS NULL ALTER TABLE Rea_BmsCenSaleDoc ADD ZX2 varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDoc', 'ZX3') IS NULL ALTER TABLE Rea_BmsCenSaleDoc ADD ZX3 varchar(50); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDocConfirm
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDocConfirm', 'ZX1') IS NULL ALTER TABLE Rea_BmsCenSaleDocConfirm ADD ZX1 varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDocConfirm', 'ZX2') IS NULL ALTER TABLE Rea_BmsCenSaleDocConfirm ADD ZX2 varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDocConfirm', 'ZX3') IS NULL ALTER TABLE Rea_BmsCenSaleDocConfirm ADD ZX3 varchar(50); ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.80");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.80) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.81
            if (IsUpdateDataBase(oldVersion, "1.0.0.81"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();
                #region B_Parameter
                updateSql = " update B_Parameter set SName='库房权限' where ParaNo='C-RBCD-ISCH-0023' ; ";
                listSQL.Add(updateSql);

                updateSql = " update B_Parameter set SName='库房权限' where ParaNo='C-RBTD-ISEO-0027' ; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.81");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.81) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.82
            if (IsUpdateDataBase(oldVersion, "1.0.0.82"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsInDtl
                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsInDtl_Rea_GoodsOrgLink]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsInDtl]')) ALTER TABLE [dbo].[Rea_BmsInDtl] DROP CONSTRAINT [FK_Rea_BmsInDtl_Rea_GoodsOrgLink]; ALTER TABLE [dbo].[Rea_BmsInDtl] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsInDtl_Rea_GoodsOrgLink] FOREIGN KEY([CompGoodsLinkID]) REFERENCES [dbo].[Rea_GoodsOrgLink] ([OrderGoodsID]); ALTER TABLE [dbo].[Rea_BmsInDtl] CHECK CONSTRAINT [FK_Rea_BmsInDtl_Rea_GoodsOrgLink]; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyBalanceDtl
                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsQtyBalanceDtl_Rea_GoodsOrgLink]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsQtyBalanceDtl]')) ALTER TABLE [dbo].[Rea_BmsQtyBalanceDtl] DROP CONSTRAINT [FK_Rea_BmsQtyBalanceDtl_Rea_GoodsOrgLink]; ALTER TABLE [dbo].[Rea_BmsQtyBalanceDtl] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsQtyBalanceDtl_Rea_GoodsOrgLink] FOREIGN KEY([CompGoodsLinkID]) REFERENCES [dbo].[Rea_GoodsOrgLink] ([OrderGoodsID]); ALTER TABLE [dbo].[Rea_BmsQtyBalanceDtl] CHECK CONSTRAINT [FK_Rea_BmsQtyBalanceDtl_Rea_GoodsOrgLink]; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsQtyBalanceDtl_Rea_BmsInDtl_InDtlID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsQtyBalanceDtl]')) ALTER TABLE [dbo].[Rea_BmsQtyBalanceDtl] DROP CONSTRAINT [FK_Rea_BmsQtyBalanceDtl_Rea_BmsInDtl_InDtlID]; ALTER TABLE [dbo].[Rea_BmsQtyBalanceDtl] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsQtyBalanceDtl_Rea_BmsInDtl_InDtlID] FOREIGN KEY([InDtlID]) REFERENCES [dbo].[Rea_BmsInDtl] ([InDtlID]); ALTER TABLE [dbo].[Rea_BmsQtyBalanceDtl] CHECK CONSTRAINT [FK_Rea_BmsQtyBalanceDtl_Rea_BmsInDtl_InDtlID]; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtl
                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsQtyDtl_Rea_GoodsOrgLink]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsQtyDtl]')) ALTER TABLE [dbo].[Rea_BmsQtyDtl] DROP CONSTRAINT [FK_Rea_BmsQtyDtl_Rea_GoodsOrgLink]; ALTER TABLE [dbo].[Rea_BmsQtyDtl] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsQtyDtl_Rea_GoodsOrgLink] FOREIGN KEY([CompGoodsLinkID]) REFERENCES [dbo].[Rea_GoodsOrgLink] ([OrderGoodsID]); ALTER TABLE [dbo].[Rea_BmsQtyDtl] CHECK CONSTRAINT [FK_Rea_BmsQtyDtl_Rea_GoodsOrgLink]; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsQtyDtl_Rea_Storage]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsQtyDtl]')) ALTER TABLE [dbo].[Rea_BmsQtyDtl] DROP CONSTRAINT [FK_Rea_BmsQtyDtl_Rea_Storage]; ALTER TABLE [dbo].[Rea_BmsQtyDtl] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsQtyDtl_Rea_Storage] FOREIGN KEY([StorageID]) REFERENCES [dbo].[Rea_Storage] ([StorageID]); ALTER TABLE [dbo].[Rea_BmsQtyDtl] CHECK CONSTRAINT [FK_Rea_BmsQtyDtl_Rea_Storage]; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsQtyDtl_Rea_Place_PlaceID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsQtyDtl]')) ALTER TABLE [dbo].[Rea_BmsQtyDtl] DROP CONSTRAINT [FK_Rea_BmsQtyDtl_Rea_Place_PlaceID]; ALTER TABLE [dbo].[Rea_BmsQtyDtl] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsQtyDtl_Rea_Place_PlaceID] FOREIGN KEY([PlaceID]) REFERENCES [dbo].[Rea_Place] ([PlaceID]); ALTER TABLE [dbo].[Rea_BmsQtyDtl] CHECK CONSTRAINT [FK_Rea_BmsQtyDtl_Rea_Place_PlaceID]; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsTransferDoc
                updateSql = " update Rea_BmsTransferDoc set DeptID=null where DeptID is null or DeptID='' or DeptID=0; ";
                listSQL.Add(updateSql);

                updateSql = " update Rea_BmsTransferDoc set DeptID=(select distinct  DeptID from HR_Dept where Rea_BmsTransferDoc.DeptName=HR_Dept.CName) where (DeptID is null or DeptID='' or DeptID=0) and DeptName is not null; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsTransferDoc_Rea_Storage_DStorageID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsTransferDoc]')) ALTER TABLE [dbo].[Rea_BmsTransferDoc] DROP CONSTRAINT [FK_Rea_BmsTransferDoc_Rea_Storage_DStorageID]; ALTER TABLE [dbo].[Rea_BmsTransferDoc] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsTransferDoc_Rea_Storage_DStorageID] FOREIGN KEY([DStorageID]) REFERENCES [dbo].[Rea_Storage] ([StorageID]); ALTER TABLE [dbo].[Rea_BmsTransferDoc] CHECK CONSTRAINT [FK_Rea_BmsTransferDoc_Rea_Storage_DStorageID]; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsTransferDoc_Rea_Storage_SStorageID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsTransferDoc]')) ALTER TABLE [dbo].[Rea_BmsTransferDoc] DROP CONSTRAINT [FK_Rea_BmsTransferDoc_Rea_Storage_SStorageID]; ALTER TABLE [dbo].[Rea_BmsTransferDoc] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsTransferDoc_Rea_Storage_SStorageID] FOREIGN KEY([SStorageID]) REFERENCES [dbo].[Rea_Storage] ([StorageID]); ALTER TABLE [dbo].[Rea_BmsTransferDoc] CHECK CONSTRAINT [FK_Rea_BmsTransferDoc_Rea_Storage_SStorageID]; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsTransferDoc_HR_Dept]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsTransferDoc]')) ALTER TABLE [dbo].[Rea_BmsTransferDoc] DROP CONSTRAINT [FK_Rea_BmsTransferDoc_HR_Dept]; ALTER TABLE [dbo].[Rea_BmsTransferDoc] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsTransferDoc_HR_Dept] FOREIGN KEY([DeptID]) REFERENCES [dbo].[HR_Dept] ([DeptID]); ALTER TABLE [dbo].[Rea_BmsTransferDoc] CHECK CONSTRAINT [FK_Rea_BmsTransferDoc_HR_Dept]; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsTransferDtl
                updateSql = "  IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsTransferDtl_Rea_GoodsOrgLink]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsTransferDtl]')) ALTER TABLE [dbo].[Rea_BmsTransferDtl] DROP CONSTRAINT [FK_Rea_BmsTransferDtl_Rea_GoodsOrgLink]; ALTER TABLE [dbo].[Rea_BmsTransferDtl] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsTransferDtl_Rea_GoodsOrgLink] FOREIGN KEY([CompGoodsLinkID]) REFERENCES [dbo].[Rea_GoodsOrgLink] ([OrderGoodsID]); ALTER TABLE [dbo].[Rea_BmsTransferDtl] CHECK CONSTRAINT [FK_Rea_BmsTransferDtl_Rea_GoodsOrgLink]; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsOutDoc
                updateSql = " update Rea_BmsOutDoc set DeptID=null where DeptID is null or DeptID='' or DeptID=0; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsOutDoc_Rea_Storage]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsOutDoc]')) ALTER TABLE [dbo].[Rea_BmsOutDoc] DROP CONSTRAINT [FK_Rea_BmsOutDoc_Rea_Storage]; ALTER TABLE [dbo].[Rea_BmsOutDoc] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsOutDoc_Rea_Storage] FOREIGN KEY([StorageID]) REFERENCES [dbo].[Rea_Storage] ([StorageID]); ALTER TABLE [dbo].[Rea_BmsOutDoc] CHECK CONSTRAINT [FK_Rea_BmsOutDoc_Rea_Storage]; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsOutDoc_HR_Dept]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsOutDoc]')) ALTER TABLE [dbo].[Rea_BmsOutDoc] DROP CONSTRAINT [FK_Rea_BmsOutDoc_HR_Dept]; ALTER TABLE [dbo].[Rea_BmsOutDoc] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsOutDoc_HR_Dept] FOREIGN KEY([DeptID]) REFERENCES [dbo].[HR_Dept] ([DeptID]); ALTER TABLE [dbo].[Rea_BmsOutDoc] CHECK CONSTRAINT [FK_Rea_BmsOutDoc_HR_Dept];";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsOutDtl
                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsOutDtl_Rea_GoodsOrgLink]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsOutDtl]')) ALTER TABLE [dbo].[Rea_BmsOutDtl] DROP CONSTRAINT [FK_Rea_BmsOutDtl_Rea_GoodsOrgLink]; ALTER TABLE [dbo].[Rea_BmsOutDtl] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsOutDtl_Rea_GoodsOrgLink] FOREIGN KEY([CompGoodsLinkID]) REFERENCES [dbo].[Rea_GoodsOrgLink] ([OrderGoodsID]); ALTER TABLE [dbo].[Rea_BmsOutDtl] CHECK CONSTRAINT [FK_Rea_BmsOutDtl_Rea_GoodsOrgLink]; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsOutDtl_Rea_Storage]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsOutDtl]')) ALTER TABLE [dbo].[Rea_BmsOutDtl] DROP CONSTRAINT [FK_Rea_BmsOutDtl_Rea_Storage]; ALTER TABLE [dbo].[Rea_BmsOutDtl] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsOutDtl_Rea_Storage] FOREIGN KEY([StorageID]) REFERENCES [dbo].[Rea_Storage] ([StorageID]); ALTER TABLE [dbo].[Rea_BmsOutDtl] CHECK CONSTRAINT [FK_Rea_BmsOutDtl_Rea_Storage]; ";
                listSQL.Add(updateSql);

                updateSql = "  IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsOutDtl_Rea_Place_PlaceID]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsOutDtl]')) ALTER TABLE [dbo].[Rea_BmsOutDtl] DROP CONSTRAINT [FK_Rea_BmsOutDtl_Rea_Place_PlaceID]; ALTER TABLE [dbo].[Rea_BmsOutDtl] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsOutDtl_Rea_Place_PlaceID] FOREIGN KEY([PlaceID]) REFERENCES [dbo].[Rea_Place] ([PlaceID]); ALTER TABLE [dbo].[Rea_BmsOutDtl] CHECK CONSTRAINT [FK_Rea_BmsOutDtl_Rea_Place_PlaceID]; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_GoodsBarcodeOperation
                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_GoodsBarcodeOperation_Rea_GoodsOrgLink]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_GoodsBarcodeOperation]')) ALTER TABLE [dbo].[Rea_GoodsBarcodeOperation] DROP CONSTRAINT [FK_Rea_GoodsBarcodeOperation_Rea_GoodsOrgLink]; ALTER TABLE [dbo].[Rea_GoodsBarcodeOperation] WITH CHECK ADD CONSTRAINT [FK_Rea_GoodsBarcodeOperation_Rea_GoodsOrgLink] FOREIGN KEY([CompGoodsLinkID]) REFERENCES [dbo].[Rea_GoodsOrgLink] ([OrderGoodsID]); ALTER TABLE [dbo].[Rea_GoodsBarcodeOperation] CHECK CONSTRAINT [FK_Rea_GoodsBarcodeOperation_Rea_GoodsOrgLink];  ";
                listSQL.Add(updateSql);

                //updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_GoodsBarcodeOperation_Rea_BmsQtyDtl]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_GoodsBarcodeOperation]')) ALTER TABLE [dbo].[Rea_GoodsBarcodeOperation] DROP CONSTRAINT [FK_Rea_GoodsBarcodeOperation_Rea_BmsQtyDtl]; ALTER TABLE [dbo].[Rea_GoodsBarcodeOperation] WITH CHECK ADD CONSTRAINT [FK_Rea_GoodsBarcodeOperation_Rea_BmsQtyDtl] FOREIGN KEY([QtyDtlID]) REFERENCES [dbo].[Rea_BmsQtyDtl] ([QtyDtlID]); ALTER TABLE [dbo].[Rea_GoodsBarcodeOperation] CHECK CONSTRAINT [FK_Rea_GoodsBarcodeOperation_Rea_BmsQtyDtl]; ";
                //listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtlOperation
                updateSql = "  IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsQtyDtlOperation_Rea_GoodsOrgLink]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsQtyDtlOperation]')) ALTER TABLE [dbo].[Rea_BmsQtyDtlOperation] DROP CONSTRAINT [FK_Rea_BmsQtyDtlOperation_Rea_GoodsOrgLink]; ALTER TABLE [dbo].[Rea_BmsQtyDtlOperation] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsQtyDtlOperation_Rea_GoodsOrgLink] FOREIGN KEY([CompGoodsLinkID]) REFERENCES [dbo].[Rea_GoodsOrgLink] ([OrderGoodsID]); ALTER TABLE [dbo].[Rea_BmsQtyDtlOperation] CHECK CONSTRAINT [FK_Rea_BmsQtyDtlOperation_Rea_GoodsOrgLink]; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsQtyDtlOperation_Rea_BmsQtyDtl]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsQtyDtlOperation]')) ALTER TABLE [dbo].[Rea_BmsQtyDtlOperation] DROP CONSTRAINT [FK_Rea_BmsQtyDtlOperation_Rea_BmsQtyDtl]; ALTER TABLE [dbo].[Rea_BmsQtyDtlOperation] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsQtyDtlOperation_Rea_BmsQtyDtl] FOREIGN KEY([QtyDtlID]) REFERENCES [dbo].[Rea_BmsQtyDtl] ([QtyDtlID]); ALTER TABLE [dbo].[Rea_BmsQtyDtlOperation] CHECK CONSTRAINT [FK_Rea_BmsQtyDtlOperation_Rea_BmsQtyDtl];  ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_CenBarCodeFormat
                updateSql = " update Rea_CenBarCodeFormat set BarCodeType=2 where BarCodeType is null or BarCodeType=''; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.82");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.82) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.83
            if (IsUpdateDataBase(oldVersion, "1.0.0.83"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_GoodsBarcodeOperation
                //更新1.0.0.80及之前的库存条码的QtyDtlID与库存记录ID的关系
                updateSql = " update Rea_GoodsBarcodeOperation set QtyDtlID=null where QtyDtlID=BDtlID; ";
                listSQL.Add(updateSql);
                //更新1.0.0.80及之前的库存条码的QtyDtlID与库存记录ID的关系
                updateSql = " update Rea_GoodsBarcodeOperation set QtyDtlID=(select qtyDtl3.QtyDtlID from( SELECT distinct qtyDtl2.QtyDtlID from Rea_GoodsBarcodeOperation left join Rea_BmsQtyDtl qtyDtl2 on BDtlID=qtyDtl2.QtyDtlID where OperTypeID!=1 and OperTypeID!=2 and qtyDtl2.QtyDtlID is not null) qtyDtl3 where BDtlID=qtyDtl3.QtyDtlID) where QtyDtlID is null or QtyDtlID=''; ";
                listSQL.Add(updateSql);
                //更新1.0.0.80之后的库存条码与库存记录ID的关系
                updateSql = " update Rea_GoodsBarcodeOperation set QtyDtlID=(select qtyDtl3.QtyDtlID from( SELECT distinct qtyDtl2.InDtlID,qtyDtl2.QtyDtlID from Rea_GoodsBarcodeOperation left join Rea_BmsQtyDtl qtyDtl2 on BDtlID=qtyDtl2.InDtlID where OperTypeID!=1 and OperTypeID!=2 and qtyDtl2.QtyDtlID is not null) qtyDtl3 where BDtlID=qtyDtl3.InDtlID) where QtyDtlID is null or QtyDtlID=''; ";
                listSQL.Add(updateSql);

                //更新1.0.0.80及之前的库存条码的InDtlID与库存记录InDtlID的关系(验货入库)
                updateSql = "  update Rea_GoodsBarcodeOperation set BDtlID=(select qtyDtl3.InDtlID from( SELECT distinct qtyDtl2.InDtlID,qtyDtl2.QtyDtlID from Rea_GoodsBarcodeOperation left join Rea_BmsQtyDtl qtyDtl2 on Rea_GoodsBarcodeOperation.QtyDtlID=qtyDtl2.QtyDtlID where OperTypeID=4) qtyDtl3 where Rea_GoodsBarcodeOperation.QtyDtlID=qtyDtl3.QtyDtlID and OperTypeID=4) where OperTypeID=4; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.83");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.83) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.84
            if (IsUpdateDataBase(oldVersion, "1.0.0.84"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_Goods
                updateSql = " IF COL_LENGTH('Rea_Goods', 'IsNeedPerformanceTest') IS NULL ALTER TABLE Rea_Goods ADD IsNeedPerformanceTest bit; ";
                listSQL.Add(updateSql);

                updateSql = " Update Rea_Goods set IsNeedPerformanceTest=0 where IsNeedPerformanceTest is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_GoodsLot
                updateSql = " IF COL_LENGTH('Rea_GoodsLot', 'IsNeedPerformanceTest') IS NULL ALTER TABLE Rea_GoodsLot ADD IsNeedPerformanceTest bit; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsLot', 'VerificationUserId') IS NULL ALTER TABLE Rea_GoodsLot ADD VerificationUserId  bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsLot', 'VerificationUserName') IS NULL ALTER TABLE Rea_GoodsLot ADD VerificationUserName  varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsLot', 'VerificationStatus') IS NULL ALTER TABLE Rea_GoodsLot ADD VerificationStatus bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsLot', 'VerificationTime') IS NULL ALTER TABLE Rea_GoodsLot ADD VerificationTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsLot', 'VerificationContent') IS NULL ALTER TABLE Rea_GoodsLot ADD VerificationContent  varchar(Max); ";
                listSQL.Add(updateSql);

                updateSql = " Update Rea_GoodsLot set IsNeedPerformanceTest=0 where IsNeedPerformanceTest is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Rea_GoodsLot set VerificationStatus=1 where VerificationStatus is null or VerificationStatus=''; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtl
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'IsNeedPerformanceTest') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD IsNeedPerformanceTest bit; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'VerificationStatus') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD VerificationStatus bigint; ";
                listSQL.Add(updateSql);

                updateSql = " Update Rea_BmsQtyDtl set IsNeedPerformanceTest=0 where IsNeedPerformanceTest is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Rea_BmsQtyDtl set VerificationStatus=1 where VerificationStatus is null or VerificationStatus=''; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_GoodsBarcodeOperation

                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'StorageID') IS NULL ALTER TABLE Rea_GoodsBarcodeOperation ADD StorageID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'PlaceID') IS NULL ALTER TABLE Rea_GoodsBarcodeOperation ADD PlaceID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'ScanCodeQty') IS NULL ALTER TABLE Rea_GoodsBarcodeOperation ADD ScanCodeQty float; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'MinBarCodeQty') IS NULL ALTER TABLE Rea_GoodsBarcodeOperation ADD MinBarCodeQty float; ";
                listSQL.Add(updateSql);

                updateSql = " Update Rea_GoodsBarcodeOperation set ScanCodeQty=1 where (ScanCodeQty is null or MinBarCodeQty=0) and OperTypeID>1; ";
                listSQL.Add(updateSql);

                //默认更新条码操作记录里的最小包装单位条码数等于条码操作记录货品的转换系数值
                updateSql = " Update Rea_GoodsBarcodeOperation set MinBarCodeQty=(SELECT distinct GonvertQty  from Rea_Goods where Rea_GoodsBarcodeOperation.GoodsID=Rea_Goods.GoodsID) where MinBarCodeQty is null or MinBarCodeQty=0;";
                listSQL.Add(updateSql);


                updateSql = " update Rea_GoodsBarcodeOperation set StorageID=(select distinct StorageID from Rea_BmsQtyDtl where Rea_GoodsBarcodeOperation.QtyDtlID=Rea_BmsQtyDtl.QtyDtlID) where Rea_GoodsBarcodeOperation.QtyDtlID is not null and Rea_GoodsBarcodeOperation.StorageID is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Rea_GoodsBarcodeOperation set PlaceID=(select distinct PlaceID from Rea_BmsQtyDtl where Rea_GoodsBarcodeOperation.QtyDtlID=Rea_BmsQtyDtl.QtyDtlID) where Rea_GoodsBarcodeOperation.QtyDtlID is not null and Rea_GoodsBarcodeOperation.PlaceID is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Rea_GoodsBarcodeOperation set OperTypeName='移库入库' where OperTypeID=6 and OperTypeName!='移库入库'; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.84");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.84) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.85
            if (IsUpdateDataBase(oldVersion, "1.0.0.85"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsCenOrderDoc
                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDoc', 'PayUserId') IS NULL ALTER TABLE Rea_BmsCenOrderDoc ADD PayUserId bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDoc', 'PayUserCName') IS NULL ALTER TABLE Rea_BmsCenOrderDoc ADD PayUserCName  varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDoc', 'PayStaus') IS NULL ALTER TABLE Rea_BmsCenOrderDoc ADD PayStaus  bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDoc', 'PayTime') IS NULL ALTER TABLE Rea_BmsCenOrderDoc ADD PayTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDoc', 'PayMemo') IS NULL ALTER TABLE Rea_BmsCenOrderDoc ADD PayMemo  varchar(500); ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsQtyDtlBarCodeLink
                updateSql = " if exists(select 1 from sysobjects where id = object_id('Rea_BmsQtyDtlBarCodeLink') and type = 'U') drop table Rea_BmsQtyDtlBarCodeLink; create table Rea_BmsQtyDtlBarCodeLink ( LabID D_系统主键 null, PReaGBOID bigint null, QtyBarCodeId bigint not null, QtyDtlID bigint null, ReaGBOID bigint null, GoodsID D_系统主键 null, GoodsUnit varchar(50) null, GoodsQty float null, MinBarCodeQty float null, DataAddTime datetime null, DataTimeStamp timestamp null, constraint PK_REA_BMSQTYDTLBARCODELINK primary key (QtyBarCodeId)); ";
                listSQL.Add(updateSql);

                updateSql = "  IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsQtyDtlBarCodeLink_Rea_BmsQtyDtl]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsQtyDtlBarCodeLink]')) ALTER TABLE [dbo].[Rea_BmsQtyDtlBarCodeLink] DROP CONSTRAINT [FK_Rea_BmsQtyDtlBarCodeLink_Rea_BmsQtyDtl]; ALTER TABLE [dbo].[Rea_BmsQtyDtlBarCodeLink] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsQtyDtlBarCodeLink_Rea_BmsQtyDtl] FOREIGN KEY([QtyDtlID]) REFERENCES [dbo].[Rea_BmsQtyDtl] ([QtyDtlID]); ALTER TABLE [dbo].[Rea_BmsQtyDtlBarCodeLink] CHECK CONSTRAINT [FK_Rea_BmsQtyDtlBarCodeLink_Rea_BmsQtyDtl]; ";
                listSQL.Add(updateSql);

                updateSql = "  IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsQtyDtlBarCodeLink_Rea_GoodsBarcodeOperation]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsQtyDtlBarCodeLink]')) ALTER TABLE [dbo].[Rea_BmsQtyDtlBarCodeLink] DROP CONSTRAINT [FK_Rea_BmsQtyDtlBarCodeLink_Rea_GoodsBarcodeOperation]; ALTER TABLE [dbo].[Rea_BmsQtyDtlBarCodeLink] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsQtyDtlBarCodeLink_Rea_GoodsBarcodeOperation] FOREIGN KEY([ReaGBOID]) REFERENCES [dbo].[Rea_GoodsBarcodeOperation] ([ReaGBOID]); ALTER TABLE [dbo].[Rea_BmsQtyDtlBarCodeLink] CHECK CONSTRAINT [FK_Rea_BmsQtyDtlBarCodeLink_Rea_GoodsBarcodeOperation]; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_GoodsBarcodeOperation
                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'ScanCodeGoodsUnit') IS NULL ALTER TABLE Rea_GoodsBarcodeOperation ADD ScanCodeGoodsUnit  varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'OverageQty') IS NULL ALTER TABLE Rea_GoodsBarcodeOperation ADD OverageQty float; ";
                listSQL.Add(updateSql);

                //验货接收,验货拒收
                updateSql = " update Rea_GoodsBarcodeOperation set ScanCodeGoodsUnit=(select distinct Rea_BmsCenSaleDtlConfirm.GoodsUnit from Rea_BmsCenSaleDtlConfirm where Rea_GoodsBarcodeOperation.BDtlID=Rea_BmsCenSaleDtlConfirm.SaleDtlConfirmID) where (OperTypeID=2 or OperTypeID=3) and ScanCodeGoodsUnit is null; ";
                listSQL.Add(updateSql);

                //验货入库,移库领用
                updateSql = " update Rea_GoodsBarcodeOperation set ScanCodeGoodsUnit=(select distinct Rea_BmsInDtl.GoodsUnit from Rea_BmsInDtl where Rea_GoodsBarcodeOperation.BDtlID=Rea_BmsInDtl.InDtlID) where (OperTypeID=4 or OperTypeID=15) and ScanCodeGoodsUnit is null; ";
                listSQL.Add(updateSql);

                //移库入库
                updateSql = " update Rea_GoodsBarcodeOperation set ScanCodeGoodsUnit=(select distinct Rea_BmsTransferDtl.GoodsUnit from Rea_BmsTransferDtl where Rea_GoodsBarcodeOperation.BDtlID=Rea_BmsTransferDtl.TransferDtlID) where OperTypeID=6 and ScanCodeGoodsUnit is null; ";
                listSQL.Add(updateSql);

                //使用出库
                updateSql = " update Rea_GoodsBarcodeOperation set ScanCodeGoodsUnit=(select distinct Rea_BmsQtyDtl.GoodsUnit from Rea_BmsQtyDtl where Rea_GoodsBarcodeOperation.QtyDtlID=Rea_BmsQtyDtl.QtyDtlID) where OperTypeID=7 and ScanCodeGoodsUnit is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Rea_GoodsBarcodeOperation set ScanCodeQty=1 where ScanCodeQty is null  or ScanCodeQty=0; ";
                listSQL.Add(updateSql);

                updateSql = " update Rea_GoodsBarcodeOperation set ScanCodeGoodsUnit=GoodsUnit where ScanCodeGoodsUnit is null; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.85");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.85) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.86
            if (IsUpdateDataBase(oldVersion, "1.0.0.86"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_Goods
                updateSql = " Update Rea_Goods set IsNeedPerformanceTest=0 where IsNeedPerformanceTest is null or IsNeedPerformanceTest=''; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_GoodsLot
                //updateSql = " IF COL_LENGTH('Rea_GoodsLot', 'GoodsID') IS NOT NULL ALTER TABLE Rea_GoodsLot DROP constraint FK_REA_GOOD_REFERENCE_REA_GOOD111 ";
                //listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsLot', 'ReaGoodsNo') IS NULL ALTER TABLE Rea_GoodsLot ADD ReaGoodsNo varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " Update Rea_GoodsLot set ReaGoodsNo=(SELECT distinct Rea_Goods.ReaGoodsNo FROM Rea_Goods where Rea_GoodsLot.GoodsID=Rea_Goods.GoodsID) where Rea_GoodsLot.ReaGoodsNo is null and Rea_GoodsLot.GoodsID is not null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Rea_GoodsLot set VerificationStatus=1 where VerificationStatus is null or VerificationStatus='' or VerificationStatus=0; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtl
                updateSql = " Update Rea_BmsQtyDtl set IsNeedPerformanceTest=0 where IsNeedPerformanceTest is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Rea_BmsQtyDtl set VerificationStatus=1 where VerificationStatus is null or VerificationStatus='' or VerificationStatus=0; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyBalanceDtl
                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDtl', 'CSQtyDtlNo') IS NULL ALTER TABLE Rea_BmsQtyBalanceDtl ADD CSQtyDtlNo varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDtl', 'CSInDtlNo') IS NULL ALTER TABLE Rea_BmsQtyBalanceDtl ADD CSInDtlNo varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDtl', 'IsNeedPerformanceTest') IS NULL ALTER TABLE Rea_BmsQtyBalanceDtl ADD IsNeedPerformanceTest bit; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDtl', 'VerificationStatus') IS NULL ALTER TABLE Rea_BmsQtyBalanceDtl ADD VerificationStatus bigint; ";
                listSQL.Add(updateSql);

                updateSql = " Update Rea_BmsQtyBalanceDtl set IsNeedPerformanceTest=0 where IsNeedPerformanceTest is null; ";
                listSQL.Add(updateSql);

                updateSql = " Update Rea_BmsQtyBalanceDtl set VerificationStatus=1 where VerificationStatus is null or VerificationStatus='' or VerificationStatus=0; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_GoodsBarcodeOperation

                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'ScanCodeGoodsID') IS NULL ALTER TABLE Rea_GoodsBarcodeOperation ADD ScanCodeGoodsID bigint; ";
                listSQL.Add(updateSql);

                //验货接收,验货拒收
                updateSql = " update Rea_GoodsBarcodeOperation set ScanCodeGoodsID=GoodsID where (OperTypeID=2 or OperTypeID=3) and ScanCodeGoodsID is null; ";
                listSQL.Add(updateSql);

                //验货入库,移库领用
                updateSql = " update Rea_GoodsBarcodeOperation set ScanCodeGoodsID=GoodsID where (OperTypeID=4 or OperTypeID=15) and ScanCodeGoodsID is null; ";
                listSQL.Add(updateSql);

                //移库入库
                updateSql = " update Rea_GoodsBarcodeOperation set ScanCodeGoodsID=GoodsID where OperTypeID=6 and ScanCodeGoodsID is null; ";
                listSQL.Add(updateSql);

                //使用出库
                updateSql = " update Rea_GoodsBarcodeOperation set ScanCodeGoodsID=GoodsID where OperTypeID=7 and ScanCodeGoodsID is null; ";
                listSQL.Add(updateSql);
                #endregion
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.86");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.86) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.87
            if (IsUpdateDataBase(oldVersion, "1.0.0.87"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsCenOrderDoc
                //统一更新为未付款状态
                updateSql = " update Rea_BmsCenOrderDoc set PayStaus=1 where PayStaus is null or PayStaus=0; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_GoodsBarcodeOperation
                updateSql = " update Rea_GoodsBarcodeOperation set ScanCodeGoodsUnit=GoodsUnit where MinBarCodeQty=1 and ScanCodeGoodsUnit!=GoodsUnit; ";
                listSQL.Add(updateSql);

                updateSql = " update Rea_GoodsBarcodeOperation set ScanCodeGoodsID=GoodsID where ScanCodeGoodsID!=GoodsID and GoodsUnit=ScanCodeGoodsUnit and ScanCodeGoodsUnit is not null; ";
                listSQL.Add(updateSql);

                //更新验收入库的BDocNo
                updateSql = " update Rea_GoodsBarcodeOperation set BDocNo=(select distinct Rea_BmsInDtl.InDocNo from Rea_BmsInDtl where Rea_GoodsBarcodeOperation.BDtlID=Rea_BmsInDtl.InDtlID) where OperTypeID=4 and BDocID=BDtlID; ";
                listSQL.Add(updateSql);

                //更新验收入库的BDocID
                updateSql = " update Rea_GoodsBarcodeOperation set BDocID=(select distinct Rea_BmsInDtl.InDocID from Rea_BmsInDtl where Rea_GoodsBarcodeOperation.BDtlID=Rea_BmsInDtl.InDtlID) where OperTypeID=4 and BDocID=BDtlID; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsTransferDoc
                updateSql = " update Rea_BmsTransferDoc set OperID=null where OperID=0; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsOutDoc
                updateSql = " update Rea_BmsOutDoc set OutBoundID=CreaterID where OutBoundID is null or OutBoundID=''; ";
                listSQL.Add(updateSql);

                updateSql = " update Rea_BmsOutDoc set OutBoundName=CreaterName where OutBoundName is null or OutBoundName=''; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtl
                //更新库存记录的二维批条码在移库后丢失的bug
                updateSql = "  update Rea_BmsQtyDtl set LotQRCode=( select distinct qty.LotQRCode from Rea_BmsQtyDtl as qty where qty.BarCodeType=0 and Rea_BmsQtyDtl.PQtyDtlID=qty.QtyDtlID) where LotQRCode is null and BarCodeType=0 and PQtyDtlID!=QtyDtlID; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.87");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.87) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.88
            if (IsUpdateDataBase(oldVersion, "1.0.0.88"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsCenSaleDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'GoodsLotID') IS NULL ALTER TABLE Rea_BmsCenSaleDtl ADD GoodsLotID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsCenSaleDtl_Rea_GoodsLot]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsCenSaleDtl]')) ALTER TABLE [dbo].[Rea_BmsCenSaleDtl] DROP CONSTRAINT [FK_Rea_BmsCenSaleDtl_Rea_GoodsLot]; ALTER TABLE [dbo].[Rea_BmsCenSaleDtl] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsCenSaleDtl_Rea_GoodsLot] FOREIGN KEY([GoodsLotID]) REFERENCES [dbo].[Rea_GoodsLot] ([GoodsLotID]); ALTER TABLE [dbo].[Rea_BmsCenSaleDtl] CHECK CONSTRAINT [FK_Rea_BmsCenSaleDtl_Rea_GoodsLot]; ";
                listSQL.Add(updateSql);

                //updateSql = " update Rea_BmsCenSaleDtl set Rea_BmsCenSaleDtl.GoodsLotID=(select distinct Rea_GoodsLot.GoodsLotID  from Rea_GoodsLot where Rea_BmsCenSaleDtl.LotNo=Rea_GoodsLot.LotNo and Rea_BmsCenSaleDtl.ReaGoodsNo=Rea_GoodsLot.ReaGoodsNo and Rea_GoodsLot.ReaGoodsNo is not null) where GoodsLotID is null or GoodsLotID=''; ";
                //listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsCenSaleDtlConfirm
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'GoodsLotID') IS NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ADD GoodsLotID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsCenSaleDtlConfirm_Rea_GoodsLot]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsCenSaleDtlConfirm]')) ALTER TABLE [dbo].[Rea_BmsCenSaleDtlConfirm] DROP CONSTRAINT [FK_Rea_BmsCenSaleDtlConfirm_Rea_GoodsLot]; ALTER TABLE [dbo].[Rea_BmsCenSaleDtlConfirm] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsCenSaleDtlConfirm_Rea_GoodsLot] FOREIGN KEY([GoodsLotID]) REFERENCES [dbo].[Rea_GoodsLot] ([GoodsLotID]); ALTER TABLE [dbo].[Rea_BmsCenSaleDtlConfirm] CHECK CONSTRAINT [FK_Rea_BmsCenSaleDtlConfirm_Rea_GoodsLot]; ";
                listSQL.Add(updateSql);

                //updateSql = " update Rea_BmsCenSaleDtlConfirm set Rea_BmsCenSaleDtlConfirm.GoodsLotID=(select distinct Rea_GoodsLot.GoodsLotID  from Rea_GoodsLot where Rea_BmsCenSaleDtlConfirm.LotNo=Rea_GoodsLot.LotNo and Rea_BmsCenSaleDtlConfirm.ReaGoodsNo=Rea_GoodsLot.ReaGoodsNo and Rea_GoodsLot.ReaGoodsNo is not null) where GoodsLotID is null or GoodsLotID=''; ";
                //listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsInDtl
                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'GoodsLotID') IS NULL ALTER TABLE Rea_BmsInDtl ADD GoodsLotID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsInDtl_Rea_GoodsLot]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsInDtl]')) ALTER TABLE [dbo].[Rea_BmsInDtl] DROP CONSTRAINT [FK_Rea_BmsInDtl_Rea_GoodsLot]; ALTER TABLE [dbo].[Rea_BmsInDtl] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsInDtl_Rea_GoodsLot] FOREIGN KEY([GoodsLotID]) REFERENCES [dbo].[Rea_GoodsLot] ([GoodsLotID]); ALTER TABLE [dbo].[Rea_BmsInDtl] CHECK CONSTRAINT [FK_Rea_BmsInDtl_Rea_GoodsLot]; ";
                listSQL.Add(updateSql);

                //updateSql = " update Rea_BmsInDtl set Rea_BmsInDtl.GoodsLotID=(select distinct Rea_GoodsLot.GoodsLotID  from Rea_GoodsLot where Rea_BmsInDtl.LotNo=Rea_GoodsLot.LotNo and Rea_BmsInDtl.ReaGoodsNo=Rea_GoodsLot.ReaGoodsNo and Rea_GoodsLot.ReaGoodsNo is not null) where GoodsLotID is null or GoodsLotID=''; ";
                //listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtl
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'GoodsLotID') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD GoodsLotID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsQtyDtl_Rea_GoodsLot]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsQtyDtl]')) ALTER TABLE [dbo].[Rea_BmsQtyDtl] DROP CONSTRAINT [FK_Rea_BmsQtyDtl_Rea_GoodsLot]; ALTER TABLE [dbo].[Rea_BmsQtyDtl] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsQtyDtl_Rea_GoodsLot] FOREIGN KEY([GoodsLotID]) REFERENCES [dbo].[Rea_GoodsLot] ([GoodsLotID]); ALTER TABLE [dbo].[Rea_BmsQtyDtl] CHECK CONSTRAINT [FK_Rea_BmsQtyDtl_Rea_GoodsLot]; ";
                listSQL.Add(updateSql);

                //updateSql = " update Rea_BmsQtyDtl set Rea_BmsQtyDtl.GoodsLotID=(select distinct Rea_GoodsLot.GoodsLotID  from Rea_GoodsLot where Rea_BmsQtyDtl.LotNo=Rea_GoodsLot.LotNo and Rea_BmsQtyDtl.ReaGoodsNo=Rea_GoodsLot.ReaGoodsNo and Rea_GoodsLot.ReaGoodsNo is not null) where GoodsLotID is null or GoodsLotID=''; ";
                //listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyBalanceDtl
                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDtl', 'GoodsLotID') IS NULL ALTER TABLE Rea_BmsQtyBalanceDtl ADD GoodsLotID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsQtyBalanceDtl_Rea_GoodsLot]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsQtyBalanceDtl]')) ALTER TABLE [dbo].[Rea_BmsQtyBalanceDtl] DROP CONSTRAINT [FK_Rea_BmsQtyBalanceDtl_Rea_GoodsLot]; ALTER TABLE [dbo].[Rea_BmsQtyBalanceDtl] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsQtyBalanceDtl_Rea_GoodsLot] FOREIGN KEY([GoodsLotID]) REFERENCES [dbo].[Rea_GoodsLot] ([GoodsLotID]); ALTER TABLE [dbo].[Rea_BmsQtyBalanceDtl] CHECK CONSTRAINT [FK_Rea_BmsQtyBalanceDtl_Rea_GoodsLot]; ";
                listSQL.Add(updateSql);

                //updateSql = " update Rea_BmsQtyBalanceDtl set Rea_BmsQtyBalanceDtl.GoodsLotID=(select distinct Rea_GoodsLot.GoodsLotID  from Rea_GoodsLot where Rea_BmsQtyBalanceDtl.LotNo=Rea_GoodsLot.LotNo and Rea_BmsQtyBalanceDtl.ReaGoodsNo=Rea_GoodsLot.ReaGoodsNo and Rea_GoodsLot.ReaGoodsNo is not null) where GoodsLotID is null or GoodsLotID=''; ";
                //listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtlOperation
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtlOperation', 'GoodsLotID') IS NULL ALTER TABLE Rea_BmsQtyDtlOperation ADD GoodsLotID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsQtyDtlOperation_Rea_GoodsLot]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsQtyDtlOperation]')) ALTER TABLE [dbo].[Rea_BmsQtyDtlOperation] DROP CONSTRAINT [FK_Rea_BmsQtyDtlOperation_Rea_GoodsLot]; ALTER TABLE [dbo].[Rea_BmsQtyDtlOperation] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsQtyDtlOperation_Rea_GoodsLot] FOREIGN KEY([GoodsLotID]) REFERENCES [dbo].[Rea_GoodsLot] ([GoodsLotID]); ALTER TABLE [dbo].[Rea_BmsQtyDtlOperation] CHECK CONSTRAINT [FK_Rea_BmsQtyDtlOperation_Rea_GoodsLot]; ";
                listSQL.Add(updateSql);

                //updateSql = " update Rea_BmsQtyDtlOperation set Rea_BmsQtyDtlOperation.GoodsLotID=(select distinct Rea_GoodsLot.GoodsLotID  from Rea_GoodsLot where Rea_BmsQtyDtlOperation.LotNo=Rea_GoodsLot.LotNo and Rea_BmsQtyDtlOperation.ReaGoodsNo=Rea_GoodsLot.ReaGoodsNo and Rea_GoodsLot.ReaGoodsNo is not null) where GoodsLotID is null or GoodsLotID=''; ";
                //listSQL.Add(updateSql);
                #endregion

                #region Rea_GoodsBarcodeOperation
                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'GoodsLotID') IS NULL ALTER TABLE Rea_GoodsBarcodeOperation ADD GoodsLotID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_GoodsBarcodeOperation_Rea_GoodsLot]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_GoodsBarcodeOperation]')) ALTER TABLE [dbo].[Rea_GoodsBarcodeOperation] DROP CONSTRAINT [FK_Rea_GoodsBarcodeOperation_Rea_GoodsLot]; ALTER TABLE [dbo].[Rea_GoodsBarcodeOperation] WITH CHECK ADD CONSTRAINT [FK_Rea_GoodsBarcodeOperation_Rea_GoodsLot] FOREIGN KEY([GoodsLotID]) REFERENCES [dbo].[Rea_GoodsLot] ([GoodsLotID]); ALTER TABLE [dbo].[Rea_GoodsBarcodeOperation] CHECK CONSTRAINT [FK_Rea_GoodsBarcodeOperation_Rea_GoodsLot]; ";
                listSQL.Add(updateSql);

                //updateSql = " update Rea_GoodsBarcodeOperation set Rea_GoodsBarcodeOperation.GoodsLotID=(select distinct Rea_GoodsLot.GoodsLotID  from Rea_GoodsLot where Rea_GoodsBarcodeOperation.LotNo=Rea_GoodsLot.LotNo and Rea_GoodsBarcodeOperation.ReaGoodsNo=Rea_GoodsLot.ReaGoodsNo and Rea_GoodsLot.ReaGoodsNo is not null) where GoodsLotID is null or GoodsLotID=''; ";
                //listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsTransferDtl
                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'GoodsLotID') IS NULL ALTER TABLE Rea_BmsTransferDtl ADD GoodsLotID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsTransferDtl_Rea_GoodsLot]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsTransferDtl]')) ALTER TABLE [dbo].[Rea_BmsTransferDtl] DROP CONSTRAINT [FK_Rea_BmsTransferDtl_Rea_GoodsLot]; ALTER TABLE [dbo].[Rea_BmsTransferDtl] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsTransferDtl_Rea_GoodsLot] FOREIGN KEY([GoodsLotID]) REFERENCES [dbo].[Rea_GoodsLot] ([GoodsLotID]); ALTER TABLE [dbo].[Rea_BmsTransferDtl] CHECK CONSTRAINT [FK_Rea_BmsTransferDtl_Rea_GoodsLot]; ";
                listSQL.Add(updateSql);

                //updateSql = " update Rea_BmsTransferDtl set Rea_BmsTransferDtl.GoodsLotID=(select distinct Rea_GoodsLot.GoodsLotID  from Rea_GoodsLot where Rea_BmsTransferDtl.LotNo=Rea_GoodsLot.LotNo and Rea_BmsTransferDtl.ReaGoodsNo=Rea_GoodsLot.ReaGoodsNo and Rea_GoodsLot.ReaGoodsNo is not null) where GoodsLotID is null or GoodsLotID=''; ";
                //listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsOutDtl
                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'GoodsLotID') IS NULL ALTER TABLE Rea_BmsOutDtl ADD GoodsLotID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsOutDtl_Rea_GoodsLot]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsOutDtl]')) ALTER TABLE [dbo].[Rea_BmsOutDtl] DROP CONSTRAINT [FK_Rea_BmsOutDtl_Rea_GoodsLot]; ALTER TABLE [dbo].[Rea_BmsOutDtl] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsOutDtl_Rea_GoodsLot] FOREIGN KEY([GoodsLotID]) REFERENCES [dbo].[Rea_GoodsLot] ([GoodsLotID]); ALTER TABLE [dbo].[Rea_BmsOutDtl] CHECK CONSTRAINT [FK_Rea_BmsOutDtl_Rea_GoodsLot]; ";
                listSQL.Add(updateSql);

                //updateSql = " update Rea_BmsOutDtl set Rea_BmsOutDtl.GoodsLotID=(select distinct Rea_GoodsLot.GoodsLotID  from Rea_GoodsLot where Rea_BmsOutDtl.LotNo=Rea_GoodsLot.LotNo and Rea_BmsOutDtl.ReaGoodsNo=Rea_GoodsLot.ReaGoodsNo and Rea_GoodsLot.ReaGoodsNo is not null) where GoodsLotID is null or GoodsLotID=''; ";
                //listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCheckDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCheckDtl', 'GoodsLotID') IS NULL ALTER TABLE Rea_BmsCheckDtl ADD GoodsLotID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Rea_BmsCheckDtl_Rea_GoodsLot]') AND parent_object_id = OBJECT_ID(N'[dbo].[Rea_BmsCheckDtl]')) ALTER TABLE [dbo].[Rea_BmsCheckDtl] DROP CONSTRAINT [FK_Rea_BmsCheckDtl_Rea_GoodsLot]; ALTER TABLE [dbo].[Rea_BmsCheckDtl] WITH CHECK ADD CONSTRAINT [FK_Rea_BmsCheckDtl_Rea_GoodsLot] FOREIGN KEY([GoodsLotID]) REFERENCES [dbo].[Rea_GoodsLot] ([GoodsLotID]); ALTER TABLE [dbo].[Rea_BmsCheckDtl] CHECK CONSTRAINT [FK_Rea_BmsCheckDtl_Rea_GoodsLot]; ";
                listSQL.Add(updateSql);

                //updateSql = " update Rea_BmsCheckDtl set Rea_BmsCheckDtl.GoodsLotID=(select distinct Rea_GoodsLot.GoodsLotID  from Rea_GoodsLot where Rea_BmsCheckDtl.LotNo=Rea_GoodsLot.LotNo and Rea_BmsCheckDtl.ReaGoodsNo=Rea_GoodsLot.ReaGoodsNo and Rea_GoodsLot.ReaGoodsNo is not null) where GoodsLotID is null or GoodsLotID=''; ";
                //listSQL.Add(updateSql);
                #endregion

                #region RBAC_Module
                //客户端授权变更模块入口由#Shell.class.rea.center.attachment.Grid更新为#Shell.class.rea.client.attachment.Grid
                updateSql = " update RBAC_Module set URL='#Shell.class.rea.client.attachment.Grid',CName='授权变更',Comment='客户端授权变更模块' where ModuleID=5230806933375061812;";
                listSQL.Add(updateSql);

                updateSql = " update RBAC_Module set Comment='apptype(应用类型:是否平台:是:1,否:0或null);只有在平台上物特定帐户才能维护条码信息;↵客户端只能查看条码规则' where ModuleID=5581567455116470933;";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.88");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.88) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.89
            if (IsUpdateDataBase(oldVersion, "1.0.0.89"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsCheckDoc
                updateSql = " IF COL_LENGTH('Rea_BmsCheckDoc', 'GoodsClass') IS NULL ALTER TABLE Rea_BmsCheckDoc ADD GoodsClass varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCheckDoc', 'GoodsClassType') IS NULL ALTER TABLE Rea_BmsCheckDoc ADD GoodsClassType varchar(100); ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsQtyMonthBalanceDoc
                updateSql = " IF COL_LENGTH('Rea_BmsQtyMonthBalanceDoc', 'GoodsClass') IS NULL ALTER TABLE Rea_BmsQtyMonthBalanceDoc ADD GoodsClass varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyMonthBalanceDoc', 'GoodsClassType') IS NULL ALTER TABLE Rea_BmsQtyMonthBalanceDoc ADD GoodsClassType varchar(100); ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.89");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.89) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.90
            if (IsUpdateDataBase(oldVersion, "1.0.0.90"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsCheckDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCheckDtl', 'BarCodeType') IS NULL ALTER TABLE Rea_BmsCheckDtl ADD BarCodeType bigint; ";
                listSQL.Add(updateSql);

                updateSql = " update Rea_BmsCheckDtl set BarCodeType=(select distinct Rea_Goods.BarCodeMgr from  Rea_Goods where Rea_BmsCheckDtl.GoodsID=Rea_Goods.GoodsID) where BarCodeType is null; ";
                listSQL.Add(updateSql);
                #endregion
                #region RBAC_Module
                //盘库管理模块入口调整为#Shell.class.rea.client.inventory.App
                updateSql = " update RBAC_Module set URL = '#Shell.class.rea.client.inventory.App' where ModuleID = 5089228353728448654 and URL = '#Shell.class.rea.client.stocktaking.App';";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.90");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.90) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.91
            if (IsUpdateDataBase(oldVersion, "1.0.0.91"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsCheckDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCheckDtl', 'OperDate') IS NOT NULL ALTER TABLE Rea_BmsCheckDtl DROP COLUMN OperDate; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCheckDtl', 'CreaterID') IS NOT NULL ALTER TABLE Rea_BmsCheckDtl DROP COLUMN CreaterID; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCheckDtl', 'CreaterName') IS NOT NULL ALTER TABLE Rea_BmsCheckDtl DROP COLUMN CreaterName; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select 1 from sysobjects where name= 'FK_REA_BMSC_REFERENCE_REA_GOOD_GoodsUnitID ' and xtype= 'F ') ALTER TABLE dbo.Rea_BmsCheckDtl DROP CONSTRAINT FK_REA_BMSC_REFERENCE_REA_GOOD_GoodsUnitID ; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCheckDtl', 'GoodsUnitID') IS NOT NULL ALTER TABLE Rea_BmsCheckDtl DROP COLUMN GoodsUnitID; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCheckDoc
                updateSql = " IF COL_LENGTH('Rea_BmsCheckDoc', 'OperDate') IS NOT NULL ALTER TABLE Rea_BmsCheckDoc DROP COLUMN OperDate; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_MaterialReaGoodsMatch
                //删除物资试剂对照关系表
                updateSql = " if exists(select 1 from sysobjects where id = object_id('Rea_MaterialReaGoodsMatch') and type = 'U') drop table Rea_MaterialReaGoodsMatch; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_MaterialReaCenOrgMatch
                //删除物资供应商对照关系表
                updateSql = " if exists(select 1 from sysobjects where id = object_id('Rea_MaterialReaCenOrgMatch') and type = 'U') drop table Rea_MaterialReaCenOrgMatch; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_GoodsBarcodeOperation
                updateSql = " update Rea_GoodsBarcodeOperation set LotNo=(select LotNo from Rea_GoodsLot where  Rea_GoodsBarcodeOperation.GoodsLotID=Rea_GoodsLot.GoodsLotID and Rea_GoodsBarcodeOperation.GoodsLotID is not null) where Rea_GoodsBarcodeOperation.GoodsLotID is not null and LotNo is null;";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_GoodsBarcodeOperation
                updateSql = " update Rea_GoodsBarcodeOperation set LotNo=(select distinct LotNo from Rea_GoodsLot where  Rea_GoodsBarcodeOperation.GoodsLotID=Rea_GoodsLot.GoodsLotID and Rea_GoodsBarcodeOperation.GoodsLotID is not null) where Rea_GoodsBarcodeOperation.GoodsLotID is not null and LotNo is null;";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_GoodsBarcodeOperation
                updateSql = "update Rea_BmsQtyDtl set SumTotal=Price*GoodsQty where GoodsQty>0 and Price>0 and SumTotal<=0;";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.91");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.91) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.92
            if (IsUpdateDataBase(oldVersion, "1.0.0.92"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region B_UserUIConfig

                updateSql = " if exists(select 1 from sysobjects where name= 'FK_B_UserUIConfig_HR_Employee ' and xtype= 'F ') ALTER TABLE dbo.B_UserUIConfig DROP CONSTRAINT FK_B_UserUIConfig_HR_Employee; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select 1 from sysobjects where id = object_id('B_UserUIConfig') and type = 'U') drop table B_UserUIConfig; create table B_UserUIConfig ( LabID D_系统主键 null, UserUIID D_系统主键 not null, TemplateTypeID bigint null, TemplateTypeCName varchar(100) null, UITypeID bigint null, UITypeName varchar(100) null, ModuleId D_系统主键 null, EmpID D_系统主键 null, IsDefault bit null, Comment ntext collate Chinese_PRC_CI_AS null, DispOrder int null, IsUse bit null, DataAddTime datetime null, DataUpdateTime datetime null, DataTimeStamp timestamp null, constraint PK_B_USERUICONFIG primary key (UserUIID)); ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS(SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_B_UserUIConfig_HR_Employee]') AND parent_object_id = OBJECT_ID(N'[dbo].[B_UserUIConfig]')) ALTER TABLE [dbo].[B_UserUIConfig] DROP CONSTRAINT [FK_B_UserUIConfig_HR_Employee]; ALTER TABLE [dbo].[B_UserUIConfig] WITH CHECK ADD CONSTRAINT [FK_B_UserUIConfig_HR_Employee] FOREIGN KEY([EmpID]) REFERENCES [dbo].[HR_Employee] ([EmpID]); ALTER TABLE [dbo].[B_UserUIConfig] CHECK CONSTRAINT [FK_B_UserUIConfig_HR_Employee]; ";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_Module
                //将原出库扫码(废)的模块更改为出库登记
                updateSql = " update RBAC_Module set CName='出库登记',Comment='技师站的检验技师使用出库登记',PinYinZiTou='CKDJ',IsUse=1,URL = '#Shell.class.rea.client.out.enrollment.App' where ModuleID = 4708695390288234016;";
                listSQL.Add(updateSql);

                #endregion
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.92");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.92) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.93
            if (IsUpdateDataBase(oldVersion, "1.0.0.93"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region B_UserUIConfig
                updateSql = " IF COL_LENGTH('B_UserUIConfig', 'UserUIKey') IS NULL ALTER TABLE B_UserUIConfig ADD UserUIKey varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('B_UserUIConfig', 'UserUIName') IS NULL ALTER TABLE B_UserUIConfig ADD UserUIName varchar(100); ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.93");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.93) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.94
            if (IsUpdateDataBase(oldVersion, "1.0.0.94"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region RBAC_Module

                //移库明细统计
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5286977522007680346) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[Comment],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,5286977522007680346,5226080549045182921,0,0,0,0,N'search.PNG',N'#Shell.class.rea.client.statistics.transferdtl.App',N'移库明细统计',N'YKMXTJ',N'YKMXTJ',N'默认合并条件:领用部门+供应商+试剂编码+单位+规格+批号+效期+领用人ID+领用日期',1,-400,N'2018/09/10 16:08:51'); else update RBAC_Module set [PicFile]='search.PNG',[URL]='#Shell.class.rea.client.statistics.transferdtl.App',[CName]='移库明细统计',[IsUse]=1,[DispOrder]=-400 where ModuleID=5286977522007680346; ";
                listSQL.Add(updateSql);

                //结转报表
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=4893407254483225394) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,4893407254483225394,5209809657140015238,0,0,0,0,N'd35d508b-acd3-49b1-8825-838054b6ae66.PNG',N'#Shell.class.rea.client.monthly.App',N'结转报表',N'JZBB',N'JZBB',1,8,N'2018/02/23 16:04:48'); else update RBAC_Module set [PicFile]='d35d508b-acd3-49b1-8825-838054b6ae66.PNG',[URL]='#Shell.class.rea.client.monthly.App',[CName]='结转报表',[IsUse]=1,[DispOrder]=8 where ModuleID=4893407254483225394;  ";
                listSQL.Add(updateSql);

                //入库-按库房
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5150541129733001382) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,5150541129733001382,5226080549045182921,0,0,0,1,N'search.PNG',N'#Shell.class.rea.client.statistics.echart.in.storage.App',N'入库-按库房',N'RK-AKF',N'RK-AKF',1,110,N'2019/02/18 14:09:56'); else update RBAC_Module set [PicFile]='search.PNG',[URL]='#Shell.class.rea.client.statistics.echart.in.storage.App',[CName]='入库-按库房' where ModuleID=5150541129733001382; ";
                listSQL.Add(updateSql);

                //入库-按供应商
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=4798209366126727956) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,4798209366126727956,5226080549045182921,0,0,0,0,N'search.PNG',N'#Shell.class.rea.client.statistics.echart.in.comp.App',N'入库-按供应商',N'RK-AGYS',N'RK-AGYS',1,111,N'2018/02/23 16:06:00') ; else update RBAC_Module set [PicFile]='search.PNG',[URL]='#Shell.class.rea.client.statistics.echart.in.comp.App',[CName]='入库-按供应商' where ModuleID=4798209366126727956; ";
                listSQL.Add(updateSql);

                //入库-按品牌
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=4784757344981945025) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,4784757344981945025,5226080549045182921,0,0,0,0,N'search.PNG',N'#Shell.class.rea.client.statistics.echart.in.brand.App',N'入库-按品牌',N'RK-APP',N'RK-APP',1,112,N'2018/02/23 16:06:14'); else update RBAC_Module set [PicFile]='search.PNG',[URL]='#Shell.class.rea.client.statistics.echart.in.brand.App',[CName]='入库-按品牌' where ModuleID=4784757344981945025; ";
                listSQL.Add(updateSql);

                //入库-按货品分类
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5704674097360512904) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,5704674097360512904,5226080549045182921,0,0,0,0,N'search.PNG',N'#Shell.class.rea.client.statistics.echart.in.goodsclass.App',N'入库-按货品分类',N'RK-AHPFL',N'RK-AHPFL',1,113,N'2018/02/23 16:05:47'); else update RBAC_Module set [PicFile]='search.PNG',[URL]='#Shell.class.rea.client.statistics.echart.in.goodsclass.App',[CName]='入库-按货品分类' where ModuleID=5704674097360512904; ";
                listSQL.Add(updateSql);

                //入库-货品分类堆叠
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=4911047587282485099) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,4911047587282485099,5226080549045182921,0,0,0,0,N'search.PNG',N'#Shell.class.rea.client.statistics.echart.in.goodsclass.stack.App',N'入库-货品分类堆叠',N'RK-HPFLDD',N'RK-HPFLDD',1,114,N'2019/02/21 17:05:06'); else update RBAC_Module set [PicFile]='search.PNG',[URL]='#Shell.class.rea.client.statistics.echart.in.goodsclass.stack.App',[CName]='入库-货品分类堆叠' where ModuleID=4911047587282485099; ";
                listSQL.Add(updateSql);


                //库存-按供应商
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5138508742887181203) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,5138508742887181203,5226080549045182921,0,0,0,0,N'search.PNG',N'#Shell.class.rea.client.statistics.echart.stock.comp.App',N'库存-按供应商',N'KC-AGYS',N'KC-AGYS',1,310,N'2018/02/23 16:08:08'); else update RBAC_Module set [PicFile]='search.PNG',[URL]='#Shell.class.rea.client.statistics.echart.stock.comp.App',[CName]='库存-按供应商',[IsUse]=1,[DispOrder]=310 where ModuleID=5138508742887181203; ";
                listSQL.Add(updateSql);

                //库存-按品牌
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5404182238743264145) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,5404182238743264145,5226080549045182921,0,0,0,0,N'default.PNG',N'#Shell.class.rea.client.statistics.echart.stock.brand.App',N'库存-按品牌',N'KC-APP',N'KC-APP',1,320,N'2018/02/23 16:07:53'); else update RBAC_Module set [PicFile]='search.PNG',[URL]='#Shell.class.rea.client.statistics.echart.stock.brand.App',[CName]='库存-按品牌',[IsUse]=1,[DispOrder]=320 where ModuleID=5404182238743264145; ";
                listSQL.Add(updateSql);

                //库存-按库房
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=4844627686950366285) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,4844627686950366285,5226080549045182921,0,0,0,0,N'default.PNG',N'#Shell.class.rea.client.statistics.echart.stock.storage.App',N'库存-按库房',N'KC-AKF',N'KC-AKF',1,340,N'2019/02/18 14:20:15'); else update RBAC_Module set [PicFile]='search.PNG',[URL]='#Shell.class.rea.client.statistics.echart.stock.storage.App',[CName]='库存-按库房',[IsUse]=1,[DispOrder]=330 where ModuleID=4844627686950366285; ";
                listSQL.Add(updateSql);

                //库存-按货品分类
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=4950009077277045739) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,4950009077277045739,5226080549045182921,0,0,0,0,N'default.PNG',N'#Shell.class.rea.client.statistics.echart.stock.goodsclass.App',N'库存-按货品分类',N'KC-AHPFL',N'KC-AHPFL',1,350,N'2018/02/23 16:08:36'); else update RBAC_Module set [PicFile]='search.PNG',[URL]='#Shell.class.rea.client.statistics.echart.stock.goodsclass.App',[CName]='库存-按货品分类',[IsUse]=1,[DispOrder]=350 where ModuleID=4950009077277045739; ";
                listSQL.Add(updateSql);

                //库存-货品分类堆叠
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5076500061229695163) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,5076500061229695163,5226080549045182921,0,0,0,0,N'default.PNG',N'#Shell.class.rea.client.statistics.echart.stock.goodsclass.stack.App',N'库存-货品分类堆叠',N'KC-HPFLDD',N'KC-HPFLDD',1,360,N'2019/02/21 17:07:10'); else update RBAC_Module set [PicFile]='search.PNG',[URL]='#Shell.class.rea.client.statistics.echart.stock.goodsclass.stack.App',[CName]='库存-货品分类堆叠',[IsUse]=1,[DispOrder]=360 where ModuleID=5076500061229695163; ";
                listSQL.Add(updateSql);

                //出库-按部门
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5408117864781928384) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,5408117864781928384,5226080549045182921,0,0,0,0,N'default.PNG',N'#Shell.class.rea.client.statistics.echart.out.dept.App',N'出库-按部门',N'CK-ABM',N'CK-ABM',1,410,N'2018/02/23 16:06:46'); else update RBAC_Module set [PicFile]='search.PNG',[URL]='#Shell.class.rea.client.statistics.echart.out.dept.App',[CName]='出库-按部门',[IsUse]=1,[DispOrder]=410 where ModuleID=5408117864781928384; ";
                listSQL.Add(updateSql);

                //出库-按仪器
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5051673220486625957) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES (1,5051673220486625957,5226080549045182921,0,0,0,0,N'default.PNG',N'#Shell.class.rea.client.statistics.echart.out.equip.App',N'出库-按仪器',N'CK-AYQ',N'CK-AYQ',1,420,N'2019/02/18 14:17:06'); else update RBAC_Module set [PicFile]='search.PNG',[URL]='#Shell.class.rea.client.statistics.echart.out.equip.App',[CName]='出库-按仪器',[IsUse]=1,[DispOrder]=420 where ModuleID=5051673220486625957; ";
                listSQL.Add(updateSql);

                //出库-按供应商
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5456779964966380474) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,5456779964966380474,5226080549045182921,0,0,0,0,N'default.PNG',N'#Shell.class.rea.client.statistics.echart.out.comp.App',N'出库-按供应商',N'CK-AGYS',N'CK-AGYS',1,430,N'2018/02/23 16:07:24'); else update RBAC_Module set [PicFile]='search.PNG',[URL]='#Shell.class.rea.client.statistics.echart.out.comp.App',[CName]='出库-按供应商',[IsUse]=1,[DispOrder]=430 where ModuleID=5456779964966380474; ";
                listSQL.Add(updateSql);

                //出库-按品牌
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5600047338526247633) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,5600047338526247633,5226080549045182921,0,0,0,0,N'default.PNG',N'#Shell.class.rea.client.statistics.echart.out.brand.App',N'出库-按品牌',N'CK-APP',N'CK-APP',1,440,N'2018/02/23 16:07:38'); else update RBAC_Module set [PicFile]='search.PNG',[URL]='#Shell.class.rea.client.statistics.echart.out.brand.App',[CName]='出库-按品牌',[IsUse]=1,[DispOrder]=440 where ModuleID=5600047338526247633; ";
                listSQL.Add(updateSql);

                //出库-按货品分类
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5512770573263302311) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,5512770573263302311,5226080549045182921,0,0,0,0,N'default.PNG',N'#Shell.class.rea.client.statistics.echart.out.goodsclass.App',N'出库-按货品分类',N'CK-AHPFL',N'CK-AHPFL',1,450,N'2018/02/23 16:06:30') ; else update RBAC_Module set [PicFile]='search.PNG',[URL]='#Shell.class.rea.client.statistics.echart.out.goodsclass.App',[CName]='出库-按货品分类',[IsUse]=1,[DispOrder]=450 where ModuleID=5512770573263302311; ";
                listSQL.Add(updateSql);

                //出库-货品分类堆叠
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5635394686675570040) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,5635394686675570040,5226080549045182921,0,0,0,0,N'default.PNG',N'#Shell.class.rea.client.statistics.echart.out.goodsclass.stack.App',N'出库-货品分类堆叠',N'CK-HPFLDD',N'CK-HPFLDD',1,460,N'2018/02/23 16:07:06') ; else update RBAC_Module set [PicFile]='search.PNG',[URL]='#Shell.class.rea.client.statistics.echart.out.goodsclass.stack.App',[CName]='出库-货品分类堆叠',[IsUse]=1,[DispOrder]=460 where ModuleID=5635394686675570040; ";
                listSQL.Add(updateSql);

                //备用统计1
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=4824818493607374259) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,4824818493607374259,5226080549045182921,0,0,0,0,N'default.PNG',N'#Shell.class.rea.client.statistics.stock2.stock.comp.App',N'备用统计1',N'BYTJ1',N'BYTJ1',1,2000,N'2019/02/21 17:09:06'); ";
                listSQL.Add(updateSql);

                //备用统计2
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5187262840568667584) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,5187262840568667584,5226080549045182921,0,0,0,0,N'default.PNG',N'备用统计2',N'BYTJ2',N'BYTJ2',1,2100,N'2019/02/21 17:09:28');  ";
                listSQL.Add(updateSql);

                //备用统计3
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5724098694513141401) INSERT [RBAC_Module] ([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,5724098694513141401,5226080549045182921,0,0,0,0,N'default.PNG',N'备用统计3',N'BFTJ3',N'BFTJ3',1,2300,N'2019/02/21 17:10:01'); ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.94");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.94) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.95
            if (IsUpdateDataBase(oldVersion, "1.0.0.95"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_DeptGoods
                updateSql = " IF COL_LENGTH('Rea_DeptGoods', 'GoodsCName') IS NOT NULL ALTER TABLE Rea_DeptGoods DROP COLUMN GoodsCName; ";
                listSQL.Add(updateSql);

                #endregion

                #region RBAC_Module
                updateSql = " if exists(select * from RBAC_Module where ModuleID=4828478982533751223) update RBAC_Module set [CName]='理论消耗量' where ModuleID=4828478982533751223;  ";
                listSQL.Add(updateSql);

                #endregion
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.95");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.95) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.96
            if (IsUpdateDataBase(oldVersion, "1.0.0.96"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsInDoc
                updateSql = " IF COL_LENGTH('Rea_BmsInDoc', 'SourceType') IS NULL ALTER TABLE Rea_BmsInDoc ADD SourceType bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDoc', 'SaleDocID') IS NULL ALTER TABLE Rea_BmsInDoc ADD SaleDocID bigint; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsInDtl
                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'SaleDtlID') IS NULL ALTER TABLE Rea_BmsInDtl ADD SaleDtlID bigint; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsOutDoc
                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'IsHasSaleCheck') IS NULL ALTER TABLE Rea_BmsOutDoc ADD IsHasSaleCheck bit; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'SaleCheckId') IS NULL ALTER TABLE Rea_BmsOutDoc ADD SaleCheckId bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'SaleCheckName') IS NULL ALTER TABLE Rea_BmsOutDoc ADD SaleCheckName varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'SaleCheckTime') IS NULL ALTER TABLE Rea_BmsOutDoc ADD SaleCheckTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'SaleCheckMemo') IS NULL ALTER TABLE Rea_BmsOutDoc ADD SaleCheckMemo varchar(500); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'IsHasFinanceCheck') IS NULL ALTER TABLE Rea_BmsOutDoc ADD IsHasFinanceCheck bit; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'FinanceCheckId') IS NULL ALTER TABLE Rea_BmsOutDoc ADD FinanceCheckId bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'FinanceCheckName') IS NULL ALTER TABLE Rea_BmsOutDoc ADD FinanceCheckName varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'FinanceCheckTime') IS NULL ALTER TABLE Rea_BmsOutDoc ADD FinanceCheckTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'FinanceCheckMemo') IS NULL ALTER TABLE Rea_BmsOutDoc ADD FinanceCheckMemo varchar(500); ";
                listSQL.Add(updateSql);

                #endregion

                #region RBAC_Role
                //将原labid=1,roleID=4845150561732500596的角色更改为测试机构"市一人民医院测试"
                updateSql = "update RBAC_Role set DeveCode='sys_admin',labID=5705145215734838227 where roleID=4845150561732500596; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.96");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.96) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.97
            if (IsUpdateDataBase(oldVersion, "1.0.0.97"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region RBAC_Role
                //将原labid=1,roleID=4845150561732500596的角色更改为测试机构"市一人民医院测试"
                updateSql = " update RBAC_Role set DeveCode='sys_admin',labID=5705145215734838227 where roleID=4845150561732500596; ";
                listSQL.Add(updateSql);

                #endregion

                #region RBAC_Module
                //供货商模块入口修改
                updateSql = " if exists(select * from RBAC_Module where ModuleID=5418145929645687885) update RBAC_Module set [Url]='#Shell.class.rea.client.reacenorg.supply.App' where ModuleID=5418145929645687885; ";
                listSQL.Add(updateSql);

                //订货方模块入口修改
                updateSql = " if exists(select * from RBAC_Module where ModuleID=4629485693532013923) update RBAC_Module set [Url]='#Shell.class.rea.client.reacenorg.purchase.App' where ModuleID=4629485693532013923; ";
                listSQL.Add(updateSql);

                //出库管理(审核):出库申请
                updateSql = " if exists(select * from RBAC_Module where ModuleID=4875456988011572797) update RBAC_Module set [Url]='#Shell.class.rea.client.out.special.apply.App' where ModuleID=4875456988011572797; ";
                listSQL.Add(updateSql);

                //出库管理(审核):销售审核
                updateSql = " if exists(select * from RBAC_Module where ModuleID=5330026505576804396) update RBAC_Module set [Url]='#Shell.class.rea.client.out.special.salecheck.App' where ModuleID=5330026505576804396; ";
                listSQL.Add(updateSql);

                //出库管理(审核):财务审核
                updateSql = " if exists(select * from RBAC_Module where ModuleID=5583571310107766458) update RBAC_Module set [Url]='#Shell.class.rea.client.out.special.financecheck.App' where ModuleID=5583571310107766458; ";
                listSQL.Add(updateSql);

                //出库管理(审核):出库审核(出库完成)
                updateSql = " if exists(select * from RBAC_Module where ModuleID=5136750174665109010) update RBAC_Module set [Url]='#Shell.class.rea.client.out.special.outaudit.App' where ModuleID=5136750174665109010; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.97");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.97) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.98
            if (IsUpdateDataBase(oldVersion, "1.0.0.98"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsOutDoc
                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'IsHasSaleCheck') IS not NULL exec sp_rename '[Rea_BmsOutDoc].[IsHasSaleCheck]','IsHasCheck';";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'IsHasFinanceCheck') IS not NULL exec sp_rename '[Rea_BmsOutDoc].[IsHasFinanceCheck]','IsHasApproval';";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'FinanceCheckId') IS not NULL exec sp_rename '[Rea_BmsOutDoc].[FinanceCheckId]','ApprovalId';";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'FinanceCheckName') IS not NULL exec sp_rename '[Rea_BmsOutDoc].[FinanceCheckName]','ApprovalCName';";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'FinanceCheckTime') IS not NULL exec sp_rename '[Rea_BmsOutDoc].[FinanceCheckTime]','ApprovalTime';";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'FinanceCheckMemo') IS not NULL exec sp_rename '[Rea_BmsOutDoc].[FinanceCheckMemo]','ApprovalMemo';";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'SaleCheckId') IS NOT NULL ALTER TABLE Rea_BmsOutDoc DROP COLUMN SaleCheckId; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'SaleCheckName') IS NOT NULL ALTER TABLE Rea_BmsOutDoc DROP COLUMN SaleCheckName; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'SaleCheckTime') IS NOT NULL ALTER TABLE Rea_BmsOutDoc DROP COLUMN SaleCheckTime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'SaleCheckMemo') IS NOT NULL ALTER TABLE Rea_BmsOutDoc DROP COLUMN SaleCheckMemo; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'ConfirmId') IS NULL ALTER TABLE Rea_BmsOutDoc ADD ConfirmId bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'ConfirmName') IS NULL ALTER TABLE Rea_BmsOutDoc ADD ConfirmName varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'ConfirmTime') IS NULL ALTER TABLE Rea_BmsOutDoc ADD ConfirmTime datetime; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'ConfirmMemo') IS NULL ALTER TABLE Rea_BmsOutDoc ADD ConfirmMemo varchar(500); ";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_Module
                //出库管理的功能模块调整

                //出库申请
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=4897631928495039961) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[Comment],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,4897631928495039961,4925427390656614626,0,0,0,0,N'960c9847-64dd-422d-85c0-59f32c97c977.PNG',N'#Shell.class.rea.client.out.apply.App?type=1',N'出库申请',N'CKSQ',N'CKSQ',N'type=1:使用出库申请:只申请，状态为暂存和提交;type=2:申请提交后自动审核、自动审批，状态直接变为审批通过;',1,-1100,N'2018/01/26 09:21:24'); else update RBAC_Module set [URL]='#Shell.class.rea.client.out.apply.App?type=1',[IsUse]=1,[DispOrder]=-1100,[CName]='出库申请',[ParentID]=4925427390656614626,[Comment]=N'type=1:使用出库申请:只申请，状态为暂存和提交;type=2:申请提交后自动审核、自动审批，状态直接变为审批通过;' where ModuleID=4897631928495039961; ";
                listSQL.Add(updateSql);

                //出库申请+
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=4875456988011572797) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[Comment],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,4875456988011572797,4925427390656614626,0,0,0,0,N'list.PNG',N'#Shell.class.rea.client.out.apply.App?type=2',N'出库申请+',N'CKSQ',N'CKSQ',N'type=1:使用出库申请:只申请，状态为暂存和提交;type=2:申请提交后自动审核、自动审批，状态直接变为审批通过;',1,-1090,N'2019/03/12 16:56:06'); else update RBAC_Module set [URL]='#Shell.class.rea.client.out.apply.App?type=2',[IsUse]=1,[DispOrder]=-1090,[CName]='出库申请+',[ParentID]=4925427390656614626,[Comment]=N'type=1:使用出库申请:只申请，状态为暂存和提交;type=2:申请提交后自动审核、自动审批，状态直接变为审批通过;' where ModuleID=4875456988011572797; ";
                listSQL.Add(updateSql);

                //出库审核
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5330026505576804396) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[Comment],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,5330026505576804396,4925427390656614626,0,0,0,0,N'list.PNG',N'#Shell.class.rea.client.out.check.App?type=1',N'出库审核',N'CKSH',N'CKSH',N'type=1:只审核，状态为审核通过，审核退回; type=2:审核后自动审批，状态为审批通过，审核退回;',1,-1080,N'2019/03/12 16:58:09'); else update RBAC_Module set [URL]='#Shell.class.rea.client.out.check.App?type=1',[IsUse]=1,[DispOrder]=-1080,[CName]='出库审核',[ParentID]=4925427390656614626,[Comment]=N'type=1:只审核，状态为审核通过，审核退回; type=2:审核后自动审批，状态为审批通过，审核退回;' where ModuleID=5330026505576804396;";
                listSQL.Add(updateSql);

                //出库审核+
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5136750174665109010) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[Comment],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,5136750174665109010,4925427390656614626,0,0,0,0,N'list.PNG',N'#Shell.class.rea.client.out.check.App?type=2',N'出库审核+',N'CKSH',N'CKSH',N'type=1:只审核，状态为审核通过，审核退回; type=2:审核后自动审批，状态为审批通过，审核退回;',1,-1070,N'2019/03/12 16:59:56'); else update RBAC_Module set [URL]='#Shell.class.rea.client.out.check.App?type=2',[IsUse]=1,[DispOrder]=-1070,[CName]='出库审核+',[ParentID]=4925427390656614626,[Comment]=N'type=1:只审核，状态为审核通过，审核退回; type=2:审核后自动审批，状态为审批通过，审核退回;' where ModuleID=5136750174665109010;";
                listSQL.Add(updateSql);

                //出库审批
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5583571310107766458) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[Comment],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,5583571310107766458,4925427390656614626,0,0,0,0,N'list.PNG',N'#Shell.class.rea.client.out.approval.App',N'出库审批',N'CKSP',N'CKSP',N'状态为审批通过，审批退回',1,-1060,N'2019/03/12 16:58:32'); else update RBAC_Module set [URL]='#Shell.class.rea.client.out.approval.App',[IsUse]=1,[DispOrder]=-1060,[CName]='出库审批',[ParentID]=4925427390656614626,[Comment]=N'状态为审批通过，审批退回;' where ModuleID=5583571310107766458;";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.98");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.98) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.99
            if (IsUpdateDataBase(oldVersion, "1.0.0.99"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsOutDoc
                //更新原出库主单的审核确认信息
                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'ConfirmId') IS NOT NULL update Rea_BmsOutDoc set ConfirmId=CheckID,ConfirmName=CheckName,ConfirmTime=CheckTime,ConfirmMemo=CheckMemo where ConfirmId is null or ConfirmId='';";
                listSQL.Add(updateSql);

                #endregion

                #region RBAC_Module
                //移库管理
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=4774905743206345598) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,4774905743206345598,5019301137460545284,0,0,0,3,N'package.PNG',N'移库管理',N'YKGL',N'YKGL',1,81,N'2018/11/16 13:38:47'); else update RBAC_Module set [URL]='',[IsUse]=1,[DispOrder]=81,[CName]='移库管理',[Comment]=N'移库管理' where ModuleID=4774905743206345598;";
                listSQL.Add(updateSql);

                //追溯
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=4861274195367138647) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,4861274195367138647,5019301137460545284,0,0,0,3,N'package.PNG',N'追溯',N'ZS',N'ZS',1,83,N'2019/03/12 16:53:02'); else update RBAC_Module set [URL]='',[IsUse]=1,[DispOrder]=83,[CName]='追溯',[Comment]=N'追溯' where ModuleID=4861274195367138647;";
                listSQL.Add(updateSql);

                //废弃模块
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5732090465046195882) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,5732090465046195882,5019301137460545284,0,0,0,3,N'package.PNG',N'废弃模块',N'FQMK',N'FQMK',1,100000,N'2019/03/21 09:41:59'); else update RBAC_Module set [URL]='',[IsUse]=1,[DispOrder]=100000,[CName]='废弃模块',[Comment]=N'废弃模块' where ModuleID=5732090465046195882;";
                listSQL.Add(updateSql);

                //移库申请
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5681144456133890599) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[Comment],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,5681144456133890599,4774905743206345598,0,0,0,0,N'ab00067b-7401-4244-8b9a-1f7dd3e37735.PNG',N'#Shell.class.rea.client.transfer.apply.App',N'移库申请',N'YKSQ',N'YKSQ',N'移库申请',1,-1000,N'2018/10/30 16:09:03'); else update RBAC_Module set [URL]='#Shell.class.rea.client.transfer.apply.App',[IsUse]=1,[DispOrder]=-1000,[CName]='移库申请',[ParentID]=4774905743206345598,[Comment]=N'' where ModuleID=5681144456133890599; ";
                listSQL.Add(updateSql);

                //直接移库
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5307474528904540595) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[Comment],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,5307474528904540595,4774905743206345598,0,0,0,0,N'325cc209-76c6-4213-ba95-a0c180d0fbfb.PNG',N'#Shell.class.rea.client.transfer.App',N'直接移库',N'ZJYK',N'ZJYK',N'直接对库存试剂进行移库登记',1,-300,N'2018/01/26 09:21:55'); else update RBAC_Module set [URL]='#Shell.class.rea.client.transfer.App',[IsUse]=1,[DispOrder]=-300,[CName]='直接移库',[ParentID]=4774905743206345598,[Comment]=N'' where ModuleID=5307474528904540595; ";
                listSQL.Add(updateSql);

                //申请后移库
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=4931598026806307124) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[Comment],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,4931598026806307124,4774905743206345598,0,0,0,0,N'9a6b8ab8-332c-4987-87ca-64acf52ac885.PNG',N'#Shell.class.rea.client.transfer.App?type=2',N'申请后移库',N'SQHYK',N'SQHYK',N'仅对移库申请单进行移库登记管理',1,-295,N'2018/10/30 17:54:12'); else update RBAC_Module set [URL]='#Shell.class.rea.client.transfer.App?type=2',[IsUse]=1,[DispOrder]=-295,[CName]='申请后移库',[ParentID]=4774905743206345598 where ModuleID=4931598026806307124; ";
                listSQL.Add(updateSql);

                //移库
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5027554228353190691) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[Comment],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,5027554228353190691,4774905743206345598,0,0,0,0,N'04aa2322-4645-4ce0-9db8-a0c56a3e41ca.PNG',N'#Shell.class.rea.client.transfer.App?type=3',N'移库',N'YK',N'YK',N'同时支持直接移库登记及对移库申请进行移库确认',1,-290,N'2018/10/30 17:55:08'); else update RBAC_Module set [URL]='#Shell.class.rea.client.transfer.App?type=3',[IsUse]=1,[DispOrder]=-290,[CName]='移库',[Comment]=N'同时支持直接移库登记及对移库申请进行移库确认',[ParentID]=4774905743206345598 where ModuleID=5027554228353190691; ";
                listSQL.Add(updateSql);

                //直接出库
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5151291363834479799) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[Comment],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,5151291363834479799,4925427390656614626,0,0,0,0,N'256374a7-313b-4732-a94b-8820bbba8b83.PNG',N'#Shell.class.rea.client.out.use.App?type=1',N'直接出库',N'SYCK',N'SYCK',N'不用申请就出库',1,-1000,N'2018/02/23 15:58:06'); else update RBAC_Module set [IsUse]=1,[DispOrder]=-1000,[CName]='直接出库',[Comment]=N'不用申请就出库' where ModuleID=5151291363834479799; ";
                listSQL.Add(updateSql);

                //使用出库
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=4826747902220238918) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[Comment],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,4826747902220238918,4925427390656614626,0,0,0,0,N'581cda0c-03ba-44fa-aefe-0faad6cfe411.PNG',N'#Shell.class.rea.client.out.use.App?type=3',N'使用出库',N'SYCK(QB)',N'SYCK(QB)',N'同时支持直接出库及对出库申请进行确认出库',1,-980,N'2018/10/30 18:07:19'); else update RBAC_Module set [IsUse]=1,[DispOrder]=-980,[ParentID]=4925427390656614626,[CName]='使用出库',[Comment]=N'同时支持直接出库及对出库申请进行确认出库' where ModuleID=4826747902220238918; ";
                listSQL.Add(updateSql);

                //出库查询
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5367839229201773101) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,5367839229201773101,4925427390656614626,0,0,0,0,N'4b708112-b7c3-4354-90a2-4fceb7607d1b.PNG',N'#Shell.class.rea.client.out.search.App',N'出库查询',N'CKCX',N'CKCX',1,500,N'2018/05/02 11:39:40'); else update RBAC_Module set [IsUse]=1,[DispOrder]=500,[ParentID]=4925427390656614626,[CName]='出库查询',[Comment]=N'' where ModuleID=5367839229201773101; ";
                listSQL.Add(updateSql);

                //入库查询
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5140291663138310607) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[Comment],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,5140291663138310607,4868532628695471683,0,0,0,0,N'73fe6144-074c-410b-8135-aaa392a98faa.PNG',N'#Shell.class.rea.client.stock.seach.App',N'入库查询',N'RKCX',N'RKCX',N'根据相关条件查询通过验收入库的入库信息',1,45,N'2018/02/23 15:56:26'); else update RBAC_Module set [IsUse]=1,[DispOrder]=45,[ParentID]=4868532628695471683,[CName]='入库查询',[Comment]=N'根据相关条件查询通过验收入库的入库信息' where ModuleID=5140291663138310607; ";
                listSQL.Add(updateSql);

                //库存变化跟踪
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5417477829625166270) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 1,5417477829625166270,4861274195367138647,0,0,0,0,N'fe997033-8008-4422-9ef5-f5a78b1367f0.PNG',N'#Shell.class.rea.client.qtyoperation.App',N'库存变化跟踪',N'KCBHGZ',N'KCBHGZ',1,3,N'2018/02/23 16:05:02'); else update RBAC_Module set [IsUse]=1,[DispOrder]=3,[ParentID]=4861274195367138647,[CName]='库存变化跟踪',[Comment]=N'' where ModuleID=5417477829625166270; ";
                listSQL.Add(updateSql);

                //库存变化跟踪
                updateSql = " if not exists(select * from RBAC_Module where ModuleID=5411518067923929024) INSERT [RBAC_Module]([LabID],[ModuleID],[ParentID],[LevelNum],[TreeCatalog],[IsLeaf],[ModuleType],[PicFile],[URL],[CName],[Shortcode],[PinYinZiTou],[Comment],[IsUse],[DispOrder],[DataAddTime]) VALUES ( 0,5411518067923929024,4861274195367138647,0,0,0,0,N'search.PNG',N'#Shell.class.rea.client.barcodeoperation.App',N'盒条码操作记录',N'HTMCZJL',N'HTMCZJL',N'提供对库存货品的盒条码操作记录查看',1,110,N'2019/01/08 14:41:47'); else update RBAC_Module set [IsUse]=1,[DispOrder]=110,[ParentID]=4861274195367138647,[CName]='盒条码操作记录',[Comment]=N'提供对库存货品的盒条码操作记录查看' where ModuleID=5411518067923929024; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.99");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.99) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.100
            if (IsUpdateDataBase(oldVersion, "1.0.0.100"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                updateSql = " IF COL_LENGTH('Rea_Goods', 'GoodsUnitID') IS NULL ALTER TABLE Rea_Goods ADD GoodsUnitID bigint; ";
                listSQL.Add(updateSql);

                #region Rea_BmsReqDtl
                updateSql = " if exists(select 1 from sysobjects where name= 'FK_REA_BMSR_REFERENCE_REA_GOOD ' and xtype= 'F ') ALTER TABLE dbo.Rea_BmsReqDtl DROP CONSTRAINT FK_REA_BMSR_REFERENCE_REA_GOOD;";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsInDtl
                updateSql = " if exists(select 1 from sysobjects where name= 'FK_REA_BMSI_REFERENCE_REA_GOOD1 ' and xtype= 'F ') ALTER TABLE dbo.Rea_BmsInDtl DROP CONSTRAINT FK_REA_BMSI_REFERENCE_REA_GOOD1;";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyBalanceDtl
                updateSql = " if exists(select 1 from sysobjects where name= 'FK_Rea_BmsQtyBalanceDtl_REFERENCE_Rea_Goods_GoodsUnitID ' and xtype= 'F ') ALTER TABLE dbo.Rea_BmsQtyBalanceDtl DROP CONSTRAINT FK_Rea_BmsQtyBalanceDtl_REFERENCE_Rea_Goods_GoodsUnitID; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtlOperation
                updateSql = " if exists(select 1 from sysobjects where name= 'FK_REA_BMSQ_REFERENCE_REA_GOODUnitID ' and xtype= 'F ') ALTER TABLE dbo.Rea_BmsQtyDtlOperation DROP CONSTRAINT FK_REA_BMSQ_REFERENCE_REA_GOODUnitID; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyMonthBalanceDtl
                updateSql = " if exists(select 1 from sysobjects where name= 'FK_REA_BMSQ_REFERENCE_Rea_GoodsUnit_GoodsUnitID ' and xtype= 'F ') ALTER TABLE dbo.Rea_BmsQtyMonthBalanceDtl DROP CONSTRAINT FK_REA_BMSQ_REFERENCE_Rea_GoodsUnit_GoodsUnitID; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsTransferDtl
                updateSql = " if exists(select 1 from sysobjects where name= 'FK_Rea_BmsTransferDtl_REFERENCE_REA_GoodsUnitID ' and xtype= 'F ') ALTER TABLE dbo.Rea_BmsTransferDtl DROP CONSTRAINT FK_Rea_BmsTransferDtl_REFERENCE_REA_GoodsUnitID; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_GoodsBarcodeOperation
                updateSql = " if exists(select 1 from sysobjects where name= 'FK_REA_GOOD_REFERENCE_REA_GoodsUnitID ' and xtype= 'F ') ALTER TABLE dbo.Rea_GoodsBarcodeOperation DROP CONSTRAINT FK_REA_GOOD_REFERENCE_REA_GoodsUnitID; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsOutDtl
                updateSql = " if exists(select 1 from sysobjects where name= 'FK_REA_BMSO_REFERENCE_REA_GOOD1 ' and xtype= 'F ') ALTER TABLE dbo.Rea_BmsOutDtl DROP CONSTRAINT FK_REA_BMSO_REFERENCE_REA_GOOD1; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.100");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.100) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.101
            if (IsUpdateDataBase(oldVersion, "1.0.0.101"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region RBAC_Role
                //更新各机构的机构管理员的所属LabId
                updateSql = " if exists(select * from RBAC_Role where RoleId=4845150561732500596) update RBAC_Role  set LabID=5705145215734838227 where DeveCode='sys_admin' and RoleId=4845150561732500596;";
                listSQL.Add(updateSql);

                //--柳州市妇幼保健院柳东医学遗传科
                updateSql = " if exists(select * from RBAC_Role where RoleId=5502190308454744802) update RBAC_Role  set LabID=5481198418450670240 where DeveCode='sys_admin' and roleId=5502190308454744802;";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from RBAC_Role where RoleId=5526880458797743518) update RBAC_Role  set LabID=5483869241004740331 where DeveCode='sys_admin' and roleId=5526880458797743518;";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from RBAC_Role where RoleId=5398411183082553244) update RBAC_Role  set LabID=5694271759057517871 where DeveCode='sys_admin' and roleId=5398411183082553244;";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from RBAC_Role where RoleId=5602643236777258910) update RBAC_Role  set LabID=5099949033311316768 where DeveCode='sys_admin' and roleId=5602643236777258910; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from RBAC_Role where RoleId=5599137722369718080) update RBAC_Role  set LabID=4658700808904551302 where DeveCode='sys_admin' and roleId=5599137722369718080; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from RBAC_Role where RoleId=5573758630490408559) update RBAC_Role  set LabID=4728647688158627225 where DeveCode='sys_admin' and roleId=5573758630490408559; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from RBAC_Role where RoleId=5207029858645361659) update RBAC_Role  set LabID=5309044096347053119 where DeveCode='sys_admin' and roleId=5207029858645361659; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from RBAC_Role where RoleId=5220932115754788207) update RBAC_Role  set LabID=4921721742224150860 where DeveCode='sys_admin' and roleId=5220932115754788207; ";
                listSQL.Add(updateSql);

                //柳州市妇幼保健院城中医学检验科
                updateSql = " if exists(select * from RBAC_Role where RoleId=5285230942163267606) update RBAC_Role  set LabID=5286936497429362744 where DeveCode='sys_admin' and roleId=5285230942163267606; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from RBAC_Role where RoleId=5268103670225384158) update RBAC_Role  set LabID=5607158249792076058 where DeveCode='sys_admin' and roleId=5268103670225384158; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from RBAC_Role where RoleId=5045138203418043802) update RBAC_Role  set LabID=5109987486252954529 where DeveCode='sys_admin' and roleId=5045138203418043802; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from RBAC_Role where RoleId=5070611860911712536) update RBAC_Role  set LabID=4951140216334387457 where DeveCode='sys_admin' and roleId=5070611860911712536; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from RBAC_Role where RoleId=5058097047709098922) update RBAC_Role  set LabID=4885932484095325799 where DeveCode='sys_admin' and roleId=5058097047709098922; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from RBAC_Role where RoleId=5156548178126305991) update RBAC_Role  set LabID=5479779158580669931 where DeveCode='sys_admin' and roleId=5156548178126305991; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from RBAC_Role where RoleId=4835872791544399133) update RBAC_Role  set LabID=5597953746529399158 where DeveCode='sys_admin' and roleId=4835872791544399133; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from RBAC_Role where RoleId=4996115074883936460) update RBAC_Role  set LabID=5617060756355161346 where DeveCode='sys_admin' and roleId=4996115074883936460; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from RBAC_Role where RoleId=4921023066816293337) update RBAC_Role  set LabID=5130022840951579996 where DeveCode='sys_admin' and roleId=4921023066816293337; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from RBAC_Role where RoleId=4690313492721380916) update RBAC_Role  set LabID=5395290755970322612 where DeveCode='sys_admin' and roleId=4690313492721380916; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from RBAC_Role where RoleId=4718563318137467312) update RBAC_Role  set LabID=5457660654853935258 where DeveCode='sys_admin' and roleId=4718563318137467312; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from RBAC_Role where RoleId=4727501818992897305) update RBAC_Role  set LabID=4901368926387639114 where DeveCode='sys_admin' and roleId=4727501818992897305; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from RBAC_Role where RoleId=4684328715300761880) update RBAC_Role  set LabID=5716166433310375975 where DeveCode='sys_admin' and roleId=4684328715300761880; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from RBAC_Role where RoleId=4780367284272901575) update RBAC_Role  set LabID=5000080113240723860 where DeveCode='sys_admin' and roleId=4780367284272901575; ";
                listSQL.Add(updateSql);

                //柳州市妇幼保健院柳东医学检验科
                updateSql = " if exists(select * from RBAC_Role where RoleId=5724882301664002508) update RBAC_Role  set LabID=5666944493267438695 where DeveCode='sys_admin' and roleId=5724882301664002508; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from RBAC_Role where RoleId=5728320785548789660) update RBAC_Role  set LabID=5271592900231106758 where DeveCode='sys_admin' and roleId=5728320785548789660; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from RBAC_Role where RoleId=5733876928674647554) update RBAC_Role  set LabID=4890542826010495731 where DeveCode='sys_admin' and roleId=5733876928674647554; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from RBAC_Role where RoleId=5654648271045630471) update RBAC_Role  set LabID=5552725764149552174 where DeveCode='sys_admin' and roleId=5654648271045630471; ";
                listSQL.Add(updateSql);

                updateSql = " if exists(select * from RBAC_Role where RoleId=5656600803653865103) update RBAC_Role  set LabID=5354406644642926051 where DeveCode='sys_admin' and roleId=5656600803653865103; ";
                listSQL.Add(updateSql);
                //删除各机构管理员角色存在labId=1的模块角色信息
                updateSql = " delete from dbo.RBAC_RoleModule where roleId=4845150561732500596 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=5502190308454744802 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=5526880458797743518 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=5398411183082553244 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=5602643236777258910 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=5599137722369718080 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=5573758630490408559 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=5207029858645361659 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=5220932115754788207 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=5207029858645361659 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=5285230942163267606 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=5268103670225384158 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=5045138203418043802 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=5070611860911712536 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=5058097047709098922 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=5156548178126305991 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=4835872791544399133 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=4996115074883936460 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=4921023066816293337 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=4690313492721380916 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=4718563318137467312 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=4727501818992897305 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=4684328715300761880 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=4780367284272901575 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=5724882301664002508 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=5728320785548789660 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=5733876928674647554 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=5654648271045630471 and labId=1; ";
                listSQL.Add(updateSql);

                updateSql = " delete from dbo.RBAC_RoleModule where roleId=5656600803653865103 and labId=1; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.101");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.101) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.102
            if (IsUpdateDataBase(oldVersion, "1.0.0.102"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_User_Storage_Link
                updateSql = " IF COL_LENGTH('Rea_User_Storage_Link', 'OperType') IS NULL ALTER TABLE Rea_User_Storage_Link ADD OperType bigint; ";
                listSQL.Add(updateSql);

                //默认将库房货架人员权限原关系类型更新为"员工库房管理类型"
                updateSql = " update Rea_User_Storage_Link set OperType=1 where (OperType is null or OperType=''); ";
                listSQL.Add(updateSql);

                updateSql = " update Rea_User_Storage_Link set CreaterID=null where CreaterID is not null and CreaterID=0; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_User_Storage_Link', 'OperName')  IS NOT NULL alter table Rea_User_Storage_Link alter column OperName varchar(50); ";
                listSQL.Add(updateSql);

                #endregion

                #region B_Parameter
                //大名县人民医院的运行参数"实验室数据升级版本"出现重复,需要删除其中一个
                updateSql = " delete from B_Parameter where ParameterID=4774961991688460509 and LabID=5309044096347053119; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.102");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.102) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.103
            //修复1.0.0.101升级更新机构管理员角色关联labId错误bug;
            if (IsUpdateDataBase(oldVersion, "1.0.0.103"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region RBAC_Role
                //柳州市妇幼保健院柳东医学遗传科
                updateSql = " if exists(select * from RBAC_Role where RoleId=5502190308454744802) update RBAC_Role  set LabID=5481198418450670240 where DeveCode='sys_admin' and roleId=5502190308454744802;";
                listSQL.Add(updateSql);

                //柳州市妇幼保健院城中医学检验科
                updateSql = " if exists(select * from RBAC_Role where RoleId=5285230942163267606) update RBAC_Role  set LabID=5286936497429362744 where DeveCode='sys_admin' and roleId=5285230942163267606; ";
                listSQL.Add(updateSql);

                //柳州市妇幼保健院柳东医学检验科
                updateSql = " if exists(select * from RBAC_Role where RoleId=5724882301664002508) update RBAC_Role  set LabID=5666944493267438695 where DeveCode='sys_admin' and roleId=5724882301664002508; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.103");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.103) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.104
            if (IsUpdateDataBase(oldVersion, "1.0.0.104"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_GoodsBarcodeOperation
                //验货入库的扫码货品ID值处理
                updateSql = " update Rea_GoodsBarcodeOperation set ScanCodeGoodsID=GoodsID where (ScanCodeGoodsID is null or ScanCodeGoodsID='') and OperTypeID=4; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.104");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.104) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.105
            if (IsUpdateDataBase(oldVersion, "1.0.0.105"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region B_Parameter
                //统一更新货品扫码设置的备注信息
                updateSql = " update B_Parameter set [ParaDesc]='设置仅对货品条码类型为盒条码生效;严格模式:1,混合模式:2' where  ParaType='CONFIG' and SName='货品扫码'; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_ExportExcelCommand
                //创建空数据表,供frx模板选择数据源使用
                updateSql = " if not exists(select 1 from sysobjects where id = object_id('Rea_ExportExcelCommand') and type = 'U') CREATE TABLE [dbo].[Rea_ExportExcelCommand]( [EEC_NowDate] [varchar](50) NULL, [EEC_StartDate] [varchar](50) NULL, [EEC_EndDate] [varchar](50) NULL, [EEC_EmployeeName] [varchar](50) NULL, [EEC_DeptName] [varchar](100) NULL, [EEC_LabName] [varchar](100) NULL) ON [PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsInDoc
                updateSql = " IF COL_LENGTH('Rea_BmsInDoc', 'UserName') IS NULL ALTER TABLE Rea_BmsInDoc ADD UserName varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDoc', 'DeptID') IS NULL ALTER TABLE Rea_BmsInDoc ADD DeptID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDoc', 'DeptName') IS NULL ALTER TABLE Rea_BmsInDoc ADD DeptName varchar(200); ";
                listSQL.Add(updateSql);

                updateSql = " update Rea_BmsInDoc set Rea_BmsInDoc.DeptID=HR_Employee.DeptID from Rea_BmsInDoc,HR_Employee where Rea_BmsInDoc.UserId=HR_Employee.EmpID and Rea_BmsInDoc.UserId is not null and Rea_BmsInDoc.DeptID is null; ";
                listSQL.Add(updateSql);

                updateSql = " update Rea_BmsInDoc set Rea_BmsInDoc.DeptName=HR_Dept.CName from Rea_BmsInDoc,HR_Dept where Rea_BmsInDoc.DeptID=HR_Dept.DeptID and Rea_BmsInDoc.DeptID is not null and Rea_BmsInDoc.DeptName is null; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.105");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.105) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.106
            if (IsUpdateDataBase(oldVersion, "1.0.0.106"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsQtyDtl
                //自动更新通过入库管理提取入库信息时,在进行入库确认时,入库信息对应的库存记录的入库单号丢失bug
                updateSql = " update dbo.Rea_BmsQtyDtl set InDocNo=Rea_BmsInDtl.InDocNo from Rea_BmsQtyDtl,Rea_BmsInDtl where Rea_BmsQtyDtl.InDtlID=Rea_BmsInDtl.InDtlID and Rea_BmsInDtl.InDocNo is not null and (Rea_BmsQtyDtl.InDocNo is null or Rea_BmsQtyDtl.InDocNo=''); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDtlConfirm
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'FactoryOutTemperature') IS NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ADD FactoryOutTemperature varchar(100);  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'ArrivalTemperature') IS NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ADD ArrivalTemperature varchar(100);  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'AppearanceAcceptance') IS NULL ALTER TABLE Rea_BmsCenSaleDtlConfirm ADD AppearanceAcceptance varchar(100);  ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsInDtl
                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'FactoryOutTemperature') IS NULL ALTER TABLE Rea_BmsInDtl ADD FactoryOutTemperature varchar(100);  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'ArrivalTemperature') IS NULL ALTER TABLE Rea_BmsInDtl ADD ArrivalTemperature varchar(100);  ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'AppearanceAcceptance') IS NULL ALTER TABLE Rea_BmsInDtl ADD AppearanceAcceptance varchar(100);  ";
                listSQL.Add(updateSql);

                #endregion


                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.106");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.106) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.107
            if (IsUpdateDataBase(oldVersion, "1.0.0.107"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsQtyBalanceDoc
                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDoc', 'PreQtyBalanceDocID') IS NULL ALTER TABLE Rea_BmsQtyBalanceDoc ADD PreQtyBalanceDocID bigint; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDoc', 'PreQtyBalanceDocNo') IS NULL ALTER TABLE Rea_BmsQtyBalanceDoc ADD PreQtyBalanceDocNo varchar(20); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDoc', 'PreBalanceDateTime') IS NULL ALTER TABLE Rea_BmsQtyBalanceDoc ADD PreBalanceDateTime datetime; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyBalanceDtl
                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDtl', 'PreGoodsQty') IS NULL ALTER TABLE Rea_BmsQtyBalanceDtl ADD PreGoodsQty float; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDtl', 'PreSumTotal') IS NULL ALTER TABLE Rea_BmsQtyBalanceDtl ADD PreSumTotal float; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDtl', 'ChangeGoodsQty') IS NULL ALTER TABLE Rea_BmsQtyBalanceDtl ADD ChangeGoodsQty float; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDtl', 'CalcGoodsQty') IS NULL ALTER TABLE Rea_BmsQtyBalanceDtl ADD CalcGoodsQty float; ";
                listSQL.Add(updateSql);
                #endregion


                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.107");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.107) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.108
            if (IsUpdateDataBase(oldVersion, "1.0.0.108"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_Storage_Goods_Link
                updateSql = " IF EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Rea_Storage_Goods_Link]') AND type in (N'U')) DROP TABLE [dbo].[Rea_Storage_Goods_Link]; CREATE TABLE [dbo].[Rea_Storage_Goods_Link]( [LabID] [bigint] NULL, [LinkID] [bigint] NOT NULL, [OperType] [bigint] NULL, [StorageID] [bigint] NULL, [StorageName] [varchar](100) NULL, [PlaceID] [bigint] NULL, [PlaceName] [varchar](100) NULL, [GoodsID] [bigint] NOT NULL, [DispOrder] [int] NULL, [Visible] [bit] NULL, [DataAddTime] [datetime] NULL, [DataUpdateTime] [datetime] NOT NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_REA_STORAGE_GOODS_LINK] PRIMARY KEY CLUSTERED ( [LinkID] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY];";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsCheckDoc
                updateSql = " IF COL_LENGTH('Rea_BmsCheckDoc', 'IsStorageGoodsLink') IS NULL ALTER TABLE Rea_BmsCheckDoc ADD IsStorageGoodsLink bit; ";
                listSQL.Add(updateSql);

                updateSql = " update Rea_BmsCheckDoc set IsStorageGoodsLink=0 where IsStorageGoodsLink is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtl
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'QtyDtlMark') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD QtyDtlMark bigint; ";
                listSQL.Add(updateSql);

                updateSql = " update Rea_BmsQtyDtl set QtyDtlMark=0 where QtyDtlMark is null; ";
                listSQL.Add(updateSql);
                #endregion


                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.108");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.108) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.109
            if (IsUpdateDataBase(oldVersion, "1.0.0.109"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsCheckDoc
                updateSql = " IF COL_LENGTH('Rea_BmsCheckDoc', 'IsStorageGoodsLink') IS NULL ALTER TABLE Rea_BmsCheckDoc ADD IsStorageGoodsLink bit; ";
                listSQL.Add(updateSql);

                updateSql = " update Rea_BmsCheckDoc set IsStorageGoodsLink=0 where IsStorageGoodsLink is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtl
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'QtyDtlMark') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD QtyDtlMark bigint; ";
                listSQL.Add(updateSql);

                updateSql = " update Rea_BmsQtyDtl set QtyDtlMark=0 where QtyDtlMark is null; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.109");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.109) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.110
            if (IsUpdateDataBase(oldVersion, "1.0.0.110"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_TestItem
                updateSql = " IF COL_LENGTH('Rea_TestItem', 'Memo') IS NOT NULL ALTER table Rea_TestItem ALTER column Memo VARCHAR(max); ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.110");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.110) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.111
            if (IsUpdateDataBase(oldVersion, "1.0.0.111"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsCheckDoc
                updateSql = " IF COL_LENGTH('Rea_BmsCheckDoc', 'IsHasZeroQty') IS NULL ALTER TABLE Rea_BmsCheckDoc ADD IsHasZeroQty bit; ";
                listSQL.Add(updateSql);

                updateSql = " update Rea_BmsCheckDoc set IsHasZeroQty=0 where IsHasZeroQty is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_CenBarCodeFormat
                //默认为二维条码规则
                updateSql = " update Rea_CenBarCodeFormat set BarCodeType=2 where BarCodeType is null or BarCodeType=''; ";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_Module
                //统计模块:出库-按库房
                updateSql = " if exists (select * from RBAC_Module where ModuleID=5226080549045182921) update  RBAC_Module set Url='#Shell.class.rea.client.statistics.echart.out.storage.App',CName='出库-按库房' where ModuleID=5226080549045182921; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.111");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.111) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.112
            if (IsUpdateDataBase(oldVersion, "1.0.0.112"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region B_Parameter
                //"订单审核通过同时直接订单上传"更新为"订单审核通过同时直接订单上传平台"
                updateSql = " update B_Parameter set Name='订单审核通过同时直接订单上传平台' where ParaNo='C-RBCO-CHEC-0018'; ";
                listSQL.Add(updateSql);

                //"订单是否上传"更新为"订单上传类型:(1:不上传;2:上传平台;3:上传第三方系统;4:上传平台及上传第三方系统;)"
                updateSql = " update B_Parameter set Name='订单上传类型',ParaDesc='1:不上传;2:上传平台;3:上传第三方系统;4:上传平台及上传第三方系统(预留,未实现);' where ParaNo='C-RBCO-UPLO-0011'; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.112");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.112) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.113
            if (IsUpdateDataBase(oldVersion, "1.0.0.113"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region RBAC_Module
                //更正1.0.0.111版本更新模块错误
                updateSql = " if exists (select * from RBAC_Module where ModuleID=5226080549045182921) update  RBAC_Module set Url='',CName='统计' where ModuleID=5226080549045182921; ";
                listSQL.Add(updateSql);

                //统计模块:出库-按库房
                updateSql = " if exists (select * from RBAC_Module where ModuleID=4824818493607374259) update  RBAC_Module set Url='#Shell.class.rea.client.statistics.echart.out.storage.App',CName='出库-按库房' where ModuleID=4824818493607374259; ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.113");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.113) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.114
            if (IsUpdateDataBase(oldVersion, "1.0.0.114"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsQtyDtl
                //通过升级脚本自动更新库存明细记录的金额
                updateSql = " update Rea_BmsQtyDtl set SumTotal=Price*GoodsQty where SumTotal!=Price*GoodsQty; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsQtyDtlOperation
                //通过升级脚本自动更新库存操作记录的金额
                updateSql = " update Rea_BmsQtyDtlOperation set SumTotal=Price*GoodsQty where SumTotal!=Price*GoodsQty; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsTransferDtl
                //通过升级脚本自动更新移库明细记录的金额
                updateSql = " update Rea_BmsTransferDtl set SumTotal=Price*GoodsQty where SumTotal!=Price*GoodsQty; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.114");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.114) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.115
            if (IsUpdateDataBase(oldVersion, "1.0.0.115"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region B_LodopTemplet
                updateSql = "  IF NOT EXISTS(SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[B_LodopTemplet]') AND type in (N'U')) CREATE TABLE [dbo].[B_LodopTemplet]( [LabID] [bigint] NOT NULL, [LTId] [bigint] NOT NULL, [CName] [varchar](40) NULL, [TypeCode] [varchar](50) NULL, [TypeCName] [varchar](50) NULL, [PaperType] [varchar](50) NULL, [PrintingDirection] [varchar](50) NULL, [PaperWidth] [float] NULL, [PaperHigh] [float] NULL, [PaperUnit] [varchar](50) NULL, [TemplateCode] [ntext] NULL, [DataTestValue] [ntext] NULL, [DispOrder] [int] NULL, [Memo] [ntext] NULL, [IsUse] [bit] NULL, [DataAddTime] [datetime] NULL, [DataTimeStamp] [timestamp] NULL, CONSTRAINT [PK_B_LODOPTEMPLET] PRIMARY KEY CLUSTERED ( [LTId] ASC)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]; ";
                listSQL.Add(updateSql);
                #endregion

                #region RBAC_Module
                updateSql = " update  RBAC_Module set Comment='用户可配置的运行参数' where ModuleID=5309255703515892110; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.115");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.115) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.116
            if (IsUpdateDataBase(oldVersion, "1.0.0.116"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_Goods
                updateSql = " IF COL_LENGTH('Rea_Goods', 'UnitName') IS NOT NULL ALTER table Rea_Goods ALTER column UnitName VARCHAR(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsReqDtl
                updateSql = " IF COL_LENGTH('Rea_BmsReqDtl', 'GoodsUnit') IS NOT NULL ALTER table Rea_BmsReqDtl ALTER column GoodsUnit VARCHAR(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenOrderDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDtl', 'GoodsUnit') IS NOT NULL ALTER table Rea_BmsCenOrderDtl ALTER column GoodsUnit VARCHAR(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtl', 'GoodsUnit') IS NOT NULL ALTER table Rea_BmsCenSaleDtl ALTER column GoodsUnit VARCHAR(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDtlConfirm
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDtlConfirm', 'GoodsUnit') IS NOT NULL ALTER table Rea_BmsCenSaleDtlConfirm ALTER column GoodsUnit VARCHAR(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_GoodsBarcodeOperation
                updateSql = " IF COL_LENGTH('Rea_GoodsBarcodeOperation', 'GoodsUnit') IS NOT NULL ALTER table Rea_GoodsBarcodeOperation ALTER column GoodsUnit VARCHAR(100); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsInDtl
                updateSql = " IF COL_LENGTH('Rea_BmsInDtl', 'GoodsUnit') IS NOT NULL ALTER table Rea_BmsInDtl ALTER column GoodsUnit VARCHAR(100); ";
                listSQL.Add(updateSql);
                #endregion
                #region Rea_BmsQtyDtl
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'GoodsUnit') IS NOT NULL ALTER table Rea_BmsQtyDtl ALTER column GoodsUnit VARCHAR(100); ";
                listSQL.Add(updateSql);
                #endregion
                #region Rea_BmsQtyBalanceDtl
                updateSql = " IF COL_LENGTH('Rea_BmsQtyBalanceDtl', 'GoodsUnit') IS NOT NULL ALTER table Rea_BmsQtyBalanceDtl ALTER column GoodsUnit VARCHAR(100); ";
                listSQL.Add(updateSql);

                #endregion
                #region Rea_BmsTransferDtl
                updateSql = " IF COL_LENGTH('Rea_BmsTransferDtl', 'GoodsUnit') IS NOT NULL ALTER table Rea_BmsTransferDtl ALTER column GoodsUnit VARCHAR(100); ";
                listSQL.Add(updateSql);
                #endregion
                #region Rea_BmsOutDtl
                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'GoodsUnit') IS NOT NULL ALTER table Rea_BmsOutDtl ALTER column GoodsUnit VARCHAR(100); ";
                listSQL.Add(updateSql);
                #endregion
                #region Rea_BmsCheckDtl
                updateSql = " IF COL_LENGTH('Rea_BmsCheckDtl', 'GoodsUnit') IS NOT NULL ALTER table Rea_BmsCheckDtl ALTER column GoodsUnit VARCHAR(100); ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.116");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.115) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.117
            if (IsUpdateDataBase(oldVersion, "1.0.0.117"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region ReaBmsCenOrderDoc
                updateSql = " IF EXISTS (SELECT * FROM information_schema.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'FK_REA_BMSC_REFERENCE_REA_CENO') ALTER TABLE [dbo].[Rea_BmsCenOrderDoc] DROP CONSTRAINT [FK_REA_BMSC_REFERENCE_REA_CENO]; ";
                listSQL.Add(updateSql);

                #endregion

                #region BmsCenOrderDtl
                updateSql = " IF EXISTS (SELECT * FROM information_schema.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'FK_Rea_BmsCenOrderDtl_Rea_GoodsOrgLink_LabcGoodsLinkID') ALTER TABLE [dbo].[Rea_BmsCenOrderDtl] DROP CONSTRAINT [FK_Rea_BmsCenOrderDtl_Rea_GoodsOrgLink_LabcGoodsLinkID]; ";
                listSQL.Add(updateSql);

                updateSql = " IF EXISTS (SELECT * FROM information_schema.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'FK_REA_BMSC_REFERENCE_REA_GOOD1') ALTER TABLE [dbo].[Rea_BmsCenOrderDtl] DROP CONSTRAINT [FK_REA_BMSC_REFERENCE_REA_GOOD1]; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsReqDtl
                updateSql = " IF EXISTS (SELECT * FROM information_schema.KEY_COLUMN_USAGE WHERE CONSTRAINT_NAME = 'FK_Rea_BmsReqDtl_REFERENCE_OrderDtlID') ALTER TABLE [dbo].[Rea_BmsReqDtl] DROP CONSTRAINT [FK_Rea_BmsReqDtl_REFERENCE_OrderDtlID]; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.117");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.117) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.118
            if (IsUpdateDataBase(oldVersion, "1.0.0.118"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_Goods
                updateSql = " alter table Rea_Goods alter column StorageType nvarchar(1000); ";
                listSQL.Add(updateSql);

                updateSql = " alter table Rea_Goods alter column Constitute nvarchar(1000); ";
                listSQL.Add(updateSql);

                updateSql = " alter table Rea_Goods alter column Purpose nvarchar(1000); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDtl
                updateSql = " alter table Rea_BmsCenSaleDtl alter column StorageType nvarchar(1000); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenSaleDtlConfirm
                updateSql = " alter table Rea_BmsCenSaleDtlConfirm alter column StorageType nvarchar(1000); ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.118");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.118) Update Error, Please Check The Log!");
            }

            #endregion

            #region 1.0.0.119
            if (IsUpdateDataBase(oldVersion, "1.0.0.119"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_Goods
                updateSql = " IF COL_LENGTH('Rea_Goods', 'NetGoodsNo') IS NULL ALTER TABLE Rea_Goods add NetGoodsNo nvarchar(100); ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.119");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.119) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.120
            if (IsUpdateDataBase(oldVersion, "1.0.0.120"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsCenOrderDoc
                updateSql = " update Rea_BmsCenOrderDoc set DeleteFlag = 0 where DeleteFlag is null; ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_GoodsLot
                updateSql = " IF COL_LENGTH('Rea_GoodsLot', 'IncreaseAppearance') IS NULL ALTER TABLE Rea_GoodsLot add IncreaseAppearance varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsLot', 'SterilityTest') IS NULL ALTER TABLE Rea_GoodsLot add SterilityTest varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsLot', 'ParallelTest') IS NULL ALTER TABLE Rea_GoodsLot add ParallelTest varchar(50); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_GoodsLot', 'GrowthTest') IS NULL ALTER TABLE Rea_GoodsLot add GrowthTest varchar(50); ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_Goods
                updateSql = " IF COL_LENGTH('Rea_Goods', 'VerificationMemo') IS NULL ALTER TABLE Rea_Goods add VerificationMemo varchar(5000); ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.120");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.120) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.121
            if (IsUpdateDataBase(oldVersion, "1.0.0.121"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_Goods
                updateSql = " IF COL_LENGTH('Rea_Goods', 'VerificationMemo') IS NULL ALTER TABLE Rea_Goods add VerificationMemo varchar(5000); ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.121");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.121) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.122
            if (IsUpdateDataBase(oldVersion, "1.0.0.122"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region B_Parameter
                updateSql = " IF COL_LENGTH('B_Parameter', 'ItemEditInfo') IS NULL ALTER TABLE B_Parameter ADD ItemEditInfo ntext; ";
                listSQL.Add(updateSql);

                //订单审核通过同时直接订单上传
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"2\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''是''},{''2'':''否''}]\"}' where ParaNo='C-RBCO-CHEC-0018' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //数据库是否独立部署
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"2\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''是''},{''2'':''否''}]\"}' where ParaNo='C-DATA-ISDI-0017' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //启用用户UI配置
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"2\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''是''},{''2'':''否''}]\"}' where ParaNo='C-EUSE-UICF-0035' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //启用库存报警
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"0\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''0'':''不启用''},{''1'':''启用''}]\"}' where ParaNo='C-RBQD-BQIW-0001' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //启用效期报警
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"0\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''0'':''不启用''},{''1'':''启用''}]\"}' where ParaNo='C-RBQD-BQEW-0002' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //验收货品扫码
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"2\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''严格模式''},{''2'':''混合模式''}]\"}' where ParaNo='C-RBDC-GASC-0004' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //入库货品扫码
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"2\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''严格模式''},{''2'':''混合模式''}]\"}' where ParaNo='C-RBID-GISC-0005' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //出库货品扫码
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"2\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''严格模式''},{''2'':''混合模式''}]\"}' where ParaNo='C-RBOD-GOSC-0006' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //移库货品扫码
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"2\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''严格模式''},{''2'':''混合模式''}]\"}' where ParaNo='C-RBTD-GTSC-0007' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //盘库货品扫码
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"2\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''严格模式''},{''2'':''混合模式''}]\"}' where ParaNo='C-RBPD-GCSC-0008' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //验收双确认方式
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"1\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''本实验室''},{''2'':''本实验室双人确认''}]\"}' where ParaNo='C-RBSC-SAAC-0010' and ItemEditInfo is null;";
                listSQL.Add(updateSql);


                //出库领用人是否为登录人
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"2\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''否''},{''2'':''是''}]\"}' where ParaNo='C-RBOD-OBIL-0012' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //盘库审核是否需要确认
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"1\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''不需要''},{''2'':''需要''}]\"}' where ParaNo='C-RBCD-ISCH-0013' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //使用出库审核是否需要确认
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"0\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''0'':''不需要''},{''1'':''需要''}]\"}' where ParaNo='C-RBCD-ISCH-0014' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //报损出库审核是否需要确认
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"0\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''0'':''不需要''},{''1'':''需要''}]\"}' where ParaNo='C-RBCD-ISCH-0015' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //退供应商出库审核是否需要确认
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"0\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''0'':''不需要''},{''1'':''需要''}]\"}' where ParaNo='C-RBCD-ISCH-0016' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //供应商确认订单时是否需要强制校验货品编码
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"2\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''是''},{''2'':''否''}]\"}' where ParaNo='C-RBCO-COFM-0019' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //使用出库仪器是否必填
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"2\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''是''},{''2'':''否''}]\"}' where ParaNo='C-RBCD-ISCH-0021' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //是否按出库人权限出库
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"2\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''是''},{''2'':''否''}]\"}' where ParaNo='C-RBCD-ISCH-0023' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //启用注册证预警
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"0\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''0'':''不启用''},{''1'':''启用''}]\"}' where ParaNo='C-RRWW-ISWA-0025' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //移库审核是否需要确认
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"0\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''0'':''不需要''},{''1'':''需要''}]\"}' where ParaNo='C-RTCD-ISCH-0026' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //是否按移库人权限移库
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"2\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''是''},{''2'':''否''}]\"}' where ParaNo='C-RBTD-ISEO-0027' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //出库确认后是否调用退库接口
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"2\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''是''},{''2'':''否''}]\"}' where ParaNo='C-RBOD-ISRI-0029' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //是否启用库存库房权限
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"2\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''是''},{''2'':''否''}]\"}' where ParaNo='C-RBQT-ISUE-0031' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //库存货品是否需要性能验证后才能使用出库
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"2\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''是''},{''2'':''否''}]\"}' where ParaNo='C-RBOD-ISPV-0032' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //是否需要支持直接出库
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"1\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''是(新增出库按钮显示)''},{''2'':''否(新增出库按钮隐藏)''}]\"}' where ParaNo='C-RBOD-ISDO-0036' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //接口数据是否需要重新生成条码
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"1\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''是''},{''2'':''否''}]\"}' where ParaNo='C-WTID-NTRB-0037' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //移库或出库扫码是否允许从所有库房获取库存货品
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"2\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''是''},{''2'':''否''}]\"}' where ParaNo='C-TOBC-ISAL-0038' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //盘库时实盘数是否取库存数
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"2\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''是''},{''2'':''否''}]\"}' where ParaNo='C-IVTY-ISTQ-0039' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //是否开启近效期
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"1\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''开启''},{''2'':''不开启''},{''3'':''界面选择默认开启''},{''4'':''界面选择默认不开启''}]\"}' where ParaNo='C-RBOU-ISON-0040' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //是否强制近效期出库
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"4\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''强制''},{''2'':''不强制''},{''3'':''界面选择默认强制''},{''4'':''界面选择默认不强制''}]\"}' where ParaNo='C-RBOU-ISNP-0041' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //列表默认分页记录数
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"20\",\"ItemUnit\":\"\",\"ItemDataSet\":\"\"}' where ParaNo='C-LRMP-UIPA-0030' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //订单审批金额
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"10000000\",\"ItemUnit\":\"\",\"ItemDataSet\":\"\"}' where ParaNo='C-RBOD-APPR-0028' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //访问BS平台的URL
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"textfield\",\"ItemDefaultValue\":\"\",\"ItemUnit\":\"\",\"ItemDataSet\":\"\"}' where ParaNo='C-BSPL-PURL-0009' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //效期预警默认已过期天数
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"500\",\"ItemUnit\":\"\",\"ItemDataSet\":\"\"}' where ParaNo='C-EAWE-DAYS-0033' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //注册证预警默认已过期天数
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"1\",\"ItemUnit\":\"\",\"ItemDataSet\":\"\"}' where ParaNo='C-REWE-DDAY-0034' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //货品效期预警天数
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"30\",\"ItemUnit\":\"\",\"ItemDataSet\":\"\"}' where ParaNo='C-RBQD-GVWD-0003' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //注册证将过期预警天数
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"numberfield\",\"ItemDefaultValue\":\"30\",\"ItemUnit\":\"\",\"ItemDataSet\":\"\"}' where ParaNo='C-RRWW-WAEN-0024' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //实验室数据升级版本
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"textfield\",\"ItemDefaultValue\":\"1.0.0.1\",\"ItemUnit\":\"\",\"ItemDataSet\":\"\"}' where ParaNo='C-ULAB-DATA-0022' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                //订单上传类型
                updateSql = " update B_Parameter set ItemEditInfo=N'{\"ItemXType\":\"radiogroup\",\"ItemDefaultValue\":\"1\",\"ItemUnit\":\"\",\"ItemDataSet\":\"[{''1'':''不上传''},{''2'':''上传平台''},{''3'':''上传第三方系统''},{''4'':''上传平台及上传第三方系统''}]\"}' where ParaNo='C-RBCO-UPLO-0011' and ItemEditInfo is null;";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsOutDoc
                updateSql = " update Rea_BmsOutDoc set DataAddTime=DataUpdateTime where DataAddTime is null and DataUpdateTime is not null; ";
                listSQL.Add(updateSql);

                updateSql = " update Rea_BmsOutDoc set DataAddTime=OperDate where DataAddTime is null and OperDate is not null; ";
                listSQL.Add(updateSql);
                #endregion
                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.122");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.122) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.123
            if (IsUpdateDataBase(oldVersion, "1.0.0.123"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_ChooseGoodsTemplate
                updateSql = " IF COL_LENGTH('Rea_ChooseGoodsTemplate', 'ContextJson') IS NOT NULL ALTER TABLE Rea_ChooseGoodsTemplate ALTER column ContextJson text; ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsOutDoc
                updateSql = " update Rea_BmsOutDoc set DataAddTime=DataUpdateTime where DataAddTime is null and DataUpdateTime is not null; ";
                listSQL.Add(updateSql);

                updateSql = " update Rea_BmsOutDoc set DataAddTime=OperDate where DataAddTime is null and OperDate is not null; ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.123");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.123) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.124
            if (IsUpdateDataBase(oldVersion, "1.0.0.124"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsInDoc
                updateSql = " IF COL_LENGTH('Rea_BmsInDoc', 'IOFlag') IS NULL ALTER TABLE Rea_BmsInDoc ADD IOFlag  int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsInDoc', 'IOMemo') IS NULL ALTER TABLE Rea_BmsInDoc ADD IOMemo  varchar(5000); ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_BmsOutDoc
                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'IOFlag') IS NULL ALTER TABLE Rea_BmsOutDoc ADD IOFlag  int; ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'IOMemo') IS NULL ALTER TABLE Rea_BmsOutDoc ADD IOMemo  varchar(5000); ";
                listSQL.Add(updateSql);

                #endregion

                #region Rea_GoodsLot
                updateSql = " IF COL_LENGTH('Rea_GoodsLot', 'ComparisonTest') IS NULL ALTER TABLE Rea_GoodsLot ADD ComparisonTest  varchar(50); ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.124");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.124) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.125
            if (IsUpdateDataBase(oldVersion, "1.0.0.125"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_GoodsLot
                updateSql = " IF COL_LENGTH('Rea_GoodsLot', 'ComparisonTest') IS NULL ALTER TABLE Rea_GoodsLot ADD ComparisonTest  varchar(50); ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.125");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.125) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.126
            if (IsUpdateDataBase(oldVersion, "1.0.0.126"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsCenSaleDocConfirm
                updateSql = " IF COL_LENGTH('Rea_BmsCenSaleDocConfirm', 'TransportNo') IS NULL ALTER TABLE Rea_BmsCenSaleDocConfirm ADD TransportNo  varchar(300); ";
                listSQL.Add(updateSql);

                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.126");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.126) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.127
            if (IsUpdateDataBase(oldVersion, "1.0.0.127"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsQtyDtl 库存表
                updateSql = " IF COL_LENGTH('Rea_BmsQtyDtl', 'TransportNo') IS NULL ALTER TABLE Rea_BmsQtyDtl ADD TransportNo  varchar(300); ";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsOutDtl 出库明细表
                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'TransportNo') IS NULL ALTER TABLE Rea_BmsOutDtl ADD TransportNo  varchar(300); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'LastLotNo') IS NULL ALTER TABLE Rea_BmsOutDtl ADD LastLotNo  varchar(100); ";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDtl', 'LastTransportNo') IS NULL ALTER TABLE Rea_BmsOutDtl ADD LastTransportNo  varchar(300); ";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.127");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.127) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.128
            if (IsUpdateDataBase(oldVersion, "1.0.0.128"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_BmsCenOrderDoc 订货单表
                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDoc', 'SupplyStatus') IS NULL ALTER TABLE Rea_BmsCenOrderDoc ADD SupplyStatus int;";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_BmsCenOrderDtl 订货明细表
                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDtl', 'SuppliedQty') IS NULL ALTER TABLE Rea_BmsCenOrderDtl ADD SuppliedQty float;";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsCenOrderDtl', 'UnSupplyQty') IS NULL ALTER TABLE Rea_BmsCenOrderDtl ADD UnSupplyQty float;";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.128");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.128) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.129
            if (IsUpdateDataBase(oldVersion, "1.0.0.129"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region B_Dict 字典表
                updateSql = " IF COL_LENGTH('B_Dict', 'MatchCode') IS NULL ALTER TABLE B_Dict ADD MatchCode varchar(100);";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.129");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.129) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.130
            if (IsUpdateDataBase(oldVersion, "1.0.0.130"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region Rea_CenOrg_本地机构表
                updateSql = " IF COL_LENGTH('Rea_CenOrg', 'NextBillType') IS NULL ALTER TABLE Rea_CenOrg ADD NextBillType int;";
                listSQL.Add(updateSql);
                #endregion

                #region Rea_Goods_本地货品表
                updateSql = " IF COL_LENGTH('Rea_Goods', 'IsMed') IS NULL ALTER TABLE Rea_Goods ADD IsMed bit;";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_Goods', 'InvType') IS NULL ALTER TABLE Rea_Goods ADD InvType varchar(100);";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_Goods', 'TS') IS NULL ALTER TABLE Rea_Goods ADD TS datetime;";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.130");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.130) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.131
            if (IsUpdateDataBase(oldVersion, "1.0.0.131"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();
                
                #region Rea_BmsOutDoc_出库总单表
                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'LabcName') IS NULL ALTER TABLE Rea_BmsOutDoc ADD LabcName varchar(100);";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'ReaServerLabcCode') IS NULL ALTER TABLE Rea_BmsOutDoc ADD ReaServerLabcCode varchar(200);";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'LabOutDocID') IS NULL ALTER TABLE Rea_BmsOutDoc ADD LabOutDocID bigint;";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'IsThirdFlag') IS NULL ALTER TABLE Rea_BmsOutDoc ADD IsThirdFlag int;";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'SupplierConfirmID') IS NULL ALTER TABLE Rea_BmsOutDoc ADD SupplierConfirmID bigint;";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'SupplierConfirmName') IS NULL ALTER TABLE Rea_BmsOutDoc ADD SupplierConfirmName varchar(100);";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'SupplierConfirmTime') IS NULL ALTER TABLE Rea_BmsOutDoc ADD SupplierConfirmTime datetime;";
                listSQL.Add(updateSql);

                updateSql = " IF COL_LENGTH('Rea_BmsOutDoc', 'SupplierConfirmMemo') IS NULL ALTER TABLE Rea_BmsOutDoc ADD SupplierConfirmMemo varchar(500);";
                listSQL.Add(updateSql);
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.131");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.131) Update Error, Please Check The Log!");
            }
            #endregion

            #region 1.0.0.132
            if (IsUpdateDataBase(oldVersion, "1.0.0.132"))
            {
                string updateSql = "";
                List<string> listSQL = new List<string>();

                #region 去掉表Rea_DeptGoods的外键FK_REA_DEPT_REFERENCE_HR_DEPT，因为要支持读取LIIP相关
                updateSql = "IF OBJECT_ID('dbo.[FK_REA_DEPT_REFERENCE_HR_DEPT]', 'C') IS NOT NULL ALTER TABLE dbo.[Rea_DeptGoods] DROP CONSTRAINT FK_REA_DEPT_REFERENCE_HR_DEPT;";
                listSQL.Add(updateSql);
                
                #endregion

                result = ExecuteUpdateSQL(listSQL);
                if (result)
                    result = UpateCompareVersionInfo("1.0.0.132");
                else
                    ZhiFang.Common.Log.Log.Error("DataBase(1.0.0.132) Update Error, Please Check The Log!");
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
