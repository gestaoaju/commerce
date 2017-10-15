/*
 * Copyright (c) gestaoaju.com.br - All rights reserved.
 * Licensed under MIT (https://github.com/gestaoaju/commerce/blob/master/LICENSE).
 */

using System.Text;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Gestaoaju.Helpers.EmailTagHelpers
{
    public class H1TagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
		{
            output.Attributes.Add("style", new StringBuilder()
                .Append("font-size:22px;")
                .Append("font-weight:700;")
                .Append("margin:0 0 20px;")
                .ToString());
        }
    }
}
