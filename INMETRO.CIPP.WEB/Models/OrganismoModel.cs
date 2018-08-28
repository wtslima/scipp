using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace INMETRO.CIPP.WEB.Models
{
    public class OrganismoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome do organismo é obrigatório")]
        [Display(Name = "NOME ORGANISMO")]
        public string  Nome { get; set; }
        [Required(ErrorMessage = "O código é obrigatório")]
        [Display(Name = "CÓDIGO-OIA-PP")]
        public string Codigo { get; set; }
        public bool Ativo { get; set; }
        public IntegracaoInfoModel Integracao { get; set; }

    }
}