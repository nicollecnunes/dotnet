using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using RestWithASPNET.Model.Base;

namespace RestWithASPNET.Model
{

    //mapeando entidades com [TABLE] E [COLUMN]
    [Table("books")]
    public class Book : BaseEntity{ //estende a entidade base. remove id pq vai herdar

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
