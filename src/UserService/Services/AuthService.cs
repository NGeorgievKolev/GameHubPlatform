using GameHub.Common.Auth;
using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.DTOs;
using UserService.Models;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly IJwtTokenGenerator _jwt;

    public AuthService(AppDbContext context, IJwtTokenGenerator jwt)
    {
        _context = context;
        _jwt = jwt;
    }

    public async Task<bool> RegisterAsync(RegisterDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email)) return false;

        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<string?> LoginAsync(LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return null;

        return _jwt.GenerateToken(user.Id, user.Username, user.Email);
    }
}
