using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Infrastructure;
using Infrastructure.DTOs;

namespace Service
{
    public interface IAuthService
    {
        IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
        IDataResult<User> Login(UserForLoginDto userForLoginDto);
        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);

    }
}