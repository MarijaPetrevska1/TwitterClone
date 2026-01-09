using TwitterClone.Domain.Entities;
using TwitterClone.Dtos.Users;
using TwitterClone.DTOs.Users;

namespace TwitterClone.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToUserDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                PostCount = user.Posts?.Count ?? 0
            };
        }
    }
}

