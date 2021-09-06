using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNET.Model;
using RestWithASPNET.Business;
using RestWithASPNET.Hypermedia.Filters;
using RestWithASPNET.Data.VO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace RestWithASPNET.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")] //aspnet core descobre, atraves da rota,
                                                      //a qual controller a 
                                                      //requisição pertence
    public class BookController : ControllerBase{
        

        private readonly ILogger<BookController> _logger;
        private ibookbusiness _bookbusiness;


        public BookController(ILogger<BookController> logger, ibookbusiness bookbusiness){
            _bookbusiness = bookbusiness;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType((200), Type = typeof(List<BookVO>))] //padrao ok
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(hypermediafilter))]
        public IActionResult get(){
            return Ok(_bookbusiness.findall());
        }
        
        [HttpGet("{id}")] //verbos get nao ambiguos
        [ProducesResponseType((200), Type = typeof(BookVO))] //padrao ok
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(hypermediafilter))]
        public IActionResult get(long id){
            var book = _bookbusiness.findbyid(id);
            if(book == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(book);
            }
            
        }
        
        [HttpPost] 
        [ProducesResponseType((200), Type = typeof(BookVO))] //padrao ok
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(hypermediafilter))]
        public IActionResult post([FromBody] BookVO book){
            if(book == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(_bookbusiness.create(book));
            }
            
        }
        
        [HttpPut] 
        [ProducesResponseType((200), Type = typeof(BookVO))] //padrao ok
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(hypermediafilter))]
        public IActionResult put([FromBody] BookVO book){
            if(book == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(_bookbusiness.update(book));
            }
            
        }

        [HttpDelete("{id}")] //verbos get nao ambiguos
        [ProducesResponseType(204)] //padrao ok
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult delete(long id)
        {
            _bookbusiness.delete(id);
           return NoContent();
           

        }



    }
}
