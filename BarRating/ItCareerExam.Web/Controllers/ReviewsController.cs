using ItCareerExam.Infrastructure.Extensions;
using ItCareerExam.Services.Data.Reviews;
using ItCareerExam.Web.DTOs.Reviews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ItCareerExam.Web.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly IReviewsService _reviewsService;

        public ReviewsController(IReviewsService reviewsService)
        {
            _reviewsService = reviewsService;
        }

        public async Task<IActionResult> Index()
        {
            // admin should be able to see all reviews and user only ones he has
            string userId = User.IsAdmin() ? string.Empty : User.GetId()!;
            var userReviews = await _reviewsService.GetUserReviewsAsync(userId);
            return View(userReviews);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            var createReviewDTO = new CreateReviewDTO
            {
                UserId = User.GetId()!,
                BarId = id,
            };
            return View(createReviewDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateReviewDTO createDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(createDTO);
            }

            await _reviewsService.CreateReviewAsync(createDTO);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (!User.IsAdmin() && !await _reviewsService.ExistsAsync(User.GetId()!, id))
            {
                return Unauthorized();
            }

            var editDTO = await _reviewsService.GetReviewEditDTO(id);
            return View(editDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditReviewDTO editDTO)
        {
            if (!User.IsAdmin() && !await _reviewsService.ExistsAsync(User.GetId()!, id))
            {
                return Unauthorized();
            }

            if (!ModelState.IsValid)
            {
                return View(editDTO);
            }

            await _reviewsService.EditReviewAsync(editDTO);
            return RedirectToAction(nameof(Index));
        }

        // will be fired through ajax
        [HttpDelete]
        public async Task Delete(int id)
        {
            if (!User.IsAdmin() && !await _reviewsService.ExistsAsync(User.GetId()!, id))
            {
                throw new InvalidOperationException("You're not admin or do not own this");
            }

            await _reviewsService.DeleteReviewAsync(id);
        }
    }
}
