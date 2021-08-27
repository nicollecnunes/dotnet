using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNET.Model;
using RestWithASPNET.Business;
using RestWithASPNET.Hypermedia.Filters;
using RestWithASPNET.Data.VO;

namespace RestWithASPNET.Controllers
{
    [ApiVersion("1")]
    [ApiController]
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
        [TypeFilter(typeof(hypermediafilter))]
        public IActionResult get(){
            return Ok(_bookbusiness.findall());
        }
        
        [HttpGet("{id}")] //verbos get nao ambiguos
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
        public IActionResult delete(long id)
        {
            _bookbusiness.delete(id);
           return NoContent();
           

        }



    }
}
