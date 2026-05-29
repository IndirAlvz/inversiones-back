using System;
using System.Security.Cryptography;
using investment_service.Models;
using investment_service.Repositories;

namespace investment_service.Services
{
    public class RefreshTokenService
    {
        private readonly IRefreshTokenRepository _repository;
        public RefreshTokenService(IRefreshTokenRepository repository)
        {
            _repository = repository;
        }

        public async Task<RefreshToken> GenerateRefreshTokenAsync(int userId)
        {
            var randomBytes = new byte[64];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            var token = Convert.ToBase64String(randomBytes);
            var refreshToken = new RefreshToken
            {
                UserId = userId,
                Token = token,
                Expires = DateTime.UtcNow.AddDays(7),
                IsRevoked = false,
                Created = DateTime.UtcNow
            };
            await _repository.AddAsync(refreshToken);
            return refreshToken;
        }

        public async Task<RefreshToken?> ValidateRefreshTokenAsync(string token)
        {
            var refreshToken = await _repository.GetByTokenAsync(token);
            if (refreshToken == null || refreshToken.IsRevoked || refreshToken.Expires < DateTime.UtcNow)
                return null;
            return refreshToken;
        }

        public async Task RevokeRefreshTokenAsync(string token)
        {
            await _repository.RevokeAsync(token);
        }
    }
}