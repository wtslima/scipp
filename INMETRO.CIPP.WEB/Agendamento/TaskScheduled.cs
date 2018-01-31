using INMETRO.CIPP.SERVICOS.Interfaces;
using INMETRO.CIPP.SERVICOS.Servicos;
using INMETRO.CIPP.WEB.ServicosAutomatizados;
using Quartz;
using System;

namespace INMETRO.CIPP.WEB.Agendamento
{
    public class TaskScheduled : IJob
    {
      

        public void Execute(IJobExecutionContext context)
        {
            
            try
            {
                //var resultado =  servico.DownloadPorRotinaAutomatica().ConfigureAwait(true);
                

            }
            catch (Exception ex)
            {
                //email.SendEmail("ryan@rrichardsconsulting.com", "Aloha Bot API Error", string.Format(
                //     "Task Scheduler Notification Sent error. Exception: {0}", ex.Message)).ConfigureAwait(false);
            }

        }
    }
}