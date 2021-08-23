
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNET.Model.Base{
    public class BaseEntity{
        [Column("id")]
        public long id { get; set; }

    }
}