using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET.Data.VO;
using RestWithASPNET.Hypermedia.Constants;

namespace RestWithASPNET.Hypermedia.Enricher{
    public class personEnricher : contentresponseenricher<PersonVO>
    {
        private readonly object _lock = new object();
        protected override Task enrichmodel(PersonVO content, IUrlHelper urlHelper)
        {
            var path = "api/person/v1";
            string link = getLink(content.Id, urlHelper, path);
            content.Links.Add(new hypermedialink(){
                action = httpactionverb.GET,
                Href = link,
                rel = relationtype.self,
                type = responsetypeformat.DefaultGet

            });

            content.Links.Add(new hypermedialink(){
                action = httpactionverb.POST,
                Href = link,
                rel = relationtype.self,
                type = responsetypeformat.DefaultPost

            });

            content.Links.Add(new hypermedialink(){
                action = httpactionverb.PUT,
                Href = link,
                rel = relationtype.self,
                type = responsetypeformat.DefaultPut

            });

            content.Links.Add(new hypermedialink()
            {
                action = httpactionverb.PATCH,
                Href = link,
                rel = relationtype.self,
                type = responsetypeformat.DefaultPatch

            });

            content.Links.Add(new hypermedialink(){
                action = httpactionverb.DELETE,
                Href = link,
                rel = relationtype.self,
                type = "int" //so devolve status code

            });

            return null;
        }

        private string getLink(long id, IUrlHelper urlHelper, string path)
        {
            lock(_lock){
                var url = new {controller = path, id = id};
                return new StringBuilder(urlHelper.Link("DefaultApi", url)).Replace("%2F", "/").ToString();
            }
        }
    }
}