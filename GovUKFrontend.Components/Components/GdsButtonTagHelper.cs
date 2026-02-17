using GovUKFrontend.Components.Common;
using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.Components
{
    [HtmlTargetElement("gds-button")]
    public class GdsButtonTagHelper : BaseTagHelper
    {
        private const string ArrowHtml = @"<svg class=""govuk-button__start-icon"" xmlns=""http://www.w3.org/2000/svg"" width=""17.5"" height=""19"" viewBox=""0 0 33 40"" aria-hidden=""true"" focusable=""false""><path fill=""currentColor"" d=""M0 0h13l20 20-20 20H0l20-20z"" /></svg>";

        public string Id { get; set; }
        public ButtonType ButtonType { get; set; }
        public ButtonStyle ButtonStyle { get; set; }
        public string Class { get; set; }
        public string Title { get; set; }
        public bool PreventDoubleClick { get; set; } = false;
        public string Href { get; set; }
        public LinkTarget Target { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (ButtonType == ButtonType.Link)
            {
                output.TagName = "a";

                output.Attributes.SetAttribute("role", "button");
                output.Attributes.SetAttribute("draggable", "false");
                output.Attributes.SetAttribute("class", $"{GetButtonStyle()} { Class }");
                output.Attributes.SetAttribute("data-module", "govuk-button");
                output.Attributes.SetAttribute("href", $"{Href}");
                output.Attributes.SetAttribute("target", $"{GetLinkTarget(Target)}");

                if(ButtonStyle == ButtonStyle.Start)
                {
                    output.PostContent.SetHtmlContent(ArrowHtml);
                }
            }
            else
            {
                output.TagName = "button";

                output.Attributes.SetAttribute("type", $"{GetButtonType()}");
                output.Attributes.SetAttribute("class", $"{GetButtonStyle()} {Class}");
                output.Attributes.SetAttribute("data-module", "govuk-button");
            }

            if (!string.IsNullOrEmpty(Title))
            {
                output.Attributes.SetAttribute("title", Title);
            }
            if (!string.IsNullOrEmpty(Id))
            {
                output.Attributes.SetAttribute("id", $"{Id}");
            }
            if (ButtonStyle == ButtonStyle.Disabled)
            {
                output.Attributes.SetAttribute("disabled ", "disabled");
                output.Attributes.SetAttribute("aria-disabled", "true");
            }
            if (PreventDoubleClick)
            {
                output.Attributes.SetAttribute("data-prevent-double-click", "true");
            }

        }

        private string GetButtonType()
        {
            return ButtonType switch
            {
                ButtonType.Submit => "submit",
                ButtonType.Reset => "reset",
                _ => "button",
            };
        }

        private string GetButtonStyle()
        {
            return ButtonStyle switch
            {
                ButtonStyle.Primary => "govuk-button",
                ButtonStyle.Start => "govuk-button govuk-button--start",
                ButtonStyle.Secondary => "govuk-button govuk-button--secondary",
                ButtonStyle.Warning => "govuk-button govuk-button--warning",
                ButtonStyle.Inverse => "govuk-button govuk-button--inverse",
                _ => "govuk-button",
            };
        }


    }
}
