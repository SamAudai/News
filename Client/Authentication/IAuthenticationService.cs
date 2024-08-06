using News.Shared.DTOs.Account;
using News.Shared.DTOs.Adminstration;
using News.Shared.Models;

namespace News.Client.Authentication
{
    public interface IAuthenticationService
    {
        Task<RegistrationResponseDto> RegisterUser(UserRegistrationDto userRegistration);
        Task<LoginResponsDto> LoginUser(LoginDto login);
        Task LogoutUser();
        Task<List<ApplicationUser>> GetAllUsers();
        Task<ApplicationUser> GetUserByName(string userName);
        Task<ApplicationUser> GetUserByEmail(string userEmail);
        Task<UsersRolesDto> GetUserWithRoles(string userId);
        Task AddUserRoles(UsersRolesDto usersRoles);
        Task<RegistrationResponseDto> ChangePassword(ChangePasswordDto changePassword);
        Task ForgotPassword(ForgotPasswordDto forgotPassword);
        Task ResetPassword(ResetPasswordDto resetPassword);
    }
}
