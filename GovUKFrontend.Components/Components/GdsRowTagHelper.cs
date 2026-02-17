using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.Components
{
    [HtmlTargetElement("gds-row")]
    public class GdsRowTagHelper: BaseTagHelper
    {
        public string Id { get; set; }
        public string Class { get; set; }
        public string Title { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            if (!string.IsNullOrEmpty(Title))
            {
                output.Attributes.SetAttribute("title", Title);
            }
            if (!string.IsNullOrEmpty(Id))
            {
                output.Attributes.SetAttribute("id", $"{Id}");
            }
            output.Attributes.SetAttribute("class", $"govuk-grid-row {Class}");
        }
    }
}
