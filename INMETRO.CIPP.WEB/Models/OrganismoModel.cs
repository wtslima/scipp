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
        [Required(ErrorMessage = "O nome do organismo é obrigatório.")]
        [Display(Name = "NOME")]
        public string  Nome { get; set; }
        [Required(ErrorMessage = "O código é obrigatório.")]
        [MinLength(4, ErrorMessage ="O código deve ter 4 dígitos.")]
        [Display(Name = "CÓDIGO-OIA-PP")]
        public string Codigo { get; set; }
        [Required(ErrorMessage = "O LI é obrigatório.")]
        [MinLength(2, ErrorMessage ="O LI deve ter 2 dígitos")]
        [Display(Name = "LI")]
        public string LI { get; set; }

        public bool Ativo { get; set; }
        public IntegracaoInfoModel Integracao { get; set; }

        public  MensagemModel Mensagem { get; set; }

    }
}