using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace ShopBridge.Common
{
    public static class Log
    {
        public static void ErrorMsg(Exception ex)
        {
            try
            {
                string ErrorlineNo, Message, Source, StackTrace, ExType;
                ErrorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
                Message = ex.Message;
                Source = ex.Source;
                StackTrace = ex.StackTrace;
                ExType = ex.GetType().ToString();

                var configuration = new ConfigurationBuilder()
                                        .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false)
                                        .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.Development.json"), optional: false).Build();

                string AccountAddress = configuration.GetSection("EmailConfiguration:AccountAddress").Value;
                string FromEmailAddress = configuration.GetSection("EmailConfiguration:FromEmailAddress").Value;
                string ReplyToEmailAddress = configuration.GetSection("EmailConfiguration:ReplyToEmailAddress").Value;
                string HostPassword = configuration.GetSection("EmailConfiguration:HostPassword").Value;
                string Host = configuration.GetSection("EmailConfiguration:Host").Value;
                int Port = configuration.GetValue<Int32>("EmailConfiguration:Port");

                string emailBody = File.ReadAllText(configuration.GetSection("FilePath:EmailTemplatePath").Value + "ExceptionEmail.html");
                emailBody = emailBody.Replace("[LogWrittenDate]", DateTime.Now.ToString());
                emailBody = emailBody.Replace("[ErrorlineNo]", ErrorlineNo);
                emailBody = emailBody.Replace("[Message]", Message);
                emailBody = emailBody.Replace("[Source]", Source);
                emailBody = emailBody.Replace("[StackTrace]", StackTrace);
                emailBody = emailBody.Replace("[ExType]", ExType);

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(FromEmailAddress),
                    Subject = "ShopBridge - Exception!",
                    Body = emailBody,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(ReplyToEmailAddress);

                var client = new SmtpClient(Host, Port)
                {
                    Credentials = new NetworkCredential(AccountAddress, HostPassword),
                    EnableSsl = true
                };
                bool sendEmail = configuration.GetValue<bool>("EmailConfiguration:SendEmail");
                if (sendEmail)
                {
                    client.Send(mailMessage);
                }
            }
            catch (Exception e)
            {
                e.ToString();
            }
        }
    }
}
