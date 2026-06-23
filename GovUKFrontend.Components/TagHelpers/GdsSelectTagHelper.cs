using GovUKFrontend.Components.Common;
using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.TagHelpers
{
    [HtmlTargetElement("gds-select")]
    public class GdsSelectTagHelper : BaseTagHelper
    {
        public ModelStateDictionary ModelState { get; set; }
        public string Name { get; set; }
        public string Class { get; set; } = "";
        public string Caption { get; set; } = "";
        public Size CaptionSize { get; set; } = Size.Large;
        public string Hint { get; set; } = "";
        public string HintClass { get; set; } = "";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            //invalid state handling
            bool NotValid = ModelState != null && !ModelState.IsValid && ModelState.ContainsKey(Name);
            string invalidGroupCss = NotValid ? "govuk-form-group--error" : string.Empty;
            output.Attributes.SetAttribute("class", $"govuk-form-group {invalidGroupCss} {Class}");

            // Get the child content of the tag helper
            var childContent = await output.GetChildContentAsync();

            string hintId = "";
            if (!string.IsNullOrEmpty(Hint))
            {
                hintId = $"{Name}-hint";
                output.Attributes.Add("aria-describedby", hintId);
            }
            if (!string.IsNullOrEmpty(Caption))
            {
                // Add the label tag inside the legend
                var labelTag = new TagBuilder("label");
                labelTag.AddCssClass($"govuk-label govuk-label-{GetSize(CaptionSize)}");
                labelTag.Attributes.Add("for", $"{Name}");
                labelTag.InnerHtml.Append(Caption);
                output.PreContent.AppendHtml(labelTag);

                //If hint exists, add it after the caption
                var hintTag = new TagBuilder("div");
                hintTag.AddCssClass($"govuk-hint {HintClass}");
                hintTag.Attributes.Add("id", hintId);
                hintTag.InnerHtml.Append(Hint);
                output.PreContent.AppendHtml(hintTag);
            }

            //add error message if invalid
            if (NotValid)
            {
                var errorMessageTag = new TagBuilder("div");
                errorMessageTag.AddCssClass("govuk-error-message");
                errorMessageTag.Attributes.Add("id", $"{Name}-error");
                errorMessageTag.InnerHtml.AppendHtml($@"<span class=""govuk-visually-hidden"">Error:</span> {ModelState[Name].Errors.FirstOrDefault()?.ErrorMessage}");
                output.PreContent.AppendHtml(errorMessageTag);
            }

            //add the select area
            var selectAreaTag = new TagBuilder("select");
            selectAreaTag.AddCssClass($"govuk-select");
            selectAreaTag.Attributes.Add("id", $"{Name}");
            selectAreaTag.Attributes.Add("name", $"{Name}");
            selectAreaTag.InnerHtml.AppendHtml(childContent);

            output.Content.SetHtmlContent(selectAreaTag);
        }
    }
}
