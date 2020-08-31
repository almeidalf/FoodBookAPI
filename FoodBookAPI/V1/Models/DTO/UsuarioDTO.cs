using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBookAPI.V1.Models.DTO
{
    public class UsuarioDTO
    {
        [Required]
        public string Nome { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Senha { get; set; }
        [Required]
        [Compare("Senha")]
        public string ConfirmacaoSenha { get; set; }
    }
}
