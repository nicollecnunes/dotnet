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

        public TokenVO ValidateCredentials(UserVO userCredentials)
        {
            var user = _repo.validateCredentials(userCredentials);
            if(user == null)
            {
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
    }
}