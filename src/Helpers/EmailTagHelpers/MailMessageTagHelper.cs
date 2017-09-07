// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using Gestaoaju.Extensions.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Text;

namespace Gestaoaju.Helpers.EmailTagHelpers
{
    public class MailMessageTagHelper : TagHelper
    {
        [HtmlAttributeNotBound, ViewContext]
        public ViewContext ViewContext { get; set; }

        private string BackgroundImage =>
            $"{ViewContext.HttpContext.Request.DomainUrl()}/img/background.png";

        private TagBuilder CreateDivTag()
        {
            var div = new TagBuilder("div");

            div.Attributes.Add("style", new StringBuilder()
                .Append($"background:#ecf0f1 url({BackgroundImage}) repeat;")
                .Append("color:#2f2936;")
                .Append("font-family:Arial,Helvetica,sans-serif;")
                .Append("font-size:16px;")
                .Append("font-weight:300;")
                .Append("margin:0;")
                .Append("padding:0;")
                .Append("width:100%;")
                .ToString());

            return div;
        }

        private TagBuilder CreateTableTag()
        {
            var table = new TagBuilder("table");

            table.Attributes.Add("style", new StringBuilder()
                .Append("background-color:#fff;")
                .Append("border:1px solid #c7d0d4;")
                .Append("border-collapse:separate;")
                .Append("border-radius:4px;")
                .Append("border-spacing:0;")
                .Append("color:#2f2936;")
                .Append("font-size:16px;")
                .Append("margin:15px auto;")
                .Append("max-width:700px;")
                .Append("padding:0;")
                .Append("width:100%;")
                .ToString());

            return table;
        }

        private TagBuilder CreateTbodyTag() => new TagBuilder("tbody");

        private TagBuilder CreateBodyTag() => new TagBuilder("body");

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "html";

            var body = CreateBodyTag();
            var div = CreateDivTag();
            var table = CreateTableTag();
            var tbody = CreateTbodyTag();

            output.PreElement.AppendHtml("<!DOCTYPE html>");
            output.PreContent.AppendHtml(body.RenderStartTag());
            output.PreContent.AppendHtml(div.RenderStartTag());
            output.PreContent.AppendHtml(table.RenderStartTag());
            output.PreContent.AppendHtml(tbody.RenderStartTag());
            output.PostContent.AppendHtml(tbody.RenderEndTag());
            output.PostContent.AppendHtml(table.RenderEndTag());
            output.PostContent.AppendHtml(div.RenderEndTag());
            output.PostContent.AppendHtml(body.RenderEndTag());
        }
    }
}
