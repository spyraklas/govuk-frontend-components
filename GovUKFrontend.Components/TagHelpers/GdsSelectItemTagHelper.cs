using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.TagHelpers
{
    [HtmlTargetElement("gds-select-item")]
    public class GdsSelectItemTagHelper : BaseTagHelper
    {
        public string Value { get; set; } = "";
        public string Class { get; set; } = "";
        public string ModelValue { get; set; } = "";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "option";
            output.Attributes.SetAttribute("class", $"{Class}");
            output.Attributes.SetAttribute("value", $"{Value}");
            if (Value == ModelValue)
            {
                output.Attributes.SetAttribute("selected", "selected");
            }
        }
    }
}