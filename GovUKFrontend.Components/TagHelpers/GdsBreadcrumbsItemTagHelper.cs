using GovUKFrontend.Components.Common;
using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.TagHelpers
{
    [HtmlTargetElement("gds-breadcrumbs-item")]
    public class GdsBreadcrumbsItemTagHelper : BaseTagHelper
    {
        public string Class { get; set; } = "";
        public string Href { get; set; } = "#";
        public LinkTarget Target { get; set; } = LinkTarget.Self;


        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "li";
            output.Attributes.SetAttribute("class", $"govuk-breadcrumbs__list-item {Class}");

            var linkTag = new TagBuilder("a");
            linkTag.AddCssClass("govuk-breadcrumbs__link");
            linkTag.Attributes.Add("href", $"{Href}");
            linkTag.Attributes.Add("target", $"{GetLinkTarget(Target)}");
            var childContent = await output.GetChildContentAsync();
            linkTag.InnerHtml.AppendHtml(childContent);

            output.Content.AppendHtml(linkTag);
        }


    }
}
