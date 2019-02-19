using System.Web;
using System.Web.Mvc;

namespace GOLD.AppRegister.MVC_CRUD
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
