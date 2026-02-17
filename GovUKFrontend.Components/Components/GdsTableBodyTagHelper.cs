using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.Components
{
    [HtmlTargetElement("gds-table-body")]
    public class GdsTableBodyTagHelper : BaseTagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "tbody";
            output.Attributes.SetAttribute("class", $"govuk-table__body");
        }
    }
}
