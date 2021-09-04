using System.Collections.Generic;
using System.Security.Claims;

namespace RestWithASPNET.Services{
    public interface ITokenService{
        string generateaccesstk(IEnumerable<Claim> claims);
        string generaterefreshtk();
        ClaimsPrincipal getprincipalfromexpiredtk(string token);
    }
}