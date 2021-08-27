using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using RestWithASPNET.Hypermedia.Abstract;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Routing;
using System.Collections.Concurrent;

namespace RestWithASPNET.Hypermedia{
    public abstract class contentresponseenricher<T> : iresponseenricher where T : isupporthypermedia{
        // a classe implementa iresponseenricher quando T implementa isupporthypermedia
        public contentresponseenricher(){ //construtor

        }
        public virtual bool CanEnrich(Type contentType){
            //objetos simples || lista
            return contentType == typeof(T) || contentType == typeof(List<T>);
        }

        protected abstract Task enrichmodel(T content, IUrlHelper urlHelper);

        bool iresponseenricher.CanEnrich(ResultExecutingContext response){
            if(response.Result is OkObjectResult okObjectResult){
                return CanEnrich(okObjectResult.Value.GetType());
            }else{
                return false;
            }
        }
        public async Task enrich(ResultExecutingContext response){
            var urlHelper = new UrlHelperFactory().GetUrlHelper(response);
            if(response.Result is OkObjectResult okObjectResult){
                if(okObjectResult.Value is T model){ //simples
                    await enrichmodel(model, urlHelper);
                }else if(okObjectResult.Value is List<T> collection){ //lista
                    ConcurrentBag<T> bag = new ConcurrentBag<T>(collection);
                    Parallel.ForEach(bag, (element) => {
                        enrichmodel(element, urlHelper);
                    });
                }
                
            }
            await Task.FromResult<object>(null);
        }
    }


}