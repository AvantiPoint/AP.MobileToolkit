using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using NCrontab;
using ReactiveUI;

namespace AP.MobileToolkit.Reactive
{
    public static class ObservableCron
    {
        public static IObservable<int> Cron(string cron) => Cron(cron, RxApp.TaskpoolScheduler);

        public static IObservable<int> Cron(string cron, IScheduler scheduler)
        {
            var schedule = CrontabSchedule.Parse(cron);
            return Observable.Generate(
                0,
                d => true,
                d => d + 1,
                d => d,
                timeSelector: d =>
                {
                    return new DateTimeOffset(schedule.GetNextOccurrence(scheduler.Now.DateTime));
                },
                scheduler: scheduler);
        }
    }
}
