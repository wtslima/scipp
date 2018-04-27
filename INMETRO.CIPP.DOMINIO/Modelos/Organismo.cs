using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace INMETRO.CIPP.DOMINIO.Modelos
{
    [Table("TB_ORGANISMO")]
    public class Organismo
    {
        [Key]
        [Column("IDT_ORGANISMO")]
        public int Id { get; set; }

        [Required]
        [Column("NOM_ORGANISMO")]
        public string Nome { get; set; }

        [Required]
        [Column("CDA_CODIGO_OIA")]
        public string CodigoOIA { get; set; }

        [Required]
        [Column("CDA_LI")]
        public string Nivel_Li { get; set; }

        [NotMapped]
        public string CodigoOiaLi { get; set; }

        [Column("CDA_ATIVO")]
        public bool EhAtivo { get; set; }

        public virtual IntegracaoInfos IntegracaoInfo { get; set; }
       
        public virtual IEnumerable<Inspecao> Inspecaoes { get; set; }

        [NotMapped]
        public ExcecaoDominio ExcecaoDominio { get; set; }

        public Organismo()
        {
            
        }

        public Organismo(string nome, int codigoOia, string nivel)
        {
            Nome = nome;
            CodigoOiaLi = codigoOia.ToString() + "-" + nivel; 
        }


    }
}