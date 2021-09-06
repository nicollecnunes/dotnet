using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Business;
using RestWithASPNET.Data.VO;
using Microsoft.AspNetCore.Authorization;

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
            //System.Console.WriteLine("username == ", user.username);
            if(user == null)
            {
                return BadRequest("Invalid Client Request");
            }
            //System.Console.WriteLine("linha 30 - authcontroller");
            var token = _loginbusiness.validateCredentials(user);
            
            if(token == null){
                //System.Console.WriteLine("linha 32 - authcontroller: token nulo");
                return Unauthorized();
            }
            return Ok(token); 
            
        }

        [HttpGet]
        [Route("refresh")]
        public IActionResult refresh([FromBody] TokenVO tokenvo)
        {
            if (tokenvo == null)
            {
                return BadRequest("Invalid client request");
            }

            var token = _loginbusiness.validateCredentials(tokenvo);
            if(token == null)
            {
                return BadRequest("Invalid client request");
            }
            return Ok(token);
        }

        [HttpGet]
        [Route ("revoke")]
        [Authorize("Bearer")] //pra identificar qual token sera revogado
        public IActionResult revoke(){
            var username = User.Identity.Name; //pega a PK que nao e o id
            var result = _loginbusiness.RevokeToken(username); //true or false

            if (!result){
                return BadRequest("INVALID CLIENT REQUEST"); 
            }
            return NoContent();
        }
    }

}