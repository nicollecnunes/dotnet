using System;

namespace RestWithASPNET.Model
{
    public class BookVO{
        public long id {get ; set;}
        public string author { get; set; }
        public DateTime launch_date { get; set; }
        public int price { get; set; }
        public string title { get; set; }

    }
}
