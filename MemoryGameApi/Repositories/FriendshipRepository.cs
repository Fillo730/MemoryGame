using MemoryGame_API.IRepositories;
using MemoryGame_API.Models;
using MemoryGame_API.Utils;
using Microsoft.EntityFrameworkCore;

namespace MemoryGame_API.Repositories;

public class FriendshipRepository (AppDbContext dbContext) : BaseRepository(dbContext), IFriendshipRepository
{
    public async Task<Friendship?> GetByIdAsync(int id)
    {
        return await _dbContext.Friendships
            .Include(f => f.Requester)
            .Include(f => f.Addressee)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Friendship?> GetBetweenUsersAsync(int userAId, int userBId)
    {
        return await _dbContext.Friendships
            .FirstOrDefaultAsync(f =>
                (f.RequesterId == userAId && f.AddresseeId == userBId) ||
                (f.RequesterId == userBId && f.AddresseeId == userAId));
    }

    public async Task<Friendship> AddAsync(Friendship friendship)
    {
        await _dbContext.Friendships.AddAsync(friendship);

        return friendship;
    }

    public async Task<IEnumerable<Friendship>> GetAcceptedFriendshipsAsync(int userId)
    {
        return await _dbContext.Friendships
            .Include(f => f.Requester)
            .Include(f => f.Addressee)
            .Where(f => f.Status == FriendshipStatusEnum.Accepted &&
                (f.RequesterId == userId || f.AddresseeId == userId))
            .ToListAsync();
    }

    public async Task<IEnumerable<Friendship>> GetIncomingPendingRequestsAsync(int userId)
    {
        return await _dbContext.Friendships
            .Include(f => f.Requester)
            .Where(f => f.Status == FriendshipStatusEnum.Pending && f.AddresseeId == userId)
            .ToListAsync();
    }

    public void Remove(Friendship friendship)
    {
        _dbContext.Friendships.Remove(friendship);
    }
}
