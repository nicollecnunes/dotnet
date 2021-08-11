using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNET.Model
{

    //mapeando entidades com [TABLE] E [COLUMN]
    [Table("books")]
    public class Book
    {
        [Column("id")]
        public long id { get; set; }

        [Column("author")]
        public string author { get; set; }

        [Column("launch_date")]
        public DateTime launch_date { get; set; }

        [Column("price")] //igual escrito no BD
        public int price { get; set; }

        [Column("title")]
        public string title { get; set; }

        

        
    }
}
