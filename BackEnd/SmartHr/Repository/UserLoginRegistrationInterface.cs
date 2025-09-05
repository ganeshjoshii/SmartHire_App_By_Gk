using Smart_HR___RMS.Dtos;


namespace Smart_HR___RMS.Repository
{
    public interface IUserLoginRegistrationInterface
    {
        public Task<UserRegisters> RegistrationAsync(UserDto registration);
        Task<UserRegisters?> LoginAsync(LoginDto login);

        public Task<List<UserProfileDto>> UserProfile();
    }
}
