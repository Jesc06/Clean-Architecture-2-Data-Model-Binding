using System.ComponentModel.DataAnnotations;

namespace testing.ViewModels
{
    public class HobbyViewModel
    {
        [Required(ErrorMessage = "Hobby name is required")]
        public string HobbyName { get; set; }

        [Required(ErrorMessage = "Hobby second name is required")]
        public string SecondHobbyName { get; set; }
       
    }
}
