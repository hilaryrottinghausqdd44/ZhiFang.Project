using System;
using System.Collections.Generic;

using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

using System.Configuration;


public class SqlServerDB
{
    public SqlConnection Conn = new SqlConnection();

    //public SqlConnection Conn=new SqlConnection();

    public string strErr = "";

    public SqlServerDB()
    {
        //CODEGEN: 该调用是 ASP.NET Web 服务设计器所必需的
        //InitializeComponent();
    }

    

    // WEB 服务示例
    // HelloWorld() 示例服务返回字符串 Hello World
    // 若要生成，请取消注释下列行，然后保存并生成项目
    // 若要测试此 Web 服务，请按 F5 键

    public bool OpenDB()
    {
        if (Conn.State == ConnectionState.Closed)
        {
            if (Conn.ConnectionString == "")
            {
                //SqlConnection MyConn = new SqlConnection();

                try
                {
                    Conn.ConnectionString = ConfigurationSettings.AppSettings["ConnectionString"];
                    //MyConn.Open();
                }
                catch
                {
                    return false;
                }
                //finally
                //{
                //    MyConn.Close();
                //    MyConn = null;
                //    System.GC.Collect();
                //}
            }

            try
            {
                if (Conn.State == ConnectionState.Closed)
                    Conn.Open();
            }
            catch (Exception ex)
            {
                string s = ex.ToString();
                return false;
            }
            return true;
        }
        return false;
    }

    [WebMethod(Description = "用户登录")]
    public bool ValidateUser(string userid, string password, out string strErr)
    {
        try
        {
            strErr = "用户名/密码不正确";

            OpenDB();
            DataSet myDS = new DataSet();
            int i = ExecCount("admin", "AdminName='" + userid + "' and AdminPassword='" + password + "'");
            if (i != 1)
            {
                return false;
            }

            return true;
        }
        catch (Exception e)
        {
            strErr = e.ToString();
            return false;
        }

    }
    [WebMethod(Description = "用户登录.")]
    public DataSet VerifyUserInfo(string UserAccount, string Password, out string strErr)
    {
        DataSet DS = null;
        strErr = "用户名不能为空";

        if (UserAccount.Trim() != "")
        {
            //User User=new User(UserAccount,Password,Domain);
            string PasswordNull = " and Password='" + Password + "'";
            if (Password.Trim() == "")
            {
                PasswordNull = " and (Password='" + Password + "' or Password is null)";
            }

            DS = ExecDS("Select * from RBAC_Users where Account='" + UserAccount + "'" + PasswordNull);
            strErr = "没有该用户或密码不正确！";
            if (DS.Tables.Count == 1)
            {
                if (DS.Tables[0].Rows.Count != 0)
                {
                    strErr = "用户已经被锁定！";
                    if (!Convert.ToBoolean(DS.Tables[0].Rows[0]["AccLock"]))
                    {
                        if (!Convert.IsDBNull(DS.Tables[0].Rows[0]["AccExpTm"]))
                        {
                            if (System.DateTime.Now >= Convert.ToDateTime(DS.Tables[0].Rows[0]["AccExpTm"]))
                            {
                                strErr = "用户已经到期，不能登录！";
                            }
                        }
                    }
                }
            }
            else
            {
                strErr = "该域数据库不能连接，请检查！";
            }
        }
        return DS;
    }

    [WebMethod(Description = "用户是否已经存在.")]
    private bool ExistUser(string UserID)
    {
        //如果存在，返回真，否则返回假
        bool bExist = false;

        //去数据库中找到USERID的用户
        int i = ExecCount("RBAC_Users", "Account='" + UserID + "'");
        if (i != 1)
        {
            return false;
        }

        return bExist;
    }


    [WebMethod(Description = "用户登录")]
    public bool test(string userid, string password)
    {
        try
        {
            string err = "";
            return (ValidateUser(userid, password, out err));
        }
        catch (Exception e)
        {
            strErr = e.ToString();
            return false;
        }

    }
    [WebMethod]
    public bool CloseDB()
    {
        try
        {
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
            if (Conn.State != ConnectionState.Closed)
            {
                Conn.Close();
            }

            return true;
        }
        catch (Exception e)
        {
            strErr = e.ToString();
            return false;
        }
    }

    [WebMethod]
    public int ExecCount(string TableName, string whereClause)
    {
        if (whereClause.Trim().Length != 0)
        {
            whereClause = " where " + whereClause;
        }

        OpenDB();
        string sql = "select count(*) from " + TableName + whereClause;
        try
        {
            if (Conn.State == ConnectionState.Closed)
            {
                Conn.Open();
            }

            SqlDataAdapter da = new SqlDataAdapter(sql, Conn);

            DataTable dt = new DataTable();

            int i = da.Fill(dt);

            i = Convert.ToInt32(dt.Rows[0][0].ToString());

            return i;
        }
        catch (Exception e)
        {
            strErr = e.ToString();
            return -1;
        }
    }

    [WebMethod]
    public int ExecLen(string TableName, string SQLOneRow, string FieldQueryLength)
    {

        OpenDB();
        string sql = "select " + FieldQueryLength + " from " + TableName + " where " + SQLOneRow;
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(sql, Conn);

            DataTable dt = new DataTable();

            int i = da.Fill(dt);

            string FieldValue = dt.Rows[0][0].ToString();

            i = FieldValue.Length;

            return i;
        }
        catch (Exception e)
        {
            strErr = e.ToString();
            return 0;
        }
    }

    [WebMethod]
    public string ExecFieldValue(string TableName, string SQLOneRow, string FieldValueQuery)
    {

        OpenDB();
        string sql = "select " + FieldValueQuery + " from " + TableName + " where " + SQLOneRow;
        try
        {
            SqlDataAdapter da = new SqlDataAdapter(sql, Conn);

            DataTable dt = new DataTable();

            int i = da.Fill(dt);

            string FieldValue = dt.Rows[0][0].ToString();

            //i=FieldValue.Length;

            return FieldValue;
        }
        catch (Exception e)
        {
            strErr = e.ToString();
            return "";
        }
    }

    [WebMethod(Description = "取一个DataSet.")]
    public DataSet ExecDS(string sText)
    {
        DataSet ds = null;

        try
        {
            OpenDB();

            SqlDataAdapter da = new SqlDataAdapter(sText, Conn);

            ds = new DataSet();
            da.Fill(ds);
        }
        catch (Exception ex)
        {
            string s = ex.Message;
        }
        finally
        {
            if (Conn.State != ConnectionState.Closed)
            {
                Conn.Close();
            }
        }

        return ds;
    }


    [WebMethod(Description = "取一个DataSet,按分页技术调用.")]
    public DataSet ExecDSPages(string sSql, int iStartPage, int iPageSize, out int StartPage, out int iCount)
    {
        DataSet ds = null;
        iCount = 0;

        if (iStartPage < 1)
        {
            iStartPage = 1;
        }
        try
        {
            OpenDB();

            SqlDataAdapter da = new SqlDataAdapter(sSql, Conn);

            ds = new DataSet();
            int i = da.Fill(ds);
            iCount = i;

            if (i > (iStartPage) * iPageSize)
            {
                //iStartPage=iStartPage;
            }
            else
            {
                iStartPage = Convert.ToInt32((i - 1) / iPageSize) + 1;
            }
            ds = new DataSet();
            i = da.Fill(ds, (iStartPage - 1) * iPageSize, iPageSize, "Table1");

        }
        catch (Exception ex)
        {
            string s = ex.Message;
            ECDS.Common.Log.Info("错误信息：" + s);
        }
        finally
        {
            if (Conn.State != ConnectionState.Closed)
            {
                Conn.Close();
            }
        }
        StartPage = iStartPage;
        return ds;
    }

    public string connectSQLClient(string strConn)
    {
        try
        {
            Conn = new SqlConnection(strConn);
            Conn.Open();
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
            }
            return "";

        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    [WebMethod(Description = "This method converts a temperature in " +
         "degrees Fahrenheit to a temperature in degrees Celsius.")]

    public DataSet ExecDataSet(string strSQL)
    {
        try
        {

            //SqlCommand cmd=new SqlCommand(strSQL,Conn);
            Conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(strSQL, Conn);

            DataSet ds = new DataSet();
            da.Fill(ds);

            return ds;
        }
        catch (Exception ex)
        {
            string strErr = ex.ToString();
            return errDataSet(strErr);
        }
    }

    private DataSet errDataSet(string strErr)
    {
        try
        {
            DataSet myD = new DataSet();
            DataTable custTable = myD.Tables.Add("|&|*");
            DataColumn myColumn;
            for (int i = 0; i < 40; i++)
            {
                myColumn = custTable.Columns.Add("错误描述" + i.ToString());
                myColumn.Unique = false;
                myColumn.MaxLength = 2560;

                myColumn.DataType = System.Type.GetType("System.String");
            }
            DataRow workRow;
            workRow = custTable.NewRow();
            workRow[0] = strErr;
            myD.Tables[0].Rows.Add(workRow);

            return myD;
        }
        catch (Exception ex)
        {
            return errDataSet(ex.ToString());
        }
    }
    [WebMethod(Description = "取数据库中的所有TABLE,不成功返回new DataSet(\"|&|*\")")]
    public DataSet ExecDataSetTable(string strConn)
    {
        try
        {
            string conMsg = connectSQLClient(strConn);
            if (conMsg != "")
            {
                Exception aa = new Exception(conMsg);
            }
            Conn.Open();
            DataTable schemaTable = new DataTable();

            //schemaTable=Conn.GetOleDbSchemaTable(OleDbSchemaGuid.Schemata,null);
            string strDataSchema = null;
            string strCatalog = null;

            //oracle 数据库的所有者
            if (2 > 1)//Conn.Provider.IndexOf("MSDAORA")>-1||Conn.Provider.IndexOf("ORAOLEDB")>-1)
            {
                strDataSchema = strConn.Substring(strConn.ToUpper().IndexOf("USER ID") + 8);
                if (strDataSchema.IndexOf(";") > -1)
                {
                    strDataSchema = strDataSchema.Substring(0, strDataSchema.IndexOf(";"));
                }
            }
            //SQL 数据库的所有者
            if (2 > 1)//Conn.Provider.IndexOf("SQLOLEDB")>-1)
            {
                strCatalog = strConn.Substring(strConn.ToUpper().IndexOf("INITIAL CATALOG") + 16);
                if (strCatalog.IndexOf(";") > -1)
                {
                    strCatalog = strCatalog.Substring(0, strCatalog.IndexOf(";"));
                }
            }

            //schemaTable = Conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,new object[] {strCatalog, strDataSchema, null, "TABLE"});
            //OleDb.OleDbSchemaGuid.Columns, New Object() {Nothing, Nothing, TableName, Nothing}

            DataSet dsSchema = new DataSet();
            dsSchema.Tables.Add(schemaTable);
            Conn.Close();
            return dsSchema;
        }
        catch (Exception ex)
        {
            string strErr = ex.ToString();
            return errDataSet(ex.ToString());
        }
    }

    [WebMethod(Description = "取数据库中的所有TABLE,不成功返回new DataSet(\"|&|*\")")]
    public string ProviderName(string strConn)
    {
        try
        {
            string conMsg = connectSQLClient(strConn);
            if (conMsg != "")
            {
                Exception aa = new Exception(conMsg);
            }
            Conn.Open();
            string strProvider = Conn.ConnectionString;
            Conn.Close();
            return strProvider;
        }
        catch (Exception ex)
        {
            return ex.ToString();

        }
    }


    [WebMethod(Description = "取数据库表中的所有OLEDBSchemaGuidColumns,不成功返回new DataSet(\"|&|*\")")]
    public DataSet ExecDataSetColumns(string strConn, string strTable)
    {
        try
        {
            string conMsg = connectSQLClient(strConn);
            if (conMsg != "")
            {
                Exception aa = new Exception(conMsg);
            }

            Conn.Open();
            DataTable schemaTable = new DataTable();

            //schemaTable = Conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,new object[] {null, null, null, "TABLE"});
            //schemaTable =Conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] {null, null, strTable, null});

            DataSet dsSchema = new DataSet();
            dsSchema.Tables.Add(schemaTable);
            Conn.Close();
            return dsSchema;
        }
        catch (Exception ex)
        {
            string strErr = ex.ToString();
            return errDataSet(ex.ToString());
        }
    }
    [WebMethod(Description = "取数据库表中的所有Primary_Keys,不成功返回new DataSet(\"|&|*\")")]
    public DataSet ExecDataSetPrimaryKeys(string strConn, string strTable)
    {
        try
        {
            string conMsg = connectSQLClient(strConn);
            if (conMsg != "")
            {
                Exception aa = new Exception(conMsg);
            }

            Conn.Open();
            DataTable schemaTable = new DataTable();

            //schemaTable = Conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,new object[] {null, null, null, "TABLE"});
            //schemaTable =Conn.GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys, new object[] {null, null, strTable});

            DataSet dsSchema = new DataSet();
            dsSchema.Tables.Add(schemaTable);
            Conn.Close();
            return dsSchema;
        }
        catch (Exception ex)
        {
            string strErr = ex.ToString();
            return errDataSet(ex.ToString());
        }
    }
    [WebMethod(Description = "取数据库表中的所有自动标识,不成功返回new DataSet(\"|&|*\")")]
    public DataSet ExecDataSetIdentities
        (string strConn, string strTable)
    {
        try
        {
            string conMsg = connectSQLClient(strConn);
            if (conMsg != "")
            {
                Exception aa = new Exception(conMsg);
            }

            Conn.Open();
            DataTable schemaTable = new DataTable();

            //schemaTable = Conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,new object[] {null, null, null, "TABLE"});
            //schemaTable =Conn.GetOleDbSchemaTable(OleDbSchemaGuid.Constraint_Column_Usage, new object[] {null, null, strTable});

            DataSet dsSchema = new DataSet();
            dsSchema.Tables.Add(schemaTable);
            Conn.Close();
            return dsSchema;
        }
        catch (Exception ex)
        {
            string strErr = ex.ToString();
            return errDataSet(ex.ToString());
        }
    }

    [WebMethod(Description = "取数据库支持的数据类型,主要指Access, Oracle, Sql Server,")]
    public DataSet OLEDBProviderType(string strConn)
    {
        try
        {
            string conMsg = connectSQLClient(strConn);
            if (conMsg != "")
            {
                Exception aa = new Exception(conMsg);
            }

            Conn.Open();
            DataTable schemaTable = new DataTable();

            //schemaTable = Conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables,new object[] {null, null, null, "TABLE"});
            //schemaTable =Conn.GetOleDbSchemaTable(OleDbSchemaGuid.Provider_Types, null);

            DataSet ds = new DataSet();
            ds.Tables.Add(schemaTable);

            Conn.Close();
            return ds;
            //return schemaTable.Rows[0].ToString();

        }
        catch (Exception ex)
        {
            string strErr = ex.ToString();
            return errDataSet(ex.ToString());
        }
    }

    [WebMethod(Description = "执行查询,返回影响的行数")]

    public int ExecuteNonQuery(string strSQL)
    {
        try
        {

            SqlCommand cmd = new SqlCommand(strSQL, Conn);

            OpenDB();
            int i = cmd.ExecuteNonQuery();

            return i;
        }
        catch (Exception ex)
        {
            string strErr = ex.ToString();
            return -1;
        }
        finally
        {
            if (Conn.State != ConnectionState.Closed)
            {
                Conn.Close();
            }
        }
    }

    [WebMethod(Description = "执行查询,返回影响的行数")]

    public string ExecuteNonQueryString(string strConn, string strSQL)
    {
        try
        {
            string conMsg = connectSQLClient(strConn);
            if (conMsg != "")
            {
                Exception aa = new Exception(conMsg);
            }
            SqlCommand cmd = new SqlCommand(strSQL, Conn);
            Conn.Open();
            int i = cmd.ExecuteNonQuery();
            Conn.Close();

            return i.ToString();
        }
        catch (Exception ex)
        {
            string strErr = ex.ToString();
            return strErr;
        }
    }

    [WebMethod(Description = "创建表,如有错误返回错误")]

    public string ExecuteCreateTable(string strConn, string strSQL)
    {
        try
        {
            string conMsg = connectSQLClient(strConn);
            if (conMsg != "")
            {
                Exception aa = new Exception(conMsg);
            }
            SqlCommand cmd = new SqlCommand(strSQL, Conn);
            Conn.Open();
            int i = cmd.ExecuteNonQuery();
            Conn.Close();

            return "";
        }
        catch (Exception ex)
        {
            string strErr = ex.ToString();
            return strErr;
        }
    }
    //[WebMethod(Description = "发送一个电子邮件")]
    //public bool Send(string From, string To, string Subject, string message, string Password, string ServerName, int Port)
    //{
    //    EmailClass myMail = new EmailClass();
    //    return myMail.SendMail(From, To, Subject, message, Password, ServerName, Port);
    //}
    //[WebMethod(Description = "发送一个电子邮件")]
    //public bool SendReplyTo(string From, string To, string Subject, string message, string Password, string ServerName, int Port, string replyto, bool IsBodyHtml)
    //{
    //    EmailClass myMail = new EmailClass();
    //    return myMail.SendMailReplyTo(From, To, Subject, message, Password, ServerName, Port, replyto, IsBodyHtml);
    //}


}

