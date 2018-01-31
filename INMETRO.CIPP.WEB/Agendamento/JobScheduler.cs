using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace INMETRO.CIPP.WEB.Agendamento
{
    public class JobScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<TaskScheduled>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                (s =>
                    s.WithIntervalInMinutes(5)
                        .OnEveryDay()
                        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(18, 20))
                        //.EndingDailyAt(TimeOfDay.HourAndMinuteOfDay(19, 0))
                        //.InTimeZone(System.TimeZoneInfo.FindSystemTimeZoneById("Hawaiian Standard Time"))
                )
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}