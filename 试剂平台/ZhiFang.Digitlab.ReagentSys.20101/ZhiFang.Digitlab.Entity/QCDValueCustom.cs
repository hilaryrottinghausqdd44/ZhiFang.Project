using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ZhiFang.Common.Public;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ZhiFang.Digitlab.Entity
{

    [DataContract]
    [DataDesc(CName = "优化查询质控数据速度使用", ClassCName = "QCDValueCustom", ShortCode = "QCDValueCustom", Desc = "优化查询质控数据速度使用")]
    public class QCDValueCustom : QCDataCustom
    {
        public QCDValueCustom(long Id, long labID, DateTime? receiveTime, int qCDataLotNo, string origlValue, string reportValue, string cVValue, int resultStatus, int isControl, string qCControlInfo, bool isUse, bool isEquipResult, string qcComment, string operatorName, byte[] dataTimeStamp, long qcItemID, string qcItemName, string qcItemSName, int qcItemValueType, byte[] qcItemDataTimeStamp, double? target, double? sd, double? cv, double? calcTarget, double? calcSD, long itemID, long qcMatID, string qcMatName, string qcMatSName, string manu, string concLevel, string qcMatLotNo, long equipID, string equipName, string equipSName, bool qcdValueIsUse, int qcMatDispOrder, int qcItemDispOrder, string qcMatTimeDesc, string qcItemDesc, int qcItemPrecision, DateTime qcItemTimeBeginDate, DateTime? qcItemTimeEndDate)
        {
            this.Id = Id;
            this.LabID = labID;
            this.ReceiveTime = receiveTime;
            this.QCDataLotNo = qCDataLotNo;
            this.OriglValue = origlValue;
            this.ReportValue = reportValue;
            this.CVValue = cVValue;
            this.ResultStatus = resultStatus;
            this.IsControl = isControl;
            this.QCControlInfo = qCControlInfo;
            this.IsUse = isUse;
            this.IsEquipResult = isEquipResult;
            this.QCComment = qcComment;
            this.QCDValueIsUse = qcdValueIsUse;
            //this.Operator = hrEmployee;
            this.OperatorName = operatorName;
            this.DataTimeStamp = dataTimeStamp;
            //质控项目相关
            this.QCItemID = qcItemID;
            this.QCItemName = qcItemName;
            this.QCItemSName = qcItemSName;
            this.QCItemValueTypeInt = qcItemValueType;
            this.QCItemDataTimeStamp = qcItemDataTimeStamp;
            this.ItemID = itemID;
            this.QCItemDispOrder = qcItemDispOrder;
            this.QCItemPrecision = qcItemPrecision;
            //质控项目时效相关
            this.Target = target == null ? "" : target.Value.ToString();
            this.SD = sd == null ? "" : sd.Value.ToString();
            this.CV = cv == null ? "" : cv.Value.ToString();
            this.CalcTarget = calcTarget == null ? "" : calcTarget.Value.ToString();
            this.CalcSD = calcSD == null ? "" : calcSD.Value.ToString();
            //质控物相关
            this.QCMatID = qcMatID;
            this.QCMatName = qcMatName;
            this.QCMatSName = qcMatSName;
            this.Manu = manu;
            this.ConcLevel = concLevel;
            this.QCMatLotNo = qcMatLotNo;
            this.QCMatDispOrder = qcMatDispOrder;
            //仪器相关
            this.EquipID = equipID;
            this.EquipName = equipName;
            this.EquipSName = equipSName;
            //时效描述
            this.QCMatAndItemDesc = qcMatTimeDesc + " " + qcItemDesc;
            this.QCItemTimeBeginDate = qcItemTimeBeginDate.ToString("yyyy-MM-dd");
            this.QCItemTimeEndDate = qcItemTimeEndDate == null ? null : qcItemTimeEndDate.Value.ToString("yyyy-MM-dd");
        }
        
        public QCDValueCustom() { }

        public override long Id { get; set; }
        
        public virtual int ResultStatus { get; set; }

        public virtual int IsControl { get; set; }

        public virtual bool IsUse { get; set; }

        public virtual QCItem QCItem { get; set; }

        public virtual string OperatorName { get; set; }
                
        public virtual int QCItemValueTypeInt { get; set; }

        public virtual int QCItemPrecision { get; set; }

    }


}
