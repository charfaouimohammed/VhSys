using MediatR;
using Microsoft.EntityFrameworkCore;
using Registration_System.Commands._Administration;
using Registration_System.Commands._Administration.Registration_System.Commands._Administration;
using Registration_System.Models;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Registration_System.Commands._Administration
{
    public class SignupCommandHandler : IRequestHandler<SignupCommand, Guid>
    {
        private readonly RegistrationSystemDbContext _context;

        public SignupCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(SignupCommand command, CancellationToken cancellationToken)
        {
            // Check if username or email already exists
            var existingUser = await _context.Administrations
                .Where(u => u.Username == command.Username || u.Email == command.Email)
                .FirstOrDefaultAsync(cancellationToken);

            if (existingUser != null)
            {
                throw new InvalidOperationException("Username or email already exists.");
            }

            // Validate password length (minimum 10 characters)
            if (command.Password.Length < 10)
            {
                throw new InvalidOperationException("Password must be at least 10 characters long.");
            }

            // Create new user and hash the password
            var user = new Administration
            {
                AdminId = Guid.NewGuid(),
                Username = command.Username,
                Email = command.Email,
                Fullname = command.Fullname,
                Role = command.Role ?? "User", // Default to 'User' if no role is provided
                IsActif = command.IsActif,
                IsDeleted = false,
                PasswordHash = EncryptPassword(command.Password)
            };

            // Add the user to the database
            _context.Administrations.Add(user);
            await _context.SaveChangesAsync(cancellationToken);

            return user.AdminId;
        }

        private string EncryptPassword(string password)
        {
            using var aesAlg = Aes.Create();
            aesAlg.Key = GetValidKey("371560199d59665e6bfa4c985a81ee7a0135ec2103bdcc93a5d80e3f6a3b884f"); // Your secret key
            aesAlg.IV = new byte[16]; // Initialization Vector (you can generate a random IV here)

            using var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            byte[] encrypted = PerformCryptography(Encoding.UTF8.GetBytes(password), encryptor);
            return Convert.ToBase64String(encrypted);
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
