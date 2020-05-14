using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Prj.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prj.WebUI.TagHelpers
{
    [HtmlTargetElement("div", Attributes="page-model")]
    public class PagingTagHelper : TagHelper
    {
        private IUrlHelperFactory _urlHelperFactory;
        public PagingTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            _urlHelperFactory = urlHelperFactory;
        }

        [ViewContext]
        public ViewContext ViewContext { get; set; }
        public PageViewModel PageModel { get; set; }
        public string ActionName { get; set; }
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            output.TagName = "div";
            StringBuilder strBuilder = new StringBuilder();
            strBuilder.Append("<ul class='pagination' >");
            for (int i = 1; i <= PageModel.GetTotalPages(); i++)
            {
                strBuilder.AppendFormat("<li class='page-item {0}'>", i == PageModel.CurrentPage ? "active" : "");
                
                
                if (!string.IsNullOrEmpty(PageModel.CurrentObj))
                {
                    PageUrlValues["categoryId"] = PageModel.CurrentObj;
                }
                PageUrlValues["page"] = i;
                strBuilder.AppendFormat("<a class='page-link' href='{0}'>{1}</a>", urlHelper.Action(ActionName, PageUrlValues), i);
                strBuilder.Append("</li>");
            }
            strBuilder.Append("</ul>");
            output.Content.SetHtmlContent(strBuilder.ToString());
        }
    }
}
