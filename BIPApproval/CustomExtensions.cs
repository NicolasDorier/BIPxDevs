using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BIPApproval
{
    public static class CustomExtensions
    {
        public static IHtmlString Approval(this HtmlHelper html, Approval? value)
        {
            if(!value.HasValue)
                return html.Raw("-");
            var img =
                value.Value == BIPApproval.Approval.Yes ? "/Content/StatusAnnotations_Complete_and_ok_32xMD_color.png" : 
                value.Value == BIPApproval.Approval.No ? "/Content/StatusAnnotations_Blocked_32xMD_color.png" :
                "/Content/info.png";
            return html.Raw("<img src=\"" + img + "\" />");
        }
    }
}
