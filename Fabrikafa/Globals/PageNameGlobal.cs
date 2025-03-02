using System;

namespace Fabrikafa.Globals;

[Obsolete("Fabrikafa.Globals -> Fabrikafa.Platform")]
public class PageNameGlobal
{
    public const string _Home = "/index";

    public const string Contact = "/contact";
    public const string Message = "/message";

    public const string Dashboard = "/dashboard";

    public const string AccountAccessDenied = "/account/accessdenied";
    public const string AccountActivate = "/account/activate";
    public const string AccountApi = "/account/api";
    public const string AccountForgetPassword = "/account/forgetpassword";
    public const string AccountHome = "/account/index";
    public const string AccountLogin = "/account/login";
    public const string AccountLogout = "/account/logout";
    public const string AccountProfile = "/account/profile";
    public const string AccountRegister = "/account/register";
    public const string AccountResendActivation = "/account/resendactivation";   
    public const string AccountResetPassword = "/account/resetpassword";

    [Obsolete("Fabrikafa.Globals -> Fabrikafa.Platform")]
    public class Admin
    {
        public const string _Home = "/admin/index";

        public const string FreeEmailProvidersForm = "/admin/freeemailprovidersform";
        public const string FreeEmailProvidersExport = "/admin/freeemailprovidersexport";

        public const string DataMigration = "/admin/datamigration";
    }
}
