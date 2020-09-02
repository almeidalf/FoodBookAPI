using FoodBookAPI.V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBookAPI.V1.Repository.Interfaces
{
    public interface IComentarios
    {
        void Cadastrar(Comentarios comentario);
        void Atualizar(Comentarios comentario);
        List<Comentarios> ObterTodosComentarios();
        void Excluir(int id);
    }
}
