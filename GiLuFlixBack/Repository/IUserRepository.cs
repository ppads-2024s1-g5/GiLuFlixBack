using GiLuFlixBack.Models;


namespace GiLuFlixBack.Repository
{
    public interface IUserRepository
    {
        Task<User> SearchByEmail(string email);

    }
}