using ItCareerExam.Web.DTOs.Reviews;

namespace ItCareerExam.Services.Data.Reviews
{
    public interface IReviewsService
    {
        Task CreateReviewAsync(CreateReviewDTO createDTO);
        Task<IEnumerable<ReviewDTO>> GetUserReviewsAsync(string id);
        Task<EditReviewDTO> GetReviewEditDTO(int id);
        Task EditReviewAsync(EditReviewDTO editDTO);
        Task DeleteReviewAsync(int id);
        Task<bool> ExistsAsync(string userId, int reviewId);
        int GetReviewCount();
    }
}
