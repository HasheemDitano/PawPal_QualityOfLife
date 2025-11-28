using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawPal_QualityOfLife.Models
{
    public class PetAssessment
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey("Pet")]
        public int PetId { get; set; }

        [Display(Name = "Assessment Date")]
        public DateTime AssessmentDate { get; set; } = DateTime.Now;

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

        [Required(ErrorMessage = "Activity level rating is required")]
        [Display(Name = "Activity Level")]
        [Range(1, 5, ErrorMessage = "Activity level must be rated between 1 and 5")]
        public int ActivityLevel { get; set; } = 1;

        [Required(ErrorMessage = "Social interaction rating is required")]
        [Display(Name = "Social Interaction")]
        [Range(1, 5, ErrorMessage = "Social interaction must be rated between 1 and 5")]
        public int SocialInteraction { get; set; } = 1;

        [Display(Name = "Additional Notes")]
        [StringLength(500, ErrorMessage = "Additional notes cannot exceed 500 characters")]
        public string? AdditionalNotes { get; set; }

        [Display(Name = "Overall Score")]
        public double OverallScore { get; set; }

        [Display(Name = "Quality Rating")]
        [StringLength(20)]
        public string QualityRating { get; set; } = string.Empty;

        public virtual Pet Pet { get; set; } = null!;

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