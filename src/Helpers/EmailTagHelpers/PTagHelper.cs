/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Gestaoaju.Helpers.EmailTagHelpers
{
    public class PTagHelper : TagHelper
    {
        public string FontSize { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var small = FontSize == "small";

            output.Attributes.RemoveAll("email");
            output.Attributes.Add("style", new StringBuilder()
                .Append("margin:0 0 15px;")
                .Append($"font-size:{(small ? 14 : 16)}px;")
                .Append("line-height:24px"));
        }
    }
}
