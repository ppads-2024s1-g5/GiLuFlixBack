using GiLuFlixBack.Models.ReviewDTO;

namespace GiLuFlixBack.Repository
{
    public interface IReviewRepository
    {
        Task<int> PostReview(ReviewForm review, string userId);

        Task<ICollection<ReviewResponse>> GetAllItemReviews(int ItemId);

        Task<int> LikeComment(int id);
    
    }
}