using ZhiFang.IDAO.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.Base;
using System.Text;
using System.Collections.Generic;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    public interface IDReaBmsCheckDocDao : IDBaseDao<ReaBmsCheckDoc, long>
    {
        /// <summary>
        /// (入库,移库,出库,退库,盘库)等操作,新增或更新库存数据,需要先判断(选择的供应商,库房,货架或全部)是否正在被盘库锁定
        /// </summary>
        /// <param name="reaCompanyID">供应商ID</param>
        /// <param name="companyName"></param>
        /// <param name="storageID">库房ID</param>
        /// <param name="storageName"></param>
        /// <param name="placeID">货架ID</param>
        /// <param name="placeName"></param>
        /// <param name="goodId">机构货品Id</param>
        /// <returns></returns>
        BaseResultBool EditValidIsLock(long? reaCompanyID, string companyName, long? storageID, string storageName, long? placeID, string placeName, long? goodId);
        /// <summary>
        /// 盘库保存前验证盘库条件是否被盘库锁定
        /// </summary>
        /// <param name="checkDoc">盘库条件实体</param>
        /// <param name="reaGoodHql">传入的机构货品盘库条件</param>
        /// <returns></returns>
        BaseResultBool EditValidIsLock(ReaBmsCheckDoc checkDoc, string reaGoodHql);
        /// <summary>
        /// 盘库保存前验证盘库条件是否被盘库锁定
        /// 盘库条件为供应商或库房,或货架及机构货品
        /// </summary>
        /// <param name="checkDoc">盘库条件实体</param>
        /// <param name="dtAddList">盘库货品明细</param>
        /// <returns></returns>
        BaseResultBool EditValidIsLock(ReaBmsCheckDoc checkDoc, IList<ReaBmsCheckDtl> dtAddList);
        /// <summary>
        /// 获取盘库条件HQL
        ///注意:机构货品盘库条件需要单独处理
        /// </summary>
        /// <param name="checkDoc">盘库条件实体</param>
        /// <param name="isLock">锁定标志:-1:不按锁定标志处理;</param>
        /// <param name="checkMemo">库条件说明</param>
        /// <returns></returns>
        string GetIsLockCheckHql(ReaBmsCheckDoc checkDoc, int isLock, ref StringBuilder checkMemo);
    }
}