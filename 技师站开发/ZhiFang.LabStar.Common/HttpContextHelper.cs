using System.Web;

namespace ZhiFang.LabStar.Common
{
    public class HttpContextHelper
    {
        public static HttpContext httpcontext { get; set; }

        public static void Sethttpcontext()
        {
            System.Web.HttpContext.Current = httpcontext;
        }

        public static bool httpcontextFlag()
        {
            return !(System.Web.HttpContext.Current == null);
        }
    }
}
