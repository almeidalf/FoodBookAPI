using AutoMapper;
using FoodBookAPI.V1.Models;
using FoodBookAPI.V1.Models.DTO;
using FoodBookAPI.V1.Repository.Interfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodBookAPI.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class UsuarioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUsuario _usuarioRepository;
        private readonly IToken _tokenRepository;
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        public UsuarioController(
                                 IMapper mapper,
                                 IUsuario usuarioRepository,
                                 IToken tokenRepository,
                                 UserManager<ApplicationUser> userManager,
                                 IConfiguration config
                                )
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _tokenRepository = tokenRepository;
            _userManager = userManager;
            _config = config;
        }


        [HttpPost("")]
        public ActionResult Cadastrar([FromBody] UsuarioDTO usuarioDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser usuario = _mapper.Map<UsuarioDTO, ApplicationUser>(usuarioDTO);

                var resultado = _userManager.CreateAsync(usuario, usuarioDTO.Senha).Result;

                if (!resultado.Succeeded)
                {
                    List<string> erros = new List<string>();
                    foreach (var erro in resultado.Errors)
                    {
                        erros.Add(erro.Description);
                    }
                    return UnprocessableEntity(erros);
                }
                else
                {
                    return Ok(usuario);
                }
            }
            else
            {
                return UnprocessableEntity(ModelState);
            }
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginUsuarioDTO usuarioDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser usuario = _usuarioRepository.Obter(usuarioDTO.Email, usuarioDTO.Senha);
                if (usuario != null)
                {
                    return GerarToken(usuario);
                }
                else
                {
                    return NotFound("Usuário não localizado!");
                }
            }
            else
            {
                return UnprocessableEntity(ModelState);
            }
        }

        [HttpPost("renovar")]
        public ActionResult Renovar([FromBody] TokenDTO tokenDTO)
        {
            var refreshTokenDB = _tokenRepository.Obter(tokenDTO.RefreshToken);

            if (refreshTokenDB == null)
                return NotFound();

            //RefreshToken antigo - Atualizar - Desativar esse refreshToken
            refreshTokenDB.Atualizado = DateTime.Now;
            refreshTokenDB.Utilizado = true;
            _tokenRepository.Atualizar(refreshTokenDB);

            //Gerar um novo Token/Refresh Token - Salvar.
            var usuario = _usuarioRepository.ObterUsuarioEspecifico(refreshTokenDB.UsuarioId);

            return GerarToken(usuario);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult BuscarUsuarioId(string id)
        {
            var usuario = _userManager.FindByIdAsync(id).Result;
            if(usuario != null)
            {
                return Ok(usuario);
            }
            else
            {
                return NotFound();
            }
        }


        private TokenDTO BuildToken(ApplicationUser usuario)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Id),
                new Claim(ClaimTypes.NameIdentifier, usuario.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["SecurityKeyAPI"]));
            var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var exp = DateTime.UtcNow.AddHours(7);

            JwtSecurityToken token = new JwtSecurityToken
                (
                    issuer: null,
                    audience: null,
                    claims: claims,
                    expires: exp,
                    signingCredentials: sign
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = Guid.NewGuid().ToString();
            var expRefreshToken = DateTime.UtcNow.AddDays(1);

            var tokenDTO = new TokenDTO { Token = tokenString, Expiration = exp, RefreshToken = refreshToken, ExpirationRefreshToken = expRefreshToken };

            return tokenDTO;
        }

        private ActionResult GerarToken(ApplicationUser usuario)
        {
            var token = BuildToken(usuario);

            var tokenModel = new Token()
            {
                RefreshToken = token.RefreshToken,
                ExpirationToken = token.Expiration,
                ExpirationRefreshToken = token.ExpirationRefreshToken,
                Usuario = usuario,
                Criado = DateTime.Now,
                Utilizado = false
            };

            _tokenRepository.Cadastrar(tokenModel);
            return Ok(token);
        }
    }
}
