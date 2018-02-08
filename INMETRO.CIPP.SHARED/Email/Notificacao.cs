using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace INMETRO.CIPP.SHARED.Email
{
    public class Notificacao
    {
        public void EnvioEmailComAnexo(string email, string assunto, string texto, string path, string custOrder)
        {

            var fromAddress = new MailAddress("aloha@deliveryhawaii.com", "Delivery Hawaii");
            var toAddress = new MailAddress(email);
            Attachment attachment = null;


            if (!string.IsNullOrEmpty(path))
            {
                attachment = new Attachment(path);
            }

            var smtp = new SmtpClient
            {
                Host = "email-smtp.us-west-2.amazonaws.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = true,
                Credentials = new NetworkCredential("AKIAIGUPGKU4GQT54OAA", "Ag5K6uLwkbbypqGFdvPWWA3MYhUYY0vJVOqD05deT568")

            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {

                Subject = assunto,
                Body = texto,
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
    }
}