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
    public class BLisEquipComFile : BaseBLL<LisEquipComFile>, ZhiFang.IBLL.LabStar.IBLisEquipComFile
    {
        public BaseResultDataValue AddLisEquipComFile(string equipResultType, string equipResultInfo, int equipResultCount, ClientComputerInfo computerInfo)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            LisEquipComFile equipComFile = new LisEquipComFile
            {
                ComFileName = computerInfo.ComFileName,
                ComFileComment = equipResultInfo,
                ComFileResultCount = equipResultCount,
                ComFileResultType = equipResultType,
                ComFileTime = DateTime.Now
            };
            if (computerInfo != null)
            {
                equipComFile.ClientComputer = computerInfo.ComputerName;
                equipComFile.ClientMac = computerInfo.MacAddress;
                equipComFile.ClientIP = computerInfo.IPAddress;
            }
            this.Entity = equipComFile;
            brdv.success = this.Add();
            if (brdv.success)
                brdv.ResultDataValue = equipComFile.Id.ToString();
            return brdv;
        }
    }

}