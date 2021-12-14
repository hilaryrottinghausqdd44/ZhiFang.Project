using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Data;
using System.Collections;
using ZhiFang.Tools;
using ZhiFang.BLLFactory;
using ZhiFang.IDAL;
using ZhiFang.DALFactory;
using ZhiFang.Model;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.Tools;
using ZhiFang.IBLL.Common;

namespace ZhiFang.BLL.Common
{
    public class RequestData : IBRequestData
    {
        IDBarCodeForm dalbcf = DalFactory<IDBarCodeForm>.GetDal("BarCodeForm", ZhiFang.Common.Dictionary.DBSource.LisDB());
        IDNRequestForm dalnrf = DalFactory<IDNRequestForm>.GetDal("NRequestForm", ZhiFang.Common.Dictionary.DBSource.LisDB());
        IDNRequestItem dalnrt = DalFactory<IDNRequestItem>.GetDal("NRequestItem", ZhiFang.Common.Dictionary.DBSource.LisDB());
        private readonly IBTestItemControl ibtic = BLLFactory<IBTestItemControl>.GetBLL("BaseDictionary.TestItemControl");
        private readonly IBSampleTypeControl ibstc = BLLFactory<IBSampleTypeControl>.GetBLL("BaseDictionary.SampleTypeControl");
        private readonly IBGenderTypeControl ibgtc = BLLFactory<IBGenderTypeControl>.GetBLL("BaseDictionary.GenderTypeControl");
        /// <summary>
        /// 取主键的查询表达式,即where条件
        /// </summary>
        /// <param name="hashForm"></param>
        /// <returns></returns>
        public string getRequestDataPkWhere(Hashtable hashForm)
        {
            string fieldWhereModal = "\"{0}\"='{1}'";
            string pkWhere = "";
            if (pkWhere != "")
                pkWhere += " AND ";
            pkWhere += string.Format(fieldWhereModal, "NRequestFormNo", hashForm["NRequestFormNo".ToUpper()].ToString());
            return pkWhere;
        }
        /// <summary>
        /// 申请单上传（将上传的申请单保存到数据库中）
        /// </summary>
        /// <param name="xmlData">xml数据</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns>0 成功； -1 失败</returns>
        public int UpLoadRequestFromBytes(byte[] xmlData, out string errorMsg)
        {
            IDNRequestForm iNReForm = ZhiFang.DALFactory.DalFactory<IDNRequestForm>.GetDal("NRequestForm", ZhiFang.Common.Dictionary.DBSource.LisDB());
            IDBarCodeForm idbcf = ZhiFang.DALFactory.DalFactory<IDBarCodeForm>.GetDal("BarCodeForm", ZhiFang.Common.Dictionary.DBSource.LisDB());
            IDNRequestItem idri = ZhiFang.DALFactory.DalFactory<IDNRequestItem>.GetDal("NRequestItem", ZhiFang.Common.Dictionary.DBSource.LisDB());

            string msg = "调用方法“UpLoadRequestFromBytes”将上传的申请单保存报告到数据库!\r\n";
            ZhiFang.Common.Log.Log.Info(msg);
            int result = -1;
            errorMsg = "";
            try
            {
                //将字节数组转换成字符串
                string xml = ConvertData.convertBytesToString(xmlData);
                if ((xml == null) || (xml == ""))
                {
                    errorMsg = "xmlData没有内容，不能上传申请单!\r\n";
                    ZhiFang.Common.Log.Log.Info(errorMsg);
                    return result;
                }
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                string xPath = "//ds";
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                XmlNode reportFormNodeSave = nodelist[0];
                string tableNameBarCode = "BarCodeForm";
                //第一级是保存到NRequestForm
                string tableNameForm = "NRequestForm";
                //第二级是保存到NRequestItem
                string tableNameItem = "NRequestItem";
                string tableNameItemXMLData = "NRequestItem";
                //表barcodeForm中的字段列表

                Hashtable hashBarCode = ZhiFang.Tools.ConvertData.getTableColumnNameList(idbcf.GetList(null));
                //表NrequestForm中的字段列表
                Hashtable hashFormColumn = ZhiFang.Tools.ConvertData.getTableColumnNameList(iNReForm.GetList(null));
                //表NrequestItem中的字段列表
                Hashtable hashItemColumn = ZhiFang.Tools.ConvertData.getTableColumnNameList(idri.GetList(null));
                //取主表的字段名称和内容
                Hashtable hashForm = GetXMLData.getXmlNodeNameAndValue(reportFormNodeSave);
                //转换字段名称为大写
                hashForm = ConvertData.convertHashKeyToUpper(hashForm);
                string pkWhere = "";
                RequestData requestData = new RequestData();
                if (hashForm.Contains("NREQUESTFORMNO"))
                {
                    //取XML数据表对应的主键(唯一索引)
                    pkWhere = requestData.getRequestDataPkWhere(hashForm);
                }
                else
                {
                    pkWhere = DateTime.Now.Ticks.ToString();
                }
                string NRequestFormNo = pkWhere;
                //先删除以前的数据
                int count = iNReForm.Delete(NRequestFormNo);
                msg = "运行SQL语句删除子表（项目表）：\r\n";
                ZhiFang.Common.Log.Log.Info(msg);
                if (count > 0)
                {
                    //再删除主表
                    msg = "运行SQL语句删除主表（报告表）：\r\n";
                    ZhiFang.Common.Log.Log.Info(msg);
                    int i = iNReForm.Delete(Convert.ToInt32(NRequestFormNo));
                }
                //插入新数据到数据库中
                string sqlForm="";
               // string sqlForm = getInsertSQL(tableNameBarCode, hashBarCode, hashForm);
                if (sqlForm != "")
                {
                    msg = "运行SQL语句插入主表数据（BarCodeForm表）：\r\n" + sqlForm;
                    ZhiFang.Common.Log.Log.Info(msg);

                }
                if (hashForm.Contains("SampleTypeNo"))
                {

                }
                //先生成插入主表的SQL脚本
               //sqlForm = Tools.getInsertSQL(tableNameForm, hashFormColumn, hashForm);
                if (sqlForm != "")
                {
                    msg = "运行SQL语句插入主表数据（报告表）：\r\n" + sqlForm;
                    ZhiFang.Common.Log.Log.Info(msg);

                    //插入主表数据
                    // CreateDB().ExecuteNonQuery(sqlForm);
                }
                xPath = "//" + tableNameItemXMLData;
                nodelist = doc.SelectNodes(xPath);
                foreach (XmlNode xmlNode in nodelist)
                {
                    //取主表的字段名称和内容
                    Hashtable hashItem = GetXMLData.getXmlNodeNameAndValue(xmlNode);
                    //先生成插入子表的SQL脚本
                    string sqlItem="";
                    //string sqlItem = Tools.getInsertSQL(tableNameItem, hashItemColumn, hashItem);
                    if (sqlItem != "")
                    {
                        msg = "运行SQL语句插入子表数据（项目表）：\r\n" + sqlItem;
                        ZhiFang.Common.Log.Log.Info(msg);
                        //插入子表数据
                        //WL.BLL.DataConn.CreateDB().ExecuteNonQuery(sqlItem);
                        //DBUtility.DBFactory.CreateDB(dbsourceconn);
                    }
                }
                msg = "成功！";
                ZhiFang.Common.Log.Log.Info(msg);

                //成功
                result = 0;
            }
            catch (System.Exception ex)
            {
                errorMsg = "上传申请单失败:将数据保存到数据库是失败:\r\n" + ex.Message;
                ZhiFang.Common.Log.Log.Info(msg);
            }
            return result;
        }
        #region UpgradeRequestForm的方法
        #region 更新数据集
        /// <summary>
        /// 更新BarcoderForm
        /// </summary>
        /// <param name="wsBarCode"></param>
        public void UpdateBarCode(string BarCodeFormNo, DataSet wsBarCode, string WebLisSourceOrgId, string WebLisOrgID)
        {
            
            if (!wsBarCode.Tables[0].Columns.Contains("WebLisSourceOrgId"))
            {
                wsBarCode.Tables[0].Columns.Add("WebLisSourceOrgId");
            }
            if (!wsBarCode.Tables[0].Columns.Contains("WebLisOrgID"))
            {
                wsBarCode.Tables[0].Columns.Add("WebLisOrgID");
            }
            if (wsBarCode.Tables[0].Columns.Contains("BarCodeFormNo"))
            {
                wsBarCode.Tables[0].Columns.Add("BarCodeFormNo");
            }
            for (int i = 0; i < wsBarCode.Tables[0].Rows.Count; i++)
            {
                wsBarCode.Tables[0].Rows[i]["WebLisSourceOrgId"] = WebLisSourceOrgId;
                wsBarCode.Tables[0].Rows[i]["WebLisOrgID"] = WebLisOrgID;
                wsBarCode.Tables[0].Rows[i]["BarCodeFormNo"] = BarCodeFormNo;
            }
        }
        /// <summary>
        /// 更新NrequestForm
        /// </summary>
        /// <param name="wsNRequestForm"></param>
        public void UpdateNRequestForm(DataSet wsNRequestForm, string WebLisSourceOrgId, string WebLisOrgID)
        {

            if (!wsNRequestForm.Tables[0].Columns.Contains("NRequestFormNo"))
            {
                wsNRequestForm.Tables[0].Columns.Add("NRequestFormNo");
            }
            if (!wsNRequestForm.Tables[0].Columns.Contains("WebLisSourceOrgId"))
            {
                wsNRequestForm.Tables[0].Columns.Add("WebLisSourceOrgId");
            }
            if (!wsNRequestForm.Tables[0].Columns.Contains("ClientNo"))
            {
                wsNRequestForm.Tables[0].Columns.Add("ClientNo");
            }
            for (int i = 0; i < wsNRequestForm.Tables[0].Rows.Count; i++)
            {
                wsNRequestForm.Tables[0].Rows[i]["NRequestFormNo"] = DateTime.Now.Ticks;
                wsNRequestForm.Tables[0].Rows[i]["WebLisSourceOrgId"] = WebLisSourceOrgId;
                wsNRequestForm.Tables[0].Rows[i]["ClientNo"] = WebLisSourceOrgId;

                if (wsNRequestForm.Tables[0].Rows[i]["jztype"].ToString() == "")
                {
                    wsNRequestForm.Tables[0].Rows[i]["jztype"] = wsNRequestForm.Tables[0].Rows[i]["sicktypeno"].ToString();
                }
            }
        }
        /// <summary>
        /// 更新NrequestItem
        /// </summary>
        /// <param name="wsNRequestItem"></param>
        public void UpdateNRequestItem(string BarCodeFormNo,DataSet wsNRequestItem, DataSet wsNRequestForm, string WebLisSourceOrgId, string WebLisOrgID)
        {
            if (wsNRequestItem.Tables[0].Columns.Contains("nrequestitemno"))
            {
                wsNRequestItem.Tables[0].Columns.Remove("nrequestitemno");
            }
            if (!wsNRequestItem.Tables[0].Columns.Contains("WebLisOrgID"))
            {
                wsNRequestItem.Tables[0].Columns.Add("WebLisOrgID");
            }
            if (!wsNRequestItem.Tables[0].Columns.Contains("WebLisSourceOrgID"))
            {
                wsNRequestItem.Tables[0].Columns.Add("WebLisSourceOrgId");
            }

            if (!wsNRequestItem.Tables[0].Columns.Contains("NRequestFormNo"))
            {
                wsNRequestItem.Tables[0].Columns.Add("NRequestFormNo");
            }

            if (!wsNRequestItem.Tables[0].Columns.Contains("SampleTypeNo"))
            {
                DataColumn dc = new DataColumn("SampleTypeNo", DbType.Int32.GetType());
                wsNRequestItem.Tables[0].Columns.Add(dc);
            }

            for (int i = 0; i < wsNRequestItem.Tables[0].Rows.Count; i++)
            {
                if (wsNRequestItem.Tables[0].Columns.Contains("NRequestFormNo"))
                {
                    wsNRequestItem.Tables[0].Rows[i]["NRequestFormNo"] = wsNRequestForm.Tables[0].Rows[i]["NRequestFormNo"];
                }

                if (wsNRequestItem.Tables[0].Columns.Contains("BarCodeFormNo"))
                {
                    wsNRequestItem.Tables[0].Rows[i]["BarCodeFormNo"] = BarCodeFormNo;
                }

                wsNRequestItem.Tables[0].Rows[i]["WebLisSourceOrgId"] = WebLisSourceOrgId;
                wsNRequestItem.Tables[0].Rows[i]["WebLisOrgID"] = WebLisOrgID;
                wsNRequestItem.Tables[0].Rows[i]["SampleTypeNo"] = wsNRequestForm.Tables[0].Rows[0]["SampleTypeNo"];
            }
        }
        #region 保存申请单，项目，条码号等数据集
        /// <summary>
        /// 保存获取到的申请单项目条码数据
        /// </summary>
        /// <param name="BarCodeNo">条码号</param>
        /// <param name="wsBarCode">条码数据集</param>
        /// <param name="wsNRequestItem">项目数据集</param>
        /// <param name="wsNRequestForm">申请单数据集</param>
        /// <returns>0 成功 -1失败</returns>
        public int SaveWebLisData(string BarCodeNo, DataSet wsBarCode, DataSet wsNRequestItem, DataSet wsNRequestForm)
        {
            //====================================
            //独立实验室中条码
            BarCodeForm barCodeForm = new BarCodeForm();
            barCodeForm.BarCode = BarCodeNo;
            DataSet lisBarCode = dalbcf.GetList(barCodeForm);
            //独立实验室中申请单  
            NRequestForm nRequestForm = new NRequestForm();
            nRequestForm.SerialNo = BarCodeNo;
            DataSet lisNRequestForm = dalnrf.GetList(nRequestForm);
            //独立实验室中项目
            NRequestItem nRequestItem = new NRequestItem();
            nRequestItem.SerialNo = BarCodeNo;
            DataSet dtLisNRItem = dalnrt.GetList(nRequestItem);
            
            int count=0;
            if (lisBarCode != null && lisBarCode.Tables.Count > 0&&lisBarCode.Tables[0].Rows.Count>0)
            {
                GetBarCodeForm(wsBarCode.Tables[0].Rows[0], barCodeForm);
                //更新条码表
                count = dalbcf.Update(barCodeForm);
            }
            else
            {
                //插入条码表
                foreach (DataRow dr in wsBarCode.Tables[0].Rows)
                {
                    GetBarCodeForm(dr, barCodeForm);
                    count = dalbcf.Add(barCodeForm);
                }
            }
            if (lisNRequestForm != null && lisNRequestForm.Tables.Count > 0&&lisNRequestForm.Tables[0].Rows.Count>0)
            {
                GetFormInfo(wsNRequestForm.Tables[0].Rows[0], nRequestForm);
                //更新申请单
                count = dalnrf.Update(nRequestForm);
                //string strSql = CreateUpdateSql(wsNRequestForm.Tables[0].Rows[0], lisNRequestForm.Tables[0].Rows[0], "NRequestForm");
            }
            else
            {
                //插入申请单
                foreach (DataRow dr in wsNRequestForm.Tables[0].Rows)
                {
                    GetFormInfo(dr, nRequestForm);
                    count = dalnrf.Add(nRequestForm);
                    //string strSql = CreatInsertSql(dr, lisNRequestForm.Tables[0].Columns, "NRequestForm");
                }
            }
            if (dtLisNRItem != null && dtLisNRItem.Tables.Count > 0&&dtLisNRItem.Tables[0].Rows.Count>0)
            {
                //更新项目
                GetItemInfo(wsNRequestItem.Tables[0].Rows[0],nRequestItem);
                count = dalnrt.Update(nRequestItem);
            }
            else
            {
                //插入项目
                foreach (DataRow dr in wsNRequestItem.Tables[0].Rows)
                {
                    GetItemInfo(dr, nRequestItem);
                    count = dalnrt.Add(nRequestItem);
                }
            }
            return 0;
        }
        public void GetFormInfo(DataRow wsNRequestForm,NRequestForm nRequestForm)
        {
            if (wsNRequestForm != null && wsNRequestForm.Table.Rows.Count > 0)
            {
                if (wsNRequestForm.Table.Columns.Contains("ClientNo"))
                {
                    nRequestForm.ClientNo = wsNRequestForm.Table.Rows[0]["ClientNo"].ToString().Trim();
                }
                if (wsNRequestForm.Table.Columns.Contains("barCode"))
                {
                    nRequestForm.SerialNo = wsNRequestForm.Table.Rows[0]["barCode"].ToString();
                }
                if (wsNRequestForm.Table.Columns.Contains("CollectDate"))
                {
                    nRequestForm.CollectDate = Convert.ToDateTime(wsNRequestForm.Table.Rows[0]["CollectDate"]);
                }
                if (wsNRequestForm.Table.Columns.Contains("CollectTime"))
                {
                    nRequestForm.CollectTime = Convert.ToDateTime(wsNRequestForm.Table.Rows[0]["CollectTime"]);
                }
                if (wsNRequestForm.Table.Columns.Contains("ClientNo"))
                {
                    nRequestForm.ClientNo = wsNRequestForm.Table.Rows[0]["ClientNo"].ToString().Trim();
                }
                if (wsNRequestForm.Table.Columns.Contains("WebLisSourceOrgID"))
                {
                    nRequestForm.WebLisSourceOrgID = wsNRequestForm.Table.Rows[0]["WebLisSourceOrgID"].ToString().Trim();
                }
                if (wsNRequestForm.Table.Columns.Contains("SampleTypeNo"))
                {
                    nRequestForm.SampleTypeNo = Convert.ToInt32(wsNRequestForm.Table.Rows[0]["SampleTypeNo"]);
                }
                if (wsNRequestForm.Table.Columns.Contains("GenderNo"))
                {
                    nRequestForm.GenderNo = Convert.ToInt32(wsNRequestForm.Table.Rows[0]["GenderNo"]);
                }
                if (wsNRequestForm.Table.Columns.Contains("jztype"))
                {
                    nRequestForm.jztype =Convert.ToInt32( wsNRequestForm.Table.Rows[0]["jztype"].ToString());
                }
                if(wsNRequestForm.Table.Columns.Contains("NRequestFormNo"))
                {
                    nRequestForm.NRequestFormNo = long.Parse(wsNRequestForm.Table.Rows[0]["NRequestFormNo"].ToString());
                }
            }
        }
        public void GetItemInfo(DataRow wsNRequestItem, NRequestItem nRequestItem)
        {
            if (wsNRequestItem != null && wsNRequestItem.Table.Rows.Count > 0)
            {
                if (wsNRequestItem.Table.Columns.Contains("ClientNo"))
                {
                    nRequestItem.ClientNo = wsNRequestItem.Table.Rows[0]["ClientNo"].ToString().Trim();
                }
                if (wsNRequestItem.Table.Columns.Contains("barCode"))
                {
                    nRequestItem.SerialNo = wsNRequestItem.Table.Rows[0]["barCode"].ToString();
                }
                if (wsNRequestItem.Table.Columns.Contains("NRequestFormNo"))
                {
                    nRequestItem.NRequestFormNo = long.Parse(wsNRequestItem.Table.Rows[0]["NRequestFormNo"].ToString());
                }
                if (wsNRequestItem.Table.Columns.Contains("BarCodeFormNo"))
                {
                    nRequestItem.BarCodeFormNo = long.Parse(wsNRequestItem.Table.Rows[0]["BarCodeFormNo"].ToString());
                }
                if (wsNRequestItem.Table.Columns.Contains("ParItemNo"))
                {
                    //nRequestItem.ParItemNo = Convert.ToInt32(wsNRequestItem.Table.Rows[0]["ParItemNo"].ToString().Trim());
                    nRequestItem.ParItemNo = wsNRequestItem.Table.Rows[0]["ParItemNo"].ToString().Trim();
                }
                if (wsNRequestItem.Table.Columns.Contains("SampleTypeNo"))
                {
                    nRequestItem.SampleTypeNo = long.Parse(wsNRequestItem.Table.Rows[0]["SampleTypeNo"].ToString().Trim());
                }
                if (wsNRequestItem.Table.Columns.Contains("OldSerialNo"))
                {
                    nRequestItem.OldSerialNo = wsNRequestItem.Table.Rows[0]["OldSerialNo"].ToString().Trim();
                }
                if (wsNRequestItem.Table.Columns.Contains("WebLisOrgID"))
                {
                    nRequestItem.WebLisOrgID = wsNRequestItem.Table.Rows[0]["WebLisOrgID"].ToString().Trim();
                }
                if (wsNRequestItem.Table.Columns.Contains("WebLisSourceOrgID"))
                {
                    nRequestItem.WebLisSourceOrgID = wsNRequestItem.Table.Rows[0]["WebLisSourceOrgID"].ToString().Trim();
                }
                if (wsNRequestItem.Table.Columns.Contains("NRequestItemNo"))
                {
                    if (wsNRequestItem.Table.Rows[0]["NRequestItemNo"].ToString() != null)
                    {
                        nRequestItem.NRequestItemNo = long.Parse(wsNRequestItem.Table.Rows[0]["NRequestItemNo"].ToString());
                    }
                }
               
            }
        }
        public void GetBarCodeForm(DataRow wsBarCode,BarCodeForm barCodeForm)
        {
            if (wsBarCode != null && wsBarCode.Table.Rows.Count > 0)
            {
                if (wsBarCode.Table.Columns.Contains("barCode"))
                {
                    barCodeForm.BarCode = wsBarCode.Table.Rows[0]["barCode"].ToString().Trim();
                }
                if (wsBarCode.Table.Columns.Contains("CollectDate"))
                {
                    barCodeForm.CollectDate = Convert.ToDateTime(wsBarCode.Table.Rows[0]["CollectDate"]);
                }
                if (wsBarCode.Table.Columns.Contains("CollectTime"))
                {
                    barCodeForm.CollectTime = Convert.ToDateTime(wsBarCode.Table.Rows[0]["CollectTime"]);
                }
                if (wsBarCode.Table.Columns.Contains("ClientNo"))
                {
                    barCodeForm.ClientNo = wsBarCode.Table.Rows[0]["ClientNo"].ToString().Trim();
                }
                if (wsBarCode.Table.Columns.Contains("BarCodeFormNo"))
                {
                    barCodeForm.BarCodeFormNo = long.Parse(wsBarCode.Table.Rows[0]["BarCodeFormNo"].ToString());
                }
                if (wsBarCode.Table.Columns.Contains("WebLisSourceOrgId"))
                {
                    barCodeForm.WebLisSourceOrgId = wsBarCode.Table.Rows[0]["WebLisSourceOrgId"].ToString();
                }
                if (wsBarCode.Table.Columns.Contains("SampleTypeNo"))
                {
                    barCodeForm.SampleTypeNo = Convert.ToInt32(wsBarCode.Table.Rows[0]["SampleTypeNo"].ToString());
                }
            }
        }
       
        #endregion
        #endregion
        #region 将数据更新到数据库
        public int UpdateWebLisOrgID(DataSet wsBarCode, string WebLisOrgID)
        {
            BarCodeForm barCode = new BarCodeForm();
            int count = 0;
            for (int i = 0; i < wsBarCode.Tables[0].Rows.Count; i++)
            {

                barCode = dalbcf.GetModel(Convert.ToInt64(wsBarCode.Tables[0].Rows[i]["BarCodeFormNo"].ToString()));
               
                barCode.WebLisOrgID = WebLisOrgID;
                count = dalbcf.UpdateByBarCodeFormNo(barCode);
            }
            return count;
        }
        public int UpdateItemWebLisOrgID(DataSet wsNRequestItem, string WebLisOrgID)
        {
            NRequestItem nRequestItem = new NRequestItem();
            int count = 0;
            for (int i = 0; i < wsNRequestItem.Tables[0].Rows.Count; i++)
            {
                nRequestItem.BarCodeFormNo = Convert.ToInt64(wsNRequestItem.Tables[0].Rows[i]["BarCodeFormNo"]);
                nRequestItem.WebLisOrgID = WebLisOrgID;
                count = dalnrt.UpdateByBarcodeFormNo(nRequestItem);
            }
            return count;
        }
        #endregion
        #endregion

    }
}