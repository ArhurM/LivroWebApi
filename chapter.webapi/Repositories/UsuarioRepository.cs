using chapter.webapi.Contexts;
using chapter.webapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chapter.webapi.Repositories
{
    public class UsuarioRepository
    {
        private readonly ChapterContext _context;

        public UsuarioRepository(ChapterContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuarios.ToList();
        }

        public void Cadastrar(Usuario u)
        {
            _context.Usuarios.Add(u);
            _context.SaveChanges();
        }

        public Usuario BuscarPorID(int id) 
        {
           return _context.Usuarios.Find(id);
        }

        public void Atualizar(int id, Usuario u)
        {
            Usuario usuarioEncontado = _context.Usuarios.Find(id);

            if(usuarioEncontado != null) 
            {
                usuarioEncontado.Email = u.Email;
                usuarioEncontado.Senha = u.Senha;
               

            }

            _context.Usuarios.Update(usuarioEncontado);

            _context.SaveChanges();
        }

        public void Deletar(int id)
        {
            Usuario usuarioEncontrado = _context.Usuarios.Find(id);

            _context.Usuarios.Remove(usuarioEncontrado);

            _context.SaveChanges();

        }

        public Usuario Login(string email, string senha)
        {
            return _context.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);
        }
    }
}
