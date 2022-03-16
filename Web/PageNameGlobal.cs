using System;
using System.Collections.Generic;
using System.Text;

namespace Fabrikafa.Web
{
    public class PageNameGlobal
    {
        public const string _Home = "/";

        public const string Contact = "/contact";
        public const string Message = "/message";

        public const string Dashboard = "/dashboard";

        public const string AccountHome = "/account/index";
        public const string AccountLogin = "/account/login";
        public const string AccountRegister = "/account/register";
        public const string AccountActivate = "/account/activate";
        public const string AccountResendActivation = "/account/resendactivation";
        public const string AccountForgetPassword = "/account/forgetpassword";
        public const string AccountResetPassword = "/account/resetpassword";

        public class Admin
        {
            public const string _Home = "/admin";

            public const string FreeEmailProvidersForm = "/admin/freeemailprovidersform";
            public const string FreeEmailProvidersExport = "/admin/freeemailprovidersexport";

            public const string DataMigration = "/admin/datamigration";
        }
    }
}
