// Copyright (c) gestaoaju.com.br - All rights reserved.
// Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).

using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Gestaoaju.Helpers.EmailTagHelpers
{
    public class ATagHelper : TagHelper
    {
        private StringBuilder Style { get; } = new StringBuilder()
            .Append("text-decoration:none;");

        public string AsButton { get; set; }

        public string Color { get; set; }

        private void ApplyAsButtonStyle()
        {
            Style
                .Append("border-bottom-width:2px;")
                .Append("border-bottom-style:solid;")
                .Append("color:#fff;")
                .Append("display:inline-block;")
                .Append("font-size:14px;")
                .Append("line-height:18px;")
                .Append("padding:8px 15px;");

            if (AsButton == "danger")
            {
                Style.Append("background-color:#e74c3c;")
                     .Append("border-bottom-color:#c0392b;");
            }
            else
            {
                Style.Append("background-color:#3498db;")
                     .Append("border-bottom-color:#2980b9;");
            }
        }

        private void ApplyLinkStyle(bool muted)
        {
            if (muted) Style.Append("color:#95a5a6;");
            else Style.Append("color:#3498db;");
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (AsButton != null) ApplyAsButtonStyle();
            else ApplyLinkStyle(muted: output.Attributes.ContainsName("muted"));

            output.Attributes.RemoveAll("as-button");
            output.Attributes.RemoveAll("muted");

            output.Attributes.Add("style", Style);
            output.Attributes.Add("target", "_blank");
        }
    }
}
