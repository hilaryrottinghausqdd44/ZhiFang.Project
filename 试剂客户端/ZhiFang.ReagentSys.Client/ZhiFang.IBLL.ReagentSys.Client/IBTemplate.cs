

using System.Collections.Generic;
using System.IO;
using System.Web;
using Newtonsoft.Json.Linq;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBTemplate : IBGenericManager<BTemplate>
    {
        /// <summary>  
        /// 获取公共模板目录的子文件夹中的所有报表模板文件  
        /// </summary>  
        /// <param name="templateType">报表模板分类</param>    
        /// <returns></returns>  
        EntityList<JObject> GetPublicTemplateDirFile(string publicTemplateDir);
        /// <summary>
        /// 将选择的公共报表模板新增保存到当前实验室的报表模板表
        /// </summary>
        /// <param name="jarray"></param>
        /// <returns></returns>
        BaseResultBool AddBTemplateOfPublicTemplate(JArray jarray, long labId, string labCName, long empID, string empName);
        /// <summary>
        /// 获取当前机构的某一模板类型的全部报表模板信息,如果当前机构未维护,取该模板类型的公共报表模板信息
        /// </summary>
        /// <param name="labId"></param>
        /// <param name="breportType">模板类型(BTemplateType的key)</param>
        /// <returns></returns>
        EntityList<JObject>  SearchBTemplateByLabIdAndType(long labId, long breportType, string publicTemplateDir);
        BaseResultDataValue AddTemplateUploadFile(HttpPostedFile file);
        BaseResultBool UpdateTemplateUploadFileByField(string[] tempArray, HttpPostedFile file);
        FileStream GetTemplateFileStream(long id, ref string fileName);
    }
}