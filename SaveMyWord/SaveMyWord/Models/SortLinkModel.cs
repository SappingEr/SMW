using System.Web.Helpers;
using System.Web.Routing;

namespace SaveMyWord.Models
{
    public class SortLinkModel
    {
        public string ActionName { get; set; }

        public string ControllerName { get; set; }

        public RouteValueDictionary RouteValues { get; set; }

        public string LinkText { get; set; }

        public System.Web.UI.WebControls.SortDirection? SortDirection { get; set; }
    }
}