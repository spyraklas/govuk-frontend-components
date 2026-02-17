using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace GovUKFrontend.Components.Components
{
    [HtmlTargetElement("gds-details")]
    public class GdsDetailsTagHelper : BaseTagHelper
    {
        public string Id { get; set; }
        public string Class { get; set; } = "";
        public string Caption { get; set; } = "";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "details";
            output.Attributes.SetAttribute("class", $"govuk-details {Class}");
            output.Attributes.SetAttribute("id", Id);

            // Create a <summary> tag programmatically for header
            var summaryTag = new TagBuilder("summary");
            summaryTag.AddCssClass("govuk-details__summary");
            var summarySpanTag = new TagBuilder("span");
            summarySpanTag.AddCssClass("govuk-details__summary-text");
            summarySpanTag.InnerHtml.Append(Caption);
            summaryTag.InnerHtml.AppendHtml(summarySpanTag);
            output.PreContent.AppendHtml(summaryTag);

            //Ctreate a <div> tag for the details content
            var detailsContentTag = new TagBuilder("div");
            detailsContentTag.AddCssClass("govuk-details__text");
            
            // Get the child content of the tag helper
            var childContent = await output.GetChildContentAsync();
            detailsContentTag.InnerHtml.AppendHtml(childContent);
           
            output.Content.AppendHtml(detailsContentTag);
        }
    }
}
