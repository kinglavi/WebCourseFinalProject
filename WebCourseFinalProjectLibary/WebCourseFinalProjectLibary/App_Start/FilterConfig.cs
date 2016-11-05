using System.Web;
using System.Web.Mvc;

namespace WebCourseFinalProjectLibary
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
