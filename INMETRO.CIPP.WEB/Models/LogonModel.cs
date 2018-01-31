using System;
using System.ComponentModel.DataAnnotations;

namespace INMETRO.CIPP.WEB.Models
{
    public class LogonModel
    {
        [Required(ErrorMessage = "O campo Usuário é requerido")]
        [Display(Name = "Usuário")]
        public String Usuario { get; set; }

        [Required(ErrorMessage = "O campo Senha é requerido")]
        [Display(Name = "Senha")]
        public String Senha { get; set; }
    }
}