using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ZhiFang.IBLL.Common.BaseDictionary
{
    public interface IBItemColorAndSampleTypeDetail : IBBase<ZhiFang.Model.ItemColorAndSampleTypeDetail>, IBDataPage<ZhiFang.Model.ItemColorAndSampleTypeDetail>
    {

        Model.UiModel.UiItemColorSampleTypeNo GetItemColorAndSampleDetail(string ColorId);

        bool SaveToItemColorAndSampleTypeDetail(List<Model.ItemColorAndSampleTypeDetail> ListModel);

        int DeleteItemColorAndSampleTypeDetailByColorId(string ColorId);
        DataSet DownloadItemColorAndSampleTypeDetail(int Top, string strWhere, string filedOrder);
    }
}
