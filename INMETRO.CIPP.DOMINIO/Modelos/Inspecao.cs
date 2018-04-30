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
        public string CodigoCipp { get; set; }
        
       
        [Column("DES_PLACA_LICENCA")]
        public string PlacaLicenca { get; set; }

        
        [Column("NUM_EQUIPAMENTO")]
        public string NumeroEquipamento { get; set; }

        [Required]
        [Column("DAT_INSPECAO")]
        [DataType(DataType.Date)]
        public DateTime DataInspecao { get; set; }
       
        [Required]
        [Column("CDA_CODIGO_OIA")]
        public string CodigoOIA { get; set; }

        public virtual Historico Historico { get; set; }

        [NotMapped]
        public virtual ExcecaoDominio ExcecaoDominio { get; set; }

        public Inspecao(string codigoCipp, string placa, string equipamento, DateTime dataInspecao,  string codigoOIA)
        {
            CodigoCipp = codigoCipp;
            PlacaLicenca = placa;
            NumeroEquipamento = equipamento;
            DataInspecao = dataInspecao;
            CodigoOIA = codigoOIA;
        }

        public Inspecao()
        {


        }
    }
}