using System.Threading;

namespace CognitoDashboard.Models
{
    public static class StringExtensions
    {
        public static string ToTitleCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return string.Empty;

            value = value.Replace('_', ' ');
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());
        }
    }
}
