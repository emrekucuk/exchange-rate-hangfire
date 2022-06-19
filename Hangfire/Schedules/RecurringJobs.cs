using Hangfire.RecurringJobs.KurOperation;

namespace Hangfire.Schedules
{
    public static class RecurringJobs
    {
        //"*/10 * * * * *" 10saniye
        //"* * * * * *" 1 dk
        //"0 * * * * *" saat başı
        //"0 0,12 * * *" saat 12 ve 00 da
        //[AutomaticRetry(Attempts = 0, LogEvents = false, OnAttemptsExceeded = AttemptsExceededAction.Delete)]
        public static void UpdateKur()
        {
            RecurringJob.RemoveIfExists(nameof(UpdateKurScheduleJob));
            RecurringJob.AddOrUpdate<UpdateKurScheduleJob>(nameof(UpdateKurScheduleJob),
                job => job.Process(), "* * * * * *", TimeZoneInfo.Local);
        }
    }
}