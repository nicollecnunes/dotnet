using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestWithASPNET.Business;
using RestWithASPNET.Hypermedia.Filters;
using Microsoft.AspNetCore.Authorization;

namespace RestWithASPNET.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Authorize("Bearer")]
    [Route("api/[controller]/v{version:apiVersion}")] //aspnet core descobre, atraves da rota,
                                                      //a qual controller a 
                                                      //requisição pertence
    public class SheetController : ControllerBase{
    
        private readonly ILogger<SheetController> _logger;
        private isheetbusiness _sheetbusiness;


        public SheetController(ILogger<SheetController> logger, isheetbusiness sheetbusiness){
            _sheetbusiness = sheetbusiness;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(hypermediafilter))]
        public IActionResult get(){
            _sheetbusiness.findall();
            return Ok();
        }
        
        [HttpGet("{id}")] //verbos get nao ambiguos
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [TypeFilter(typeof(hypermediafilter))]
        public IActionResult get(long row){
            //System.Console.WriteLine("Iniciando Busca na linha ", row);
            //_sheetbusiness.findbyrow(row);
            return Ok("chegou aqui com o valor");  
        }

    }
}
