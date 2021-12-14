
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IBLL.WebAssist;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.WebAssist;

namespace ZhiFang.BLL.WebAssist
{
    /// <summary>
    ///
    /// </summary>
    public class BMEGroupSampleItem : BaseBLL<MEGroupSampleItem>, ZhiFang.IBLL.WebAssist.IBMEGroupSampleItem
    {
        public BaseResultDataValue SaveMEGroupSampleItemOfGK(MEGroupSampleForm docEntity, ref IList<MEGroupSampleItem> dtlEntityList, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = true;

            foreach (var entity in dtlEntityList)
            {
                entity.DataAddTime = DateTime.Now;
                entity.GroupSampleFormID = docEntity.Id;
                //this.Entity = entity;
                //bool result = this.Add();

                bool result = IDAO.NHB.WebAssist.DataAccess_SQL.CreateMEGroupSampleItemDao_SQL().Insert(entity);
                if (result == false)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "保存检验单项目明细失败！";
                    //throw new Exception(brdv.ErrorInfo);
                    break;
                }
            }
            return brdv;
        }
    }
}