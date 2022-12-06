using ScSoMeBlazorServer.Models;

namespace ScSoMeBlazorServer.Data.LogActivityService
{
    public interface ILogActivityService
    {
        public Task<List<Activity>> GetActivities(string user);

        public Task<Activity> GetActivity(string user);

        public Task LogActivity(Activity activity,string user);
    }
}
