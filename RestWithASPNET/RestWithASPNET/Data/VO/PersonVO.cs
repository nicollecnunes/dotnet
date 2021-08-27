using System.Collections.Generic;
using RestWithASPNET.Hypermedia;
using RestWithASPNET.Hypermedia.Abstract;

namespace RestWithASPNET.Data.VO{ //value object

    public class PersonVO : isupporthypermedia{
        public long id {get; set;}
        public string address { get; set; }
        public string fname { get; set; }
        public string gender { get; set; }
        public string lname { get; set; }
        public List<hypermedialink> Links { get; set ;} = new List<hypermedialink>();
    }
}
