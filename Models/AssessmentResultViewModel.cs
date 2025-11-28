namespace PawPal_QualityOfLife.Models
{
    public class AssessmentResultViewModel
    {
        public string PetName { get; set; } = string.Empty;
        public string PetType { get; set; } = string.Empty;
        public int PetAge { get; set; }
        public string PetBreed { get; set; } = string.Empty;
        public string OwnerName { get; set; } = string.Empty;
        
        public double OverallScore { get; set; }
        public string QualityRating { get; set; } = string.Empty;
        public string ScoreColor { get; set; } = string.Empty;
        
        public int PainLevel { get; set; }
        public int Mobility { get; set; }
        public int Appetite { get; set; }
        public int Happiness { get; set; }
        public int ActivityLevel { get; set; }
        public int SocialInteraction { get; set; }
        
        public string? AdditionalNotes { get; set; }
        public DateTime AssessmentDate { get; set; } = DateTime.Now;

        public string GetPainLevelDescription()
        {
            return PainLevel switch
            {
                1 => "Severe Pain",
                2 => "Moderate Pain", 
                3 => "Mild Pain",
                4 => "Minimal Pain",
                5 => "No Pain",
                _ => "Unknown"
            };
        }

        public string GetCategoryDescription(int score)
        {
            return score switch
            {
                5 => "Excellent",
                4 => "Good",
                3 => "Fair",
                2 => "Poor",
                1 => "Very Poor",
                _ => "Unknown"
            };
        }

        public List<string> GetRecommendations()
        {
            var recommendations = new List<string>();
            
            if (OverallScore < 3.0)
            {
                recommendations.Add("Consider consulting with a veterinarian about your pet's quality of life.");
            }
            
            if (PainLevel <= 2)
            {
                recommendations.Add("Discuss pain management options with your veterinarian.");
            }
            
            if (Mobility <= 2)
            {
                recommendations.Add("Consider physical therapy or mobility aids for your pet.");
            }
            
            if (Appetite <= 2)
            {
                recommendations.Add("Monitor your pet's eating habits and discuss with your vet if appetite doesn't improve.");
            }
            
            if (Happiness <= 2 || ActivityLevel <= 2)
            {
                recommendations.Add("Try engaging your pet with favorite activities or toys to improve mood and activity.");
            }
            
            if (SocialInteraction <= 2)
            {
                recommendations.Add("Encourage gentle social interaction appropriate for your pet's comfort level.");
            }

            if (recommendations.Count == 0)
            {
                recommendations.Add("Your pet appears to have a good quality of life. Continue current care routines.");
                recommendations.Add("Regular check-ups with your veterinarian are always recommended.");
            }
            
            return recommendations;
        }
    }
}