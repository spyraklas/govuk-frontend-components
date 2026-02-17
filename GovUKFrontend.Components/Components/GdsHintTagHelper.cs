using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.Components
{
    [HtmlTargetElement("gds-hint")]
    public class GdsHintTagHelper : BaseTagHelper
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public string Title { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            if (!string.IsNullOrEmpty(Title))
            {
                output.Attributes.SetAttribute("title", Title);
            }
            if (!string.IsNullOrEmpty(Name))
            {
                output.Attributes.SetAttribute("id", $"{Name}-hint");
            }
            output.Attributes.SetAttribute("class", $"govuk-hint {Class}");
        }
    }
}
