using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrikafa.Common
{
    //ROOT 
    public class Settings
    {
        public static Settings Current { get; set; }

        public Settings()
        {
            Current = this;
        }

        public MongoDB MongoDB { get; set; }
        public Application Application { get; set; }
        public SMTP SMTP { get; set; }
        public Google Google { get; set; }
    }

    #region LEVEL1

    //LEVEL 1
    public class MongoDB
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public class Application
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Adminuser { get; set; }
        public string Adminpass { get; set; }
        public EMail EMail { get; set; }
        public ServiceKeys ServiceKeys { get; set; }
        public string EMailTemplateRootPath { get; set; }
        public string Host { get; set; }
        public string RedirectorHost { get; set; }
        public Quotas Quotas { get; set; }
    }

    public class SMTP
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string EnableSsl { get; set; }
    }

    public class Google
    {
        public string Recaptcha_V3_SiteKey { get; set; }
        public string Recaptcha_V3_SecretKey { get; set; }
        public string RecaptchaScoreTreshold { get; set; }
    }

    #endregion


    #region LEVEL2

    //LEVEL2
    public class EMail
    {
        public string NoReplyAddress { get; set; }
        public string ValidationPattern { get; set; }
        public string FeedbackAddress { get; set; }
    }

    public class ServiceKeys
    {
        public string EMailList { get; set; }
    }

    public class Quotas
    {
        public int IndividualQuota { get; set; }

        public int CorporateQuota { get; set; }

        public int EnterpriseQuota { get; set; }
    }

    #endregion

}
