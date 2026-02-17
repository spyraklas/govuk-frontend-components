using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.Components
{
    [HtmlTargetElement("gds-checkbox-divider")]
    public class GdsCheckboxDividerTagHelper : BaseTagHelper
    {
        public string Id { get; set; }
        public string Class { get; set; } = "";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", $"govuk-checkboxes__divider {Class}");
            output.Attributes.SetAttribute("id", Id);
        }
    }
}
