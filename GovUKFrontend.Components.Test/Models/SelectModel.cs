using System.ComponentModel.DataAnnotations;

namespace GovUKFrontend.Components.Test.Models
{
    public class SelectModel
    {
        [Required(ErrorMessage = "You must select one of the options")] 
        public string SelectTest { get; set; }
    }
}
