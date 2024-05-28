using GiLuFlixBack.Models;


namespace GiLuFlixBack.Repository
{
    public interface IUserRepository
    {
        Task<User> SearchByEmail(string email);

        Task<User> GetUserById(int Id);

        Task<int> requestFriendship(int requesterId,int requestedId );

        Task<ICollection<User>> GetFriends(int requesterId);

    }
}