using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNET.Model{
    
    [Table("users")]
    public class User{
        
        [Key]
        [Column("id")]
        public long id {get; set;}


        [Column("user_name")]
        public string username{get; set;}

        [Column("full_name")]
        public string fullname{get; set;}

        [Column("password")]
        public string password{get; set;}

        [Column("refresh_token")]
        public string refreshtoken{get; set;}

        [Column("refresh_token_expiry_time")]
        public DateTime refreshtokenexpirytime{get; set;}
    }
}