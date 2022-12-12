using ScSoMeBlazorServer.Models.UserData;

namespace ScSoMeBlazorServer.Data.UserService
{
    public interface IUserService
    {
        Task<UserInfo> GetUserInfo(string username);

        Task<User> GetValidatedUser(string username, string password);
        Task PostCreateUser(UserInfo userInfo);
        Task DeleteUser(string username);

        Task BlockUser(string username, string toBeBlockedUser);

        Task PatchUserInfo(UserInfo user);
    }
}
