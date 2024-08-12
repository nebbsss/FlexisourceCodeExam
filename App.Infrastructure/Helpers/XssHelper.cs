using Ganss.Xss;

namespace App.Infrastructure.Helpers;

public class XssHelper
{
    private static HtmlSanitizer? _sanitizer = null;

    public static string? SanitizeString(string? str)
    {
        if (String.IsNullOrEmpty(str)) return str;

        str = Sanitize(str);
        return str;
    }

    private static string? Sanitize(string? str)
    {
        if (_sanitizer == null)
        {
            _sanitizer = new HtmlSanitizer();
        }

        if (String.IsNullOrEmpty(str)) return str;

        return _sanitizer.Sanitize(str);
    }
}

