using System.Web;
using System.Web.Mvc;

namespace FurnitureStore
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            // Globalni filter koji sprecava kretanje sajtom za ne prijavljene korisnike
            filters.Add(new AuthorizeAttribute());

            // Na ovaj nacin onemogucava se pritup aplikaciji preko http protokola
            // dozvoljen je pritup samo preko https
            filters.Add(new RequireHttpsAttribute());
        }
    }
}
