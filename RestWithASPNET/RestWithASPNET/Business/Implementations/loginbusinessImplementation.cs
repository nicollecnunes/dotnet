using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using RestWithASPNET.Configurations;
using RestWithASPNET.Data.VO;
using RestWithASPNET.Repository;
using RestWithASPNET.Services;

namespace RestWithASPNET.Business.Implementations{
    public class loginbusinessImplementation : iloginbusiness
    {
        private const string DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        private TokenConfiguration _config;
        private IUserRepository _repo;
        private readonly ITokenService _tkService;

        public loginbusinessImplementation(TokenConfiguration config, IUserRepository repo, ITokenService tkService)
        {
            _config = config;
            _repo = repo;
            _tkService = tkService;
        }

        public TokenVO validateCredentials(UserVO userCredentials)
        {
            //System.Console.WriteLine("linha 27 - login business implementation");
            var user = _repo.validateCredentials(userCredentials);
            
            if(user is null)
            {
                //System.Console.WriteLine("linha 31 - login business implementation: user nulo");
                return null;
            }
           
           var claims = new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                new Claim(JwtRegisteredClaimNames.UniqueName,user.username)
            };
            var accessToken = _tkService.generateaccesstk(claims);
            var refreshToken = _tkService.generaterefreshtk(); //quando o acesstoken estiver expirado

            user.refreshtoken = refreshToken;
            user.refreshtokenexpirytime = DateTime.Now.AddDays(_config.daystoExpire); //pega do appsetings
            _repo.RefreshUserInfo(user);
            
            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_config.minutes);

            

            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
            );
        }

        public TokenVO validateCredentials(TokenVO token){
            var accessToken =  token.AccessToken;
            var refreshToken = token.RefreshToken;

            var principal = _tkService.getprincipalfromexpiredtk(accessToken);
            var username = principal.Identity.Name;

            var user = _repo.validateCredentials(username);

            if (user == null ||
                user.refreshtoken != refreshToken ||
                user.refreshtokenexpirytime <= DateTime.Now)
            {
                    return null;
            }

            accessToken = _tkService.generateaccesstk(principal.Claims);
            refreshToken = _tkService.generaterefreshtk();

            user.refreshtoken = refreshToken;

            _repo.RefreshUserInfo(user);

            DateTime createDate = DateTime.Now;
            DateTime expirationDate = createDate.AddMinutes(_config.minutes);

            return new TokenVO(
                true,
                createDate.ToString(DATE_FORMAT),
                expirationDate.ToString(DATE_FORMAT),
                accessToken,
                refreshToken
            );

        }

        public bool RevokeToken(string username)
        {
            return _repo.RevokeToken(username);
        }
    }
}