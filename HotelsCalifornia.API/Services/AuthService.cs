namespace HotelsCalifornia.Services;
using HotelsCalifornia.Data;
using HotelsCalifornia.DTOs;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

public interface IAuthService
{
    Task<TokenResponseDTO> Login(LoginRequestDTO request);
}

public class AuthService(IUserRepository repo, IConfiguration config) : IAuthService
{
    private readonly IUserRepository _repo = repo;
    private readonly IConfiguration _config = config;

    public async Task<TokenResponseDTO> Login(LoginRequestDTO request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) ||
            string.IsNullOrWhiteSpace(request.Password))
            throw new UnauthorizedAccessException("Username and password are required");

        BuildTokenDTO userInfo = await _repo.GetUserByUsernameAsync(request.Username);
        if (userInfo.PasswordHash != request.Password)
            throw new UnauthorizedAccessException("Invalid username or password");
        return BuildToken(userInfo);
    }

    private TokenResponseDTO BuildToken(BuildTokenDTO user)
    {
        List<Claim> claims =
        [
            new Claim(ClaimTypes.NameIdentifier, user.NameIdentifier),
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.Role, user.Role)
        ];

        if (user.HotelId is not null)
            claims.Add(new Claim("hotelId", user.HotelId.Value.ToString()));
        
        string jwtKey = _config["Jwt:Key"]
            ?? throw new InvalidOperationException("Jwt:Key missing from config");
        string jwtIssuer = _config["Jwt:Issuer"]!;
        string jwtAudience = _config["Jwt:Audience"]!;

        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(jwtKey));
        SigningCredentials creds = new(key, SecurityAlgorithms.HmacSha256);

        DateTime expires = DateTime.UtcNow.AddHours(1);
        JwtSecurityToken token = new(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims.ToArray(),
            expires: expires,
            signingCredentials: creds
        );

        string serialized = new JwtSecurityTokenHandler().WriteToken(token);
        return new TokenResponseDTO
        {
            Token = serialized,
            ExpiresAt = expires
        };
    }
}
