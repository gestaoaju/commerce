/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Gestaoaju.Helpers.EmailTagHelpers
{
    public class MailFooterTagHelper : TagHelper
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
                .Append("border-top:1px solid #e7ebee;")
                .Append("color:#687276;")
                .Append("margin:0 auto;")
                .Append("max-width:600px;")
                .Append("padding:35px 20px;")
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
