using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Model;
using ZhiFang.Model.DownloadDict;
using ZhiFang.Model.UiModel;

namespace ZhiFang.IBLL.Common.BaseDictionary
{
    public interface IBDownloadDictHelp
    {
        string GetClientNo(string labcode);
        DownloadDictEntity<Model.GroupItem> DownloadGroupItem(string maxDataTimeStamp);
        DownloadDictEntity<D_Department> DownloadDepartment(string maxDataTimeStamp);
        DownloadDictEntity<D_ItemColorDict> DownloadItemColorDict(string maxDataTimeStamp);
        DownloadDictEntity<D_ItemColorAndSampleTypeDetail> DownloadItemColorAndSampleTypeDetail(string maxDataTimeStamp);
        DownloadDictEntity<D_BPhysicalExamType> DownloadBPhysicalExamType(string maxDataTimeStamp);
        DownloadDictEntity<D_Lab_SampleType> DownloadLabSampleTypeByLabCode(string labcode, string maxDataTimeStamp);
        DownloadDictEntity<D_Lab_TestItem> DownloadLabTestItemByLabCode(string labcode, string maxDataTimeStamp);
        DownloadDictEntity<Model.Lab_GroupItem> DownloadBLabGroupItemByLabCode(string labcode, string maxDataTimeStamp);
        DownloadDictEntity<D_BLabSickType> DownloadBLabSickTypeByLabCode(string labcode, string maxDataTimeStamp);
        DownloadDictEntity<D_BLabDoctor> DownloadBLabDoctorByLabCode(string labcode, string maxDataTimeStamp);
        //DownloadDictEntity<D_BLabDepartment> DownloadBLabDepartmentByLabCode(string labcode, string maxDataTimeStamp);
        DownloadDictEntity<D_BLabFolkType> DownloadBLabFolkTypeByLabCode(string labcode, string maxDataTimeStamp);
        
    }
}
