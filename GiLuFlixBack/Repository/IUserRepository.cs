using GiLuFlixBack.Models;


namespace GiLuFlixBack.Repository
{
    public interface IUserRepository
    {
        Task<User> SearchByEmail(string email);

        Task<User> GetUserById(int Id);

        Task<string> GetUserRole(string email);

        Task<int> requestFriendship(int requesterId,int requestedId);
        
        Task<ICollection<User>> GetFriends(int requesterId);

        Task<int> acceptFriendship(int user1,int user2 );

    }
}