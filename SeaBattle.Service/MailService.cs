using Interfaces;
using SeaBattle.Data.Context;
using System.Net;
using System.Net.Mail;

namespace SeaBattle.Service
{
    public class MailService:Base,IMail
    {
        public MailService(SeaBattleContext context):base(context)
        {

        }

        public async void SendMail(string mail)
        {
            MailAddress from = new MailAddress("ouremail", "SeaBattle");
            MailAddress to = new MailAddress(mail);

            MailMessage messge = new MailMessage(from, to);
            messge.Body = "Your account registred. To confirm go to ...";
            messge.Subject = "SeaBattle Acount registration";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("ouremail", "ourpass");
            smtp.EnableSsl = true;

            await smtp.SendMailAsync(messge);

        }
    }
}
