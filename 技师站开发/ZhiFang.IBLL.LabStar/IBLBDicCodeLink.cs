using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBDicCodeLink : IBGenericManager<LBDicCodeLink>
    {
        EntityList<LBDicCodeLinkVO> SearchBasicsDicAndLBDicCodeLink(long SickTypeId, string type, string DicInfo, string CName, string sort, int page, int limit);
        EntityList<LBDicCodeLinkVO> SearchBasicsDicDataBySickTypeId(long SickTypeId, string type, string DicInfo, string CName);
        EntityList<LBDicCodeLinkVO> GetParaDicData(string type);
        BaseResultDataValue AddCopyLBDicCodeLinkContrast(string sickTypeIds, string thisSickTypeId, string dictype);
        HISInterfaceHISOrderFromVO HISOrderFormDataDicContrast(HISInterfaceHISOrderFromVO hISInterfaceHISOrderFrom,out bool DataFormatISOK);
    }
}