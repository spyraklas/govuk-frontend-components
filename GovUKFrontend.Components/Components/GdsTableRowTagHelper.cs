using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.Components
{
    [HtmlTargetElement("gds-table-row")]
    public class GdsTableRowTagHelper : BaseTagHelper
    {
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "tr";
            output.Attributes.SetAttribute("class", $"govuk-table__row");
        }
    }
}
