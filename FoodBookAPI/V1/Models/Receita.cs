using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBookAPI.V1.Models
{
    public class Receita
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Ingredientes { get; set; }
        public string UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        public ApplicationUser Usuario { get; set; }

    }
}
