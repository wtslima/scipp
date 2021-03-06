﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INMETRO.CIPP.DOMINIO.Modelos
{
    [Table("TB_HISTORICO_DOWNLOAD")]
    public class Historico
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IDT_HISTORICO_DOWNLOAD")]
        public int Id { get; set; }

        [Required]
        [Column("IDT_INSPECAO")]
        public int InspecaoId { get; set; }

        [Required]
        [Column("NOM_USUARIO")]
        public string Usuario { get; set; }

        [Required]
        [Column("DAT_DOWNLOAD")]
        public DateTime DataDownload { get; set; }

        public virtual Inspecao Inspecao {get;set;}
    }
}