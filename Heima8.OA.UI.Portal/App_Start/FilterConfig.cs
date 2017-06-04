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
            filters.Add(new LoginCheckFilterAttribute(){IsCheck = true}); //这里加上true就说明默认所有网页都要经过logincheck
        }
    }
}