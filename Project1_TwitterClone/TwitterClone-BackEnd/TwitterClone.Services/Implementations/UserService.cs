using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TwitterClone.DataAccess.Interfaces;
using TwitterClone.Domain.Entities;
using TwitterClone.Dtos.Users;
using TwitterClone.DTOs.Users;
using TwitterClone.Helpers;
using TwitterClone.Services.Interfaces;
using TwitterClone.Shared.CustomExceptions;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepo;
    private readonly AppSettings _appSettings;

    public UserService(IUserRepository userRepo, IOptions<AppSettings> appSettings)
    {
        _userRepo = userRepo;
        _appSettings = appSettings.Value;
    }

    public void RegisterUser(RegisterUserDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
            throw new UserDataException("Username and password are required.");

        if (_userRepo.GetByUsername(dto.Username) != null)
            throw new UserDataException("Username already exists.");

        var user = new User
        {
            Username = dto.Username,
            PasswordHash = PasswordHelper.HashPassword(dto.Password)
        };

        _userRepo.Add(user);
    }

    public string LoginUser(LoginUserDto dto)
    {
        var user = _userRepo.GetByUsername(dto.Username);

        if (user == null || !PasswordHelper.VerifyPassword(dto.Password, user.PasswordHash))
            throw new UserDataException("Invalid username or password.");


        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            }),
            Expires = DateTime.UtcNow.AddHours(2), 
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public UserDto GetUserById(int id)
    {
        var user = _userRepo.GetById(id);
        if (user == null)
            throw new UserNotFoundException(id);

        return new UserDto
        {
            Id = user.Id,
            Username = user.Username
        };
    }
}

