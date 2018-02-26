using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace INMETRO.CIPP.SHARED.Email
{
    public class Notificacao
    {
        public void EnviarEmailComAnexo(string email, string path)
        {

            var fromAddress = new MailAddress("cipp_naoresponda@inmetro.gov.br", "CIPP");
            var toAddress = new MailAddress(email);
            Attachment attachment = null;


            if (!string.IsNullOrEmpty(path))
            {
                attachment = new Attachment(path);
            }

            var smtp = new SmtpClient
            {
                
                Host = "webmail.inmetro.gov.br",
                Port = 25,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential("cipp_naoresponda", "#@cert!654#@")

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
                    //await SendEmail("ryan@rrichardsconsulting.com", "Failed To Send Notification Email - DHI", String.Format("Failed to send email\n\n Exception: {0}\n\n to {1}\n\n, subject: {2}\n\n text: {3}", ex.Message, email, subject, text));
                }

            }

        }


        public void EnviarEmail(string email, string mensagem)
        {

            var fromAddress = new MailAddress("cipp_naoresponda@inmetro.gov.br", "CIPP");
            var toAddress = new MailAddress(email);
            string mensagemErro = mensagem;

            
            var smtp = new SmtpClient
            {

                Host = "webmail.inmetro.gov.br",
                Port = 25,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential("cipp_naoresponda", "#@cert!654#@")

            };



            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = "Erro na realização da inspeções automáticas",
                Body = mensagemErro,
                IsBodyHtml = false
            })
            {

                try
                {
                    smtp.Send(message);

                }
                catch (Exception ex)
                {
                    //await SendEmail("ryan@rrichardsconsulting.com", "Failed To Send Notification Email - DHI", String.Format("Failed to send email\n\n Exception: {0}\n\n to {1}\n\n, subject: {2}\n\n text: {3}", ex.Message, email, subject, text));
                }

            }

        }
    }
}