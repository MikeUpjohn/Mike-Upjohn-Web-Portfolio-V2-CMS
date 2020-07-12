using System.Web;
using System.Web.Mvc;

namespace MikeUpjohnWebPortfolioV2CMS
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
