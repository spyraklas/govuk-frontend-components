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
        public LabelType LabelType { get; set; } = LabelType.Label;
        public string Hint { get; set; } = "";
        public string HintClass { get; set; } = "";
        public bool Small { get; set; } = false;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            //output.TagName = "div";
            //output.Attributes.SetAttribute("id", $"{Id}");

            ////invalid state handling
            //bool NotValid = ModelState != null && !ModelState.IsValid && ModelState.ContainsKey(Id);
            //string invalidGroupCss = NotValid ? "govuk-form-group--error" : string.Empty;
            //output.Attributes.SetAttribute("class", $"govuk-form-group {invalidGroupCss} {Class}");

            //// Get the child content of the tag helper
            //var childContent = await output.GetChildContentAsync();

            //string hintId = "";
            //if (!string.IsNullOrEmpty(Hint))
            //{
            //    hintId = $"{Name}-hint";
            //    output.Attributes.Add("aria-describedby", hintId);
            //}
            //if (!string.IsNullOrEmpty(Caption))
            //{
            //    // Create a <legend> tag for the caption
            //    var captionTag = new TagBuilder("legend");
            //    string captionSizeClass = $"govuk-fieldset__legend-{GetSize(CaptionSize)}";
            //    captionTag.AddCssClass($"govuk-fieldset__legend {captionSizeClass}");

            //    // Add the label tag inside the legend
            //    var labelTag = new TagBuilder(GetLabelTag(LabelType));
            //    labelTag.AddCssClass("govuk-fieldset__heading");
            //    labelTag.InnerHtml.Append(Caption);

            //    captionTag.InnerHtml.AppendHtml(labelTag);
            //    fieldTag.InnerHtml.AppendHtml(captionTag);

            //    //If hint exists, add it after the caption
            //    var hintTag = new TagBuilder("div");
            //    hintTag.AddCssClass($"govuk-hint {HintClass}");
            //    hintTag.Attributes.Add("id", hintId);
            //    hintTag.InnerHtml.Append(Hint);
            //    fieldTag.InnerHtml.AppendHtml(hintTag);
            //}

            ////add error message if invalid
            //if (NotValid)
            //{
            //    var errorMessageTag = new TagBuilder("div");
            //    errorMessageTag.AddCssClass("govuk-error-message");
            //    errorMessageTag.Attributes.Add("id", $"{Id}-error");
            //    errorMessageTag.InnerHtml.AppendHtml($@"<span class=""govuk-visually-hidden"">Error:</span> {ModelState[Id].Errors.FirstOrDefault()?.ErrorMessage}");
            //    fieldTag.InnerHtml.AppendHtml(errorMessageTag);
            //}

            ////add the select area
            //var selectAreaTag = new TagBuilder("select");
            //selectAreaTag.AddCssClass($"govuk-select");
            //selectAreaTag.Attributes.Add("id", $"{Name}");
            //selectAreaTag.Attributes.Add("name", $"{Name}");
            //selectAreaTag.InnerHtml.AppendHtml(childContent);

            //output.Content.SetHtmlContent(selectAreaTag);
        }
    }
}
