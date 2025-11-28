using System.ComponentModel.DataAnnotations;

namespace PawPal_QualityOfLife.Models
{
    public class Pet
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Pet name is required")]
        [Display(Name = "Pet Name")]
        [StringLength(50, ErrorMessage = "Pet name cannot exceed 50 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pet type is required")]
        [Display(Name = "Pet Type")]
        [StringLength(30, ErrorMessage = "Pet type cannot exceed 30 characters")]
        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pet age is required")]
        [Display(Name = "Pet Age (years)")]
        [Range(0, 30, ErrorMessage = "Pet age must be between 0 and 30 years")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Pet breed is required")]
        [Display(Name = "Pet Breed")]
        [StringLength(50, ErrorMessage = "Pet breed cannot exceed 50 characters")]
        public string Breed { get; set; } = string.Empty;

        [Required(ErrorMessage = "Owner name is required")]
        [Display(Name = "Owner Name")]
        [StringLength(100, ErrorMessage = "Owner name cannot exceed 100 characters")]
        public string OwnerName { get; set; } = string.Empty;

        [Display(Name = "Owner Email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string? OwnerEmail { get; set; }

        [Display(Name = "Owner Phone")]
        [Phone(ErrorMessage = "Please enter a valid phone number")]
        [StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters")]
        public string? OwnerPhone { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Display(Name = "Last Updated")]
        public DateTime LastUpdated { get; set; } = DateTime.Now;

        public virtual ICollection<PetAssessment> Assessments { get; set; } = new List<PetAssessment>();
    }
}