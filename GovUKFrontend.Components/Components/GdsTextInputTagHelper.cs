using GovUKFrontend.Components.Common;
using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.Components
{
    [HtmlTargetElement("gds-text-input")]
    public class GdsTextInputTagHelper : BaseTagHelper
    {
        public ModelStateDictionary ModelState { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Class { get; set; } = "";
        public string Placeholder { get; set; } = "";
        public string Autocomplete { get; set; } = "";
        public string TextType { get; set; } = "";
        public bool Spellcheck { get; set; } = false;
        public bool Disabled { get; set; } = false;
        public string AriaDescribedBy { get; set; }
        public InputWidth InputWidth { get; set; }
        public InputMode InputMode { get; set; }
        public bool ExtraLetterSpacing { get; set; } = false;
        public string Prefix { get; set; } = "";
        public string Suffix { get; set; } = "";

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            //Build CSS classes
            string invalidGroupCss = string.Empty;
            string invalidCss = string.Empty;
            if(ModelState != null && !ModelState.IsValid && ModelState.ContainsKey(Name))
            {
                if (ModelState[Name].ValidationState == ModelValidationState.Invalid)
                {
                    invalidGroupCss = "govuk-form-group--error";
                    invalidCss = "govuk-input--error";
                }
            }
            string widthCss = !string.IsNullOrEmpty(GetInputWidth(InputWidth)) ? GetInputWidth(InputWidth) : string.Empty;
            string letterSpacingCss = ExtraLetterSpacing ? "govuk-input--extra-letter-spacing" : string.Empty;
            string inputCss = "govuk-input";
            if (!string.IsNullOrEmpty(invalidCss))
                inputCss += $" {invalidCss}";
            if (!string.IsNullOrEmpty(widthCss))
                inputCss += $" {widthCss}";
            if (!string.IsNullOrEmpty(letterSpacingCss))
                inputCss += $" {letterSpacingCss}";
            if (!string.IsNullOrEmpty(Class))
                inputCss += $" {Class}";

            // Create an <input> tag programmatically
            var inputTag = new TagBuilder("input");
            inputTag.Attributes.Add("type", TextType);
            inputTag.Attributes.Add("class", inputCss);
            inputTag.Attributes.Add("id", Id);
            inputTag.Attributes.Add("name", Name);
            inputTag.Attributes.Add("value", Value);

            // Conditional attributes
            if (!string.IsNullOrEmpty(Placeholder))
                inputTag.Attributes.Add("placeholder", Placeholder);
            if (Disabled)
                inputTag.Attributes.Add("disabled", "disabled");
            if (!string.IsNullOrEmpty(Autocomplete))
                inputTag.Attributes.Add("autocomplete", Autocomplete);
            if (Spellcheck)
                inputTag.Attributes.Add("spellcheck", "true");
            if (!string.IsNullOrEmpty(AriaDescribedBy))
                inputTag.Attributes.Add("aria-describedby", AriaDescribedBy);
            if (!string.IsNullOrEmpty(GetInputMode(InputMode)) || GetInputMode(InputMode) != "decimal")
                inputTag.Attributes.Add("inputmode", $"{GetInputMode(InputMode)}");

            //Prefix and Suffix handling
            if(!string.IsNullOrEmpty(Prefix) || !string.IsNullOrEmpty(Suffix))
            {
                var wrapperDiv = new TagBuilder("div");
                wrapperDiv.AddCssClass("govuk-input__wrapper");
                if (!string.IsNullOrEmpty(Prefix))
                {
                    var prefixDiv = new TagBuilder("div");
                    prefixDiv.AddCssClass("govuk-input__prefix");
                    prefixDiv.Attributes.Add("aria-hidden", "true");
                    prefixDiv.InnerHtml.Append(Prefix);
                    wrapperDiv.InnerHtml.AppendHtml(prefixDiv);
                }
                wrapperDiv.InnerHtml.AppendHtml(inputTag);
                if (!string.IsNullOrEmpty(Suffix))
                {
                    var suffixDiv = new TagBuilder("div");
                    suffixDiv.AddCssClass("govuk-input__suffix");
                    suffixDiv.Attributes.Add("aria-hidden", "true");
                    suffixDiv.InnerHtml.Append(Suffix);
                    wrapperDiv.InnerHtml.AppendHtml(suffixDiv);
                }
                inputTag = wrapperDiv;
            }

            // Set div group attributes
            output.Attributes.SetAttribute("class", $"govuk-form-group {invalidGroupCss}");

            // Append to TagHelperOutput
            output.PostContent.SetHtmlContent(inputTag);
        }
    }
}
