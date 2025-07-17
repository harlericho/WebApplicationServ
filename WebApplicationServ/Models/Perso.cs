using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationServ.Models
{
    [Table("perso")]
    public class Perso
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("bin_id")]
        public int BinId { get; set; }
        [Column("cliente")]
        public string Cliente { get; set; } = null!;
        [Column("fecha")]
        public DateTime Fecha { get; set; }
    }
}
