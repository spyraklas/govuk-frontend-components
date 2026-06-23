using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.TagHelpers
{
    [HtmlTargetElement("gds-breadcrumbs")]
    public class GdsBreadcrumbsTagHelper : BaseTagHelper
    {
        public string Id { get; set; }
        public string Class { get; set; } = "";
        public bool CollapseOnMobile { get; set; } = false;
        public bool Inverse { get; set; } = false;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            string cssCollapseOnMobile = CollapseOnMobile ? "govuk-breadcrumbs--collapse-on-mobile" : "";
            cssCollapseOnMobile = Inverse ? $"{cssCollapseOnMobile} govuk-breadcrumbs--inverse" : cssCollapseOnMobile;

            output.TagName = "nav";
            output.Attributes.SetAttribute("id", $"{Id}");
            output.Attributes.SetAttribute("class", $"govuk-breadcrumbs {cssCollapseOnMobile} {Class}");

            var listTag = new TagBuilder("ol");
            listTag.AddCssClass("govuk-breadcrumbs__list");
            var childContent = await output.GetChildContentAsync();
            listTag.InnerHtml.AppendHtml(childContent);

            output.Content.AppendHtml(listTag);
        }


    }
}
