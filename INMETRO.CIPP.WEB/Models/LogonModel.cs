using System;
using System.ComponentModel.DataAnnotations;

namespace INMETRO.CIPP.WEB.Models
{
    public class LogonModel
    {
        [Required(ErrorMessage = "O campo Usuário é obrigatório")]
        [Display(Name = "Usuário")]
        public String Usuario { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public String Senha { get; set; }
    }
}