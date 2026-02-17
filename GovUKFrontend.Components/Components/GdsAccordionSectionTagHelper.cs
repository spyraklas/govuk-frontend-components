using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.Components
{
    [HtmlTargetElement("gds-accordion-section")]
    public class GdsAccordionSectionTagHelper : BaseTagHelper
    {
        public string Id { get; set; }
        public string Class { get; set; } = "";
        public string HeadingCaption { get; set; } = "";
        public string SummaryCaption { get; set; } = "";
        public bool Expanded { get; set; } = false;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            string sectionClass = Expanded ? " govuk-accordion__section--expanded" : "govuk-accordion__section";

            output.TagName = "div";
            output.Attributes.SetAttribute("id", $"{Id}");
            output.Attributes.SetAttribute("class", $"{sectionClass} {Class}");

            //Create the heading tag
            var headerTag = new TagBuilder("div");
            headerTag.Attributes.Add("id", $"{Id}-header");
            headerTag.AddCssClass("govuk-accordion__section-header");
            
            var headingTag = new TagBuilder("h2");
            headingTag.Attributes.Add("id", $"{Id}-heading");
            headingTag.AddCssClass("govuk-accordion__section-heading");
            
            var headingButton = new TagBuilder("span");
            headingButton.Attributes.Add("id", $"{Id}-heading-button");
            headingButton.AddCssClass("govuk-accordion__section-button");
            headingButton.InnerHtml.SetHtmlContent(HeadingCaption);

            headingTag.InnerHtml.SetHtmlContent(headingButton);
            headerTag.InnerHtml.AppendHtml(headingTag);

            //Create the summary tag
            if (!string.IsNullOrEmpty(SummaryCaption))
            {
                var summaryTag = new TagBuilder("div");
                summaryTag.Attributes.Add("id", $"{Id}-summary");
                summaryTag.AddCssClass("govuk-accordion__section-summary govuk-body");
                summaryTag.InnerHtml.SetHtmlContent(SummaryCaption);
                headerTag.InnerHtml.AppendHtml(summaryTag);
            }

            //Set the header tag as the pre-content of the accordion section
            output.PreContent.SetHtmlContent(headerTag);


            // Get the child content of the tag helper
            var childContent = await output.GetChildContentAsync();

            // Create the content tag
            var contentTag = new TagBuilder("div");
            contentTag.Attributes.Add("id", $"{Id}-content");
            contentTag.AddCssClass("govuk-accordion__section-content");
            contentTag.InnerHtml.SetHtmlContent(childContent);

            //set the content tag as the main content of the accordion section
            output.Content.SetHtmlContent(contentTag);
        }

    }
}
