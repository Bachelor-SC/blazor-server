using ScSoMeBlazorServer.Models.User;
using System.Text.Json;

namespace ScSoMeBlazorServer.Data.UserService
{
    public class UserService : IUserService
    {
        private HttpClient client;
        private string userURL = "https://localhost:5003/User";

        public UserService()
        {
            client = new HttpClient();
        }

        public async Task<User> GetValidatedUser(string username, string password)
        {
            Task<string> info = client.GetStringAsync(userURL + $"Login?username={username}&password={password}");
            string message = await info;
            User result = JsonSerializer.Deserialize<User>(message);

            return result;
        }

        public Task DeleteUser(string username)
        {
            throw new NotImplementedException();
        }

        public Task<UserInfo> GetUserInfo(string username)
        {
            throw new NotImplementedException();
        }

        public Task PostCreateUser(UserInfo userInfo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PostPasswordHash(UserInfo user)
        {
            throw new NotImplementedException();
        }

        public Task BlockUser(string username, string toBeBlockedUser)
        {
            throw new NotImplementedException();
        }
    }
}
