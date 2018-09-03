
namespace INMETRO.CIPP.WEB.Models
{
    public class IntegracaoInfoModel
    {
       
        public int Id { get; set; }

        public int OrganismoId { get; set; }

        public string DiretorioInspecao { get; set; }

        public int TipoIntegracao { get; set; }

        public string DiretorioInspecaoLocal { get; set; }

        public string HostURI { get; set; }

        public string Porta { get; set; }

        public string Usuario { get; set; }

        public string Senha { get; set; }

        public string PrivateKey { get; set; }

        public MensagemModel Mensagem { get; set; }

    }
}