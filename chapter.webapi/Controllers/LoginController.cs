using chapter.webapi.Models;
using chapter.webapi.Repositories;
using chapter.webapi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace chapter.webapi.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]

    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;

        public LoginController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            try
            {
               Usuario usuarioBuscado = _usuarioRepository.Login(login.email, login.senha);

                if(usuarioBuscado == null)
                {
                    return NotFound("E-mail ou senha Invalidos");
                }

                var minhasClains = new[] {
                    new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.Id.ToString())
                    //new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.Tipo.ToString())
                };

                var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("chapter-chave-autenticacao"));

                var credencial = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var meuToken = new JwtSecurityToken(
                    issuer: "chapter.webApi",
                    audience: "chapter.webApi",
                    claims: minhasClains,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: credencial
                 );

                return Ok(
                    new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(meuToken),
                    }
                    
                    
                    
                    );
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
