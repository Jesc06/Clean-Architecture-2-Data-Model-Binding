using System.ComponentModel.DataAnnotations;

namespace testing.ViewModels
{
    public class InformationViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        public string name { get; set; }

        [Required(ErrorMessage = "Lastname is required")]
        public string lastname { get; set; }
    }
}
