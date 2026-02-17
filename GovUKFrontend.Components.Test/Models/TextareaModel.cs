using System.ComponentModel.DataAnnotations;

namespace GovUKFrontend.Components.Test.Models
{
    public class TextareaModel
    {
        [Required(ErrorMessage = "You must enter a value")]
        public string? InputTest { get; set; }
        public string? CharacterCount { get; set; } = "Test Characters";
        public string? WordCount { get; set; } = "Test Words";
    }
}
