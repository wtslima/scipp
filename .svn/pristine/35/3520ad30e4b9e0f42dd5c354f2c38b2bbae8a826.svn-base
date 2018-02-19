using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INMETRO.CIPP.DOMINIO.Modelos
{
    [Table("TB_INSPECAO_CIPP")]
    public class Inspecao
    {
        [Key]
        [Column("IDT_INSPECAO")]
        public int Id { get; set; }

       
        [StringLength(20)]
        [Required]
        [Column("CDN_CIPP")]
        public string CodigoCIPP { get; set; }
        
        [Required]
        [Column("DES_PLACA_LICENCA")]
        public string PlacaLicenca { get; set; }

        [Required]
        [Column("NUM_EQUIPAMENTO")]
        public int NumeroEquipamento { get; set; }

        [Required]
        [Column("DAT_INSPECAO")]
        public DateTime DataInspecao { get; set; }

        [Column("NOM_RESPONSAVEL_TECNICO")]
        public string ResponsavelTecnico { get; set; }
        [Required]
        [Column("CDA_CODIGO_OIA")]
        public string CodigoOIA { get; set; }

        public virtual Historico Historico { get; set; }

       public Inspecao(string codigoCipp, string placa, int equipamento, DateTime dataInspecao, string responsavel, string codigoOIA)
        {
            CodigoCIPP = codigoCipp;
            PlacaLicenca = placa;
            NumeroEquipamento = equipamento;
            DataInspecao = dataInspecao;
            ResponsavelTecnico = responsavel;
            CodigoOIA = codigoOIA;
        }

        public Inspecao()
        {


        }
    }
}