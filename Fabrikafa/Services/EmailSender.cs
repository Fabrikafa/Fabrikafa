using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Fabrikafa.Common;
using Microsoft.Extensions.Configuration;

namespace Fabrikafa.Services;

/// <summary>
/// Custom e-mail sender for fabrikafa when there is no SMTP settings
/// </summary>
public class DevEmailSender : IEmailSender
{
    readonly IConfiguration _configuration;

    public DevEmailSender(IConfiguration Configuration)
    {
        this._configuration = Configuration;
    }

    /// <summary>
    /// Sends an email to Emails with given subject and HTML content
    /// </summary>
    /// <param name="Emails">TO Emails string seperated with valid split chars</param>
    /// <param name="Subject">Subject of the email</param>
    /// <param name="HtmlMessage">Body of email as HTML</param>
    /// <returns>Task</returns>
    public Task SendEmailAsync(string email, string subject, string message)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Sends an email to Emails TO, CC and BCC with given subject and HTML content
    /// </summary>
    /// <param name="EmailsTo">To Emails string seperated with valid split chars</param>
    /// <param name="EmailsCc">Cc Emails string seperated with valid split chars</param>
    /// <param name="EmailsBcc">Bcc Emails string seperated with valid split chars</param>
    /// <param name="Subject">Subject of the email</param>
    /// <param name="HtmlMessage">Body of email as HTML</param>
    /// <returns>Task</returns>
    public Task SendEmailAsync(string EmailsTo, string EmailsCc, string EmailsBcc, string Subject, string HtmlMessage)
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
    readonly string smtpDomain;

    readonly string noreplyEmail;
    readonly string validationPattern;

    private readonly FabrikafaSettings_ fabrikafaSettings;

    public EmailSender(IConfiguration Configuration)
    {
        this._configuration = Configuration;

        fabrikafaSettings = _configuration.Get<FabrikafaSettings_>();

        smtpHost = fabrikafaSettings.Settings.SMTP.Host;
        int.TryParse(fabrikafaSettings.Settings.SMTP.Port, out smtpPort);
        bool.TryParse(fabrikafaSettings.Settings.SMTP.EnableSsl, out enableSsl);
        bool.TryParse(fabrikafaSettings.Settings.SMTP.UseDefaultCredentials, out useDefaultCredentials);
        smtpUsername = fabrikafaSettings.Settings.SMTP.Username;
        smtpPassword = fabrikafaSettings.Settings.SMTP.Password;
        smtpDomain = fabrikafaSettings.Settings.SMTP.Domain;

        noreplyEmail = fabrikafaSettings.Settings.Application.EMail.NoReplyAddress;
        validationPattern = fabrikafaSettings.Settings.Application.EMail.ValidationPattern;
    }

    /// <summary>
    /// Sends an email to Emails with given subject and HTML content
    /// </summary>
    /// <param name="Emails">TO Emails string seperated with valid split chars</param>
    /// <param name="Subject">Subject of the email</param>
    /// <param name="HtmlMessage">Body of email as HTML</param>
    /// <returns>Task</returns>
    public async Task SendEmailAsync(string Emails, string Subject, string HtmlMessage)
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

        addressAddHelper(Emails, mailMessage, emailType.To);

        mailMessage.Subject = Subject;
        mailMessage.Body = HtmlMessage;
        mailMessage.IsBodyHtml = true;

        //// Set the method that is called back when the send operation ends.
        //client.SendCompleted += new
        //SendCompletedEventHandler(SendCompletedCallback);

        await client.SendMailAsync(mailMessage);
    }

    /// <summary>
    /// Sends an email to Emails TO, CC and BCC with given subject and HTML content
    /// </summary>
    /// <param name="EmailsTo">To Emails string seperated with valid split chars</param>
    /// <param name="EmailsCc">Cc Emails string seperated with valid split chars</param>
    /// <param name="EmailsBcc">Bcc Emails string seperated with valid split chars</param>
    /// <param name="Subject">Subject of the email</param>
    /// <param name="HtmlMessage">Body of email as HTML</param>
    /// <returns>Task</returns>
    public async Task SendEmailAsync(string EmailsTo, string EmailsCc, string EmailsBcc, string Subject, string HtmlMessage)
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

        addressAddHelper(EmailsTo, mailMessage, emailType.To);
        addressAddHelper(EmailsCc, mailMessage, emailType.Cc);
        addressAddHelper(EmailsBcc, mailMessage, emailType.Bcc);

        mailMessage.Subject = Subject;
        mailMessage.Body = HtmlMessage;
        mailMessage.IsBodyHtml = true;

        await client.SendMailAsync(mailMessage);
    }

    /// <summary>
    /// Add emails to relevant collection of MailMessage 
    /// </summary>
    /// <param name="Emails">Emails string seperated with valid split chars</param>
    /// <param name="MailMessage">MailMessage instance to add emails</param>
    /// <param name="EmailType">Type to determine which MailMessage email collection will be used</param>
    private void addressAddHelper(string Emails, MailMessage MailMessage, emailType EmailType)
    {
        List<string> addresses = Common.Functions.SplitEMailAddresses(Emails);

        foreach (string item in addresses)
        {
            string emailAddress = item.RemoveAllWhitespaces();

            if (Common.Functions.IsValidEmail(emailAddress, validationPattern)) //add if the email address is valid against the validation pattern
            {
                switch (EmailType)
                {
                    case emailType.To:
                        MailMessage.To.Add(new MailAddress(emailAddress));
                        break;
                    case emailType.Cc:
                        MailMessage.CC.Add(new MailAddress(emailAddress));
                        break;
                    case emailType.Bcc:
                        MailMessage.Bcc.Add(new MailAddress(emailAddress));
                        break;
                    default:
                        MailMessage.To.Add(new MailAddress(emailAddress));
                        break;
                }
            }
        }
    }

    /// <summary>
    /// This email type is used to determine type of email collection when adding to relevant collection of MailMessage 
    /// </summary>
    private enum emailType
    { 
        To = 0,
        Cc = 1,
        Bcc = 2
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
