using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.IDAL;
using ZhiFang.IBLL.Common.BaseDictionary;

namespace ZhiFang.BLL.Common.BaseDictionary
{
    public partial class ItemColorAndSampleTypeDetail : IBItemColorAndSampleTypeDetail
    {
        IDAL.IDItemColorAndSampleTypeDetail dal;
        public ItemColorAndSampleTypeDetail()
        {
            dal = DALFactory.DalFactory<IDAL.IDItemColorAndSampleTypeDetail>.GetDal("ItemColorAndSampleTypeDetail", ZhiFang.Common.Dictionary.DBSource.LisDB());
        }


        public Model.UiModel.UiItemColorSampleTypeNo GetItemColorAndSampleDetail(string ColorId)
        {
            return dal.GetItemColorAndSampleDetail(ColorId);
        }

        public bool SaveToItemColorAndSampleTypeDetail(List<Model.ItemColorAndSampleTypeDetail> ListModel)
        {
            return dal.SaveToItemColorAndSampleTypeDetail(ListModel);
        }

        public int Add(Model.ItemColorAndSampleTypeDetail model)
        {
            throw new NotImplementedException();
        }

        public int Update(Model.ItemColorAndSampleTypeDetail model)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet GetList(Model.ItemColorAndSampleTypeDetail model)
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount()
        {
            throw new NotImplementedException();
        }

        public int GetTotalCount(Model.ItemColorAndSampleTypeDetail model)
        {
            throw new NotImplementedException();
        }

        public System.Data.DataSet GetAllList()
        {
            throw new NotImplementedException();
        }
        
        public System.Data.DataSet GetListByPage(Model.ItemColorAndSampleTypeDetail t, int nowPageNum, int nowPageSize)
        {
            throw new NotImplementedException();
        }

        public int DeleteItemColorAndSampleTypeDetailByColorId(string ColorId)
        {
            return dal.DeleteItemColorAndSampleTypeDetailByColorId(ColorId);
        }
        public System.Data.DataSet DownloadItemColorAndSampleTypeDetail(int Top, string strWhere, string filedOrder)
        {
            return dal.DownloadItemColorAndSampleTypeDetail(Top, strWhere, filedOrder);
        }
    }
}
