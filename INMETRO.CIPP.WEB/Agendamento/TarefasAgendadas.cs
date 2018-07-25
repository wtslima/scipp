using System;
using System.Configuration;
using Quartz;
using Quartz.Impl;

namespace INMETRO.CIPP.WEB.Agendamento
{
    public class TarefasAgendadas
    {

        public static ISchedulerFactory Factory;
       

        public static void TarefasRegistradas()
        {
            var factory = new StdSchedulerFactory();

            var sched = factory.GetScheduler();
            sched.Start();

            DownloadPorRotinaAutomaticaScheduled(sched);

        }

        private static readonly string HoraAgendada = ConfigurationManager.AppSettings["HoraAgendadaDownload"];
        private static readonly string MinutosAgendados = ConfigurationManager.AppSettings["MinutosAgendadosDownload"];

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
                            .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(Convert.ToInt32(HoraAgendada), Convert.ToInt32(MinutosAgendados)
                            )))
                            .ForJob(jobDownloadPorRotinaAutomatica)
                            .Build();

            scheduler.ScheduleJob(jobDownloadPorRotinaAutomatica, triggerDownloadPorRotinaAutomatica);

        }



    }
}
