using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ZhiFang.IDAL
{
    public interface IDItemColorAndSampleTypeDetail : IDataBase<Model.ItemColorAndSampleTypeDetail>, IDataPage<Model.ItemColorAndSampleTypeDetail>
    {
        Model.UiModel.UiItemColorSampleTypeNo GetItemColorAndSampleDetail(string ColorId);

        bool SaveToItemColorAndSampleTypeDetail(List<Model.ItemColorAndSampleTypeDetail> ListModel);
        int DeleteItemColorAndSampleTypeDetailByColorId(string ColorId);
        DataSet DownloadItemColorAndSampleTypeDetail(int Top, string strWhere, string filedOrder);
    }
}
