using System.ComponentModel.DataAnnotations;

namespace GovUKFrontend.Components.Test.Models
{
    public class InputFormModel
    {
        [Required(ErrorMessage = "You must enter a value")]
        [StringLength(5, ErrorMessage = "The value must be 5 characters or fewer")]
        public string? InputTest { get; set; }
        public string? InputWidthFull { get; set; }
        public string? InputWidthThreeQuarters { get; set; }
        public string? InputWidthTwoThirds { get; set; }
        public string? InputWidthOneHalf { get; set; }
        public string? InputWidthOneThird { get; set; }
        public string? InputWidthOneQuarter { get; set; }
        public string? InputWidthWidth20 { get; set; }
        public string? InputWidthWidth10 { get; set; }
        public string? InputWidthWidth5 { get; set; }
        public string? InputWidthWidth4 { get; set; }
        public string? InputWidthWidth3 { get; set; }
        public string? InputWidthWidth2 { get; set; }
        public string? InputNumeric { get; set; }
        public string? InputDecimal { get; set; }
        public string? InputCode { get; set; } = "SECRET-CODE";
        public string? InputSuffixPrefix { get; set; }
    }
}
