using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.Digitlab.ReagentSys.ServerContract;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.ServiceCommon;
using System.Web;
using ZhiFang.Common.Public;
using System.IO;
using System.Data;
using ZhiFang.Digitlab.ReagentSys.BusinessObject;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.Response;
using System.ServiceModel.Channels;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace ZhiFang.Digitlab.ReagentSys
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ReaManageService : IReaManageService
    {
        IBLL.ReagentSys.IBReaCenOrg IBReaCenOrg { get; set; }

        IBLL.ReagentSys.IBReaGoods IBReaGoods { get; set; }

        IBLL.ReagentSys.IBReaDeptGoods IBReaDeptGoods { get; set; }

        IBLL.ReagentSys.IBReaGoodsOrgLink IBReaGoodsOrgLink { get; set; }


        public Message RM_UDTO_UploadGoodsDataByExcel()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                if (iTotal == 0)
                {
                    baseResultDataValue.ErrorInfo = "未检测到文件！";
                    baseResultDataValue.ResultDataValue = "";
                    baseResultDataValue.success = false;
                }
                HttpPostedFile file = HttpContext.Current.Request.Files[0];
                int len = file.ContentLength;
                if (len > 0 && !string.IsNullOrEmpty(file.FileName))
                {
                    string parentPath = HttpContext.Current.Server.MapPath("~/UploadBaseTableInfo/");
                    if (!Directory.Exists(parentPath))
                    {
                        Directory.CreateDirectory(parentPath);
                    }
                    string LabID = HttpContext.Current.Request.Form["LabID"];
                    string CompID = HttpContext.Current.Request.Form["CompID"];
                    string ProdID = HttpContext.Current.Request.Form["ProdID"];
                    string filepath = Path.Combine(parentPath, Common.Public.GUIDHelp.GetGUIDString() + '_' + Path.GetFileName(file.FileName));
                    file.SaveAs(filepath);
                    baseResultDataValue = IBReaGoods.CheckGoodsExcelFormat(filepath, HttpContext.Current.Server.MapPath("~/"));
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue = IBReaGoods.AddGoodsDataFormExcel(LabID, CompID, ProdID, filepath, HttpContext.Current.Server.MapPath("~/"));
                    }
                }
                else
                {
                    baseResultDataValue.ErrorInfo = "文件大小为0或为空！";
                    baseResultDataValue.success = false;
                };
            }
            catch (Exception ex)
            {
                baseResultDataValue.ErrorInfo = ex.Message;
                baseResultDataValue.ResultDataValue = "";
                baseResultDataValue.success = false;
            }
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultDataValue);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        } //

        public BaseResultDataValue RM_UDTO_SearchReaGoodsByHRDeptID(long deptID, string where, int page, int limit, string fields, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaDeptGoods> entityList = new EntityList<ReaDeptGoods>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                    sort = CommonServiceMethod.GetSortHQL(sort);

                entityList = IBReaDeptGoods.SearchReaGoodsByHRDeptID(deptID, where, sort, page, limit);

                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaDeptGoods>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

    }
}
