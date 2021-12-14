using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ZhiFang.BLL.Base;
using ZhiFang.Common.Log;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;

namespace ZhiFang.BLL.LabStar
{
    /// <summary>
    /// 
    /// </summary>
    public class BLisEquipComLog : BaseBLL<LisEquipComLog>, ZhiFang.IBLL.LabStar.IBLisEquipComLog
    {
        public BaseResultDataValue AddLisEquipComLog(int comLogType, string comLogInfo, ClientComputerInfo computerInfo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            LisEquipComLog equipComLog = new LisEquipComLog
            {
                ComLogType = comLogType,
                ComLogInfo = comLogInfo,
                ComLogTime = DateTime.Now,
            };
            if (computerInfo != null)
            {
                equipComLog.ComFileID = computerInfo.SComFileID;
                equipComLog.EquipID = computerInfo.SEquipID;
                equipComLog.EquipName = computerInfo.SEquipName;
                equipComLog.SectionID = computerInfo.SSectionID;
                equipComLog.SectionName = computerInfo.SSectionName;
            }
            this.Entity = equipComLog;
            brdv.success = this.Add();
            return brdv;
        }
    }

}