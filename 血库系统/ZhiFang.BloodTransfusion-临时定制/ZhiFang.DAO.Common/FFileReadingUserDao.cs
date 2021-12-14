using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Common;
using ZhiFang.IDAO.Common;

namespace ZhiFang.DAO.NHB.Common
{
    public class FFileReadingUserDao : BaseDaoNHB<FFileReadingUser, long>, IDFFileReadingUserDao
	{
        public bool DeleteByFFileId(long fFileId)
        {
            int result = 0;
            result = this.HibernateTemplate.Delete("From FFileReadingUser ffilereadinguser where ffilereadinguser.FFile.Id=" + fFileId);
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