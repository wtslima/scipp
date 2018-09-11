using FluentValidator;
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

       
        [Column("NOM_ORGANISMO")]
        public string Nome { get; set; }

        
        [Column("CDA_CODIGO_OIA_PP")]
        public string CodigoOIA { get; set; }

        [Column("CDA_ATIVO")]
        public bool EhAtivo { get; set; }

        public virtual IntegracaoInfos IntegracaoInfo { get; set; }
       
        public virtual IEnumerable<Inspecao> Inspecaoes { get; set; }

        [NotMapped]
        public ExcecaoDominio ExcecaoDominio { get; set; }

        public Organismo()
        {
            
        }

        public Organismo(int id, string nome, string codigoOia, IntegracaoInfos integracaoInfo)
        {
            Nome = nome;
            CodigoOIA = codigoOia;
            IntegracaoInfo  = new IntegracaoInfos
            {
                HostURI = integracaoInfo.HostURI+":"+integracaoInfo.Porta+"//"+integracaoInfo.DiretorioInspecao,
                DiretorioInspecaoLocal = integracaoInfo.DiretorioInspecaoLocal,
                TipoIntegracao = integracaoInfo.TipoIntegracao,
                Usuario = integracaoInfo.Usuario,
                Senha = integracaoInfo.Senha,
                PrivateKey = integracaoInfo.PrivateKey

            };
        }


    }
}