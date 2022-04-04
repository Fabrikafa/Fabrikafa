using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

using Fabrikafa;
using System.Diagnostics;

namespace Fabrikafa.Services;

// This class is used by the application to send email for account confirmation and password reset.
// For more details see https://go.microsoft.com/fwlink/?LinkID=532713
public class DevEmailSender : IEmailSender
{
    readonly IConfiguration _configuration;

    public DevEmailSender(IConfiguration Configuration)
    {
        this._configuration = Configuration;

    }

    public Task SendEmailAsync(string email, string subject, string message)
    {
        return Task.CompletedTask;
    }
}

/// <summary>
/// Custom e-mail sender for fabrikafa
/// </summary>
public class EmailSender : IEmailSender
{
    readonly IConfiguration _configuration;

    readonly string smtpHost;
    readonly int smtpPort;
    readonly bool enableSsl;
    readonly bool useDefaultCredentials;
    readonly string smtpUsername;
    readonly string smtpPassword;
    //readonly string smtpDomain;

    readonly string noreplyEmail;
    readonly string validationPattern;

    public EmailSender(IConfiguration Configuration)
    {
        this._configuration = Configuration;

        smtpHost = _configuration["Settings:SMTP:Host"];
        int.TryParse(_configuration["Settings:SMTP:Port"], out smtpPort);
        bool.TryParse(_configuration["Settings:SMTP:EnableSsl"], out enableSsl);
        bool.TryParse(_configuration["Settings:SMTP:UseDefaultCredentials"], out useDefaultCredentials);
        smtpUsername = _configuration["Settings:SMTP:Username"];
        smtpPassword = _configuration["Settings:SMTP:Password"];
        //smtpDomain = _configuration["Settings:SMTP:Domain"];

        noreplyEmail = _configuration["Settings:Application:EMail:NoReplyAddress"];
        validationPattern = _configuration["Settings:Application:EMail:ValidationPattern"];
    }

    public async Task SendEmailAsync(string emails, string subject, string htmlMessage)
    {
        var client = new SmtpClient(smtpHost, smtpPort)
        {
            EnableSsl = enableSsl,
            UseDefaultCredentials = useDefaultCredentials,
            Credentials = new NetworkCredential(smtpUsername, smtpPassword)
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(noreplyEmail)
        };

        List<string> toAddresses = Common.Functions.SplitEMailAddresses(emails);
       
        foreach (string item in toAddresses) //set to addresses
        {
            string emailAddress = item.RemoveAllWhitespaces();

            if (Common.Functions.IsValidEmail(emailAddress, validationPattern)) //add if the email address is valid against the validation pattern
            {
                mailMessage.To.Add(new MailAddress(emailAddress));
            }
        }

        mailMessage.Subject = subject;
        mailMessage.Body = htmlMessage;
        mailMessage.IsBodyHtml = true;

        //// Set the method that is called back when the send operation ends.
        //client.SendCompleted += new
        //SendCompletedEventHandler(SendCompletedCallback);

        await client.SendMailAsync(mailMessage);
    }

    static bool mailSent = false;
    private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
    {
        // Get the unique identifier for this asynchronous operation.
        String token = (string)e.UserState;

        if (e.Cancelled)
        {
            Console.WriteLine("[{0}] Send canceled.", token);
        }
        if (e.Error != null)
        {
            Console.WriteLine("[{0}] {1}", token, e.Error.ToString());
        }
        else
        {
            Console.WriteLine("Message sent.");
            mailSent = true;
        }

        Debug.Assert(mailSent);
    }
}
