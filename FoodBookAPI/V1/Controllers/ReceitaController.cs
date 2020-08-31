using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodBookAPI.V1.Models;
using FoodBookAPI.V1.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodBookAPI.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class ReceitaController : ControllerBase
    {
        private readonly IReceita _receitaRepository;
        public ReceitaController(IReceita receitaRepository)
        {
            _receitaRepository = receitaRepository;
        }

        [HttpPost("", Name = "CadastrarReceita")]
        [Authorize]
        public ActionResult CadastrarReceita([FromBody]Receita receita)
        {
            if(receita != null)
            {
                ModelState.Remove("Usuario");
                _receitaRepository.Cadastrar(receita);
                return Ok();
            }
            else if(receita == null)
            {
                return BadRequest();
            }
            else
            {
                return UnprocessableEntity();
            }
        }
    }
}
