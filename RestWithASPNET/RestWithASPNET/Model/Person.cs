using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RestWithASPNET.Model
{

    //mapeando entidades com [TABLE] E [COLUMN]
    [Table("person")]
    public class Person
    {
        [Column("id")]
        public long id { get; set; }

        [Column("address")]
        public string address { get; set; }

        [Column("fname")]
        public string fname { get; set; }

        [Column("gender")] //igual escrito no BD
        public string gender { get; set; }

        [Column("lname")]
        public string lname { get; set; }

        

        
    }
}
