using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace chapter.webapi.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Informe o e-mail do usuario")]
        public string email { get; set; }

        [Required(ErrorMessage = "Informe a senha do usuario")]
        public string senha { get; set; }

    }
}
