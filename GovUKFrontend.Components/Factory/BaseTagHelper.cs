using GovUKFrontend.Components.Common;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace GovUKFrontend.Components.Factory
{
    public abstract class BaseTagHelper : TagHelper 
    {
        protected string GetInputMode(InputMode inputMode)
        {
            return inputMode switch
            {
                InputMode.Default => "",
                InputMode.Numeric => "numeric",
                InputMode.Decimal => "decimal",
                _ => "",
            };
        }

        protected string GetInputWidth(InputWidth inputWidth)
        {
            return inputWidth switch
            {
                InputWidth.Default => "",
                InputWidth.Full => "govuk-!-width-full",
                InputWidth.ThreeQuarters => "govuk-!-width-three-quarters",
                InputWidth.TwoThirds => "govuk-!-width-two-thirds",
                InputWidth.OneHalf => "govuk-!-width-one-half",
                InputWidth.OneThird => "govuk-!-width-one-third",
                InputWidth.OneQuarter => "govuk-!-width-one-quarter",
                InputWidth.Width20 => "govuk-input--width-20",
                InputWidth.Width10 => "govuk-input--width-10",
                InputWidth.Width5 => "govuk-input--width-5",
                InputWidth.Width4 => "govuk-input--width-4",
                InputWidth.Width3 => "govuk-input--width-3",
                InputWidth.Width2 => "govuk-input--width-2",
                _ => "",
            };
        }

        protected string GetLabelTag(LabelType labelType)
        {
            return labelType switch
            {
                LabelType.Label => "label",
                LabelType.H1 => "h1",
                LabelType.H2 => "h2",
                LabelType.H3 => "h3",
                LabelType.H4 => "h4",
                LabelType.H5 => "h5",
                LabelType.H6 => "h6",
                _ => "label",
            };
        }

        protected string GetHeadingTag(HeadingType headingType)
        {
            return headingType switch
            {
                HeadingType.H1 => "h1",
                HeadingType.H2 => "h2",
                HeadingType.H3 => "h3",
                HeadingType.H4 => "h4",
                HeadingType.H5 => "h5",
                HeadingType.H6 => "h6",
                _ => "h2",
            };
        }

        protected string GetSize(Size size)
        {
            return size switch
            {
                Size.ExtraLarge => "-xl",
                Size.Large => "-l",
                Size.Medium => "-m",
                Size.Small => "-s",
                _ => "-m",
            };
        }

        protected string GetHeadingSize(HeadingSize size)
        {
            return size switch
            {
                HeadingSize.Large => "-l",
                HeadingSize.Medium => "-m",
                HeadingSize.Small => "-s",
                _ => "-m",
            };
        }

        protected string GetCaptionSize(CaptionSize size)
        {
            return size switch
            {
                CaptionSize.Large => "-l",
                CaptionSize.Medium => "-m",
                CaptionSize.Small => "-s",
                _ => "-m",
            };
        }

        protected async Task AppendHtmlFromInnerTagHelperAsync<T>(string tagTargetName, T childTagHelper, TagHelperContext context, TagHelperOutput output) where T : TagHelper 
        {
            var childOutput = new DefaultTagHelperContent();

            // Create execution context
            var childContext = new TagHelperContext(
                tagName: tagTargetName,
                allAttributes: new TagHelperAttributeList(),
                items: new Dictionary<object, object>(),
                uniqueId: Guid.NewGuid().ToString("N")
            );

            var childTagHelperOutput = new TagHelperOutput(
                tagTargetName,
                new TagHelperAttributeList(),
                (useCachedResult, encoder) =>
                {
                    var tagHelperContent = new DefaultTagHelperContent();
                    tagHelperContent.SetHtmlContent("");
                    return Task.FromResult<TagHelperContent>(tagHelperContent);
                }
            );

            // Run TagHelper
            await childTagHelper.ProcessAsync(childContext, childTagHelperOutput);

            // Append rendered HTML of the child TagHelper
            output.Content.AppendHtml(childTagHelperOutput);
        }

        protected string GetLinkTarget(LinkTarget target)
        {
            return target switch
            {
                LinkTarget.Self => "_self",
                LinkTarget.Blank => "_blank",
                LinkTarget.Parent => "_parent",
                LinkTarget.Top => "_top",
                _ => "_self",
            };
        }

        protected string GetTableCellScope(TableCellScope scope)
        {
            return scope switch
            {
                TableCellScope.Row => "row",
                TableCellScope.Column => "col",
                _ => ""
            };
        }

        protected string GetTableCellTag(TableCellType type)
        {
            return type switch
            {
                TableCellType.Header => "th",
                TableCellType.Content => "td",
                _ => "td"
            };
        }

        protected HeadingSize ToHeadingSize(Size size)
        {
            return size switch
            {
                Size.ExtraLarge => HeadingSize.Large,
                Size.Large => HeadingSize.Large,
                Size.Medium => HeadingSize.Medium,
                Size.Small => HeadingSize.Small,
                _ => HeadingSize.Medium,
            };
        }

        protected CaptionSize ToCaptionSize(Size size)
        {
            return size switch
            {
                Size.ExtraLarge => CaptionSize.Large,
                Size.Large => CaptionSize.Large,
                Size.Medium => CaptionSize.Medium,
                Size.Small => CaptionSize.Small,
                _ => CaptionSize.Medium,
            };
        }

        protected string GetLayoutStyleClass(LayoutStyle style)
        {
            return style switch
            {
                LayoutStyle.Full => "-full",
                LayoutStyle.OneHalf => "-one-half",
                LayoutStyle.OneThird => "-one-third",
                LayoutStyle.TwoThirds => "-two-thirds",
                LayoutStyle.OneQuarter => "-one-quarter",
                LayoutStyle.ThreeQuarters => "-three-quarters",
                _ => "-full",
            };
        }
    }
}
