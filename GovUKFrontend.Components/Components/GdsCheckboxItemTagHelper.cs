using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.Components
{
    [HtmlTargetElement("gds-checkbox-item")]
    public class GdsCheckboxItemTagHelper : BaseTagHelper
    {
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Hint { get; set; }
        public string Class { get; set; } = "";
        public bool Checked { get; set; } = false;
        public bool Disabled { get; set; } = false;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.Attributes.SetAttribute("class", $"govuk-checkboxes__item {Class}");

            var childContent = await output.GetChildContentAsync();
            bool hasConditionalContent = childContent != null && !childContent.IsEmptyOrWhiteSpace;

            // Create an <input> tag programmatically
            var inputTag = new TagBuilder("input");
            inputTag.Attributes.Add("type", "checkbox");
            inputTag.Attributes.Add("class", "govuk-checkboxes__input");
            inputTag.Attributes.Add("id", Name);
            inputTag.Attributes.Add("name", Name);
            inputTag.Attributes.Add("value", "true");
            if (Checked)
                inputTag.Attributes.Add("checked", "checked");
            if (Disabled)
                inputTag.Attributes.Add("disabled", "disabled");
            if(hasConditionalContent)
                inputTag.Attributes.Add("data-aria-controls", $"{Name}-conditional");
            output.Content.AppendHtml(inputTag);

            // handle caption and hint
            if (!string.IsNullOrEmpty(Caption))
            {
                //create a <label> tag programmatically
                var labelTag = new TagBuilder("label");
                labelTag.Attributes.Add("class", "govuk-label govuk-checkboxes__label");
                labelTag.Attributes.Add("for", Name);
                labelTag.InnerHtml.Append(Caption);
                output.Content.AppendHtml(labelTag);
            }
            if (!string.IsNullOrEmpty(Hint))
            {
                //create a <div> tag for hint
                var hintTag = new TagBuilder("div");
                hintTag.Attributes.Add("class", "govuk-hint govuk-checkboxes__hint");
                hintTag.Attributes.Add("id", $"{Name}-hint");
                hintTag.InnerHtml.Append(Hint);
                output.Content.AppendHtml(hintTag);
            }

            //Add contitional content if any
            if (hasConditionalContent)
            {
                //create a <div> tag for conditional content
                var conditionalTag = new TagBuilder("div");
                conditionalTag.Attributes.Add("class", "govuk-checkboxes__conditional govuk-checkboxes__conditional--hidden");
                conditionalTag.Attributes.Add("id", $"{Name}-conditional");
                conditionalTag.InnerHtml.AppendHtml(childContent);
                output.Content.AppendHtml(conditionalTag);
            }
        }
    }
}