using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INMETRO.CIPP.DOMINIO.Modelos
{
    [Table("TB_HISTORICO_DOWNLOAD_INSPECAO")]
    public class Historico
    {
        [Required]
        [Column("IDT_INSPECAO")]
        public int InspecaoId { get; set; }

        [Required]
        [Column("NOM_RESPONSAVEL")]
        public string Usuario { get; set; }

        [Required]
        [Column("DAT_DOWNLOAD")]
        public DateTime DataDownload { get; set; }

        public virtual Inspecao Inspecao {get;set;}
    }
}