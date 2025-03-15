using System;
using Microsoft.AspNetCore.Http;

namespace Fabrikafa.Web;

public class Functions
{
    public static void WriteCookie(string Key, string Value, IHttpContextAccessor httpContextAccessor)
    {
        var option = new CookieOptions();
        option.Expires = DateTime.Now.AddYears(10); // set expires to very long time from now.

        httpContextAccessor.HttpContext.Response.Cookies.Append(Key, Value, option);
    }

    public static string ReadCookie(string Key, IHttpContextAccessor httpContextAccessor)
    {
        string value = string.Empty;

        try
        {
            if (httpContextAccessor.HttpContext.Request.Cookies.ContainsKey(Key))
            {
                value = httpContextAccessor.HttpContext.Request.Cookies[Key];
            }
            else
            {
                WriteCookie(Key, value, httpContextAccessor);
            }
        }
        catch (Exception)
        {
            WriteCookie(Key, value, httpContextAccessor);
        }

        return value;
    }
}
