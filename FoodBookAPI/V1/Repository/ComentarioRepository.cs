using FoodBookAPI.DB;
using FoodBookAPI.V1.Models;
using FoodBookAPI.V1.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBookAPI.V1.Repository
{
    public class ComentarioRepository : IComentarios
    {
        private readonly FoodBookContext _banco;
        public ComentarioRepository(FoodBookContext banco)
        {
            _banco = banco;
        }
        public void Cadastrar(Comentarios comentario)
        {
            _banco.Comentarios.Add(comentario);
            _banco.SaveChanges();
        }
        public void Atualizar(Comentarios comentario)
        {
            _banco.Comentarios.Update(comentario);
            _banco.SaveChanges();
        }

        public List<Comentarios> ObterTodosComentarios()
        {
           return _banco.Comentarios.ToList();
        }

        public void Excluir(int id)
        {
            var idComentario = _banco.Comentarios.Find(id);
            if(idComentario != null)
            {
                _banco.Comentarios.Remove(idComentario);
                _banco.SaveChanges();
            }
        }
    }
}
