using System;
using System.Collections.Generic;
using RestWithASPNET.Hypermedia;
using RestWithASPNET.Hypermedia.Abstract;

namespace RestWithASPNET.Data.VO
{
    public class BookVO : isupporthypermedia{
        public long id {get ; set;}
        public string author { get; set; }
        public DateTime launch_date { get; set; }
        public int price { get; set; }
        public string title { get; set; }
        public List<hypermedialink> Links { get; set ;} = new List<hypermedialink>();

    }
}
