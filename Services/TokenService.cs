using DocumentSharingSystem.Context;
using DocumentSharingSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace DocumentSharingSystem.Services
{
    public class TokenService
    {
        private readonly AppDBContext _db;

        public TokenService(AppDBContext db)
        {
            _db = db;
        }

        public async Task<string> CreateTokenAsync(string documentId, DateTime expiryTime)
        {
            var rawToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

            var token = new Token
            {
                TokenHash = Hash(rawToken),
                DocumentId = documentId,
                ExpiryTime = expiryTime
            };

            _db.Tokens.Add(token);
            await _db.SaveChangesAsync();

            return rawToken;
        }

        public async Task<Token?> ValidateTokenAsync(string token)
        {
            var hash = Hash(token);

            var entity = await _db.Tokens
                .Include(t => t.Document)
                .FirstOrDefaultAsync(t => t.TokenHash == hash);

            if (entity == null) return null;
            if (entity.IsRevoked) return null;
            if (entity.IsUsed) return null;
            if (entity.ExpiryTime < DateTime.UtcNow) return null;

            entity.IsUsed = true;
            entity.DownloadCount++;

            await _db.SaveChangesAsync();
            return entity;
        }

        private static string Hash(string input)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = SHA256.HashData(inputBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
