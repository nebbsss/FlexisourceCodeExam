using App.Infrastructure.Helpers;
using System.Web;

namespace App.Infrastructure.Extensions;

public  static class StringExtensions
{
    public static string? Sanitize(this string? input)
    {
        return XssHelper.SanitizeString(input);
    }

    public static string? HtmlDecode(this string? input)
    {
        return HttpUtility.HtmlDecode(input);
    }
}
