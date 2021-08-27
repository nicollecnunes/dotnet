using System.Text;

namespace RestWithASPNET.Hypermedia{
    public class hypermedialink{
        public string rel{get; set;} 
        private string href; 
        public string Href{
            get{
                object _lock = new object();
                lock(_lock){
                    StringBuilder sb = new StringBuilder(href);
                    return sb.Replace("%2F", "/").ToString(); //o c# muda / pra %2f na url
                }
            }
            set{
                href = value;
            }
        } 
        public string type{get; set;} 
        public string action{get; set;} 
        
    }
}