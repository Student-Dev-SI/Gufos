using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("localizacao")]
    public partial class Localizacao
    {
        public Localizacao()
        {
            Evento = new HashSet<Evento>();
        }

        [Key]
        [Column("IDLocalizacao")]
        public int Idlocalizacao { get; set; }
        [Required]
        [Column("CNPJ")]
        [StringLength(14)]
        public string Cnpj { get; set; }
        [Required]
        [StringLength(255)]
        public string RazãoSocial { get; set; }
        [Required]
        [StringLength(255)]
        public string Endereco { get; set; }

        [InverseProperty("IdlocalizacaoNavigation")]
        public virtual ICollection<Evento> Evento { get; set; }
    }
}
