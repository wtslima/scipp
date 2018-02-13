using System.ComponentModel.DataAnnotations;

namespace INMETRO.CIPP.WEB.Models
{
    public class DownloadModel
    {
        [Required(ErrorMessage = "Campo Obrigatório")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Por favor digite um numero válido.")]
        [MaxLength(4)]
        public string CodigoOia { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "Por favor digite um numero válido.")]
        public string CodigoCipp { get; set; }
    }
}