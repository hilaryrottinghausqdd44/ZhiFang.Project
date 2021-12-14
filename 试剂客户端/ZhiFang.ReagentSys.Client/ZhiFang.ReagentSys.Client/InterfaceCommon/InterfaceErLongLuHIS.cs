using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Reflection;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Spring.Context;
using Spring.Context.Support;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.RBAC;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.ReagentSys.Client.WebReference_HIS_ErLongLu;

namespace ZhiFang.ReagentSys.Client
{
    public class InterfaceErLongLu
    {        
        public BaseResultData GetBaseDataInfo(string dictType)
        {
            BaseResultData brd = new BaseResultData();
            string paraBase = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?><ROOT><LEAF></LEAF></ROOT>";
            string serviceName = "";
            if (dictType.ToUpper() == "DEPT")
                serviceName = "DepartmentQueryToZFKSXX";
            else if (dictType.ToUpper() == "EMPLOYEE")
                serviceName = "DepartmentQueryToZFRYXX";
            else if (dictType.ToUpper() == "REACENORG")
                serviceName = "DepartmentQueryToZFGYSXX";
            else if (dictType.ToUpper() == "REAGOODS")
                serviceName = "DepartmentQueryToZFHPXX";
            else if (dictType.ToUpper() == "REASTORAGE")
                serviceName = "DepartmentQueryToZFKFXX";
            WSEntryClassService wsEntryClassService = new WSEntryClassService();
            //string strXML = XDocument.Load("D:\\接口文档说明\\二龙路医院接口文档\\接口测试程序\\接口测试返回的xml\\ReaGoods.xml").ToString();          
            string strXML = wsEntryClassService.invoke(serviceName, "","", paraBase);
            ZhiFang.Common.Log.Log.Info("接口服务invoke--" + serviceName + "返回值：" + strXML);
            if (!string.IsNullOrEmpty(strXML))
            {
                StringReader StrStream = new StringReader(strXML);
                XDocument xml = XDocument.Load(StrStream);
                XElement xeRoot = xml.Root;//根目录
                XElement xeBody = xeRoot.Element("LEAF");
                string appCode = xeBody.Attribute("APPCODE").Value;
                IList<XElement> xeBodyChildList = xeBody.Elements("ITEM").ToList();
                InterfaceCommon interfaceCommon = new InterfaceCommon();
                if (appCode == "1")
                {
                    if (dictType.ToUpper() == "DEPT")
                        brd = interfaceCommon.DeptSyncData(xeBodyChildList);
                    else if (dictType.ToUpper() == "EMPLOYEE")
                        brd = interfaceCommon.EmployeeSyncData(xeBodyChildList);
                    else if (dictType.ToUpper() == "REACENORG")
                        brd = interfaceCommon.ReaCenOrgSyncData(xeBodyChildList);
                    else if (dictType.ToUpper() == "REAGOODS")
                        brd = interfaceCommon.ReaGoodsSyncData(xeBodyChildList);
                    else if (dictType.ToUpper() == "REASTORAGE")
                        brd = interfaceCommon.ReaStorageSyncData(xeBodyChildList);
                }
            }
            else
            {
                brd.success = false;
                brd.message = "接口服务invoke--"+ serviceName + "返回值为空！";
                ZhiFang.Common.Log.Log.Info(brd.message);
            }
            return brd;
        }

        public BaseResultData GetReaGoodInfo(ref IList<ReaCenOrg> listReaCenOrg, ref IList<ReaGoods> listReaGoods)
        {
            BaseResultData brd = new BaseResultData();
            string paraBase = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?><ROOT><LEAF></LEAF></ROOT>";
            string serviceName =  "DepartmentQueryToZFHPXX";
            WSEntryClassService wsEntryClassService = new WSEntryClassService();
            //string strXML = XDocument.Load("D:\\接口文档说明\\二龙路医院接口文档\\接口测试程序\\接口测试返回的xml\\ReaGoods.xml").ToString();
            string strXML = wsEntryClassService.invoke(serviceName, "","", paraBase);
            ZhiFang.Common.Log.Log.Info("接口服务invoke--" + serviceName + "返回值：" + strXML);
            if (!string.IsNullOrEmpty(strXML))
            {
                StringReader StrStream = new StringReader(strXML);
                XDocument xml = XDocument.Load(StrStream);
                XElement xeRoot = xml.Root;//根目录
                XElement xeBody = xeRoot.Element("LEAF");
                string appCode = xeBody.Attribute("APPCODE").Value;
                IList<XElement> xeBodyChildList = xeBody.Elements("ITEM").ToList();
                InterfaceCommon interfaceCommon = new InterfaceCommon();
                if (appCode == "1")
                {
                    brd = interfaceCommon.ReaGoodsSyncData(xeBodyChildList, ref listReaCenOrg, ref listReaGoods);
                }
            }
            else
            {
                brd.success = false;
                brd.message = "接口服务invoke--" + serviceName + "返回值为空！";
                ZhiFang.Common.Log.Log.Info(brd.message);
            }
            return brd;
        }

        public BaseResultData GetReaSaleDocInfo(string saleDocNo, IBReaBmsCenSaleDoc IBReaBmsCenSaleDoc, HREmployee emp, CenOrg cenOrg, ref ReaBmsCenSaleDoc saleDoc, ref IList<ReaBmsCenSaleDtl> saleDtlList, ref long storageID)
        {
            BaseResultData brd = new BaseResultData();
            string paraBase = "<?xml version=\"1.0\" encoding=\"UTF - 8\"?><ROOT><LEAF SaleDocNo=\"" + saleDocNo + "\"></LEAF></ROOT> ";

            WSEntryClassService wsEntryClassService = new WSEntryClassService();
            string strSaleDocXML = wsEntryClassService.invoke("DepartmentQueryToZFGHZD01", "", "", paraBase);
            ZhiFang.Common.Log.Log.Info("接口服务invoke--DepartmentQueryToZFGHZD01返回值：" + strSaleDocXML);
            string strSaleDtlXML = wsEntryClassService.invoke("DepartmentQueryToZFGHZD02", "", "", paraBase);
            ZhiFang.Common.Log.Log.Info("接口服务invoke--DepartmentQueryToZFGHZD02返回值：" + strSaleDtlXML);
            if ((!string.IsNullOrEmpty(strSaleDocXML)) && (!string.IsNullOrEmpty(strSaleDtlXML)))
            {
                StringReader StrStream = new StringReader(strSaleDocXML);
                XDocument xml = XDocument.Load(StrStream);
                XElement xeRoot = xml.Root;//根目录
                XElement xeBody = xeRoot.Element("LEAF");
                string appCode = xeBody.Attribute("APPCODE").Value;
                IList<XElement> xeBodyChildList = xeBody.Elements("ITEM").ToList();
                StringReader StrStream2 = new StringReader(strSaleDtlXML);
                XDocument xml2 = XDocument.Load(StrStream2);
                XElement xeRoot2 = xml2.Root;//根目录
                XElement xeBody2 = xeRoot2.Element("LEAF");
                string appCode2 = xeBody2.Attribute("APPCODE").Value;
                IList<XElement> xeBodyChildList2 = xeBody2.Elements("ITEM").ToList();
                InterfaceCommon interfaceCommon = new InterfaceCommon();
                if (appCode == "1" && appCode2 == "1")
                {

                    IBReaBmsCenSaleDoc.AddReaBmsCenSaleDocByInterface(xeBodyChildList, xeBodyChildList2, cenOrg, emp, ref saleDoc, ref saleDtlList, false);
                }
            }
            else
            {
                brd.success = false;
                brd.message = "接口服务invoke--DepartmentQueryToZFGHZD01或02返回值为空！";
                ZhiFang.Common.Log.Log.Info(brd.message);
            }
            return brd;
        }


    }
}
