using ItCareerExam.Data.Models;
using ItCareerExam.Data.Repositories;
using ItCareerExam.Services.Mapping;
using ItCareerExam.Web.DTOs.Reviews;
using Microsoft.EntityFrameworkCore;

namespace ItCareerExam.Services.Data.Reviews
{
    public class ReviewsService : IReviewsService
    {
        private readonly IRepository<Review> _reviewRepository;

        public ReviewsService(IRepository<Review> reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task CreateReviewAsync(CreateReviewDTO createDTO)
        {
            var review = AutoMapperConfig.MapperInstance.Map<Review>(createDTO);
            await _reviewRepository.AddAsync(review);
            await _reviewRepository.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(int id)
        {
            var review = await _reviewRepository.All().FirstAsync(r => r.Id == id);
            _reviewRepository.Delete(review);
            await _reviewRepository.SaveChangesAsync();
        }

        public async Task EditReviewAsync(EditReviewDTO editDTO)
        {
            var review = await _reviewRepository.All().FirstAsync(r => r.Id == editDTO.Id);

            review.Text = editDTO.Text;
            review.Score = editDTO.Score;

            _reviewRepository.Update(review);
            await _reviewRepository.SaveChangesAsync();
        }

        public Task<bool> ExistsAsync(string userId, int reviewId) => _reviewRepository.AllAsNoTracking().AnyAsync(r => r.UserId == userId && r.Id == reviewId);

        public int GetReviewCount() => _reviewRepository.AllAsNoTracking().Count();

        public async Task<EditReviewDTO> GetReviewEditDTO(int id)
        {
            var review = await _reviewRepository.AllAsNoTracking().Include(r => r.Bar).FirstAsync(r => r.Id == id);
            return AutoMapperConfig.MapperInstance.Map<EditReviewDTO>(review);
        }

        public async Task<IEnumerable<ReviewDTO>> GetUserReviewsAsync(string id)
        {
            var userReviews = await _reviewRepository.AllAsNoTracking()
                .Include(r => r.Bar)
                .Include(r => r.User)
                .To<ReviewDTO>()
                .ToListAsync();

            if (!string.IsNullOrEmpty(id))
            {
                userReviews = userReviews.Where(ur => ur.UserId == id).ToList();
            }

            return userReviews;
        }
    }
}
