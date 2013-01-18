using System.Web;
using System.Web.Mvc;

namespace EmptyMvcForTesting
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}