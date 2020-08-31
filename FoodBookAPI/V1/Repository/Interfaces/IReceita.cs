using FoodBookAPI.V1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBookAPI.V1.Repository.Interfaces
{
    public interface IReceita
    {
        void Cadastrar(Receita receita);
        void Atualizar(Receita receita);
        Receita AtualizacaoCampos(Receita receita);
        List<Receita> ObterTodasReceitas();
        Receita ObterReceitaEspecifica(int id);
        void Excluir(int id);
    }
}
