using System;
using System.ComponentModel.DataAnnotations;

namespace INMETRO.CIPP.WEB.Models
{
    public class LogonModel
    {
        [Required(ErrorMessage = "O campo Usuário é obrigatório")]
        [Display(Name = "Usuário")]
        public string Usuario { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório")]
        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}