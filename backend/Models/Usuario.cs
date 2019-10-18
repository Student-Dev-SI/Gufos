using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models
{
    [Table("usuario")]
    public partial class Usuario
    {
        public Usuario()
        {
            Presenca = new HashSet<Presenca>();
        }

        [Key]
        [Column("IDUsuario")]
        public int Idusuario { get; set; }
        [Required]
        [StringLength(255)]
        public string Nome { get; set; }
        [Required]
        [StringLength(255)]
        public string Email { get; set; }
        [Required]
        [StringLength(255)]
        public string Senha { get; set; }
        [Column("IDTipoUsuario")]
        public int? IdtipoUsuario { get; set; }

        [ForeignKey(nameof(IdtipoUsuario))]
        [InverseProperty(nameof(TipoUsuario.Usuario))]
        public virtual TipoUsuario IdtipoUsuarioNavigation { get; set; }
        [InverseProperty("IdusuarioNavigation")]
        public virtual ICollection<Presenca> Presenca { get; set; }
    }
}
