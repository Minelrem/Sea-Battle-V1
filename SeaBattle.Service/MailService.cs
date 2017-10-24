using Interfaces;
using SeaBattle.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SeaBattle.Service
{
    public class MailService:Base,IMail
    {
        public MailService(SeaBattleContext context):base(context)
        {

        }

        public async void SendMail(string mail)
        {
            MailAddress from = new MailAddress("millenium.minelrem@gmail.com", "SeaBattle");
            MailAddress to = new MailAddress(mail);

            MailMessage messge = new MailMessage(from, to);
            messge.Body = "Your account registred. To confirm go to ...";
            messge.Subject = "SeaBattle Acount registration";

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("millenium.minelrem@gmail.com", "1998validate");
            smtp.EnableSsl = true;

            await smtp.SendMailAsync(messge);

        }
    }
}
