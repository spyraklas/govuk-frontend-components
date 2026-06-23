using System.ComponentModel.DataAnnotations;

namespace GovUKFrontend.Components.Test.Models
{
    public class DateFormModel
    {
        [Required(ErrorMessage = "Enter the date input testing field")]
        public DateTime? DateInputTest { get; set; } = null;
    }
}
