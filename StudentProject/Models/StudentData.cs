using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentProject.Models
{
    public class StudentData
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string? FirstName { get; set; }

        [Required (ErrorMessage ="Last Name is required")]
        
        public string ? LastName { get; set; }

        [Required]
        public string ? Course { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [MaxLength(12, ErrorMessage = "Phone number cannot exceed 12 characters.")]
        [RegularExpression(@"^\d{10,12}$", ErrorMessage = "Invalid phone number.")]
        public string? Phone { get; set; }
        public string? Gender { get; set; }

        
        [Required (ErrorMessage ="Enter your Email ")]
        public string? Email { get; set; }

        [Required (ErrorMessage = "can't leave empty")]
        [RegularExpression("pass" , ErrorMessage ="Enter a valid password")]
        public string? Password { get; set; }


        //Adding image property
        [NotMapped]       
        [Required(ErrorMessage = "Please Choose an Image")]
        public IFormFile ? Image { get; set; }

    }
}
