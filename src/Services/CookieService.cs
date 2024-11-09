using Microsoft.AspNetCore.Http;
using System;

public class CookieService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CookieService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void SetCookie(string key, string value, int? expireTime)
    {
        CookieOptions option = new CookieOptions();

        if (expireTime.HasValue)
            option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
        else
            option.Expires = DateTime.Now.AddMilliseconds(10);

        _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, option);
    }

    public string GetCookie(string key)
    {
        return _httpContextAccessor.HttpContext.Request.Cookies[key];
    }

    public void DeleteCookie(string key)
    {
        _httpContextAccessor.HttpContext.Response.Cookies.Delete(key);
    }
}
