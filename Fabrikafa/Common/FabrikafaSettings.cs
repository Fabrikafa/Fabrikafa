using System;
using System.Text.Json.Serialization;

namespace Fabrikafa.Common;

[Obsolete("Use Fabrikafa.Platform.Settings.FabrikafaSettings")]
public class FabrikafaSettings_
{
    public FabrikafaSettings_()
    {
        Logging = new Logging();
        AllowedHosts = String.Empty;
        Settings = new Settings();
    }

    public Logging Logging { get; set; }

    public string AllowedHosts { get; set; }

    public Settings Settings { get; set; }
}

#region ROOT

//ROOT 
public class Logging
{
    public Logging()
    {
        LogLevel = new LogLevel();
    }

    public LogLevel LogLevel { get; set; }
}

public class LogLevel
{
    public LogLevel()
    {
        Default = String.Empty;
        Microsoft_AspNetCore = String.Empty;
    }

    public string Default { get; set; }

    //TODO: we cannot find a way to bind this property with the names have dot in it. We need to assign this value manually
    [JsonPropertyName("Microsoft.AspNetCore")]
    public string Microsoft_AspNetCore { get; set; }
}

public class Settings
{
    public static Settings Current { get; set; }

    public Settings()
    {
        Current = this;
        MongoDB = new MongoDB();
        Application = new Application();
        SMTP = new SMTP();
        Google = new Google();
    }

    public MongoDB MongoDB { get; set; }

    public Application Application { get; set; }

    public SMTP SMTP { get; set; }

    public Google Google { get; set; }
} 

#endregion

#region LEVEL1

//LEVEL 1
public class MongoDB
{
    public MongoDB()
    {
        ConnectionString = String.Empty;
        DatabaseName = String.Empty;
    }

    public string ConnectionString { get; set; }

    public string DatabaseName { get; set; }
}

public class Application
{
    public Application()
    {
        EMail = new EMail();
        ServiceKeys = new ServiceKeys();
        Quotas = new Quotas();
        API = new API();
    }

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

    public API API { get; set; }
}

public class SMTP
{
    public string Host { get; set; }

    public string Port { get; set; }

    public string Username { get; set; }

    public string Password { get; set; }

    public string EnableSsl { get; set; }

    public string UseDefaultCredentials { get; set; }

    public string Domain { get; set; }
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

public class API
{
    public string Secret { get; set; }

    public string Issuer { get; set; }

    public string Audience { get; set; }
}

#endregion