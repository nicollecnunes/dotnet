using System.Collections.Generic;
using System.Security.Claims;
using RestWithASPNET.Configurations;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Security.Cryptography;

namespace RestWithASPNET.Services{
    public class TokenService : ITokenService //implementa. o Ialgumacoisa é tipo o .h em c
    {
        private TokenConfiguration _config;

        public TokenService(TokenConfiguration config) //construtor
        {
            _config = config;
        }

        public string generateaccesstk(IEnumerable<Claim> claims)
        {
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.secret)); //secret definida no json
            var signinCredentials = new SigningCredentials(secretkey , SecurityAlgorithms.HmacSha256);

            var opt = new JwtSecurityToken(
                issuer: _config.issuer,
                audience: _config.audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_config.minutes),
                signingCredentials: signinCredentials  
            );
            string tokenstring = new JwtSecurityTokenHandler().WriteToken(opt);
            return tokenstring;
        }

        public string generaterefreshtk()
        {
            var random = new byte[32]; //array de 32
            using (var rng = RandomNumberGenerator.Create()){
                rng.GetBytes(random);

                return Convert.ToBase64String(random);
            };
        }

        public ClaimsPrincipal getprincipalfromexpiredtk(string token)
        {
            var tokenValidationParams = new TokenValidationParameters{
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.secret)),
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParams, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if(jwtSecurityToken == null ||
                !jwtSecurityToken.Header.Alg.Equals(
                    SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCulture
                ))
            {
                throw new SecurityTokenException("Invalid Token");
            }

            return principal;
        }
    }
}