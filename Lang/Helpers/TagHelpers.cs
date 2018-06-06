using Lang.Data;
using Lang.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lang.Helpers
{
    public class TagHelpers
    {
        public class LanguageLabelTagHelper : TagHelper
        {
            public string Language { get; set; }
            public string Icon { get; set; }
            public LanguageLevel Fluency { get; set; }

            public override void Process(TagHelperContext context, TagHelperOutput output)
            {
                output.TagName = "a";    // Replaces <email> with <a> tag
            }
        }
    }
}
