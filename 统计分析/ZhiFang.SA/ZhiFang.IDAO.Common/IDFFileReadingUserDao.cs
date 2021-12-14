using ZhiFang.Entity.Common;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.Common
{
    public interface IDFFileReadingUserDao : IDBaseDao<FFileReadingUser, long>
	{
        bool DeleteByFFileId(long fFileId);
        
    } 
}