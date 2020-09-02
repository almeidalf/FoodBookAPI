using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBookAPI.V1.Models
{
    public class Comentarios
    {
        [Key]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Estrelas { get; set; }
        public string UsuarioId { get; set; }
        [ForeignKey("UsuarioId")]
        [MaxLength(75)]
        public ApplicationUser Usuario { get; set; }
        public int ReceitaId { get; set; }
        [ForeignKey("ReceitaId")]
        [MaxLength(75)]
        public Receita Receita { get; set; }
    }
}
