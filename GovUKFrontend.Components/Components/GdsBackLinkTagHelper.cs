using GovUKFrontend.Components.Common;
using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.Components
{
    [HtmlTargetElement("gds-back-link")]
    public class GdsBackLinkTagHelper : BaseTagHelper
    {
        public string Id { get; set; }
        public bool InvertLink { get; set; } = false;
        public string Class { get; set; }
        public string Title { get; set; }
        public string Href { get; set; }
        public LinkTarget Target { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            string backLinkClass = InvertLink ? "govuk-back-link--inverse" : "";
            string value = !string.IsNullOrEmpty(Href) ? Href : "#";

            output.TagName = "a";

            output.Attributes.SetAttribute("class", "");
            output.Attributes.SetAttribute("class", $"govuk-back-link {backLinkClass} {Class}");
            output.Attributes.SetAttribute("href", $"{value}");
            output.Attributes.SetAttribute("target", $"{GetLinkTarget(Target)}");

            if (!string.IsNullOrEmpty(Title))
            {
                output.Attributes.SetAttribute("title", Title);
            }
            if (!string.IsNullOrEmpty(Id))
            {
                output.Attributes.SetAttribute("id", $"{Id}");
            }
        }

    }


}
