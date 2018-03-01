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

        [Column("CDA_ATIVO")]
        public bool EhAtivo { get; set; }

        public virtual FTPInfo FtpInfo { get; set; }
       
        public virtual IEnumerable<Inspecao> Inspecaoes { get; set; }

        [NotMapped]
        public ExcecaoDominio ExcecaoDominio { get; set; }

        public Organismo()
        {
            
        }
        public Organismo(int id, string nome,string codigo)
        {
            Id = id;
            Nome = nome;
            CodigoOIA = codigo;
        }

        public Organismo(string codigo, FTPInfo ftpInfo)
        {
            CodigoOIA = codigo;
            FtpInfo = ftpInfo;
        }


      
    }
}