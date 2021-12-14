
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;
using ZhiFang.BLL.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.BloodTransfusion;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.BloodTransfusion;

namespace ZhiFang.BLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public class BBloodBagOperationDtl : BaseBLL<BloodBagOperationDtl>, ZhiFang.IBLL.BloodTransfusion.IBBloodBagOperationDtl
    {
        IDBDictDao IDBDictDao { get; set; }

        public BaseResultDataValue AddDtlListOfHandover(BloodBagOperation entityDoc, IList<BloodBagOperationDtl> bloodBagOperationDtlList, long empID, string empName)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            brdv.success = true;

            byte[] dataTimeStamp = { 1, 2, 3, 4, 5, 6, 7, 8 };
            if (entityDoc.DataTimeStamp == null)
                entityDoc.DataTimeStamp = dataTimeStamp;
            foreach (BloodBagOperationDtl entity in bloodBagOperationDtlList)
            {
                entity.BloodBagOperation = entityDoc;
                entity.DataUpdateTime = DateTime.Now;
                if (entity.BDict == null)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "传入参数交接记录项信息(BDict)为空!";
                    ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                    break;
                }
                BDict bdict = IDBDictDao.Get(entity.BDict.Id);
                if (bdict == null)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "获取交接记录项信息(BDict)为" + entity.BDict.Id + "的信息为空!";
                    ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                    break;
                }
                entity.BDict = bdict;
                this.Entity = entity;
                bool result = this.Add();
                if (!result)
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "血制品交接登记记录项保存失败!";
                    break;
                }
            }
            return brdv;
        }

    }
}