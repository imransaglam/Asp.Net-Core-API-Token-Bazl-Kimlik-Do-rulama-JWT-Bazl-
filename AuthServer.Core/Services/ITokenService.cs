using AuthServer.Core.Configuration;
using AuthServer.Core.DTOs;
using AuthServer.Core.Models;

namespace AuthServer.Core.Services
{
    public interface ITokenService
    {
        TokenDto CreateToken(UserApp userApp);// Sadece token üretmek ile ilgili business var.
        ClientTokenDto CreateTokenByClient(Client client);
    }
}
