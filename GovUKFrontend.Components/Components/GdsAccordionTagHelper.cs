using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.Components
{
    [HtmlTargetElement("gds-accordion")]
    public class GdsAccordionTagHelper : BaseTagHelper
    {
        public string Id { get; set; }
        public string Class { get; set; } = "";
        public string Title { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("id", $"{Id}");
            output.Attributes.SetAttribute("class", $"govuk-accordion {Class}");
            output.Attributes.SetAttribute("data-module", "govuk-accordion");
            if (!string.IsNullOrEmpty(Title))
                output.Attributes.SetAttribute("title", Title);
        }

    }
}
