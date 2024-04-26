using GiLuFlixBack.Models;

namespace GiLuFlixBack.Repository
{
    public interface IReviewRepository
    {
        Task<int> PostReview();
    }
}