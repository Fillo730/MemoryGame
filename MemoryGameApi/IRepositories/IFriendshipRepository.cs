using MemoryGame_API.Models;

namespace MemoryGame_API.IRepositories;

public interface IFriendshipRepository
{
    Task<Friendship?> GetByIdAsync(int id);

    Task<Friendship?> GetBetweenUsersAsync(int userAId, int userBId);

    Task<Friendship> AddAsync(Friendship friendship);

    Task<IEnumerable<Friendship>> GetAcceptedFriendshipsAsync(int userId);

    Task<IEnumerable<Friendship>> GetIncomingPendingRequestsAsync(int userId);

    void Remove(Friendship friendship);

    Task SaveChangesAsync();
}
