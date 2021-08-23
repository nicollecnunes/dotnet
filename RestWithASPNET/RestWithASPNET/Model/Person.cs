using System.ComponentModel.DataAnnotations.Schema;
using RestWithASPNET.Model.Base;

namespace RestWithASPNET.Model
{

    //mapeando entidades com [TABLE] E [COLUMN]
    [Table("person")]
    public class Person : BaseEntity{

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
