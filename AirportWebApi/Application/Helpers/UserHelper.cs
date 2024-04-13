using System.Security.Cryptography;
using System.Text;

namespace Application.Helpers;

public static class UserHelper
{
    public static string ComputeSha256Hash(this string rawData)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));
        var builder = new StringBuilder();
        foreach (var t in bytes)
        {
            builder.Append(t.ToString(ClaimsConstants.HashFormat));
        }

        return builder.ToString();
    }
}