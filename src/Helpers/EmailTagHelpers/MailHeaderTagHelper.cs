/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Gestaoaju.Helpers.EmailTagHelpers
{
    public class MailHeaderTagHelper : TagHelper
    {
        private TagBuilder CreateTdTag()
        {
            var td = new TagBuilder("td");

            td.Attributes.Add("style", new StringBuilder()
                .Append("margin:0;")
                .Append("padding:0;")
                .ToString());

            return td;
        }

        private TagBuilder CreateDivTag()
        {
            var div = new TagBuilder("div");

            div.Attributes.Add("style", new StringBuilder()
                .Append("padding:20px 40px;")
                .Append("font-size:14px;")
                .Append("border-bottom:1px solid #dee7eb;")
                .ToString());

            return div;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
		{
            output.TagName = "tr";

            var td = CreateTdTag();
            var div = CreateDivTag();

            output.PreContent.AppendHtml(td.RenderStartTag());
            output.PreContent.AppendHtml(div.RenderStartTag());
            output.PostContent.AppendHtml(div.RenderEndTag());
            output.PostContent.AppendHtml(td.RenderEndTag());
        }
    }
}
