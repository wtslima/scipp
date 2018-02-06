using Quartz;
using Quartz.Impl;

namespace INMETRO.CIPP.WEB.Agendamento
{
    public class TarefasAgendadas
    {

        protected static ISchedulerFactory Factory;

        public static void TarefasRegistradas()
        {
            var factory = new StdSchedulerFactory();

            var sched = factory.GetScheduler();
            sched.Start();

            DownloadPorRotinaAutomaticaScheduled(sched);
            ExclusaoPorRotinaAutomaticaScheduled(sched);

        }


        private static void DownloadPorRotinaAutomaticaScheduled(IScheduler scheduler)
        {
            var jobDownloadPorRotinaAutomatica = JobBuilder.Create<DownloadPorRotinaAutomaticaJob>()
                .WithIdentity("jobDownloadPorRotinaAutomatica", "group1")
                .Build();


            var triggerDownloadPorRotinaAutomatica = TriggerBuilder.Create()
                    .WithIdentity("triggerDownloadPorRotinaAutomatica", "group1")
                .WithDailyTimeIntervalSchedule
                (s => s.WithIntervalInHours(24)
                            .OnEveryDay()
                            .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(17, 22)))
                            .ForJob(jobDownloadPorRotinaAutomatica)
                            .Build();

            scheduler.ScheduleJob(jobDownloadPorRotinaAutomatica, triggerDownloadPorRotinaAutomatica);
        }

        private static void ExclusaoPorRotinaAutomaticaScheduled(IScheduler scheduler)
        {
            var jobExclusaoPorRotinaAutomatica = JobBuilder.Create<ExclusaoPorRotinaAutomaticaJob>()
                .WithIdentity("jobjobExclusaoPorRotinaAutomatica", "group1")
                .Build();


            var triggerjobExclusaoPorRotinaAutomatica = TriggerBuilder.Create()
                .WithIdentity("triggerExclusaoPorRotinaAutomatica", "group1")
                .WithDailyTimeIntervalSchedule
                (s => s.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(17, 20)))
                .ForJob(jobExclusaoPorRotinaAutomatica)
                .Build();

            scheduler.ScheduleJob(jobExclusaoPorRotinaAutomatica, triggerjobExclusaoPorRotinaAutomatica);
        }

       
    }
}