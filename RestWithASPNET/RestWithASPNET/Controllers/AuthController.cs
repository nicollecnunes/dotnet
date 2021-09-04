using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNET.Model;
using RestWithASPNET.Business;
using RestWithASPNET.Hypermedia.Filters;
using RestWithASPNET.Data.VO;
using System.Collections.Generic;

namespace RestWithASPNET.Controllers
{
    [ApiVersion("1")]
    [Route("api/[controller]/v{version:apiVersion}")]
    [ApiController]

    public class AuthController : ControllerBase
    {
        private iloginbusiness _loginbusiness;

        public AuthController(iloginbusiness loginbusiness)
        {
            _loginbusiness = loginbusiness;
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult signin([FromBody] UserVO user)
        {
            if(user == null)
            {
                return BadRequest("Invalid Client Request");
            }
            var token = _loginbusiness.ValidateCredentials(user);
            if(token == null){
                return Unauthorized();
            }else{
               return Ok(token); 
            }
        }
    }

}