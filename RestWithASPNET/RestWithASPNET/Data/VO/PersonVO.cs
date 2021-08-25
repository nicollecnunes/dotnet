//using System.Text.Json.Serialization;

namespace RestWithASPNET.Data.VO{ //value object

    public class PersonVO{
        //[JsonPropertyName("FrontNameIdHere")]
        //[JsonIgnore] não serializa
        public long id {get; set;}
        public string address { get; set; }
        public string fname { get; set; }
        public string gender { get; set; }
        public string lname { get; set; }

    }
}
