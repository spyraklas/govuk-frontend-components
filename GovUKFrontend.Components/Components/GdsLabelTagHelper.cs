using GovUKFrontend.Components.Common;
using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.Components
{
    [HtmlTargetElement("gds-label")]
    public class GdsLabelTagHelper : BaseTagHelper
    {
        public LabelType Tag { get; set; }
        public string For { get; set; }
        public Size Size { get; set; }
        public string Class { get; set; }
        public string Title { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = GetLabelTag(Tag);


            if (!string.IsNullOrEmpty(Title))
            {
                output.Attributes.SetAttribute("title", Title);
            }
            if (!string.IsNullOrEmpty(For))
            {
                output.Attributes.SetAttribute("for", $"{For}");
            }

            string componentCss = GetLabelTag(Tag) == "label" ? $"govuk-label govuk-label--{GetSize(Size)}" : $"govuk-heading-{GetHeadingSize(ToHeadingSize(Size))}";
            output.Attributes.SetAttribute("class", $"{componentCss} {Class}");
        }
    }
}
