using NCrontab;
using ReactiveUI;
using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

namespace AP.MobileToolkit.Reactive
{
    public static class ObservableCron
    {
        public static IObservable<int> Cron(string cron) => Cron(cron, RxApp.TaskpoolScheduler);

        public static IObservable<int> Cron(string cron, IScheduler scheduler)
        {
            var schedule = CrontabSchedule.Parse(cron);
            return Observable.Generate(0, d => true, d => d + 1, d => d,
                d => new DateTimeOffset(schedule.GetNextOccurrence(scheduler.Now.DateTime)), scheduler);
        }
    }
}
