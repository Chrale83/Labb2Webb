using System.Text;

namespace Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static string FormatString(this string stringToFormat)
        {
            string s = stringToFormat.Trim().ToLower();
            StringBuilder sr = new();

            for (int i = 0; i < s.Length; i++)
            {
                if (i < s.Length - 1 && char.IsWhiteSpace(s[i]) && char.IsWhiteSpace(s[i + 1]))
                {
                    continue;
                }
                sr.Append(s[i]);
            }

            return sr.ToString();
        }
    }
}
