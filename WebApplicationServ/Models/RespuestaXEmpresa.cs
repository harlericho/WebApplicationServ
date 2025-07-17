using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationServ.Models
{
    [Table("respuesta_x_empresa")]
    public class RespuestaXEmpresa
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("diseno")]
        public int Diseno { get; set; }
        [Column("hora_1")]
        public DateTime Hora1 { get; set; }
        [Column("hora_2")]
        public DateTime Hora2 { get; set; }
        [Column("hora_3")]
        public DateTime Hora3 { get; set; }
        [Column("hora_4")]
        public DateTime Hora4 { get; set; }
        [Column("fecha_respuesta")]
        public DateTime FechaRespuesta { get; set; }
    }
}
