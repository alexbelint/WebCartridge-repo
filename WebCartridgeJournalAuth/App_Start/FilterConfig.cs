using System.Web;
using System.Web.Mvc;

namespace WebCartridgeJournalAuth
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
