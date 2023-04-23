using System.Security.Cryptography;

namespace BasicAuth.Service
{
    public static class AuthTokenGenerator
    {
        public static string GenerateAuthToken()
        {
            var rng = new RNGCryptoServiceProvider();
            var tokenBytes = new byte[32];
            rng.GetBytes(tokenBytes);
            return Convert.ToBase64String(tokenBytes);
        }
    }
}
