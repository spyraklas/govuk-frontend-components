using GovUKFrontend.Components.Common;
using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.Components
{
    [HtmlTargetElement("gds-heading")]
    public class GdsHeadingTagHelper : BaseTagHelper
    {
        public string Id { get; set; }
        public string Class { get; set; }
        public string Title { get; set; }
        public HeadingSize Size { get; set; }
        public HeadingType Type { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = GetHeadingTag(Type);

            if (!string.IsNullOrEmpty(Id))
            {
                output.Attributes.SetAttribute("id", $"{Id}");
            }

            if (!string.IsNullOrEmpty(Title))
            {
                output.Attributes.SetAttribute("title", Title);
            }

            output.Attributes.SetAttribute("class", $"govuk-heading{GetHeadingSize(Size)} {Class}");
        }


    }
}
