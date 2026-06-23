using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.TagHelpers
{
    [HtmlTargetElement("gds-radio-item")]
    public class GdsRadioItemTagHelper : BaseTagHelper
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Hint { get; set; }
        public string Class { get; set; } = "";
        public string Value { get; set; } = "";
        public string ModelValue { get; set; } = "";
        public bool Disabled { get; set; } = false;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", $"govuk-radios__item {Class}");

            var childContent = await output.GetChildContentAsync();
            bool hasConditionalContent = childContent != null && !childContent.IsEmptyOrWhiteSpace;

            // Create an <input> tag programmatically
            var inputTag = new TagBuilder("input");
            inputTag.Attributes.Add("type", "radio");
            inputTag.Attributes.Add("class", "govuk-radios__input");
            inputTag.Attributes.Add("id", Id);
            inputTag.Attributes.Add("name", Name);
            inputTag.Attributes.Add("value", Value);
            if (ModelValue == Value)
                inputTag.Attributes.Add("checked", "checked");
            if (Disabled)
                inputTag.Attributes.Add("disabled", "disabled");
            if(hasConditionalContent)
                inputTag.Attributes.Add("data-aria-controls", $"{Id}-conditional");
            output.Content.AppendHtml(inputTag);

            // handle caption and hint
            if (!string.IsNullOrEmpty(Caption))
            {
                //create a <label> tag programmatically
                var labelTag = new TagBuilder("label");
                labelTag.Attributes.Add("class", "govuk-label govuk-radios__label");
                labelTag.Attributes.Add("for", Id);
                labelTag.InnerHtml.Append(Caption);
                output.Content.AppendHtml(labelTag);
            }
            if (!string.IsNullOrEmpty(Hint))
            {
                //create a <div> tag for hint
                var hintTag = new TagBuilder("div");
                hintTag.Attributes.Add("class", "govuk-hint govuk-radios__hint");
                hintTag.Attributes.Add("id", $"{Id}-hint");
                hintTag.InnerHtml.Append(Hint);
                output.Content.AppendHtml(hintTag);
            }

            //Add contitional content if any
            if (hasConditionalContent)
            {
                //create a <div> tag for conditional content
                var conditionalTag = new TagBuilder("div");
                conditionalTag.Attributes.Add("class", "govuk-radios__conditional govuk-radios__conditional--hidden");
                conditionalTag.Attributes.Add("id", $"{Id}-conditional");
                conditionalTag.InnerHtml.AppendHtml(childContent);
                output.Content.AppendHtml(conditionalTag);
            }
        }
    }
}