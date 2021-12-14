using ZhiFang.Entity.Common;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.Common
{
    public interface IDFFileCopyUserDao : IDBaseDao<FFileCopyUser, long>
    {
        bool DeleteByFFileId(long fFileId);
        
    }
}