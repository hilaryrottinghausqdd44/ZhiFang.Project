using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using System.Collections.Generic;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.Linq;
using System.IO;
using ZhiFang.ReagentSys.Client.Common;
using System;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaGoodsLot : BaseBLL<ReaGoodsLot>, ZhiFang.IBLL.ReagentSys.Client.IBReaGoodsLot
    {
        IDReaGoodsDao IDReaGoodsDao { get; set; }
        public override bool Add()
        {
            ReaGoodsLot reaGoodsLot = null;
            BaseResultBool baseResultBool = this.EditValid(ref reaGoodsLot);
            if (baseResultBool.success == false) return false;

            this.Entity.DataUpdateTime = System.DateTime.Now;
            //当前货品批号的性能验证判断处理
            ReaGoods reaGoods = null;
            if (!this.Entity.IsNeedPerformanceTest.HasValue)
            {
                if (this.Entity.GoodsID.HasValue)
                {
                    reaGoods = IDReaGoodsDao.Get(this.Entity.GoodsID.Value);
                }
                else if (!string.IsNullOrEmpty(this.Entity.ReaGoodsNo))
                {
                    IList<ReaGoods> tempList = IDReaGoodsDao.GetListByHQL(" reagoods.Visible=1 and reagoods.ReaGoodsNo='" + this.Entity.ReaGoodsNo + "'");
                    if (tempList != null && tempList.Count > 0)
                    {
                        reaGoods = tempList[0];
                    }
                }
            }
            if (reaGoods != null)
            {
                if (string.IsNullOrEmpty(this.Entity.ReaGoodsNo))
                    this.Entity.ReaGoodsNo = reaGoods.ReaGoodsNo;
                this.Entity.IsNeedPerformanceTest = reaGoods.IsNeedPerformanceTest;
            }
            if (!this.Entity.VerificationStatus.HasValue)
                this.Entity.VerificationStatus = long.Parse(ReaGoodsLotVerificationStatus.未验证.Key);
            reaGoodsLot = this.Entity;
            bool a = DBDao.Save(this.Entity);
            return a;
        }
        public override bool Update(string[] strParas)
        {
            ReaGoodsLot reaGoodsLot = null;
            BaseResultBool baseResultBool = this.EditValid(ref reaGoodsLot);
            //if (baseResultBool.success == false) return false;
            return DBDao.Update(strParas);
        }
        public BaseResultBool AddAndValid(ref ReaGoodsLot reaGoodsLot)
        {
            BaseResultBool baseResultBool = this.EditValid(ref reaGoodsLot);
            if (baseResultBool.success == false) return baseResultBool;
            baseResultBool.success = this.Add();
            reaGoodsLot = this.Entity;
            return baseResultBool;
        }
        public BaseResultBool EditValid(ref ReaGoodsLot reaGoodsLot)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            //reaGoodsLot = this.Entity;
            if (string.IsNullOrEmpty(this.Entity.LotNo))
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "货品批号不能为空!";
                return baseResultBool;
            }
            if (string.IsNullOrEmpty(this.Entity.ReaGoodsNo) && this.Entity.GoodsID.HasValue)
            {
                ReaGoods reaGoods = IDReaGoodsDao.Get(this.Entity.GoodsID.Value);
                if (reaGoods != null)
                    this.Entity.ReaGoodsNo = reaGoods.ReaGoodsNo;
            }
            if (string.IsNullOrEmpty(this.Entity.ReaGoodsNo))
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "货品批号的货品编码不能为空!";
                return baseResultBool;
            }
            if (!this.Entity.InvalidDate.HasValue)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "有效期不能为空!";
                return baseResultBool;
            }

            IList<ReaGoodsLot> tempList = this.SearchListByHQL(string.Format("reagoodslot.Id!={0} and reagoodslot.LotNo='{1}' and reagoodslot.ReaGoodsNo='{2}' and reagoodslot.LabID={3}", this.Entity.Id, this.Entity.LotNo, this.Entity.ReaGoodsNo, this.Entity.LabID));
            if (tempList != null && tempList.Count > 0)
            {
                reaGoodsLot = tempList.OrderByDescending(p => p.DataAddTime).ElementAt(0);
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = string.Format("货品批号为{0},货品编码为{1},已经存在!", this.Entity.LotNo, this.Entity.ReaGoodsNo);
                //ZhiFang.Common.Log.Log.Error(baseResultBool.ErrorInfo);
                return baseResultBool;
            }
            else
            {
                reaGoodsLot = this.Entity;
                baseResultBool.success = true;
                return baseResultBool;
            }
        }
        public IList<ReaGoodsLot> SearchReaGoodsLotListByAllJoinHql(string where, string reaGoodsHql, string sort, int page, int limit)
        {
            IList<ReaGoodsLot> entityList = new List<ReaGoodsLot>();
            entityList = ((IDReaGoodsLotDao)base.DBDao).SearchReaGoodsLotListByAllJoinHql(where, reaGoodsHql, sort, page, limit);
            return entityList;
        }
        public EntityList<ReaGoodsLot> SearchReaGoodsLotEntityListByAllJoinHql(string where, string reaGoodsHql, string sort, int page, int limit)
        {
            EntityList<ReaGoodsLot> entityList = new EntityList<ReaGoodsLot>();
            entityList = ((IDReaGoodsLotDao)base.DBDao).SearchReaGoodsLotEntityListByAllJoinHql(where, reaGoodsHql, sort, page, limit);
            return entityList;
        }

        public Stream SearchReaGoodsLotOfExcelByHql(long labId, string labCName, string breportType, string where, string reaGoodsHql, string sort, string frx, ref string fileName)
        {
            Stream stream = null;
            if (string.IsNullOrEmpty(sort)) sort = "";
            IList<ReaGoodsLot> entityList = this.SearchReaGoodsLotListByAllJoinHql(where, reaGoodsHql, sort, -1, -1);
            if (string.IsNullOrEmpty(frx))
                frx = "批号性能验证清单.xlsx";
            string saveFullPath = "";
            string fileExt = frx.Substring(frx.LastIndexOf("."));
            string excelFile = "批号性能验证清单" + fileExt;
            for (int i = 0; i < entityList.Count; i++)
            {
                if (entityList[i].VerificationStatus.HasValue)
                    entityList[i].VerificationStatusCName = ReaGoodsLotVerificationStatus.GetStatusDic()[entityList[i].VerificationStatus.ToString()].Name;
                //机构货品验证说明信息
                entityList[i] = GetVerificationMemo(entityList[i]);
            }
            ExportExcelCommand excelCommand = ExportDataToExcelHelp.CreateExportExcelCommand("", "");
            stream = ExportDataToExcelHelp.ExportDataToXSSFSheet<ReaGoodsLot, ReaGoodsLot>(null, entityList, excelCommand, breportType, labId, frx, excelFile, ref saveFullPath);
            fileName = "批号性能验证清单信息" + fileExt;
            return stream;
        }
        public ReaGoodsLot GetVerificationMemo(ReaGoodsLot entity)
        {
            if (!string.IsNullOrEmpty(entity.ReaGoodsNo))
            {
                IList<ReaGoods> entityList2 = IDReaGoodsDao.GetListByHQL("reagoods.VerificationMemo is not null and reagoods.ReaGoodsNo='" + entity.ReaGoodsNo + "'");
                if (entityList2.Count > 0)
                {
                    foreach (var item in entityList2)
                    {
                        if (!string.IsNullOrEmpty(item.VerificationMemo))
                        {
                            entity.VerificationMemo = item.VerificationMemo;
                            break;
                        }
                    }
                }
            }
            return entity;
        }
        public BaseResultBool UpdateVerification(ReaGoodsLot entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity == null)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "entity参数为空!";
                return baseResultBool;
            }
            if (string.IsNullOrEmpty(entity.LotNo))
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "货品批号不能为空!";
                return baseResultBool;
            }
            if (string.IsNullOrEmpty(entity.ReaGoodsNo) && entity.GoodsID.HasValue)
            {
                ReaGoods reaGoods = IDReaGoodsDao.Get(entity.GoodsID.Value);
                if (reaGoods != null)
                    entity.ReaGoodsNo = reaGoods.ReaGoodsNo;
            }
            if (string.IsNullOrEmpty(entity.ReaGoodsNo))
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "货品批号的货品编码不能为空!";
                return baseResultBool;
            }
            if (!entity.VerificationStatus.HasValue)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "货品信息的性能验证结果为空!";
                return baseResultBool;
            }
            if (!string.IsNullOrEmpty(fields))
                fields = fields + ",DataUpdateTime";
            entity.DataUpdateTime = DateTime.Now;

            try
            {
                this.Entity = entity;
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(this.Entity, fields);
                    if (tempArray != null)
                    {
                        baseResultBool.success = this.Update(tempArray);
                    }
                    // baseResultBool.success = this.Edit();
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    baseResultBool.success = this.Edit();
                }

            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Error("货品批号性能验证处理:错误信息" + ex.StackTrace);
            }
            return baseResultBool;
        }
    }
}