using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Smart_HR___RMS.Data;
using Smart_HR___RMS.Dtos;


namespace Smart_HR___RMS.Repository
{
    public class RegistrationService : IUserLoginRegistrationInterface
    {
        private readonly SmartHireDbContext _context;

        public RegistrationService(SmartHireDbContext context)
        {
            _context = context;
        }

        public async Task<UserRegisters?> LoginAsync(LoginDto login)
        {
            var user = await _context.UserRegisters
                .FirstOrDefaultAsync(u => u.Email == login.Email);

            if (user == null)
                return null;

            var passwordVerification = new PasswordHasher<UserRegisters>()
                .VerifyHashedPassword(user, user.Password, login.Password);

            if (passwordVerification == PasswordVerificationResult.Failed)
                return null;

            return user;
        }


        public async Task<UserRegisters?> RegistrationAsync(UserDto registration)
        {
            if (await _context.UserRegisters.AnyAsync(u => u.Email == registration.Email))
                return null;

            var user = new UserRegisters();
            var hashedpassword = new PasswordHasher<UserRegisters>()
                .HashPassword(user, registration.Password);

            user.FirstName = registration.FirstName;
            user.LastName = registration.LastName;
            user.Password = hashedpassword;
            user.Email = registration.Email;
            user.Role = registration.Role;
            

            _context.UserRegisters.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<List<UserProfileDto>> UserProfile()
        {
            var result = await _context.UserRegisters
                .Select(u => new UserProfileDto
                {
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Role = u.Role
                }).ToListAsync();

            return result;
        }

    }
}
