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
            //var fromAddress = new MailAddress("aloha@deliveryhawaii.com", "Scipp");
            var toAddress = new MailAddress("wtslima@gmail.com");
            Attachment attachment = null;


            if (!string.IsNullOrEmpty(path))
            {
                attachment = new Attachment(path);
            }

            var smtp = new SmtpClient
            {
                //Host = "email-smtp.us-west-2.amazonaws.com",
                //Port = 587,
                //EnableSsl = true,
                //DeliveryMethod = SmtpDeliveryMethod.Network,
                //UseDefaultCredentials = true,
                //Credentials = new NetworkCredential("AKIAIGUPGKU4GQT54OAA", "Ag5K6uLwkbbypqGFdvPWWA3MYhUYY0vJVOqD05deT568")
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
                catch (Exception ex)
                {
                    // ignored
                }
            }

        }


        public void EnviarEmail(string email, List<string> erros, string codigo)
        {

            var smtp = new SmtpClient
            {
                //Host = "email-smtp.us-west-2.amazonaws.com",
                //Port = 587,
                //EnableSsl = true,
                //DeliveryMethod = SmtpDeliveryMethod.Network,
                //UseDefaultCredentials = true,
                //Credentials = new NetworkCredential("AKIAIGUPGKU4GQT54OAA", "Ag5K6uLwkbbypqGFdvPWWA3MYhUYY0vJVOqD05deT568")
                Host = Configurations.HostEmail(),
                Port = 25,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(Configurations.CredentialMailUsername(), Configurations.CredentialMailUsername())

            };

            var messageErro = new MailMessage();

            messageErro.To.Add("wtslima@gmail.com");
            messageErro.Subject = "Erro na realização da inspeções automáticas ";
            //messageErro.From = new MailAddress("aloha@deliveryhawaii.com", "Scipp");
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
            catch (Exception ex)
            {
                // ignored
            }
        }



        public void EnviarEmailErroDownloadAutomátivo(string email, List<string> erros)
        {

            var smtp = new SmtpClient
            {
                //Host = "email-smtp.us-west-2.amazonaws.com",
                //Port = 587,
                //EnableSsl = true,
                //DeliveryMethod = SmtpDeliveryMethod.Network,
                //UseDefaultCredentials = true,
                //Credentials = new NetworkCredential("AKIAIGUPGKU4GQT54OAA", "Ag5K6uLwkbbypqGFdvPWWA3MYhUYY0vJVOqD05deT568")
                Host = Configurations.HostEmail(),
                Port = 25,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential(Configurations.CredentialMailUsername(), Configurations.CredentialMailUsername())

            };

            var messageErro = new MailMessage();

            messageErro.To.Add("wtslima@gmail.com");
            messageErro.Subject = "Erro na realização da inspeções automáticas ";
            //messageErro.From = new MailAddress("aloha@deliveryhawaii.com", "Scipp");
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
            catch (Exception ex)
            {
                // ignored
            }
        }
    }
}