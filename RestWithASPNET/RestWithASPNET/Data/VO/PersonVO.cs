using System.Collections.Generic;
using RestWithASPNET.Hypermedia;
using RestWithASPNET.Hypermedia.Abstract;

namespace RestWithASPNET.Data.VO{ //value object

    public class PersonVO : isupporthypermedia{
        public long Id {get; set;}
        public string Address { get; set; }
        public string Fname { get; set; }
        public string Gender { get; set; }
        public string Lname { get; set; }
        public bool Enabled { get; set; }
        public List<hypermedialink> Links { get; set ;} = new List<hypermedialink>();
    }
}
