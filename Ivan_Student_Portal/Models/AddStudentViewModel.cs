using System.ComponentModel.DataAnnotations;

namespace Ivan_Student_Portal.Models
{
    public class AddStudentViewModel
    {
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

    }
}
