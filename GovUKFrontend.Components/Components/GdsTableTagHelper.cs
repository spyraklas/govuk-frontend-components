using GovUKFrontend.Components.Common;
using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.Components
{
    [HtmlTargetElement("gds-table")]
    public class GdsTableTagHelper : BaseTagHelper
    {
        public string Id { get; set; } = "";
        public string Class { get; set; } = "";
        public string Title { get; set; } = "";
        public string Caption { get; set; } = "";
        public Size CaptionSize { get; set; } = Size.Large;
        public bool Small { get; set; } = false;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "table";

            string cssTableClass = Small ? $"govuk-table govuk-table--small-text-until-tablet" : "govuk-table";

            output.Attributes.SetAttribute("id", Id);
            output.Attributes.SetAttribute("class", $"{cssTableClass} {Class}");
            if (!string.IsNullOrEmpty(Title))
                output.Attributes.SetAttribute("title", Title);

            if(!string.IsNullOrEmpty(Caption))
            {
                var captionTag = new TagBuilder("caption");
                captionTag.AddCssClass($"govuk-table__caption govuk-table__caption-{GetSize(CaptionSize)}");
                captionTag.InnerHtml.Append(Caption);
                output.PreContent.AppendHtml(captionTag);
            }
        }
    }
}
