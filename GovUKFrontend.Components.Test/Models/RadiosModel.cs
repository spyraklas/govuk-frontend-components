using System.ComponentModel.DataAnnotations;

namespace GovUKFrontend.Components.Test.Models
{
    public class RadiosModel
    {
        [Required(ErrorMessage = "You must select one of the options below")] 
        public string RadioTest { get; set; }
    }
}
