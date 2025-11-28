using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PawPal_QualityOfLife.Models;

namespace PawPal_QualityOfLife.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Assessment()
        {
            return View(new PetAssessmentViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Assessment(PetAssessmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = new AssessmentResultViewModel
            {
                PetName = model.PetName,
                PetType = model.PetType,
                PetAge = model.PetAge,
                PetBreed = model.PetBreed,
                OwnerName = model.OwnerName,
                PainLevel = model.PainLevel,
                Mobility = model.Mobility,
                Appetite = model.Appetite,
                Happiness = model.Happiness,
                ActivityLevel = model.ActivityLevel,
                SocialInteraction = model.SocialInteraction,
                AdditionalNotes = model.AdditionalNotes,
                OverallScore = model.CalculateOverallScore(),
                QualityRating = model.GetQualityOfLifeRating(),
                ScoreColor = model.GetScoreColor()
            };

            return View("AssessmentResult", result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
