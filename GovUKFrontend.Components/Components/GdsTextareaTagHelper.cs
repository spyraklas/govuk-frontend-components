using GovUKFrontend.Components.Common;
using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.Components
{
    [HtmlTargetElement("gds-textarea")]
    public class GdsTextareaTagHelper : BaseTagHelper
    {
        public ModelStateDictionary ModelState { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Class { get; set; } = "";
        public int Rows { get; set; } = 5;
        public bool Disabled { get; set; } = false;
        public int MaxCount { get; set; } = 0;

        public TextareaCount Count  { get; set; } = TextareaCount.None;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            //Build CSS classes
            string invalidGroupCss = string.Empty;
            string invalidCss = string.Empty;
            if (ModelState != null && !ModelState.IsValid && ModelState.ContainsKey(Name))
            {
                if (ModelState[Name].ValidationState == ModelValidationState.Invalid)
                {
                    invalidGroupCss = "govuk-form-group--error";
                    invalidCss = "govuk-input--error";
                }
            }
            string countGroupCss = Count != TextareaCount.None ? "govuk-character-count" : string.Empty;
            string countCss = Count != TextareaCount.None ? "govuk-js-character-count" : string.Empty;
            string textareaCss = "govuk-textarea";
            if (!string.IsNullOrEmpty(invalidCss))
                textareaCss += $" {invalidCss}";
            if (!string.IsNullOrEmpty(countCss))
                textareaCss += $" {countCss}";
            if (!string.IsNullOrEmpty(Class))
                textareaCss += $" {Class}";

            // Create an <input> tag programmatically
            var textareaTag = new TagBuilder("textarea");
            textareaTag.Attributes.Add("id", Id);
            textareaTag.Attributes.Add("name", Name);
            textareaTag.Attributes.Add("class", textareaCss);
            textareaTag.Attributes.Add("rows", Rows.ToString());
            textareaTag.InnerHtml.SetHtmlContent(Value);

            //Add character count
            var countDiv = new TagBuilder("div");
            if (Count != TextareaCount.None)
            {
                output.Attributes.Add("data-module", "govuk-character-count");
                textareaTag.Attributes.Add("aria-describedby", $"{Name}-info");

                // Add a div for the count message
                countDiv.Attributes.Add("id", $"{Name}-info");
                countDiv.Attributes.Add("class", "govuk-hint govuk-character-count__message");
                if (Count == TextareaCount.Character)
                {
                    output.Attributes.Add("data-maxlength", (MaxCount).ToString());
                    countDiv.InnerHtml.SetHtmlContent($"You can enter up to {MaxCount} characters");
                }
                else if (Count == TextareaCount.Word)
                {
                    output.Attributes.Add("data-maxwords", (MaxCount).ToString());
                    countDiv.InnerHtml.Append($"You can enter up to {MaxCount} words");
                }
            }

            // Conditional attributes
            if (Disabled)
                textareaTag.Attributes.Add("disabled", "disabled");

            // Set div group attributes
            output.Attributes.SetAttribute("class", $"govuk-form-group {invalidGroupCss} {countGroupCss}");

            // Append to TagHelperOutput
            output.PostContent.AppendHtml(textareaTag);
            if (Count != TextareaCount.None)
                output.PostContent.AppendHtml(countDiv);
        }

    }
}
