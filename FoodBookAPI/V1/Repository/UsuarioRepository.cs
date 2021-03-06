﻿using FoodBookAPI.V1.Models;
using FoodBookAPI.V1.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Text;

namespace FoodBookAPI.V1.Repository
{
    public class UsuarioRepository : IUsuario
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UsuarioRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public void Cadastrar(ApplicationUser usuario, string senha)
        {
            var result = _userManager.CreateAsync(usuario, senha).Result;
            if (!result.Succeeded)
            {
                StringBuilder sb = new StringBuilder();
                foreach(var erro in result.Errors)
                {
                    sb.Append(erro.Description);
                };
                throw new Exception($"Usuário não cadastrado! {sb.ToString()}");
            }
        }

        public ApplicationUser Obter(string email, string senha)
        {
            var usuario = _userManager.FindByEmailAsync(email).Result;
            if (_userManager.CheckPasswordAsync(usuario, senha).Result)
            {
                return usuario;
            }
            else
            {
                throw new Exception("Usuário não localizado!");
            }
        }

        public ApplicationUser ObterUsuarioEspecifico(string id)
        {
            return _userManager.FindByIdAsync(id).Result;
        }
    }
}
