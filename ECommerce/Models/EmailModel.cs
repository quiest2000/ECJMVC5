﻿using System.Net.Mail;

namespace ECommerce.Models
{
    public class EmailModel
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public EmailModel(string to, string sub, string bo)
        {
            this.From = "tmdt2012j@gmail.com";
            this.To = to;
            this.Subject = sub;
            this.Body = bo;
        }
    }

    public class EmailTool
    {
        public bool SendMail(EmailModel model)
        {
            if (string.IsNullOrEmpty(model.From) || string.IsNullOrEmpty(model.To) || string.IsNullOrEmpty(model.Subject) || string.IsNullOrEmpty(model.Body))
                return false;
            try
            {
                var mail = new MailMessage();
                mail.To.Add(model.To);
                mail.From = new MailAddress(model.From);
                mail.Subject = model.Subject;
                var Body = model.Body;
                mail.Body = Body;
                mail.IsBodyHtml = true;
                var smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential
                (model.From, "thuongmaidientu");// Enter seders User name and password
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            
        }
    }
}