using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Digitlab.IDAO;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Common.Public;

namespace ZhiFang.Digitlab.BLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public class BXmlConfig : ZhiFang.Digitlab.IBLL.ReagentSys.IBXmlConfig
    {
        ZhiFang.Digitlab.IBLL.Business.IBBParameter IBBParameter { get; set; }

        public BaseResultDataValue GetInputXmlConfig(int xmlConfigType, string baseFilePath)
        {
            //以后可能实验室和供应商有不同的模板
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string filePath = getXmlFileName(xmlConfigType, baseFilePath);
            if (System.IO.File.Exists(filePath))
            {
                DataSet ds = ZhiFang.Common.Public.XmlToData.XmlFileToDataSet(filePath);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    baseResultDataValue.ResultDataValue = "{count:" + ds.Tables[0].Rows.Count +
                        ",list: " + JsonHelp.DataTableToJson(ds.Tables[0]) + "}";
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "配置信息不存在！";
                ZhiFang.Common.Log.Log.Info("配置信息不存在！");
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue SaveInputXmlConfigToFile(int xmlConfigType, string xmlConfig, string baseFilePath)
        {
            //以后可能实验室和供应商有不同的模板
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string filePath = getXmlFileName(xmlConfigType, baseFilePath);
            DataSet ds = new DataSet();
            ds.DataSetName = "NewDataSet";
            DataTable dt = ZhiFang.Common.Public.ObjectArrayToDataSet.JsonToDataTable(xmlConfig, "list");
            if (dt != null)
            {
                dt.TableName = "TableName";
                ds.Tables.Add(dt);
                //ZhiFang.Common.Public.TransDataToXML.CDataToXmlFile(ds, -1, filePath);
                ZhiFang.Common.Public.TransDataToXML.DataTableToXML(dt, filePath, "GB2312");
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "没有要保存的配置信息！";
                ZhiFang.Common.Log.Log.Info("没有要保存的配置信息！");
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue SaveInputXmlConfigToFile1(int xmlConfigType, string xmlConfig, string baseFilePath)
        {
            //以后可能实验室和供应商有不同的模板
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            xmlConfig = "{count:27,list:[{\"FieldName\":\"GoodsNo\",\"ExcelFieldName\":\"产品编码（必填）\",\"IsPrimaryKey\":\"1\",\"IsRequiredField\":\"1\"},{\"FieldName\":\"CompGoodsNo\",\"ExcelFieldName\":\"供应商产品编码\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"0\"},{\"FieldName\":\"ProdGoodsNo\",\"ExcelFieldName\":\"厂商产品编码\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"0\"},{\"FieldName\":\"ProdNo\",\"ExcelFieldName\":\"厂商机构编码（必填）\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"1\"},{\"FieldName\":\"CName\",\"ExcelFieldName\":\"试剂中文名称（必填）\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"1\"},{\"FieldName\":\"EName\",\"ExcelFieldName\":\"英文名称\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"0\"},{\"FieldName\":\"ShortCode\",\"ExcelFieldName\":\"产品简码\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"0\"},{\"FieldName\":\"UnitMemo\",\"ExcelFieldName\":\"包装规格（必填）\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"1\"},{\"FieldName\":\"UnitName\",\"ExcelFieldName\":\"包装单位（必填）\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"1\"},{\"FieldName\":\"GoodsClass\",\"ExcelFieldName\":\"一级分类\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"0\"},{\"FieldName\":\"GoodsClassType\",\"ExcelFieldName\":\"二级分类\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"0\"},{\"FieldName\":\"StorageType\",\"ExcelFieldName\":\"储藏条件\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"0\"},{\"FieldName\":\"RegistNo\",\"ExcelFieldName\":\"注册证\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"0\"},{\"FieldName\":\"RegistDate\",\"ExcelFieldName\":\"注册日期\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"0\"},{\"FieldName\":\"RegistNoInvalidDate\",\"ExcelFieldName\":\"注册证到期日期\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"0\"},{\"FieldName\":\"GoodsDesc\",\"ExcelFieldName\":\"货品描述\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"0\"},{\"FieldName\":\"ApproveDocNo\",\"ExcelFieldName\":\"批准文号\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"0\"},{\"FieldName\":\"Standard\",\"ExcelFieldName\":\"国标\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"0\"},{\"FieldName\":\"Constitute\",\"ExcelFieldName\":\"结构组成\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"0\"},{\"FieldName\":\"Visible\",\"ExcelFieldName\":\"是否使用\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"0\"},{\"FieldName\":\"Price\",\"ExcelFieldName\":\"采购单价(元)\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"0\"},{\"FieldName\":\"BiddingNo\",\"ExcelFieldName\":\"招标号\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"0\"},{\"FieldName\":\"IsRegister\",\"ExcelFieldName\":\"是否有注册证\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"0\"},{\"FieldName\":\"BarCodeMgr\",\"ExcelFieldName\":\"盒条码类型\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"0\"},{\"FieldName\":\"IsPrintBarCode\",\"ExcelFieldName\":\"是否打印条码\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"0\"},{\"FieldName\":\"SuitableType\",\"ExcelFieldName\":\"适用机型\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"0\"},{\"FieldName\":\"TestCount\",\"ExcelFieldName\":\"测试数\",\"IsPrimaryKey\":\"0\",\"IsRequiredField\":\"0\"}]}";
            string filePath = getXmlFileName(xmlConfigType, baseFilePath);
            DataSet ds = new DataSet();
            ds.DataSetName = "NewDataSet";
            DataTable dt = ZhiFang.Common.Public.ObjectArrayToDataSet.JsonToDataTable(xmlConfig, "list");
            if (dt != null)
            {
                dt.TableName = "TableName";
                ds.Tables.Add(dt);
                ZhiFang.Common.Public.TransDataToXML.CDataToXmlFile(ds, -1, baseFilePath + "\\BaseTableXML\\Goods122323.xml", "GB2312");
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "没有要保存的配置信息！";
                ZhiFang.Common.Log.Log.Info("没有要保存的配置信息！");
            }
            return baseResultDataValue;
        }

        private string getXmlFileName(int xmlConfigType, string baseFilePath)
        {
            string filePath = "";
            switch (xmlConfigType)
            {
                case 101:
                    filePath = baseFilePath + "\\BaseTableXML\\Goods.xml";
                    break;
                case 102:
                    filePath = baseFilePath + "\\BaseTableXML\\BmsCenOrderDoc.xml";
                    break;
                case 103:
                    filePath = baseFilePath + "\\BaseTableXML\\BmsCenOrderDtl.xml";
                    break;
                case 104:
                    filePath = baseFilePath + "\\BaseTableXML\\BmsCenSaleDoc.xml";
                    break;
                case 105:
                    filePath = baseFilePath + "\\BaseTableXML\\BmsCenSaleDtl.xml";
                    break;
                case 106:
                    filePath = baseFilePath + "\\BaseTableXML\\ReaGoods.xml";
                    break;
            }
            return filePath;
        }

        public BaseResultDataValue GetJsonConfig(string jsonConfigType)
        {
            //以后可能实验室和供应商有不同的模板
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            IList<BParameter> paraList = IBBParameter.SearchListByHQL(" bparameter.ParaNo=\'" + jsonConfigType + "\'");
            if (paraList != null && paraList.Count > 0)
            {
                baseResultDataValue.ResultDataValue = paraList[0].ParaValue;
                ZhiFang.Common.Public.JsonHelp.JsonConfigToDataTable(paraList[0].ParaValue);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue EditJsonConfig(string jsonConfigType, string jsonConfig)
        {
            //以后可能实验室和供应商有不同的模板
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (!string.IsNullOrEmpty(jsonConfig))
                jsonConfig = StringPlus.Unicode2String(jsonConfig);
            IList<BParameter> paraList = IBBParameter.SearchListByHQL(" bparameter.ParaNo=\'" + jsonConfigType + "\'");
            if (paraList != null && paraList.Count > 0)
            {
                paraList[0].ParaValue = jsonConfig;
                IBBParameter.Entity = paraList[0];
                baseResultDataValue.success = IBBParameter.Edit();
            }
            else
            {
                BParameter paraEntity = new BParameter();
                paraEntity.Name = "实验室数据库链接字符串";
                paraEntity.ParaNo = "LabADODBLinkInfo";
                paraEntity.ParaType = "LabDBLinkConfig";
                paraEntity.ParaValue = jsonConfig;
                paraEntity.ParaDesc = "实验室数据库链接字符串ADO";
                paraEntity.IsUse = true;
                paraEntity.DataAddTime = DateTime.Now;
                IBBParameter.Entity = paraEntity;
                baseResultDataValue.success = IBBParameter.Add();
            }
            return baseResultDataValue;
        }

    }
}