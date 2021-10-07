using System.ComponentModel.DataAnnotations.Schema;
using RestWithASPNET.Model.Base;

namespace RestWithASPNET.Model
{

    //mapeando entidades com [TABLE] E [COLUMN]
    [Table("person")]
    public class Person : BaseEntity{

        [Column("address")]
        public string Address { get; set; }

        [Column("fname")]
        public string Fname { get; set; }

        [Column("gender")] //igual escrito no BD
        public string Gender { get; set; }
        
        [Column("enabled")] //igual escrito no BD
        public bool Enabled { get; set; }

        [Column("lname")]
        public string Lname { get; set; }

        

        
    }
}
