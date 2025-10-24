using System.Threading;
using System.Threading.Tasks;

namespace Evo.Application.Contracts.Security
{
    public record GoogleUserInfo(
        string Subject,
        string Email,
        string Name,
        bool EmailVerified,
        string? PictureUrl
    );

    public interface IGoogleAuthService
    {
        Task<GoogleUserInfo> ValidateIdTokenAsync(string idToken, string? audience = null, CancellationToken cancellationToken = default);
    }
}

