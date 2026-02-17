using GovUKFrontend.Components.Common;
using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.Components
{
    [HtmlTargetElement("gds-caption")]
    public class GdsCaptionTagHelper : BaseTagHelper
    {
        public string Id { get; set; }
        public string Class { get; set; }
        public string Title { get; set; }
        public CaptionSize Size { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "span";

            if (!string.IsNullOrEmpty(Id))
            {
                output.Attributes.SetAttribute("id", $"{Id}");
            }

            if (!string.IsNullOrEmpty(Title))
            {
                output.Attributes.SetAttribute("title", Title);
            }

            output.Attributes.SetAttribute("class", $"govuk-caption{GetCaptionSize(Size)} {Class}");
        }


    }
}
