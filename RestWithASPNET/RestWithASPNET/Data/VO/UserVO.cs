using System.Collections.Generic;
using RestWithASPNET.Hypermedia;
using RestWithASPNET.Hypermedia.Abstract;

namespace RestWithASPNET.Data.VO{ //value object

    public class UserVO : isupporthypermedia{
        public string username { get; set; }
        public string password { get; set; }
        public List<hypermedialink> Links { get; set ;} = new List<hypermedialink>();
    }
}
