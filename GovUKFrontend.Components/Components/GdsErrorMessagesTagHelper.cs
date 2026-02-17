using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.Components
{
    [HtmlTargetElement("gds-error-messages")]
    public class GdsErrorMessagesTagHelper : BaseTagHelper
    {
        public string For { get; set; }
        public ModelStateDictionary ModelState { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("id", $"{For}-error");

            if (ModelState != null && ModelState.ContainsKey(For))
            {
                foreach (var error in ModelState[For].Errors)
                {
                    var spanTag = new TagBuilder("p");
                    spanTag.Attributes.Add("class", "govuk-error-message");
                    spanTag.InnerHtml.AppendHtml($@"<span class=""govuk-visually-hidden"">Error:</span> {error.ErrorMessage}");
                    output.Content.AppendHtml(spanTag);
                }
            }
        }
    }
}
