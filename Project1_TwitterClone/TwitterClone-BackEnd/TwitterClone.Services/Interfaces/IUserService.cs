using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitterClone.Dtos.Users;
using TwitterClone.DTOs.Users;

namespace TwitterClone.Services.Interfaces
{
    public interface IUserService
    {
        void RegisterUser(RegisterUserDto dto);
        string LoginUser(LoginUserDto dto);
        UserDto GetUserById(int id);
    }
}
