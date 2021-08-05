﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNET.Model;
using RestWithASPNET.Business;

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
        public IActionResult get(){
            return Ok(_personbusiness.findall());
        }
        
        [HttpGet("{id}")] //verbos get nao ambiguos
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
        public IActionResult post([FromBody] Person person){
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
        public IActionResult put([FromBody] Person person){
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
        public IActionResult delete(long id)
        {
            _personbusiness.delete(id);
           return NoContent();
           

        }



    }
}
