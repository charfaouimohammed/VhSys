using MediatR;
using Registration_System.Models;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Registration_System.Commands._Administration
{
    public class UpdateAdministrationCommandHandler : IRequestHandler<UpdateAdministrationCommand, Unit>
    {
        private readonly RegistrationSystemDbContext _context;

        public UpdateAdministrationCommandHandler(RegistrationSystemDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateAdministrationCommand request, CancellationToken cancellationToken)
        {
            // Find the existing administration
            var admin = await _context.Administrations.FindAsync(request.AdminId);
            if (admin == null || admin.IsDeleted == true)
                throw new Exception("Administration not found or is deleted.");

            // Check if the username or email already exists (excluding the current user)
            var existingUser = await _context.Administrations
                .Where(u => (u.Username == request.Username || u.Email == request.Email) && u.AdminId != request.AdminId)
                .FirstOrDefaultAsync(cancellationToken);

            if (existingUser != null)
            {
                throw new InvalidOperationException("Username or email already exists.");
            }

            // Update the fields with the provided values or retain the current ones
            admin.Username = request.Username ?? admin.Username;
            admin.PasswordHash = !string.IsNullOrEmpty(request.PasswordHash)
                ? EncryptPassword(request.PasswordHash)
                : admin.PasswordHash;
            admin.Role = request.Role ?? admin.Role;
            admin.Email = request.Email ?? admin.Email;
            admin.Fullname = request.Fullname ?? admin.Fullname;
            admin.IsActif = request.IsActif ?? admin.IsActif;

            // Save the changes to the database
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
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
