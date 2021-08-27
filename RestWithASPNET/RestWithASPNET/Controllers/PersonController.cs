using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNET.Model;
using RestWithASPNET.Business;
using RestWithASPNET.Data.VO;
using RestWithASPNET.Hypermedia.Filters;
using System.Collections.Generic;

namespace RestWithASPNET.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")] //aspnet core descobre, atraves da rota,
                                                      //a qual controller a 
                                                      //requisição pertence
    public class PersonController : ControllerBase{
        

        private readonly ILogger<PersonController> _logger;
        private ipersonbusiness _personbusiness;


        public PersonController(ILogger<PersonController> logger, ipersonbusiness personbusiness){
            _personbusiness = personbusiness;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<PersonVO>))] //padrao ok
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(hypermediafilter))]
        public IActionResult get(){
            return Ok(_personbusiness.findall());
        }
        
        [HttpGet("{id}")] //verbos get nao ambiguos
        [ProducesResponseType((200), Type = typeof(PersonVO))] //padrao ok
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(hypermediafilter))]
        public IActionResult get(long id){
            var person = _personbusiness.findbyid(id);
            if(person == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(person);
            }
            
        }
        
        [HttpPost] 
        [ProducesResponseType((200), Type = typeof(PersonVO))] //padrao ok
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(hypermediafilter))]
        public IActionResult post([FromBody] PersonVO person){
            if(person == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(_personbusiness.create(person));
            }
            
        }
        
        [HttpPut] 
        [ProducesResponseType((200), Type = typeof(PersonVO))] //padrao ok
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(hypermediafilter))]
        public IActionResult put([FromBody] PersonVO person){
            if(person == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(_personbusiness.update(person));
            }
            
        }

        [HttpDelete("{id}")] //verbos get nao ambiguos
        [ProducesResponseType(204)] //padrao ok
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult delete(long id)
        {
            _personbusiness.delete(id);
           return NoContent();
           

        }



    }
}
