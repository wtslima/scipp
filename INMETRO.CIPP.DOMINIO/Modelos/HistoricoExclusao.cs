using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INMETRO.CIPP.DOMINIO.Modelos
{
    [Table("TB_HISTORICO_EXCLUSAO")]
    public class HistoricoExclusao
    {
        [Key]
        [Column("IDT_HISTORICO_EXCLUSAO")]
        public int Id { get; set; }

        [Required]
        [Column("CDA_CIPP")]
        [StringLength(20)]
        public string Cipp { get; set; }

        [Required]
        [Column("CDA_CODIGO_OIA")]
        public string CodigoOia { get; set; }

        [Required]
        [Column("DAT_DOWNLOAD")]
        public DateTime DataExclusao { get; set; }

    }
}