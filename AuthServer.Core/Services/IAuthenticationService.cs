using AuthServer.Core.DTOs;
using SharedLibrary.Dtos;

namespace AuthServer.Core.Services
{
    public interface IAuthenticationService
    {
        //kullanıcı bilgilerinin doğruluğuna göre token üretme işlemi var.
        Task<Response<TokenDto>> CreateTokenAsync(LoginDto loginDto);
        //Refresh Token ile birlikte yeni bir token almak
        Task<Response<TokenDto>> CreateTokenByRefreshToken(string refreshToken);
        //Refresh Token'ı sonlandırma
        Task<Response<NoDataDto>> RevokeRefreshToken(string refreshToken);

        //Client ile birlikte üyelik sistemi olmadan token almak
        Task<Response<ClientTokenDto>> CreateTokenByClient(ClientLoginDto clientLoginDto);
    }
}
