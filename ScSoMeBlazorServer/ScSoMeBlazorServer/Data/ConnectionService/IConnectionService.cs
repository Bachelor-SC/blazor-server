using ScSoMeBlazorServer.Models;

namespace ScSoMeBlazorServer.Data.ConnectionService
{
    public interface IConnectionService
    {
        public Task<List<Connection>> GetAllConnections(string username);

        public Task AddConnection(string username, string connectionUsername);

        public Task<bool> CheckIfAlreadyConnected(string username, string connectionUsername);

        public Task DeleteConnection(string username, string connectionUsername);

        public Task AddBlock(string username, string connectionUsername);

        public Task<List<Connection>> GetBlocksForUser(string username);

        public Task<string> CheckIfBlocked(string username, string connectionUsername);
        public Task DeleteBlock(string username, string connectionUsername);

    }
}
