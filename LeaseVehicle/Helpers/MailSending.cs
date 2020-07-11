using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace LeaseVehicle.Helpers
{
    public class MailSending
    {
        public static bool SendMail(string receiver, string subject, string message)
        {
            try
            {
                var senderEmail = new MailAddress("vehicleleasing2020@gmail.com");
                var receiverEmail = new MailAddress(receiver, "Receiver");
                var password = "Vleasing@2020";
                var sub = subject;
                var body = message;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body
                })
                {
                   smtp.Send(mess);
                }
                return true;
            }
            catch
            {
                return false;
            }
            
        }
    }
}