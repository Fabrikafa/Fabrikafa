using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Fabrikafa.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Fabrikafa.Services;

/// <summary>
/// Custom e-mail sender for fabrikafa when there is no SMTP_ settings
/// </summary>
public class DevEmailSender : IEmailSender
{
    readonly IConfiguration _configuration;

    public DevEmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    /// Sends an email to Emails with given subject and HTML content
    /// </summary>
    /// <param name="email">TO Emails string seperated with valid split chars</param>
    /// <param name="subject">Subject of the email</param>
    /// <param name="message">Body of email as HTML</param>
    /// <returns>Task</returns>
    public Task SendEmailAsync(string email, string subject, string message)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    /// Sends an email to Emails TO, CC and BCC with given subject and HTML content
    /// </summary>
    /// <param name="emailsTo">To Emails string seperated with valid split chars</param>
    /// <param name="emailsCc">Cc Emails string seperated with valid split chars</param>
    /// <param name="emailsBcc">Bcc Emails string seperated with valid split chars</param>
    /// <param name="subject">Subject of the email</param>
    /// <param name="htmlMessage">Body of email as HTML</param>
    /// <returns>Task</returns>
    public Task SendEmailAsync(string emailsTo, string emailsCc, string emailsBcc, string subject, string htmlMessage)
    {
        return Task.CompletedTask;
    }
}

/// <summary>
/// Custom e-mail sender for fabrikafa
/// </summary>
public class EmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;
    private readonly string _smtpHost;
    private readonly int _smtpPort;
    private readonly bool _enableSsl;
    private readonly bool _useDefaultCredentials;
    private readonly string _smtpUsername;
    private readonly string _smtpPassword;
    private readonly string _noreplyEmail;
    private readonly string _validationPattern;

    public EmailSender(IConfiguration configuration)
    {
        _configuration = configuration;

        _smtpHost = _configuration["CustomSettings:SMTP:Host"];
        int.TryParse(_configuration["CustomSettings:SMTP:Port"], out _smtpPort);
        bool.TryParse(_configuration["CustomSettings:SMTP:EnableSsl"], out _enableSsl);
        bool.TryParse(_configuration["CustomSettings:SMTP:UseDefaultCredentials"], out _useDefaultCredentials);
        _smtpUsername = _configuration["CustomSettings:SMTP:Username"];
        _smtpPassword = _configuration["CustSettings:SMTP:Password"];

        _noreplyEmail = _configuration["CustomSettings:App:EMail:NoReplyAddress"];
        _validationPattern = _configuration["CustomSettings:App:EMail:ValidationPattern"];
    }

    /// <summary>
    /// Sends an email to Emails with given subject and HTML content
    /// </summary>
    /// <param name="emails">TO Emails string seperated with valid split chars</param>
    /// <param name="subject">Subject of the email</param>
    /// <param name="htmlMessage">Body of email as HTML</param>
    /// <returns>Task</returns>
    public async Task SendEmailAsync(string emails, string subject, string htmlMessage)
    {
        await SendEmailAsync(emails, null, null, subject, htmlMessage);
    }

    /// <summary>
    /// Sends an email to Emails TO, CC and BCC with given subject and HTML content
    /// </summary>
    /// <param name="emailsTo">To Emails string seperated with valid split chars</param>
    /// <param name="emailsCc">Cc Emails string seperated with valid split chars</param>
    /// <param name="emailsBcc">Bcc Emails string seperated with valid split chars</param>
    /// <param name="subject">Subject of the email</param>
    /// <param name="htmlMessage">Body of email as HTML</param>
    /// <returns>Task</returns>
    public async Task SendEmailAsync(string emailsTo, string emailsCc, string emailsBcc, string subject, string htmlMessage)
    {
        using var client = new SmtpClient(_smtpHost, _smtpPort)
        {
            EnableSsl = _enableSsl,
            UseDefaultCredentials = _useDefaultCredentials,
            Credentials = new NetworkCredential(_smtpUsername, _smtpPassword)
        };

        using var mailMessage = new MailMessage
        {
            From = new MailAddress(_noreplyEmail),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };

        AddAddressesToMessage(emailsTo, mailMessage, EmailType.To);
        AddAddressesToMessage(emailsCc, mailMessage, EmailType.Cc);
        AddAddressesToMessage(emailsBcc, mailMessage, EmailType.Bcc);

        await client.SendMailAsync(mailMessage);
    }

    /// <summary>
    /// Add emails to relevant collection of MailMessage 
    /// </summary>
    /// <param name="emails">Emails string seperated with valid split chars</param>
    /// <param name="mailMessage">MailMessage instance to add emails</param>
    /// <param name="emailType">Type to determine which MailMessage email collection will be used</param>
    private void AddAddressesToMessage(string emails, MailMessage mailMessage, EmailType emailType)
    {
        if (string.IsNullOrWhiteSpace(emails))
            return;

        List<string> addresses = Common.Functions.SplitEMailAddresses(emails);

        foreach (string item in addresses)
        {
            string emailAddress = item.RemoveAllWhitespaces();

            if (Common.Functions.IsValidEmail(emailAddress, _validationPattern))
            {
                switch (emailType)
                {
                    case EmailType.To:
                        mailMessage.To.Add(new MailAddress(emailAddress));
                        break;
                    case EmailType.Cc:
                        mailMessage.CC.Add(new MailAddress(emailAddress));
                        break;
                    case EmailType.Bcc:
                        mailMessage.Bcc.Add(new MailAddress(emailAddress));
                        break;
                    default:
                        mailMessage.To.Add(new MailAddress(emailAddress));
                        break;
                }
            }
        }
    }

    /// <summary>
    /// This email type is used to determine type of email collection when adding to relevant collection of MailMessage 
    /// </summary>
    private enum EmailType
    { 
        To,
        Cc,
        Bcc
    }
}
