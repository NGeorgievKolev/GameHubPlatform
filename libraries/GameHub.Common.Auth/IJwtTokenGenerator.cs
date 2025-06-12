namespace GameHub.Common.Auth;

public interface IJwtTokenGenerator
{
    string GenerateToken(int userId, string username, string email, string? role = null);
}
