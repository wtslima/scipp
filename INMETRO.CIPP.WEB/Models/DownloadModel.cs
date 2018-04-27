using System.ComponentModel.DataAnnotations;

namespace INMETRO.CIPP.WEB.Models
{
    public class DownloadModel
    {
        [Required(ErrorMessage = "O campo é obrigatório")]
        //[RegularExpression("([0-9]+)", ErrorMessage = "Por favor digite um numero válido.")]
        [MaxLength(7)]
        public string CodigoOia { get; set; }
        [RegularExpression("([0-9]+)", ErrorMessage = "Por favor digite um numero válido.")]
        public string CodigoCipp { get; set; }

        public string DataInspecao { get; set; }

        public string PlacaLicenca { get; set; }

    }
}