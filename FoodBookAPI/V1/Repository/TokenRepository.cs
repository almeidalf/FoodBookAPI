using FoodBookAPI.DB;
using FoodBookAPI.V1.Models;
using FoodBookAPI.V1.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodBookAPI.V1.Repository
{
    public class TokenRepository : IToken
    {
        private readonly FoodBookContext _banco;
        public TokenRepository(FoodBookContext banco)
        {
            _banco = banco;
        }
        public void Atualizar(Token token)
        {
            _banco.Tokens.Update(token);
            _banco.SaveChanges();
        }

        public void Cadastrar(Token token)
        {
            _banco.Tokens.Add(token);
            _banco.SaveChanges();
        }

        public Token Obter(string refreshToken)
        {
            return _banco.Tokens.FirstOrDefault(a => a.RefreshToken == refreshToken && a.Utilizado == false);
        }
    }
}
