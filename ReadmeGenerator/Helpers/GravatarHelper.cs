using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Quera.Helpers;

public static class GravatarHelper {
    private const string GravatarBaseUrl = "https://www.gravatar.com/avatar/";

    public static async Task<string?> GetGravatarUrlAsync(string email) {
        if (string.IsNullOrWhiteSpace(email))
            return null;

        // Compute the MD5 hash of the trimmed and lowercased email
        var hash = GetMd5Hash(email.Trim().ToLower());

        // Construct the Gravatar URL
        var gravatarUrl = $"{GravatarBaseUrl}{hash}?d=404"; // 404 response if no custom image

        // Check if the URL returns a valid image (non-default)
        var imageExists = await CheckIfImageExists(gravatarUrl);

        return imageExists ? gravatarUrl : null;
    }

    private static string GetMd5Hash(string input) {
        using var md5 = MD5.Create();
        var inputBytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = md5.ComputeHash(inputBytes);

        var sb = new StringBuilder();
        foreach (var b in hashBytes)
            sb.Append(b.ToString("x2"));

        return sb.ToString();
    }

    private static async Task<bool> CheckIfImageExists(string url) {
        try {
            using var client = new HttpClient();
            var response = await client.GetAsync(url);

            // 404 means the image does not exist (or is default)
            return response.IsSuccessStatusCode;
        }
        catch {
            return false; // Handle exceptions gracefully
        }
    }
}