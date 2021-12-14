using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAO.RBAC;
using System.IO;
using System.Xml;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;
using System.Reflection;
using System.Data.OleDb;
using System.Data;

namespace ZhiFang.BLL.RBAC
{
    public class BImportFile : IBImportFile
    {
        public IBHREmployee IBHREmployee { get; set; }
        public IBRBACUser IBRBACUser { get; set; }
        public IBHRDept IBHRDept { get; set; }

        /// <summary>
        /// 批量导入员工（可支持账户同步生成）支持EXCEL\XML两种方式
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="filetype"></param>
        /// <param name="isCreateAccount"></param>
        /// <returns></returns>
        public string RJ_AddInEmpList(FileStream fs, ZhiFang.Common.Public.FileType type, bool isCreateAccount)
        {
            if (type == Common.Public.FileType.XML)
            {
                XmlDocument doc = ConvertFileStreamToXml(fs);
                XmlNodeList xns = doc.DocumentElement.SelectNodes("//Employee/Employee");
                if (xns == null)
                {
                    return "未找到需要导入的数据";
                }
                foreach (XmlNode xn in xns)
                {
                    HREmployee entity = new HREmployee();
                    entity.LabID = 1;
                    entity.Id = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
                    entity = SetObjectProperty<HREmployee>(entity, xn);
                    IBHREmployee.Entity = entity;
                    IBHREmployee.Add();

                    if (isCreateAccount)
                    {
                        IBRBACUser.RJ_AutoCreateUserAccount(entity.Id.ToString(), "");
                    }
                }
            }
            else if (type == Common.Public.FileType.EXCEL)
            {
                DataSet ExcelDs = ConvertFileStreamToDataSet(fs);
                if (ExcelDs == null || ExcelDs.Tables.Count <= 0 || ExcelDs.Tables[0].Rows.Count <= 0)
                {
                    return "未找到需要导入的数据";
                }

                foreach (DataRow dr in ExcelDs.Tables[0].Rows)
                {
                    HREmployee entity = new HREmployee();
                    entity.LabID = 1;
                    entity.Id = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
                    entity = SetObjectProperty<HREmployee>(entity, dr);
                    IBHREmployee.Entity = entity;
                    IBHREmployee.Add();

                    if (isCreateAccount)
                    {
                        IBRBACUser.RJ_AutoCreateUserAccount(entity.Id.ToString(), "");
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// 批量导入部门,支持EXCEL\XML两种方式
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="filetype"></param>
        /// <returns></returns>
        public string RJ_AddInDeptList(FileStream fs, ZhiFang.Common.Public.FileType type)
        {
            if (type == Common.Public.FileType.XML)
            {

                XmlDocument doc = ConvertFileStreamToXml(fs);
                XmlNodeList xns = doc.DocumentElement.SelectNodes("//Dept/Dept");
                if (xns == null)
                {
                    return "未找到需要导入的数据";
                }
                foreach (XmlNode xn in xns)
                {
                    HRDept entity = new HRDept();
                    entity.LabID = 1;
                    entity.Id = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
                    entity = SetObjectProperty<HRDept>(entity, xn);
                    IBHRDept.Entity = entity;
                    IBHRDept.Add();
                }
            }
            else if (type == Common.Public.FileType.EXCEL)
            {
                DataSet ExcelDs = ConvertFileStreamToDataSet(fs);
                if (ExcelDs == null || ExcelDs.Tables.Count <= 0 || ExcelDs.Tables[0].Rows.Count <= 0)
                {
                    return "未找到需要导入的数据";
                }

                foreach (DataRow dr in ExcelDs.Tables[0].Rows)
                {
                    HRDept entity = new HRDept();
                    entity.LabID = 1;
                    entity.Id = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0);
                    entity = SetObjectProperty<HRDept>(entity, dr);
                    IBHRDept.Entity = entity;
                    IBHRDept.Add();
                }
            }

            return "";
        }

        /// <summary>
        /// 转换文件流为Xml文档
        /// </summary>
        /// <param name="fs"></param>
        /// <returns></returns>
        private XmlDocument ConvertFileStreamToXml(FileStream fs)
        {      
            byte[] buffer = new byte[Convert.ToInt32(fs.Length)];
            fs.Read(buffer, 0, buffer.Length);
            
            string strXml = System.Text.Encoding.Default.GetString(buffer);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(strXml);

            fs.Close();
            return doc;
        }

        /// <summary>
        /// 转换文件流为DataSet
        /// </summary>
        /// <param name="fs"></param>
        /// <returns></returns>
        private DataSet ConvertFileStreamToDataSet(FileStream fs)
        {
            string fileName = fs.Name;
            fs.Close();
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + fileName + ";" + "Extended Properties='Excel 8.0;HDR=False;IMEX=1'";
            OleDbDataAdapter ExcelDA = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", strConn);
            DataSet ExcelDs = new DataSet();
            try
            {
                ExcelDA.Fill(ExcelDs, "ExcelInfo");
            }
            catch (Exception ex)
            {
                return null;
            }
            return ExcelDs;
        }

        /// <summary>
        /// 给对象的属性赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="xn"></param>
        /// <returns></returns>
        private T SetObjectProperty<T>(T t, XmlNode xn)
        {
            Type type = t.GetType();
            PropertyInfo[] pi = type.GetProperties();
            foreach (PropertyInfo item in pi)
            {
                if (xn.Attributes.GetNamedItem(item.Name) != null)
                {
                    item.SetValue(t, Convert.ChangeType(xn.Attributes.GetNamedItem(item.Name).InnerText, item.PropertyType), null);
                }
            }
            return t;
        }

        /// <summary>
        /// 给对象的属性赋值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="xn"></param>
        /// <returns></returns>
        private T SetObjectProperty<T>(T t, DataRow dr)
        {
            Type type = t.GetType();
            PropertyInfo[] pi = type.GetProperties();
            foreach (PropertyInfo item in pi)
            {
                if(dr.Table.Columns.Contains(item.Name))
                {
                    item.SetValue(t, Convert.ChangeType(dr[item.Name].ToString(), item.PropertyType), null);
                }
            }
            return t;
        }
    }
}
