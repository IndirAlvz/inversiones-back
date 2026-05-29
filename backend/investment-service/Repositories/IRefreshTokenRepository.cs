using investment_service.Models;
using System.Threading.Tasks;

namespace investment_service.Repositories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetByTokenAsync(string token);
        Task AddAsync(RefreshToken refreshToken);
        Task RevokeAsync(string token);
        Task DeleteExpiredAsync();
    }
}