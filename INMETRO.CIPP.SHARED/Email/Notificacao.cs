using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace INMETRO.CIPP.SHARED.Email
{
    public class Notificacao
    {
        public void EnviarEmailComAnexo(string email, string path)
        {

            var fromAddress = new MailAddress(Configurations.EmailSCIPPAdministrativo(), "CIPP");
            var toAddress = new MailAddress(email);
            Attachment attachment = null;


            if (!string.IsNullOrEmpty(path))
            {
                attachment = new Attachment(path);
            }

            var smtp = new SmtpClient
            {
               
                Host = Configurations.HostEmail(),
                Port = 25,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(Configurations.CredentialMailUsername(), Configurations.CredentialMailPassword())

            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {

                Subject = "Inspeções realizadas por rotina automática",
                Body = "Segue em anexo as inspeções",
                IsBodyHtml = true,
                Attachments = { attachment }

            })
            {

                try
                {
                    smtp.Send(message);

                }
                catch 
                {
                    // ignored
                }
            }

        }


        public void EnviarEmail(string email, List<string> erros, string codigo)
        {

            var smtp = new SmtpClient
            {
                
                Host = Configurations.HostEmail(),
                Port = 25,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(Configurations.CredentialMailUsername(), Configurations.CredentialMailPassword())

            };

            var messageErro = new MailMessage();

            messageErro.To.Add(email);
            messageErro.Bcc.Add("scipp-recebe@inmetro.gov.br");
            messageErro.Subject = "Erro na realização da inspeções automáticas";
          
            messageErro.From = new MailAddress(Configurations.EmailSCIPPAdministrativo(), "CIPP");
            messageErro.IsBodyHtml = true;

            foreach (var item in erros)
            {
                messageErro.Body += codigo + "</br>" +
                    item + "</br>";
            }


            try
            {
                smtp.Send(messageErro);

            }
            catch
            {
                // ignored
            }
        }



        public void EnviarEmailErroDownloadAutomatico(string email, List<string> erros)
        {

            var smtp = new SmtpClient
            {
                Host = Configurations.HostEmail(),
                Port = 25,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(Configurations.CredentialMailUsername(), Configurations.CredentialMailPassword())

            };

            var messageErro = new MailMessage();

            messageErro.To.Add(email);
            messageErro.Bcc.Add("scipp-recebe@inmetro.gov.br");
            messageErro.Subject = "Erro na realização da inspeções automáticas ";
            messageErro.From = new MailAddress(Configurations.EmailSCIPPAdministrativo(), "CIPP");
            messageErro.IsBodyHtml = true;

            foreach (var item in erros)
            {
                messageErro.Body += item + "</br>";
            }


            try
            {
                smtp.Send(messageErro);

            }
            catch 
            {
                // ignored
            }
        }
    }
}