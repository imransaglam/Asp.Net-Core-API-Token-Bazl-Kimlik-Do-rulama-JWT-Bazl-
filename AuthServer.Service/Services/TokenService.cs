using AuthServer.Core.Configuration;
using AuthServer.Core.DTOs;
using AuthServer.Core.Models;
using AuthServer.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace AuthServer.Service.Services
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly CustomTokenOption _tokenOption;
        public TokenService(UserManager<UserApp> userManager, IOptions<CustomTokenOption> options)
        {
            _userManager = userManager;
            _tokenOption = options.Value;
        }

        //Refresh Token oluşturacak metot
        private string CreateRefreshToken()
        {
            var numberByte = new byte[32];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(numberByte);
            return Convert.ToBase64String(numberByte);
        }

        //Üyelik sistemi gerektiren bir token oluşturulduğunda paylodu doldurmak istediğimizde bu claim kullanıcaz
        private IEnumerable<Claim> GetClaims(UserApp userApp, List<string> audiences)
        {
            //tokenın payloadında olmasını istediğimiz tüm dataları claim olarak eklendi
            var userList = new List<Claim>()
            {
                 new Claim(ClaimTypes.NameIdentifier,userApp.Id),
                 new Claim(JwtRegisteredClaimNames.Email,userApp.Email),
                 new Claim(ClaimTypes.Name,userApp.UserName),
                 new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),

            };

            userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            return userList;


        }

        //Üyelik sistemi gerektirmeyen bir token oluşturmka istediğimizde claimler bu metotda oluşsun
        private IEnumerable<Claim> GetClaimsByClient(Client client)
        {
            var claims = new List<Claim>();
            claims.AddRange(client.Audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString());//Random guid oluşturma
            new Claim(JwtRegisteredClaimNames.Sub, client.Id.ToString());//token kim için oluşturuyor
            return claims;
        }


        public TokenDto CreateToken(UserApp userApp)
        {
            //token ömrü
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
            var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);
            //tokenı imzalayacak key
            var securityKey = SignService.GetSymetricSecurityKey(_tokenOption.SecurityKey);
            //security key ile imza oluşturma algoritma ile
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                //tokenı yayınlayan kim?
                issuer: _tokenOption.Issuer,
                //Bu token ne zaman biticek?
                expires: accessTokenExpiration,
                //Verilen dakika öncesinde geçersiz olmasın
                notBefore: DateTime.Now,
                claims: GetClaims(userApp, _tokenOption.Audience),
                signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new TokenDto
            {
                AccessToken = token,
                RefreshToken = CreateRefreshToken(),
                AccessTokenExpiration = accessTokenExpiration,
                RefreshTokenExpiration = refreshTokenExpiration,
            };
            return tokenDto;

        }

        public ClientTokenDto CreateTokenByClient(Client client)
        {
            //token ömrü
            var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);

            //tokenı imzalayacak key
            var securityKey = SignService.GetSymetricSecurityKey(_tokenOption.SecurityKey);
            //security key ile imza oluşturma algoritma ile
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                //tokenı yayınlayan kim?
                issuer: _tokenOption.Issuer,
                //Bu token ne zaman biticek?
                expires: accessTokenExpiration,
                //Verilen dakika öncesinde geçersiz olmasın
                notBefore: DateTime.Now,
                claims: GetClaimsByClient(client),
                signingCredentials: signingCredentials);

            var handler = new JwtSecurityTokenHandler();

            var token = handler.WriteToken(jwtSecurityToken);

            var tokenDto = new ClientTokenDto
            {
                AccessToken = token,

                AccessTokenExpiration = accessTokenExpiration,

            };
            return tokenDto;
        }
    }
}
