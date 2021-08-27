using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RestWithASPNET.Hypermedia.Abstract{
    public interface iresponseenricher{
        bool CanEnrich(ResultExecutingContext context);
        
        Task enrich(ResultExecutingContext context);

    }
}