using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBookAPI.V1.Models
{
    public class Receita
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(150)]
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string[] Ingredientes { get; set; }
        public string[] ModoPreparo { get; set; }
        public string UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        [MaxLength(75)]
        public ApplicationUser Usuario { get; set; }
    }
}
