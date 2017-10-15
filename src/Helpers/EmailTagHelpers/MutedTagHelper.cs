/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Gestaoaju.Helpers.EmailTagHelpers
{
    public class MutedTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
		{
            output.Attributes.Add("style", new StringBuilder()
                .Append("color:#95a5a6;")
                .Append("font-size:13px;"));
        }
    }
}
