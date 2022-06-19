namespace Hangfire.RecurringJobs.KurOperation
{
    public class UpdateKurScheduleJob
    {
        [AutomaticRetry(Attempts = 2)]
        public async Task Process()
        {
            await KurManager.UpdateKurAsync();
        }

    }
}
