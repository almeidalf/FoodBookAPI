using FoodBookAPI.DB;
using FoodBookAPI.V1.Models;
using FoodBookAPI.V1.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBookAPI.V1.Repository
{
    public class ReceitaRepository : IReceita
    {
        private readonly FoodBookContext _banco;
        public ReceitaRepository(FoodBookContext banco)
        {
            _banco = banco;
        }
        public Receita AtualizacaoCampos(Receita receita)
        {
            throw new NotImplementedException();
        }

        public Receita ObterReceitaEspecifica(int id)
        {
            var receita = _banco.Receitas.Find(id);
            return receita;
        }

        public List<Receita> ObterTodasReceitas()
        {
            return _banco.Receitas.ToList();
        }

        public void Atualizar(Receita receita)
        {
            _banco.Update(receita);
            _banco.SaveChanges();
        }

        public void Cadastrar(Receita receita)
        {
            _banco.Add(receita);
            _banco.SaveChanges();
        }

        public void Excluir(int id)
        {
            var verificarId = _banco.Receitas.Find(id);
            if(verificarId != null)
            {
                _banco.Remove(verificarId);
                _banco.SaveChanges();
            }
        }
    }
}
