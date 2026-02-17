using System.ComponentModel.DataAnnotations;

namespace GovUKFrontend.Components.Test.Models
{
    public class ErrorSummaryModel
    {
        [Required(ErrorMessage = "You must enter your full name")]
        public string? Fullname { get; set; }

        [Required(ErrorMessage = "You must enter some comments")]
        [StringLength(50, ErrorMessage = "The comments must be 50 characters or fewer")]
        public string? Comments { get; set; }
    }
}
