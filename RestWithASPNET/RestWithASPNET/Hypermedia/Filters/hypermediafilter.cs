using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RestWithASPNET.Hypermedia.Abstract;

namespace RestWithASPNET.Hypermedia.Filters{
    public class hypermediafilter : ResultFilterAttribute{ //estende
        private readonly hypermediafilteroptions _hypermediafilteroptions;

        public hypermediafilter(hypermediafilteroptions hypermediafilteroptions){
            _hypermediafilteroptions = hypermediafilteroptions;

        }

        public override void OnResultExecuting(ResultExecutingContext context)
        {
            tryenrichresult(context);
            base.OnResultExecuting(context);
        }

        public void tryenrichresult(ResultExecutingContext context){
            if(context.Result is OkObjectResult objectResult){
                var enricher = _hypermediafilteroptions
                    .contentresponseenricherlist
                    .FirstOrDefault(x => x.CanEnrich(context));

                if(enricher != null){
                    Task.FromResult(enricher.enrich(context));
                }
            }
        }



    }
}