using GovUKFrontend.Components.Common;
using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.Components
{
    [HtmlTargetElement("gds-fieldset")]
    public class GdsFieldsetTagHelper: BaseTagHelper
    {
        public string Id { get; set; }
        public string Class { get; set; }
        public string Title { get; set; }
        public string Caption { get; set; }
        public LabelType LabelType { get; set; } = LabelType.H2;
        public Size LabelSize { get; set; } = Size.Medium;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "fieldset";
            output.Attributes.SetAttribute("id", Id);
            output.Attributes.SetAttribute("class", $"govuk-fieldset {Class}");

            if (!string.IsNullOrEmpty(Title))
                output.Attributes.SetAttribute("title", Title);

            //create a legend for labeling fieldset
            if (!string.IsNullOrEmpty(Caption)) 
            { 
                var legendTag = new TagBuilder("legend");
                legendTag.AddCssClass($"govuk-fieldset__legend govuk-fieldset__legend--{GetSize(LabelSize)}"); 
                
                var captionTag = new TagBuilder(GetLabelTag(LabelType)); 
                captionTag.AddCssClass("govuk-fieldset__heading"); 
                captionTag.InnerHtml.Append(Caption);
                legendTag.InnerHtml.AppendHtml(captionTag);

                output.PreContent.AppendHtml(legendTag);
            } 
            
            var childContent = await output.GetChildContentAsync(); 
            output.Content.SetHtmlContent(childContent);
        }
    }
}
