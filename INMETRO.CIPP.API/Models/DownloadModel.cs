using System.ComponentModel.DataAnnotations;

namespace INMETRO.CIPP.API.Models
{
    public class DownloadModel
    {
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(20)]
        public string CodigoOia { get; set; }
        public string CodigoCipp { get; set; }
    }
}