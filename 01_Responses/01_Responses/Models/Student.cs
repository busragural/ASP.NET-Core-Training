using System.ComponentModel.DataAnnotations;

namespace _00_Basics.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="name is required")]
        [StringLength(10)]
        public string Name { get; set; }
        [Required]
        [Range(10,20)]
        public int Age { get; set;}
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
