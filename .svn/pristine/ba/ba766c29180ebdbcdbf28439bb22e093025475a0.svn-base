using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INMETRO.CIPP.DOMINIO.Modelos
{
    [Table("TB_USUARIO")]
    public class Usuario
    {
        [Key]
        [Required]
        [Column("IDT_USUARIO")]
        public int Id { get; set; }

        [Required]
        [Column("CDA_USUARIO")]
        public string UsuarioDiois { get; set; }
      
        [Column("CDA_ATIVO")]
        public bool Status { get; set; }

    }
}