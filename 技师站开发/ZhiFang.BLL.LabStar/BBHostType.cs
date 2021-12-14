using System;
using System.Collections.Generic;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IDAO.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public class BBHostType : BaseBLL<BHostType>, ZhiFang.IBLL.LabStar.IBBHostType
    {
        IDBParaItemDao IDBParaItemDao { get; set; }

        public EntityList<BHostType> SearchBHostTypeNotPara(int page, int limit, string where, string sort)
        {
            EntityList<BHostType> entityList = new EntityList<BHostType>();           
            List<string> paratypes = new List<string>(); 
            foreach (var item in Pre_AllModules.GetStatusDic())
            {
                paratypes.Add(item.Value.DefaultValue);
            }
            IList<object> notetypes = IDBParaItemDao.QueryParaSystemTypeInfoByParaTypeCodesDao(Para_SystemType.检验站点相关.Key,string.Join("','", paratypes));
            List<long> ids = new List<long>();
            if(notetypes.Count > 0){
                foreach (var item in notetypes)
                {
                    Array dataarr = (Array)item;
                    ids.Add(long.Parse(dataarr.GetValue(0).ToString()));
                }
            }
            if (ids.Count > 0) {
                where += " and Id not in(" + string.Join(",", ids) + ")";
            }
            entityList = DBDao.GetListByHQL(where,sort, page, limit);
            return entityList;
        }
    }
}