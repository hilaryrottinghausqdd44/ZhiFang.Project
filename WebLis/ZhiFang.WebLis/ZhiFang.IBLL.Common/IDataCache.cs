using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZhiFang.IBLL.Common
{
    public interface IDataCache
    {
        object GetCache(string CacheKey);
        void SetCache(string CacheKey, object objObject);
        void SetCache(string CacheKey, object objObject, DateTime absoluteExpiration, TimeSpan slidingExpiration);
    }
}
