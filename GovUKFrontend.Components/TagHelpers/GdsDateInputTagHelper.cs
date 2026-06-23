using GovUKFrontend.Components.Common;
using GovUKFrontend.Components.Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Reflection;

namespace GovUKFrontend.Components.TagHelpers
{
    [HtmlTargetElement("gds-date-input")]
    public class GdsDateInputTagHelper : BaseTagHelper
    {
        private const string DayKey = "Day";
        private const string MonthKey = "Month";
        private const string YearKey = "Year";

        public ModelStateDictionary ModelState { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime? Value { get; set; }
        public string Class { get; set; } = "";
        public bool Disabled { get; set; } = false;
        public Size LagendSize { get; set; } = Size.Large;
        public bool AutoComplete { get; set; } = false;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";

            //Build CSS classes
            string invalidGroupCss = string.Empty;
            string invalidCss = string.Empty;
            if(ModelState != null && !ModelState.IsValid && ModelState.ContainsKey(Name))
            {
                if (ModelState[Name].ValidationState == ModelValidationState.Invalid)
                {
                    invalidGroupCss = "govuk-form-group--error";
                    invalidCss = "govuk-input--error";
                }
            }

            string inputDivCss = "govuk-date-input";
            if (!string.IsNullOrEmpty(Class))
                inputDivCss += $" {Class}";


            //Add a fieldset and legend for accessibility
            var fieldsetTag = new TagBuilder("fieldset");
            fieldsetTag.AddCssClass("govuk-fieldset");
            fieldsetTag.Attributes.Add("role", "group");
            fieldsetTag.Attributes.Add("aria-describedby", $"{Id}-hint");

            var legendTag = new TagBuilder("legend");
            legendTag.AddCssClass("govuk-fieldset__legend");

            legendTag.InnerHtml.AppendHtml("Date of birth");

            fieldsetTag.InnerHtml.AppendHtml(legendTag);
            output.PostContent.AppendHtml(fieldsetTag);


            // Create an input div tag programmatically
            var inputDivTag = new TagBuilder("div");
            inputDivTag.AddCssClass(inputDivCss);
            inputDivTag.Attributes.Add("id", Id);

            // Append the day items to the input div
            inputDivTag.InnerHtml.AppendHtml(AddInputDivItemTag(DayKey, invalidCss, Value?.Day.ToString()));
            inputDivTag.InnerHtml.AppendHtml(AddInputDivItemTag(MonthKey, invalidCss, Value?.Month.ToString()));
            inputDivTag.InnerHtml.AppendHtml(AddInputDivItemTag(YearKey, invalidCss, Value?.Year.ToString()));


            // Set div group attributes
            output.Attributes.SetAttribute("class", $"govuk-form-group {invalidGroupCss}");

            // Append to TagHelperOutput
            output.PostContent.SetHtmlContent(inputDivTag);
        }

        /// <summary>
        /// Adding an input div for each date part (day, month, year) to ensure the correct structure and styling for the GDS date input component.
        /// </summary>
        /// <param name="datePart">date part (day, month, year)</param>
        /// <param name="invalidCss">the invalid css class</param>
        /// <returns></returns>
        private TagBuilder AddInputDivItemTag(string datePart, string invalidCss, string value)
        {
            // Create an input div day item tag programmatically
            var inputDivDayItemTag = new TagBuilder("div");
            inputDivDayItemTag.Attributes.Add("class", "govuk-date-input__item");

            // Create an input div day form group tag programmatically
            var inputDivDayFormGroup = new TagBuilder("div");
            inputDivDayItemTag.AddCssClass("govuk-form-group");

            // Create an input label day tag programmatically
            var labelDay = new TagBuilder("label");
            labelDay.AddCssClass("govuk-label govuk-date-input__label");
            labelDay.Attributes.Add("for", $"{Id}-{datePart}");
            labelDay.InnerHtml.AppendHtml(datePart);
            inputDivDayItemTag.InnerHtml.AppendHtml(labelDay);

            // Create a day <input> tag programmatically
            var inputTag = new TagBuilder("input");
            string cssLength = "--width-2";
            if (datePart == YearKey)
            {
                cssLength = "--width-4";
            }
            inputTag.AddCssClass($"govuk-input govuk-date-input__input govuk-input{cssLength} {invalidCss}");
            inputTag.Attributes.Add("id", $"{Id}-{datePart}");
            inputTag.Attributes.Add("name", $"{Name}-{datePart}");
            inputTag.Attributes.Add("value", value);
            inputTag.Attributes.Add("type", "text");
            inputTag.Attributes.Add("inputmode", "numeric");
            inputDivDayItemTag.InnerHtml.AppendHtml(inputTag);

            return inputDivDayItemTag;
        }


        public static void BindDatesFromForm<T>(T model, IFormCollection form, ModelStateDictionary modelState, Dictionary<string, string> DateInputLabels = null)
        {
            if (model == null || form == null || modelState == null) 
                return;

            var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                      .Where(p => p.CanWrite &&
                                                 (p.PropertyType == typeof(DateTime) ||
                                                  p.PropertyType == typeof(DateTime?)));

            if (DateInputLabels == null)
            {
                DateInputLabels = new Dictionary<string, string>();
            }

            foreach (var prop in properties)
            {
                string propName = prop.Name;

                string dayKey = $"{propName}-{DayKey}";
                string monthKey = $"{propName}-{MonthKey}";
                string yearKey = $"{propName}-{YearKey}";

                if (!form.ContainsKey(dayKey) ||
                    !form.ContainsKey(monthKey) ||
                    !form.ContainsKey(yearKey))
                    continue;

                string modelStateKey = propName;
                if (!DateInputLabels.ContainsKey(propName))
                {
                    DateInputLabels.Add(propName, $"{propName}");
                }

                // Try parse values
                bool dayParsed = int.TryParse(form[dayKey], out int day);
                bool monthParsed = int.TryParse(form[monthKey], out int month);
                bool yearParsed = int.TryParse(form[yearKey], out int year);


                // Check if all empty
                bool isEmpty = string.IsNullOrWhiteSpace(form[dayKey]) &&
                               string.IsNullOrWhiteSpace(form[monthKey]) &&
                               string.IsNullOrWhiteSpace(form[yearKey]);


                if (!isEmpty)
                {
                    //Clear any empty model state error for the date field as we will add specific errors for day/month/year
                    modelState.Remove(modelStateKey);

                    // Dial per field parsing errors
                    if (!dayParsed)
                    {
                        modelState.AddModelError($"{modelStateKey}-{DayKey}", $"{DateInputLabels[propName]} must include a valid day.");                        
                    }
                    if (!monthParsed)
                    {
                        modelState.AddModelError($"{modelStateKey}-{MonthKey}", $"{DateInputLabels[propName]} must include a valid month.");
                    }
                    if (!yearParsed)
                    {
                        modelState.AddModelError($"{modelStateKey}-{YearKey}", $"{DateInputLabels[propName]} must include a valid year.");
                    }

                    try
                    {
                        var date = new DateTime(year, month, day);

                        prop.SetValue(model, date);

                        //success → clear errors
                        modelState.Remove(modelStateKey);
                    }
                    catch
                    {
                        // Invalid date
                        modelState.AddModelError(modelStateKey, $"{DateInputLabels[propName]} must be a real date.");
                    }
                }

            }
        }

    }
}
