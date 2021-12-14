using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBPrintModel : IBGenericManager<BPrintModel>
    {
        EntityList<BPrintModelVO> LS_UDTO_SearchBPrintModelAndAutoUploadModel(string BusinessTypeCode, string ModelTypeCode, string where, string sort);
        string FileNameReconsitution(string path, string code, string mtcode, string labid, string filename, out string newfilename);
        string PrintDataByPrintModel(string Data, long PrintModelID);
        string ExportDataByPrintModel(string Data, long PrintModelID);

        string PrePrintGatherVoucherByPrintModel(List<ProofLisBarCodeFormVo> forms, List<ProofLisBarCodeItemVo> items,string modelcode);
    }
}