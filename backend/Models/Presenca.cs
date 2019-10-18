using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("presenca")]
    public partial class Presenca
    {
        [Key]
        [Column("IDPresenca")]
        public int Idpresenca { get; set; }
        [Required]
        [StringLength(255)]
        public string PresencaStatus { get; set; }
        [Column("IDUsuario")]
        public int? Idusuario { get; set; }
        [Column("IDEvento")]
        public int? Idevento { get; set; }

        [ForeignKey(nameof(Idevento))]
        [InverseProperty(nameof(Evento.Presenca))]
        public virtual Evento IdeventoNavigation { get; set; }
        [ForeignKey(nameof(Idusuario))]
        [InverseProperty(nameof(Usuario.Presenca))]
        public virtual Usuario IdusuarioNavigation { get; set; }
    }
}
