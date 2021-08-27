using System.Collections.Generic;
using RestWithASPNET.Hypermedia.Abstract;

namespace RestWithASPNET.Hypermedia.Filters{
    public class hypermediafilteroptions{
        public List<iresponseenricher> contentresponseenricherlist {get; set;} = new List<iresponseenricher>();
    }
    
}