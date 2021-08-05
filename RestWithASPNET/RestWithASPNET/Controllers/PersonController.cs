using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNET.Model;
using RestWithASPNET.Services;

namespace RestWithASPNET.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("api/[controller]/v{version:apiVersion}")]
    public class PersonController : ControllerBase{
        

        private readonly ILogger<PersonController> _logger;
        private ipersonservice _personservice;


        public PersonController(ILogger<PersonController> logger, ipersonservice personservice){
            _personservice = personservice;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult get(){
            return Ok(_personservice.findall());
        }
        
        [HttpGet("{id}")] //verbos get nao ambiguos
        public IActionResult get(long id){
            var person = _personservice.findbyid(id);
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
        public IActionResult post([FromBody] Person person){
            if(person == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(_personservice.create(person));
            }
            
        }
        
        [HttpPut] 
        public IActionResult put([FromBody] Person person){
            if(person == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(_personservice.update(person));
            }
            
        }

        [HttpDelete("{id}")] //verbos get nao ambiguos
        public IActionResult delete(long id)
        {
            _personservice.delete(id);
           return NoContent();
           

        }



    }
}
