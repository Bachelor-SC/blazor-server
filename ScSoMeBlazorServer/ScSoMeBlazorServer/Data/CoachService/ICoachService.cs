using ScSoMeBlazorServer.Models;

namespace ScSoMeBlazorServer.Data.CoachService
{
    public interface ICoachService
    {
        public Task<List<Coach>> GetCoaches();

        public Task<Coach> GetCoach(string coach);

        public Task PostCoach(Coach coach);
    }
}
