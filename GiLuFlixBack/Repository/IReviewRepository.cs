using GiLuFlixBack.Models.ReviewDTO;

namespace GiLuFlixBack.Repository
{
    public interface IReviewRepository
    {
        Task<int> PostReview(ReviewForm review, string userId);

        Task<ICollection<ReviewResponse>> GetAllItemReviews(int itemId);
        
        Task<ICollection<ReviewResponse>> GetAllUserReviews(int userId);

        Task<int> LikeComment(int id);
    
    }
}