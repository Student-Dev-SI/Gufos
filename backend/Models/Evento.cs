using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("evento")]
    public partial class Evento
    {
        public Evento()
        {
            Presenca = new HashSet<Presenca>();
        }

        [Key]
        [Column("IDEvento")]
        public int Idevento { get; set; }
        [Required]
        [StringLength(255)]
        public string Titulo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime DataDoEvento { get; set; }
        [Required]
        public bool? AcessoLivre { get; set; }
        [Column("IDCategoria")]
        public int? Idcategoria { get; set; }
        [Column("IDLocalizacao")]
        public int? Idlocalizacao { get; set; }

        [ForeignKey(nameof(Idcategoria))]
        [InverseProperty(nameof(Categoria.Evento))]
        public virtual Categoria IdcategoriaNavigation { get; set; }
        [ForeignKey(nameof(Idlocalizacao))]
        [InverseProperty(nameof(Localizacao.Evento))]
        public virtual Localizacao IdlocalizacaoNavigation { get; set; }
        [InverseProperty("IdeventoNavigation")]
        public virtual ICollection<Presenca> Presenca { get; set; }
    }
}
