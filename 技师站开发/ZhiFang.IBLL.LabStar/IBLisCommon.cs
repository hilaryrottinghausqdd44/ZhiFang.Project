using System.Collections.Generic;
using System.Data;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLisCommon
    {
        /// <summary>
        /// 根据实体名称和指定字段名称获取该字段的最大值
        /// </summary>
        /// <param name="entityName">实体名称</param>
        /// <param name="fieldName">实体字段名</param>
        /// <returns></returns>
        string GetMaxNoByFieldName(string entityName, string fieldName);

        int GetMaxNoByFieldName<T>(string fieldName);

        BaseResultDataValue AddCommonBaseRelationEntity<T>(IBGenericManager<T> commonIB, IList<T> addEntityList, bool isCheckEntityExist, bool isDelExist, Dictionary<string, object> propertyList, string delIDList) where T : BaseEntity;

        BaseResultDataValue DeleteCommonBaseRelationEntity<T>(IBGenericManager<T> commonIB, string entityIDList);

        BaseResultDataValue AddEntityDataFormByExcelFile(string entityName, string excelFilePath, string serverPath);

        BaseResultDataValue AddEntityDataFormByExcelFile<T>(string entityName, string entityCName, IBGenericManager<T> commonIB, string excelFilePath, string serverPath) where T : BaseEntity;

        DataSet QueryEntityDataInfo<T>(string entityName, IBGenericManager<T> commonIB, string idList, string where, string sort, string serverPath);

        BaseResultDataValue ExecSQL(string strSQL);

        DataSet QuerySQL(string strSQL);

    }
}