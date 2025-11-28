using System.ComponentModel.DataAnnotations;

namespace PawPal_QualityOfLife.Models
{
    public class PetAssessmentViewModel
    {
        [Required(ErrorMessage = "Pet name is required")]
        [Display(Name = "Pet Name")]
        [StringLength(50, ErrorMessage = "Pet name cannot exceed 50 characters")]
        public string PetName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pet type is required")]
        [Display(Name = "Pet Type")]
        public string PetType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pet age is required")]
        [Display(Name = "Pet Age (years)")]
        [Range(0, 30, ErrorMessage = "Pet age must be between 0 and 30 years")]
        public int PetAge { get; set; }

        [Required(ErrorMessage = "Pet breed is required")]
        [Display(Name = "Pet Breed")]
        [StringLength(50, ErrorMessage = "Pet breed cannot exceed 50 characters")]
        public string PetBreed { get; set; } = string.Empty;

        [Required(ErrorMessage = "Owner name is required")]
        [Display(Name = "Owner Name")]
        [StringLength(100, ErrorMessage = "Owner name cannot exceed 100 characters")]
        public string OwnerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Pain level rating is required")]
        [Display(Name = "Pain Level")]
        [Range(1, 5, ErrorMessage = "Pain level must be rated between 1 and 5")]
        public int PainLevel { get; set; } = 1;

        [Required(ErrorMessage = "Mobility rating is required")]
        [Display(Name = "Mobility")]
        [Range(1, 5, ErrorMessage = "Mobility must be rated between 1 and 5")]
        public int Mobility { get; set; } = 1;

        [Required(ErrorMessage = "Appetite rating is required")]
        [Display(Name = "Appetite")]
        [Range(1, 5, ErrorMessage = "Appetite must be rated between 1 and 5")]
        public int Appetite { get; set; } = 1;

        [Required(ErrorMessage = "Happiness rating is required")]
        [Display(Name = "Happiness")]
        [Range(1, 5, ErrorMessage = "Happiness must be rated between 1 and 5")]
        public int Happiness { get; set; } = 1;

        [Display(Name = "Activity Level")]
        [Range(1, 5, ErrorMessage = "Activity level must be rated between 1 and 5")]
        public int ActivityLevel { get; set; } = 1;

        [Display(Name = "Social Interaction")]
        [Range(1, 5, ErrorMessage = "Social interaction must be rated between 1 and 5")]
        public int SocialInteraction { get; set; } = 1;

        [Display(Name = "Additional Notes")]
        [StringLength(500, ErrorMessage = "Additional notes cannot exceed 500 characters")]
        public string? AdditionalNotes { get; set; }

        public double CalculateOverallScore()
        {
            var adjustedPainScore = 6 - PainLevel;
            
            var totalScore = adjustedPainScore + Mobility + Appetite + Happiness + ActivityLevel + SocialInteraction;
            return Math.Round((totalScore / 6.0), 2);
        }

        public string GetQualityOfLifeRating()
        {
            var score = CalculateOverallScore();
            return score switch
            {
                >= 4.0 => "Excellent",
                >= 3.5 => "Good",
                >= 3.0 => "Fair",
                >= 2.5 => "Poor",
                _ => "Very Poor"
            };
        }

        public string GetScoreColor()
        {
            var score = CalculateOverallScore();
            return score switch
            {
                >= 4.0 => "success",
                >= 3.5 => "info",
                >= 3.0 => "warning",
                >= 2.5 => "danger",
                _ => "dark"
            };
        }
    }
}