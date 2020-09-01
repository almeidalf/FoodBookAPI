using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FoodBookAPI.V1.Models;
using FoodBookAPI.V1.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FoodBookAPI.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class ReceitaController : ControllerBase
    {
        private readonly IReceita _receitaRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public ReceitaController(IReceita receitaRepository,
                                UserManager<ApplicationUser> userManager)
        {
            _receitaRepository = receitaRepository;
            _userManager = userManager;
        }

        [HttpPost("", Name = "CadastrarReceita")]
        [Authorize]
        public ActionResult CadastrarReceita([FromBody] Receita receita)
        {
            if (receita != null)
            {
                ModelState.Remove("Usuario");
                _receitaRepository.Cadastrar(receita);
                return Ok();
            }
            else if (receita == null)
            {
                return BadRequest();
            }
            else
            {
                return UnprocessableEntity();
            }
        }

        [HttpGet]
        public ActionResult BuscarTodas()
        {
            return Ok(_receitaRepository.ObterTodasReceitas());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult BuscarEspecifica(int id)
        {
            var receita = _receitaRepository.ObterReceitaEspecifica(id);
            if (receita != null)
            {
                return Ok(receita);
            }
            else if (receita == null)
            {
                return NotFound();
            }
            else
            {
                return UnprocessableEntity();
            }
        }

        [HttpPut]
        [Route("{id}")]
        [Authorize]
        public ActionResult EditarReceita(int id, [FromBody]Receita receita)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var usuario = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (usuario != receita.UsuarioId)
            {
                return Forbid();
            }
            else
            {
                receita.Id = id;
               _receitaRepository.Atualizar(receita);
               return Ok();
            }
        }

        [HttpDelete]
        [Authorize]
        public ActionResult Excluir(int id)
        {
            _receitaRepository.Excluir(id);
            return Ok();
        }

    }
}
