using System.ComponentModel.DataAnnotations;

namespace INMETRO.CIPP.WEB.Models
{
    public class DownloadModel
    {
        [Required(ErrorMessage = "Campo Obrigatório")]
        [MaxLength(20)]
        public string CodigoOia { get; set; }
        public string CodigoCipp { get; set; }
    }
}