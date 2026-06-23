using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.TagHelpers
{
    [HtmlTargetElement("gds-radio-divider")]
    public class GdsRadioDividerTagHelper : BaseTagHelper
    {
        public string Id { get; set; }
        public string Class { get; set; } = "";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", $"govuk-radios__divider {Class}");
            output.Attributes.SetAttribute("id", Id);
        }
    }
}
