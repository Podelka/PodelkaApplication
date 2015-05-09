using Podelka.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Podelka.Core.Source
{
    public static class PagingHelpersWorkroomProducts
    {
        public static MvcHtmlString PageLinksWorkroomProducts(this HtmlHelper html,
        PageInfo pageInfo, long workroomId, Func<int, string> pageUrl)
        {
            int a = (int)Math.Ceiling((decimal)pageInfo.TotalItems / pageInfo.PageSize);
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder tag_div = new TagBuilder("div");
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("data-ajax", "true");
                tag.MergeAttribute("data-ajax-begin", String.Format("active_menu_products({0});", workroomId));
                tag.MergeAttribute("data-ajax-loading", "#loading");
                tag.MergeAttribute("data-ajax-loading-duration", "500");
                tag.MergeAttribute("data-ajax-mode", "replace");
                tag.MergeAttribute("data-ajax-success", "review()");
                tag.MergeAttribute("data-ajax-update", "#rightBody");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                // если текущая страница, то выделяем ее,
                // например, добавляя класс
                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("active");
                }
                tag_div.InnerHtml = tag.ToString();
                result.Append(tag_div.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }

    }
}