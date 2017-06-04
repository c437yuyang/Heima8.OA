using System.Web;
using System.Web.Mvc;
using Heima8.OA.UI.Portal.Models;

namespace Heima8.OA.UI.Portal
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            //filters.Add(new HandleErrorAttribute());
            filters.Add(new MyExceptionFilter());
        }
    }
}