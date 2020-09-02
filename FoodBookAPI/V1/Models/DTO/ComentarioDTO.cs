using System.ComponentModel.DataAnnotations;


namespace FoodBookAPI.V1.Models.DTO
{
    public class ComentarioDTO
    {
        [Required]
        public string Descricao { get; set; }
        [Required]
        public string Estrelas { get; set; }
        [Required]
        public string UsuarioId { get; set; }
        [Required]
        public int ReceitaId { get; set; }
    }
}
