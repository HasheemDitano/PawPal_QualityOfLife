using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PawPal_QualityOfLife.Data;
using PawPal_QualityOfLife.Models;

namespace PawPal_QualityOfLife.Controllers
{
    public class PetsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var pets = await _context.Pets
                .Include(p => p.Assessments)
                .OrderByDescending(p => p.CreatedDate)
                .ToListAsync();
            
            return View(pets);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .Include(p => p.Assessments)
                .OrderByDescending(a => a.CreatedDate)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Type,Age,Breed,OwnerName,OwnerEmail,OwnerPhone")] Pet pet)
        {
            if (ModelState.IsValid)
            {
                pet.CreatedDate = DateTime.Now;
                pet.LastUpdated = DateTime.Now;
                _context.Add(pet);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Pet '{pet.Name}' has been successfully created!";
                return RedirectToAction(nameof(Index));
            }
            return View(pet);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }
            return View(pet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type,Age,Breed,OwnerName,OwnerEmail,OwnerPhone,CreatedDate")] Pet pet)
        {
            if (id != pet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    pet.LastUpdated = DateTime.Now;
                    _context.Update(pet);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = $"Pet '{pet.Name}' has been successfully updated!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PetExists(pet.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pet);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets
                .Include(p => p.Assessments)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (pet == null)
            {
                return NotFound();
            }

            return View(pet);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet != null)
            {
                _context.Pets.Remove(pet);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"Pet '{pet.Name}' has been successfully deleted!";
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> AddAssessment(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            var assessment = new PetAssessment
            {
                PetId = pet.Id,
                Pet = pet
            };

            return View(assessment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAssessment([Bind("PetId,PainLevel,Mobility,Appetite,Happiness,ActivityLevel,SocialInteraction,AdditionalNotes")] PetAssessment assessment)
        {
            if (ModelState.IsValid)
            {
                assessment.AssessmentDate = DateTime.Now;
                assessment.OverallScore = assessment.CalculateOverallScore();
                assessment.QualityRating = assessment.GetQualityOfLifeRating();
                
                _context.Add(assessment);
                await _context.SaveChangesAsync();
                
                TempData["SuccessMessage"] = "Assessment has been successfully added!";
                return RedirectToAction(nameof(Details), new { id = assessment.PetId });
            }

            assessment.Pet = await _context.Pets.FindAsync(assessment.PetId);
            return View(assessment);
        }

        private bool PetExists(int id)
        {
            return _context.Pets.Any(e => e.Id == id);
        }
    }
}