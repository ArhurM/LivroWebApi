using chapter.webapi.Models;
using chapter.webapi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chapter.webapi.Controllers
{
    [Produces("application/json")]

    [Route("api/[controller]")]

    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuariosController(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;

        }

        [HttpGet]

        public IActionResult Listar()
        {
            try
            {
               return Ok( _usuarioRepository.Listar());
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
           
        }

        [HttpGet("{id}")]
        public IActionResult BuscarPorID(int id)
        {
            try
            {
                Usuario usuarioProcurado = _usuarioRepository.BuscarPorID(id);

                if (usuarioProcurado == null)
                {
                    return NotFound("Usuario não Econtrado");
                }
                return Ok(usuarioProcurado);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        public IActionResult Cadastrar(Usuario u) 
        {
            try
            {
                _usuarioRepository.Cadastrar(u);

                return StatusCode(201);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [HttpPut("{id}")]

        public IActionResult Atualizar(int id, Usuario u)
        {
            try
            {
                _usuarioRepository.Atualizar(id, u);

                return StatusCode(204);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
        }

        [HttpDelete("{id}")]

        public IActionResult Deletar(int id)
        {
            try
            {
                _usuarioRepository.Deletar(id);

                return StatusCode(204);

            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }


        }

    }
}
