﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FoodBookAPI.V1.Models;
using FoodBookAPI.V1.Models.DTO;
using FoodBookAPI.V1.Repository.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoodBookAPI.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class ComentarioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IComentarios _comentarioRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public ComentarioController(
                                 IMapper mapper,
                                 IComentarios comentarioRepository,
                                 UserManager<ApplicationUser> userManager
                                )
        {
            _mapper = mapper;
            _comentarioRepository = comentarioRepository;
            _userManager = userManager;
        }


        [HttpPost]
        public ActionResult Cadastrar([FromBody] ComentarioDTO comentarioDTO)
        {
            Comentarios comentario = _mapper.Map<ComentarioDTO, Comentarios>(comentarioDTO);
            if (comentario != null)
            {
                _comentarioRepository.Cadastrar(comentario);
                return Ok();
            }else if(comentario == null)
            {
                return BadRequest();
            }else
            {
                return UnprocessableEntity();
            }
        }

        [HttpGet]
        public ActionResult BuscarTodosComentarios()
        {
            return Ok(_comentarioRepository.ObterTodosComentarios());
        }

        [HttpPut]
        public ActionResult AtualizarComentario(int id,[FromBody] Comentarios comentario)
        {
            if(comentario != null)
            {
                comentario.Id = id;
                _comentarioRepository.Atualizar(comentario);
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        public ActionResult ExcluirComentario(int id)
        {
            _comentarioRepository.Excluir(id);
            return Ok();
        }
    }
}
