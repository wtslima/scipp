using System.ComponentModel.DataAnnotations;
using INMETRO.CIPP.SHARED;

namespace INMETRO.CIPP.WEB.Models
{
    public class DownloadModel
    {
        //[Required(ErrorMessage = MensagemSistema.CampoObrigatorio)]
        [RegularExpression("([0-9]+)", ErrorMessage = "Por favor digite um numero válido.")]
        [MaxLength(4)]
        public string CodigoOia { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "Por favor digite um numero válido.")]
        public string CodigoCipp { get; set; }

        public bool IsSuccess { get; set; }
        public string Mensagem { get; set; }
    }
}