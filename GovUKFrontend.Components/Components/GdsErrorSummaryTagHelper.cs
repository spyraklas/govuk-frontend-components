using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.Components
{
    [HtmlTargetElement("gds-error-summary")]
    public class GdsErrorSummaryTagHelper : BaseTagHelper
    {
        public string Id { get; set; }
        public string Class { get; set; }
        public ModelStateDictionary ModelState { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (ModelState != null && !ModelState.IsValid)
            {
                output.TagName = "div";
                output.Attributes.SetAttribute("id", "id");
                output.Attributes.SetAttribute("class", $"govuk-error-summary {Class}");
                output.Attributes.SetAttribute("data-module", "govuk-error-summary");

                // Build the error summary content
                var divBodyTag = new TagBuilder("div");
                divBodyTag.Attributes.Add("role", "alert");

                //Build Header   
                var headerTag = new TagBuilder("h2");
                headerTag.AddCssClass("govuk-error-summary__title");
                headerTag.InnerHtml.Append("There is a problem");
                divBodyTag.InnerHtml.AppendHtml(headerTag);

                //Build a div body for error summary
                var divBodyContentTag = new TagBuilder("div");
                divBodyContentTag.AddCssClass("govuk-error-summary__body");

                //Build error list
                var errorListTag = new TagBuilder("ul");
                errorListTag.AddCssClass("govuk-list govuk-error-summary__list");

                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        var listItemTag = new TagBuilder("li");
                        var linkTag = new TagBuilder("a");
                        linkTag.Attributes.Add("href", $"#{state.Key}");
                        linkTag.InnerHtml.Append(error.ErrorMessage);
                        listItemTag.InnerHtml.AppendHtml(linkTag);
                        errorListTag.InnerHtml.AppendHtml(listItemTag);
                    }
                }
                divBodyTag.InnerHtml.AppendHtml(errorListTag);
                output.PostContent.AppendHtml(divBodyTag);
            }
        }

    }
}
