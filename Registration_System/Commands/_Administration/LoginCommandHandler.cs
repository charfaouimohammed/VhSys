using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Registration_System.Commands._Administration
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly RegistrationSystemDbContext _context;
        private readonly IConfiguration _configuration;

        public LoginCommandHandler(RegistrationSystemDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<string> Handle(LoginCommand command, CancellationToken cancellationToken)
        {
            // Check if input is valid
            if (string.IsNullOrWhiteSpace(command.Username) || string.IsNullOrWhiteSpace(command.Password))
            {
                throw new UnauthorizedAccessException("Username or password is missing.");
            }

            // Retrieve user by username (ensure we are filtering out deleted users)
            var user = await _context.Administrations
                .Where(u => u.Username == command.Username && u.IsDeleted == false) // Explicitly checking IsDeleted == false
                .FirstOrDefaultAsync(cancellationToken);

            // Check if user was found
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            // Verify password hash
            bool passwordValid = VerifyPasswordHash(command.Password, user.PasswordHash);
            if (!passwordValid)
            {
                throw new UnauthorizedAccessException("Invalid credentials.");
            }

            // If login is successful, generate JWT token
            return JwtHelper.GenerateToken(user.Username, user.Role, _configuration["JwtSettings:SecretKey"],
                _configuration["JwtSettings:Issuer"], _configuration["JwtSettings:Audience"]);
        }

        private bool VerifyPasswordHash(string password, string storedHash)
        {
            // AES decryption to verify password
            string decryptedPassword = DecryptPassword(storedHash);
            return password == decryptedPassword;
        }

        private string DecryptPassword(string encryptedPassword)
        {
            using var aesAlg = Aes.Create();
            aesAlg.Key = GetValidKey(_configuration["JwtSettings:SecretKey"]);
            aesAlg.IV = new byte[16]; // The IV should match the one used during encryption

            using var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            byte[] decrypted = PerformCryptography(Convert.FromBase64String(encryptedPassword), decryptor);
            return Encoding.UTF8.GetString(decrypted);
        }

        private byte[] PerformCryptography(byte[] data, ICryptoTransform cryptoTransform)
        {
            using var memoryStream = new System.IO.MemoryStream();
            using (var cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
            {
                cryptoStream.Write(data, 0, data.Length);
            }
            return memoryStream.ToArray();
        }

        private byte[] GetValidKey(string key)
        {
            // Ensure key length is 32 bytes (256 bits)
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            // Resize or trim key to 32 bytes (256 bits)
            if (keyBytes.Length < 32)
            {
                Array.Resize(ref keyBytes, 32); // Pad to 32 bytes
            }
            else if (keyBytes.Length > 32)
            {
                Array.Resize(ref keyBytes, 32); // Trim to 32 bytes
            }

            return keyBytes;
        }
    }
}
