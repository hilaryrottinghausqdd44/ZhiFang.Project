using System.Data;
using ZhiFang.Entity.Base;

namespace ZhiFang.IDAO.LabStar
{
    public interface IDLisCommonDao
    {
        /// <summary>
        /// 根据实体名称和指定字段名称获取该字段的最大值
        /// </summary>
        /// <param name="entityName">实体名称</param>
        /// <param name="fieldName">实体字段名</param>
        /// <returns></returns>
        int GetMaxNoByFieldNameDao(string entityName, string fieldName);

        BaseResultDataValue ExecSQLDao(string strSQL);
        DataSet QuerySQLDao(string strSQL);
    }
}