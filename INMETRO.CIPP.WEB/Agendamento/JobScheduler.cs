using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Common.Logging.Configuration;

namespace INMETRO.CIPP.WEB.Agendamento
{
    public class JobScheduler
    {
        public static void Start()
        {

            StdSchedulerFactory factory = new StdSchedulerFactory();

            // get a scheduler
            IScheduler sched = factory.GetScheduler();
            sched.Start();

            
            IJobDetail job = JobBuilder.Create<DownloadPorRotinaAutomaticaScheduled>()
                .WithIdentity("myJob", "group1")
                .Build();


            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule
                (s =>
                    s.WithIntervalInHours(24)
                        .OnEveryDay()
                        .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(0, 0))
                        //.EndingDailyAt(TimeOfDay.HourAndMinuteOfDay(19, 0))
                        //.InTimeZone(System.TimeZoneInfo.FindSystemTimeZoneById("Hawaiian Standard Time"))
                )
                .Build();

            sched.ScheduleJob(job, trigger);
        }
    }
}