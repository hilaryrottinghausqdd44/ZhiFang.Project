using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Common
{
    public interface IBRequestData
    {
        void UpdateBarCode(string barcodeFormNo,DataSet wsBarCode, string WebLisSourceOrgId, string WebLisOrgID);
        int UpLoadRequestFromBytes(byte[] xmlData, out string errorMsg);
        void UpdateNRequestForm( DataSet wsNRequestForm, string WebLisSourceOrgId, string WebLisOrgID);
        void UpdateNRequestItem(string barcodeFormNo, DataSet wsNRequestItem, DataSet wsNRequestForm, string WebLisSourceOrgId, string WebLisOrgID);
        int SaveWebLisData(string BarCodeNo, DataSet wsBarCode, DataSet wsNRequestItem, DataSet wsNRequestForm);
        int UpdateWebLisOrgID(DataSet wsBarCode, string WebLisOrgID);
        int UpdateItemWebLisOrgID(DataSet wsNRequestItem, string WebLisOrgID);
    }
}
