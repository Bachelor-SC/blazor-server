using ScSoMeBlazorServer.Models.UserData;
using ScSoMeBlazorServer.Network;
using System;
using System.IO;
using System.Net;
using System.Text.Json;

namespace ScSoMeBlazorServer.Data.UserService
{
    public class UserService : IUserService
    {
        private readonly IAPIClient client;

        public UserService(IAPIClient client)
        {
            this.client = client;
        }

        public Task BlockUser(string username, string toBeBlockedUser)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteUser(string username)
        {
            await client.deleteFromAPI($"User/{username}");
        }

        public async Task<UserInfo> GetUserInfo(string username)
        {
            string json = await client.getFromAPI($"User/Info?username={username}");

            UserInfo result = JsonSerializer.Deserialize<UserInfo>(json);

            return result;
        }

        public async Task<User> GetValidatedUser(string username, string password)
        {
           string json = await client.getFromAPI($"User/Login?username={username}&password={password}");

           User result = JsonSerializer.Deserialize<User>(json);

           return result;
        }

        public async Task PatchUserInfo(UserInfo user)
        {
            await client.patchToAPI($"User/Patch?user={user}", user);
        }

        public async Task PostCreateUser(UserInfo userInfo)
        {
            await client.postToAPI($"User/Signup?user={userInfo}", userInfo);
        }

    }
}
