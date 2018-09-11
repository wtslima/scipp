
using System.ComponentModel.DataAnnotations;

namespace INMETRO.CIPP.WEB.Models
{
    public class IntegracaoInfoModel
    {
       
        public int Id { get; set; }
        [Required(ErrorMessage = "O código-OIA-PP é obrigatório.")]
        public int OrganismoId { get; set; }

        public string DiretorioInspecao { get; set; }

        public int TipoIntegracao { get; set; }
        
        public string DiretorioInspecaoLocal { get; set; }
        [Required(ErrorMessage = "O endereço(host) é obrigatório.")]
        public string HostURI { get; set; }
        [Required(ErrorMessage = "A porta é obrigatória.")]
        public string Porta { get; set; }
        [Required(ErrorMessage = "O Usuário é obrigatório.")]
        public string Usuario { get; set; }
        [Required(ErrorMessage = "A Senha é obrigatória.")]
        public string Senha { get; set; }

        public string PrivateKey { get; set; }

        public MensagemModel Mensagem { get; set; }

    }
}