using GovUKFrontend.Components.Common;
using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.Components
{
    [HtmlTargetElement("gds-table-cell")]
    public class GdsTableCellTagHelper : BaseTagHelper
    {
        public string Class { get; set; } = "";
        public string Title { get; set; } = "";
        public TableCellScope Scope { get; set; }
        public TableCellType Type { get; set; } = TableCellType.Content;
        public LayoutStyle Style { get; set; }
        public bool IsNumeric { get; set; } = false;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = GetTableCellTag(Type);

            var cellClass = Type == TableCellType.Header ? "govuk-table__header" : "govuk-table__cell";
            var cellNumericClass = IsNumeric ? " govuk-table__cell--numeric" : "";
            cellClass += $" govuk-!{GetLayoutStyleClass(Style)}{cellNumericClass}"; 

            output.Attributes.SetAttribute("class", $"{cellClass} {Class}");
            if (!string.IsNullOrEmpty(GetTableCellScope(Scope)))
                output.Attributes.SetAttribute("scope", GetTableCellScope(Scope));
            if (!string.IsNullOrEmpty(Title))
                output.Attributes.SetAttribute("title", Title);
        }
    }
}
