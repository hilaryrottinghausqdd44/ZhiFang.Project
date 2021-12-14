using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Common;
using ZhiFang.IDAO.Common;


namespace ZhiFang.DAO.NHB.Common
{	
	public class FFileCopyUserDao : BaseDaoNHB<FFileCopyUser, long>, IDFFileCopyUserDao
	{
        public bool DeleteByFFileId(long fFileId) {
            int result = 0;
            result = this.HibernateTemplate.Delete("From FFileCopyUser ffilecopyuser where ffilecopyuser.FFile.Id = " + fFileId);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return true;
            }
        }

    } 
}