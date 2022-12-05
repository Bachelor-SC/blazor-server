namespace ScSoMeBlazorServer.Network
{
    public interface IAPIClient
    {
        public Task<string> postToAPI(string path, Object PostObject);
        public Task<string> getFromAPI(string path);
    }
}
