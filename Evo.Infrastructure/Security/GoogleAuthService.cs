using Evo.Application.Contracts.Security;
using Google.Apis.Auth;
using Microsoft.Extensions.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace Evo.Infrastructure.Security
{
    public class GoogleAuthService(IConfiguration _config) : IGoogleAuthService
    {
        public async Task<GoogleUserInfo> ValidateIdTokenAsync(string idToken, string? audience = null, CancellationToken cancellationToken = default)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings();

            // If a specific audience (client id) is provided in config, validate against it
            var configuredClientId = _config["Google:ClientId"];
            var aud = audience ?? configuredClientId;
            if (!string.IsNullOrWhiteSpace(aud))
            {
                settings.Audience = new[] { aud };
            }

            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
                return new GoogleUserInfo(
                    payload.Subject,
                    payload.Email,
                    payload.Name ?? string.Empty,
                    payload.EmailVerified,
                    payload.Picture
                );
            }
            catch (Google.Apis.Auth.InvalidJwtException ex) when (ex.Message.Contains("expired"))
            {
                // Token expired - handle accordingly, e.g., throw custom exception or return null
                throw new Exception("Google token has expired. Please login again.");
            }
            catch (Exception ex)
            {
                // Other validation errors
                throw;
            }

        }

        /*
        * -----------------------------------------------------------------------------
        *  GoogleAuthService – ID Token Validation Process
        * -----------------------------------------------------------------------------
        *  Required NuGet Package:
        *      → Google.Apis.Auth
        *        (Provides GoogleJsonWebSignature.ValidateAsync() for verifying Google ID tokens)
        *
        *  Purpose:
        *      Validates a Google ID token received from the frontend (after Google Sign-In)
        *      and extracts verified user information (email, name, etc.).
        *
        *  Flow Summary:
        *  1️  Frontend:
        *      - The frontend (web or mobile app) uses Google Sign-In with your
        *        public Google OAuth Client ID (e.g., "1234567890-abc.apps.googleusercontent.com").
        *      - After a successful sign-in, Google returns an ID token (a JWT).
        *      - The frontend sends that ID token to your backend API (e.g., /api/account/google-login).
        *
        *  2️  Backend (this service):
        *      - The backend receives the ID token and calls:
        *          GoogleJsonWebSignature.ValidateAsync(idToken, settings)
        *      - The Google .NET SDK performs the validation locally.
        *
        *  3️  What ValidateAsync() does internally:
        *      a. Decodes the JWT (ID token) into header, payload, and signature parts.
        *      b. Fetches Google’s public RSA keys from:
        *         https://www.googleapis.com/oauth2/v3/certs
        *      c. Matches the "kid" (key ID) in the token header to one of Google’s keys.
        *      d. Verifies the token’s signature using that public key (RS256 algorithm).
        *      e. Validates standard claims:
        *           - iss  (issuer) must be "accounts.google.com" or "https://accounts.google.com"
        *           - exp  (expiration) must be in the future
        *           - aud  (audience) must match your app’s Client ID
        *           - iat  (issued at) should not be in the future
        *      f. If all checks pass, returns a verified GoogleJsonWebSignature.Payload object.
        *
        *  4️  Audience (Client ID):
        *      - The audience claim ("aud") identifies which app the token was issued for.
        *      - The backend uses its configured Client ID (from appsettings.json)
        *        to ensure the token was meant for this application only.
        *
        *  5️  Result:
        *      - If validation succeeds, the payload contains verified user data:
        *            payload.Subject        → unique Google user ID
        *            payload.Email          → user’s email
        *            payload.EmailVerified  → email verification status
        *            payload.Name           → full name (if available)
        *            payload.Picture        → profile picture URL
        *      - The backend maps this data to a custom model (GoogleUserInfo).
        *
        *  6️⃣  Security Notes:
        *      - The Client ID is public and safe to use in both frontend and backend.
        *      - The Client Secret is NOT used for ID token validation and must
        *        never be exposed to the frontend.
        *      - Token validation happens locally using Google’s public keys —
        *        no secret exchange is required.
        *
        * -----------------------------------------------------------------------------
        *  Example:
        *      var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
        *      // Throws exception if invalid; returns user info if valid.
        * -----------------------------------------------------------------------------
        */

    }
}

