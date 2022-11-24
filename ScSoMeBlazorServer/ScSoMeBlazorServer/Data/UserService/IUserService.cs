using ScSoMeBlazorServer.Models.User;

namespace ScSoMeBlazorServer.Data.UserService
{
    public interface IUserService
    {
        Task<UserInfo> GetUserInfo(string username);

        Task<User> GetValidatedUser(string username, string password);
        Task PostCreateUser(UserInfo userInfo);
        Task<bool> PostPasswordHash(UserInfo user);
        Task DeleteUser(string username);

        Task BlockUser(string username, string toBeBlockedUser);


    }
}
