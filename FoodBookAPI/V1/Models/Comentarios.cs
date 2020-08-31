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
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Estrelas { get; set; }
        public string ReceitaId { get; set; }
        [ForeignKey("ReceitaId")]
        [MaxLength(85)]
        public Receita Receita { get; set; }
    }
}
