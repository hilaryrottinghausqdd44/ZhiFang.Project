using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBPara : IBGenericManager<BPara>
    {
        BaseResultDataValue AddAndEditPara(IList<BPara> listPara, string operaterID, string operater);

        IList<BPara> GetSystemDefaultPara(string paraTypeCode, string operaterID, string operater);

        IList<BPara> QueryFactoryParaListByParaClassName(string classname);

        IList<BaseClassDicEntity> QueryFactoryParaInfoByParaName(string paraName);

        BPara QuerySystemDefaultParaValueByParaNo(string paraNo);

        bool JudgeParaBoolValue(IList<BPara> listPara, string paraNo, string paraValue = "1");

        IList<PreParaEnumTypeEntity> QueryOrderBarCodeSelectFieldsByClassName(string classname);

       
    }
}