using System;
using System.Data;
using System.ServiceModel.Activation;
using ZhiFang.Entity.Base;
using ZhiFang.LabStar.DAO.ADO;
using ZhiFang.LabStar.TechnicianStation.WebService;

namespace ZhiFang.LabStar.TechnicianStation.ServerWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class MsgManageService : IMsgManageService
    {
        public BaseResultDataValue TestMsgSend(string msg)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            MsgService msgService = new MsgService();
            msgService.ClientRequest(msg);
            return brdv;
        }

        public BaseResultDataValue MsgAccept(string msg)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                MsgService msgService = new MsgService();
                baseResultDataValue.ResultDataValue = msgService.ClientRequest(msg);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "消息接收错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue Msg_GetLisDictInfo(string dictName, string strWhere)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue.ResultDataValue = GetLisDictInfo(dictName, strWhere);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue Msg_GetUserInfoByPWD(string userName, string userPWD)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue.ResultDataValue = GetUserInfoByPWD(userName, userPWD);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue Msg_GetNPUserInfoByPWD(string userName, string userPWD)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue.ResultDataValue = GetNPUserInfoByPWD(userName, userPWD);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue Msg_CovertPassword(string userPWD)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue.ResultDataValue = CovertPassword(userPWD);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue Msg_UnCovertPassword(string userPWD)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue.ResultDataValue = UnCovertPassword(userPWD);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue Msg_UnCovertPasswordCheck(string userPWD)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue.ResultDataValue = UnCovertPasswordCheck(userPWD);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        private string GetLisDictInfo(string dictName, string strWhere)
        {
            string strJson = "";
            string strSQL = "";
            switch (dictName.ToLower())
            {
                case "doctor":
                    strSQL = "select DoctorNo as Id,CName,ShortCode as  LisCode,HisOrderCode as  HisCode,0 as  DispOrder from Doctor where 1=1 ";
                    break;
                case "department":
                    //strSQL = "select DeptNo as Id,CName,ShortCode as  LisCode,HisOrderCode as  HisCode,DispOrder from Department where 1=1 ";
                    //strSQL = "select DeptNo as Id,CName,DeptNo as  LisCode,case when isnull(code_1,'')='' then code_2 else code_1 end as HisCode,DispOrder from Department where 1=1 ";
                    strSQL = "select DeptNo as Id,CName,DeptNo as  LisCode,code_1 as HisCode,DispOrder from Department where 1=1 ";
                    break;
                case "puser":
                    strSQL = "select UserNo as Id,CName,ShortCode as  LisCode,HisOrderCode as  HisCode,DispOrder from PUser where 1=1 ";
                    break;
                case "npuser":
                    strSQL = "select * from NPUser where 1=1 ";
                    break;
                case "pgroup":
                    strSQL = "select SectionNo as Id,CName,ShortCode as  LisCode,\'\' as  HisCode,DispOrder from PGroup where 1=1 ";
                    break;
                case "sicktype":
                    strSQL = "select SickTypeNo as Id,CName,ShortCode as  LisCode,HisOrderCode as  HisCode,DispOrder from SickType where 1=1 ";
                    break;
                    //case "doctor":
                    //    strSQL = "select DoctorNo as LisDict_Id, CName as LisDict_CName,ShortCode as  LisDict_LisCode,HisOrderCode as  LisDict_HisCode,0 as  LisDict_DispOrder from Doctor where 1=1 " + strWhere;
                    //    break;
                    //case "department":
                    //    strSQL = "select DeptNo as LisDict_Id, CName as LisDict_CName,ShortCode as  LisDict_LisCode,HisOrderCode as  LisDict_HisCode, DispOrder as LisDict_DispOrder from Department where 1=1 " + strWhere;
                    //    break;
                    //case "puser":
                    //    strSQL = "select UserNo as LisDict_Id, CName as LisDict_CName,ShortCode as  LisDict_LisCode,HisOrderCode as  LisDict_HisCode, DispOrder as LisDict_DispOrder from PUser where 1=1 " + strWhere;
                    //    break;
                    //case "pgroup":
                    //    strSQL = "select SectionNo as LisDict_Id, CName as LisDict_CName,ShortCode as  LisDict_LisCode,\'\' as  LisDict_HisCode, DispOrder as LisDict_DispOrder from PGroup where 1=1 " + strWhere;
                    //    break;
                    //case "sicktype":
                    //    strSQL = "select SickTypeNo as LisDict_Id, CName as LisDict_CName,ShortCode as  LisDict_LisCode,HisOrderCode as  LisDict_HisCode, DispOrder as LisDict_DispOrder from SickType where 1=1 " + strWhere;
                    //    break;
            }
            if (strWhere != null && strWhere != "")
            {
                strSQL += " and " + strWhere;
            }
            DataSet ds = SqlServerHelper.QuerySql(strSQL, SqlServerHelper.DigitlabConnectStr);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
            {
                strJson = "{count:" + ds.Tables[0].Rows.Count + ",list:" + ZhiFang.Common.Public.JsonHelp.DataTableToJson(ds.Tables[0]) + "}";
            }
            return strJson;
        }

        private string GetFieldValueByName(string tableName, string fieldName, string whereFieldName, string whereFieldValue)
        {
            string strResult = "";
            if ((!string.IsNullOrEmpty(tableName)) && (!string.IsNullOrEmpty(fieldName)) && (!string.IsNullOrEmpty(whereFieldName)) && (!string.IsNullOrEmpty(whereFieldValue)))
            {
                string sql = "select " + fieldName + " from " + tableName +
                    " where " + whereFieldName + "=\'" + whereFieldValue + "\'";
                DataSet ds = SqlServerHelper.QuerySql(sql, SqlServerHelper.DigitlabConnectStr);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    strResult = ds.Tables[0].Rows[0][0].ToString();
                }
            }
            return strResult;
        }

        private string GetUserInfoByPWD(string userName, string userPWD)
        {
            string strJson = "";
            if (userName != null && userName.Trim().Length > 0)
            {
                string tempPWD = CovertPassword(userPWD);
                string strSQL = "select * from PUser where ShortCode=\'" + userName + "\' and Password=\'" + tempPWD + "\'";
                DataSet ds = SqlServerHelper.QuerySql(strSQL, SqlServerHelper.DigitlabConnectStr);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    ds.Tables[0].Columns.Remove("userimage");
                    if (ds.Tables[0].Columns.Contains("UserDataRights"))
                    {
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            string jsonStr = dr["UserDataRights"] != null ? dr["UserDataRights"].ToString() : "";
                            if (jsonStr != "")
                                dr["UserDataRights"] = jsonStr.Replace("\"", "\\\"");
                        }
                    }
                    strJson = "{count:" + ds.Tables[0].Rows.Count + ",list:" + ZhiFang.Common.Public.JsonHelp.DataTableToJson(ds.Tables[0]) + "}";
                }
            }
            return strJson;
        }

        private string GetNPUserInfoByPWD(string userName, string userPWD)
        {
            string strJson = "";
            if (userName != null && userName.Trim().Length > 0)
            {
                string tempPWD = CovertPassword(userPWD);
                string strSQL = "select * from NPUser where ShortCode=\'" + userName + "\' and Password=\'" + tempPWD + "\'";
                DataSet ds = SqlServerHelper.QuerySql(strSQL, SqlServerHelper.DigitlabConnectStr);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    strJson = "{count:" + ds.Tables[0].Rows.Count + ",list:" + ZhiFang.Common.Public.JsonHelp.DataTableToJson(ds.Tables[0]) + "}";
                }
            }
            return strJson;
        }

        #region 密码加密解密（delphi版本）
        //Delphi技师站密码加密转换C#版本
        private string CovertPassword(string strPWD)
        {
            string result = "";
            if (strPWD == null || strPWD.Length == 0)
                return "=";
            int lengthPWD = strPWD.Length;
            int quotient = lengthPWD / 3; //商 取整
            int remainder = lengthPWD % 3; //余数
            while (quotient > 0)
            {
                result = Get64Str(strPWD, lengthPWD, 3) + result;
                lengthPWD = lengthPWD - 3;
                quotient--;
            }
            switch (remainder)
            {
                case 1:
                    result = Get64Str(strPWD, lengthPWD, 1) + result;
                    break;
                case 2:
                    result = Get64Str(strPWD, lengthPWD, 2) + result;
                    break;
            }
            return result;
        }

        private string UnCovertPassword(string strEncrypt)
        {
            string result = "";
            if (strEncrypt == null || strEncrypt.Length == 0)
                return result;
            int lengthEncrypt = strEncrypt.Length;
            int quotient = lengthEncrypt / 4; //商 取整
            int remainder = lengthEncrypt % 4; //余数
            while (quotient > 0)
            {
                result = Get256Str(strEncrypt, lengthEncrypt, 4) + result;
                lengthEncrypt = lengthEncrypt - 4;
                quotient--;
            }
            switch (remainder)
            {
                case 1:
                    result = Get256Str(strEncrypt, lengthEncrypt, 1) + result;
                    break;
                case 2:
                    result = Get256Str(strEncrypt, lengthEncrypt, 2) + result;
                    break;
                case 3:
                    result = Get256Str(strEncrypt, lengthEncrypt, 3) + result;
                    break;
            }
            return result;
        }

        private string UnCovertPasswordCheck(string strEncrypt)
        {
            string tempEncrypt = UnCovertPassword(strEncrypt);
            for (int i = 0; i < tempEncrypt.Length; i++)
            {
                char charPWD = tempEncrypt[i];
                int charASCII = (int)charPWD;
                if (charASCII < 32 || charASCII > 127)
                    return strEncrypt;
            }
            return tempEncrypt;
        }

        private string Get64Str(string strPWD, int lengthPWD, int num)
        {
            const string strConst = "=qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890+";
            string result = "";
            int count64 = 0;
            for (int i = 0; i < num; i++)
            {
                char charPWD = strPWD[lengthPWD - num + i];
                int charASCII = (int)charPWD;
                count64 = count64 * 256 + charASCII;
            }
            for (int i = 0; i < 4; i++)
            {
                if (count64 == 0)
                    result = "=" + result;
                else
                {
                    int remainder64 = (count64 % 64) + 1;
                    count64 = count64 / 64;
                    char charPWD = strConst[remainder64 - 1];
                    result = charPWD + result;
                }
            }
            return result;
        }

        private string Get256Str(string strEncrypt, int lengthEncrypt, int num)
        {
            const string strConst = "=qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890+";
            string result = "";
            int count64 = 0;
            for (int i = 0; i < num; i++)
            {
                string charPWD = strEncrypt[lengthEncrypt - num + i].ToString();
                int charPos = strConst.IndexOf(charPWD);
                count64 = count64 * 64 + charPos;
            }
            for (int i = 0; i < 3; i++)
            {
                if (count64 != 0)
                {
                    int remainder256 = (count64 % 256);
                    count64 = count64 / 256;
                    char charPWD = (char)remainder256;
                    result = charPWD + result;
                }
            }
            return result;
        }

        #endregion

    }
}
